using System;
using System.Collections.Generic;
using System.Text;
using Datos.Mapping.Usuarios;
using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class DbContextProy : DbContext
    {
        public DbSet <Usuario> Usuarios { get; set; }//Exponer la coleccion de usuarios en ese objeto
        public DbSet<RolUsuarios> Roles { get; set; }//Exponer la coleccion de usuarios en ese objeto

        //CONSTRUCTOR 
        public DbContextProy(DbContextOptions<DbContextProy> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//nos permite mapear las entidades con la base de datos
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuariosMap());//Aplicar la configuracion de UsuariosMap
            modelBuilder.ApplyConfiguration(new RolMap());//Aplicar la configuracion de RolMap

        }
    }
}
