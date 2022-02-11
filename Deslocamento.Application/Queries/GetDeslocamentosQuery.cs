using DeslocamentoApp.Domain.Interfaces;
using DeslocamentoApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeslocamentoAPI.Application.Queries
{
    public class GetDeslocamentosQuery : IRequest<List<Deslocamento>>
    {
    }

    public class GetDeslocamentosQueryHandler :
        IRequestHandler<GetDeslocamentosQuery, List<Deslocamento>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDeslocamentosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Deslocamento>> Handle(
            GetDeslocamentosQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryDeslocamento =
                _unitOfWork.GetRepository<Deslocamento>();

            var deslocamentos = await repositoryDeslocamento
                .GetAll()
                .ToListAsync(cancellationToken);

            return deslocamentos;
        }
    }
}
