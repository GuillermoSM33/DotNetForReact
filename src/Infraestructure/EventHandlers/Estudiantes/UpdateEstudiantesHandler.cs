using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using ApplicationCore.Interfaces;
using MediatR;

namespace Infraestructure.EventHandlers.Estudiantes
{
    public class UpdateEstudiantesHandler : IRequestHandler<EstudianteUpdateCommand, Response<int>>
    {
        private readonly ApplicationCore.Interfaces.IEstudianteRepository _estudianteRepository;

        public UpdateEstudiantesHandler(ApplicationCore.Interfaces.IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        public async Task<Response<int>> Handle(EstudianteUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _estudianteRepository.UpdateEstudianteAsync(request);
        }
    }
}