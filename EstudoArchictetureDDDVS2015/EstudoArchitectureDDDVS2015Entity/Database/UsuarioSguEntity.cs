using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchitectureDDDVS2015Entity.Database
{
    public partial class UsuarioSguEntity
    {
        public UsuarioSguEntity()
        {
            //this.LogImportacao = new List<LogImportacaoEntity>();
        }

        public int IdUsuarioSgu { get; set; }
        public long? IdSgu { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        //public virtual ICollection<LogImportacaoEntity> LogImportacao { get; set; }

    }
}
