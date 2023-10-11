using Domain.Entities;
using Domain.Specifications;
using Infrastructure.Repositories;

namespace Domain.UnitTests.Specifications;
public class UserSpecificationsTest
{
    private readonly List<User> _users;

    public UserSpecificationsTest()
    {
        _users = new()
        {
            new User
            {
                FirstName = "Nilav",
                LastName = "Patel",
                Email = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Cart = new()
            },
            new User
            {
                FirstName = "Nilav1",
                LastName = "Patel",
                Email = "nilav1patel1992@gmail.com",
                Password = "Test1234",
                Cart = new()
            },
            new User
            {
                FirstName = "Nilav2",
                LastName = "Patel",
                Email = "nilav2patel1992@gmail.com",
                Password = "Test1235",
                Cart = new()
            }
        };
    }

    [Fact]
    public void Given_ValidData_When_GetUserByEmailAndPasswordSpec_Then_ReturnValidData()
    {
        // Arrange
        var spec = UserSpecifications.GetUserByEmailAndPasswordSpec("nilavpatel1992@gmail.com", "Test123");

        // Act
        var count = SpecificationEvaluator<User>.GetQuery(_users.AsQueryable(), spec).Count();

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void Given_ValidData_When_GetAllUsersSpec_Then_ReturnValidData()
    {
        // Arrange
        var spec = UserSpecifications.GetAllUsersSpec();

        // Act
        var count = SpecificationEvaluator<User>.GetQuery(_users.AsQueryable(), spec).Count();

        // Assert
        Assert.Equal(3, count);
    }
}
