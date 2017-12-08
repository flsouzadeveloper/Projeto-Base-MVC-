using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Security
{
    public class SuperUsuarioLogado : UsuarioLogado
    {
        public override bool IsSuperUsuario
        {
            get { return true; }
        }
    }
}
