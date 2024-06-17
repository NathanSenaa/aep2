using DoaFacil.Pedidos.Application.Commands.DTO;
using DoaFacil.Pedidos.Application.Commands.Solicitantes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DoaFacil.API.Controllers
{
    [Route("solicitante")]
    public class SolicitanteController : MainController
    {
        private readonly IMediator _mediatorHandler;
        public SolicitanteController(IMediator mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarSOlicitante(SolicitanteDTO solicitanteDTO)
        {
            try
            {
                var solicitante = new AdicionarSolicitanteCommand(solicitanteDTO.Nome, solicitanteDTO.Endereco, solicitanteDTO.Telefone, solicitanteDTO.Email, solicitanteDTO.Cnpj);
                return CustomResponse(await _mediatorHandler.Send(solicitante));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }       
    }
}
