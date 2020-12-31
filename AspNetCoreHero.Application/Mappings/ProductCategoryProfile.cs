using AspNetCoreHero.Application.Features.ProductCategories.Commands.Create;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;

namespace AspNetCoreHero.Application.Mappings
{
    class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, GetAllProductCategoryViewModel>().ReverseMap();
            CreateMap<ProductCategory, CreateProductCategoryCommand>().ReverseMap();
        }
    }
}
