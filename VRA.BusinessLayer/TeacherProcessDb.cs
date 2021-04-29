using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess;
using VRA.Dto;
using VRA.BusinessLayer.Converters;

namespace VRA.BusinessLayer
{
    public class TeacherProcessDb : ITeacherProcess
    {
        private readonly ITeacherDao _teacherDao;
        public TeacherProcessDb()
        {
            //Получаем объект для работы с учителями в базе
            _teacherDao = DaoFactory.GetTeacherDao();
        }
        public IList<TeacherDto> GetList()
        {
            return DtoConverter.Convert(_teacherDao.GetAll());
        }

        public TeacherDto Get(int id)
        {
            return DtoConverter.Convert(_teacherDao.Get(id));
        }
        public void Add(TeacherDto teacher)
        {
            _teacherDao.Add(DtoConverter.Convert(teacher));
        }
        public void Update(TeacherDto teacher)
        {
            _teacherDao.Update(DtoConverter.Convert(teacher));
        }
        public void Delete(int id)
        {
            _teacherDao.Delete(id);
        }

        public IList<TeacherDto> SearchTeachers(string SecondName)
        {
            return DtoConverter.Convert(_teacherDao.SearchTeachers(SecondName));
        }

    }
}