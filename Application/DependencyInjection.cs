



using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;

namespace Application;

public static class DependencyInjection
{
   
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {

        var assembly = typeof(DependencyInjection).Assembly;
        
        service.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(assembly));

        service.AddValidatorsFromAssembly(assembly);

        service.AddScoped<ChainService>();
        service.AddScoped<ChainStepService>();
        service.AddScoped<TagService>();
        service.AddScoped<UserService>();        
        
        return service;
    
    }
}