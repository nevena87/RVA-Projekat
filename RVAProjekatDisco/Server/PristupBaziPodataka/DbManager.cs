using Common.Dodatno;
using Common.Model;
using Common.ObjektiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

/*
Korisnici: dodavanje, izmena imena i prezimena
Plejliste: dodavanje, izmena, brisanje, dupliranje
Pesme: dodavanje, izmena, brisanje (+dupliranje)
*/

namespace Server.PristupBaziPodataka
{
    public class DbManager
    {
        private static DbManager instance = null;
        public DiscoContext discoContext = null;

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

        private DbManager()
        {
            discoContext = new DiscoContext();
        }

        // Obrisati ???
        public void SacuvajPromene()
        {
            lock (discoContext)
            {
                discoContext.SaveChanges();
            }
        }

        #region Korisnici
        public Korisnik DobaviKorisnikaPoKorisnickomImenu(string korisnickoIme)
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
                // Provera 1: za isti username
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
                    // Provera 2: za isti ID, ali nigde nema neko grananje?
                    Korisnik korisnikPostoji = discoContext.Korisnici.Find(korisnik.IdKorisnika);

                    discoContext.Korisnici.Add(korisnik);
                    discoContext.SaveChanges();
                }
                else
                {
                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Korisnik sa tim korisnicim imenom vec postoji u bazi.";
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

        #region Plejliste
        public Plejlista DodajPlejlistu(Plejlista plejlista)
        {
            Plejlista retVal = null;
            lock (discoContext)
            {
                retVal = discoContext.Plejliste.Add(plejlista);
                discoContext.SaveChanges();
            }
            return retVal;
        }

        public List<Plejlista> DobaviSvePlejliste()
        {
            lock (discoContext)
            {
                return discoContext.Plejliste.ToList();
            }
        }

        public Plejlista DobaviPlejlistu(int idPlejliste)
        {
            lock (discoContext)
            {
                return discoContext.Plejliste.Find(idPlejliste);
            }
        }

        public void IzmeniPlejlistu(Plejlista plejlista)
        {
            lock (discoContext)
            {
                Plejlista plejlistaIzBaze = discoContext.Plejliste.Find(plejlista.IdPlejliste);
                if (plejlistaIzBaze != null)
                {
                    plejlistaIzBaze.Naziv = plejlista.Naziv;
                    plejlistaIzBaze.Autor = plejlista.Autor;
                    plejlistaIzBaze.ListaPesama = plejlista.ListaPesama;
                    discoContext.SaveChanges();
                }                
            }
        }

        public void ObrisiPlejlistu(int idPlejliste)
        {
            lock (discoContext)
            {
                Plejlista plejlista = discoContext.Plejliste.Find(idPlejliste);
                if (plejlista != null)
                {
                    ObrisiSvePesmeIzPlejliste(idPlejliste);
                    discoContext.SaveChanges();

                    discoContext.Plejliste.Remove(plejlista);
                    discoContext.SaveChanges();
                }
            }
        }
        #endregion Plejliste

        #region Pesme
        public Pesma DodajPesmu(Pesma pesma)
        {
            Pesma retVal = null;
            lock (discoContext)
            {
                retVal = discoContext.Pesme.Add(pesma);
                discoContext.SaveChanges();
            }
            return retVal;
        }

        public Pesma DobaviPesmu(int idPesme)
        {
            lock (discoContext)
            {
                return discoContext.Pesme.Find(idPesme);
            }
        }

        public List<Pesma> DobaviSvePesme()
        {
            lock (discoContext)
            {
                return discoContext.Pesme.ToList();
            }
        }

        public void IzmeniPesmu(Pesma pesma)
        {
            lock (discoContext)
            {
                Pesma pesmaIzBaze = discoContext.Pesme.Find(pesma.IdPesme);
                if (pesmaIzBaze != null)
                {
                    pesmaIzBaze.Naziv = pesma.Naziv;
                    pesmaIzBaze.Autor = pesma.Autor;
                    pesmaIzBaze.DuzinaMinute = pesma.DuzinaMinute;
                    pesmaIzBaze.DuzinaSekunde = pesma.DuzinaSekunde;
                    discoContext.SaveChanges();
                }
            }
        }

        public void ObrisiPesmu(int idPesme)
        {
            lock (discoContext)
            {
                Pesma pesma = discoContext.Pesme.Find(idPesme);
                if (pesma != null)
                {
                    discoContext.Pesme.Remove(pesma);
                    discoContext.SaveChanges();
                }
            }
        }

        public void DodajPesmuUPlejlistu(Pesma pesma, int idPlejliste)
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

        public void ObrisiSvePesmeIzPlejliste(int idPlejliste)
        {
            lock (discoContext)
            {
                Plejlista plejlista = discoContext.Plejliste.Find(idPlejliste);
                discoContext.Pesme.RemoveRange(plejlista.ListaPesama);
                discoContext.SaveChanges();
            }
        }
        #endregion Pesme
    }
}
