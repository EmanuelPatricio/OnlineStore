using Domain.Entities;

namespace Application.Models.DTOs;
public class UserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    public string RoleText { get; set; }
    public List<Tool> Cart { get; set; }

    public UserDTO(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Role = (int)user.Role;
        RoleText = user.Role.ToString();
        Cart = user.Cart;
    }
}