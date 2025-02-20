using System.IO;
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
using Microsoft.Win32;
using Newtonsoft.Json;
using SheduleCreator.Models;

namespace SheduleCreator.Pages
{
    public partial class ViewSubjectsTeachersPage : Page
    {
        private DataService _dataService = new DataService();

        public ViewSubjectsTeachersPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<Subject> subjects = _dataService.LoadSubjects();
            SubjectsDataGrid.ItemsSource = subjects;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                // Скрываем MainFrame и показываем главный контент
                mainWindow.MainFrame.Visibility = Visibility.Collapsed;
                mainWindow.MainContent.Visibility = Visibility.Visible;
            }
        }

        // Экспорт данных в JSON
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Сохранить данные в JSON"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    var data = new DataModel
                    {
                        Subjects = _dataService.LoadSubjects(),
                        Teachers = _dataService.LoadTeachers()
                    };

                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                    MessageBox.Show("Данные успешно экспортированы!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте данных: {ex.Message}");
                }
            }
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Выберите файл для импорта"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string json = File.ReadAllText(filePath);
                    var data = JsonConvert.DeserializeObject<DataModel>(json);

                    if (data != null)
                    {
                        _dataService.SaveSubjects(data.Subjects);
                        _dataService.SaveTeachers(data.Teachers);
                        MessageBox.Show("Данные успешно импортированы!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте данных: {ex.Message}");
                }
            }

            LoadData();
        }
    }

    // Модель данных для импорта/экспорта
    public class DataModel
    {
        public List<Subject> Subjects { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
