using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using DataAccess;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Models.BusinessModel
{
    public class TaiKhoanCustomBusiness:BaseFunctions<TaiKhoanCustom>
    {
        public List<TaiKhoanCustom> TaiKhoanCustom_GetAll()
        {
            return GetAll();
        }
    }
}