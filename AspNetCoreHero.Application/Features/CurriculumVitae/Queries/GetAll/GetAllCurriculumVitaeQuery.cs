using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.CurriculumVitae.Queries.GetAll
{
    public class GetAllCurriculumVitaeQuery : IRequest<Response<IEnumerable<GetAllCurriculumVitaeViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCurriculumVitaeQueryHandler : IRequestHandler<GetAllCurriculumVitaeQuery, Response<IEnumerable<GetAllCurriculumVitaeViewModel>>>
    {
        private readonly ICurriculumVitaeRepositoryAsync _curriculumVitaeRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllCurriculumVitaeQueryHandler(ICurriculumVitaeRepositoryAsync productCategoryRepository, IMapper mapper)
        {
            _curriculumVitaeRepositoryAsync = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllCurriculumVitaeViewModel>>> Handle(GetAllCurriculumVitaeQuery request, CancellationToken cancellationToken)
        {
            var categories = await _curriculumVitaeRepositoryAsync.GetAllAsync();
            var categoriesViewModel = _mapper.Map<IEnumerable<GetAllCurriculumVitaeViewModel>>(categories);
            return new Response<IEnumerable<GetAllCurriculumVitaeViewModel>>(categoriesViewModel);
        }
    }
}
