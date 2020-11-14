using Arsenal.Models.DataBase;
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

        public ArsenalDB DataBase { get; set; }
        public ObservableCollection<item> Items { get; set; }
        public ObservableCollection<loader> Loaders { get; set; }
        public ObservableCollection<inventarisation> Inventarisations { get; set; }
        public ObservableCollection<storage> Storages { get; set; }
        public ObservableCollection<delivery> Deliveries { get; set; }
        public ObservableCollection<issue> Issues { get; set; }
        public ObservableCollection<history> Histories { get; set; }
        public ObservableCollection<_operator> Operators { get; set; }

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
        }
    }
}
