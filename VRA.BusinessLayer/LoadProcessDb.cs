using System;
using System.Collections.Generic;
using System.Text;
using VRA.DataAccess;
using VRA.Dto;
using VRA.BusinessLayer.Converters;

namespace VRA.BusinessLayer
{
    public class LoadProcessDb : ILoadProcess
    {
        private readonly ILoadDao _loadDao;
        public LoadProcessDb()
        {
            _loadDao = DaoFactory.GetLoadDao();
        }
        public IList<LoadDto> GetList()
        {
            return DtoConverter.Convert(_loadDao.GetAll());
        }

        public LoadDto Get(int id)
        {
            return DtoConverter.Convert(_loadDao.Get(id));
        }
        public void Add(LoadDto load)
        {
            _loadDao.Add(DtoConverter.Convert(load));
        }
        public void Update(LoadDto load)
        {
            _loadDao.Update(DtoConverter.Convert(load));
        }
        public void Delete(int id)
        {
            _loadDao.Delete(id);
        }
    }
}
