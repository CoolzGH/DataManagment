using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface ILoadProcess
    {
        IList<LoadDto> GetList();
        LoadDto Get(int id);
        void Add(LoadDto load);
        void Update(LoadDto load);
        void Delete(int id);
    }
}
