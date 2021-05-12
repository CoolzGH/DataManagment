using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;
using VRA.Dto;
using VRA.BusinessLayer;
using System.Collections.ObjectModel;


namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private ObservableCollection<ReportItemDto> collection = new ObservableCollection<ReportItemDto>();
        private readonly List<decimal> axisYDataLoads = new List<decimal>();
        private readonly List<string> axisXData = new List<string>();

        public ReportWindow()
        {
            InitializeComponent();
            // Т.к. все графики находятся в пределах области построения, создадим ее.
            chart.ChartAreas.Add(new ChartArea("Default"));
            // Добавим график, и назначим его в ранее созданную область «Default».
            chart.Series.Add(new Series("Нагрузки"));
            chart.Series["Нагрузки"].ChartArea = "Default";
            // Определяем легенду.
            chart.Legends.Add(new Legend("Legend"));
            chart.Legends["Legend"].DockedToChartArea = "Default";
            chart.Series["Нагрузки"].Legend = "Legend";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IList<LoadDto> load = ProcessFactory.GetLoadProcess().GetList();
            datePicker1.Text = load[0].LoadDate.ToString();
            datePicker2.Text = load[load.Count - 1].LoadDate.ToString();
            btn_accept_Click(sender, e);
        }

        private void btn_accept_Click(object sender, RoutedEventArgs e)
        {
            DateCompare();
            FillCollection();
            GraphType();
            DrawGraph();
        }

        private void DateCompare()
        {
            if ((Convert.ToDateTime(datePicker1.Text)) >= Convert.ToDateTime(datePicker2.Text))
            {
                MessageBox.Show("Дата окончания интервала запроса \n меньше либо равна дате начала");
            }
        }

        private void FillCollection()
        {
            axisYDataLoads.Clear();

            if (radioDay.IsChecked != null && radioDay.IsChecked.Value)
            {
                TimeSpan ts = (Convert.ToDateTime(datePicker2.Text)).Subtract(Convert.ToDateTime(datePicker1.Text));
                if (ts.Days > 30)
                {
                    MessageBox.Show("Выбранный Вами период времени слишком велик! \n Максимальная длина периода - 30 дней");
                    datePicker2.Text = Convert.ToDateTime(datePicker1.Text).Date.AddDays(30).ToString();
                }
                collection.Clear();
                collection = ProcessFactory.GetReportProcess().GetLoadVisual("day", Convert.ToDateTime(datePicker1.Text),
                Convert.ToDateTime(datePicker2.Text));
            }
            if (radioMounth.IsChecked != null && radioMounth.IsChecked.Value)
            {
                TimeSpan ts = (Convert.ToDateTime(datePicker2.Text)).Subtract(Convert.ToDateTime(datePicker1.Text));
                if (ts.Days / 30 > 12)
                {
                    MessageBox.Show("Выбранный Вами период времени слишком велик! \n Максимальная длина периода - 12 месяцев ");
                    datePicker2.Text = Convert.ToDateTime(datePicker1.Text).Date.AddMonths(12).ToString();
                }
                collection.Clear();
                collection = ProcessFactory.GetReportProcess().GetLoadVisual("month", Convert.ToDateTime(datePicker1.Text),
                Convert.ToDateTime(datePicker2.Text));
            }
            if (radioYear.IsChecked != null && radioYear.IsChecked.Value)
            {
                TimeSpan ts = (Convert.ToDateTime(datePicker2.Text)).Subtract(Convert.ToDateTime(datePicker1.Text));
                if (ts.Days / (30 * 12) > 10)
                {
                    MessageBox.Show("Выбранный Вами период времени слишком велик! \n Максимальная длина периода - 10 лет ");
                    datePicker2.Text = Convert.ToDateTime(datePicker1.Text).Date.AddYears(10).ToString();
                }
                collection.Clear();
                collection = ProcessFactory.GetReportProcess().GetLoadVisual("year", Convert.ToDateTime(datePicker1.Text),
                Convert.ToDateTime(datePicker2.Text));
            }


            foreach (var item in collection)
            {
                axisYDataLoads.Add(item.count);
            }
        }

        private void GraphType()
        {
            if (radioGist.IsChecked != null && radioGist.IsChecked.Value)
            {
                // Определяем вид графиков.
                chart.Series["Нагрузки"].ChartType = SeriesChartType.Column;
            }
            if (radioSpline.IsChecked != null && radioSpline.IsChecked.Value)
            {
                // Определяем вид графиков.
                chart.Series["Нагрузки"].ChartType = SeriesChartType.Line;
            }
        }

        private void DrawGraph()
        {
            // Очищаем старые данные.
            axisXData.Clear();
            chart.Series["Нагрузки"].Points.Clear();
            // Добавляем подписи по оси X.
            foreach (var item in collection)
            {
                axisXData.Add(item.date);
            }
            // Настраиваем легенду.
            if (axisYDataLoads.Count != 0)
            {
                chart.Series["Нагрузки"].IsVisibleInLegend = true;
            }
            else
            {
                chart.Series["Нагрузки"].IsVisibleInLegend = false;
            }
            // Строим графики.
            if (axisYDataLoads.Count != 0) chart.Series["Нагрузки"].Points.DataBindXY(axisXData, axisYDataLoads);
        }

    }
}
