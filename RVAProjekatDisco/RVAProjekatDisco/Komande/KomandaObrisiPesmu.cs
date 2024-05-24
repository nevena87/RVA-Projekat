using Common.Model;
using RVAProjekatDisco.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.Komande
{
    public class KomandaObrisiPesmu : IUndoRedo
    {
        DodajPlejlistuVM dodajPlejlistuVM = null;
        Pesma pesmaZaBrisanje;
        int idPesme;

        public KomandaObrisiPesmu(DodajPlejlistuVM viewModel, Pesma pesmaZaBrisanje, int idPesme)
        {
            this.dodajPlejlistuVM = viewModel;
            this.pesmaZaBrisanje = pesmaZaBrisanje;
            this.idPesme = idPesme;
        }

        public void Izvrsi()
        {
            dodajPlejlistuVM.ListaPesamaIzTabele.RemoveAt(idPesme);
        }

        public void Vrati()
        {
            dodajPlejlistuVM.ListaPesamaIzTabele.Insert(idPesme, pesmaZaBrisanje);
        }
    }
}
