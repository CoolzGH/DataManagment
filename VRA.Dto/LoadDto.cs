using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    public class LoadDto
    {
        /// <summary>
        /// Идентификатор нагрузки
        /// </summary>
        public int LoadId { get; set; }

        /// <summary>
        /// Учитель
        /// </summary>
        public TeacherDto Teacher { get; set; }

        /// <summary>
        /// Номер группы
        /// </summary>
        public int GroupNumber { get; set; }

        /// <summary>
        /// Дата нагрузки
        /// </summary>
        public DateTime? LoadDate { get; set; }

        /// <summary>
        /// Предмет
        /// </summary>
        public SubjectDto Subject { get; set; }

        /// <summary>
        /// Тип занятия
        /// </summary>
        public TypeOfClassDto TypeOfClass { get; set; }
    }
}
