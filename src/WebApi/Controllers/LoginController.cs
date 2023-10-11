using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public LoginController(IConfiguration config, IUserService userService)
    {
        _config = config;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginReq userLogin)
    {
        var user = await Authenticate(userLogin);

        if (user is null)
        {
            return NotFound("User not found");
        }

        userLogin.Id = user.Id;
        userLogin.JwtToken = GenerateToken(user);

        return Ok(userLogin);
    }

    // To generate token
    private string GenerateToken(UserDTO user)
    {
        var key = _config["Jwt:Key"] ?? throw new Exception("Key not found");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleText)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                         _config["Jwt:Audience"],
                                         claims,
                                         expires: DateTime.Now.AddMinutes(15),
                                         signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //To authenticate user
    private async Task<UserDTO?> Authenticate(UserLoginReq userLogin)
    {
        var result = await _userService.GetAllUsers();

        var isUserValid = await _userService.ValidateUser(userLogin);

        if (!isUserValid)
        {
            return null;
        }

        var currentUser = result.Data.FirstOrDefault(x => x.Email == userLogin.Email);

        return currentUser;
    }
}
