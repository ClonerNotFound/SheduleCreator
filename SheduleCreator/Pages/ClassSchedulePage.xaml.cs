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
using Newtonsoft.Json;
using SheduleCreator.Models;
using Microsoft.Win32;

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
            if (File.Exists("schedule.json"))
            {
                try
                {
                    // Загружаем данные из файла
                    string json = File.ReadAllText("schedule.json");
                    var data = JsonConvert.DeserializeObject<Dictionary<string, List<SubjectHours>>>(json);

                    if (data != null)
                    {
                        Class1Grid.ItemsSource = data.ContainsKey("Class1") ? data["Class1"] : new List<SubjectHours>();
                        Class2Grid.ItemsSource = data.ContainsKey("Class2") ? data["Class2"] : new List<SubjectHours>();
                        Class3Grid.ItemsSource = data.ContainsKey("Class3") ? data["Class3"] : new List<SubjectHours>();
                        Class4Grid.ItemsSource = data.ContainsKey("Class4") ? data["Class4"] : new List<SubjectHours>();
                        Class5Grid.ItemsSource = data.ContainsKey("Class5") ? data["Class5"] : new List<SubjectHours>();
                        Class6Grid.ItemsSource = data.ContainsKey("Class6") ? data["Class6"] : new List<SubjectHours>();
                        Class7Grid.ItemsSource = data.ContainsKey("Class7") ? data["Class7"] : new List<SubjectHours>();
                        Class8Grid.ItemsSource = data.ContainsKey("Class8") ? data["Class8"] : new List<SubjectHours>();
                        Class9Grid.ItemsSource = data.ContainsKey("Class9") ? data["Class9"] : new List<SubjectHours>();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                }
            }
            else
            {
                // Если файл не существует, инициализируем пустые списки
                Class1Grid.ItemsSource = new List<SubjectHours>();
                Class2Grid.ItemsSource = new List<SubjectHours>();
                Class3Grid.ItemsSource = new List<SubjectHours>();
                Class4Grid.ItemsSource = new List<SubjectHours>();
                Class5Grid.ItemsSource = new List<SubjectHours>();
                Class6Grid.ItemsSource = new List<SubjectHours>();
                Class7Grid.ItemsSource = new List<SubjectHours>();
                Class8Grid.ItemsSource = new List<SubjectHours>();
                Class9Grid.ItemsSource = new List<SubjectHours>();
            }
        }

        private List<SubjectHours> LoadClassData(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<SubjectHours>>(json);
            }
            return new List<SubjectHours>(); // Возвращаем пустой список
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
                    // Загружаем данные из выбранного файла
                    string json = File.ReadAllText(filePath);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, List<SubjectHours>>>(json);

                    if (data != null)
                    {
                        Class1Grid.ItemsSource = data.ContainsKey("Class1") ? data["Class1"] : new List<SubjectHours>();
                        Class2Grid.ItemsSource = data.ContainsKey("Class2") ? data["Class2"] : new List<SubjectHours>();
                        Class3Grid.ItemsSource = data.ContainsKey("Class3") ? data["Class3"] : new List<SubjectHours>();
                        Class4Grid.ItemsSource = data.ContainsKey("Class4") ? data["Class4"] : new List<SubjectHours>();
                        Class5Grid.ItemsSource = data.ContainsKey("Class5") ? data["Class5"] : new List<SubjectHours>();
                        Class6Grid.ItemsSource = data.ContainsKey("Class6") ? data["Class6"] : new List<SubjectHours>();
                        Class7Grid.ItemsSource = data.ContainsKey("Class7") ? data["Class7"] : new List<SubjectHours>();
                        Class8Grid.ItemsSource = data.ContainsKey("Class8") ? data["Class8"] : new List<SubjectHours>();
                        Class9Grid.ItemsSource = data.ContainsKey("Class9") ? data["Class9"] : new List<SubjectHours>();

                        MessageBox.Show("Данные успешно импортированы!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте данных: {ex.Message}");
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow != null)
            {
                _mainWindow.MainFrame.Visibility = Visibility.Collapsed;
                _mainWindow.MainContent.Visibility = Visibility.Visible;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем словарь для хранения данных всех классов
                var data = new Dictionary<string, List<SubjectHours>>
        {
            { "Class1", (List<SubjectHours>)Class1Grid.ItemsSource },
            { "Class2", (List<SubjectHours>)Class2Grid.ItemsSource },
            { "Class3", (List<SubjectHours>)Class3Grid.ItemsSource },
            { "Class4", (List<SubjectHours>)Class4Grid.ItemsSource },
            { "Class5", (List<SubjectHours>)Class5Grid.ItemsSource },
            { "Class6", (List<SubjectHours>)Class6Grid.ItemsSource },
            { "Class7", (List<SubjectHours>)Class7Grid.ItemsSource },
            { "Class8", (List<SubjectHours>)Class8Grid.ItemsSource },
            { "Class9", (List<SubjectHours>)Class9Grid.ItemsSource }
        };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText("schedule.json", json);

                MessageBox.Show("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }
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
                    // Создаем словарь для хранения данных всех классов
                    var data = new Dictionary<string, List<SubjectHours>>
            {
                { "Class1", (List<SubjectHours>)Class1Grid.ItemsSource },
                { "Class2", (List<SubjectHours>)Class2Grid.ItemsSource },
                { "Class3", (List<SubjectHours>)Class3Grid.ItemsSource },
                { "Class4", (List<SubjectHours>)Class4Grid.ItemsSource },
                { "Class5", (List<SubjectHours>)Class5Grid.ItemsSource },
                { "Class6", (List<SubjectHours>)Class6Grid.ItemsSource },
                { "Class7", (List<SubjectHours>)Class7Grid.ItemsSource },
                { "Class8", (List<SubjectHours>)Class8Grid.ItemsSource },
                { "Class9", (List<SubjectHours>)Class9Grid.ItemsSource }
            };

                    // Сохраняем данные в выбранный файл
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
        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Class1Grid.ItemsSource = new List<SubjectHours>();
            Class2Grid.ItemsSource = new List<SubjectHours>();
            Class3Grid.ItemsSource = new List<SubjectHours>();
            Class4Grid.ItemsSource = new List<SubjectHours>();
            Class5Grid.ItemsSource = new List<SubjectHours>();
            Class6Grid.ItemsSource = new List<SubjectHours>();
            Class7Grid.ItemsSource = new List<SubjectHours>();
            Class8Grid.ItemsSource = new List<SubjectHours>();
            Class9Grid.ItemsSource = new List<SubjectHours>();

            MessageBox.Show("Все данные очищены!");
        }
        private void AddDefaultRow_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            var button = sender as Button;
            if (button == null) return;

            // Получаем имя таблицы из свойства Tag
            string tableName = button.Tag as string;
            if (string.IsNullOrEmpty(tableName)) return;

            // Получаем DataGrid, связанный с этой кнопкой
            DataGrid dataGrid = null;
            switch (tableName)
            {
                case "Class1":
                    dataGrid = Class1Grid;
                    break;
                case "Class2":
                    dataGrid = Class2Grid;
                    break;
                case "Class3":
                    dataGrid = Class3Grid;
                    break;
                case "Class4":
                    dataGrid = Class4Grid;
                    break;
                case "Class5":
                    dataGrid = Class5Grid;
                    break;
                case "Class6":
                    dataGrid = Class6Grid;
                    break;
                case "Class7":
                    dataGrid = Class7Grid;
                    break;
                case "Class8":
                    dataGrid = Class8Grid;
                    break;
                case "Class9":
                    dataGrid = Class9Grid;
                    break;
            }

            if (dataGrid == null) return;

            // Получаем коллекцию, связанную с DataGrid
            var itemsSource = dataGrid.ItemsSource as IList<SubjectHours>;
            if (itemsSource == null) return;

            // Добавляем строку с данными по умолчанию
            itemsSource.Add(new SubjectHours
            {
                Subject = "Предмет",
                HoursPerWeek = 1,
                Teacher = "Преподаватель",
                Difficulty = ""
            });

            // Обновляем DataGrid
            dataGrid.Items.Refresh();
        }
        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, на которую нажали
            var button = sender as Button;
            if (button == null) return;

            // Получаем элемент данных, связанный с этой строкой
            var item = button.DataContext as SubjectHours;
            if (item == null) return;

            // Получаем DataGrid, к которому принадлежит кнопка
            var dataGrid = FindParent<DataGrid>(button);
            if (dataGrid == null) return;

            // Получаем коллекцию, связанную с DataGrid
            var itemsSource = dataGrid.ItemsSource as IList<SubjectHours>;
            if (itemsSource != null)
            {
                // Удаляем элемент из коллекции
                itemsSource.Remove(item);

                // Обновляем DataGrid
                dataGrid.Items.Refresh();
            }
        }

        // Вспомогательный метод для поиска родительского элемента
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }
    }
}
