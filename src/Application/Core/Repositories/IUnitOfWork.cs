using Domain.Core.Models;

namespace Application.Core.Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}
