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
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("CONTA");

            builder.HasKey(c => c.ContaId);

            //mapeamento do campo ContaId
            builder.Property(c => c.ContaId).HasColumnName("CONTAID");

            //mapeamento do campo Observacao
            builder.Property(c => c.Observacao).HasColumnName("OBSERVACAO");

            //mapeamento do campo Nome
            builder.Property(c => c.Nome).HasColumnName("NOME").IsRequired();

            //mapeamento do campo Data
            builder.Property(c => c.Data).HasColumnName("DATA").HasColumnType("date").IsRequired();

            //mapeamento do campo Valor
            builder.Property(c => c.Valor).HasColumnName("VALOR").HasColumnType("decimal(18,4)").IsRequired();

            //mapeamento do campo UsuarioId
            builder.Property(c => c.UsuarioId).HasColumnName("USUARIOID").IsRequired();

            //mapeamento do campo CategoriaId
            builder.Property(c => c.CategoriaId).HasColumnName("CATEGORIAID").IsRequired();


            //mapeamento do relacionamento com Usuario (OneToMany)
            builder.HasOne(c => c.Usuario) //Conta TEM 1 Usuario
                   .WithMany(c => c.Contas) //Usuario TEM MUITAS Contas
                   .HasForeignKey(c => c.UsuarioId); //chave estrangeira

            //mapeamento do relacioanamento com Categoria (OneToMany)
            builder.HasOne(c => c.Categoria) //Conta TEM 1 Categoria
                   .WithMany(c => c.Contas) //Categoria TEM MUITAS Contas 
                   .HasForeignKey(c => c.CategoriaId) //chave estrangeira
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
