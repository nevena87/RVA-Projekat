using Common.Dodatno;
using Common.Interfejsi;
using Common.Model;
using Common.ObjektiDTO;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.KomunikacijaWCF
{
    public class KomunikacijaWCF
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KomunikacijaWCF));

        private ChannelFactory<ILogovanjeServis> logovanjeServisFactory;
        private ChannelFactory<IKorisnikServis> korisnikServisFactory;

        private ILogovanjeServis logovanjeServisProxy;
        private IKorisnikServis korisnikServisProxy;

        private Sesija sesija = null;

        public KomunikacijaWCF()
        {
            logovanjeServisFactory = new ChannelFactory<ILogovanjeServis>(typeof(ILogovanjeServis).ToString());
            korisnikServisFactory = new ChannelFactory<IKorisnikServis>(typeof(IKorisnikServis).ToString());

            logovanjeServisProxy = logovanjeServisFactory.CreateChannel();
            korisnikServisProxy = korisnikServisFactory.CreateChannel();
        }

        #region Prijava
        public void PrijaviSe(KorisnikZaLogovanje korisnik)
        {
            try
            {
                sesija = logovanjeServisProxy.PrijaviSe(korisnik);
                log.Info("Prijava uspjesno izvrsena!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void OdjaviSe()
        {
            logovanjeServisProxy.OdjaviSe(sesija);
            log.Info("Odjava uspjesno izvrsena!");
        }
        #endregion Prijava

        #region Korisnik
        public KorisnikDTO VratiInfoKorisnika()
        {
            if (sesija == null)
            {
                return null;
            }
            log.Info("Informacije o prijavljenom korisnika");
            return korisnikServisProxy.VratiInfoKorisnika(sesija);
        }

        public void IzmeniInfoKorisnika(KorisnikDTO korisnik)
        {
            korisnikServisProxy.IzmeniInfoKorisnika(sesija, korisnik);
            log.Info("Informacije korisnika su izmijenjene!");
        }

        public void DodajKorisnika(Korisnik korisnik)
        {
            log.Info("Dodavanje novog korisnika: " + korisnik.KorisnickoIme);
            korisnikServisProxy.DodajKorisnika(sesija, korisnik);
        }

        public bool KorisnikPostojiUBP(KorisnikZaLogovanje korisnik)
        {
            return logovanjeServisProxy.PostojiUBaziKorisnik(korisnik);
        }
        #endregion Korisnik

        #region Plejliste
        // TODO
        #endregion Plejliste

        #region Pesme
        // TODO
        #endregion Pesme
    }
}
