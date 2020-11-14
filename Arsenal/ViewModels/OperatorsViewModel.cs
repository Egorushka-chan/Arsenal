using Arsenal.Models;
using Arsenal.Models.DataBase;
using Equipment_accounting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Arsenal.ViewModels
{
    class OperatorsViewModel : BaseVM
    {
        public _operator CurrentOperator { get; set; }
        public _operator SelectedOperator { get; set; }

        public void Initialize()
        {
            foreach(_operator _Operator in Operators)
            {
                if (_Operator.PN == EnterWindow.OperatorID)
                {
                    CurrentOperator = _Operator;
                }
                    
            }

            SelectedOperator = CurrentOperator;
        }

        public ICommand OpenAddOperator
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
