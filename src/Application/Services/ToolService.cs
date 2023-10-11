using Application.Core.Repositories;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Specifications;

namespace Application.Services;
public class ToolService : IToolService
{
    private readonly IUnitOfWork _unitOfWork;

    public ToolService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllToolsRes> GetAllTools()
    {
        var getAllToolsSpec = ToolSpecifications.GetAllToolsSpec();
        var tools = await _unitOfWork.Repository<Tool>().ListAsync(getAllToolsSpec);

        return new GetAllToolsRes()
        {
            Data = tools.Select(x => new ToolDTO(x)).ToList()
        };
    }

    public async Task<GetSingleToolRes> GetToolById(Guid id)
    {
        var getToolByIdSpec = ToolSpecifications.GetToolByIdSpec(id);
        var tool = await _unitOfWork.Repository<Tool>().FirstOrDefaultAsync(getToolByIdSpec)
            ?? throw new Exception("Tool not found in database");

        return new GetSingleToolRes()
        {
            Data = new ToolDTO(tool)
        };
    }
}
