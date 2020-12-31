using AspNetCoreHero.Application.Enums.Services;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class CurriculumVitaeRepositoryAsync : GenericRepositoryAsync<CurriculumVitae>, ICurriculumVitaeRepositoryAsync
    {
        private readonly DbSet<CurriculumVitae>  _category;

        public CurriculumVitaeRepositoryAsync(ApplicationContext dbContext, Func<CacheTech, ICacheService> cacheService) : base(dbContext, cacheService)
        {
            _category = dbContext.Set<CurriculumVitae>();
        }
    }
}
