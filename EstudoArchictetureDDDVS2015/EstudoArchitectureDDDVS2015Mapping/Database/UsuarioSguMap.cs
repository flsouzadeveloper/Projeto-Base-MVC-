using EstudoArchitectureDDDVS2015Entity.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchitectureDDDVS2015Mapping.Database
{
    public class UsuarioSguMap : EntityTypeConfiguration<UsuarioSguEntity>
    {
        public UsuarioSguMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioSgu);

            // Properties
            this.Property(t => t.Nome)
                .HasMaxLength(255);

            this.Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Email)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("TB_USUARIO_SGU");
            this.Property(t => t.IdUsuarioSgu).HasColumnName("IdUsuarioSgu");
            this.Property(t => t.IdSgu).HasColumnName("IdSgu");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
