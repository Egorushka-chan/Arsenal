using Arsenal.Models;
using Arsenal.Models.DataBase;
using Arsenal.Views.AddWindows;
using Equipment_accounting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Arsenal.ViewModels
{
    class ReportsViewModel : BaseVM
    {
        DataTable currentTable;
        public DataTable CurrentTable { get => currentTable; set { currentTable = value; OnPropertyChanged(nameof(CurrentTable)); } }

        public ReportsViewModel()
        {
            CurrentTable = new DataTable();
            ReportFilters = new List<ReportFilter>();
        }

        private List<ReportFilter> reportFilters;
        public List<ReportFilter> ReportFilters { get => reportFilters; set { reportFilters = value; OnPropertyChanged(nameof(CurrentTable)); } }

        public ICommand AllAmmo => 
            new MyCommand(obj =>                 
            {
                var entireAmmo = from storage in Storages
                                 where storage.item.type.Type == "Патрон"
                                 select storage;
                DataTable newTable = new DataTable("AllAmmo");
                List<string> names = new List<string> {"ID", "Количество", "Имя", "Свойство", "Производитель", "На складе"};
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (storage ammo in entireAmmo)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ID"] = ammo.ID;
                    dataRow["Количество"] = ammo.Quantity;
                    dataRow["Имя"] = ammo.item.Name;
                    dataRow["Свойство"] = ammo.item.Properities;
                    dataRow["Производитель"] = ammo.item.Manufacturer;
                    dataRow["На складе"] = ammo.issue.ToList().Count == 0 ? "Да":"Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand AllWeapon =>
            new MyCommand(obj =>
            {
                var entireWeapon = from storage in Storages
                                 where storage.item.type.Type == "Орудие"
                                 select storage;
                DataTable newTable = new DataTable("AllWeapon");
                List<string> names = new List<string> { "ID", "Количество", "Имя", "Свойство", "Производитель", "На складе" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (storage weapon in entireWeapon)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ID"] = weapon.ID;
                    dataRow["Количество"] = weapon.Quantity;
                    dataRow["Имя"] = weapon.item.Name;
                    dataRow["Свойство"] = weapon.item.Properities;
                    dataRow["Производитель"] = weapon.item.Manufacturer;
                    dataRow["На складе"] = weapon.issue.ToList().Count == 0 ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand AllItem =>
            new MyCommand(obj =>
            {
                var entireWeapon = from storage in Storages
                                   select storage;
                DataTable newTable = new DataTable("AllItem");
                List<string> names = new List<string> { "ID", "Количество", "Имя", "Свойство", "Производитель", "На складе" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (storage weapon in entireWeapon)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ID"] = weapon.ID;
                    dataRow["Количество"] = weapon.Quantity;
                    dataRow["Имя"] = weapon.item.Name;
                    dataRow["Свойство"] = weapon.item.Properities;
                    dataRow["Производитель"] = weapon.item.Manufacturer;
                    dataRow["На складе"] = weapon.issue.ToList().Count == 0 ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });


        public ICommand BestLoaders =>
            new MyCommand(obj =>
            {
                var bestLoaders = from t in Loaders
                         orderby t.issue.Count + t.delivery.Count descending
                         select t;
                DataTable newTable = new DataTable("BestLoaders");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм", "Работает", "Баллы" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (loader loader in bestLoaders)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = loader.PN;
                    dataRow["ФИО"] = loader.FullName;
                    dataRow["Баллы"] = loader.issue.Count + loader.delivery.Count;
                    dataRow["Паспорт"] = loader.passport_ID;
                    dataRow["ДР"] = loader.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = loader.Hiring_date.ToShortDateString();
                    dataRow["Работает"] = loader.Dismissal_date == default ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand HiredLoaders =>
            new MyCommand(obj =>
            {
                var hiredLoaders = from t in Loaders
                                   where t.Dismissal_date != default
                                  select t;
                DataTable newTable = new DataTable("HiredLoaders");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм", "Увольнение"};
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (loader loader in hiredLoaders)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = loader.PN;
                    dataRow["ФИО"] = loader.FullName;
                    dataRow["Паспорт"] = loader.passport_ID;
                    dataRow["ДР"] = loader.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = loader.Hiring_date.ToShortDateString();
                    dataRow["Увольнение"] = loader.Dismissal_date;
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand AllLoaders =>
            new MyCommand(obj =>
            {
                var hiredLoaders = from t in Loaders
                                   select t;
                DataTable newTable = new DataTable("AllLoaders");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм", "Работает" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (loader loader in hiredLoaders)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = loader.PN;
                    dataRow["ФИО"] = loader.FullName;
                    dataRow["Паспорт"] = loader.passport_ID;
                    dataRow["ДР"] = loader.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = loader.Hiring_date.ToShortDateString();
                    dataRow["Работает"] = loader.Dismissal_date == default ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand ActiveOperators =>
            new MyCommand(obj =>
            {
                var activeOperators = from t in Operators
                                      where t.Dismissal_date == default
                                   select t;
                DataTable newTable = new DataTable("ActiveOperators");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм"};
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (_operator @operator in activeOperators)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = @operator.PN;
                    dataRow["ФИО"] = @operator.FullName;
                    dataRow["Паспорт"] = @operator.passport_ID;
                    dataRow["ДР"] = @operator.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = @operator.Hiring_date.ToShortDateString();
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand HiredOperators =>
            new MyCommand(obj =>
            {
                var hiredOperators = from t in Operators
                                     where t.Dismissal_date != default
                                   select t;
                DataTable newTable = new DataTable("HiredOperators");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм", "Работает" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (_operator @operator in hiredOperators)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = @operator.PN;
                    dataRow["ФИО"] = @operator.FullName;
                    dataRow["Паспорт"] = @operator.passport_ID;
                    dataRow["ДР"] = @operator.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = @operator.Hiring_date.ToShortDateString();
                    dataRow["Работает"] = @operator.Dismissal_date == default ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;
            });

        public ICommand ParamAcseesInfo =>
            new MyCommand(obj =>
            {
                
                ReportFilter IN = new ReportFilter { Context = "Номер входа" };
                IN.RegisterHandler(delegate (string content, object value)
                {
                    if (value is history)
                    {
                        history histor = value as history;
                        if (histor.Entrance_Num == Convert.ToInt32(content) || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilter PN = new ReportFilter { Context = "ТН оператора" };
                PN.RegisterHandler(delegate (string content, object value)
                {
                    if (value is history)
                    {
                        history histor = value as history;
                        if (histor.PN == Convert.ToInt32(content) || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilters.Add(IN);
                ReportFilters.Add(PN);

                var parametricWindow = new ParametricWindow();
                parametricWindow.DataContext = this;
                parametricWindow.ShowDialog();

                var paramAcseesInfo = from t in Histories
                                      where ReportFilters[0].GetResult(t) == true && ReportFilters[1].GetResult(t) == true
                                      select t;

                DataTable newTable = new DataTable("ParamAcseesInfo");
                List<string> names = new List<string> { "Вход", "ID действия", "ТН", "Дата", "Действие", "Работает" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (history @history in paramAcseesInfo)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["Вход"] = @history.Entrance_Num;
                    dataRow["ID действия"] = @history.ActionID;
                    dataRow["ТН"] = @history.PN;
                    dataRow["Дата"] = @history.Date;
                    dataRow["Действие"] = @history.Actions;
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;

                ReportFilters.Clear();
            });

        public ICommand ParamOperators=>
            new MyCommand(obj =>
            {

                ReportFilter PN = new ReportFilter { Context = "Табельный номер" };
                PN.RegisterHandler(delegate (string content, object value)
                {
                    if (value is _operator)
                    {
                        _operator histor = value as _operator;
                        if (histor.PN == Convert.ToInt32(content) || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilter Name = new ReportFilter { Context = "Имя оператора" };
                Name.RegisterHandler(delegate (string content, object value)
                {
                    if (value is _operator)
                    {
                        _operator histor = value as _operator;
                        if (histor.First_Name == content || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilter Passport = new ReportFilter { Context = "Паспорт" };
                Passport.RegisterHandler(delegate (string content, object value)
                {
                    if (value is _operator)
                    {
                        _operator histor = value as _operator;
                        if (histor.passport_ID == Convert.ToInt32(content) || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilter Login = new ReportFilter { Context = "Логин" };
                Login.RegisterHandler(delegate (string content, object value)
                {
                    if (value is _operator)
                    {
                        _operator histor = value as _operator;
                        if (histor.Login == content || string.IsNullOrWhiteSpace(content))
                            return true;
                    }
                    return false;
                });

                ReportFilters.Add(PN);
                ReportFilters.Add(Name);
                ReportFilters.Add(Passport);
                ReportFilters.Add(Login);

                var parametricWindow = new ParametricWindow();
                parametricWindow.DataContext = this;
                parametricWindow.ShowDialog();

                var ParamOperators = from t in Operators
                                      where ReportFilters[0].GetResult(t) == true && ReportFilters[1].GetResult(t) == true && ReportFilters[2].GetResult(t) == true && ReportFilters[3].GetResult(t) == true
                                      select t;

                DataTable newTable = new DataTable("ParamOperators");
                List<string> names = new List<string> { "ТН", "ФИО", "Паспорт", "ДР", "Найм", "Работает" };
                foreach (string name in names)
                    newTable.Columns.Add(new DataColumn(name));
                foreach (_operator @operator in ParamOperators)
                {
                    DataRow dataRow = newTable.NewRow();
                    dataRow["ТН"] = @operator.PN;
                    dataRow["ФИО"] = @operator.FullName;
                    dataRow["Паспорт"] = @operator.passport_ID;
                    dataRow["ДР"] = @operator.Date_of_Birth.ToShortDateString();
                    dataRow["Найм"] = @operator.Hiring_date.ToShortDateString();
                    dataRow["Работает"] = @operator.Dismissal_date == default ? "Да" : "Нет";
                    newTable.Rows.Add(dataRow);
                }
                CurrentTable.Dispose();
                CurrentTable = newTable;

                ReportFilters.Clear();
            });
    }
}
