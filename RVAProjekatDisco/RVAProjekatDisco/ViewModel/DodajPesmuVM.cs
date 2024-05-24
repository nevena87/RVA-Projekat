using Common.Enumeracije;
using RVAProjekatDisco.Komande;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class DodajPesmuVM
    {
        public string Naziv { get; set; }
        public string Autor { get; set; }
        public int DuzinaMinute { get; set; }
        public int DuzinaSekunde { get; set; }

        public IEnumerable<FormatZapisa> VrstaFormataZapisa
        {
            get
            {
                return Enum.GetValues(typeof(FormatZapisa)).Cast<FormatZapisa>();
            }
        }
        public FormatZapisa FormatZapisa { get; set; }

        public bool Sacuvano { get; set; }

        public ICommand DodavanjePesmeKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public Window Roditelj { get; set; }

        public DodajPesmuVM()
        {
            Sacuvano = false;
            OtkaziKomanda = new RelayCommand(ZatvoriProzor);
            DodavanjePesmeKomanda = new RelayCommand(PesmaDodata, ValidacijaPesama);
        }

        public DodajPesmuVM(string naziv, string autor, int duzinaMinute, int duzinaSekunde, FormatZapisa formatZapisa) : this()
        {
            Naziv = naziv;
            Autor = autor;
            DuzinaMinute = duzinaMinute;
            DuzinaSekunde = duzinaSekunde;
            FormatZapisa = formatZapisa;
        }

        public void ZatvoriProzor()
        {
            Roditelj.Close();
        }

        public bool ValidacijaPesama()
        {
            return Naziv != null && Naziv.Length > 0 &&
                    Autor != null && Autor.Length > 0 &&
                    DuzinaMinute.ToString() != null && DuzinaMinute.ToString().Length > 0 &&
                    DuzinaSekunde.ToString() != null && DuzinaSekunde.ToString().Length > 0 &&
                    FormatZapisa.ToString() != null && FormatZapisa.ToString().Length > 0;
        }
        public void PesmaDodata()
        {
            Sacuvano = true;
            Roditelj.Close();
        }
    }
}
