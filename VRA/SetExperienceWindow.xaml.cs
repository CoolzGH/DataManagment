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
    /// Логика взаимодействия для SetExperienceWindow.xaml
    /// </summary>
    public partial class SetExperienceWindow : Window
    {
        public bool execset;

        public SetExperienceWindow(string status)
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

        private void SetExperience(object sender, RoutedEventArgs e)
        {
                ProcessFactory.GetTeacherProcess().SetExperienceP();
                this.execset = true;
                this.Close();
        }
    }
}