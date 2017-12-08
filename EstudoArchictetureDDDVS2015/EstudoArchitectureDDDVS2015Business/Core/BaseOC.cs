using System;
using System.Collections.Generic;
using System.Linq;
using EstudoArchitectureDDDVS2015DataAccess;
using EstudoArchictetureDDDVS2015Util.Security;

namespace EstudoArchitectureDDDVS2015Business.Core
{
    public abstract class BaseOC : IDisposable
    {
        #region Fields

        protected readonly TestContext _context;
        private readonly List<BaseOC> _outrasBCVinculadas = new List<BaseOC>();

        #endregion

        #region Properties

        protected bool Disposing { get; private set; }
        public UsuarioLogado Usuario { get; private set; }

        #endregion

        #region Contrutors

        protected BaseOC(UsuarioLogado usuario)
        {
            _context = new TestContext();
            Usuario = usuario;
        }

        protected BaseOC(BaseOC otherBC)
        {
            _context = otherBC._context;
            Usuario = otherBC.Usuario;
        }

        #endregion

        #region Methods

        public T OutraBC<T>()where T : BaseOC
        {
            var instance = _outrasBCVinculadas.OfType<T>().FirstOrDefault();

            if(instance == null)
            {
                instance = (T)Activator.CreateInstance(typeof (T), new object[] { this });

                _outrasBCVinculadas.Add(instance);
            }

            return instance;                
        }

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            if (Disposing) return;
            Disposing = true;

            if (_context != null) _context.Dispose();
            if (_outrasBCVinculadas != null && _outrasBCVinculadas.Any())
            {
                _outrasBCVinculadas.ForEach(f => f.Dispose());
            }
        }

        #endregion
    }
}
