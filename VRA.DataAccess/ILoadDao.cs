using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface ILoadDao
    {
        Load Get(int id);

        IList<Load> GetAll();

        void Add(Load load);

        void Update(Load load);

        void Delete(int id);
    }
}
