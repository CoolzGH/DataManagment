using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class LoadDao : BaseDao, ILoadDao
    {
        private static Load LoadLoad(SqlDataReader reader)
        {
            //Создаём пустой объект
            Load load = new Load();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            load.LoadID = reader.GetInt32(reader.GetOrdinal("LoadID"));
            load.TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID"));
            load.GroupNumber = reader.GetInt32(reader.GetOrdinal("GroupNumber"));  
            object loaddate = reader["LoadDate"];
            if (loaddate != DBNull.Value)
                load.LoadDate = Convert.ToDateTime(loaddate);
            load.SubjectID = reader.GetInt32(reader.GetOrdinal("SubjectID"));
            load.TypeOfClassID = reader.GetInt32(reader.GetOrdinal("TypeOfClassID"));
            return load;
        }

        public Load Get(int id)
        {
            //Получаем объект подключения к базе
            using (var conn = GetConnection())
            {
                //Открываем соединение
                conn.Open();
                //Создаем sql команду
                using (var cmd = conn.CreateCommand())
                {
                    //Задаём текст команды
                    cmd.CommandText = "SELECT * FROM Load WHERE LoadID = @id";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@id", id);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return !dataReader.Read() ? null : LoadLoad(dataReader);
                    }
                }
            }
        }

        public IList<Load> GetAll()
        {
            IList<Load> loads = new List<Load>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Load";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            loads.Add(LoadLoad(dataReader));
                        }
                    }
                }
            }
            return loads;
        }

        public void Add(Load load)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Load (TeacherID, GroupNumber, LoadDate, SubjectID, TypeOfClassID) VALUES (@TeacherID, @GroupNumber, @LoadDate, @SubjectID, @TypeOfClassID)";
                    cmd.Parameters.AddWithValue("@TeacherID", load.TeacherID);
                    cmd.Parameters.AddWithValue("@GroupNumber", load.GroupNumber);
                    object loaddate = load.LoadDate.HasValue ? (object)load.LoadDate.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@LoadDate", loaddate);
                    cmd.Parameters.AddWithValue("@SubjectID", load.SubjectID);
                    cmd.Parameters.AddWithValue("@TypeOfClassID", load.TypeOfClassID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Load load)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Load SET TeacherID=@TeacherID, GroupNumber=@GroupNumber, LoadDate=@LoadDate, SubjectID=@SubjectID, TypeOfClassID=@TypeOfClassID WHERE LoadID = @ID";
                    cmd.Parameters.AddWithValue("@TeacherID", load.TeacherID);
                    cmd.Parameters.AddWithValue("@GroupNumber", load.GroupNumber);
                    object loaddate = load.LoadDate.HasValue ? (object)load.LoadDate.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@LoadDate", loaddate);
                    cmd.Parameters.AddWithValue("@SubjectID", load.SubjectID);
                    cmd.Parameters.AddWithValue("@TypeOfClassID", load.TypeOfClassID);
                    cmd.Parameters.AddWithValue("@ID", load.LoadID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Load WHERE LoadID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Load> SearchLoads(string LoadID, string TeacherID, string GroupNumber, string LoadDate, string SubjectID, string TypeOfClassID, string StartDate, string EndDate, int check1, int check2)
        {
            IList<Load> loads = new List<Load>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Load WHERE LoadID like CASE WHEN @LoadID not like '' THEN @LoadID Else '%' END AND TeacherID like CASE WHEN @TeacherID not like '' THEN @TeacherID Else '%' END AND GroupNumber like CASE WHEN @GroupNumber not like '' THEN @GroupNumber Else '%' END AND (LoadDate like @LoadDate OR (LoadDate IS NULL AND @LoadDate like '%')) AND SubjectID like CASE WHEN @SubjectID not like '' THEN @SubjectID Else '%' END AND TypeOfClassID like CASE WHEN @TypeOfClassID not like '' THEN @TypeOfClassID Else '%' END AND (LoadDate BETWEEN CASE WHEN @check1=1 THEN @StartDate ELSE '1000-01-01' END AND CASE WHEN @check2=1 THEN @EndDate ELSE '3000-01-01' END OR (LoadDate IS NULL AND @StartDate like '%'))";
                    cmd.Parameters.AddWithValue("@LoadID", LoadID);
                    cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
                    cmd.Parameters.AddWithValue("@GroupNumber", GroupNumber);
                    cmd.Parameters.AddWithValue("@LoadDate", LoadDate);
                    cmd.Parameters.AddWithValue("@SubjectID", SubjectID);
                    cmd.Parameters.AddWithValue("@TypeOfClassID", TypeOfClassID);
                    cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", EndDate);
                    cmd.Parameters.AddWithValue("@check1", check1);
                    cmd.Parameters.AddWithValue("@check2", check2);
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            loads.Add(LoadLoad(dataReader));
                        }
                    }
                }
            }
            return loads;
        }
    }
}