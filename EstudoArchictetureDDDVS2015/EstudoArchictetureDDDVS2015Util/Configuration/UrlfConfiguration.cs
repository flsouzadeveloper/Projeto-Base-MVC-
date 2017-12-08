using EstudoArchictetureDDDVS2015Util.Helpers.Definitions;
using System.Configuration;

namespace EstudoArchictetureDDDVS2015Util.Configuration
{
    public class UrlfConfiguration
    {
        #region Properties

        public string Versao1 { get; private set; }
        public string Versao2 { get; private set; } //Se necessário desenvolver uma nova versão do projeto

        #endregion

        #region Construtor

        internal UrlfConfiguration()
        {
            Versao1 = ConfigurationManager.AppSettings.GetUrl("fConfiguracao.Config.Urlf.Versao1");
            Versao2 = ConfigurationManager.AppSettings.GetUrl(" Colocar a key se houver necessidade de uma nova versão do projeto ");
        }

        #endregion
    }
}
