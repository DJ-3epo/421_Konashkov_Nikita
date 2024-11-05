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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            TextBoxLogin.Clear();
            PasswordBox.Clear();
            PasswordBoxConfirm.Clear();
            TextBoxFIO.Clear();
            CmbRole.SelectedIndex = 0;
            NavigationService.GoBack();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            // 7.1 Проверка заполненности полей
            if (string.IsNullOrEmpty(TextBoxLogin.Text) ||
                string.IsNullOrEmpty(PasswordBox.Password) ||
                string.IsNullOrEmpty(PasswordBoxConfirm.Password) ||
                string.IsNullOrEmpty(TextBoxFIO.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // 7.2 Проверка уникальности логина
            using (Entities db = new Entities())
            {
                if (db.User.Any(user => user.Login == TextBoxLogin.Text))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }
            }

            // 7.3 Проверка пароля
            if (PasswordBox.Password.Length < 6 ||
                !PasswordBox.Password.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')) ||
                !PasswordBox.Password.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов, включать только английские буквы и хотя бы одну цифру.");
                return;
            }

            // 7.4 Проверка совпадения паролей
            if (PasswordBox.Password != PasswordBoxConfirm.Password)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            // 7.5 Запись пользователя в базу данных
            using (Entities db = new Entities())
            {
                User newUser = new User
                {
                    FIO = TextBoxFIO.Text,
                    Login = TextBoxLogin.Text,
                    Password = PasswordBox.Password,
                    Role = (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                db.User.Add(newUser);
                db.SaveChanges();
            }

            // 7.6 Сообщение об успешной регистрации
            MessageBox.Show("Пользователь успешно зарегистрирован!");
        }
    }
}
