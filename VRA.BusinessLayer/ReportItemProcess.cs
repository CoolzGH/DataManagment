using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using VRA.Dto;
using VRA.DataAccess;
using VRA.BusinessLayer.Converters;

namespace VRA.BusinessLayer
{
    public class ReportItemProcess : IReportItemProcess
    {
        private static readonly IReportItemDao reportDao = new ReportDao();

        private static ObservableCollection<ReportItemDto> GetCollection(IList<ReportItemDto> Items, string period, DateTime start, DateTime stop)
        {
            ObservableCollection<ReportItemDto> Collection = new ObservableCollection<ReportItemDto>();
            // Условие проверки наличия принятых данных.
            if (Items == null) { return null; }
            switch (period)
            {
                case "day":
                    {
                        DateTime d = start;
                        while (d <= stop)
                        {
                            ReportItemDto repItem = new ReportItemDto
                            {
                                date = d.Date.ToString("dd.MM.yyyy"),
                                count = 0,

                            };
                            foreach (var item in Items)
                            {
                                if (Convert.ToDateTime(item.date).Date == d)
                                {
                                    repItem.count += item.count;
                                }
                            }
                            Collection.Add(repItem);
                            d = d.AddDays(1);
                        }
                        break;
                    }
                case "month":
                    {

                        DateTime d = start;
                        while (d <= stop)
                        {
                            ReportItemDto repItem = new ReportItemDto
                            {
                                date = d.Date.ToString("Y"),
                                count = 0,
                            };
                            foreach (var item in Items)
                            {
                                if ((Convert.ToDateTime(item.date).Month == d.Month) && (Convert.ToDateTime(item.date).Year == d.Year))
                                {
                                    repItem.count += item.count;
                                }
                            }
                            Collection.Add(repItem);
                            d = d.AddMonths(1);
                        }
                        break;
                    }
                case "year":
                    {
                        DateTime d = start;
                        while (d <= stop)
                        {
                            ReportItemDto repItem = new ReportItemDto
                            {
                                date = d.Date.Year.ToString(),
                                count = 0,
                            };
                            foreach (var item in Items)
                            {
                                if (Convert.ToDateTime(item.date).Year == d.Year)

                                {
                                    repItem.count += item.count;
                                }
                            }
                            Collection.Add(repItem);
                            d = d.AddYears(1);
                        }
                        break;
                    }
            }
            // Возвращаем коллекцию.
            return Collection;
        }

        public ObservableCollection<ReportItemDto> GetLoadVisual(string period, DateTime start, DateTime stop)
        {
            IList<ReportItemDto> ReportList;
            switch (period)
            {
                case  "day":
                    {
                        ReportList = DtoConverter.Convert(reportDao.getPerDays(start, stop)); 
                        break;
                    }
                case "month":
                    {
                        ReportList = DtoConverter.Convert(reportDao.getPerMonth(start, stop));
                        break;
                    }
                case "year":
                    {
                        ReportList = DtoConverter.Convert(reportDao.getPerYear(start, stop));
                        break;
                    }
                default:
                    {
                        ReportList = null;
                        break;
                    }
            }
            return GetCollection(ReportList, period, start, stop);
        }
    }
}
