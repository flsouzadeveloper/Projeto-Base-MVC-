using System.Web;

namespace EstudoArchictetureDDDVS2015Util.Security
{
    public static class UsuarioLogadoHelper
    {
        public const string ChaveSessao = "_UsuarioLogado_";

        public static UsuarioLogado GetUsuarioLogado(this HttpSessionStateBase session)
        {
            return session[ChaveSessao] as UsuarioLogado;
        }

        public static void SetUsuarioLogado(this HttpSessionStateBase session, UsuarioLogado usuario)
        {
            session[ChaveSessao] = usuario;
        }
    }
}
