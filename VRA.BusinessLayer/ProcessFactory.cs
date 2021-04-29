using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    /// <summary>
    /// Фабрика классов бизнес-логики
    /// </summary>
    public class ProcessFactory
    {
        /// <summary>
        /// Возвращает объект, реализующий <seealso cref=«ITeacherProcess»/>
        /// </summary>
        /// <returns></returns>
        public static ITeacherProcess GetTeacherProcess()
        {
            return new TeacherProcessDb();
        }

        public static ISubjectProcess GetSubjectProcess()
        {
            return new SubjectProcessDb();
        }

        public static ITypeOfClassProcess GetTypeOfClassProcess()
        {
            return new TypeOfClassProcessDb();
        }

        public static ILoadProcess GetLoadProcess()
        {
            return new LoadProcessDb();
        }

        public static ISettingsProcess GetSettingsProcess()
        {
            return new SettingsProcess();
        }
    }
}