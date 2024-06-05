using RVAProjekatDisco.ViewModel;
using RVAProjekatDisco.WindowManager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RVAProjekatDisco.View
{
    public partial class Logovanje : Window
    {
        private LogovanjeVM viewModel;

        public Logovanje()
        {
            InitializeComponent();
            IProzorManager windowManager = new ProzorManager(this);
            viewModel = new LogovanjeVM(windowManager);
            base.DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                viewModel.Lozinka = passwordBox.Password;
            }
        }

        private void ExitDugme_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
