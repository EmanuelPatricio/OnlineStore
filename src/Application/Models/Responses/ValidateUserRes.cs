namespace Application.Models.Responses;
public class ValidateUserRes
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
