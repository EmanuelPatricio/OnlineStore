using Application.Core.Repositories;
using Application.Models.Requests;
using Application.Services;
using Domain.Core.Specifications;
using Domain.Entities;
using Moq;
using static Domain.Enums.UserEnums;

namespace Application.UnitTests.Services;
public class UserServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly UserService _userService;

    public UserServiceTest()
    {
        _userService = new UserService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Given_WithValidData_When_CreateUser_Then_SuccessfullyCreateUser()
    {
        // Arrange
        var id = Guid.NewGuid();
        _unitOfWorkMock.Setup(x => x.Repository<User>().AddAsync(It.IsAny<User>()))
            .ReturnsAsync(new User
            {
                Id = id,
                FirstName = "Nilav",
                LastName = "Patel",
                Email = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Role = UserRole.Guest,
                Cart = new()
            });

        _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _userService.CreateUser(new CreateUserReq
        {
            FirstName = "Nilav",
            LastName = "Patel",
            Email = "nilavpatel1992@gmail.com",
            Password = "Test123",
            ConfirmPassword = "Test123",
            Role = UserRole.Guest,
        });

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(id, result.Data.Id);
    }

    [Fact]
    public async Task Given_UserNotExist_When_ValidateUser_Then_ThrowException()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
            .ReturnsAsync(null as User);

        // Act
        Assert.False(await Task.Run(() => _userService.ValidateUser(new UserLoginReq
        {
            Email = "nilavpatel1992@gmail.com",
            Password = "Test123"
        })));
    }

    [Fact]
    public async Task Given_ValidData_When_ValidateUser_Then_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        User user = new()
        {
            Id = id,
            FirstName = "Nilav",
            LastName = "Patel",
            Email = "nilavpatel1992@gmail.com",
            Password = "Test123",
            Role = UserRole.Guest,
            Cart = new()
        };

        _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.ValidateUser(new UserLoginReq
        {
            Email = "nilavpatel1992@gmail.com",
            Password = "Test123"
        });

        // Assert
        Assert.True(result);
    }
}
