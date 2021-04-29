using System;
using System.Collections.Generic;
using System.Text;
using VRA.Dto;


namespace VRA.BusinessLayer
{
    public interface ISubjectProcess
    {
        /// <summary>
        /// Возвращает список предметов
        /// </summary>
        /// <returns>список предметов</returns>
        IList<SubjectDto> GetList();
        /// <summary>
        /// Возвращает предмет по его id
        /// </summary>
        /// <param name=«id»>id предмета</param>
        /// <returns>Предмет</returns>
        SubjectDto Get(int id);
        /// <summary>
        /// Добавляет предмет
        /// </summary>
        /// <param name=«subject»></param>
        void Add(SubjectDto subject);
        /// <summary>
        /// Обновляет данные о предмете
        /// </summary>
        /// <param name=«предмет»>Предмет, изменения которого надо сохранить</param>
        void Update(SubjectDto subject);
        /// <summary>
        /// Удаляет предмет
        /// </summary>
        /// <param name=«id»>id предмета, которого надо удалить</param>
        void Delete(int id);
    }
}