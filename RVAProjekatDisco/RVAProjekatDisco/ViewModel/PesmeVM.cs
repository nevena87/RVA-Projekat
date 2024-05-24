using Common.Model;
using RVAProjekatDisco.Komande;
using RVAProjekatDisco.KomunikacijaWCF;
using RVAProjekatDisco.View;
using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class PesmeVM : ProzorManagingVM
    {
        public ObservableCollection<Pesma> listaPesama { get; set; }
        public Pesma selektovanaPesma { get; set; }

        public ICommand DodajPesmuKomanda { get; set; }
        public ICommand ObrisiPesmuKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public PesmeVM(IProzorManager prozorManager) : base(prozorManager)
        {
            List<Pesma> listaPesamaIzBaze = KreirajKomunikaciju.Komunikacija.VratiPesme();
            listaPesama = new ObservableCollection<Pesma>(listaPesamaIzBaze);

            DodajPesmuKomanda = new RelayCommand(DodajPesmu);
            ObrisiPesmuKomanda = new RelayCommand(ObrisiPesmu, SelektovanaPesma);
            OtkaziKomanda = new KomandaOtkazi(this);
        }

        public Pesma SacuvajPesmu(Pesma pesma)
        {
            if (pesma == null)
            {
                pesma = new PesmaMP3();
            }

            DodajPesmuVM dodajPesmuVM = new DodajPesmuVM(pesma.Naziv, pesma.Autor, pesma.DuzinaMinute, pesma.DuzinaMinute, pesma.Format);
            DodajPesmu dodajPesmu = new DodajPesmu(dodajPesmuVM);
            dodajPesmu.ShowDialog();

            if (dodajPesmuVM.Sacuvano && ValidacijaPodataka(dodajPesmuVM.Naziv, dodajPesmuVM.Autor))
            {
                pesma.Naziv = dodajPesmuVM.Naziv;
                pesma.Autor = dodajPesmuVM.Autor;
                pesma.DuzinaMinute = dodajPesmuVM.DuzinaMinute;
                pesma.DuzinaSekunde = dodajPesmuVM.DuzinaSekunde;
                pesma.Format = dodajPesmuVM.FormatZapisa;

                pesma.IdPesme = KreirajKomunikaciju.Komunikacija.DodajPesmu(pesma);

                return pesma;
            }
            else
            {
                NevalidanUnos nevalidanUnos = new NevalidanUnos();
                nevalidanUnos.ShowDialog();
            }

            return null;
        }

        public void DodajPesmu()
        {
            Pesma novaPesma = SacuvajPesmu(null);

            if (novaPesma != null)
            {
                listaPesama.Add(novaPesma);
            }
        }

        public bool SelektovanaPesma()
        {
            return selektovanaPesma != null;
        }

        public void ObrisiPesmu()
        {
            KreirajKomunikaciju.Komunikacija.ObrisiPesmu(selektovanaPesma.IdPesme);
            listaPesama.Remove(selektovanaPesma);
        }

        public bool ValidacijaPodataka(string naziv, string autor)
        {
            Regex r = new Regex("^[a-zA-Z]*$");
            if (r.IsMatch(naziv) && r.IsMatch(autor))
            {
                return true;
            }
            return false;
        }
    }
}
