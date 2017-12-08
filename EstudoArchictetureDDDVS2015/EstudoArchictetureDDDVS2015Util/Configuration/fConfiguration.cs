using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Configuration
{
    public class fConfiguration
    {
        #region Singleton Design Pattern

        private static fConfiguration _config;

        public static fConfiguration Config
        {
            get { return _config = _config ?? new fConfiguration(); }
        }

        private fConfiguration()
        {
            LoginCookie = new LoginCookieConfiguration();
            Urlsf = new UrlfConfiguration();
        }

        #endregion

        #region Properties

        public LoginCookieConfiguration LoginCookie { get; set; }

        public UrlfConfiguration Urlsf { get; set; }

        #endregion

        #region Methods

        public static void LoadSingleton()
        {
            _config = new fConfiguration();
        }

        #endregion
    }
}
