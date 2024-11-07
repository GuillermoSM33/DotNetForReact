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
    public class ColaboradoresService : IColaboradoresService
    {
        private readonly ApplicationDbContext _context;

        public ColaboradoresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> GetColaboradores()
        {
            var colaboradores = await _context.Colaboradores.ToListAsync();
            return new Response<object>(colaboradores);
        }

        public async Task<Response<List<Colaboradores>>> GetColaboradorByRangeOfDate(DateTime FechaCreacion, DateTime FechaFinal)
        {
            var colaboradores = await _context.Colaboradores
                .Where(c => c.FechaCreacion >= FechaCreacion && c.FechaCreacion <= FechaFinal)
                .ToListAsync();
            return new Response<List<Colaboradores>>(colaboradores);
        }

        public async Task<Response<List<Colaboradores>>> GetColaboradorByValue(int IsProfessor)
        {
            var colaboradores = await _context.Colaboradores
                .Where(c => c.IsProfessor == IsProfessor)
                .ToListAsync();
            return new Response<List<Colaboradores>>(colaboradores);
        }

        public async Task<Response<List<Colaboradores>>> GetColaboradorFiltered(DateTime FechaCreacion, DateTime FechaFinal, int IsProfessor, int Edad)
        {
            var colaboradores = await _context.Colaboradores
                .Where(c => c.FechaCreacion >= FechaCreacion && c.FechaCreacion <= FechaFinal && c.IsProfessor == IsProfessor && c.Edad == Edad)
                .ToListAsync();
            return new Response<List<Colaboradores>>(colaboradores);
        }

    }
}
