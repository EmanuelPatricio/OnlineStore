using Application.Models.Requests;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Helpers;
using WebUI.Services;
using WebUI.Session;

namespace WebUI.Pages;

public partial class Login
{
    [Inject]
    public required AuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    [Inject]
    public required SessionHelper Session { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }

    public required Validations LoginValidationsRef;
    public required Validations RegisterValidationsRef;

    private string _selectedTab = "login";

    private CreateUserReq _createUserReq = new()
    {
        FirstName = string.Empty,
        LastName = string.Empty,
        Email = string.Empty,
        ConfirmPassword = string.Empty,
        Password = string.Empty,
        Role = Domain.Enums.UserEnums.UserRole.Buyer
    };

    private async Task OnLoginClicked()
    {
        if (await LoginValidationsRef.ValidateAll())
        {
            var state = await AuthStateProvider.GetAuthenticationStateAsync();

            if (state.User.Claims.ToList().Any())
            {
                await LoginValidationsRef.ClearAll();
                NavigationManager.NavigateTo(PagesRoutes.DefaultNavigation.Home, true);
            }
        }
    }

    private async Task OnRegisterClicked()
    {
        if (!await RegisterValidationsRef.ValidateAll())
        {
            return;
        }

        var validation = await UserService.CreateUser(_createUserReq);

        if (validation.SavedSuccessfully)
        {
            _createUserReq = new()
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                ConfirmPassword = string.Empty,
                Password = string.Empty,
                Role = Domain.Enums.UserEnums.UserRole.Buyer
            };
            await RegisterValidationsRef.ClearAll();
        }
    }

    private Task OnSelectedTabChanged(string name)
    {
        _selectedTab = name;

        return Task.CompletedTask;
    }
}