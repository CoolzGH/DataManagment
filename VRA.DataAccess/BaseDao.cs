using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;


namespace VRA.DataAccess
{
    public class BaseDao
    {
        /// <summary>
        /// Возвращает объект подключения к базе
        /// </summary>
        /// <returns></returns>
        protected static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        /// <summary>
        /// Возвращает строку подключения к базе
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["vradb"].ConnectionString;
        }

    }
}
