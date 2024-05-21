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
    /// Interaction logic for Logovanje.xaml
    /// </summary>
    public partial class Logovanje : Window
    {
        LogovanjeVM viewModel;

        public Logovanje()
        {
            InitializeComponent();
            IProzorManager windowManager = new ProzorManager(this);

            viewModel = new LogovanjeVM(windowManager);
            base.DataContext = viewModel;
        }

        private void ExitDugme_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
