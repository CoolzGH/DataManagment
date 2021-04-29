using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace VRA.DataAccess
{
    public class TeacherDao : BaseDao, ITeacherDao
    {
        private static Teacher LoadTeacher(SqlDataReader reader)
        {
            //Создаём пустой объект
            Teacher teacher = new Teacher();
            //Заполняем поля объекта в соответствии с названиями полей результирующего
            // набора данных
            teacher.TeacherID = reader.GetInt32(reader.GetOrdinal("TeacherID"));
            teacher.SecondName = reader.GetString(reader.GetOrdinal("SecondName"));
            teacher.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
            teacher.MiddleName = reader.GetString(reader.GetOrdinal("MiddleName"));
            teacher.AcademicDegree = reader.GetString(reader.GetOrdinal("AcademicDegree"));
            teacher.Position = reader.GetString(reader.GetOrdinal("Position"));
            object experience = reader["Experience"];
            if (experience != DBNull.Value)
                teacher.Experience = Convert.ToInt32(experience);
            return teacher;
        }

        public Teacher Get(int id)
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
                    cmd.CommandText = "SELECT TeacherID, SecondName, FirstName, MiddleName, AcademicDegree, Position, Experience FROM Teacher WHERE TeacherID = @id";
                    //Добавляем значение параметра
                    cmd.Parameters.AddWithValue("@id", id);
                    //Открываем SqlDataReader для чтения полученных в результате
                    // выполнения запроса данных
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        return !dataReader.Read() ? null : LoadTeacher(dataReader);
                    }
                }
            }
        }

        public IList<Teacher> GetAll()
        {
            IList<Teacher> teachers = new List<Teacher>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TeacherID, SecondName, FirstName, MiddleName, AcademicDegree, Position, Experience FROM Teacher";
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            teachers.Add(LoadTeacher(dataReader));
                        }
                    }
                }
            }
            return teachers;
        }

        public void Add(Teacher teacher)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Teacher (SecondName, FirstName, MiddleName, AcademicDegree, Position, Experience) VALUES (@SecondName, @FirstName, @MiddleName, @AcademicDegree, @Position, @Experience)";
                    cmd.Parameters.AddWithValue("@SecondName", teacher.SecondName);
                    cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", teacher.MiddleName);
                    cmd.Parameters.AddWithValue("@AcademicDegree", teacher.AcademicDegree);
                    cmd.Parameters.AddWithValue("@Position", teacher.Position);
                    object experience = teacher.Experience.HasValue ? (object)teacher.Experience.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@Experience", experience);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Teacher teacher)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Teacher SET SecondName=@SecondName, FirstName=@FirstName, MiddleName=@MiddleName, AcademicDegree=@AcademicDegree, Position=@Position, Experience=@Experience WHERE TeacherID = @ID";
                    cmd.Parameters.AddWithValue("@SecondName", teacher.SecondName);
                    cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", teacher.MiddleName);
                    cmd.Parameters.AddWithValue("@AcademicDegree", teacher.AcademicDegree);
                    cmd.Parameters.AddWithValue("@Position", teacher.Position);
                    cmd.Parameters.AddWithValue("@ID", teacher.TeacherID);
                    object experience = teacher.Experience.HasValue ? (object)teacher.Experience.Value : DBNull.Value;
                    cmd.Parameters.AddWithValue("@Experience", experience);
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
                    cmd.CommandText = "DELETE FROM Teacher WHERE TeacherID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Teacher> SearchTeachers(string SecondName)
        {
            IList<Teacher> teachers = new List<Teacher>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TeacherID, SecondName, FirstName, MiddleName, AcademicDegree, Position, Experience FROM Teacher WHERE SecondName like @SecondName";
                    cmd.Parameters.AddWithValue("@SecondName","%"+SecondName +"%");
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            teachers.Add(LoadTeacher(dataReader));
                        }
                    }
                }
            }
            return teachers;
        }

    }
}