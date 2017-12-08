using System.Web;
using System.Web.Mvc;
using EstudoArchitectureDDDVS2015Business.Core;
using EstudoArchictetureDDDVS2015Util.Security;

namespace EstudoArchictetureDDDVS2015.Filters
{
    public class fAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var usuarioLogado = httpContext.Session.GetUsuarioLogado();

            if (usuarioLogado != null) return true;

            var idUsuario = httpContext.GetUserIdCookie();

            if (idUsuario != null) return false;

            using (var bc = new UsuarioBC())
            {
                try
                {
                    //usuarioLogado = bc.CarregarUsuarioAutenticado((int)idUsuario); -- Método não existe
                    usuarioLogado = bc.CarregarUsuarioLogado((int)idUsuario);

                    httpContext.Session.SetUsuarioLogado(usuarioLogado);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}