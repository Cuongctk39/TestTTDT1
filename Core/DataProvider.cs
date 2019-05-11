using System;
using System.Data;

namespace DataAccess
{
    public abstract class DataProvider
    {
        private static DataProvider instance = null;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new SqlDataProvider("SQLConnectionString");
                return instance;
            }
        }

        public abstract object ExecuteNonQueryWithOutput(string outputParam, string spName, params object[] parameterValues);

        public abstract int ExecuteNonQuery(string spName, params object[] parameterValues);

        public abstract DataSet ExecuteDataset(string spName, params object[] parameterValues);

        public abstract IDataReader ExecuteReader(string spName, params object[] parameterValues);

        public abstract object ExecuteScalar(string spName, params object[] parameterValues);


    }
}
