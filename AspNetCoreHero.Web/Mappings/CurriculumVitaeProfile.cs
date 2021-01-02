using AspNetCoreHero.Application.Features.CurriculumVitae.Commands.Create;
using AspNetCoreHero.Application.Features.CurriculumVitae.Queries.GetAll;
using AspNetCoreHero.Web.Areas.CurriculumVitae.ViewModels;
using AutoMapper;

namespace AspNetCoreHero.Web.Mappings
{
    class CurriculumVitaeProfile : Profile
    {
        public CurriculumVitaeProfile()
        {
            CreateMap<CreateCurriculumVitaeCommand, CurriculumVitaeViewModel>().ReverseMap();
            CreateMap<GetAllCurriculumVitaeViewModel, CurriculumVitaeViewModel>().ReverseMap();
        }
    }
}
