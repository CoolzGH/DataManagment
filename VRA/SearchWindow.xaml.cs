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
using VRA.Dto;
using VRA.BusinessLayer;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly IList<TeacherDto> AllowTeachers = ProcessFactory.GetTeacherProcess().GetList();

        public IList<TeacherDto> FindedTeachers;

        public bool exec;

        public SearchWindow(string status)
        {
            InitializeComponent();
            switch (status)
            {
                case "teacher":
                    break;
            }
        }

        private void CloseForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchTeachers(object sender, RoutedEventArgs e)
        {
            this.FindedTeachers = ProcessFactory.GetTeacherProcess().SearchTeachers(this.tbSecondName.Text);
            this.exec = true;
            this.Close();
        }
    }
}
