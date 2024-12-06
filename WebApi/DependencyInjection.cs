


using WebApi.Mapper;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection service)
    {
        service.AddRouting();
        service.AddControllers();
        service.AddAutoMapper(typeof(AppMappingProfile));
        service.AddRazorPages();
        return service;
    }
}