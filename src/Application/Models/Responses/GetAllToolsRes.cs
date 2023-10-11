using Application.Models.DTOs;

namespace Application.Models.Responses;
public class GetAllToolsRes
{
    public IList<ToolDTO> Data { get; set; }
}
