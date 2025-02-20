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
    public partial class ClassSchedulePage : Page
    {
        private MainWindow _mainWindow;

        public ClassSchedulePage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow; // Сохраняем ссылку на MainWindow
            LoadData();
        }

        private void LoadData()
        {
            // Пример данных для каждого класса
            Class1Grid.ItemsSource = new List<SubjectHours>
            {
                new SubjectHours { Subject = "Математика", HoursPerWeek = 5 },
                new SubjectHours { Subject = "Физика", HoursPerWeek = 3 }
            };

            Class2Grid.ItemsSource = new List<SubjectHours>
            {
                new SubjectHours { Subject = "Литература", HoursPerWeek = 4 },
                new SubjectHours { Subject = "История", HoursPerWeek = 2 }
            };

            // Добавьте данные для остальных классов (3-9) аналогично
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow != null)
            {
                // Скрываем MainFrame и показываем главный контент
                _mainWindow.MainFrame.Visibility = Visibility.Collapsed;
                _mainWindow.MainContent.Visibility = Visibility.Visible;
            }
        }
    }
}
