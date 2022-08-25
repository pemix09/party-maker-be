using System.Configuration;
using System.Reflection;
using Application.Message.Commands;
using Core.Models;
using Infrastructure.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DbContext;
using Persistence.Services.Database;
using Persistence.UnitOfWork;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddDbContext<PartyMakerDbContext>(options =>
{
    options.UseNpgsql(GetConnectionString());

});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

})
.AddEntityFrameworkStores<PartyMakerDbContext>()
.AddRoles<IdentityRole>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<BanService>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddMediatR(typeof(CreateEventCommand).Assembly);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(GetFrontEndAddress(), "http://localhost:7101")
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials()
);
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

CreateRoles(app).Wait();
app.Run();

string GetConnectionString()
{
    List<string> dataBaseConfig = configuration.GetSection("DataBase").Get<List<string>>();
    string connectionString = "";

    foreach (string key in dataBaseConfig)
    {
        connectionString += key;
        connectionString += ";";
    }
    
    return connectionString;
}

string GetFrontEndAddress()
{
    string frontAddress = configuration.GetSection("FrontEndAddress").Key;

    return frontAddress;
}

async Task CreateRoles(IApplicationBuilder _app)
{
    using (var scope = _app.ApplicationServices.CreateScope())
    {
        var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

        List<string> roleNames = configuration.GetSection("Roles").Get<List<string>>();

        foreach (string role in roleNames)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
