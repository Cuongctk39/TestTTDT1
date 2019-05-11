using System.Web;
using System.Web.Mvc;

namespace Test_2_TrungTamDaoTao_Real
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
