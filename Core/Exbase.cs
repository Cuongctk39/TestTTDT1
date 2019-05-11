using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Exbase
    {
        public string ConnectionString
        { get; set; }

        public DataProvider sqlDataProvider
        { get; set; }

        public Exbase() 
        {
            this.sqlDataProvider = new SqlDataProvider(this.ConnectionString);
        }

        public Exbase(string connectionStringName) 
        {
            this.ConnectionString = connectionStringName;
            this.sqlDataProvider = new SqlDataProvider(this.ConnectionString);
        }

        //Dinh nghia cac phuong thuc tao cac SQL

        public int Add(object obj)
        {
            return 1;
        }
    }
}
