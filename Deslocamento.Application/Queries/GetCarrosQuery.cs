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

    public class GetCarrosQuery : IRequest<List<Carro>>
    {
    }

    public class GetCarrosQueryHandler :
        IRequestHandler<GetCarrosQuery, List<Carro>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCarrosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Carro>> Handle(
            GetCarrosQuery request,
            CancellationToken cancellationToken)
        {
            var repositoryCarro =
                _unitOfWork.GetRepository<Carro>();

            var carros = await repositoryCarro
                .GetAll()
                .ToListAsync(cancellationToken);

            return carros;
        }
    }
}
