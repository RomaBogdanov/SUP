using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using SupRealClient.Models;

namespace SupRealClient.ViewModels
{
    class Base3ViewModel : Base1ViewModel
    {
        public ICommand Watch
        { get; set; }

        public override void SetModel(Base1ModelAbstr modelAbstr)
        {
            base.SetModel(modelAbstr);
            Base3ModelAbstr model = modelAbstr as Base3ModelAbstr;
            if (model == null)
            {
                throw new Exception(@"Данный метод может принимать только
                    классы-наследники от Base3ModelAbstr");
            }
            this.Watch = new RelayCommand(arg => model.Watch());
        }
    }
}
