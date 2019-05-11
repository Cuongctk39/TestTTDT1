using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using DataAccess;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Models.BusinessModel
{
    public class TaiKhoanBusiness:BaseFunctions<TaiKhoan>
    {
        public int TaiKhoan_Insert(TaiKhoan t)
        {
            return Add(t);
        }
        public int TaiKhoan_InsertOutputId(TaiKhoan t)
        {
            return int.Parse(DataProvider.Instance.ExecuteScalar("TaiKhoan_InsertOutputId", GetInsertUpdateValues(t).ToArray()).ToString());
        }
        public List<TaiKhoan> TaiKhoanDaTonTai(string e)
        {
            return CBO.FillCollection<TaiKhoan>(DataProvider.Instance.ExecuteReader("TaiKhoan_DaTonTai", e));
        }
        public void TaiKhoan_Update(TaiKhoan t)
        {
            DataProvider.Instance.ExecuteNonQuery("TaiKhoan_Update", GetInsertUpdateValues(t).ToArray());
        }
        public TaiKhoan TaiKhoan_GetByID(int id)
        {
            return CBO.FillObject<TaiKhoan>(DataProvider.Instance.ExecuteReader("TaiKhoan_GetByID", id));
        }
    }
}