using Arsenal.Models;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Arsenal.ViewModels
{
    class HistoryViewModel : BaseVM
    {
        public ICollectionView HistoryView { get; set; }

        public void Initialize()
        {
            HistoryView = CollectionViewSource.GetDefaultView(Histories);
            HistoryView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        }

        public ICommand AddNoteOpen
        {
            get
            {
                return new MyCommand(obj =>
                {
                    var w = new AddNoteInHistoryWindow();
                    if(w.ShowDialog() == true)
                    {
                        NewHistoryNote($"Добавлено примечание: {w.Text}");

                        DataBase.SaveChanges();
                    }
                });
            }
        }
    }
}
