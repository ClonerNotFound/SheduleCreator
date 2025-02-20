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
    public partial class EditSubjectsPage : Page
    {
        private DataService _dataService = new DataService();
        private List<Subject> _subjects;
        private MainWindow _mainWindow;

        public EditSubjectsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow; // Сохраняем ссылку на MainWindow
            LoadData();
        }

        private void LoadData()
        {
            _subjects = _dataService.LoadSubjects();
            SubjectsDataGrid.ItemsSource = _subjects;
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            _subjects.Add(new Subject { Name = "Новый предмет" });
            SubjectsDataGrid.Items.Refresh();
        }

        private void RemoveSubject_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsDataGrid.SelectedItem is Subject selectedSubject)
            {
                _subjects.Remove(selectedSubject);
                SubjectsDataGrid.Items.Refresh();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Сохраняем данные перед выходом
            _dataService.SaveSubjects(_subjects);

            if (_mainWindow != null)
            {
                // Скрываем MainFrame и показываем главный контент
                _mainWindow.MainFrame.Visibility = Visibility.Collapsed;
                _mainWindow.MainContent.Visibility = Visibility.Visible;
            }
        }
    }
}
