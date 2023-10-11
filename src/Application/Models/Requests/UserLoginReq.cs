namespace Application.Models.Requests;
public class UserLoginReq
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string JwtToken { get; set; } = string.Empty;
}
