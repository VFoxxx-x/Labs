using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.IO;

namespace WpfApp9
{
    public partial class Page2 : Page
    {
        private readonly UserData userData;

        public Page2(UserData data)
        {
            InitializeComponent();
            userData = data;            
            Email.Text= userData.Email;
            Phone.Text= userData.Phone;
            
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();           
            userData.Email = Email.Text;
            userData.Phone = Phone.Text;
            
            }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            
            if (!Regex.IsMatch(Email.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный Email.");
                return;
            }

            if (!Regex.IsMatch(Phone.Text, @"^\+?\d{10,15}$"))
            {
                MessageBox.Show("Введите корректный номер телефона.");
                return;
            }

            userData.Email = Email.Text;
            userData.Phone = Phone.Text;

            this.NavigationService.Navigate(new Page3(userData));
        }
    }
}