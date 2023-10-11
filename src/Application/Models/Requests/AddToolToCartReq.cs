using Application.Models.DTOs;

namespace Application.Models.Requests;
public class AddToolToCartReq
{
    public Guid UserId { get; set; }
    public required ToolDTO Tool { get; set; }
}
