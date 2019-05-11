using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_2_TrungTamDaoTao_Real.Models
{
    public class PermissionAttribute:AuthorizeAttribute
    {
        public string QuyenTruyCap { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["Quyen"] == null)
            {
                return false;
            }
            string quyenHienTai = HttpContext.Current.Session["Quyen"].ToString().ToLower();
            if (this.QuyenTruyCap.ToLower().Contains(quyenHienTai))
            {
                return true;
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new RedirectToRouteResult(
            //                       new System.Web.Routing.RouteValueDictionary
            //                       {
            //                           { "action", "Login" },
            //                           { "controller", "Home" }
            //                       });
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/_401.cshtml"
            };
        }
    }
}