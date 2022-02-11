using DeslocamentoApp.Domain.Interfaces;
using DeslocamentoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeslocamentoAPI.Application.DeslocamentoCommands
{
    public class CadastrarDeslocamentoCommand : IRequest<Deslocamento>
    {
        public long CarroId { get; set; }
        public long ClienteId { get; set; }
        public long CondutorId { get; set; }
        public decimal QuilometragemInicial { get; set; }
    }

    public class CadastrarDeslocamentoCommandHandler :
        IRequestHandler<CadastrarDeslocamentoCommand, Deslocamento>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarDeslocamentoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Deslocamento> Handle(
            CadastrarDeslocamentoCommand request,
            CancellationToken cancellationToken)
        {
            var deslocamentoInserir = new Deslocamento(
                 request.CarroId,
                 request.ClienteId,
                 request.CondutorId,
                 request.QuilometragemInicial);

            var repositoryDeslocamento =
                _unitOfWork.GetRepository<Deslocamento>();

            repositoryDeslocamento.Add(deslocamentoInserir);

            await _unitOfWork.CommitAsync();

            return deslocamentoInserir;
        }
    }
}
