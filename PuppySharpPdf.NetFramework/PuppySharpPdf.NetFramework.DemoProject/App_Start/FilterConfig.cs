using System.Web;
using System.Web.Mvc;

namespace PuppySharpPdf.NetFramework.DemoProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
