using System.Threading.Tasks;
using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<Estudiantes> GetByIdAsync(int id);
        Task UpdateAsync(Estudiantes estudiante);
        Task<Response<int>> UpdateEstudianteAsync(EstudianteUpdateCommand command);
        Task<List<Estudiantes>> GetAllAsync();
    }
}