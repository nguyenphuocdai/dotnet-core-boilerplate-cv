﻿using AspNetCoreHero.Application.Features.ProductCategories.Commands.Create;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Update;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Areas.ProductCategories.ViewModels;
using AutoMapper;

namespace AspNetCoreHero.Web.Mappings
{
    class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<GetAllProductCategoryViewModel, ProductCategoryViewModel>().ReverseMap();
            CreateMap<CreateProductCategoryCommand, ProductCategoryViewModel>().ReverseMap();
            CreateMap<ProductCategoryViewModel, UpdateProductCategoryCommand>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
        }
    }
}
