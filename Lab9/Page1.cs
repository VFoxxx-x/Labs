using System.Windows;
using System.Windows.Controls;
using WpfApp9;

namespace WpfApp9
{
    public partial class Page1 : Page
    {
        private readonly UserData userData;

        public Page1(UserData data)
        {
            InitializeComponent();
            userData = data;
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(LastName.Text) || BirthDate.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            userData.FirstName = FirstName.Text;
            userData.LastName = LastName.Text;
            userData.BirthDate = BirthDate.SelectedDate;

            this.NavigationService.Navigate(new Page2(userData));
        }
    }
}