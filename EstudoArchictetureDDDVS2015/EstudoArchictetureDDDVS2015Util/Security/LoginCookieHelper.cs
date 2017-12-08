using EstudoArchictetureDDDVS2015Util.Configuration;
using System;
using System.Linq;
using System.Web;

namespace EstudoArchictetureDDDVS2015Util.Security
{
    public static class LoginCookieHelper
    {
        public static void SetUserCookie(this HttpContextBase httpContext, int id)
        {
            var cookie = new HttpCookie(fConfiguration.Config.LoginCookie.Chave)
            {
                Value = id.ToString(),
                Domain = fConfiguration.Config.LoginCookie.Dominio,
                Expires = DateTime.Now + fConfiguration.Config.LoginCookie.TempoVida
            };

            httpContext.Response.Cookies.Add(cookie);
        }

        public static int? GetUserIdCookie(this HttpContextBase httpContext)
        {
            try
            {
                var cookie = httpContext.Response.Cookies[fConfiguration.Config.LoginCookie.Chave];

                return int.Parse(cookie.Value);
            }
            catch
            {
                return null;
            }
        }

        public static void ClearUserIdCookie(this HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies.AllKeys.Contains(fConfiguration.Config.LoginCookie.Chave))
            {
                var cookie = httpContext.Request.Cookies[fConfiguration.Config.LoginCookie.Chave];

                if (cookie != null)
                {
                    cookie.Domain = fConfiguration.Config.LoginCookie.Dominio;
                    cookie.Expires = new DateTime(1970, 1, 1, 0, 0, 1);

                    httpContext.Response.Cookies.Add(cookie);
                }
            }
        }
    }
}
