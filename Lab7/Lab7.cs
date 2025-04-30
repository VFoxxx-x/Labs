using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AnimatedButtonApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Анимация затухания текста
            var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
            fadeOut.Completed += (s, a) =>
            {
                ResultText.Text = "Clicked!";
                var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
                ResultText.BeginAnimation(OpacityProperty, fadeIn);
            };

            ResultText.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}