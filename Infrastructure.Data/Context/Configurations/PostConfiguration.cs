using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Context.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(post => post.Perfil)
                .WithMany(perfil => perfil.Posts)
                .HasPrincipalKey(perfil => perfil.Id)
                .HasForeignKey(post => post.PerfilId);

            builder
                .Property(x => x.Multimedia)
                .HasMaxLength(250);

            builder
                .Property(x => x.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.Descricao)
                .HasMaxLength(3000)
                .IsRequired();
        }
    }
}
