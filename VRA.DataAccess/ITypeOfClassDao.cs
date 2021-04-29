using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    /// <summary>
    /// Описание действий с объектом типа занятия в базе
    /// </summary>
    public interface ITypeOfClassDao
    {
        /// <summary>
        /// Получить типа занятия по ID
        /// </summary>
        /// <param name="id">ID типа занятия</param>
        /// <returns>Тип занятия</returns>
        TypeOfClass Get(int id);

        /// <summary>
        /// Получить список всех типов занятий в базе
        /// </summary>
        /// <returns>Список всех типов занятий в базе</returns>
        IList<TypeOfClass> GetAll();

        /// <summary>
        /// Добавить тип занятия в базу
        /// </summary>
        /// <param name="typeofclass">Новый тип занятия</param>
        void Add(TypeOfClass typeofclass);

        /// <summary>
        /// Обновить тип занятия
        /// </summary>
        /// <param name="typeofclass">Обновленный тип занятия</param>
        void Update(TypeOfClass typeofclass);

        /// <summary>
        /// Удалить тип занятия
        /// </summary>
        /// <param name="id">ID удаляемого типа занятия</param>
        void Delete(int id);
    }
}
