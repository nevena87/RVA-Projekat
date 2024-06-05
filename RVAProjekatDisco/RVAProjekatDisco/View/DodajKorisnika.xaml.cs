using RVAProjekatDisco.ViewModel;
using RVAProjekatDisco.WindowManager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RVAProjekatDisco.View
{
    /// <summary>
    /// Interaction logic for DodajKorisnika.xaml
    /// </summary>
    public partial class DodajKorisnika : Window
    {
        private DodajKorisnikaVM viewModel;

        public IProzorManager ProzorManager { get; set; }

        public DodajKorisnika(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
            InitializeComponent();

            viewModel = new DodajKorisnikaVM(prozorManager);
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                viewModel.NoviKorisnik.Lozinka = passwordBox.Password;
            }
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
