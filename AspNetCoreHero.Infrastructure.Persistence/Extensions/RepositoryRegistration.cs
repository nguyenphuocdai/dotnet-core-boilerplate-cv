using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreHero.Infrastructure.Persistence.Extensions
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IProductCategoryRepositoryAsync, ProductCategoryRepositoryAsync>();
            services.AddTransient<ICurriculumVitaeRepositoryAsync, CurriculumVitaeRepositoryAsync>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
