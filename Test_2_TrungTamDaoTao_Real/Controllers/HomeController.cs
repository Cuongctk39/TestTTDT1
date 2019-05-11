using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test_2_TrungTamDaoTao_Real.Models.BusinessModel;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Controllers
{
    /// <summary>
    /// Đăng nhập, đăng xuất, đăng ký tài khoản
    /// </summary>
    public class HomeController : Controller
    {
        TaiKhoanBusiness tkB;
        QuyenBusiness qB;
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Đăng ký tài khoản - Get
        /// </summary>
        /// <returns></returns>
        public ActionResult DangKyTaiKhoan()
        {
            //lấy đúng tab Đăng ký
            ViewBag.tab = "DangKy";

            return View("DangNhap_DangKyTaiKhoan");
        }
        /// <summary>
        /// Đăng ký tài khoản - Post
        /// </summary>
        /// <param name="tkdk"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DangKyTaiKhoan(TaiKhoanDangKy tkdk)
        {
            if (ModelState.IsValid)
            {
                tkB = new TaiKhoanBusiness();
                TaiKhoan tk = new TaiKhoan();
                tk.Email = tkdk.EmailDK;
                tk.HoTenLot = tkdk.HoTenLotDK;
                tk.Ten = tkdk.TenDK;
                //hash mật khẩu
                string mkDaHash;
                using(MD5 md5Hash = MD5.Create())
                {
                    mkDaHash = GetMd5Hash(md5Hash, tkdk.MatKhauDK);
                }
                tk.MatKhau = mkDaHash;
                tk.IdQuyen = 1;

                //Thêm vào database
                tkB.TaiKhoan_Insert(tk);
            }
            ViewBag.tab = "DangNhap";
            ViewBag.taoTKThanhCong = "Bạn đã tạo tài khoản thành công";
            return View("DangNhap_DangKyTaiKhoan");
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
                return Json(false,JsonRequestBehavior.AllowGet);
            }
            return Json(true , JsonRequestBehavior.AllowGet);
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
        public ActionResult DangNhapTaiKhoan()
        {
            ViewBag.tab = "DangNhap";
            return View("DangNhap_DangKyTaiKhoan");
        }

        /// <summary>
        /// Đăng nhập tài khoản vào hệ thống( thông thường)
        /// </summary>
        /// <param name="tkdn"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DangNhapTaiKhoan(TaiKhoanDangNhap tkdn)
        {
            tkB = new TaiKhoanBusiness();
            if (ModelState.IsValid)
            {
                //Kiếm tra mail tồn tại
                List<TaiKhoan> dsTK = tkB.TaiKhoanDaTonTai(tkdn.Email);
                if (dsTK.Count > 0)//Nếu tài khoản đã tồn tại
                {
                    string hashedPassword;
                    using(MD5 md5hash = MD5.Create())
                    {
                        hashedPassword = GetMd5Hash(md5hash, tkdn.MatKhau);
                    }
                    if(hashedPassword== dsTK[0].MatKhau)// So sánh mật khẩu, nếu đúng thì gán Session
                    {
                        qB = new QuyenBusiness();
                        int quyenID= dsTK[0].IdQuyen;
                        string tenQuyen = qB.Quyen_GetNameById(quyenID);
                        
                        Session["Id"] = dsTK[0].IdTaiKhoan;
                        Session["Email"] = dsTK[0].Email;
                        Session["Ten"] = dsTK[0].Ten;
                        Session["Quyen"] = tenQuyen;
                        Session.Timeout=20;
                        return RedirectToAction("Index", "Home");
                    }
                    else//Nếu sai mật khẩu
                    {
                        ModelState.AddModelError("LoiDangNhap", "Mật khẩu không đúng");
                        ViewBag.tab = "DangNhap";
                        return View("DangNhap_DangKyTaiKhoan");
                    }
                }
                else//Nếu Email không đúng => tài khoản không tồn tại => báo lôi
                {
                    ModelState.AddModelError("LoiDangNhap", "Địa chỉ Email không đúng");
                }
            }
            //Active tab
            ViewBag.tab = "DangNhap";
            return View("DangNhap_DangKyTaiKhoan");
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult DangXuat()
        {
            Session["Id"] = null;
            Session["Email"] = null;
            Session["Ten"] = null;

            //Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        #region Đăng nhập với Facebook
        
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = "http://localhost:61543/Home/FacebookCallback",
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            //var url= Request.Url;
            //Nếu code = null <==> Người dùng Cancel
            if (code == null)
            {
                return RedirectToAction("DangNhapTaiKhoan","Home");
            }

            //Nếu người dùng đồng ý <=> code !=null
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = "http://localhost:61543/Home/FacebookCallback",
                code = code
            });
            
            //Session["AccessToken"] = result.access_token;
            //Gán Access Token
            fb.AccessToken = result.access_token;
            //Các thông tin lấy về
            dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
            string email = me.email;
            string firstName = me.first_name;
            string middleName = me.middle_name;
            string lastName = me.last_name;
            
            
            tkB = new TaiKhoanBusiness();
            int id;

            //Lấy tài khoản trùng với email
            List<TaiKhoan> dsTaiKhoan = tkB.TaiKhoanDaTonTai(email);

            //Nếu Email tài khoản chưa tồn tại thì tạo mới tài khoản
            if (dsTaiKhoan.Count != 1)
            {
                TaiKhoan tk = new TaiKhoan();
                tk.Email = email;
                tk.HoTenLot = middleName + " " + lastName;
                tk.Ten = firstName;
                tk.MatKhau = null;
                tk.IdQuyen = 1;
                id = tkB.TaiKhoan_InsertOutputId(tk);
            }
            else //Nếu Email đã tồn tại
            {
                id = dsTaiKhoan[0].IdTaiKhoan;
                email = dsTaiKhoan[0].Email;
                firstName = dsTaiKhoan[0].Ten;
            }

            Session["Id"] = id;
            Session["Email"] = email;
            Session["Ten"] = firstName;
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Gửi mail quên mật khẩu
        [HttpPost]
        public JsonResult QuenMatKhau(string diaChiEmail)
        {
            int result = 0;
            tkB = new TaiKhoanBusiness();
            if (ModelState.IsValid)
            {
                List<TaiKhoan> dsTK = tkB.TaiKhoanDaTonTai(diaChiEmail);
                if (dsTK.Count == 0) //Tài khoản chưa tồn tài
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else if (dsTK.Count != 0 && dsTK[0].MatKhau == "")// Tài khoản đã tồn tại, nhưng đăng nhập facebook
                {
                    result = 1;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                result = 2;//Tài khoản đã tồn tại
                string matKhauRandom = RandomString(5);
                string noiDungMail = "<p> Mật khẩu của bạn đã được đổi thành </p> " + matKhauRandom;
                //Gửi mail
                GuiMail(diaChiEmail, "Đổi mật khẩu", noiDungMail);

                //Cập nhật lại mật khẩu
                string mkMoiHashed;
                using(MD5 md5hash = MD5.Create())
                {
                    mkMoiHashed = GetMd5Hash(md5hash, matKhauRandom);
                }
                dsTK[0].MatKhau = mkMoiHashed;
                tkB.TaiKhoan_Update(dsTK[0]);
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void GuiMail(string emailNguoiNhan, string tieuDe, string noiDung)
        {
            //Lấy Tài khoản người gửi mail
            string emailNguoiGui = ConfigurationManager.AppSettings["EmailNguoiGui"];
            string matKhauNguoiGui= ConfigurationManager.AppSettings["MatKhauEmailNguoiGui"];

            //Cấu hình gửi mail
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailNguoiGui, matKhauNguoiGui);

            MailMessage mailMessage = new MailMessage(emailNguoiGui, emailNguoiNhan, tieuDe, noiDung);
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;

            client.Send(mailMessage);
        }

        /// <summary>
        /// Random ra chuỗi để đổi thành mật khẩu mới 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        static string RandomString(int size)
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder();
            char c;
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(r.Next(65, 90));
                sb.Append(c);
            }
            return sb.ToString();
        }
        #endregion
    }
}