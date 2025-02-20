using System.Text;
using System.IO;
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
using SheduleCreator.Pages;

namespace SheduleCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
            MainFrame.Visibility = Visibility.Visible; // Показываем MainFrame
            MainContent.Visibility = Visibility.Collapsed; // Скрываем главный контент
        }

        private void ViewSubjectsTeachers_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ViewSubjectsTeachersPage());
        }

        private void EditSubjects_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new EditSubjectsPage(this));
        }

        private void EditTeachers_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new EditTeachersPage(this));
        }
        private void ClassSchedule_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ClassSchedulePage(this));
        }

        // Обработчик события возврата на главную страницу
        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content == null) // Если контент отсутствует (например, при возврате)
            {
                MainFrame.Visibility = Visibility.Collapsed; // Скрываем MainFrame
                MainContent.Visibility = Visibility.Visible; // Показываем главный контент
            }
        }
    }
}