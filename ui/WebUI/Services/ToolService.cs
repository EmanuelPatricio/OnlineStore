using Domain.Entities;
using WebUI.Helpers;
using WebUI.HttpClientUtilities;

namespace WebUI.Services;

public interface IToolService
{
    Task<List<Tool>> GetAllTools();
    Task<Tool> GetToolById(Guid id);
}

public class ToolService : IToolService
{
    private readonly ApiHttpClient _apiHttpClient;

    public ToolService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public Task<List<Tool>> GetAllTools()
    {
        return _apiHttpClient.SendRequest<List<Tool>>(HttpMethod.Get, ApiRoutes.Tool.Get);
    }

    public Task<Tool> GetToolById(Guid id)
    {
        return _apiHttpClient.SendRequest<Tool>(HttpMethod.Get, $"{ApiRoutes.Tool.Get}/{id}");
    }
}
