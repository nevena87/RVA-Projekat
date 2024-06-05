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
    /// Interaction logic for IzmeniPodatkeKorisnika.xaml
    /// </summary>
    public partial class IzmeniPodatkeKorisnika : Window
    {
        public IProzorManager ProzorManager { get; set; }
        public IzmeniPodatkeKorisnika(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
            InitializeComponent();

            IzmeniPodatkeKorisnikaVM viewModel = new IzmeniPodatkeKorisnikaVM(ProzorManager);
            DataContext = viewModel;
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
