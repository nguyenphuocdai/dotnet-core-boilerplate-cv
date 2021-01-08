using AspNetCoreHero.Web.Areas.Admin.ViewModels;
using AutoMapper;
using System.Security.Claims;

namespace AspNetCoreHero.Web.Mappings
{
    public class ClaimsProfile : Profile
    {
        public ClaimsProfile()
        {
            CreateMap<Claim, RoleClaimsViewModel>().ReverseMap();
        }   
    }
}
