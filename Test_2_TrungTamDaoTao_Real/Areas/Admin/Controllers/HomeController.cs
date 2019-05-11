using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_2_TrungTamDaoTao_Real.Models;
using Test_2_TrungTamDaoTao_Real.Models.BusinessModel;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Areas.Admin.Controllers
{
    [Permission(QuyenTruyCap ="admin")]
    public class HomeController : Controller
    {
        TaiKhoanBusiness tkB;
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Kiểm tra tên tài khoản đã tồn tại trong database hay chưa
        /// </summary>
        /// <param name="EmailDK"></param>
        /// <returns></returns>
        public JsonResult IsAlreadyExistEmail(string EmailDK)
        {
            tkB = new TaiKhoanBusiness();
            List<TaiKhoan> dsTK = tkB.TaiKhoanDaTonTai(EmailDK);
            if (dsTK.Count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}