using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.CurriculumVitae.Commands.Delete
{
    public class DeleteCurriculumVitaeByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCurriculumByIdCommandHandler : IRequestHandler<DeleteCurriculumVitaeByIdCommand, Response<int>>
        {
            private readonly ICurriculumVitaeRepositoryAsync _iCurriculumVitaeRepositoryAsync;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteCurriculumByIdCommandHandler(ICurriculumVitaeRepositoryAsync iCurriculumVitaeRepositoryAsync, IUnitOfWork unitOfWork)
            {
                _iCurriculumVitaeRepositoryAsync = iCurriculumVitaeRepositoryAsync;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(DeleteCurriculumVitaeByIdCommand command, CancellationToken cancellationToken)
            {
                var curriculumVitae = await _iCurriculumVitaeRepositoryAsync.GetByIdAsync(command.Id);
                if (curriculumVitae == null)
                {
                    throw new ApiException($"CurriculumVitae Not Found.");
                }

                await _iCurriculumVitaeRepositoryAsync.DeleteAsync(curriculumVitae);
                await _unitOfWork.Commit(cancellationToken);
                return new Response<int>(curriculumVitae.Id);
            }
        }
    }
}
