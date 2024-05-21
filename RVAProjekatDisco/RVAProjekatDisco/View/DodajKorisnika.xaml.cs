using RVAProjekatDisco.ViewModel;
using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RVAProjekatDisco.View
{
    /// <summary>
    /// Interaction logic for DodajKorisnika.xaml
    /// </summary>
    public partial class DodajKorisnika : Window
    {
        public IProzorManager ProzorManager { get; set; }

        public DodajKorisnika(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
            InitializeComponent();

            DodajKorisnikaVM viewModel = new DodajKorisnikaVM(prozorManager);
            DataContext = viewModel;
        }
    }
}
