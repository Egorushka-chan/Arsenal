using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Arsenal.ViewModels
{
    class BaseActionsVM : BaseVM
    {
        public item SelectedItem { get; set; }
        public loader SelectedLoader { get; set; }

        public void Initialize()
        {
            if (Items.Count > 0)
                SelectedItem = Items[0];

            if (Loaders.Count > 0)
                SelectedLoader = Loaders[0];
        }

        public ICommand SaveChangesDB
        {
            get
            {
                return new MyCommand((obj) => 
                {
                    try
                    {
                        DataBase.SaveChanges();
                    }catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                });
            }
        }

        public ICommand DeleteItemLoader
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    if(obj is item item)
                    {
                        Items.Remove(item);

                        if (Items.Count > 0 && SelectedItem == item)
                            SelectedItem = Items[0];
                    }
                    if (obj is loader loader)
                    {
                        Loaders.Remove(loader);

                        if (Items.Count > 0 && SelectedLoader == loader)
                            SelectedLoader = Loaders[0];
                    }
                });
            }
        }

        public ICommand OpenAddDeliveryWindow
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    AddDeliveryWindow deliveryWindow = new AddDeliveryWindow()
                    {
                        DataContext = this
                    };
                    deliveryWindow.ShowDialog();
                });
            }
        }

        public ICommand OpenAddIssueWindow
        {
            get
            {
                return new MyCommand((obj) =>
                {
                    AddIssueWindow issueWindow = new AddIssueWindow()
                    {
                        DataContext = this
                    };
                    issueWindow.ShowDialog();
                });
            }
        }
    }
}
