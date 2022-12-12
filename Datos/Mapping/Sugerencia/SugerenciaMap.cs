using Entidades.Sugerencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapping.Sugerencia
{
    class SugerenciaMap : IEntityTypeConfiguration<SugerenciaMedicina>
    {
        public void Configure(EntityTypeBuilder<SugerenciaMedicina> builder)
        {
            builder.ToTable("Sugerencia")
            .HasKey(s => s.idSugerencia);
        }
    }
}
