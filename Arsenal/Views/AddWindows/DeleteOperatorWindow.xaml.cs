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

namespace Arsenal.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для DeleteOperatorWindow.xaml
    /// </summary>
    public partial class DeleteOperatorWindow : Window
    {
        public DeleteOperatorWindow()
        {
            InitializeComponent();
        }

        public string Text { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text = textBox.Text;
            DialogResult = true;
        }
    }
}
