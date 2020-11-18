using Arsenal.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AddLoginPassword.xaml
    /// </summary>
    public partial class AddLoginPassword : Window
    {
        public AddLoginPassword()
        {
            InitializeComponent();
        }

        public ObservableCollection<_operator> Operators { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "")
            {
                loginBox.Text = "Введите логин для входа";
                loginBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF787878"));
            }
        }

        private void loginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "Введите логин")
            {
                loginBox.Text = null;
                loginBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                //Error(false);
            }
        }

        private void passBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Focus();
            Panel.SetZIndex(passwordBox, 2);
            //Error(false);
        }

        private void passBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "" || passwordBox.Password is null)
                Panel.SetZIndex(passwordBox, 0);
        }

        private void passTryBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordTryBox.Focus();
            Panel.SetZIndex(passwordTryBox, 2);
        }

        private void passwordTryBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordTryBox.Password == "" || passwordTryBox.Password is null)
                Panel.SetZIndex(passwordTryBox, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password == passwordTryBox.Password)
            {
                bool IsLoginUnique = true;
                foreach(_operator @operator in Operators)
                {
                    if (loginBox.Text == @operator.Login)
                        IsLoginUnique = false;
                }

                if (IsLoginUnique)
                {
                    Login = loginBox.Text;
                    Password = passwordBox.Password;


                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Логин не уникален");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }

        private void passTryBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
