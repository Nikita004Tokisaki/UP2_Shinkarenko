using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Project21.Pages
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        // Обработчик регистрации (Шаг 5)
        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            // 5.1. Проверка заполнения всех полей
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password) ||
                string.IsNullOrEmpty(PasswordBoxConfirm.Password) || string.IsNullOrEmpty(TextBoxFIO.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            // 5.2. Проверка уникальности логина
            using (var db = new Model1())
            {
                if (db.User.Any(u => u.Login == TextBoxLogin.Text))
                {
                    MessageBox.Show("Логин уже существует!");
                    return;
                }
            }

            string pwd = PasswordBox.Password;

            // 5.3. Проверка пароля по трем условиям методички
            if (pwd.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать 6 или более символов!");
                return;
            }
            if (!Regex.IsMatch(pwd, @"^[A-Za-z0-9]+$")) // Только английская раскладка
            {
                MessageBox.Show("Пароль должен содержать только английские буквы и цифры!");
                return;
            }
            if (!Regex.IsMatch(pwd, @"\d")) // Хотя бы одна цифра
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру!");
                return;
            }

            // 5.4. Проверка совпадения паролей
            if (pwd != PasswordBoxConfirm.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            // 5.5. Запись в БД
            using (var db = new Model1())
            {
                var user = new User
                {
                    Login = TextBoxLogin.Text,
                    Password = AuthPage.GetHash(pwd), // Хешируем пароль 
                    Role = CmbRole.Text,
                    FIO = TextBoxFIO.Text
                };
                db.User.Add(user);
                db.SaveChanges();
            }

            // 5.6. Сообщение об успехе
            MessageBox.Show("Регистрация успешна!");

            // 5.7. Очистка полей
            TextBoxLogin.Clear(); PasswordBox.Clear(); PasswordBoxConfirm.Clear(); TextBoxFIO.Clear(); CmbRole.SelectedIndex = 0;
        }
    }
}