using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.Komande
{
    public class KomandaOtkazi : ICommand
    {
        public KomandaOtkazi(ProzorManagingVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;
        ProzorManagingVM ViewModel { get; set; }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.ProzorManager.PrethodnaStrana();
        }
    }
}
