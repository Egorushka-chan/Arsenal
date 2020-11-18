using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using System;
using Word = Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Data.Entity;
using System.ComponentModel;
using System.Windows.Data;
using MySql.Data.MySqlClient;

namespace Arsenal.ViewModels
{
    class OperatorsViewModel : BaseVM
    {

        public ICollectionView OperationalOperator { get; set; }

        private _operator _SelectedOperator;
        public _operator SelectedOperator 
        { 
            get => _SelectedOperator; 
            set
            {
                _SelectedOperator = value;
                OnPropertyChanged(nameof(SelectedOperator));
            }
        }

        public void Initialize()
        {
            SelectedOperator = CurrentOperator;

            OperationalOperator = CollectionViewSource.GetDefaultView(Operators);
            OperationalOperator.Filter = (objFilter) =>
            {
                if (objFilter is _operator @operator)
                {
                    return @operator.Dismissal_date == default;
                }
                MessageBox.Show("ОйОЙОЙ что то не так 49");
                return false;
            };
        }

        public ICommand OpenAddOperator
        {
            get
            {
                return new MyCommand(obj =>
                {
                    NewOperator = new _operator()
                    {
                        First_Name = "Алеша",
                        Date_of_Birth = DateTime.Now,
                        Hiring_date = DateTime.Now,
                        Last_Name = "Попович",
                        passport_ID = 111111
                    };
                    var w = new AddOperarorWindow();
                    w.DataContext = this;
                    w.ShowDialog();
                });
            }
        }

        public _operator NewOperator { get; set; }

        public MyCommand AddPersonal
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    var w = new AddLoginPassword()
                    {
                        Operators = Operators
                    };
                    if (w.ShowDialog() == true)
                    {
                        NewOperator.Login = w.Login;
                        NewOperator.Password = w.Password;

                        Operators.Add(NewOperator);

                        NewHistoryNote($"Добавлен новый оператор {NewOperator.FullName}");

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

                        object nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{NewOperator.PN} {NewOperator.FullName} Трудовой договор.doc";
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
                                    textReplace = NewOperator.FullName;
                                    break;
                                case 3:
                                    textReplace = "Оператор";
                                    break;
                                case 4:
                                    textReplace = DateTime.Now.ToShortDateString();
                                    break;
                                case 5:
                                    textReplace = NewOperator.passport_ID.ToString();
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
                    }
                }, obj => NewOperator.First_Name != "" && NewOperator.Last_Name != "" && NewOperator.Date_of_Birth != default && NewOperator.Hiring_date != default && NewOperator.passport_ID.ToString().Length == 6);
            }
        }

        public ICommand OpenDeleteOperator
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new DeleteOperatorWindow();
                    w.DataContext = this;
                    if(w.ShowDialog() == true)
                    {
                        string excuse = w.Text;

                        SelectedOperator.Dismissal_date = DateTime.Now;
                        OnPropertyChanged(nameof(SelectedOperator));

                        NewHistoryNote($"Уволен оператор {SelectedOperator.FullName}");

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

                        object nameNewFile = Environment.CurrentDirectory + $"\\Договоры\\{SelectedOperator.PN} {SelectedOperator.FullName} Увольнение.doc";
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
                                    textReplace = SelectedOperator.FullName;
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

                        SelectedOperator = CurrentOperator;
                    }
                }, obj => Operators.Count > 1 && SelectedOperator != CurrentOperator && SelectedOperator.Dismissal_date == default);
            }
        }
    }
}
