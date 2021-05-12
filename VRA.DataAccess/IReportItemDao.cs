using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRA.DataAccess.Entities;

namespace VRA.DataAccess
{
    public interface IReportItemDao
    {
        IList<Report> getPerDays(DateTime start, DateTime end);

        IList<Report> getPerMonth(DateTime start, DateTime end);

        IList<Report> getPerYear(DateTime start, DateTime end);
    }
}
