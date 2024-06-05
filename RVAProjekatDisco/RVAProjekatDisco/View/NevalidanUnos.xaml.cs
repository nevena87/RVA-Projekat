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
    /// Interaction logic for NevalidanUnos.xaml
    /// </summary>
    public partial class NevalidanUnos : Window
    {
        public NevalidanUnos()
        {
            InitializeComponent();
        }

        private void NazadDugme_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
