﻿using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Entities;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<PagedResponse<Product>> GetAllWithCategoriesAsync(int pageNumber, int pageSize, bool isCached = false);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
