
using System.Windows;

namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserData SharedUserData { get; set; } = new UserData();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Page1(SharedUserData));
        }
    }
}