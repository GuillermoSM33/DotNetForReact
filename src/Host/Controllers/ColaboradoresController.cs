using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Host.Controllers
{
    [Route("api/colaboradores")]
    [ApiController]
    public class ColaboradoresController : ControllerBase
    {

        private readonly IColaboradoresService _service;
        private readonly IMediator _mediator;

        public ColaboradoresController(IColaboradoresService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [Route("getColaboradores")]
        [HttpGet]

        public async Task<IActionResult> GetColaboradores()
        {
            var result = await _service.GetColaboradores();
            return Ok(result);
        }

        [HttpGet("getColaboradoresByDate")]
        public async Task<IActionResult> GetColaboradorByRangeOfDate(DateTime FechaCreacion, DateTime FechaFinal)
        {
            var result = await _service.GetColaboradorByRangeOfDate(FechaCreacion, FechaFinal);
            if (result == null)
            {
                return NotFound(new Response<string>("Colaborador no encontrado"));
            }
            return Ok(result);
        }

        [HttpGet("getColaboradoresByValue")]
        public async Task<IActionResult> GetColaboradorByValue(int isProfessor)
        {
            var result = await _service.GetColaboradorByValue(isProfessor);
            if (result == null)
            {
                return NotFound(new Response<string>("Colaborador no encontrado"));
            }
            return Ok(result);
        }

        [HttpGet("getColaboradoresFiltered")]
        public async Task<IActionResult> GetColaboradorFiltered(DateTime FechaCreacion, DateTime FechaFinal, int isProfessor, int Edad)
        {
            var result = await _service.GetColaboradorFiltered(FechaCreacion, FechaFinal, isProfessor, Edad);
            if (result == null)
            {
                return NotFound(new Response<string>("Colaborador no encontrado"));
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> CreateColaboradores(ColaboradoresCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
