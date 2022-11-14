using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades.Medicamento
{
    public class Medicamento
    {
        public int idMedicamento { get; set; }
        [Required]
        [StringLength(60, MinimumLength =3, ErrorMessage = "El nombre no debe tener mas de 50 ni menos de 3 caracteres")]
        public string nombre { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 ni menos de 3 caracteres")]

        public string compuestoQuimico { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 50 ni menos de 3 caracteres")]

        public string dosis { get; set; }
        public bool estado { get; set; }

    }
}
