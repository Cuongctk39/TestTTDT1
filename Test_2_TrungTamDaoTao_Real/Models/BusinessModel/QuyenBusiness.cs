using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using DataAccess;
using Test_2_TrungTamDaoTao_Real.Models.Model;

namespace Test_2_TrungTamDaoTao_Real.Models.BusinessModel
{
    public class QuyenBusiness:BaseFunctions<Quyen>
    {
        public string Quyen_GetNameById(int id)
        {
            return DataProvider.Instance.ExecuteScalar("Quyen_GetNameById", id).ToString();
        }
        public List<Quyen> Quyen_GetAll()
        {
            return GetAll();
        }
    }
}