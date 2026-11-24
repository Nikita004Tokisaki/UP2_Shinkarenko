using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project21.Pages
{
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            // Стартовые настройки фильтров (Шаг 12, п.2)
            CmbSorting.SelectedIndex = 0;
            CheckDriver.IsChecked = false;
            UpdateUsers();
        }

        // Метод поиска, фильтрации и сортировки (Шаг 12, п.3)
        private void UpdateUsers()
        {
            // Получаем всех пользователей
            var currentUsers = Model1.GetContext().User.ToList();

            // Поиск по ФИО (без учета регистра)
            if (!string.IsNullOrWhiteSpace(TextBoxSearch.Text))
                currentUsers = currentUsers.Where(x => x.FIO != null && x.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            // Фильтр по роли
            if (CheckDriver.IsChecked == true)
                currentUsers = currentUsers.Where(x => x.Role.Contains("Пользователь")).ToList();

            // Сортировка
            if (CmbSorting.SelectedIndex == 0)
                ListUser.ItemsSource = currentUsers.OrderBy(x => x.FIO).ToList();
            else
                ListUser.ItemsSource = currentUsers.OrderByDescending(x => x.FIO).ToList();
        }

        // Обработчики событий (Шаг 12, п.4)
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e) => UpdateUsers();
        private void CmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateUsers();
        private void CheckDriver_Changed(object sender, RoutedEventArgs e) => UpdateUsers();

        // Очистка фильтров (Шаг 12, п.5)
        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearch.Text = string.Empty;
            CmbSorting.SelectedIndex = 0;
            CheckDriver.IsChecked = false;
            UpdateUsers();
        }
    }
}