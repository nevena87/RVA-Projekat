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
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class PlejlisteVM : ProzorManagingVM
    {
        public ObservableCollection<Plejlista> Plejliste { get; set; }

        public Plejlista SelektovanaPlejlista { get; set; }

        public ICommand OtkaziKomanda { get; set; }
        public ICommand DodajPlejlistuKomanda { get; set; }
        public ICommand IzmeniPlejlistuKomanda { get; set; }
        public ICommand ObrisiPlejlistuKomanda { get; set; }
        public ICommand KlonirajPlejlistuKomanda { get; set; }
        public ICommand PretraziPlejlistuKomanda { get; set; }

        public IEnumerable<KriterijumiPretrage> TipoviPretrage
        {
            get
            {
                return Enum.GetValues(typeof(KriterijumiPretrage)).Cast<KriterijumiPretrage>();
            }
        }

        public KriterijumiPretrage TipoviPretrageEnum { get; set; } = KriterijumiPretrage.Naziv;

        public string KriterijumPretrageTekst { get; set; }

        List<Plejlista> listaPlejlistaIzBaze;

        public PlejlisteVM(IProzorManager prozorManager) : base(prozorManager)
        {
            listaPlejlistaIzBaze = KreirajKomunikaciju.Komunikacija.VratiPlejliste();
            Plejliste = new ObservableCollection<Plejlista>(listaPlejlistaIzBaze);

            OtkaziKomanda = new KomandaOtkazi(this);
            DodajPlejlistuKomanda = new RelayCommand(DodajPlejlistu);
            IzmeniPlejlistuKomanda = new RelayCommand(IzmeniPlejlistu, IzabranaPlejlista);
            KlonirajPlejlistuKomanda = new RelayCommand(KlonirajPlejlistu, IzabranaPlejlista);
            ObrisiPlejlistuKomanda = new RelayCommand(ObrisiPlejlistu, IzabranaPlejlista);
            PretraziPlejlistuKomanda = new RelayCommand(PretraziPlejlistu);

            PretraziPlejlistu();
        }

        public bool IzabranaPlejlista()
        {
            bool selektovano = (SelektovanaPlejlista != null) ? true : false;
            return selektovano;
        }

        public void DodajPlejlistu()
        {
            DodajPlejlistuVM sacuvajPlejlistuVM = new DodajPlejlistuVM();
            DodajPlejlistu sacuvajPlejlistu = new DodajPlejlistu(sacuvajPlejlistuVM);
            sacuvajPlejlistu.ShowDialog();

            ResetujListuPlejlista();
        }

        public void IzmeniPlejlistu()
        {
            DodajPlejlistuVM sacuvajPlejlistuVM = new DodajPlejlistuVM(SelektovanaPlejlista);
            DodajPlejlistu sacuvajPlejlistu = new DodajPlejlistu(sacuvajPlejlistuVM);
            sacuvajPlejlistu.ShowDialog();

            ResetujListuPlejlista();
        }

        public void KlonirajPlejlistu()
        {
            KreirajKomunikaciju.Komunikacija.DuplirajPlejlistu(SelektovanaPlejlista.IdPlejliste);

            ResetujListuPlejlista();
        }

        public void ObrisiPlejlistu()
        {
            KreirajKomunikaciju.Komunikacija.ObrisiPlejlistu(SelektovanaPlejlista.IdPlejliste);

            ResetujListuPlejlista();
        }

        public void PretraziPlejlistu()
        {
            IEnumerable<Plejlista> pretrazenePlejliste = null;

            if (KriterijumPretrageTekst == null || KriterijumPretrageTekst.Trim().Length == 0)
            {
                pretrazenePlejliste = listaPlejlistaIzBaze;
            }
            else
            {
                switch (TipoviPretrageEnum)
                {
                    case KriterijumiPretrage.IdPlejliste:
                        pretrazenePlejliste = listaPlejlistaIzBaze.Where(plejlista => plejlista.IdPlejliste.ToString().Equals(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.Naziv:
                        pretrazenePlejliste = listaPlejlistaIzBaze.Where(plejlista => plejlista.Naziv.ToString().Contains(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.Autor:
                        pretrazenePlejliste = listaPlejlistaIzBaze.Where(plejlista => plejlista.Autor.ToString().Contains(KriterijumPretrageTekst));
                        break;
                }
            }
            Plejliste.Clear();
            foreach (var item in pretrazenePlejliste)
            {
                Plejliste.Add(item);
            }
        }

        public void ResetujListuPlejlista()
        {
            bool novaListaIzmenjena = PostaviNoveVrednostiUServer();
            if (novaListaIzmenjena)
            {
                Console.WriteLine("Lista resetovana!!!");
                PretraziPlejlistu();
            }
        }

        private bool PostaviNoveVrednostiUServer()
        {
            List<Plejlista> novaListaPlejlista = KreirajKomunikaciju.Komunikacija.VratiPlejliste();
            bool izmenjeno = false;

            izmenjeno = novaListaPlejlista.Count != listaPlejlistaIzBaze.Count;

            if (!izmenjeno)
            {
                for (int i = 0; i < novaListaPlejlista.Count; i++)
                {
                    if (novaListaPlejlista[i].IdPlejliste != listaPlejlistaIzBaze[i].IdPlejliste ||
                        novaListaPlejlista[i].Verzija != listaPlejlistaIzBaze[i].Verzija)
                    {
                        izmenjeno = true;
                        break;
                    }
                }
            }

            if (izmenjeno)
            {
                listaPlejlistaIzBaze = novaListaPlejlista;
                return true;
            }

            return false;
        }
    }

    public enum KriterijumiPretrage
    {
        IdPlejliste, Naziv, Autor
    }
}
