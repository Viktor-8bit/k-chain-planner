



using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection service)
    {
        service.AddDbContext<ApplicationContext>();
        return service;
    }
}