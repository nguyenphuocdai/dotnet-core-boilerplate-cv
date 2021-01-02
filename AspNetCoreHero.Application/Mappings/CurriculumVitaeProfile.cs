using AspNetCoreHero.Application.Features.CurriculumVitae.Commands.Create;
using AspNetCoreHero.Application.Features.CurriculumVitae.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;

namespace AspNetCoreHero.Application.Mappings
{
    class CurriculumVitaeProfile : Profile
    {
        public CurriculumVitaeProfile()
        {
            //CreateMap<CreateCurriculumVitaeCommand, CurriculumVitae>();
            //CreateMap<CurriculumVitae, GetAllCurriculumVitaeViewModel>().ReverseMap();
            CreateMap<CurriculumVitae, GetAllCurriculumVitaeViewModel>().ReverseMap();
            CreateMap<CurriculumVitae, CreateCurriculumVitaeCommand>().ReverseMap();
        }
    }
}
