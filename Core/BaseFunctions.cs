using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Core;

using System.Reflection;

namespace DataAccess
{
    public class BaseFunctions<T> where T : class, new()
    {
        private string GetTypeT()
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// Hàm l?y danh sách các giá tr? các thu?c tính c?a m?t ??i t??ng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<object> GetInsertUpdateValues(T item)
        {
            //Lây danh sách các thu?c tính c?a m?t ?t
            object[] props = item.GetType().GetProperties();

            //Danh sách giá tr? các thu?c tính
            List<object> objvalue = new List<object>();

            //Duy?t danh sách các thu?c tính
            foreach (PropertyInfo prop in props)
            {
                //L?y giá tr? c?a m?t thu?c tính và add và danh sách giá tr? các thu?c tính
                objvalue.Add(prop.GetValue(item, null));
            }

            return objvalue;
        }

        public List<T> GetAll()
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_GetAll"));
        }

        public List<T> SelectPage(object From, object To)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_SelectPage", From, To));
        }

        public int Add(T obj)
        {
            return DataProvider.Instance.ExecuteNonQuery(this.GetTypeT() + "_Insert", this.GetInsertUpdateValues(obj).ToArray());
        }

        public int Update(T obj)
        {

            return DataProvider.Instance.ExecuteNonQuery(this.GetTypeT() + "_Update", this.GetInsertUpdateValues(obj).ToArray());
        }

        public int Delete(object ID)
        {

            return DataProvider.Instance.ExecuteNonQuery(this.GetTypeT() + "_Delete", ID);
        }

        public List<T> SelectBy(object item, string Name)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_SelectBy_" + Name, item));
        }

        public List<T> SelectByID(object ID)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_SelectByID", ID));
        }

        public List<T> FindKeyWord(object item)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_FindKeyWord", item));
        }

        public DataTable SelectTables(string SPName, params object[] parameterValues)
        {
            return null;
        }

    }
}
