using Common.Enumeracije;
using Common.ObjektiDTO;
using RVAProjekatDisco.KomunikacijaWCF;
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
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Window
    {
        public Visibility DugmeDodajKorisnikaVidljivo { get; set; }

        public IProzorManager ProzorManager { get; private set; }

        public string ImePrezime { get; set; }

        public Pocetna(IProzorManager prozorManager)
        {
            KorisnikDTO korisnikDTO = TrenutnoUlogovan();
            ProzorManager = prozorManager;
            if (JaSamAdmin() && korisnikDTO != null)
            {
                ImePrezime = korisnikDTO.Ime + " " + korisnikDTO.Prezime;
                InitializeComponent();
                DataContext = this;
            }
        }

        private void DodajKorisnikaDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.DodajKorisnika);
        }

        private void OdjavaDugme_Click(object sender, RoutedEventArgs e)
        {
            KreirajKomunikaciju.Komunikacija.OdjaviSe();
            ProzorManager.PrethodnaStrana();
        }

        private void IzmeniPodatkeDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.IzmeniPodatkeKorisnika);
        }

        private bool JaSamAdmin()
        {
            if (KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika() == null)
                return false;

            if (KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika().Tip == TipKorisnika.Admin)
            {
                DugmeDodajKorisnikaVidljivo = Visibility.Visible;
            }
            else
            {
                DugmeDodajKorisnikaVidljivo = Visibility.Hidden;
            }
            return true;
        }

        private KorisnikDTO TrenutnoUlogovan()
        {
            return KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika();
        }

        private void PlejlisteDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.Plejliste);
        }

        private void PrikaziPesmeDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.Pesme);
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
