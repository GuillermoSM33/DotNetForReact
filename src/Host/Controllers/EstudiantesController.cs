using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudiantesService _service;
        private readonly IMediator _mediator;
        public EstudiantesController(IEstudiantesService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        //<sumary>
        //Lista de Usuarios - Get
        //</sumary>

        [Route("getEstudiantes")]
        [HttpGet]

        public async Task<IActionResult> GetEstudiantes()
        {
            var result = await _service.GetEstudiantes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstudianteById(int id)
        {
            var result = await _service.GetEstudianteById(id);
            if (result == null)
            {
                return NotFound(new Response<string>("Estudiante no encontrado"));
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> CreateEstudiante(EstudianteCreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Response<int>>> DeleteEstudiante(int id)
        {
            var command = new EstudianteDeleteCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Response<int>>> UpdateEstudiante(int id, EstudianteUpdateCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("El ID del comando no coincide con el ID de la URL.");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("pdf")]

        public async Task<ActionResult> GetPDF()
        {
            var pdfile = await _service.GetPDF();
            return File(pdfile, "application/pdf", "Listado-de-estudiantes.pdf");
        }



    }
}
