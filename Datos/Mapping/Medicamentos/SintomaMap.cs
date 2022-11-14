using Entidades.Medicamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapping.Medicamentos
{
    public class SintomaMap : IEntityTypeConfiguration<Sintoma>
    {
        public void Configure(EntityTypeBuilder<Sintoma> builder)
        {
            builder.ToTable("Sintoma")
             .HasKey(s => s.idSintoma);
        }
    }
}
