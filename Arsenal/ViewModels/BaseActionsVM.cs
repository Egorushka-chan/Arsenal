using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;

namespace Arsenal.ViewModels
{
    class BaseActionsVM : BaseVM
    {
        public MainViewModel Main { get; set; }

        private loader selectedLoader;
        private item selectedItem;

        public item SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); } }
        public loader SelectedLoader { get => selectedLoader; set { selectedLoader = value; OnPropertyChanged(nameof(SelectedLoader)); } }

        public ICollectionView OperationalLoader { get; set; }
        public ICollectionView ExistingStorages { get; set; }

        public void Initialize()
        {
            if (Items.Count > 0)
                SelectedItem = Items[0];

            if (Loaders.Count > 0)
                SelectedLoader = Loaders[0];

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

            OperationalLoader = CollectionViewSource.GetDefaultView(Loaders);
            OperationalLoader.Filter = (objFilter) =>
            {
                if (objFilter is loader loader)
                {
                    return loader.Dismissal_date == default;
                }
                MessageBox.Show("ОйОЙОЙ что то не так 49");
                return false;
            };
        }

        void Refresh()
        {
            OperationalLoader.Refresh();
            ExistingStorages.Refresh();
        }

        public loader NewLoader { get; set; }

        public ICommand OpenAddOperator
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    NewLoader = new loader()
                    {
                        Date_of_Birth = DateTime.Now,
                        Hiring_date = DateTime.Now
                    };

                    var w = new AddLoaderWindow();
                    w.DataContext = this;
                    w.ShowDialog();
                });
            }
        }

        public MyCommand AddPersonal
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    Loaders.Add(NewLoader);

                    NewHistoryNote($"Добавлен новый грузчик {NewLoader.FullName}");

                    try
                    {
                        DataBase.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " " + ex.Source);
                        DataBase.loader.Load();
                        Loaders = DataBase.loader.Local;
                    }

                    object nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{NewLoader.PN} {NewLoader.FullName} Трудовой договор.doc";
                    File.Copy(Environment.CurrentDirectory + "\\Договоры\\Трудовой договор.doc", nameNewFile.ToString(), true);
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
                                textReplace = new Random(414).Next(1, 100).ToString();
                                break;
                            case 2:
                                textReplace = NewLoader.FullName;
                                break;
                            case 3:
                                textReplace = "Грузчик";
                                break;
                            case 4:
                                textReplace = DateTime.Now.ToShortDateString();
                                break;
                            case 5:
                                textReplace = NewLoader.passport_ID.ToString();
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

                }, obj => NewLoader.First_Name != "" && NewLoader.Last_Name != "" && NewLoader.Date_of_Birth != default && NewLoader.Hiring_date != default && NewLoader.passport_ID.ToString().Length == 6);
            }
        }

        public ICommand DeleteLoader
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new DeleteOperatorWindow();
                    w.DataContext = this;
                    if (w.ShowDialog() == true)
                    {
                        string excuse = w.Text;

                        SelectedLoader.Dismissal_date = DateTime.Now;
                        OnPropertyChanged(nameof(SelectedLoader));

                        NewHistoryNote($"Уволен грузчик {SelectedLoader.FullName}");

                        try
                        {
                            DataBase.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + " " + ex.Source);
                            DataBase.loader.Load();
                            Loaders = DataBase.loader.Local;
                        }

                        object nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{SelectedLoader.PN} {SelectedLoader.FullName} Увольнение.doc";

                        try
                        {
                            File.Copy(Environment.CurrentDirectory + "\\Договоры\\Заявление на увольнение.doc", nameNewFile.ToString(), true);
                        }
                        catch (ArgumentException)
                        {
                            nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{SelectedLoader.PN} Увольнение.doc";
                            File.Copy(Environment.CurrentDirectory + "\\Договоры\\Заявление на увольнение.doc", nameNewFile.ToString(), true);
                        }

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
                                    textReplace = SelectedLoader.FullName;
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
                        Refresh();
                        SelectedLoader = Loaders.Where(p => p.Dismissal_date == default).FirstOrDefault();
                    }
                }, obj => Loaders.Count > 1 && SelectedLoader.Dismissal_date == default && SelectedLoader != null);
            }
        }

        //OpenAddItem
        //DeleteItem
        public item NewItem { get; set; }

        public ICommand OpenAddItem
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    NewItem = new item();
                    AddItemWindow itemWindow = new AddItemWindow()
                    {
                        DataContext = this
                    };
                    itemWindow.ShowDialog();
                });
            }
        }

        public ICommand DeleteItem
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (MessageBox.Show($"Вы действительно хотите удалить {SelectedItem.Name}", "Вы уверены?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Items.Remove(SelectedItem);
                        NewHistoryNote($"Удален предмет: {SelectedItem.Name}");
                        DataBase.SaveChanges();
                        SelectedItem = Items[0];
                    }

                }, obj => SelectedItem != default);
            }
        }

        public ICommand AddItem
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (NewItem.Type_ID == default)
                        NewItem.Type_ID = NewItem.type.ID;

                    Items.Add(NewItem);
                    NewHistoryNote($"Добавлен предмет {NewItem.Name}");

                    try
                    {
                        DataBase.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " " + ex.Source);
                    }

                }, obj => NewItem.Name != default && NewItem.Manufacturer != default && NewItem.Properities != default && (NewItem.Type_ID != default || NewItem.type != default));
            }
        }

        public delivery NewDelivery { get; set; }
        public storage NewStorage { get; set; }

        public ICommand OpenAddDeliveryWindow
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    NewDelivery = new delivery()
                    {
                        Date = DateTime.Now
                    };
                    NewStorage = new storage()
                    {
                        Quantity = 1
                    };

                    AddDeliveryWindow deliveryWindow = new AddDeliveryWindow()
                    {
                        DataContext = this
                    };
                    deliveryWindow.ShowDialog();
                });
            }
        }

        public ICommand AddDelivery
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (NewStorage.Item_ID == default)
                        NewStorage.Item_ID = NewStorage.item.Item_ID;

                    if (NewDelivery.PN == default)
                        NewDelivery.PN = NewDelivery.loader.PN;
                    NewStorage.ID = Storages.Count + 1;


                    Storages.Add(NewStorage);

                    NewDelivery.Storage_ID = NewStorage.ID;
                    NewDelivery.Item_ID = NewStorage.Item_ID;

                    Deliveries.Add(NewDelivery);

                    NewHistoryNote($"Оформлена новая поставка {NewStorage.item.Name} {NewStorage.Quantity} шт.");

                    try
                    {
                        DataBase.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " " + ex.Source);
                    }


                }, obj => NewDelivery.Date != default && NewDelivery.loader != default && NewStorage.item != default && NewStorage.Quantity != default);
            }
        }

        public issue NewIssue { get; set; }

        public ICommand OpenAddIssueWindow
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    NewIssue = new issue()
                    {
                        Date = DateTime.Now
                    };

                    AddIssueWindow issueWindow = new AddIssueWindow()
                    {
                        DataContext = this
                    };
                    issueWindow.ShowDialog();
                });
            }
        }

        public ICommand AddIssue
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if (NewIssue.PN == default)
                        NewIssue.PN = NewIssue.loader.PN;

                    if (NewIssue.Storage_ID == default)
                        NewIssue.Storage_ID = NewIssue.storage.ID;

                    if (NewIssue.Item_ID == default)
                        NewIssue.Item_ID = NewIssue.storage.Item_ID;

                    var x = Storages.Where(p => p.ID == NewIssue.storage.ID).ToList();
                    if (x.Count == 0)
                    {
                        MessageBox.Show("Pizdets");
                    }
                    else
                    {
                        Storages[Storages.IndexOf(x[0])].issue.Add(NewIssue);
                        Main.Storages[Storages.IndexOf(x[0])].issue.Add(NewIssue);
                    }

                    Issues.Add(NewIssue);

                    NewHistoryNote($"Оформлена выгрузка {NewIssue.storage.item.Name} {NewIssue.storage.Quantity} шт.");

                    try
                    {
                        DataBase.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " " + ex.Source);
                    }
                    Main.Refresh();
                    Refresh();

                }, obj => NewIssue.Date != default && NewIssue.loader != default && NewIssue.storage != default);
            }
        }
    }
}
