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
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //nome da tablea no banco de dados
            builder.ToTable("CATEGORIA");
            //chave primaria
            builder.HasKey(c => c.CategoriaId);
            //mapeando o campo 'Id'
            builder.Property(c => c.CategoriaId).HasColumnName("CATEGORIAID");
            //mapeando o campo 'Nome'
            builder.Property(c => c.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            //mapeando o campo 'Tipo'
            builder.Property(c => c.Tipo).HasColumnName("TIPO").IsRequired();

            //mapeando o campo 'UsuarioId'
            builder.Property(c => c.UsuarioId).HasColumnName("USUARIOID").IsRequired();

            //mapeando o relacionamento OneToMany
            builder.HasOne(c => c.Usuario) //Categoria TEM 1 Usuário
                   .WithMany(c => c.Categorias) //Usuário TEM MUITAS Categorias
                   .HasForeignKey(c => c.UsuarioId) //Chave estrangeira
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
