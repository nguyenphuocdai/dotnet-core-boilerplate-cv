using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.CurriculumVitae.Commands.Create
{
    public partial class CreateCurriculumVitaeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TemplatePreviewID { get; set; }
        public bool IsPublished { get; set; }
    }

    public partial class CreateCurriculumVitaeCommand : IRequestHandler<CreateCurriculumVitaeCommand, Response<int>>
    {
        private readonly ICurriculumVitaeRepositoryAsync _iCurriculumVitaeRepositoryAsyncAsync;
        private readonly IMapper _mapper;
        private IUnitOfWork UnitOfWork { get; set; }

        public CreateCurriculumVitaeCommand(ICurriculumVitaeRepositoryAsync curriculumVitaeRepositoryAsync, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _iCurriculumVitaeRepositoryAsyncAsync = curriculumVitaeRepositoryAsync;
            UnitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Response<int>> Handle(CreateCurriculumVitaeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.CurriculumVitae category = _mapper.Map<Domain.Entities.CurriculumVitae>(request);
                await _iCurriculumVitaeRepositoryAsyncAsync.AddAsync(category);

                return new Response<int>(await UnitOfWork.Commit(cancellationToken));
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
