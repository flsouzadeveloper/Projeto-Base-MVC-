using EstudoArchictetureDDDVS2015Util.Security;
using EstudoArchitectureDDDVS2015Business.Conversion;
using EstudoArchitectureDDDVS2015DTO;
using System.Collections.Generic;
using System.Linq;

namespace EstudoArchitectureDDDVS2015Business.Core
{
    public class TesteBC : BaseOC
    {
        public TesteBC(UsuarioLogado usuario) : base(usuario)
        {

        }

        public IEnumerable<testeDTO> CarregarDados()
        {
            //var dto = 
            var dados = _context.testeObj.Select(s => new testeDTO {
                COL1 = s.COL1,
                COL2 = s.COL2,
                COL3 = s.COL3
            }).ToList();

            return dados;
        }

        public void SalvarTeste(testeDTO dto)
        {
            var teste = testeConverter.ParaEntity(dto);
            _context.testeObj.Add(teste);
            _context.SaveChanges();
        }

        public void EditarTeste(testeDTO teste)
        {
            var entity = _context.testeObj.Single(s => s.COL1 == teste.COL1);
            testeConverter.CopiarPara(teste, entity);
            _context.SaveChanges();
        }

        public void DeletarTeste(int col1)
        {
            var teste = _context.testeObj.Single(s => s.COL1 == col1);
            _context.testeObj.Remove(teste);
            _context.SaveChanges();
        }
    }
}
