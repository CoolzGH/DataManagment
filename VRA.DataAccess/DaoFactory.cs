using System;
using System.Collections.Generic;
using System.Text;

namespace VRA.DataAccess
{
    public class DaoFactory
    {
        public static ITeacherDao GetTeacherDao()
        {
            return new TeacherDao();
        }

        public static ISubjectDao GetSubjectDao()
        {
            return new SubjectDao();
        }

        public static ITypeOfClassDao GetTypeOfClassDao()
        {
            return new TypeOfClassDao();
        }

        public static SettingsDao GetSettingsDao()
        {
            return new SettingsDao();
        }

        public static LoadDao GetLoadDao()
        {
            return new LoadDao();
        }
    }
}
