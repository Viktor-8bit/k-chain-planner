



using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    
    
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {

        var assembly = typeof(DependencyInjection).Assembly;
        
        service.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(assembly));

        service.AddValidatorsFromAssembly(assembly);

        return service;
    
    }
}