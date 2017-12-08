using System.Collections.Generic;

namespace EstudoArchitectureDDDVS2015Entity.Database
{
    public partial class LogImportacaoLinhaEntity
    {
        public LogImportacaoLinhaEntity()
        {
            this.LogImportacaoErro = new List<LogImportacaoErroEntity>();
        }

        public int IdLogImportacaoLinha { get; set; }
        public int IdLogImportacao { get; set; }
        public int NumeroLinha { get; set; }
        public int NumeroConta { get; set; }
        public bool Importado { get; set; }

        public virtual LogImportacaoEntity LogImportacao { get; set; }
        public virtual ICollection<LogImportacaoErroEntity> LogImportacaoErro { get; set; }
    }
}
