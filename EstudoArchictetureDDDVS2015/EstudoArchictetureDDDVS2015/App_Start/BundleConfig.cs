using System.Web;
using System.Web.Optimization;

namespace EstudoArchictetureDDDVS2015
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterJquery(bundles);
            RegisterJqueryval(bundles);
            RegisterSlider(bundles);
            RegisterModernizr(bundles);
            RegisterJqueryUI(bundles);
            RegisterBootstrap(bundles);
            RegisterBootbox(bundles);
            RegisterBaseSite(bundles);
            RegisterDominios(bundles);
            //RegisterForeachGrid(bundles);
        }

        private static void RegisterBaseSite(BundleCollection bundles)
        {
            //Site
            bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js"));
        }

        private static void RegisterJquery(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
        }

        private static void RegisterJqueryUI(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.maskedinput-{version}.js", // para mascarar campos de data.
                        "~/Scripts/jquery-ui-datepicker-ptBR.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery-ui-1.9.2.css",
                        "~/Content/themes/base/jquery-ui-1.9.2.min.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

        private static void RegisterBootstrap(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css", "~/Content/bootstrap.min.css"));
        }

        private static void RegisterBootbox(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include("~/Scripts/bootbox.min.js"));
        }

        private static void RegisterJqueryval(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
        }

        private static void RegisterSlider(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/slider/css").Include("~/Content/slider/fSlider.css"));
            bundles.Add(new ScriptBundle("~/bundles/slider").Include("~/Scripts/fSlider.js"));
        }

        private static void RegisterModernizr(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));
        }

        private static void RegisterDominios(BundleCollection bundles)
        {
            // teste
            bundles.Add(new ScriptBundle("~/bundles/dominios/teste").Include("~/Scripts/dominios/teste.js"));
        }
    }
}
