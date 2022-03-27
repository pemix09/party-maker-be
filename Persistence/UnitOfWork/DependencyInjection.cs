using System.Net.NetworkInformation;
using Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.UnitOfWork;

public static class DependencyInjection
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        //I don't know if we have to add below thing
        //services.AddTransient<IEventRepository, EventRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}