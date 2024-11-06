using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Profesor")]
    public class Profesor
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Colaboradores")]
        public int FKColaborador { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public virtual Colaboradores Colaborador { get; set; }
    }
}
