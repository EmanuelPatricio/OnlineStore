using Application.Models.DTOs;

namespace Application.Models.Responses;
public class CreateUserRes
{
    public required UserDTO Data { get; set; }
}
