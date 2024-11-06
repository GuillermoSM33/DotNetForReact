using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Administrativo")]
    public class Administrativo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Colaboradores")]
        public int FKColaborador { get; set; }

        public string Correo { get; set; }

        public string Puesto { get; set; }

        public string Nomina { get; set; }
        public virtual Colaboradores Colaborador { get; set; }
    }
}
