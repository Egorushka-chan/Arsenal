using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using Word = Microsoft.Office.Interop.Word;
using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
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
        private storage selectedStorage;
        private List<loader> bestLoaders;
        private List<string> loses;

        public List<loader> BestLoaders { get => bestLoaders; set { bestLoaders = value; OnPropertyChanged(nameof(BestLoaders)); } }
        public List<string> Loses { get => loses; set { loses = value; OnPropertyChanged(nameof(Loses)); } }

        public ICollectionView AmmoView { get; set; }
        public ICollectionView WeaponView { get; set; }
        public ICollectionView ExistingStorages { get; set; }

        public storage SelectedStorage { get => selectedStorage; set { selectedStorage = value; OnPropertyChanged(nameof(SelectedStorage)); } }

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

            DataBase.type.Load();
            Types = DataBase.type.Local;

            EntranseID = 1;
            foreach (history history in Histories)
            {
                if (history.Entrance_Num >= EntranseID)
                    EntranseID = history.Entrance_Num;
            }
            EntranseID++;

            BestLoaders = new List<loader>();
            Loses = new List<string>();

            AmmoView = new CollectionViewSource { Source = Storages }.View;
            AmmoView.Filter = (objFilter) =>
            {
                if (objFilter is storage storage)
                {
                    return storage.item.type.Type == "Патрон" && storage.issue.ToList().Count == 0;
                }

                MessageBox.Show("ОйОЙОЙ что то не так 41");
                return false;
            };

            WeaponView = new CollectionViewSource { Source = Storages }.View;
            WeaponView.Filter = (objFilter) =>
            {
                if (objFilter is storage storage)
                {
                    return storage.item.type.Type == "Орудие" && storage.issue.ToList().Count == 0;
                }
                MessageBox.Show("ОйОЙОЙ что то не так 41");
                return false;
            };

            ExistingStorages = CollectionViewSource.GetDefaultView(Storages);
            ExistingStorages.Filter = (objFilter) =>
            {
                if (objFilter is storage storage)
                {
                    return storage.issue.Count == 0;
                }
                MessageBox.Show("ОйОЙОЙ что то не так 51");
                return false;
            };

            AmmoView.GroupDescriptions.Add(new PropertyGroupDescription("item.Name"));
            WeaponView.GroupDescriptions.Add(new PropertyGroupDescription("item.Name"));

            var yt = from t in Loaders
                     orderby t.issue.Count + t.delivery.Count descending
                     select t;
            BestLoaders = yt.ToList();

            var ty = from t in Inventarisations
                     where t.Real_Quantity != t.Nominal_Quantity
                     orderby t.Real_Quantity != t.Nominal_Quantity descending
                     select t.storage.item.Name + " " + (t.Nominal_Quantity - t.Real_Quantity).ToString();

            Loses.Clear();

            foreach (string qoe in ty)
            {
                Loses.Add($"{Loses.Count + 1} {qoe}");
            }

            foreach (_operator _Operator in Operators)
            {
                if (_Operator.PN == EnterWindow.OperatorID)
                {
                    CurrentOperator = _Operator;
                }

            }

            Histories.Add(new history() { Entrance_Num = EntranseID, ActionID = 1, Date = DateTime.Now, PN = CurrentOperator.PN, Actions = "Вход в систему" });
            DataBase.SaveChanges();

            Inventarisations.CollectionChanged += Inventarisations_CollectionChanged;
        }

        public void Refresh()
        {
            //AmmoView.Filter = (objFilter) =>
            //{
            //    if (objFilter is storage storage)
            //    {
            //        return true;
            //    }

            //    MessageBox.Show("ОйОЙОЙ что то не так 41");
            //    return false;
            //};

            //WeaponView.Filter = (objFilter) =>
            //{
            //    if (objFilter is storage storage)
            //    {
            //        return true;
            //    }
            //    MessageBox.Show("ОйОЙОЙ что то не так 41");
            //    return false;
            //};

            AmmoView.GroupDescriptions.Clear();
            WeaponView.GroupDescriptions.Clear();

            AmmoView.GroupDescriptions.Add(new PropertyGroupDescription("item.Name"));
            WeaponView.GroupDescriptions.Add(new PropertyGroupDescription("item.Name"));

            AmmoView.Refresh();
            WeaponView.Refresh();
            ExistingStorages.Refresh();
            //AmmoView.Filter = (objFilter) =>
            //{
            //    if (objFilter is storage storage)
            //    {
            //        return storage.item.type.Type == "Патрон" && storage.issue.ToList().Count == 0;
            //    }

            //    MessageBox.Show("ОйОЙОЙ что то не так 41");
            //    return false;
            //};

            //WeaponView.Filter = (objFilter) =>
            //{
            //    if (objFilter is storage storage)
            //    {
            //        return storage.item.type.Type == "Орудие" && storage.issue.ToList().Count == 0;
            //    }
            //    MessageBox.Show("ОйОЙОЙ что то не так 41");
            //    return false;
            //};
        }

        private void Inventarisations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var yt = from t in Loaders
                     orderby t.issue.Count + t.delivery.Count descending
                     select t;
            BestLoaders = yt.ToList();

            var ty = from t in Inventarisations
                     where t.Real_Quantity != t.Nominal_Quantity
                     orderby t.Real_Quantity != t.Nominal_Quantity descending
                     select t.storage.item.Name + " " + (t.Nominal_Quantity - t.Real_Quantity).ToString();

            Loses.Clear();

            foreach (string qoe in ty)
            {
                Loses.Add($"{Loses.Count + 1} {qoe}");
            }

            Refresh();
        }

        #region Команды открытия форм

        public ICommand BaseActionOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new BaseActionsWindow();
                    var vm = new BaseActionsVM()
                    {
                        Main = this
                    };
                    SetData(this, vm);
                    vm.Initialize();
                    w.DataContext = vm;
                    w.Show();
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
                    var vm = new OperatorsViewModel()
                    {
                        CurrentOperator = CurrentOperator
                    };
                    SetData(this, vm);
                    vm.Initialize();
                    w.DataContext = vm;
                    w.Show();
                });
            }
        }

        public ICommand HistoryOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new HistoryWindow();
                    var vm = new HistoryViewModel()
                    {
                        CurrentOperator = CurrentOperator
                    };
                    SetData(this, vm);
                    vm.Initialize();
                    w.DataContext = vm;
                    w.Show();
                });
            }
        }

        public ICommand SettingsOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new SettingsWindow();
                    w.DataContext = this;
                    w.Show();
                });
            }
        }

        public ICommand ReportsOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new ReportsWindow();
                    var vm = new ReportsViewModel()
                    {
                        CurrentOperator = CurrentOperator
                    };
                    SetData(this, vm);
                    //vm.Initialize();
                    w.DataContext = vm;
                    w.Show();
                });
            }
        }

        public ICommand InventarisatonOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new InventarisationWindow();
                    w.DataContext = this;
                    w.Show();
                });
            }
        }

        #endregion

        public inventarisation NewInventarisation { get; set; }

        public ICommand AddInventarisaton
        {
            get
            {
                NewInventarisation = new inventarisation()
                {
                    Date = DateTime.Now
                };
                return new MyCommand(obj =>
                {
                    var w = new AddInventarisaton();
                    w.DataContext = this;
                    w.ShowDialog();
                });
            }
        }

        public ICommand AddAddInventarisaton
        {
            get
            {
                return new MyCommand(obj =>
                {
                    if (NewInventarisation.Nominal_Quantity == default)
                        NewInventarisation.Nominal_Quantity = NewInventarisation.storage.Quantity;

                    if (NewInventarisation.Storage_ID == default)
                        NewInventarisation.Storage_ID = NewInventarisation.storage.ID;

                    if (NewInventarisation.Item_ID == default)
                        NewInventarisation.Item_ID = NewInventarisation.storage.Item_ID;

                    if (NewInventarisation.Nominal_Quantity != NewInventarisation.Real_Quantity)
                        Storages[Storages.IndexOf(NewInventarisation.storage)].Quantity = NewInventarisation.Real_Quantity;

                    Inventarisations.Add(NewInventarisation);

                    NewHistoryNote($"Проведена новая инвенторизация {NewInventarisation.storage.FullInfo}");

                    try
                    {
                        DataBase.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " " + ex.Source);
                    }
                }, obj => NewInventarisation.storage != default && NewInventarisation.Date != default && NewInventarisation.Real_Quantity != default);
            }
        }

        public ICommand AddInventarisatonTextBox_SelectionChanged
        {
            get
            {
                return new MyCommand(obj =>
                {
                    NewInventarisation.Nominal_Quantity = NewInventarisation.storage.Quantity;
                });
            }
        }

        public ICommand RestartApplication
        {
            get
            {
                return new MyCommand(obj =>
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                });
            }
        }

        public ICommand HireMyself
        {
            get
            {
                return new MyCommand(obj =>
                {
                    if (MessageBox.Show("Вы точно хотите уволить себя?", "Точно?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        if (MessageBox.Show("Вы АБСОЛЮТНО УВЕРЕНЫ что хотите лишить себя работы?", "Точно точно ТОЧНО?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var w = new DeleteOperatorWindow();
                            w.DataContext = this;
                            if (w.ShowDialog() == true)
                            {
                                string excuse = w.Text;

                                CurrentOperator.Dismissal_date = DateTime.Now;
                                OnPropertyChanged(nameof(CurrentOperator));

                                NewHistoryNote($"Самоувольнение");

                                try
                                {
                                    DataBase.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message + " " + ex.Source);
                                    DataBase._operator.Load();
                                    Operators = DataBase._operator.Local;
                                }

                                object nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{CurrentOperator.PN} {CurrentOperator.FullName} Увольнение.doc";
                                File.Copy(Environment.CurrentDirectory + "\\Договоры\\Заявление на увольнение.doc", nameNewFile.ToString(), true);
                                Word.Application app = new Word.Application();
                                app.Documents.Open(ref nameNewFile);
                                Word.Find find = app.Selection.Find;
                                for (int i = 1; i < 6; i++)
                                {
                                    find.Text = "[" + i + "]";
                                    string textReplace = string.Empty;
                                    switch (i)
                                    {
                                        case 1:
                                            textReplace = CurrentOperator.FullName;
                                            break;
                                        case 2:
                                            textReplace = excuse;
                                            break;
                                        case 3:
                                            textReplace = DateTime.Now.ToShortDateString();
                                            break;
                                    }
                                    find.Replacement.Text = textReplace;
                                    find.Execute(FindText: Type.Missing,
                                                MatchCase: true,
                                                MatchWholeWord: true,
                                                MatchWildcards: false,
                                                MatchSoundsLike: Type.Missing,
                                                MatchAllWordForms: false,
                                                Forward: true,
                                                Wrap: Word.WdFindWrap.wdFindContinue,
                                                Format: false,
                                                ReplaceWith: Type.Missing,
                                                Replace: Word.WdReplace.wdReplaceAll);
                                }
                                app.ActiveDocument.Save();
                                app.ActiveDocument.Close();
                                app.Quit();

                                RestartApplication.Execute("Чтож, прощай");
                            }
                        }
                });
            }
        }
    }
}