using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_2_TrungTamDaoTao_Real.Models.Model
{
    public class Quyen
    {
        public int IdQuyen { get; set; }
        public string TenQuyen { get; set; }
        public Quyen() { }
        public Quyen(int id, string tenQuyen)
        {
            this.IdQuyen = id;
            this.TenQuyen = tenQuyen;
        }
    }
}