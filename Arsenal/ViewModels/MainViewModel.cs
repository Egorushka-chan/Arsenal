using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views;
using Equipment_accounting.Model;
using MySqlX.XDevAPI;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Arsenal.ViewModels
{
    class MainViewModel : BaseVM
    {
        public List<loader> BestLoaders { get; set; }
        public List<string> Loses { get; set; }

        public ICollectionView AmmoView { get; set; }
        public ICollectionView WeaponView { get; set; }

        public MainViewModel()
        {
            DataBase = new ArsenalDB();

            DataBase.item.Load();
            Items = DataBase.item.Local;

            DataBase.loader.Load();
            Loaders = DataBase.loader.Local;

            DataBase.inventarisation.Load();
            Inventarisations = DataBase.inventarisation.Local;

            DataBase.storage.Load();
            Storages = DataBase.storage.Local;

            DataBase.issue.Load();
            Issues = DataBase.issue.Local;

            DataBase.delivery.Load();
            Deliveries = DataBase.delivery.Local;

            DataBase._operator.Load();
            Operators = DataBase._operator.Local;

            DataBase.history.Load();
            Histories = DataBase.history.Local;

            BestLoaders = new List<loader>();
            Loses = new List<string>();

            AmmoView = new CollectionViewSource { Source = Storages }.View;
            AmmoView.Filter = (objFilter) =>
            {
                if (objFilter is storage storage)
                {
                    return storage.item.type.Type == "Патрон" && storage.issue.Count == 0;
                }

                MessageBox.Show("ОйОЙОЙ что то не так 41");
                return false;
            };

            WeaponView = new CollectionViewSource { Source = Storages }.View;
            WeaponView.Filter = (objFilter) =>
            {
                if (objFilter is storage storage)
                {
                    return storage.item.type.Type == "Орудие" && storage.issue.Count == 0;
                }
                MessageBox.Show("ОйОЙОЙ что то не так 41");
                return false;
            };

            foreach(loader loader in Loaders)
            {
                int result = loader.delivery.Count + loader.issue.Count;
                foreach(loader bestLoader in BestLoaders)
                {
                    int best = bestLoader.issue.Count + bestLoader.delivery.Count;
                    
                    if (best < result)
                    {
                        BestLoaders[BestLoaders.IndexOf(bestLoader)] = loader;
                    }
                }
                if (BestLoaders.Count < 3)
                {
                    BestLoaders.Add(loader);
                }
            }

            foreach(inventarisation inventarisation in Inventarisations)
            {
                if(inventarisation.Real_Quantity != inventarisation.Nominal_Quantity)
                {
                    Loses.Add($"{Loses.Count + 1}.  {inventarisation.storage.item.Name} - {inventarisation.Nominal_Quantity - inventarisation.Real_Quantity}");
                }
            }
        }

        public ICommand BaseActionOpen
        {
            get
            {
                return new MyCommand(obj => 
                {
                    var w = new BaseActionsWindow();
                    var vm = new BaseActionsVM();
                    SetData(this, vm);
                    vm.Initialize();
                    w.DataContext = vm;
                    w.ShowDialog();
                });
            }
        }

        public ICommand OperatorsOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new OperatorsWindow();
                    var vm = new OperatorsViewModel();
                    SetData(this, vm);
                    vm.Initialize();
                    w.DataContext = vm;
                    w.ShowDialog();
                });
            }
        }
    }
}
