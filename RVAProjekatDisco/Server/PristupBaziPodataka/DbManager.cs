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

        // Singleton pattern
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

        #region Korisnici
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
        #endregion Korisnici

        #region Pesme
        public Pesma DodajPesmu(Pesma pesma)
        {
            Pesma povratnaVrednost = null;

            lock (discoContext)
            {
                povratnaVrednost = discoContext.Pesme.Add(pesma);
                discoContext.SaveChanges();
            }
            return povratnaVrednost;
        }
        public Pesma VratiPesmuPrekoId(int idPesme)
        {
            lock (discoContext)
            {
                return discoContext.Pesme.Find(idPesme);
            }
        }

        public List<Pesma> VratiSvePesme()
        {
            lock (discoContext)
            {
                return discoContext.Pesme.ToList();
            }
        }

        public void ObrisiPesmu(int idPesme)
        {
            lock (discoContext)
            {
                Pesma pesma = discoContext.Pesme.Find(idPesme);
                discoContext.Pesme.Remove(pesma);
                discoContext.SaveChanges();
            }
        }
        #endregion Pesme

        #region Plejliste
        public Plejlista DodajPlejlistu(Plejlista plejlista)
        {
            Plejlista povratnaVrednost = null;
            lock (discoContext)
            {
                povratnaVrednost = discoContext.Plejliste.Add(plejlista);
                discoContext.SaveChanges();
            }
            return povratnaVrednost;
        }
        
        public List<Plejlista> VratiSvePlejliste()
        {
            lock (discoContext)
            {
                return discoContext.Plejliste.ToList();
            }
        }

        public void DodajPesmuUPlejlistu(int idPlejliste, Pesma pesma)
        {
            lock (discoContext)
            {
                bool nadjen = false;

                Plejlista plejlista = discoContext.Plejliste.Find(idPlejliste);
                foreach (var item in plejlista.ListaPesama)
                {
                    if (item.Equals(pesma))
                    {
                        nadjen = true;
                        break;
                    }
                }
                if (!nadjen)
                {
                    plejlista.ListaPesama.Add(pesma);
                    discoContext.SaveChanges();
                }
            }
        }

        // Brise sve pesme iz plejliste
        public void ObrisiPesmuIzPlejliste(int idPlejliste)
        {
            lock (discoContext)
            {
                Plejlista plejlista = discoContext.Plejliste.Find(idPlejliste);
                discoContext.Pesme.RemoveRange(plejlista.ListaPesama);
                discoContext.SaveChanges();
            }
        }

        public void ObrisiPlejlistu(int idPlejliste)
        {
            lock (discoContext)
            {
                Plejlista plejlista = discoContext.Plejliste.Find(idPlejliste);

                ObrisiPesmuIzPlejliste(idPlejliste);
                SacuvajPromene();

                discoContext.Plejliste.Remove(plejlista);
                discoContext.SaveChanges();
            }
        }

        public Plejlista VratiPlejlistu(int idPlejliste)
        {
            lock (discoContext)
            {
                return discoContext.Plejliste.Find(idPlejliste);
            }
        }
        #endregion Plejliste
    }
}
