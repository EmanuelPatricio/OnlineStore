using Application.Models.Responses;

namespace Application.Interfaces;
public interface IToolService
{
    Task<GetAllToolsRes> GetAllTools();
    Task<GetSingleToolRes> GetToolById(Guid id);
}
