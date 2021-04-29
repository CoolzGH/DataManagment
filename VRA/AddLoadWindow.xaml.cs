using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AddLoadWindow.xaml
    /// </summary>
    public partial class AddLoadWindow : Window
    {
        /// <summary>
        /// Поле хранит идентификатор 
        /// </summary>
        private int _id;

        private readonly IList<TeacherDto> Teachers = ProcessFactory.GetTeacherProcess().GetList();
        private readonly IList<SubjectDto> Subjects = ProcessFactory.GetSubjectProcess().GetList();
        private readonly IList<TypeOfClassDto> TypeOfClasses = ProcessFactory.GetTypeOfClassProcess().GetList();

        public AddLoadWindow()
        {
            InitializeComponent();
            cbTeacher.ItemsSource = Teachers;
            cbTeacher.SelectedIndex = 0;
            cbSubject.ItemsSource = (from A in Subjects orderby A.Title select A);
            cbTypeOfClass.ItemsSource = (from A in TypeOfClasses orderby A.TypeOfClassId select A);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int groupnumber = 0;

            if (string.IsNullOrEmpty(tbGroupNumber.Text))
            {
                MessageBox.Show("Номер группы не должен быть пустым", "Проверка");
                return;
            }

            if (cbTeacher.SelectedItem == null)
            {
                MessageBox.Show("Выберите учителя", "Проверка");
                return;
            }

            if (cbSubject.SelectedItem == null)
            {
                MessageBox.Show("Выберите предмет", "Проверка");
                return;
            }

            if (cbTypeOfClass.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип занятия", "Проверка");
                return;
            }


            if (!string.IsNullOrEmpty(tbGroupNumber.Text))
            {
                int intGroupNumber;
                if (!int.TryParse(tbGroupNumber.Text, out intGroupNumber))
                {
                    MessageBox.Show("Номер группы должен быть целым числом", "Проверка");
                    return;
                }
                if (intGroupNumber < 0)
                {
                    MessageBox.Show("Номер группы не может быть  отрицательным числом", "Проверка");
                    return;
                }
                groupnumber = intGroupNumber;
            }

            LoadDto load = new LoadDto();

            load.Teacher = cbTeacher.SelectedItem as TeacherDto;
            load.GroupNumber = groupnumber;
            if (string.IsNullOrEmpty(dpLoadDate.Text))
            {
                load.LoadDate = null;
            }
            else
            {
                load.LoadDate = dpLoadDate.SelectedDate.Value.Date;
            }
            load.Subject = cbSubject.SelectedItem as SubjectDto;
            load.TypeOfClass = cbTypeOfClass.SelectedItem as TypeOfClassDto;

            ILoadProcess loadProcess = ProcessFactory.GetLoadProcess();
            //если это новый объект - сохраняем его
            if (_id == 0)
            {
                //Сохраняем учителя
                loadProcess.Add(load);
            }
            else //иначе обновляем
            {
                //копируем обратно идентификатор объекта
                load.LoadId = _id;
                //обновляем
                loadProcess.Update(load);
            }
            Close();
        }


        public void Load(LoadDto load)
        {
            if (load == null)
                return;
            //сохраняем id 
            _id = load.LoadId;
            //заполняем визуальные компоненты для отображения данных
            cbTeacher.SelectedItem = load.Teacher;
            tbGroupNumber.Text = load.GroupNumber.ToString();
            dpLoadDate.Text = load.LoadDate.ToString();
            cbSubject.SelectedItem = load.Subject;
            cbTypeOfClass.SelectedItem = load.TypeOfClass;

            foreach (TeacherDto t in Teachers)
            {
                if (load.Teacher.TeacherId == t.TeacherId)
                {
                    cbTeacher.SelectedItem = t;
                    break;
                }
            }

            foreach (SubjectDto s in Subjects)
            {
                if (load.Subject.SubjectId == s.SubjectId)
                {
                    cbSubject.SelectedItem = s;
                    break;
                }
            }

            foreach (TypeOfClassDto toc in TypeOfClasses)
            {
                if (load.TypeOfClass.TypeOfClassId == toc.TypeOfClassId)
                {
                    cbTypeOfClass.SelectedItem = toc;
                    break;
                }
            }
        }
    }
}
