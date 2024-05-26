using Common.Dodatno;
using Common.Interfejsi;
using Common.Model;
using Common.ObjektiDTO;
using Server.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servisi
{
    public class KorisnikServis : IKorisnikServis
    {
        public void DodajKorisnika(Sesija sesija, Korisnik korisnik)
        {
            try
            {
                SesijaManager.Instance.AdministratorIzuzetak(sesija);
                DbManager.Instance.DodajKorisnika(korisnik);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void IzmeniInfoKorisnika(Sesija sesija, KorisnikDTO korisnik)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Korisnik ulogovaniKorisnik = SesijaManager.Instance.VratiKorisnika(sesija);

                ulogovaniKorisnik.Ime = korisnik.Ime;
                ulogovaniKorisnik.Prezime = korisnik.Prezime;

                DbManager.Instance.IzmeniKorisnika(ulogovaniKorisnik);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void PromeniLozinku(Sesija sesija, string lozinka)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Korisnik ulogovaniKorisnik = SesijaManager.Instance.VratiKorisnika(sesija);

                ulogovaniKorisnik.Lozinka = lozinka;
                DbManager.Instance.IzmeniKorisnika(ulogovaniKorisnik);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public KorisnikDTO DobaviInfoKorisnika(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Korisnik ulogovaniKorisnik = SesijaManager.Instance.VratiKorisnika(sesija);
                if (ulogovaniKorisnik == null)
                {
                    return null;
                }

                KorisnikDTO korisnikDTO = new KorisnikDTO()
                {
                    Ime = ulogovaniKorisnik.Ime,
                    Prezime = ulogovaniKorisnik.Prezime,
                    KorisnickoIme = ulogovaniKorisnik.KorisnickoIme,
                    Tip = ulogovaniKorisnik.Tip
                };
                return korisnikDTO;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }
    }
}
