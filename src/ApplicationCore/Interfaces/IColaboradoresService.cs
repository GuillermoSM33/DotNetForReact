using ApplicationCore.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IColaboradoresService

    {
            Task<Response<object>> GetColaboradores();

            Task<Response<List<Colaboradores>>> GetColaboradorByRangeOfDate(DateTime FechaCreacion, DateTime FechaFinal);
            Task<Response<List<Colaboradores>>> GetColaboradorByValue(int IsProfessor);
            Task<Response<List<Colaboradores>>> GetColaboradorFiltered(DateTime FechaCreacion, DateTime FechaFinal, int IsProfessor, int Edad);

    }
}
