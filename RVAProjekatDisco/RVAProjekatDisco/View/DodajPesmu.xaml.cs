using RVAProjekatDisco.ViewModel;
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
    /// Interaction logic for DodajPesmu.xaml
    /// </summary>
    public partial class DodajPesmu : Window
    {
        public DodajPesmu(DodajPesmuVM viewModel)
        {
            InitializeComponent();
            viewModel.Roditelj = this;
            DataContext = viewModel;
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
