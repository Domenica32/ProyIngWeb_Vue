using Entidades.Medicamento;
using ProyectoIngWeb.Models.Medicamentos;
using ProyectoIngWeb.Models.Sintomas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.MedicametoSintomas
{
    public class MedicamentoSintomaViewModel
    {
        public int idMedicamento_Sintoma { get; set; }
        public int idMedicamento_FK { get; set; }
        public int idSintoma_FK { get; set; }
        // public  Medicamento medicamento { get; set; }
        // public   Sintoma sintoma { get; set; }
        public MedicamentosViewModel medicamento { get; set; }
        public SintomasViewModel sintoma { get; set; }
    }
}
