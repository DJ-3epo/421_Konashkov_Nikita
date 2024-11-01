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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }
        private void TextBoxLogin_Changed(object sender, RoutedEventArgs e)
        {
            txtHintLogin.Visibility = string.IsNullOrEmpty(TextBoxLogin.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void PasswordBoxPassword_Changed(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = string.IsNullOrEmpty(PasswordBoxPassword.Password) ? Visibility.Visible : Visibility.Hidden;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(PasswordBoxPassword.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

           
            using (var DataBase = new Entities())
            {
                var user = DataBase.User
                    .AsNoTracking() 
                    .FirstOrDefault(u => u.Login == TextBoxLogin.Text && u.Password == PasswordBoxPassword.Password);

                
                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

             
                MessageBox.Show("Вход выполнен успешно!");
                
                switch (user.Role)
                {
                    case "администратор":
                        NavigationService?.Navigate(new AdminMenu());
                        break;
                    case "пользователь":
                        NavigationService?.Navigate(new UserMenu());
                        break;

                }
                
            }
        }

    }
}
