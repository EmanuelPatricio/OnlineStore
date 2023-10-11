using WebUI.Services;

namespace WebUI.DependencyResolver;

public static class DependencyResolverService
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IToolService, ToolService>();
    }
}
