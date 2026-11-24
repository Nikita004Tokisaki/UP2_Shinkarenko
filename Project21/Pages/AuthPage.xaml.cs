using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Project21.Pages
{
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Хеширование пароля алгоритмом SHA-1 (Шаг 6)
        /// </summary>
        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        // Обработчик входа (Шаг 4, п.9-11)
        private void ButtonEnter_OnClick(object sender, RoutedEventArgs e)
        {
            // Проверка заполненности полей
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            // Хешируем введенный пароль
            string hash = GetHash(PasswordBox.Password);

            using (var db = new Model1())
            {
                // Ищем запись в БД
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password == hash);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

                MessageBox.Show("Успешный вход!");

                // Переход на страницу согласно роли
                switch (user.Role)
                {
                    case "Администратор": NavigationService.Navigate(new AdminPage()); break;
                    case "Пользователь": NavigationService.Navigate(new UserPage()); break;
                }
            }
        }

        // Переход на регистрацию
        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
