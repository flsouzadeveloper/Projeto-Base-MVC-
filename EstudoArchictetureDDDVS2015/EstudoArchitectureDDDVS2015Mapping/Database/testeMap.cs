using EstudoArchitectureDDDVS2015Entity.Database;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstudoArchitectureDDDVS2015Mapping.Database
{
    public class testeMap : EntityTypeConfiguration<testeEntity>
    {
        public testeMap()
        {
            //Primary Key
            this.HasKey(t => t.COL1);

            //Properties
            //this.Property(t => t.COL2).IsRequired();

            this.ToTable("teste");
            this.Property(t => t.COL1).HasColumnName("COL1");
            this.Property(t => t.COL2).HasColumnName("COL2");
            this.Property(t => t.COL3).HasColumnName("COL3");

            // Relationships - se houver relacionamento entre as tabelas 
            //this.HasRequired(t => t.TipoBaixaContrato)
            //    .WithMany(t => t.BaixaContratoArquivo)
            //    .HasForeignKey(d => d.IdTipoBaixaContrato)
        }
    }
}
