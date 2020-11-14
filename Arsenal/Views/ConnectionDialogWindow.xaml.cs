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
using System.Windows.Shapes;

namespace Arsenal
{
    /// <summary>
    /// Логика взаимодействия для ConnectionDialogWindow.xaml
    /// </summary>
    public partial class ConnectionDialogWindow : Window
    {
        string w;
        public ConnectionDialogWindow()
        {
            InitializeComponent();
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["BaseConnection"] = 
                $"server = {tb1.Text};port = {tb2.Text};username={tb3.Text};password={tb4.Text};database=arsenal";
            Properties.Settings.Default.Save();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            w = (string)Properties.Settings.Default["BaseConnection"];
            string[] arr = w.Split(';');
            tb1.Text = arr[0].Remove(0, 9);
            tb2.Text = arr[1].Remove(0, 7);
            tb3.Text = arr[2].Remove(0, 9);
            tb4.Text = arr[3].Remove(0, 9);
        }
    }
}
