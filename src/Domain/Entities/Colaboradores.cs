﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Colaboradores")]
    public class Colaboradores
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public DateTime Birthdate { get; set; }

        public int IsProfessor { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
