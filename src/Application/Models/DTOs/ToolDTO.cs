using Domain.Entities;

namespace Application.Models.DTOs;
public class ToolDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }

    public ToolDTO(Tool tool)
    {
        Id = tool.Id;
        Name = tool.Name;
        Description = tool.Description;
        Image = tool.Image;
    }
}
