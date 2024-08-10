


using WebApi.Mapper;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection service)
    {
        service.AddRouting(); // options => options.LowercaseUrls = true
        service.AddControllers();
        service.AddAutoMapper(typeof(AppMappingProfile));
        return service;
    }
}