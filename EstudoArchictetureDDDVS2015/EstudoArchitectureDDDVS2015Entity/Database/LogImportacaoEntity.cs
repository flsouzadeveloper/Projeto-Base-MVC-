using System;
using System.Collections.Generic;

namespace EstudoArchitectureDDDVS2015Entity.Database
{
    public partial class LogImportacaoEntity
    {
        public LogImportacaoEntity()
        {
            this.LogImportacaoLinha = new List<LogImportacaoLinhaEntity>();
        }

        public int IdLogImportacao { get; set; }
        public DateTime DataImportacao { get; set; }
        public int IdUsuario { get; set; }
        public int IdFormaEnvioBaixa { get; set; }
        public byte[] Arquivo { get; set; }
        public virtual ICollection<LogImportacaoLinhaEntity> LogImportacaoLinha { get; set; }
        public virtual UsuarioSguEntity UsuarioSgu { get; set; }
    }
}
