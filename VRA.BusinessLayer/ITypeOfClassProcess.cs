using System;
using System.Collections.Generic;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ITypeOfClassProcess
    {
        /// <summary>
        /// Возвращает список типов занятий
        /// </summary>
        /// <returns>список типов занятий</returns>
        IList<TypeOfClassDto> GetList();
        /// <summary>
        /// Возвращает тип занятия по его id
        /// </summary>
        /// <param name=«id»>id типа занятия</param>
        /// <returns>Тип занятия</returns>
        TypeOfClassDto Get(int id);
        /// <summary>
        /// Добавляет тип занятия
        /// </summary>
        /// <param name=«typeofclass»></param>
        void Add(TypeOfClassDto typeofclass);
        /// <summary>
        /// Обновляет данные о типе занятия
        /// </summary>
        /// <param name=«тип занятия»>Тип занятия, изменения которого надо сохранить</param>
        void Update(TypeOfClassDto typeofclass);
        /// <summary>
        /// Удаляет тип занятия
        /// </summary>
        /// <param name=«id»>id предмета, которого надо удалить</param>
        void Delete(int id);
    }
}