﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; set; }
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync(bool isCached = false);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
