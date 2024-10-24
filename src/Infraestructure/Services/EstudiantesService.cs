using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> GetEstudiantes()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return new Response<object>(estudiantes);
        }

        public async Task<Response<Estudiantes>> GetEstudianteById(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return new Response<Estudiantes>("Estudiante no encontrado");
            }
            return new Response<Estudiantes>(estudiante);
        }
    }
}