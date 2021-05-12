using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRA.DataAccess.Entities;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class ReportDao : BaseDao, IReportItemDao
    {
        private static Report LoadReport(SqlDataReader reader)
        {
            //Создаём пустой объект
            Report report = new Report();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            try
            {
                report.date = reader.GetDateTime(reader.GetOrdinal("mydate"));
            }
            catch (Exception ex)
            {
                report.date = DateTime.Now.Date;
            }
            report.count = reader.GetInt32(reader.GetOrdinal("mycount"));
            return report;
        }
        public IList<Report> getPerDays(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(varchar, @start, 105) + ' - ' + CONVERT(varchar, @stop, 105)  as mydate, ISNULL(count(LoadID), 0.0) as mycount from Load where LoadDate between @start and @stop";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getPerMonth(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(varchar, @start, 105) + ' - ' + CONVERT(varchar, @stop, 105)  as mydate, ISNULL(count(LoadID), 0.0) as mycount from Load where LoadDate between @start and @stop";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }

        public IList<Report> getPerYear(DateTime start, DateTime end)
        {
            IList<Report> reports = new List<Report>();
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "select CONVERT(varchar, @start, 105) + ' - ' + CONVERT(varchar, @stop, 105)  as mydate, ISNULL(count(LoadID), 0.0) as mycount from Load where DATEPART(Year, LoadDate) between DATEPART(Year,@start) and DATEPART(YEAR, @stop)";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@stop", end);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            reports.Add(LoadReport(dataReader));
                        }
                    }
                }
            }
            return reports;
        }
    }
}
