using System.Windows;
using System.Data;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(UIElement element in MainGrid.Children)
            {
                if (element is Button)
                {
                    ((Button) element).Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string str1 = (string)((Button)e.OriginalSource).Content;

                if (str1 == "AC")
                {
                    Ftext.Text = "";
                }
                else if (str1 == "Del")
                {
                    Ftext.Text = Ftext.Text.Substring(0, Ftext.Text.Length-1);
                }
                else if (str1 == "=")
                {
                    string val = new DataTable().Compute(Ftext.Text, null).ToString();
                    Ftext.Text = val;
                }
                else
                    Ftext.Text = Ftext.Text.Replace(",",".") + str1;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}