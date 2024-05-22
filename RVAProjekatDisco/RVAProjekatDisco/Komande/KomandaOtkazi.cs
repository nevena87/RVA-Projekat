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
        ProzorManagingVM ViewModel { get; set; }

        public KomandaOtkazi(ProzorManagingVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

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
