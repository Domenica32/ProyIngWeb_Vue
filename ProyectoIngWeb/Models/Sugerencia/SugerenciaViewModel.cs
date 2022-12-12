using Entidades.Usuarios;
using ProyectoIngWeb.Models.Medicamentos;
using ProyectoIngWeb.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.Sugerencia
{
    public class SugerenciaViewModel
    {
        public int idSugerencia { get; set; }
        
        public int idUsuario_FK { get; set; }
       
        public int idMedicamento_FK { get; set; }

        public MedicamentosViewModel medicamento { get; set; }

        public virtual UsuariosViewModel usuario { get; set; }
    }
}
