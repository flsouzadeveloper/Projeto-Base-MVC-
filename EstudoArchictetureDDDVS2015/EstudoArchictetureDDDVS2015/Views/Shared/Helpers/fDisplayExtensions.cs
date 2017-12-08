using EstudoArchictetureDDDVS2015Util.Definitions;
using EstudoArchictetureDDDVS2015Util.Helpers.Definitions;
using EstudoArchictetureDDDVS2015Util.Helpers.Reflection;
using EstudoArchictetureDDDVS2015Util.Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace EstudoArchictetureDDDVS2015.Views.Shared.Helpers
{
    public static class fDisplayExtensions
    {
        #region Extensions Methods

        /// <summary>
        /// <para>
        /// Retorna um texto html para cada propriedade do objeto que esta representado pelo
        /// parametro expression.
        /// </para>
        /// <para>
        /// O Helper GesinDisplayFor funciona como o helper nativo DisplayFor, porem ele
        /// faz tratamento para:
        /// </para>
        /// <para>&#8226; Quebra de linha.</para>
        /// <para>&#8226; Formatacao de valores Enum e StringEnum.</para>
        /// <para>&#8226; Apresentar texto para um código quando a ViewBag está preenchida.</para>
        /// </summary>
        public static MvcHtmlString fDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            try
            {
                return GetText(htmlHelper, expression);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// <para>
        /// Retorna um texto html para cada propriedade do objeto que esta representado pelo
        /// parametro expression.
        /// </para>
        /// <para>
        /// O Helper GesinDisplayFor funciona como o helper nativo DisplayFor, porem ele
        /// faz tratamento para:
        /// </para>
        /// <para>&#8226; Quebra de linha.</para>
        /// <para>&#8226; Formatacao de valores Enum e StringEnum.</para>
        /// <para>&#8226; Apresentar texto para um código quando a ViewBag está preenchida.</para>
        /// </summary>
        public static MvcHtmlString fDisplayFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string format)
        {
            try
            {
                return GetText(htmlHelper, expression, format);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna um texto html para cada propriedade do objeto que esta representado pelo
        /// parametro expression. Essa sobrecarga é utilizada exclusivamente para apresentar
        /// Uma similaridade com CheckBoxListFor.
        /// </summary>
        public static MvcHtmlString fDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<int>>> expression, string title, IEnumerable<SelectListItem> items)
        {
            return fDisplayFor(htmlHelper, expression, title, items, null);
        }

        /// <summary>
        /// Retorna um texto html para cada propriedade do objeto que esta representado pelo
        /// parametro expression. Essa sobrecarga é utilizada exclusivamente para apresentar
        /// Uma similaridade com CheckBoxListFor.
        /// </summary>
        public static MvcHtmlString fDisplayFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, IEnumerable<int>>> expression, string title, IEnumerable<SelectListItem> items, object htmlAttributes)
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

            div.InnerHtml = htmlHelper.Partial("~/Views/Shared/Helpers/_DisplayForCheckBoxList.cshtml", parametros).ToHtmlString();

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region Support Methods

        private static MvcHtmlString GetText<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            // Para outros valores tentar localizar o StringFormater.
            var format = GetFormat(expression);

            return GetText(htmlHelper, expression, format);
        }

        private static MvcHtmlString GetText<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string format)
        {
            var value = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            // Caso exista uma viewbag definida buscar pela viewbag.
            if (GetViewBagSelectList(htmlHelper, expression) != null)
            {
                return MvcHtmlString.Create(GetTextFromViewBagSelectList(htmlHelper, expression, value));
            }

            // Caso value for uma string, tratar quebra de linha.
            var valueStr = value as string;
            if (valueStr != null) return HtmlBreakingLines(valueStr);

            // Para outros valores tentar localizar o StringFormater.
            if ((value is Enum || value is StringEnum) && format == "{0:D}")
            {
                valueStr = value is Enum
                    ? ((Enum)(object)value).ToDescriptionString()
                    : ((StringEnum)(object)value).ToDescriptionString();
            }
            else
            {
                valueStr = string.Format(format, value);
            }

            return HtmlBreakingLines(valueStr);
        }

        public static MvcHtmlString HtmlBreakingLines(string text)
        {
            return MvcHtmlString.Create(text.Replace(Environment.NewLine, "<br/>"));
        }

        private static string GetTextFromViewBagSelectList<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, TValue value)
        {
            var data = GetViewBagSelectList(htmlHelper, expression);

            if (data == null) return null;

            var strValue = string.Format("{0}", value);

            return data.Where(w => w.Value == strValue).Select(s => s.Text).FirstOrDefault() ?? string.Empty;
        }

        private static IEnumerable<SelectListItem> GetViewBagSelectList<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.ViewData[ExpressionHelper.GetExpressionText(expression)] as IEnumerable<SelectListItem>;
        }

        private static string GetFormat<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var formatAttr = expression.GetAttribute<DisplayFormatAttribute, TModel, TValue>();

            return formatAttr != null ? formatAttr.DataFormatString : "{0}";
        }

        #endregion
    }
}