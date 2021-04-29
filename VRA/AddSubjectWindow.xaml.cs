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
    /// Логика взаимодействия для AddSubjectWindow.xaml
    /// </summary>
    public partial class AddSubjectWindow : Window
    {
        /// <summary>
        /// Поле хранит идентификатор предмета
        /// </summary>
        private int _id;
        public AddSubjectWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int? subjecthours = null;

            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("Название предмета не должно быть пустым", "Проверка");
                return;
            }

            if (!string.IsNullOrEmpty(tbSubjectHours.Text))
            {
                int intSubjectHours;
                if (!int.TryParse(tbSubjectHours.Text, out intSubjectHours))
                {
                    MessageBox.Show("Часы должены быть целым числом", "Проверка");
                    return;
                }
                if (intSubjectHours < 0)
                {
                    MessageBox.Show("Часы не могут быть отрицательным числом", "Проверка");
                    return;
                }
                subjecthours = intSubjectHours;
            }

            SubjectDto subject = new SubjectDto();

            subject.Title = tbTitle.Text;
            subject.SubjectHours = subjecthours;

            ISubjectProcess subjectProcess = ProcessFactory.GetSubjectProcess();
            //если это новый объект - сохраняем его
            if (_id == 0)
            {
                //Сохраняем предмет
                subjectProcess.Add(subject);
            }
            else //иначе обновляем
            {
                //копируем обратно идентификатор объекта
                subject.SubjectId = _id;
                //обновляем
                subjectProcess.Update(subject);
            }
            Close();
        }

        /// <summary>
        /// Метод загружает объект предмета для редактирования
        /// </summary>
        /// <param name=«subject»>редактируемый объект предмета</param>
        public void Load(SubjectDto subject)
        {
            //если объект не существует, выходим
            if (subject == null)
                return;
            //сохраняем id предмета
            _id = subject.SubjectId;
            //заполняем визуальные компоненты для отображения данных
            tbTitle.Text = subject.Title;
            if (subject.SubjectHours.HasValue)
            {
                tbSubjectHours.Text = subject.SubjectHours.ToString();
            }
        }
    }
}
