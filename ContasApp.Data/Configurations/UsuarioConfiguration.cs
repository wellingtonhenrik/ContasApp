using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela
            builder.ToTable("USUARIO");

            //chave primario
            builder.HasKey(c => c.UsuarioId);

            //mapeamento do campo UsuarioId
            builder.Property(c => c.UsuarioId).HasColumnName("USUARIOID");

            //mapeamento do campo Email
            builder.Property(c => c.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();

            //criando um indice no campo Email  tornando unico
            builder.HasIndex(u => u.Email).IsUnique();

            //mapeamento do campo Senha
            builder.Property(c => c.Senha).HasColumnName("SENHA").HasMaxLength(40).IsRequired();

        }
    }
}
