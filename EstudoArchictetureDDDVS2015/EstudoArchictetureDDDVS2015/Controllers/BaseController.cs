using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EstudoArchictetureDDDVS2015Util.Mensagens;
using EstudoArchictetureDDDVS2015Util.Security;
using EstudoArchitectureDDDVS2015Business.Core;
using System.ComponentModel;
using EstudoArchictetureDDDVS2015Util.Helpers.Logs;
using EstudoArchictetureDDDVS2015.Views.Shared.Helpers;

namespace EstudoArchictetureDDDVS2015.Controllers
{
    [NoCache]
    public class BaseController : Controller
    {
        #region Businner Access

        // Contem a referencia da primeira bc instanciada.
        private BaseOC _baseBC;

        private TesteBC _testeBC;

        protected TesteBC TesteBC
        {
            get { return _testeBC ?? (_testeBC = CriarBC<TesteBC>()); }
        }

        private T CriarBC<T>() where T : BaseOC
        {
            if (_baseBC != null) return _baseBC.OutraBC<T>();

            var usuario = Session.GetUsuarioLogado();

            var bc = (T)Activator.CreateInstance(typeof (T), new object[] { usuario });

            _baseBC = bc;

            return bc;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_baseBC != null) _baseBC.Dispose();
            // Quando uma bc eh criada a partir de outra (pelo metodo OutraBC<T>)
            // as demais bc's ficam vinculadas ou seja, se eh chamado o Dispose na primeira
            // o Dispose eh cascateado pelas outras.
        }

        #endregion

        #region Handling Errors

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var value in ModelState.Values)
            {
                // Acerta erros onde o ModelError.ErrorMessage vem vazio.
                if (value != null)
                {

                }
                value.FixErrorsWithoutMessage();
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            ErrorStorer.Storer.Create(filterContext.Exception);

            if (Request.IsAjaxRequest()) // Para requisicao Ajax.
            {
                Response.Clear();
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception is PrintableException
                            ? fDisplayExtensions.HtmlBreakingLines(filterContext.Exception.Message).ToHtmlString()
                            : MensagensGenericas.OcorreuUmErroAoTentarProcessarAInformacao
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else // Para requisicao tradicional (nao Ajax).
            {
                TempData["Exception"] = filterContext.Exception;

                filterContext.Result = RedirectToAction("Default", "Error");
            }
        }

        #endregion

        #region Mensagens do Sistema

        public void AlertaComMensagem(string mensagem)
        {
            ControllerHelper.AlertaComMensagem(this, mensagem);
        }

        public void AlertaComMensagem(string mensagem, bool somenteViewContext)
        {
            ControllerHelper.AlertaComMensagem(this, mensagem, somenteViewContext);
        }

        #endregion

    }

    public static class ControllerHelper
    {
        #region Extensões para as mensagens do sistema

        public static void AlertaComMensagem(this Controller controller, string mensagem)
        {
            var msg = controller.TempData["MensagemSistema"] as string;

            controller.TempData["MensagemSistema"] = msg != null ? msg + Environment.NewLine + mensagem : mensagem;
        }

        public static void AlertaComMensagem(this Controller controller, string mensagem, bool somenteViewContext)
        {
            if (!somenteViewContext) controller.AlertaComMensagem(mensagem);

            var msg = controller.TempData["MensagemSistema"] as string;

            controller.TempData["MensagemSistema"] = msg != null ? msg + Environment.NewLine + mensagem : mensagem;

        }

        #endregion

        #region Extensoes para SelectList

        public static IEnumerable<SelectListItem> AddBlank(this IEnumerable<SelectListItem> selectList)
        {
            var items = selectList.ToList();

            items.Insert(0, new SelectListItem { Value = string.Empty, Text = string.Empty });

            return items;
        }

        public static IEnumerable<SelectListItem> AddBlank(this IEnumerable<SelectListItem> selectList, string text)
        {
            var items = selectList.ToList();

            items.Insert(0, new SelectListItem { Value = string.Empty, Text = text });

            return items;
        }

        public static IEnumerable<SelectListItem> SelectedValue(this IEnumerable<SelectListItem> selectList, object value)
        {
            var strValue = string.Format("{0}", value);

            selectList.Where(w => w.Value == strValue).ToList().ForEach(f => f.Selected = true);

            return selectList;
        }

        public static IEnumerable<SelectListItem> SelectListFrom(Type enumType)
        {
            var nomes = Enum.GetNames(enumType);
            var itens = new List<SelectListItem>();

            foreach (var nome in nomes)
            {
                try
                {
                    itens.Add(GenerateItemFrom(enumType, nome));
                }
                catch
                {
                }
            }

            return itens;
        }

        private static SelectListItem GenerateItemFrom(Type enumType, string nome)
        {
            var member = enumType.GetMember(nome).First();
            var description = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute));

            var text = description != null
                ? description.Description
                : nome;

            var valor = (int)Enum.Parse(enumType, nome);

            return new SelectListItem { Value = valor.ToString(), Text = text };
        }

        public static IEnumerable<SelectListItem> SelectListFrom(Type enumType, object selectedValue)
        {
            return SelectListFrom(enumType).SelectedValue(selectedValue);
        }

        public static IEnumerable<SelectListItem> SelectListFrom<T>(IEnumerable<T> enumCollection)
        {
            var enumType = typeof(T);
            var itens = new List<SelectListItem>();

            enumCollection
                .Select(s => string.Format("{0}", s))
                .ToList()
                .ForEach(f =>
                {
                    try
                    {
                        itens.Add(GenerateItemFrom(enumType, f));
                    }
                    catch
                    {
                    }
                });

            return itens;
        }

        #endregion

        #region Extensoes para ModelState

        public static string GetErrorsText(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;

            return modelState
                .SelectMany(s => s.Value.Errors.Select(t => t.ErrorMessage))
                .Aggregate(string.Empty, (ac, i) => ac + Environment.NewLine + TraduzirRequired(i))
                .Substring(2);
        }

        /// <summary>
        /// Este metodo foi inserido pois o pacote MVC Foolproof Validation tem apresentado, um erro.
        /// O erro consiste em utilizar a mensagem padrao (em ingles) para qualquer campo de tipo primitivo
        /// nao anulavel (int, long, short, double) em que na QueryString ou no POST nao foi incluido o
        /// mesmo.
        /// Para outros casos a mensagem tem respeitado o parametro 'ErrorMessage', ou seja a mensagem
        /// vem traduzida.
        /// Ate o momento desta codificacao o pacote MVC Foolproof Validation nao foi atualizado portanto
        /// o erro persiste. Caso o pacote seja atualizado e o erro corrigido este metodo pode ser removido.
        /// </summary>
        public static string TraduzirRequired(string msg)
        {
            var match = Regex.Match(msg, @"(?<=^The\s+).+(?=\s+field is required[.]$)");

            return match.Success
                ? string.Format(PadraoDataAnnotations.Required, match.Value)
                : msg;
        }

        /// <summary>
        /// Acerta erros onde o ModelError.ErrorMessage vem vazio.
        /// </summary>
        public static void FixErrorsWithoutMessage(this ModelState modelState)
        {
            if (modelState.Errors.All(a => !string.IsNullOrEmpty(a.ErrorMessage))) return;

            foreach (var error in modelState.Errors.Where(w => string.IsNullOrEmpty(w.ErrorMessage)).ToList())
            {
                var exception = error.Exception.GetBaseException();

                if (IsIntegerOverflowException(exception))
                {
                    modelState.Errors.Remove(error);

                    var errorMessage = string.Format(PadraoDataAnnotations.IntegerOverflow, modelState.Value.AttemptedValue);
                    modelState.Errors.Add(new ModelError(errorMessage));
                }
            }
        }

        private static bool IsIntegerOverflowException(Exception exception)
        {
            if (!(exception is OverflowException)) return false;

            var integerRegex = new Regex(@"(?<=(^|[ .]))(U)?Int((16)|(32)|(64))(?=(\W|$))");
            var parseIntegerRegex = new Regex(@"^Parse(U)?Int((16)|(32)|(64))$");

            if (!integerRegex.IsMatch(exception.Message)) return false;

            var frames = new StackTrace(exception, false).GetFrames();
            if (frames == null) return false;

            return frames
                .Select(s => s.GetMethod())
                .Any(a => parseIntegerRegex.IsMatch(a.Name)
                       && a.DeclaringType != null
                       && a.DeclaringType.FullName == "System.Number");
        }

        #endregion
    }


    #region Controle de Cache

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }

    #endregion
}