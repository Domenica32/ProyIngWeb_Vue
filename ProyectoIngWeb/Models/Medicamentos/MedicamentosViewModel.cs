using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.Medicamentos
{
    public class MedicamentosViewModel
    {
        public int idMedicamento { get; set; }
        public string nombre { get; set; }
        public string compuestoQuimico { get; set; }
        public string dosis { get; set; }
        public bool estado { get; set; }
    }
}
