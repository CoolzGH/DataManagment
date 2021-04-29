using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class SubjectDao : BaseDao, ISubjectDao
    {
        private static Subject LoadSubject(SqlDataReader reader)
        {
            //Создаём пустой объект
            Subject subject = new Subject();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            subject.SubjectID = reader.GetInt32(reader.GetOrdinal("SubjectID"));
            subject.Title = reader.GetString(reader.GetOrdinal("Title"));
            object subjecthours = reader["SubjectHours"];
            if (subjecthours != DBNull.Value)
                subject.SubjectHours = Convert.ToInt32(subjecthours);
            return subject;
        }

        public Subject Get(int id)
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
                    cmd.CommandText = "SELECT SubjectID, Title, SubjectHours FROM Subject WHERE SubjectID = @id";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@id", id);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return !dataReader.Read() ? null : LoadSubject(dataReader);
                    }
                }
            }
        }

        public IList<Subject> GetAll()
        {
            IList<Subject> subjects = new List<Subject>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Subject";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            subjects.Add(LoadSubject(dataReader));
                        }
                    }
                }
            }
            return subjects;
        }

        public void Add(Subject subject)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Subject (Title, SubjectHours) VALUES (@Title, @SubjectHours)";
                    cmd.Parameters.AddWithValue("@Title", subject.Title);
                    object subjecthours = subject.SubjectHours.HasValue ? (object)subject.SubjectHours.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@SubjectHours", subjecthours);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Subject subject)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Subject SET Title=@Title, SubjectHours=@SubjectHours WHERE SubjectID = @ID";
                    cmd.Parameters.AddWithValue("@Title", subject.Title);
                    cmd.Parameters.AddWithValue("@ID", subject.SubjectID);
                    object subjecthours = subject.SubjectHours.HasValue ? (object)subject.SubjectHours.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@SubjectHours", subjecthours);
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
                    cmd.CommandText = "DELETE FROM Subject WHERE SubjectID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

