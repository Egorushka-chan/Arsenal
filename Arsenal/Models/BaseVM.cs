using Arsenal.Models.DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arsenal.Models
{
    abstract class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private ObservableCollection<item> items;
        private ObservableCollection<loader> loaders;
        private ObservableCollection<inventarisation> inventarisations;
        private ObservableCollection<storage> storages;
        private ObservableCollection<delivery> deliveries;
        private ObservableCollection<issue> issues;
        private ObservableCollection<history> histories;
        private ObservableCollection<_operator> operators;
        private ObservableCollection<type> types;

        public ArsenalDB DataBase { get; set; }
        public ObservableCollection<item> Items { get => items; set { items = value; OnPropertyChanged(nameof(Items)); } }
        public ObservableCollection<loader> Loaders { get => loaders; set { loaders = value; OnPropertyChanged(nameof(Loaders)); } }
        public ObservableCollection<inventarisation> Inventarisations { get => inventarisations; set { inventarisations = value; OnPropertyChanged(nameof(Inventarisations)); } }
        public ObservableCollection<storage> Storages { get => storages; set { storages = value; OnPropertyChanged(nameof(Storages)); } }
        public ObservableCollection<delivery> Deliveries { get => deliveries; set { deliveries = value; OnPropertyChanged(nameof(Deliveries)); } }
        public ObservableCollection<issue> Issues { get => issues; set { issues = value; OnPropertyChanged(nameof(Issues)); } }
        public ObservableCollection<history> Histories { get => histories; set { histories = value; OnPropertyChanged(nameof(Histories)); } }
        public ObservableCollection<_operator> Operators { get => operators; set { operators = value; OnPropertyChanged(nameof(Operators)); } }
        public ObservableCollection<type> Types { get => types; set { types = value; OnPropertyChanged(nameof(Types)); } }


        public int EntranseID { get; set; }

        public _operator CurrentOperator { get; set; }

        /// <summary>
        /// Импортирует базу данных из одного ViewModel в другой
        /// </summary>
        /// <param name="viewI">Из</param>
        /// <param name="viewII">В</param>
        protected void SetData(BaseVM viewI, BaseVM viewII)
        {
            viewII.DataBase = viewI.DataBase;
            viewII.Deliveries = viewI.Deliveries;
            viewII.Histories = viewI.Histories;
            viewII.Inventarisations = viewI.Inventarisations;
            viewII.Issues = viewI.Issues;
            viewII.Items = viewI.Items;
            viewII.Loaders = viewI.Loaders;
            viewII.Operators = viewI.Operators;
            viewII.Storages = viewI.Storages;
            viewII.EntranseID = viewI.EntranseID;
            viewII.CurrentOperator = viewI.CurrentOperator;
            viewII.Types = viewI.Types;
        }

        protected void NewHistoryNote(string Info)
        {
            int ActionCount;
            using (MySqlConnection connection = new MySqlConnection(DataBase.Database.Connection.ConnectionString))
            {
                connection.Open();
                object Action = new MySqlCommand($"SELECT COUNT(ActionID) from history where `Entrance Num` = {EntranseID}", connection).ExecuteScalar();
                connection.Close();
                ActionCount = int.Parse(Action.ToString()) + 1;
            }

            Histories.Add(new history() { Entrance_Num = EntranseID, ActionID = ActionCount, Actions = Info, Date = DateTime.Now, PN = CurrentOperator.PN });

        }
    }
}
