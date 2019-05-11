using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_2_TrungTamDaoTao_Real.Models.Model
{
    public class TaiKhoan
    {
        public int IdTaiKhoan { get; set; }
        public string Email { get; set; }
        [Display(Name = "Họ và tên lót")]
        [Required(ErrorMessage = "Vui lòng điền Họ và tên lót")]
        public string HoTenLot { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng điền Tên")]
        public string Ten { get; set; }
        public string MatKhau { get; set; }
        public int IdQuyen { get; set; }
        public TaiKhoan() { }
        public TaiKhoan(int id, string email, string hoTenlot, string ten, string matKhau, int idQuyen)
        {
            this.IdTaiKhoan = id;
            this.Email = email;
            this.HoTenLot = hoTenlot;
            this.Ten = ten;
            this.MatKhau = matKhau;
            this.IdQuyen = idQuyen;
        }
    }
    public class TaiKhoanDangKy
    {
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền Địa chỉ Email")]
        [System.Web.Mvc.Remote("IsAlreadyExistEmail","Home",ErrorMessage ="Email này đã tồn tại")]
        public string EmailDK { get; set; }

        [Display(Name = "Họ và tên lót")]
        [Required(ErrorMessage = "Vui lòng điền Họ và tên lót")]
        public string HoTenLotDK { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng điền Tên")]
        public string TenDK { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền Mật khẩu")]
        public string MatKhauDK { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền lại Mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("MatKhauDK",ErrorMessage ="Mật khẩu nhập lại không trùng")]
        public string NhapLaiMatKhauDK { get; set; }
    }

    public class TaiKhoanDangKyAdmin
    {
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền Địa chỉ Email")]
        [System.Web.Mvc.Remote("IsAlreadyExistEmail", "Home", ErrorMessage = "Email này đã tồn tại")]
        public string EmailDK { get; set; }

        [Display(Name = "Họ và tên lót")]
        [Required(ErrorMessage = "Vui lòng điền Họ và tên lót")]
        public string HoTenLotDK { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng điền Tên")]
        public string TenDK { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền Mật khẩu")]
        public string MatKhauDK { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền lại Mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("MatKhauDK", ErrorMessage = "Mật khẩu nhập lại không trùng")]
        public string NhapLaiMatKhauDK { get; set; }

        public int IdQuyen { get; set; }
    }

    public class TaiKhoanDangNhap
    {
        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng điền Địa chỉ Email")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền Mật khẩu")]
        public string MatKhau { get; set; }
    }
    
}