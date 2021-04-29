using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess;
using VRA.Dto;
using VRA.BusinessLayer.Converters;

namespace VRA.BusinessLayer
{
    public class SubjectProcessDb : ISubjectProcess
    {
        private readonly ISubjectDao _subjectDao;
        public SubjectProcessDb()
        {
            //Получаем объект для работы с учителями в базе
            _subjectDao = DaoFactory.GetSubjectDao();
        }
        public IList<SubjectDto> GetList()
        {
            return DtoConverter.Convert(_subjectDao.GetAll());
        }

        public SubjectDto Get(int id)
        {
            return DtoConverter.Convert(_subjectDao.Get(id));
        }
        public void Add(SubjectDto subject)
        {
            _subjectDao.Add(DtoConverter.Convert(subject));
        }
        public void Update(SubjectDto subject)
        {
            _subjectDao.Update(DtoConverter.Convert(subject));
        }
        public void Delete(int id)
        {
            _subjectDao.Delete(id);
        }
    }
}
