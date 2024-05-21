using Common.ObjektiDTO;
using RVAProjekatDisco.Komande;
using RVAProjekatDisco.KomunikacijaWCF;
using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class IzmeniPodatkeKorisnikaVM : ProzorManagingVM, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public KorisnikDTO InformacijeKorisnika { get; set; }
        public ICommand OtkaziKomanda { get; set; }
        public ICommand SacuvajKomanda { get; set; }

        public String Ime
        {
            get
            {
                return InformacijeKorisnika.Ime;
            }
            set
            {
                InformacijeKorisnika.Ime = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Ime)));
            }
        }

        public String Prezime
        {
            get
            {
                return InformacijeKorisnika.Prezime;
            }
            set
            {
                InformacijeKorisnika.Prezime = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Prezime)));
            }
        }

        public IzmeniPodatkeKorisnikaVM(IProzorManager prozorManager) : base(prozorManager)
        {
            InformacijeKorisnika = KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika();
            OtkaziKomanda = new KomandaOtkazi(this);
            SacuvajKomanda = new RelayCommand(SacuvajPodatkeOKorisniku, ValidacijaSacuvajPodatkeKorisnika);
        }

        public void SacuvajPodatkeOKorisniku()
        {
            KreirajKomunikaciju.Komunikacija.IzmeniInfoKorisnika(InformacijeKorisnika);
            ProzorManager.PrethodnaStrana();
        }

        public bool ValidacijaSacuvajPodatkeKorisnika()
        {
            if (InformacijeKorisnika.Ime.Length > 0 && InformacijeKorisnika.Prezime.Length > 0)
                return true;
            else
                return false;
        }
    }
}
