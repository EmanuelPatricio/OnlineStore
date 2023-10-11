using Domain.Core.Models;
using static Domain.Enums.UserEnums;

namespace Domain.Entities;
public class User : BaseEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public UserRole Role { get; set; }
    public required List<Tool> Cart { get; set; }
}
