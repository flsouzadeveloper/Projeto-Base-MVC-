using EstudoArchictetureDDDVS2015.Views.Shared.Helpers;
using EstudoArchictetureDDDVS2015Util.Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class CheckBoxListExtension
    {
        public static MvcHtmlString CheckBoxListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<int>>> expression, string title, IEnumerable<SelectListItem> items)
        {
            var defaultHtmlAttributes = new { style = "width: 100%; height: 200px;" };

            return htmlHelper.CheckBoxListFor(expression, title, items, defaultHtmlAttributes);
        }

        public static MvcHtmlString CheckBoxListFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<int>>> expression, string title, IEnumerable<SelectListItem> items, object htmlAttributes)
        {
            // Recupera o nome a partir da expressao
            var name = htmlHelper.NameFor(expression).ToString();
            var id = htmlHelper.IdFor(expression).ToString();
            var idUnico = DateTime.Now.Ticks.ToString();

            // Recupera a lista de itens a ser construida.
            var itens = items ?? htmlHelper.ViewData[name] as IEnumerable<SelectListItem>;

            if (itens == null) throw new ArgumentOutOfRangeException(string.Format(MensagensGenericas.NaoFoiEncontradoUmaViewbagComNomeDefinido, name));

            // Constroi a div principal
            var div = new TagBuilder("div");

            div.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            div.AddCssClass("checkbox-list");
            div.MergeAttribute("id", id);
            div.MergeAttribute("data-idunico", idUnico);

            // Recupera os itens que virao selecionados.
            IEnumerable<int> selecionados;

            try
            {
                selecionados = expression.Compile().Invoke(htmlHelper.ViewData.Model);
            }
            catch
            {
                selecionados = null;
            }

            // Chama a partial view que renderiza o conteudo deste helper.
            var parametros = new CheckBoxListForParametros
            {
                IdHtml = id,
                IdUnico = idUnico,
                NomeHtml = name,
                Selecionados = (selecionados ?? new List<int>()).ToArray(),
                Itens = itens.ToArray(),
                Title = title
            };

            div.InnerHtml = htmlHelper.Partial("~/Views/Shared/Helpers/_CheckBoxListFor.cshtml", parametros).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }
    }

    public class CheckBoxListForParametros
    {
        public string IdHtml { get; set; }

        public string IdUnico { get; set; }

        public string NomeHtml { get; set; }

        public int[] Selecionados { get; set; }

        public SelectListItem[] Itens { get; set; }

        public string Title { get; set; }
    }
}
