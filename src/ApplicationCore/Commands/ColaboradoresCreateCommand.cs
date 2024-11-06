using ApplicationCore.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands
{
    public class ColaboradoresCreateCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public DateTime Birthdate { get; set; }

        public int IsProfessor { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; } 
        public string Puesto { get; set; }      
        public string Nomina { get; set; }
    }
}
