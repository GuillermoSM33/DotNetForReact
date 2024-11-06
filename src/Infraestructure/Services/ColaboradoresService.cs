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
    }
}
