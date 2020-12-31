using AspNetCoreHero.Application.Features.CurriculumVitae.Commands.Create;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;

namespace AspNetCoreHero.Application.Mappings
{
    class CurriculumVitaeProfile : Profile
    {
        public CurriculumVitaeProfile()
        {
            CreateMap<CreateCurriculumVitaeCommand, CurriculumVitae>();
        }
    }
}
