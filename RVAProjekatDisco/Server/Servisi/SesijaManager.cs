using Common.Dodatno;
using Common.Enumeracije;
using Common.Model;
using log4net;
using Server.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servisi
{
    public class SesijaManager
    {
        private static SesijaManager instance = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(SesijaManager));

        Dictionary<string, SessionInstance> sesije = new Dictionary<string, SessionInstance>();

        // Singleton pattern
        public static SesijaManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SesijaManager();
                }
                return instance;
            }
        }

        private SesijaManager() { }

        public Sesija NapraviNovuSesiju(Korisnik korisnikSesije)
        {
            string idSesije = null;
            do
            {
                idSesije = Guid.NewGuid().ToString("n");
            }
            while (sesije.ContainsKey(idSesije));

            Sesija sesija = new Sesija()
            {
                IdSesije = idSesije
            };

            SessionInstance sessionInstance = new SessionInstance()
            {
                KorisnikSesije = korisnikSesije,
                SesijaCookie = sesija
            };

            sesije.Add(idSesije, sessionInstance);

            return sesija;
        }

        public bool ObrisiSesiju(Sesija sesija)
        {
            return sesije.Remove(sesija.IdSesije);
        }

        public bool ProveraAutentifikacije(Sesija sesija)
        {
            if (sesija == null)
            {
                return false;
            }
            try
            {
                if (sesije.ContainsKey(sesija.IdSesije))
                {
                    return true;
                }
                else
                {
                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Ne postoji sesija!.";
                    throw new FaultException<Izuzetak>(ex);
                }
            }
            catch (FaultException<NullReferenceException> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail);
                return false;
            }
        }

        public bool ProveriAdministratora(Sesija sesija)
        {
            SessionInstance instance = null;
            if (sesije.TryGetValue(sesija.IdSesije, out instance))
            {
                return instance.KorisnikSesije.Tip == TipKorisnika.Admin;
            }
            return false;
        }

        public Korisnik VratiKorisnika(Sesija sesija)
        {
            if (sesija == null)
            {
                return null;
            }

            SessionInstance sessionInstance = sesije[sesija.IdSesije];
            return DbManager.Instance.GetUserByUsername(sessionInstance.KorisnikSesije.KorisnickoIme);
        }

        public void AutentifikacijaIzuzetak(Sesija sesija)
        {
            try
            {
                if (!ProveraAutentifikacije(sesija))
                {
                    log.Warn("Korisnik nije autentifikovan!");

                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Korisnik nije autentifikovan!";
                    throw new FaultException<Izuzetak>(ex);
                }
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void AdministratorIzuzetak(Sesija sesija)
        {
            if (!ProveriAdministratora(sesija))
            {
                log.Warn("Korisnik nije administrator");

                Izuzetak ex = new Izuzetak();
                ex.Poruka = "Korisnik nije administrator!";
                throw new FaultException<Izuzetak>(ex);
            }
        }

        public bool PostojiUBazi(KorisnikZaLogovanje korisnik)
        {
            if (DbManager.Instance.ProveriLozinkaIspravna(korisnik) == null)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
    }
}
