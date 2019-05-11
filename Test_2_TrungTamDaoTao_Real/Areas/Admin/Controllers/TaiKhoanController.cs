using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Test_2_TrungTamDaoTao_Real.Models;
using Test_2_TrungTamDaoTao_Real.Models.BusinessModel;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Areas.Admin.Controllers
{
    [Permission(QuyenTruyCap ="admin")]
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        TaiKhoanBusiness tkB;
        TaiKhoanCustomBusiness tkCustomB;
        QuyenBusiness qB;
        public ActionResult Index()
        {
            ViewBag.navLink = "TaiKhoan";
            qB = new QuyenBusiness();
            List<Quyen> dsQuyen = qB.Quyen_GetAll();
            ViewBag.dsQuyen = new SelectList(dsQuyen, "IdQuyen", "TenQuyen");
            return View();
        }
        public JsonResult LayDSTaiKhoan()
        {
            tkCustomB = new TaiKhoanCustomBusiness();
            List<TaiKhoanCustom> dsTaiKhoanCustom= tkCustomB.TaiKhoanCustom_GetAll();
            return Json(new { data = dsTaiKhoanCustom },JsonRequestBehavior.AllowGet);
        }
        public JsonResult LayThongTinTaikhoan(int idtk)
        {
            tkB = new TaiKhoanBusiness();
            TaiKhoan tk= tkB.TaiKhoan_GetByID(idtk);
            var data = JsonConvert.SerializeObject(tk, Formatting.Indented);
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChinhSuaTaiKhoan(TaiKhoan t)
        {
            tkB = new TaiKhoanBusiness();
            tkB.TaiKhoan_Update(t);
            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TaoMoiTaiKhoan(TaiKhoanDangKyAdmin t)
        {
            tkB = new TaiKhoanBusiness();
            TaiKhoan tk = new TaiKhoan();
            tk.Email = t.EmailDK;
            tk.HoTenLot = t.HoTenLotDK;
            tk.Ten = t.TenDK;
            string passwordHashed;
            using(MD5 md5hash = MD5.Create())
            {
                passwordHashed = GetMd5Hash(md5hash, t.MatKhauDK);
            }
            tk.MatKhau =passwordHashed;
            tk.IdQuyen = t.IdQuyen;
            tkB.TaiKhoan_Insert(tk);
            return Json(JsonRequestBehavior.AllowGet);
        }

        #region MD5
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #endregion
    }
}