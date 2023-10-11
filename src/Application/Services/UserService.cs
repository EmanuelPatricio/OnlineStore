using Application.Core.Repositories;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Specifications;
using System.Data;

namespace Application.Services;
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ValidateUser(UserLoginReq req)
    {
        var validateUserSpec = UserSpecifications.GetUserByEmailAndPasswordSpec(req.Email, EncodePasswordToBase64(req.Password));

        var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(validateUserSpec);

        if (user is null)
        {
            return false;
        }

        return true;
    }

    public async Task<GetAllUsersRes> GetAllUsers()
    {
        try
        {
            var activeUsersSpec = UserSpecifications.GetAllUsersSpec();
            var users = await _unitOfWork.Repository<User>().ListAsync(activeUsersSpec);

            return new GetAllUsersRes()
            {
                Data = users.Select(x => new UserDTO(x)).ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CreateUserRes> CreateUser(CreateUserReq req)
    {
        var user = await _unitOfWork.Repository<User>().AddAsync(new User
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Email = req.Email,
            Password = EncodePasswordToBase64(req.Password),
            Role = req.Role,
            Cart = new()
        });

        await _unitOfWork.SaveChangesAsync();

        return new CreateUserRes()
        {
            Data = new UserDTO(user)
        };
    }

    public async Task<bool> AddToolToCart(AddToolToCartReq req)
    {
        var getUserByIdSpec = UserSpecifications.GetUserByIdSpec(req.UserId);

        var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(getUserByIdSpec);

        if (user is null)
        {
            return false;
        }

        user.Cart.Add(new()
        {
            Id = req.Tool.Id,
            Name = req.Tool.Name,
            Description = req.Tool.Description,
            Image = req.Tool.Image,
        });

        return true;
    }

    //code from https://www.c-sharpcorner.com/blogs/how-to-encrypt-or-decrypt-password-using-asp-net-with-c-sharp1
    private static string EncodePasswordToBase64(string password)
    {
        var encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
        string encodedData = Convert.ToBase64String(encData_byte);
        return encodedData;
    }
}
