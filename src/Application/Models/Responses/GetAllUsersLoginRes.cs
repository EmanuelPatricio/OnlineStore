using Application.Models.Requests;

namespace Application.Models.Responses;
public class GetAllUsersLoginRes
{
    public required IList<UserLoginReq> Data { get; set; }
}
