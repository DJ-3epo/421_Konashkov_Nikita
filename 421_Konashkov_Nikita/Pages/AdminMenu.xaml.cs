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
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        public AdminMenu()
        {
            InitializeComponent();

            DataGridUser.ItemsSource = Entities.GetContext().User.ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Entities db = new Entities();
            // Создаем нового пользователя
            var newUser = new User
            {
                Login = "NewUser", // Здесь можно добавить логику ввода
                Password = "Password123",
                Role = "User",
                FIO = "New User",
                Photo = "photo_path" // Можно оставить пустым или задать значение по умолчанию
            };

            // Добавление пользователя в контекст и сохранение изменений
            db.User.Add(newUser);
            db.SaveChanges(); // Сохраняем изменения в базу данных

            // Обновляем данные в DataGrid
            DataGridUser.ItemsSource = db.User.ToList();
        }


        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            // Используем существующий контекст, а не создаем новый
            var db = Entities.GetContext(); // Получаем существующий контекст из вашего класса

            // Получаем выбранного пользователя
            var selectedUser = DataGridUser.SelectedItem as User;

            if (selectedUser != null)
            {
                // Удаляем пользователя из контекста
                db.User.Remove(selectedUser);
                db.SaveChanges(); // Сохраняем изменения в базе данных

                // Обновляем данные в DataGrid
                DataGridUser.ItemsSource = db.User.ToList();
            }
            else
            {
                // Если пользователь не выбран, показываем сообщение
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
        }
    }
}