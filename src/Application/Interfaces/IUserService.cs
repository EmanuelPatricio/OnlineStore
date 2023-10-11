using Application.Models.Requests;
using Application.Models.Responses;

namespace Application.Interfaces;
public interface IUserService
{
    Task<CreateUserRes> CreateUser(CreateUserReq req);
    Task<GetAllUsersRes> GetAllUsers();
    Task<bool> ValidateUser(UserLoginReq req);
    Task<bool> AddToolToCart(AddToolToCartReq req);
}
