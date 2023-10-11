using Application.Interfaces;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ToolController : ControllerBase
{
    private readonly IToolService _toolService;

    public ToolController(IToolService toolService)
    {
        _toolService = toolService;
    }

    [AllowAnonymous]
    [HttpGet("news")]
    public async Task<IActionResult> GetAllTools()
    {
        var toolsToDisplay = new List<Tool>();
        var visibleNews = await _toolService.GetAllTools();

        foreach (var news in visibleNews.Data)
        {
            toolsToDisplay.Add(new Tool
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                Image = news.Image,
            });
        }

        return Ok(toolsToDisplay);
    }

    [AllowAnonymous]
    [HttpGet("news/{id:Guid}")]
    public async Task<IActionResult> GetToolById(Guid id)
    {
        try
        {
            var news = await _toolService.GetToolById(id);

            var singleNews = new Tool
            {
                Id = news.Data.Id,
                Name = news.Data.Name,
                Description = news.Data.Description,
                Image = news.Data.Image,
            };

            return Ok(singleNews);
        }
        catch (Exception ex)
        {
            var erroresValidacion = new List<string>() { ex.Message };
            return Ok(new ValidationDto { SavedSuccessfully = false, ValidationErrors = erroresValidacion });
        }
    }
}
