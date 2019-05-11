using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_2_TrungTamDaoTao_Real.Models.BusinessModel;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Controllers
{
    public class TaiKhoanController : Controller
    {
        TaiKhoanBusiness tkB;
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LayThongTinTaiKhoan(int id)
        {
            tkB = new TaiKhoanBusiness();
            TaiKhoan tk= tkB.TaiKhoan_GetByID(id);
            return Json(tk, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChinhSuaTaiKhoan(TaiKhoan t)
        {
            tkB = new TaiKhoanBusiness();
            tkB.TaiKhoan_Update(t);
            Session["Ten"] = t.Ten;
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}