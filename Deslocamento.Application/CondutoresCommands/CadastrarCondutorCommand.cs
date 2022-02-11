using DeslocamentoApp.Domain.Interfaces;
using DeslocamentoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeslocamentoAPI.Application.CondutoresCommands
{
    public class CadastrarCondutorCommand : IRequest<Condutor>
    {
        public string Nome { get; set; }

        public string Email { get; set; }

    }
    public class CadastrarCondutorCommandHandler :
        IRequestHandler<CadastrarCondutorCommand, Condutor>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarCondutorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Condutor> Handle(
            CadastrarCondutorCommand request,
            CancellationToken cancellationToken)
        {
            var condutorInserir = new Condutor(
                 request.Nome,
                 request.Email);

            var repositoryCondutor =
                _unitOfWork.GetRepository<Condutor>();

            repositoryCondutor.Add(condutorInserir);

            await _unitOfWork.CommitAsync();

            return condutorInserir;
        }
    }
}
