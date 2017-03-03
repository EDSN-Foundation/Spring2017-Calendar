using System.Web;
using System.Web.Mvc;

namespace EDSNCalendar_ProjectBlue
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
