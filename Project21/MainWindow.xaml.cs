using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Project21
{
    /// <summary>
    /// Логика взаимодействия для главного окна MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Таймер для обновления текущей даты и времени каждую секунду
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => TextBlockDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            timer.Start();
        }

        // Обработчик закрытия окна (Шаг 1, п.8 - выход с подтверждением)
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        // Обработчик события Navigated (Шаг 4, п.12 - смена заголовка и видимости кнопки "Назад")
        private void MainFrame_OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                // Установка заголовка окна с названием текущей страницы
                Title = "УП.02 — " + page.Title;
                // Показываем кнопку "Назад", только если есть куда возвращаться
                ButtonBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // Обработчик кнопки "Назад" (Шаг 4, п.13)
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }
    }
}