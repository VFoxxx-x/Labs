using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp9
{
    public partial class Page3 : Page
    {
        private readonly UserData userData;

        public Page3(UserData data)
        {
            InitializeComponent();
            userData = data;
            City.Text= userData.City;
            Street.Text= userData.Street;
            HouseNumber.Text= userData.HouseNumber;
            ApartmentNumber.Text=userData.ApartmentNumber;
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();         
            userData.City = City.Text;
            userData.Street = Street.Text;
            userData.HouseNumber = HouseNumber.Text;
            userData.ApartmentNumber = ApartmentNumber.Text;
            
        }

        private void SubmitForm(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(City.Text) || string.IsNullOrWhiteSpace(Street.Text) || string.IsNullOrWhiteSpace(HouseNumber.Text))
            {
                MessageBox.Show("Заполните обязательные поля: город, улица и номер дома.");
                return;
            }

            userData.City = City.Text;
            userData.Street = Street.Text;
            userData.HouseNumber = HouseNumber.Text;
            userData.ApartmentNumber = ApartmentNumber.Text;

            string summary = $"Имя: {userData.FirstName}\n" +
                             $"Фамилия: {userData.LastName}\n" +
                             $"Дата рождения: {userData.BirthDate:dd.MM.yyyy}\n\n" +
                             $"Email: {userData.Email}\n" +
                             $"Телефон: {userData.Phone}\n\n" +
                             $"Адрес: г. {userData.City}, ул. {userData.Street}, д. {userData.HouseNumber}, кв. {userData.ApartmentNumber}";

            MessageBox.Show(summary, "Результат");
        }
    }
}