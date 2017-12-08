using EstudoArchictetureDDDVS2015Util.Helpers.Definitions;
using System;
using System.Configuration;

namespace EstudoArchictetureDDDVS2015Util.Configuration
{
    public class LoginCookieConfiguration
    {
        #region Properties

        public string Chave { get; private set; }

        public TimeSpan TempoVida { get; private set; }

        public string Dominio { get; private set; }

        #endregion

        #region Construtor

        internal LoginCookieConfiguration()
        {
            Chave = ConfigurationManager.AppSettings.GetString("fConfiguracao.Config.LoginCookie.Chave");
            TempoVida = ConfigurationManager.AppSettings.GetTimeSpan("fConfiguracao.Config.LoginCookie.TempoVida");
            Dominio = ConfigurationManager.AppSettings.GetString("fConfiguracao.Config.LoginCookie.Dominio");
        } 

        #endregion
    }
}
