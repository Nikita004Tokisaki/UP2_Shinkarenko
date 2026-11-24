using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project21.Pages
{
    public partial class AddUserPage : Page
    {
        private User _currentUser;

        // Конструктор принимает пользователя (или null для добавления)
        public AddUserPage(User selectedUser)
        {
            InitializeComponent();
            _currentUser = selectedUser ?? new User();
            DataContext = _currentUser;
        }

        // Сохранение (Шаг 8, п.3)
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Login)) errors.AppendLine("Укажите логин!");
            if (string.IsNullOrWhiteSpace(_currentUser.Password)) errors.AppendLine("Укажите пароль!");
            if (string.IsNullOrWhiteSpace(cmbRole.Text)) errors.AppendLine("Выберите роль!");
            else _currentUser.Role = cmbRole.Text;

            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }

            if (_currentUser.ID == 0) Model1.GetContext().User.Add(_currentUser);

            try
            {
                Model1.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}