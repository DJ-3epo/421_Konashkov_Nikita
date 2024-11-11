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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) { }
        private void ButtonDel_Click(object sender, RoutedEventArgs e) { }
    }
}
