using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using VRA.Dto;

namespace VRA.BusinessLayer
{
    public interface IReportItemProcess
    {
        ObservableCollection<ReportItemDto> GetLoadVisual(string period, DateTime start, DateTime stop);
    }
}
