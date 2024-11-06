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

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> CreateColaboradores(ColaboradoresCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
