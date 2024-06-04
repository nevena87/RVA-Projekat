using Common.Model;
using Common.ObjektiDTO;
using log4net;
using RVAProjekatDisco.Komande;
using RVAProjekatDisco.KomunikacijaWCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class DodajPlejlistuVM
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DodajPlejlistuVM));

        //polja 
        public string NazivPlejliste { get; set; }
        public string AutorPlejliste { get; set; }

        public Plejlista TrenutnaPlejlista { get; set; }
        public ObservableCollection<Pesma> ListaPesama { get; set; }
        public ObservableCollection<Pesma> ListaPesamaIzTabele { get; set; }

        public Pesma selektovanaPesma { get; set; }
        public Pesma selektovanaPesmaIzTabele { get; set; }
        public int IdSelektovanePesme { get; set; }

        public ICommand DodajPesmuKomanda { get; set; }
        public ICommand ObrisiPesmuKomanda { get; set; }
        public ICommand DodajPlejlistuKomanda { get; set; }
        public ICommand UndoKomanda { get; set; }
        public ICommand RedoKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        CommandExecutor commandExecutor = new CommandExecutor();

        public Window Roditelj { get; set; }

        public DodajPlejlistuVM()
        {
            ListaPesamaIzTabele = new ObservableCollection<Pesma>();

            List<Pesma> pesmeIzBaze = KreirajKomunikaciju.Komunikacija.VratiPesme();

            ListaPesama = new ObservableCollection<Pesma>(pesmeIzBaze);

            selektovanaPesma = ListaPesama.FirstOrDefault();

            DodajPesmuKomanda = new RelayCommand(DodajPesmuUTabelu);
            ObrisiPesmuKomanda = new RelayCommand(ObrisiPesmu, SelektovanaPesma);
            DodajPlejlistuKomanda = new RelayCommand(SacuvajPlejlistu, ValidacijaSacuvajPlejlistu);

            UndoKomanda = new RelayCommand(commandExecutor.Undo, commandExecutor.ValidacijaUndo);
            RedoKomanda = new RelayCommand(commandExecutor.Redo, commandExecutor.ValidacijaRedo);

            OtkaziKomanda = new RelayCommand(ZatvoriProzor);
        }

        public DodajPlejlistuVM(Plejlista trenutnaPlejlista) : this()
        {
            TrenutnaPlejlista = trenutnaPlejlista;

            ListaPesamaIzTabele = new ObservableCollection<Pesma>(trenutnaPlejlista.ListaPesama);

            NazivPlejliste = TrenutnaPlejlista.Naziv;

            AutorPlejliste = TrenutnaPlejlista.Autor;
        }

        public void DodajPesmuUTabelu()
        {
            IUndoRedo komandaDodajPesmu = new KomandaDodajPesmu(this,
                selektovanaPesma.Naziv, selektovanaPesma.Autor, selektovanaPesma.DuzinaMinute, selektovanaPesma.DuzinaSekunde, selektovanaPesma.Format);
            commandExecutor.DodajIIzvrsi(komandaDodajPesmu);
        }

        public void ObrisiPesmu()
        {
            IUndoRedo obrisiPesmu = new KomandaObrisiPesmu(this, selektovanaPesmaIzTabele, IdSelektovanePesme);
            commandExecutor.DodajIIzvrsi(obrisiPesmu);
        }

        public bool SelektovanaPesma()
        {
            return selektovanaPesmaIzTabele != null;
        }

        public void SacuvajPlejlistu()
        {
            //bool prvoPravljenje = false;
            if (TrenutnaPlejlista == null)
            {
                PlejlistaDTO kreiranaPlejlista = new PlejlistaDTO()
                {
                    Naziv = NazivPlejliste,
                    Autor = AutorPlejliste
                };
                TrenutnaPlejlista = KreirajKomunikaciju.Komunikacija.KreirajPlejlistu(kreiranaPlejlista);
                //prvoPravljenje = true;
            }

            PlejlistaIzmeniDTO izmeniPlejlistuDTO = new PlejlistaIzmeniDTO()
            {
                NoviNaziv = NazivPlejliste,
                IdPlejliste = TrenutnaPlejlista.IdPlejliste,
                NoviAutor = AutorPlejliste,
                NovaListaPesama = ListaPesamaIzTabele.ToList(),
                Verzija = TrenutnaPlejlista.Verzija
            };

            bool uspesnoIzmenjen = KreirajKomunikaciju.Komunikacija.IzmeniPlejlistu(izmeniPlejlistuDTO);

            if (!uspesnoIzmenjen /*&& !prvoPravljenje*/)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Plejlista je vec izmenjena od strane drugog korisnika. Da li zelite pregaziti tudje izmene", "Pregazi izmene",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (dialogResult)
                {
                    case MessageBoxResult.Yes:
                        log.Warn("Pregazi tudje izmene");
                        izmeniPlejlistuDTO.Azurirano = true;
                        uspesnoIzmenjen = KreirajKomunikaciju.Komunikacija.IzmeniPlejlistu(izmeniPlejlistuDTO);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            Roditelj.Close();
        }

        public bool ValidacijaSacuvajPlejlistu()
        {
            bool uspesno = (NazivPlejliste != null && NazivPlejliste.Length > 0 &&
                AutorPlejliste != null && NazivPlejliste.Length > 0 &&
                ListaPesamaIzTabele != null && ListaPesamaIzTabele.Count > 0) ? true : false;

            return uspesno;
        }

        public void ZatvoriProzor()
        {
            Roditelj.Close();
        }
    }
}
