using Application.Models.DTOs;
using Application.Models.Requests;
using WebUI.Helpers;
using WebUI.HttpClientUtilities;

namespace WebUI.Services;

public interface IUserService
{
    Task<ValidationDto> CreateUser(CreateUserReq req);
    Task<ValidationDto> AddToolToCart(AddToolToCartReq req);
}

public class UserService : IUserService
{
    private readonly ApiHttpClient _apiHttpClient;

    public UserService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public Task<ValidationDto> AddToolToCart(AddToolToCartReq req)
    {
        return _apiHttpClient.SendRequest<ValidationDto>(HttpMethod.Post, ApiRoutes.Users.AddToolToCart);
    }

    public Task<ValidationDto> CreateUser(CreateUserReq req)
    {
        return _apiHttpClient.SendRequest<ValidationDto>(HttpMethod.Post, ApiRoutes.Users.Create, req);
    }
}
