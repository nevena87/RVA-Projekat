using Common.Enumeracije;
using Common.Model;
using RVAProjekatDisco.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.Komande
{
    public class KomandaDodajPesmu : IUndoRedo
    {
        DodajPlejlistuVM dodajPlejlistuVM;
        Pesma novaPesma = null;
        string Naziv;
        string Autor;
        int DuzinaMinute;
        int DuzinaSekunde;
        FormatZapisa FormatZapisa;

        public KomandaDodajPesmu(DodajPlejlistuVM viewModel, string naziv, string autor, int duzinaMinute,
            int duzinaSekunde, FormatZapisa formatZapisa)
        {
            this.dodajPlejlistuVM = viewModel;
            this.Naziv = naziv;
            this.Autor = autor;
            this.DuzinaMinute = duzinaMinute;
            this.DuzinaSekunde = duzinaSekunde;
            this.FormatZapisa = formatZapisa;
        }

        public void Izvrsi()
        {
            PesmaFactory fabrika = new PesmaFactory();
            novaPesma = fabrika.NapraviPesmu(this.FormatZapisa);

            novaPesma.Naziv = Naziv;
            novaPesma.Autor = Autor;
            novaPesma.DuzinaMinute = DuzinaMinute;
            novaPesma.DuzinaSekunde = DuzinaSekunde;
            novaPesma.Format = FormatZapisa;

            dodajPlejlistuVM.ListaPesamaIzTabele.Add(novaPesma);
        }

        public void Vrati()
        {
            dodajPlejlistuVM.ListaPesamaIzTabele.Remove(novaPesma);
        }
    }
}
