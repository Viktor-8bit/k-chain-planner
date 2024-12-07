



using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection service, string connectionString)
    {
        
        service.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
        
        service.AddScoped<IChainRepository, ChainRepository>();
        service.AddScoped<ITagRepository, TagRepository>();
        service.AddScoped<IChainStepRepository, ChainStepRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        
        return service;
    }
}