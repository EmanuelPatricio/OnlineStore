﻿using Application.Core.Repositories;
using Domain.Core.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    protected readonly OnlineStoreContext _dbContext;
    private readonly IDictionary<Type, dynamic> _repositories;

    public UnitOfWork(OnlineStoreContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, dynamic>();
    }

    public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
    {
        var entityType = typeof(T);
        if (_repositories.ContainsKey(entityType))
        {
            return _repositories[entityType];
        }

        var repositoryType = typeof(BaseRepositoryAsync<>);
        var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

        if (repository is null)
        {
            return default!;
        }

        _repositories.Add(entityType, repository);
        return (IBaseRepositoryAsync<T>)repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task RollBackChangesAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}
