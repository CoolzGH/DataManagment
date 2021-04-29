using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    /// <summary>
    /// Класс - учитель
    /// </summary>

    public class TeacherDto
    {
        /// <summary>
        /// id учителя
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Учёная степень
        /// </summary>
        public string AcademicDegree { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Опыт
        /// </summary>
        public int? Experience { get; set; }
    }
}