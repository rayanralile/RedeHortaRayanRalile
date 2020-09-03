using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure.Data.Context.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(x => x.Foto)
                .HasMaxLength(250)
                .IsRequired();

            builder
                .Property(x => x.Biografia)
                .HasMaxLength(1000);

            builder
                .Property(x => x.Interesses)
                .HasMaxLength(50);

            builder
                .Property(x => x.TipoHorta)
                .HasMaxLength(50);

            builder
                .HasIndex(x => x.UsuarioLogin)
                .IsUnique();
        }
    }
}
