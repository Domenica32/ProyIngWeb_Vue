using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.MedicametoSintomas
{
    public class ActualizarViewModel
    {
        public int idMedicamento_Sintoma { get; set; }
        public int idMedicamento_FK { get; set; }
        public int idSintoma_FK { get; set; }
    }
}
