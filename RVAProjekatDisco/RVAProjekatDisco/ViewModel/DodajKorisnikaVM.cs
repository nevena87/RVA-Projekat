using Common.Enumeracije;
using Common.Model;
using RVAProjekatDisco.Komande;
using RVAProjekatDisco.KomunikacijaWCF;
using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class DodajKorisnikaVM : ProzorManagingVM
    {
        public Korisnik NoviKorisnik { get; set; }
        public ICommand DodajKorisnikaKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public IEnumerable<TipKorisnika> TipKorisnika
        {
            get
            {
                return Enum.GetValues(typeof(TipKorisnika)).Cast<TipKorisnika>();
            }
        }

        public DodajKorisnikaVM(IProzorManager prozorManager) : base(prozorManager)
        {
            NoviKorisnik = new Korisnik();
            NoviKorisnik.Tip = Common.Enumeracije.TipKorisnika.Korisnik;
            OtkaziKomanda = new KomandaOtkazi(this);
            DodajKorisnikaKomanda = new RelayCommand(DodajKorisnika, ValidacijaDodajKorisnika);
        }

        public void DodajKorisnika()
        {
            KreirajKomunikaciju.Komunikacija.DodajKorisnika(NoviKorisnik);
            ProzorManager.PrethodnaStrana();
        }

        public bool ValidacijaDodajKorisnika()
        {
            if (NoviKorisnik.Ime != null && NoviKorisnik.Ime.Length > 0 &&
                NoviKorisnik.Prezime != null && NoviKorisnik.Prezime.Length > 0 &&
                NoviKorisnik.KorisnickoIme != null && NoviKorisnik.KorisnickoIme.Length > 0 &&
                NoviKorisnik.Lozinka != null && NoviKorisnik.Lozinka.Length > 0)
                return true;
            else
                return false;
        }
    }
}
