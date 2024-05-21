using Common.Dodatno;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.PristupBaziPodataka
{
    public class DbManager
    {
        private static DbManager instance = null;

        public DiscoContext discoContext = null;

        private DbManager()
        {
            discoContext = new DiscoContext();
        }

        public static DbManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbManager();
                }
                return instance;
            }
        }

        public void SacuvajPromene()
        {
            lock (discoContext)
            {
                discoContext.SaveChanges();
            }
        }

        public Korisnik GetUserByUsername(string korisnickoIme)
        {
            lock (discoContext)
            {
                return discoContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);
            }
        }

        public Korisnik ProveriLozinkaIspravna(KorisnikZaLogovanje korisnik)
        {
            lock (discoContext)
            {
                return discoContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnik.KorisnickoIme && k.Lozinka == korisnik.Lozinka);
            }
        }

        public void DodajKorisnika(Korisnik korisnik)
        {
            lock (discoContext)
            {
                bool nadjeno = false;
                List<Korisnik> listaKorisnika = discoContext.Korisnici.ToList();
                foreach (var item in listaKorisnika)
                {
                    if (item.KorisnickoIme == korisnik.KorisnickoIme)
                    {
                        nadjeno = true;
                        break;
                    }
                }

                if (nadjeno == false)
                {
                    Korisnik korisnikPostoji = discoContext.Korisnici.Find(korisnik.IdKorisnika);

                    discoContext.Korisnici.Add(korisnik);
                    discoContext.SaveChanges();

                }
                else
                {
                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Korisnik vec postoji u bazi.";
                    throw new FaultException<Izuzetak>(ex);
                }
            }
        }

        public void IzmeniKorisnika(Korisnik korisnik)
        {
            lock (discoContext)
            {
                Korisnik korisnikIzBaze = discoContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnik.KorisnickoIme);
                korisnikIzBaze.Ime = korisnik.Ime;
                korisnikIzBaze.Prezime = korisnik.Prezime;
                korisnikIzBaze.Lozinka = korisnik.Lozinka;
                korisnikIzBaze.Tip = korisnik.Tip;
                discoContext.SaveChanges();
            }
        }
    }
}
