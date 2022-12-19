using Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Entidades.Medicamento;

namespace Entidades.Sugerencia
{
    [Table("Sugerencia")]

    public class SugerenciaMedicina
    {
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Key]
        public int idSugerencia { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public int idUsuario_FK { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public int idMedicamento_FK { get; set; }

        public DateTime fecha { get; set; }

        [ForeignKey("idMedicamento_FK")]

        public virtual Medicamentos medicamento { get; set; }
        [ForeignKey("idUsuario_FK")]

        public virtual Usuario usuario { get; set; }



    }
}
