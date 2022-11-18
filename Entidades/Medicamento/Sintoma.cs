using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades.Medicamento
{
    public class Sintoma
    {
        [Required]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public int idSintoma { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 20 ni menos de 3 caracteres")]

        public string LugarSintoma{ get; set; }
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 20 ni menos de 3 caracteres")]

        public string  TipoMalestar{ get; set; }

        public bool estado { get; set; }

        public virtual List<RelacionMedicamentoSintoma> MedicamentoSintomas { get; set; }
    }
}
