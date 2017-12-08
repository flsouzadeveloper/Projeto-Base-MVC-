using EstudoArchictetureDDDVS2015.Models;
using EstudoArchitectureDDDVS2015DTO;
using System.Web.Mvc;

namespace EstudoArchictetureDDDVS2015.Controllers
{
    public class testeController : BaseController
    {
        // GET: teste
        public ActionResult teste()
        {
            return View();
        }

        // GET: teste
        public ActionResult _ExibirTeste()
        {
            var modelGrid = TesteBC.CarregarDados();
            return PartialView("_ExibirTeste", modelGrid);
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <returns>Retorna valor selecionado na tela</returns>
        public ActionResult ConsultarTeste()
        {
            return View();
        }

        // POST: teste
        public ActionResult _SalvarTeste(TesteModel model)
        {
            TesteBC.SalvarTeste(model.ParaDTO());

            //var modelGrid = TesteBC.CarregarDados();
            return PartialView("_ExibirTeste");
        }

        // GET: Editar Teste
        //public ActionResult _EditarTeste(TesteModel model)
        public ActionResult _EditarTeste(int idTeste)
        {
            //TesteBC.EditarTeste(model.ParaDTO());

            AlertaComMensagem("teste");
            return View();
            //return PartialView("_ExibirTeste");
            //return null;
        }

        public ActionResult _DeletarTeste(int idDelTeste)
        {
            TesteBC.DeletarTeste(idDelTeste);
            return PartialView("_ExibirTeste");
        }
    }
}
