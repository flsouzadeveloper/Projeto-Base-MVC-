using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class ButtonExtensions
    {
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonId, string buttonText)
        {
            return htmlHelper.Button(buttonId, buttonText, true);
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonId, string buttonText, bool visible)
        {
            return htmlHelper.Button(buttonId, buttonText, visible, null);
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonId, string buttonText, bool visible, object htmlAttributes)
        {
            return htmlHelper.Button(buttonId, buttonText, visible, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string buttonId, string buttonText, bool visible, IDictionary<string, object> htmlAttributes)
        {
            if (!visible) return null;

            var botao = new TagBuilder("input");

            if (htmlAttributes != null)
            {
                botao.MergeAttributes(htmlAttributes);
            }

            botao.MergeAttribute("Id", buttonId);
            botao.MergeAttribute("type","button");
            botao.MergeAttribute("value", buttonText);

            return MvcHtmlString.Create(botao.ToString(TagRenderMode.Normal));
        }
    }
}