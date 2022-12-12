using System;
using System.Collections.Generic;
using System.Text;
using Datos.Mapping.Medicamentos;
using Datos.Mapping.Usuarios;
using Entidades.Medicamento;
using Entidades.Sugerencia;
using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class DbContextProy : DbContext
    {
        
        public DbSet <Usuario> Usuarios { get; set; }//Exponer la coleccion de usuarios en ese objeto
        public DbSet<RolUsuarios> Roles { get; set; }//Exponer la coleccion de usuarios en ese objeto

        public DbSet<Medicamentos> Medicamento { get; set; }
        public DbSet<Sintoma> Sintoma { get; set; }

        public DbSet<RelacionMedicamentoSintoma> MedicamentoSintoma { get; set; }

        public DbSet<SugerenciaMedicina> Sugerencia { get; set; }

        //CONSTRUCTOR 
        public DbContextProy(DbContextOptions<DbContextProy> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//nos permite mapear las entidades con la base de datos
        
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new UsuariosMap());//Aplicar la configuracion de UsuariosMap
            modelBuilder.ApplyConfiguration(new RolMap());//Aplicar la configuracion de RolMap
            modelBuilder.ApplyConfiguration(new MedicamentoMap());
            modelBuilder.ApplyConfiguration(new SintomaMap());
           // modelBuilder.ApplyConfiguration(new MedicamentoSintomaMap());
            //modelBuilder.Entity<RelacionMedicamentoSintoma>().HasKey(x => new { x.idMedicamento_FK, x.idSintoma_FK });



        }
    }
}
