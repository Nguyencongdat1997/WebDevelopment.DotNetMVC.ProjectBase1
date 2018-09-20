using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DotNetMVCBaseProject.DAO
{
    public abstract class DBContext<T>
    {
        public SqlConnection connection;
        public DBContext()
        {
            string connectionString = ConfigurationManager.
                ConnectionStrings["DBConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

    }
}