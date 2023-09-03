﻿using Tempus.Core.Entities;

namespace Tempus.Core.Interfaces;

public interface IRepository<T> : IEnumerable<T>, IQueryable<T>, IAsyncEnumerable<T> where T : EntityBase
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();
    void RemoveById(Guid entityId);
}