using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    /// <summary>
    /// Класс - тип занятия
    /// </summary>
    public class TypeOfClassDto
    {
        /// <summary>
        /// Идентификатор типа занятия
        /// </summary>
        public int TypeOfClassId { get; set; }

        /// <summary>
        /// Тип занятия
        /// </summary>
        public string TypeOfClassName { get; set; }

        /// <summary>
        /// Часы занятия
        /// </summary>
        public int? ClassHours { get; set; }
    }
}
