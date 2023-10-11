using Domain.Core.Specifications;
using Domain.Entities;

namespace Domain.Specifications;
public static class UserSpecifications
{
    public static BaseSpecification<User> GetUserByIdSpec(Guid id)
    {
        return new BaseSpecification<User>(x => x.Id == id);
    }

    public static BaseSpecification<User> GetUserByEmailAndPasswordSpec(string email, string password)
    {
        return new BaseSpecification<User>(x => x.Email == email && x.Password == password);
    }

    public static BaseSpecification<User> GetAllUsersSpec()
    {
        return new BaseSpecification<User>(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}
