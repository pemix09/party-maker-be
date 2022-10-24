using System.Configuration;
using System.Reflection;
using Application.Message.Commands;
using AutoMapper;
using Core.Mapper;
using Core.Models;
using Infrastructure.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DbContext;
using Persistence.Services.Database;
using Persistence.UnitOfWork;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Hubs;
using Infrastructure.NotificationsHandlers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Persistence.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Persistence.Exceptions;

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
.AddRoles<IdentityRole>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtOptions =>
    {
        JwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name"
        };
        JwtOptions.IncludeErrorDetails = true;
        JwtOptions.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration["JWT:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true
        };
        JwtOptions.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async context =>
            {
                var ex = new UserNotAuthenticatedException();
                throw ex;
            },
            OnChallenge = async context =>
            {
                var ex = new UserNotLoggedException();
                throw ex;
            },
            OnForbidden = async context =>
            {
                var ex = new UserNotAuthorizedException();
                throw ex;
            }
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<BanService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EntrancePassService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddSignalR();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddMediatR(typeof(CreateEventCommand).Assembly, typeof(MessageSentNotificationHandler).Assembly);
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
        Description = "Authorization header, schema: (\"Bearer {token}\")",
        In = ParameterLocation.Header, 
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AllMappersProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();
app.MapHub<MessageHub>("/Messages");

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
AddSuperAdmins(app).Wait();
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

async Task AddSuperAdmins(IApplicationBuilder _app)
{
    using (var scope = _app.ApplicationServices.CreateScope())
    {
        var AdminService = (AdminService)scope.ServiceProvider.GetService(typeof(AdminService));
        await AdminService.AddSuperAdmins();
    }
}
