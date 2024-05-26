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
        private ChannelFactory<IDataServis> dataServisFactory;

        private ILogovanjeServis logovanjeServisProxy;
        private IKorisnikServis korisnikServisProxy;
        private IDataServis dataServisProxy;

        private Sesija sesija = null;

        public KomunikacijaWCF()
        {
            logovanjeServisFactory = new ChannelFactory<ILogovanjeServis>(typeof(ILogovanjeServis).ToString());
            korisnikServisFactory = new ChannelFactory<IKorisnikServis>(typeof(IKorisnikServis).ToString());
            dataServisFactory = new ChannelFactory<IDataServis>(typeof(IDataServis).ToString());

            logovanjeServisProxy = logovanjeServisFactory.CreateChannel();
            korisnikServisProxy = korisnikServisFactory.CreateChannel();
            dataServisProxy = dataServisFactory.CreateChannel();
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
            return korisnikServisProxy.DobaviInfoKorisnika(sesija);
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
        public List<Plejlista> VratiPlejliste()
        {
            log.Info("Vrati sve plejliste...");
            return dataServisProxy.VratiPlejliste(sesija);
        }

        public void ObrisiPlejlistu(int idPlejliste)
        {
            dataServisProxy.ObrisiPlejlistu(sesija, idPlejliste);
            log.Info("Brisanje plejliste sa id-em: " + idPlejliste);
        }

        public Plejlista KreirajPlejlistu(PlejlistaDTO plejlista)
        {
            log.Info("Kreiranje plejliste");
            Plejlista p = dataServisProxy.KreirajPlejlistu(sesija, plejlista);
            return p;
        }

        public bool IzmeniPlejlistu(PlejlistaIzmeniDTO plejlistaDTO)
        {
            log.Info("Podaci o plejlisti su izmenjeni");
            bool uspesno = dataServisProxy.IzmeniPlejlistu(sesija, plejlistaDTO);
            return uspesno;
        }

        public Plejlista DuplirajPlejlistu(int idPlejliste)
        {
            log.Info("Kloniranje plejliste sa id-em" + idPlejliste);
            return dataServisProxy.DuplirajPlejlistu(sesija, idPlejliste);
        }
        #endregion Plejliste

        #region Pesme
        public int DodajPesmu(Pesma pesma)
        {
            log.Info("Dodavanje pesme sa id-em: " + pesma.IdPesme);
            return dataServisProxy.DodajPesmu(sesija, pesma);
        }

        public Pesma VratiPesmu(int idPesme)
        {
            log.Info("Vrati informacije o pesmi sa id-em: " + idPesme);
            return dataServisProxy.VratiPesmu(sesija, idPesme);
        }

        public List<Pesma> VratiPesme()
        {
            log.Info("Vrati sve pesme...");
            return dataServisProxy.VratiPesme(sesija);
        }

        public void ObrisiPesmu(int idPesme)
        {
            dataServisProxy.ObrisiPesmu(sesija, idPesme);
            log.Info("Brisanje pesme sa id-em: " + idPesme);
        }
        #endregion Pesme
    }
}
