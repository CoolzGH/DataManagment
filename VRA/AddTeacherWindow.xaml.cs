using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddTeacherWindow.xaml
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        /// <summary>
        /// Поле хранит идентификатор учителя
        /// </summary>
        private int _id;

        public AddTeacherWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int? experience = null;

            if (string.IsNullOrEmpty(tbSecondName.Text))
            {
                MessageBox.Show("Фамилия учителя не должна быть пустой", "Проверка");
                return;
            }

            if (string.IsNullOrEmpty(tbFirstName.Text))
            {
                MessageBox.Show("Имя учителя не должно быть пустым", "Проверка");
                return;
            }


            if (!string.IsNullOrEmpty(tbExperience.Text))
            {
                int intExperience;
                if (!int.TryParse(tbExperience.Text, out intExperience))
                {
                    MessageBox.Show("Опыт должен быть целым числом", "Проверка");
                    return;
                }
                if (intExperience < 0)
                {
                    MessageBox.Show("Опыт не может быть  отрицательным числом", "Проверка");
                    return;
                }
                experience = intExperience;
            }

            TeacherDto teacher = new TeacherDto();

            teacher.SecondName = tbSecondName.Text;
            teacher.FirstName = tbFirstName.Text;
            teacher.MiddleName = tbMiddleName.Text;
            teacher.AcademicDegree = tbAcademicDegree.Text;
            teacher.Position = tbPosition.Text;
            teacher.Experience = experience;

            ITeacherProcess teacherProcess = ProcessFactory.GetTeacherProcess();
            //если это новый объект - сохраняем его
            if (_id == 0)
            {
                //Сохраняем учителя
                teacherProcess.Add(teacher);
            }
            else //иначе обновляем
            {
                //копируем обратно идентификатор объекта
                teacher.TeacherId = _id;
                //обновляем
                teacherProcess.Update(teacher);
            }
            Close();
        }

        /// <summary>
        /// Метод загружает объект учителя для редактирования
        /// </summary>
        /// <param name=«teacher»>редактируемый объект учителя</param>
        public void Load(TeacherDto teacher)
        {
            //если объект не существует, выходим
            if (teacher == null)
                return;
            //сохраняем id учителя
            _id = teacher.TeacherId;
            //заполняем визуальные компоненты для отображения данных
            tbFirstName.Text = teacher.FirstName;
            tbSecondName.Text = teacher.SecondName;
            tbMiddleName.Text = teacher.MiddleName;
            tbAcademicDegree.Text = teacher.AcademicDegree;
            tbPosition.Text = teacher.Position;
            if (teacher.Experience.HasValue)
            {
                tbExperience.Text = teacher.Experience.ToString();
            }
        }

    }
}