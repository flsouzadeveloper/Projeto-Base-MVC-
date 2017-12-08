using System.Web;
using System.Web.Mvc;

namespace EstudoArchictetureDDDVS2015
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
