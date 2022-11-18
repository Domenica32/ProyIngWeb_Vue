using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades.Medicamento
{

    [Table ("Medicamento_Sintoma")]

    public class RelacionMedicamentoSintoma
    {


        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Key]
        public int idMedicamento_Sintoma { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public int idMedicamento_FK { get; set; }
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public int idSintoma_FK { get; set; }
        [ForeignKey("idMedicamento_FK")]
        public virtual Medicamento medicamento { get; set;}
        [ForeignKey("idSintoma_FK")]

        public virtual Sintoma sintoma { get; set; }


    }
}
