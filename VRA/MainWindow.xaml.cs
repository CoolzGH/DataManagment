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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VRA.Dto;
using VRA.BusinessLayer;

namespace VRA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string status; //Устанавливает, с какой таблицей в текущий момент работает пользователь

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "teacher":
                    var window = new AddTeacherWindow();
                    window.ShowDialog();
                    dgTeachers.ItemsSource = ProcessFactory.GetTeacherProcess().GetList();
                    break;
                case "subject":
                    var subject_window = new AddSubjectWindow();
                    subject_window.ShowDialog();
                    dgSubjects.ItemsSource = ProcessFactory.GetSubjectProcess().GetList();
                    break;
                case "typeofclass":
                    var typeofclass_window = new AddTypeOfClassWindow();
                    typeofclass_window.ShowDialog();
                    dgTypeOfClasses.ItemsSource = ProcessFactory.GetTypeOfClassProcess().GetList();
                    break;
                case "load":
                    var load_window = new AddLoadWindow();
                    load_window.ShowDialog();
                    dgLoads.ItemsSource = ProcessFactory.GetLoadProcess().GetList();
                    break;
                default:
                    MessageBox.Show("Это еще не готово", "Проверка");
                    break;
            }
        }

        private void btnTeachers_Click(object sender, RoutedEventArgs e)
        {
            //Устанавливаем таблицу, с которой работаем в данный момент
            status = "teacher";
            //Помещаем в элемент dgTeachers список всех учителей
            dgTeachers.ItemsSource = ProcessFactory.GetTeacherProcess().GetList();
            //Отображаем в окне элемент dgTeachers
            this.dgTeachers.Visibility = Visibility.Visible;
            //Скрываем все остальные элементы Data Grid
            this.dgSubjects.Visibility = Visibility.Hidden;
            this.dgTypeOfClasses.Visibility = Visibility.Hidden;
            this.dgLoads.Visibility = Visibility.Hidden;
            //Отображаем внизу окна название таблицы
            this.statusLabel.Content = "Работа с таблицей: Учителя";
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "teacher":
                    dgTeachers.ItemsSource = ProcessFactory.GetTeacherProcess().GetList();
                    break;
                case "subject":
                    dgSubjects.ItemsSource = ProcessFactory.GetSubjectProcess().GetList();
                    break;
                case "typeofclass":
                    dgTypeOfClasses.ItemsSource = ProcessFactory.GetTypeOfClassProcess().GetList();
                    break;
                case "load":
                    dgLoads.ItemsSource = ProcessFactory.GetLoadProcess().GetList();
                    break;
                default:
                    MessageBox.Show("Это еще не готово", "Проверка");
                    break;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем, с какой таблицей работаем
            switch (status)
            {
                case "teacher":
                    //Получаем выделенную строку с объектом учителя
                    TeacherDto item = dgTeachers.SelectedItem as TeacherDto;
                    //если там не учитель или пользователь ничего не выбрал сообщаем об этом
                    if (item == null)
                    {
                        MessageBox.Show("Выберите запись для удаления", "Удаление");
                        return;
                    }
                    //Просим подтвердить удаление
                    MessageBoxResult result = MessageBox.Show("Удалить учителя " + item.FirstName + ' ' + item.SecondName + "?", "Удаление учителя", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    //Если пользователь не подтвердил, выходим
                    if (result != MessageBoxResult.Yes)
                        return;
                    //Если все проверки пройдены и подтверждение получено, удаляем учителя
                    ProcessFactory.GetTeacherProcess().Delete(item.TeacherId);
                    //И перезагружаем список учителей
                    dgTeachers.ItemsSource = ProcessFactory.GetTeacherProcess().GetList();
                    break;
                case "subject":
                    //Получаем выделенную строку с объектом предмета
                    SubjectDto item_s = dgSubjects.SelectedItem as SubjectDto;
                    //если там не предмет или пользователь ничего не выбрал сообщаем об этом
                    if (item_s == null)
                    {
                        MessageBox.Show("Выберите запись для удаления", "Удаление");
                        return;
                    }
                    //Просим подтвердить удаление
                    MessageBoxResult result_s = MessageBox.Show("Удалить предмет " + item_s.Title + "?", "Удаление предмета", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    //Если пользователь не подтвердил, выходим
                    if (result_s != MessageBoxResult.Yes)
                        return;
                    //Если все проверки пройдены и подтверждение получено, удаляем предмет
                    ProcessFactory.GetSubjectProcess().Delete(item_s.SubjectId);
                    //И перезагружаем список предметов
                    dgSubjects.ItemsSource = ProcessFactory.GetSubjectProcess().GetList();
                    break;
                case "typeofclass":
                    //Получаем выделенную строку с объектом тип занятия
                    TypeOfClassDto item_t = dgTypeOfClasses.SelectedItem as TypeOfClassDto;
                    //если там не тип занятия или пользователь ничего не выбрал сообщаем об этом
                    if (item_t == null)
                    {
                        MessageBox.Show("Выберите запись для удаления", "Удаление");
                        return;
                    }
                    //Просим подтвердить удаление
                    MessageBoxResult result_t = MessageBox.Show("Удалить тип занятия " + item_t.TypeOfClassName + " " + item_t.ClassHours + "?", "Удаление типа занятия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    //Если пользователь не подтвердил, выходим
                    if (result_t != MessageBoxResult.Yes)
                        return;
                    //Если все проверки пройдены и подтверждение получено, удаляем тип занятия
                    ProcessFactory.GetTypeOfClassProcess().Delete(item_t.TypeOfClassId);
                    //И перезагружаем список предметов
                    dgTypeOfClasses.ItemsSource = ProcessFactory.GetTypeOfClassProcess().GetList();
                    break;
                case "load":
                    //Получаем выделенную строку с объектом тип занятия
                    LoadDto item_l = dgLoads.SelectedItem as LoadDto;
                    //если там не тип занятия или пользователь ничего не выбрал сообщаем об этом
                    if (item_l == null)
                    {
                        MessageBox.Show("Выберите запись для удаления", "Удаление");
                        return;
                    }
                    //Просим подтвердить удаление
                    MessageBoxResult result_l = MessageBox.Show("Удалить нагрузку " + item_l.LoadId + "?", "Удаление нагрузки", MessageBoxButton.YesNo, MessageBoxImage.Warning); ;
                    //Если пользователь не подтвердил, выходим
                    if (result_l != MessageBoxResult.Yes)
                        return;
                    //Если все проверки пройдены и подтверждение получено, удаляем нагрузку
                    ProcessFactory.GetLoadProcess().Delete(item_l.LoadId);
                    //И перезагружаем список нагрузок
                    dgLoads.ItemsSource = ProcessFactory.GetLoadProcess().GetList();
                    break;
                //Даём понять пользователю, что таблица не выбрана
                default:
                    MessageBox.Show("Это еще не готово", "Проверка");
                    break;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            switch (status)
            {
                case "teacher":
                    //Получаем выделенную строку с объектом учителя
                    TeacherDto item = dgTeachers.SelectedItem as TeacherDto;
                    //если там не учитель или пользователь ничего не выбрал сообщаем об этом
                    if (item == null)
                    {
                        MessageBox.Show("Выберите запись для редактирования", "Редактирование");
                        return;
                    }
                    //Создаем окно
                    AddTeacherWindow window = new AddTeacherWindow();
                    //Передаем объект на редактирование
                    window.Load(item);
                    //Отображаем окно с данными
                    window.ShowDialog();
                    //Перезагружаем список объектов
                    dgTeachers.ItemsSource = ProcessFactory.GetTeacherProcess().GetList();
                    break;

                case "subject":
                    //Получаем выделенную строку с объектом предмета
                    SubjectDto item_s = dgSubjects.SelectedItem as SubjectDto;
                    //если там не предмет или пользователь ничего не выбрал сообщаем об этом
                    if (item_s == null)
                    {
                        MessageBox.Show("Выберите запись для редактирования", "Редактирование");
                        return;
                    }
                    //Создаем окно
                    AddSubjectWindow window_s = new AddSubjectWindow();
                    //Передаем объект на редактирование
                    window_s.Load(item_s);
                    //Отображаем окно с данными
                    window_s.ShowDialog();
                    //Перезагружаем список объектов
                    dgSubjects.ItemsSource = ProcessFactory.GetSubjectProcess().GetList();
                    break;

                case "typeofclass":
                    //Получаем выделенную строку с объектом типа занятия
                    TypeOfClassDto item_t = dgTypeOfClasses.SelectedItem as TypeOfClassDto;
                    //если там не тип занятия или пользователь ничего не выбрал сообщаем об этом
                    if (item_t == null)
                    {
                        MessageBox.Show("Выберите запись для редактирования", "Редактирование");
                        return;
                    }
                    //Создаем окно
                    AddTypeOfClassWindow window_t = new AddTypeOfClassWindow();
                    //Передаем объект на редактирование
                    window_t.Load(item_t);
                    //Отображаем окно с данными
                    window_t.ShowDialog();
                    //Перезагружаем список объектов
                    dgTypeOfClasses.ItemsSource = ProcessFactory.GetTypeOfClassProcess().GetList();
                    break;

                case "load":
                    //Получаем выделенную строку с объектом 
                    LoadDto item_l = dgLoads.SelectedItem as LoadDto;
                    //если там не нагрузка или пользователь ничего не выбрал сообщаем об этом
                    if (item_l == null)
                    {
                        MessageBox.Show("Выберите запись для редактирования", "Редактирование");
                        return;
                    }
                    //Создаем окно
                    AddLoadWindow window_l = new AddLoadWindow();
                    //Передаем объект на редактирование
                    window_l.Load(item_l);
                    //Отображаем окно с данными
                    window_l.ShowDialog();
                    //Перезагружаем список объектов
                    dgLoads.ItemsSource = ProcessFactory.GetLoadProcess().GetList();
                    break;

                //Даём понять пользователю, что таблица не выбрана
                default:
                    MessageBox.Show("Это еще не готово", "Проверка");
                    break;
            }
        }

        private void btnSubjects_Click(object sender, RoutedEventArgs e)
        {
            //Устанавливаем таблицу, с которой работаем в данный момент
            status = "subject";
            //Помещаем в элемент dgSubjects список всех предметов
            dgSubjects.ItemsSource = ProcessFactory.GetSubjectProcess().GetList();
            //Отображаем в окне элемент dgSubjects
            this.dgSubjects.Visibility = Visibility.Visible;
            //Скрываем все остальные элементы Data Grid
            this.dgTeachers.Visibility = Visibility.Hidden;
            this.dgTypeOfClasses.Visibility = Visibility.Hidden;
            this.dgLoads.Visibility = Visibility.Hidden;
            //Отображаем внизу окна название таблицы
            this.statusLabel.Content = "Работа с таблицей: Предметы";
        }

        private void btnTypeOfClasses_Click(object sender, RoutedEventArgs e)
        {
            //Устанавливаем таблицу, с которой работаем в данный момент
            status = "typeofclass";
            //Помещаем в элемент dgTypeOfClasses список всех типов занятий
            dgTypeOfClasses.ItemsSource = ProcessFactory.GetTypeOfClassProcess().GetList();
            //Отображаем в окне элемент dgTypeOfClasses
            this.dgTypeOfClasses.Visibility = Visibility.Visible;
            //Скрываем все остальные элементы Data Grid
            this.dgTeachers.Visibility = Visibility.Hidden;
            this.dgSubjects.Visibility = Visibility.Hidden;
            this.dgLoads.Visibility = Visibility.Hidden;
            //Отображаем внизу окна название таблицы
            this.statusLabel.Content = "Работа с таблицей: Типы занятий";
        }

        private void btnDatabase_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }

        private void btnLoads_Click(object sender, RoutedEventArgs e)
        {
            status = "load";
            dgLoads.ItemsSource = ProcessFactory.GetLoadProcess().GetList();
            this.dgLoads.Visibility = Visibility.Visible;
            this.dgTeachers.Visibility = Visibility.Hidden;
            this.dgSubjects.Visibility = Visibility.Hidden;
            this.dgTypeOfClasses.Visibility = Visibility.Hidden;
            this.statusLabel.Content = "Работа с таблицей: Нагрузка";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow search = new SearchWindow(status);
            {
                switch (status)
                {
                    case "teacher":
                        search.ShowDialog();
                        if (search.exec)
                        {
                            this.dgTeachers.ItemsSource = search.FindedTeachers;
                        }
                        break;
                    case "subject":
                        search.ShowDialog();
                        if (search.exec)
                        {
                            this.dgSubjects.ItemsSource = search.FindedSubjects;
                        }
                        break;
                    case "typeofclass":
                        search.ShowDialog();
                        if (search.exec)
                        {
                            this.dgTypeOfClasses.ItemsSource = search.FindedTypeOfClasses;
                        }
                        break;
                    case "load":
                        search.ShowDialog();
                        if (search.exec)
                        {
                            this.dgLoads.ItemsSource = search.FindedLoads;
                        }
                        break;
                    default: MessageBox.Show("Для поиска необходимо выбрать таблицу!"); break;
                }
            }
        }

        private void btnSetExperience_Click(object sender, RoutedEventArgs e)
        {
            SetExperienceWindow setExperience = new SetExperienceWindow(status);
            switch (status)
            {
                case "teacher":
                    setExperience.ShowDialog();
                    break;
                default: MessageBox.Show("Только для таблицы Учителя!"); break;
            }

        }

    }
}