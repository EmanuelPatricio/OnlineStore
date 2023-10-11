using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static Domain.Enums.UserEnums;

namespace Infrastructure.UnitTests.Repositories;
public class BaseRepositoryAsyncTest
{
    private readonly OnlineStoreContext _onlineStoreContext;
    private readonly UnitOfWork _unitOfWork;

    public BaseRepositoryAsyncTest()
    {
        var options = new DbContextOptionsBuilder<OnlineStoreContext>().UseInMemoryDatabase(databaseName: "OnlineStore").Options;
        _onlineStoreContext = new OnlineStoreContext(options);
        _unitOfWork = new UnitOfWork(_onlineStoreContext);
    }

    [Fact]
    public async void Given_ValidData_When_AddAsync_Then_SuccessfullyInsertData()
    {
        // Arrange
        var user = new User
        {
            FirstName = "Nilav",
            LastName = "Patel",
            Email = "nilavpatel1992@gmail.com",
            Password = "Test123",
            Role = UserRole.Guest,
            Cart = new()
        };

        // Act
        var result = await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Assert
        Assert.Equal(result, _onlineStoreContext.Users.Find(result.Id));
    }
}
