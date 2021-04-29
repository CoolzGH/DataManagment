using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRA.Dto
{
    /// <summary>
    /// Класс - предмет
    /// </summary>
    public class SubjectDto
    {
        /// <summary>
        /// Идентификатор предмета
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// Название предмета
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Часы предмета
        /// </summary>
        public int? SubjectHours { get; set; }
    }
}
