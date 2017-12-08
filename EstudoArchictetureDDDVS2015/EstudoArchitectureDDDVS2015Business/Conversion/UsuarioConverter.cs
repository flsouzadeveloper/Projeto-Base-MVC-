using EstudoArchictetureDDDVS2015Util.Security;
using EstudoArchitectureDDDVS2015DTO;
using EstudoArchitectureDDDVS2015Entity.Database;
using System;
using System.Linq.Expressions;

namespace EstudoArchitectureDDDVS2015Business.Conversion
{
    public static class UsuarioConverter
    {
        public static Expression<Func<UsuarioSguEntity, UsuarioDTO>> ParaDTO
        {
            get
            {
                return entity => new UsuarioDTO
                {
                    IdUsuario = entity.IdUsuarioSgu,
                    IdSgu = entity.IdSgu,
                    Nome = entity.Nome,
                    Login = entity.Login,
                    Email = entity.Email
                };
            }
        }

        public static UsuarioLogado ParaUsuarioLogado(UsuarioSguEntity entity)
        {
            return new UsuarioLogado
            {
                IdUsuarioNoSistema = entity.IdUsuarioSgu,
                IdUsuarioNoSgu = entity.IdSgu,
                Nome = entity.Nome,
                Login = entity.Login,
                Email = entity.Email
                // TODO: Carregar Perfil e Autorizacoes. Nao mecher nesta parte sem supervisao do lider tecnico.
            };
        }

        public static SuperUsuarioLogado ParaSuperUsuarioLogado(UsuarioSguEntity entity)
        {
            return new SuperUsuarioLogado
            {
                IdUsuarioNoSistema = entity.IdUsuarioSgu,
                IdUsuarioNoSgu = entity.IdSgu,
                Nome = entity.Nome,
                Login = entity.Login,
                Email = entity.Email
                // TODO: Carregar Perfil e Autorizacoes. Nao mecher nesta parte sem supervisao do lider tecnico.
            };
        }
    }
}
