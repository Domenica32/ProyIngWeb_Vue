using Entidades.Medicamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Mapping.Medicamentos
{
    public class MedicamentoSintomaMap : IEntityTypeConfiguration<RelacionMedicamentoSintoma>
    {
        public void Configure(EntityTypeBuilder<RelacionMedicamentoSintoma> builder)
        {
            builder.ToTable("RelacionMedicamentoSintoma")
            .HasKey(ms => ms.idMedicamento_Sintoma);
        }
    }
}
