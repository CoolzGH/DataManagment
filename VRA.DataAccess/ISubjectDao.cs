using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    /// <summary>
    /// Описание действий с объектом предмета в базе
    /// </summary>
    public interface ISubjectDao
    {
            /// <summary>
            /// Получить предмета по ID
            /// </summary>
            /// <param name="id">ID предмета</param>
            /// <returns>Предмет</returns>
            Subject Get(int id);

            /// <summary>
            /// Получить список всех предметов в базе
            /// </summary>
            /// <returns>Список всех предметов в базе</returns>
            IList<Subject> GetAll();

            /// <summary>
            /// Добавить предмет в базу
            /// </summary>
            /// <param name="subject">Новый предмет</param>
            void Add(Subject subject);

            /// <summary>
            /// Обновить предмет
            /// </summary>
            /// <param name="subject">Обновленный предмет</param>
            void Update(Subject subject);

            /// <summary>
            /// Удалить предмет
            /// </summary>
            /// <param name="id">ID удаляемого предмета</param>
            void Delete(int id);
    }
}
