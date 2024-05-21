using Common.Dodatno;
using Common.Model;
using log4net;
using RVAProjekatDisco.KomunikacijaWCF;
using RVAProjekatDisco.View;
using RVAProjekatDisco.WindowManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RVAProjekatDisco.ViewModel
{
    public class LogovanjeVM : ProzorManagingVM, INotifyPropertyChanged, ICommand
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogovanjeVM));

        public KorisnikZaLogovanje KorisnikZaLog { get; set; }
        public ICommand LogovanjeKomanda { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public String KorisnickoIme
        {
            get { return KorisnikZaLog.KorisnickoIme; }
            set
            {
                KorisnikZaLog.KorisnickoIme = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(KorisnickoIme)));
            }
        }

        public LogovanjeVM(IProzorManager prozorManager) : base(prozorManager)
        {
            LogovanjeKomanda = this;
            KorisnikZaLog = new KorisnikZaLogovanje();

            KorisnikZaLog.KorisnickoIme = "admin";
            KorisnikZaLog.Lozinka = "admin";

            Console.WriteLine("Korisnicko ime: {0} \n Lozinka: {1}", KorisnikZaLog.KorisnickoIme, KorisnikZaLog.Lozinka);
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                if (KreirajKomunikaciju.Komunikacija.KorisnikPostojiUBP(KorisnikZaLog))
                {
                    KreirajKomunikaciju.Komunikacija.PrijaviSe(KorisnikZaLog);
                    ProzorManager.PrikaziStranu(StanjeProzora.Pocetna);
                }
                else
                {
                    NevalidanUnos unos = new NevalidanUnos();
                    unos.ShowDialog();
                }

            }
            catch (FaultException<Izuzetak> izuzetak)
            {
                log.Error("Nastala je greska prilikom logovanja", izuzetak);
                Console.WriteLine(izuzetak.Detail.Poruka);
            }
        }
    }
}
