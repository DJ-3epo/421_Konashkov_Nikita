using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace _421_Konashkov_Nikita.Pages
{
    public partial class AddUserPage : Page
    {
        private User _currentUser;

        public AddUserPage(User selectedUser)
        {
            InitializeComponent();
         
            if (selectedUser != null) _currentUser = selectedUser;
            DataContext = _currentUser;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                errors.AppendLine("Укажите логин!");
            if (string.IsNullOrWhiteSpace(_currentUser.Password))
                errors.AppendLine("Укажите пароль!");
            if (string.IsNullOrWhiteSpace(_currentUser.FIO))
                errors.AppendLine("Укажите ФИО!");
            if (string.IsNullOrWhiteSpace(_currentUser.Role) || cmbRole.SelectedItem == null)
                errors.AppendLine("Выберите роль!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            // Добавление пользователя в базу данных
            if (_currentUser.ID == 0)
                Entities.GetContext().User.Add(_currentUser);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на страницу с таблицей пользователей
            NavigationService.GoBack();
        }
    }
}