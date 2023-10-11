using Application.Models.DTOs;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Components;
using WebUI.Services;
using WebUI.Session;
using IToolService = Application.Interfaces.IToolService;

namespace WebUI.Pages;

public partial class Index
{
    [Inject]
    public required SessionHelper Session { get; set; }
    [Inject]
    public required IToolService ToolService { get; set; }
    [Inject]
    public required IUserService UserService { get; set; }

    private GetAllToolsRes Tools { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await base.OnInitializedAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                Tools = await ToolService.GetAllTools();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task AddToolToCart(ToolDTO tool)
    {
        await UserService.AddToolToCart(new AddToolToCartReq()
        {
            UserId = Session.Id,
            Tool = tool
        });
    }
}