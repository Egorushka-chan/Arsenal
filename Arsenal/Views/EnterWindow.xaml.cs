using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace Arsenal
{
    /// <summary>
    /// Логика взаимодействия для EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window
    {
        public EnterWindow()
        {
            InitializeComponent();
        }

        public static int OperatorID { get; private set; }

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
            if (loginBox.Text == "Введите логин для входа")
            {
                loginBox.Text = null;
                loginBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                Error(false);
            }
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (passBox.Text == "Введите пароль для входа")
            //{
            //    passBox.Text = null;
            //    passBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            //}
            passwordBox.Focus();
            Panel.SetZIndex(passwordBox, 2);
            Error(false);
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (passBox.Text == "")
            //{
            //    passBox.Text = "Введите пароль для входа";
            //    passBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF787878"));
            //}
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void passwordBox_LostFocus_1(object sender, RoutedEventArgs e)
        { 
            if (passwordBox.Password == "" || passwordBox.Password is null)
            Panel.SetZIndex(passwordBox, 0);
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default["BaseConnection"].ToString()))
                {
                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "SELECT PN FROM operator WHERE Login LIKE '" + loginBox.Text + "' AND Password LIKE '" + passwordBox.Password + "'";
                    command.Connection = connection;

                    connection.Open();
                    string data = Convert.ToString(command.ExecuteScalar());
                    bool res = null == data;
                    connection.Close();
                    if (res || data == string.Empty)
                    {
                        Error(true, "Неверные данные");
                    }
                    else
                    {
                        OperatorID = int.Parse(data);
                        DialogResult = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Error(true, "Невозможно подключится к серверу");
                MessageBox.Show(ex.Message + " " + ex.StackTrace + " " + ex.Source);
                return;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Вспомни", "Пароль", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        void Error(bool IsMarkOn, string text = "Ошибка")
        {
            switch (IsMarkOn)
            {
                case true:
                    errorLabel.Visibility = Visibility.Visible;
                    break;
                case false:
                    errorLabel.Visibility = Visibility.Hidden;
                    break;
            }
            errorLabel.Content = text;
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionDialogWindow dialogWindow = new ConnectionDialogWindow();
            dialogWindow.ShowDialog();
        }
    }
}
