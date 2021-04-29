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
using System.Linq;

namespace VRA
{
    /// <summary>
    /// Логика взаимодействия для AddTypeOfClassWindow.xaml
    /// </summary>
    public partial class AddTypeOfClassWindow : Window
    {
        private static readonly string[] TypeOfClasses = { "лекция", "практика" };

        private int _id;
        public AddTypeOfClassWindow()
        {
            InitializeComponent();
            cbTypeOfClass.ItemsSource = TypeOfClasses;
            cbTypeOfClass.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int? classhours = null;

            if (string.IsNullOrEmpty(cbTypeOfClass.Text))
            {
                MessageBox.Show("Тип занятия не должен быть пустым", "Проверка");
                return;
            }

            if (!string.IsNullOrEmpty(tbClassHours.Text))
            {
                int intClassHours;
                if (!int.TryParse(tbClassHours.Text, out intClassHours))
                {
                    MessageBox.Show("Часы должены быть целым числом", "Проверка");
                    return;
                }
                if (intClassHours < 0)
                {
                    MessageBox.Show("Часы не могут быть отрицательным числом", "Проверка");
                    return;
                }
                classhours = intClassHours;
            }

            TypeOfClassDto typeofclass = new TypeOfClassDto();

            typeofclass.TypeOfClassName = cbTypeOfClass.SelectedItem.ToString();
            typeofclass.ClassHours = classhours;

            ITypeOfClassProcess typeofclassProcess = ProcessFactory.GetTypeOfClassProcess();
            //если это новый объект - сохраняем его
            if (_id == 0)
            {
                //Сохраняем тип занятия
                typeofclassProcess.Add(typeofclass);
            }
            else //иначе обновляем
            {
                //копируем обратно идентификатор объекта
                typeofclass.TypeOfClassId = _id;
                //обновляем
                typeofclassProcess.Update(typeofclass);
            }
            Close();
        }

        /// <summary>
        /// Метод загружает объект типа занятия для редактирования
        /// </summary>
        /// <param name=«typeofclass»>редактируемый объект типа занятия</param>
        public void Load(TypeOfClassDto typeofclass)
        {
            //если объект не существует, выходим
            if (typeofclass == null || !TypeOfClasses.Contains(typeofclass.TypeOfClassName))
                return;
            //сохраняем id типа занятия
            _id = typeofclass.TypeOfClassId;
            //заполняем визуальные компоненты для отображения данных
            cbTypeOfClass.SelectedItem = typeofclass.TypeOfClassName;
            if (typeofclass.ClassHours.HasValue)
            {
                tbClassHours.Text = typeofclass.ClassHours.ToString();
            }
        }
    }
}
