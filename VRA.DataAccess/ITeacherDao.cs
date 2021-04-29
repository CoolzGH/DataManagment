using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    /// <summary>
    /// Описание действий с объектом учителя в базе
    /// </summary>
    public interface ITeacherDao
    {
        /// <summary>
        /// Получить учителя по ID
        /// </summary>
        /// <param name="id">ID учителя</param>
        /// <returns>Учитель</returns>
        Teacher Get(int id);

        /// <summary>
        /// Получить список всех учителей в базе
        /// </summary>
        /// <returns>Список всех учителей в базе</returns>
        IList<Teacher> GetAll();

        /// <summary>
        /// Добавить учителя в базу
        /// </summary>
        /// <param name="teacher">Новый учитель</param>
        void Add(Teacher teacher);

        /// <summary>
        /// Обновить учителя
        /// </summary>
        /// <param name="teacher">Обновленный учитель</param>
        void Update(Teacher teacher);

        /// <summary>
        /// Удалить учителя
        /// </summary>
        /// <param name="id">ID удаляемого учителя</param>
        void Delete(int id);

        IList<Teacher> SearchTeachers(string SecondName);
    }
}