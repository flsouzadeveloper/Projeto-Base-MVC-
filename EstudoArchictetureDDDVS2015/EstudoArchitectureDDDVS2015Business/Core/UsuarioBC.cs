using EstudoArchictetureDDDVS2015Util.Configuration;
using EstudoArchictetureDDDVS2015Util.Mensagens;
using EstudoArchictetureDDDVS2015Util.Security;
using EstudoArchitectureDDDVS2015Business.Conversion;
using EstudoArchitectureDDDVS2015DTO;
using EstudoArchitectureDDDVS2015Entity.Database;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchitectureDDDVS2015Business.Core
{
    public class UsuarioBC : BaseOC
    {
        #region Constructor

        // Utilizar este construtor somente para efetuar login.
        public UsuarioBC() : base((UsuarioLogado)null)
        {
        }

        public UsuarioBC(UsuarioLogado usuarioLogado) : base(usuarioLogado)
        {
        }

        public UsuarioBC(BaseOC outraBC) : base(outraBC)
        {
        }

        #endregion

        #region Login

        public UsuarioLogado CarregarUsuarioLogado(int idUsuario)
        {
            var usuario = _context.UsuarioSgu.Single(s => s.IdUsuarioSgu == idUsuario);
            
            // Retornar usuario logado.
            return UsuarioConverter.ParaUsuarioLogado(usuario);
        }

        public UsuarioLogado CarregarUsuarioAutenticadoParaDeveloperAdm(string usuario, string senha, int idUsuarioSgu)
        {
            var usuEntity = _context.UsuarioSgu.SingleOrDefault(s => s.IdUsuarioSgu == idUsuarioSgu);

            if (usuEntity == null) throw new PrintableException(MensagensGenericas.UsuarioSimuladoNaoEncontrado);

            const string hashUsuarioEsperado = "c92fccecc030aed29320c09b7aa8e0b5";
            const string hashSenhaEsperado = "c5da5b640e277366c85935fc00e116e3";

            var hashUsuario = GetMD5Hash(usuario);
            var hashSenha = GetMD5Hash(senha);

            if (hashUsuario != hashUsuarioEsperado || hashSenha != hashSenhaEsperado)
            {
                throw new PrintableException(MensagensGenericas.FalhaAutenticacaoUsuario);
            }

            var usuLogado = UsuarioConverter.ParaSuperUsuarioLogado(usuEntity);

            return usuLogado;
        }

        private static string GetMD5Hash(string text)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

                var stringBuilder = new StringBuilder();

                foreach (var byteItem in data)
                {
                    stringBuilder.Append(byteItem.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        private void SalvarEAtualizarUsuario(UsuarioLogado usuario)
        {
            // Localizar usuário no banco
            var query = PredicateBuilder.New<UsuarioSguEntity>();

            if(usuario.IdUsuarioNoSgu == null)
            {
                query = query.And(a => a.Nome == usuario.Nome).And(a => a.Login == usuario.Login);
            }else
            {
                query = query.And(a => a.IdSgu == usuario.IdUsuarioNoSgu);
            }

            var usuEntity = _context.UsuarioSgu.AsExpandable().FirstOrDefault(query);

            // Cria e adiciona o objeto caso ainda nao exista.
            if(usuEntity == null)
            {
                usuEntity = new UsuarioSguEntity();
                _context.UsuarioSgu.Add(usuEntity);
            }

            // Atualiza Campos
            usuEntity.IdSgu = usuario.IdUsuarioNoSgu;
            usuEntity.Nome = usuario.Nome;
            usuEntity.Login = usuario.Login;
            usuEntity.Email = usuario.Email;

            _context.SaveChanges();

            // Atualizar a chave
            usuario.IdUsuarioNoSistema = usuEntity.IdUsuarioSgu;
        }

        #endregion

        #region Crud

        public List<UsuarioDTO> ListarUsuarios()
        {
            return _context.UsuarioSgu.Select(UsuarioConverter.ParaDTO).ToList();
        }

        #endregion
    }
}
