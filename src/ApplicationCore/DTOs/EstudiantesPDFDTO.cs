using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class EstudiantesPDFDTO
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public List<EstudianteDTO> Estudiantes { get; set; }
    }
}
