using System;
using System.Collections.Generic;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ITeacherProcess
    {
        /// <summary>
        /// Возвращает список Учителей
        /// </summary>
        /// <returns>список учителей</returns>
        IList<TeacherDto> GetList();
        /// <summary>
        /// Возвращает учителя по его id
        /// </summary>
        /// <param name=«id»>id учителя</param>
        /// <returns>Учитель</returns>
        TeacherDto Get(int id);
        /// <summary>
        /// Добавляет учителя
        /// </summary>
        /// <param name=«teacher»></param>
        void Add(TeacherDto teacher);
        /// <summary>
        /// Обновляет данные о учителе
        /// </summary>
        /// <param name=«teacher»>Учитель, изменения которого надо сохранить</param>
        void Update(TeacherDto teacher);
        /// <summary>
        /// Удаляет учителя
        /// </summary>
        /// <param name=«id»>id учителя, которого надо удалить</param>
        void Delete(int id);

        IList<TeacherDto> SearchTeachers(string SecondName);
    }
}