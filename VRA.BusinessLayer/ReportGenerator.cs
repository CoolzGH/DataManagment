using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRA.Dto;
using System.Windows;
using System.Windows.Forms;

namespace VRA.BusinessLayer
{
    public class ReportGenerator : IReportGenerator
    {
        public void fillExcelTableByType(IEnumerable<object> grid, string status, FileInfo xlsxFile)
        {
            if (grid != null)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage pck = new ExcelPackage(xlsxFile);
                var excel = pck.Workbook.Worksheets.Add(status);
                int x = 1;
                int y = 1;
                // Устанавливает фиксированный десятичный разделитель (нужно для верногораспознавания типа данных Excel'ем).
                CultureInfo cultureInfo = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                // Первая строка (шапка таблицы) – жирным стилем.
                excel.Cells["A1:Z1"].Style.Font.Bold = true;
                // Выравнивание текста в ячейках – по левому краю.
                excel.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                // Устанавливает формат ячеек.
                excel.Cells.Style.Numberformat.Format = "General";

                // Пустой объект для получения списка property.
                Object dtObj = new Object();
                switch (status)
                {
                    case "teacher":
                        dtObj = new TeacherDto();
                        break;
                    case "subject":
                        dtObj = new SubjectDto();
                        break;
                    case "typeofclass":
                        dtObj = new TypeOfClassDto();
                        break;
                    case "load":
                        dtObj = new LoadDto();
                        break;
                }
                // Генерация шапки таблицы.
                foreach (var prop in dtObj.GetType().GetProperties())
                {
                    excel.Cells[y, x].Value = prop.Name.Trim();
                    x++;
                }
                // Генерация строк-записей таблицы.
                foreach (var item in grid)
                {
                    y++;
                    // Объект-контейнер для текущего читаемого элемента.
                    Object itemObj = item;
                    x = 1;
                    foreach (var prop in itemObj.GetType().GetProperties())
                    {
                        object t = prop.GetValue(itemObj, null);
                        object val;
                        if (t == null)
                            val = "";
                        else
                        {
                            val = t.ToString();
                            // Если тип сложный, то вытаскиваем нужное поле.
                            if (t is TeacherDto)
                                val = ((TeacherDto)t).SecondName;
                            if (t is SubjectDto)
                                val = ((SubjectDto)t).Title;
                            if (t is TypeOfClassDto)
                                val = ((TypeOfClassDto)t).TypeOfClassName;
                            if (t is LoadDto)
                                val = ((LoadDto)t).LoadDate;
                        }
                        excel.Cells[y, x].Value = val;
                        x++;
                    }
                }
                // Устанавливаем размер колонок по ширине содержимого.
                excel.Cells.AutoFitColumns();
                // Сохраняем файл.
                pck.Save();
            }
            else MessageBox.Show("Данные не загружены!");
        }
    }
}
