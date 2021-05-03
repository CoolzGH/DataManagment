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
        private readonly IList<SubjectDto> AllowSubjects = ProcessFactory.GetSubjectProcess().GetList();
        private readonly IList<TypeOfClassDto> AllowTypeOfClasses = ProcessFactory.GetTypeOfClassProcess().GetList();
        private readonly IList<LoadDto> AllowLoads = ProcessFactory.GetLoadProcess().GetList();

        public IList<TeacherDto> FindedTeachers;
        public IList<SubjectDto> FindedSubjects;
        public IList<TypeOfClassDto> FindedTypeOfClasses;
        public IList<LoadDto> FindedLoads;

        private static readonly string[] TypeOfClasses = { "лекция", "практика" };

        public bool exec;

        public SearchWindow(string status)
        {
            InitializeComponent();

            this.cbTypeOfClassName.ItemsSource = TypeOfClasses;
            this.cbTeacherID.ItemsSource = AllowTeachers;
            this.cbSubjectID.ItemsSource = AllowSubjects;
            this.cbTypeOfClassID.ItemsSource = AllowTypeOfClasses;

            switch (status)
            {
                case "teacher":
                    this.SearchTab.SelectedIndex = 0;
                    this.sSubject.Visibility = Visibility.Collapsed;
                    this.sTypeOfClass.Visibility = Visibility.Collapsed;
                    this.sLoad.Visibility = Visibility.Collapsed;
                    break;
                case "subject":
                    this.SearchTab.SelectedIndex = 1;
                    this.sTeacher.Visibility = Visibility.Collapsed;
                    this.sTypeOfClass.Visibility = Visibility.Collapsed;
                    this.sLoad.Visibility = Visibility.Collapsed;
                    break;
                case "typeofclass":
                    this.SearchTab.SelectedIndex = 2;
                    this.sSubject.Visibility = Visibility.Collapsed;
                    this.sTeacher.Visibility = Visibility.Collapsed;
                    this.sLoad.Visibility = Visibility.Collapsed;
                    break;
                case "load":
                    this.SearchTab.SelectedIndex = 3;
                    this.sSubject.Visibility = Visibility.Collapsed;
                    this.sTeacher.Visibility = Visibility.Collapsed;
                    this.sTypeOfClass.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CloseForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchTeachers(object sender, RoutedEventArgs e)
        {
            int intExperience;
            if (int.TryParse(tbExperience.Text, out intExperience) || tbExperience.Text == "")
            {
                this.FindedTeachers = ProcessFactory.GetTeacherProcess().SearchTeacher(this.tbSecondName.Text, this.tbFirstName.Text, this.tbMiddleName.Text, this.tbAcademicDegree.Text, this.tbPosition.Text, this.tbExperience.Text);
                this.exec = true;
                this.Close();
            }
            else 
            {
                MessageBox.Show("Опыт должен быть числом", "Проверка");
            }
        }

        private void SearchSubjects(object sender, RoutedEventArgs e)
        {
            int intSubjectID;
            int intSubjectHours;
            if ((int.TryParse(tbSubjectID.Text, out intSubjectID) || tbSubjectID.Text == "") && (int.TryParse(tbSubjectHours.Text, out intSubjectHours) || tbSubjectHours.Text == ""))
            {
                this.FindedSubjects = ProcessFactory.GetSubjectProcess().SearchSubject(this.tbSubjectID.Text, this.tbTitle.Text, this.tbSubjectHours.Text);
                this.exec = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("ID или Часы должны быть числом", "Проверка");
            }
        }

        private void SearchTypeOfClasses(object sender, RoutedEventArgs e)
        {
            int intTypeOfClassID;
            int intClassHours;
            if ((int.TryParse(tbTypeOfClassID.Text, out intTypeOfClassID) || tbTypeOfClassID.Text == "") && (int.TryParse(tbClassHours.Text, out intClassHours) || tbClassHours.Text == ""))
            {
                this.FindedTypeOfClasses = ProcessFactory.GetTypeOfClassProcess().SearchTypeOfClass(this.tbTypeOfClassID.Text, this.cbTypeOfClassName.Text, this.tbClassHours.Text);
                this.exec = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("ID или Часы должны быть числом", "Проверка");
            }
        }

        private void SearchLoads(object sender, RoutedEventArgs e)
        {
            int intLoadID;
            int intGroupNumber;
            if ((int.TryParse(tbLoadID.Text, out intLoadID) || tbLoadID.Text == "") && (int.TryParse(tbGroupNumber.Text, out intGroupNumber) || tbGroupNumber.Text == ""))
            {
                this.FindedLoads = ProcessFactory.GetLoadProcess().SearchLoad(this.tbLoadID.Text, this.cbTeacherID.Text, this.tbGroupNumber.Text, this.dpLoadDate.Text, this.cbSubjectID.Text, this.cbTypeOfClassID.Text);
                this.exec = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("ID или Номер группы должны быть числом", "Проверка");
            }
        }
    }
}
