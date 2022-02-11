using DeslocamentoApp.Domain.Interfaces;
using DeslocamentoApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeslocamentoAPI.Application.ClientesCommands
{
    public class CadastrarClienteCommand : IRequest<Cliente>
    {
        public string Cpf { get; set; }

        public string Nome { get; set; }

    }
    public class CadastrarClienteCommandHandler :
        IRequestHandler<CadastrarClienteCommand, Cliente>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CadastrarClienteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cliente> Handle(
            CadastrarClienteCommand request,
            CancellationToken cancellationToken)
        {
            var clienteInserir = new Cliente(
                 request.Cpf,
                 request.Nome);

            var repositoryCliente =
                _unitOfWork.GetRepository<Cliente>();

            repositoryCliente.Add(clienteInserir);

            await _unitOfWork.CommitAsync();

            return clienteInserir;
        }
    }
}
