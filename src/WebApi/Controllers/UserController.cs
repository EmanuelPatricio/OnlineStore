using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("User/add-tool")]
    public async Task<IActionResult> AddToolToCart(AddToolToCartReq req)
    {
        try
        {
            _ = await _userService.AddToolToCart(req);
            return Ok(new ValidationDto { SavedSuccessfully = true });
        }
        catch (Exception ex)
        {
            var erroresValidacion = new List<string>() { ex.Message };
            return Ok(new ValidationDto { SavedSuccessfully = false, ValidationErrors = erroresValidacion });
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserReq user)
    {
        try
        {
            _ = await _userService.CreateUser(user);
            return Ok(new ValidationDto { SavedSuccessfully = true });
        }
        catch (Exception ex)
        {
            var erroresValidacion = new List<string>() { ex.Message };
            return Ok(new ValidationDto { SavedSuccessfully = false, ValidationErrors = erroresValidacion });
        }
    }
}
