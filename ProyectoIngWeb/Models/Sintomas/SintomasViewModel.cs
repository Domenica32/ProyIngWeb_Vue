using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.Sintomas
{
    public class SintomasViewModel
    {
        public int idSintoma { get; set; }
        public string LugarSintoma { get; set; }
        public string TipoMalestar { get; set; }
        public bool estado { get; set; }

    }
}
