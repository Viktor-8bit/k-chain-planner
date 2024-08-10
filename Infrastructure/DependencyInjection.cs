



using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection service)
    {
        service.AddDbContext<ApplicationContext>();
        
        service.AddScoped<IChainRepository, ChainRepository>();
        service.AddScoped<ITagRepository, TagRepository>();
        service.AddScoped<IChainStepRepository, ChainStepRepository>();
        
        return service;
    }
}