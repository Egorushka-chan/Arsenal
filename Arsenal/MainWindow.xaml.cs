using MySql.Data.MySqlClient;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Arsenal.ViewModels;

namespace Arsenal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            
            EnterWindow enterWindow = new EnterWindow();
            if (enterWindow.ShowDialog() != true)
            {
                Environment.Exit(0);
            }
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();

            //using (MySqlConnection connection = new MySqlConnection(Properties.Settings.Default["BaseConnection"].ToString()))
            //{
            //    string CommandText = $"SELECT `operators`.`First name`, `operators`.`Last name`, `operators`.`Patronymic` FROM `operators` WHERE Login LIKE '{operatoris}'";

            //    DataSet dataSetFIO = Execute_Data_Query(connection, CommandText);
            //    labelFIO.Content = ($"{dataSetFIO.Tables[0].Rows[0][0]} {dataSetFIO.Tables[0].Rows[0][1]} {dataSetFIO.Tables[0].Rows[0][2]}");

            //    CommandText = "SELECT `ammunition`.`Caliber` AS `Калибр`, `ammunition`.`Appoiment` AS `Назначение`, `ammunition storage`.`Quantity` AS `Количество` " +
            //        "FROM `ammunition storage` INNER JOIN `ammunition` ON `ammunition storage`.`IDammo` = `ammunition`.`IDammo` " + "WHERE `ammunition storage`.`IDissue` IS NULL";

            //    DataSet dataSetAmmo = Execute_Data_Query(connection, CommandText);
            //    ammunitionTable.ItemsSource = dataSetAmmo.Tables[0].DefaultView;
            //    labelAmmoCount.Content = $"Статей хранения: {ammunitionTable.Items.Count}";

            //    CommandText = "Select `weapons`.`Weapon name` AS `Название оружия`, `weapons`.`Manufacturer` AS `Произодитель`, `weapon storage`.`Quantity` AS `Количество` " +
            //        "FROM `weapon storage` INNER JOIN `weapons` ON `weapon storage`.`IDweapon` = `weapons`.`IDweapon` " + "WHERE `weapon storage`.`IDissue` IS NULL";

            //    DataSet dataSetWeapon = Execute_Data_Query(connection, CommandText);
            //    weaponTable.ItemsSource = dataSetWeapon.Tables[0].DefaultView;
            //    labelWeaponCount.Content = $"Статей хранения: {weaponTable.Items.Count}";

            //    CommandText = "SELECT `loaders`.`First Name` AS `Имя`, `loaders`.`Last Name` AS `Фамилия`, `loaders`.`Patronymic` AS `Отчество`, COUNT(`delivery`.`shifts_IDshift`) AS `Кол-во` " +
            //        "From((`loaders` INNER JOIN `shifts` ON `shifts`.`PN` = `loaders`.`PN`) INNER JOIN `delivery` ON `shifts`.`IDshift` = `delivery`.`shifts_IDshift`) " +
            //        "GROUP BY `loaders`.`First Name`, `loaders`.`Last Name`, `loaders`.`Patronymic`";

            //    DataSet dataSetDelivery = Execute_Data_Query(connection, CommandText);

            //    CommandText = "SELECT `loaders`.`First Name` AS `Имя`, `loaders`.`Last Name` AS `Фамилия`, `loaders`.`Patronymic` AS `Отчество`, COUNT(`issue`.`shifts_IDshift`) AS `Кол-во` " +
            //        "From((`loaders` INNER JOIN `shifts` ON `shifts`.`PN` = `loaders`.`PN`) INNER JOIN `issue` ON `shifts`.`IDshift` = `issue`.`shifts_IDshift`) " +
            //        "GROUP BY `loaders`.`First Name`, `loaders`.`Last Name`, `loaders`.`Patronymic`";

            //    DataSet dataSetIssue = Execute_Data_Query(connection, CommandText);

            //    DataTable dataDelivery = dataSetDelivery.Tables[0];
            //    DataTable dataIssue = dataSetIssue.Tables[0];

            //    foreach (DataRow dataRowD in dataDelivery.Rows)
            //    {
            //        foreach (DataRow dataRowI in dataIssue.Rows)
            //        {
            //            if ((dataRowI.ItemArray[0].ToString() == dataRowD.ItemArray[0].ToString()) & (dataRowI.ItemArray[1].ToString() == dataRowD.ItemArray[1].ToString()))
            //            {
            //                dataRowI[3] = Convert.ToInt32(dataRowI.ItemArray[3].ToString()) + Convert.ToInt32(dataRowD.ItemArray[3].ToString());
            //            }
            //        }
            //    }

            //    dataIssue.DefaultView.Sort = "Кол-во DESC";
            //    labelFirstLoader.Content = $"1. {dataIssue.Rows[0][0]} {dataIssue.Rows[0][1]} {dataIssue.Rows[0][2]}";
            //    labelSecondLoader.Content = $"2. {dataIssue.Rows[1][0]} {dataIssue.Rows[1][1]} {dataIssue.Rows[1][2]}";
            //    labelThirdLoader.Content = $"3. {dataIssue.Rows[2][0]} {dataIssue.Rows[2][1]} {dataIssue.Rows[2][2]}";
            //}
        }

        DataSet Execute_Data_Query(MySqlConnection connection, string commandText)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandText = commandText;
            command.Connection = connection;

            DataSet dataSet = new DataSet();
            connection.Open();
            new MySqlDataAdapter(command).Fill(dataSet);
            connection.Close();

            return dataSet;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = DateTime.Now.ToLongTimeString();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void headingGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        bool isWiden = false;
        private void Move_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isWiden = true;
        }
        private void Move_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isWiden = false;
            Rectangle rect = (Rectangle)sender;
            rect.ReleaseMouseCapture();
        }
        private void HorizontalMove_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (isWiden)
            {
                rect.CaptureMouse();
                double newWidth = e.GetPosition(this).X + 5;
                if (newWidth > 0) this.Width = newWidth;
            }
        }
        private void VerticalMove_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            if (isWiden)
            {
                rect.CaptureMouse();
                double newHeight = e.GetPosition(this).Y + 5;
                if (newHeight > 0) this.Height = newHeight;
            }
        }
    }
}
