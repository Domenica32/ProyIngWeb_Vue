using Entidades.Medicamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapping.Medicamentos
{
    public class MedicamentoMap : IEntityTypeConfiguration<Entidades.Medicamento.Medicamentos>
    {
        public void Configure(EntityTypeBuilder<Entidades.Medicamento.Medicamentos> builder)
        {
            builder.ToTable("Medicamento")
            .HasKey(m => m.idMedicamento);
        }
    }
}
