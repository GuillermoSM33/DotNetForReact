using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using ApplicationCore.DTOs;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;
using DevExpress.DataAccess.ObjectBinding;

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

        public async Task<byte[]> GetPDF()
        {
            var report = new ApplicationCore.PDF.EstudiantesPDF();

            // Obtén los datos de estudiantes
            var estudiantes = await (from e in _context.Estudiantes
                                     select new EstudianteDTO
                                     {
                                         Id = e.Id,
                                         Edad = e.Edad,
                                         Nombre = e.Nombre,
                                         Correo = e.Correo
                                     }).ToListAsync();

            // Asigna la lista directamente como la fuente de datos del reporte
            report.DataSource = estudiantes;

            using (var memory = new MemoryStream())
            {
                await report.ExportToPdfAsync(memory);
                memory.Position = 0;
                return memory.ToArray();
            }
        }


    }
}