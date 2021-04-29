using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess;
using VRA.Dto;
using VRA.BusinessLayer.Converters;

namespace VRA.BusinessLayer
{
    public class TypeOfClassProcessDb : ITypeOfClassProcess
    {
        private readonly ITypeOfClassDao _typeofclassDao;
        public TypeOfClassProcessDb()
        {
            //Получаем объект для работы с учителями в базе
            _typeofclassDao = DaoFactory.GetTypeOfClassDao();
        }
        public IList<TypeOfClassDto> GetList()
        {
            return DtoConverter.Convert(_typeofclassDao.GetAll());
        }

        public TypeOfClassDto Get(int id)
        {
            return DtoConverter.Convert(_typeofclassDao.Get(id));
        }
        public void Add(TypeOfClassDto typeofclass)
        {
            _typeofclassDao.Add(DtoConverter.Convert(typeofclass));
        }
        public void Update(TypeOfClassDto typeofclass)
        {
            _typeofclassDao.Update(DtoConverter.Convert(typeofclass));
        }
        public void Delete(int id)
        {
            _typeofclassDao.Delete(id);
        }
    }
}
