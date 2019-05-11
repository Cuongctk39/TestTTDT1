using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_2_TrungTamDaoTao_Real.Models.Model
{
    public class TaiKhoanCustom
    {
        public int IdTaiKhoan { get; set; }
        public string Email { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public string MatKhau { get; set; }
        public int IdQuyen { get; set; }
        public string TenQuyen { get; set; }
        public TaiKhoanCustom() { }
        public TaiKhoanCustom(int id, string email, string hoTenlot, string ten, string matKhau, int idQuyen, string tenQuyen)
        {
            this.IdTaiKhoan = id;
            this.Email = email;
            this.HoTenLot = hoTenlot;
            this.Ten = ten;
            this.MatKhau = matKhau;
            this.IdQuyen = idQuyen;
            this.TenQuyen = tenQuyen;
        }
    }
}