using Application.Models.DTOs;

namespace Application.Models.Responses;
public class GetAllUsersRes
{
    public required IList<UserDTO> Data { get; set; }
}
