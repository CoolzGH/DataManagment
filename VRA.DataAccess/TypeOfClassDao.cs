using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class TypeOfClassDao : BaseDao, ITypeOfClassDao
    {
        private static TypeOfClass LoadTypeOfClass(SqlDataReader reader)
        {
            //Создаём пустой объект
            TypeOfClass typeofclass = new TypeOfClass();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            typeofclass.TypeOfClassID = reader.GetInt32(reader.GetOrdinal("TypeOfClassID"));
            typeofclass.TypeOfClassName = reader.GetString(reader.GetOrdinal("TypeOfClassName"));
            object classhours = reader["ClassHours"];
            if (classhours != DBNull.Value)
                typeofclass.ClassHours = Convert.ToInt32(classhours);
            return typeofclass;
        }

        public TypeOfClass Get(int id)
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
                    cmd.CommandText = "SELECT * FROM TypeOfClass WHERE TypeOfClassID = @id";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@id", id);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return !dataReader.Read() ? null : LoadTypeOfClass(dataReader);
                    }
                }
            }
        }

        public IList<TypeOfClass> GetAll()
        {
            IList<TypeOfClass> typeofclasses = new List<TypeOfClass>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TypeOfClass";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            typeofclasses.Add(LoadTypeOfClass(dataReader));
                        }
                    }
                }
            }
            return typeofclasses;
        }

        public void Add(TypeOfClass typeofclass)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO TypeOfClass (TypeOfClassName, ClassHours) VALUES (@TypeOfClassName, @ClassHours)";
                    cmd.Parameters.AddWithValue("@TypeOfClassName", typeofclass.TypeOfClassName);
                    object classhours = typeofclass.ClassHours.HasValue ? (object)typeofclass.ClassHours.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@ClassHours", classhours);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(TypeOfClass typeofclass)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE TypeOfClass SET TypeOfClassName=@TypeOfClassName, ClassHours=@ClassHours WHERE TypeOfClassID = @ID";
                    cmd.Parameters.AddWithValue("@TypeOfClassName", typeofclass.TypeOfClassName);
                    cmd.Parameters.AddWithValue("@ID", typeofclass.TypeOfClassID);
                    object classhours = typeofclass.ClassHours.HasValue ? (object)typeofclass.ClassHours.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@ClassHours", classhours);
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
                    cmd.CommandText = "DELETE FROM TypeOfClass WHERE TypeOfClassID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

