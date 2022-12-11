using ProyectoIngWeb.Models.Usuarios.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIngWeb.Models.Usuarios
{
    public class UsuariosViewModel
    {
        public int idUsuarios { get; set; }
        public int idRolUsuarios_FK { get; set; }
        //public string rol { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public byte[] PasswordUsuario_hash { get; set; }
        public bool condicion { get; set; }

        public RolViewModel rol { get; set; }

    }
}
