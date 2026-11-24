using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project21.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            // Загрузка данных при открытии
            DataGridUser.ItemsSource = Model1.GetContext().User.ToList();
        }

        // Обновление таблицы при возврате (Шаг 8, п.4)
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Model1.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridUser.ItemsSource = Model1.GetContext().User.ToList();
            }
        }

        // Добавление (Шаг 8, п.1)
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(null));
        }

        // Редактирование (Шаг 9, п.2)
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button).DataContext as User;
            NavigationService.Navigate(new AddUserPage(user));
        }

        // Удаление (Шаг 10)
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridUser.SelectedItems.Cast<User>().ToList();
            if (usersForRemoving.Count == 0) { MessageBox.Show("Выберите записи!"); return; }

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Model1.GetContext().User.RemoveRange(usersForRemoving);
                    Model1.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DataGridUser.ItemsSource = Model1.GetContext().User.ToList();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
