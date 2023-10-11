using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyResolver;
public static class DependencyResolverService
{
    public static void Register(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IToolService, ToolService>();
    }
}
