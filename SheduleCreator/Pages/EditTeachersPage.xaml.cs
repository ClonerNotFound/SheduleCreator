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
using SheduleCreator.Models;

namespace SheduleCreator.Pages
{
    public partial class EditTeachersPage : Page
    {
        private DataService _dataService = new DataService();
        private List<Teacher> _teachers;
        private MainWindow _mainWindow;

        public EditTeachersPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow; // Сохраняем ссылку на MainWindow
            LoadData();
        }

        private void LoadData()
        {
            // Загружаем данные учителей
            _teachers = _dataService.LoadTeachers();
            // Привязываем данные к DataGrid
            TeachersDataGrid.ItemsSource = _teachers;
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            // Добавляем нового учителя
            _teachers.Add(new Teacher { Name = "Новый учитель" });
            // Обновляем DataGrid
            TeachersDataGrid.Items.Refresh();
        }

        private void RemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            // Удаляем выбранного учителя
            if (TeachersDataGrid.SelectedItem is Teacher selectedTeacher)
            {
                _teachers.Remove(selectedTeacher);
                TeachersDataGrid.Items.Refresh();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем данные перед выходом
            _dataService.SaveTeachers(_teachers);

            if (_mainWindow != null)
            {
                // Скрываем MainFrame и показываем главный контент
                _mainWindow.MainFrame.Visibility = Visibility.Collapsed;
                _mainWindow.MainContent.Visibility = Visibility.Visible;
            }
        }
    }
}
