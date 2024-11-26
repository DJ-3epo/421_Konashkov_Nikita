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

namespace _421_Konashkov_Nikita.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Page
    {
        public UserMenu()
        {
            InitializeComponent();
            var currentUsers = Entities.GetContext().User.ToList();
            ListUser.ItemsSource = currentUsers;
            // Установим начальные значения для элементов управления
            CmbSorting.SelectedIndex = 0;  // сортировка по возрастанию
            CheckDriver.IsChecked = false; // фильтр по роли не выбран
        }

        private void UpdateUsers()
        {
            // Загружаем всех пользователей
            var currentUsers = Entities.GetContext().User.ToList();

            // Фильтруем по Ф.И.О. без учета регистра
            currentUsers = currentUsers.Where(x => x.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            // Фильтруем по роли "Пользователь"
            if (CheckDriver.IsChecked.Value)
                currentUsers = currentUsers.Where(x => x.Role.Contains("Пользователь")).ToList();

            // Сортируем в зависимости от выбора пользователя
            if (CmbSorting.SelectedIndex == 0)
                ListUser.ItemsSource = currentUsers.OrderBy(x => x.FIO).ToList(); // по возрастанию
            else
                ListUser.ItemsSource = currentUsers.OrderByDescending(x => x.FIO).ToList(); // по убыванию
        }
        // Обработчик для поиска по Ф.И.О.
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        // Обработчик для сортировки
        private void ComboBoxSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        // Обработчик для фильтрации по роли
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }

        // Обработчик для кнопки очистки фильтра
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Сброс всех фильтров
            TextBoxSearch.Clear();
            CheckDriver.IsChecked = false;
            CmbSorting.SelectedIndex = 0;
            UpdateUsers();
        }

    }
}
