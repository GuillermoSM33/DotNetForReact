using System.Threading.Tasks;
using ApplicationCore.Commands;
using ApplicationCore.Wrappers;
using ApplicationCore.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Persistence;

namespace Infraestructure.Repositories
{
    public class EstudianteRepository : ApplicationCore.Interfaces.IEstudianteRepository
    {
        private readonly ApplicationDbContext _context;

        public EstudianteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> UpdateEstudianteAsync(EstudianteUpdateCommand command)
        {
            var estudiante = await _context.Estudiantes.FindAsync(command.Id);
            if (estudiante == null)
            {
                return new Response<int>("Estudiante no encontrado");
            }

            estudiante.Nombre = command.Nombre;
            estudiante.Edad = command.Edad;
            estudiante.Correo = command.Correo;

            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();

            return new Response<int>(estudiante.Id, "Estudiante actualizado exitosamente");
        }

        public async Task<Estudiantes> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task UpdateAsync(Estudiantes estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Estudiantes>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }
    }
}