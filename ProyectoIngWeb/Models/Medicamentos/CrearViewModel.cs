using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.Medicamentos
{
    public class CrearViewModel
    {
        
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "E l nombre no debe tener m[as de 50 ni menos de 3 caracteres")]
        public string nombre { get; set; }
        public string compuestoQuimico { get; set; }
        public string dosis { get; set; }
       // public bool estado { get; set; }
    }
}
