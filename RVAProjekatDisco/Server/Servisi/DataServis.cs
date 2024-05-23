using Common.Dodatno;
using Common.Interfejsi;
using Common.Model;
using Common.ObjektiDTO;
using log4net;
using Server.PristupBaziPodataka;
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

namespace Server.Servisi
{
    // TODO implement
    public class DataServis : IDataServis
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataServis));

        #region Plejliste
        public Plejlista DodajPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Plejlista novaPlejlista = new Plejlista(plejlistaDTO);
                novaPlejlista = DbManager.Instance.DodajPlejlistu(novaPlejlista);
                log.Info("Plejlista sa id-em " + novaPlejlista.IdPlejliste + " je sacuvana!");

                return novaPlejlista;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }
        
        public List<Plejlista> DobaviSvePlejliste(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                List<Plejlista> plejliste = DbManager.Instance.DobaviSvePlejliste();
                List<Plejlista> kloniranePlejliste = new List<Plejlista>(plejliste.Count);

                // Da li klonirati ili vratiti original?
                foreach (var item in plejliste)
                {
                    kloniranePlejliste.Add(item.KlonirajPlejlistu());
                }
                return kloniranePlejliste;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public bool IzmeniPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Plejlista izmenjenaPlejlista = DbManager.Instance.DobaviPlejlistu(plejlistaDTO.IdPlejliste);
                
                if (plejlistaDTO.Naziv != izmenjenaPlejlista.Naziv)
                {
                    izmenjenaPlejlista.Naziv = plejlistaDTO.Naziv;
                }
                if (plejlistaDTO.Autor != izmenjenaPlejlista.Autor)
                {
                    izmenjenaPlejlista.Autor = plejlistaDTO.Autor;
                }
                if (plejlistaDTO.ListaPesama != null)
                {
                    // Prvo isprazni celu plejlistu
                    if (izmenjenaPlejlista.ListaPesama != null)
                    {
                        DbManager.Instance.ObrisiSvePesmeIzPlejliste(izmenjenaPlejlista.IdPlejliste);
                    }

                    // Onda je ponovo popuni
                    foreach (var item in plejlistaDTO.ListaPesama)
                    {
                        Pesma pesma = izmenjenaPlejlista.DodajPesmu(item);
                        izmenjenaPlejlista.ListaPesama.Add(pesma);
                    }
                }

                DbManager.Instance.SacuvajPromene();
                log.Info("Plejlista sa id-em " + izmenjenaPlejlista.IdPlejliste + " je izmenjena");

                // Za šta će mi ovo ?
                Plejlista p = DbManager.Instance.DobaviPlejlistu(izmenjenaPlejlista.IdPlejliste);

                return true;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return false;
            }
        }

        public void ObrisiPlejlistu(Sesija sesija, int idPlejliste)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                DbManager.Instance.ObrisiPlejlistu(idPlejliste);
                log.Info("Plejlista sa id-em " + idPlejliste + " je obrisana!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public Plejlista KlonirajPlejlistu(Sesija sesija, int idPlejliste)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion Plejliste

        #region Pesme
        public Pesma DodajPesmu(Sesija sesija, Pesma pesma)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Pesma pesmaIzBaze = DbManager.Instance.DobaviPesmu(pesma.IdPesme);
                // Ako pesma ne postoji, samo je dodaj u bazu
                if (pesmaIzBaze == null)
                {
                    pesmaIzBaze = DbManager.Instance.DodajPesmu(pesma);
                }
                // Ako već postoji, izmeni je
                else
                {
                    pesmaIzBaze.Autor = pesma.Autor;
                    pesmaIzBaze.Naziv = pesma.Naziv;
                    pesmaIzBaze.DuzinaMinute = pesma.DuzinaMinute;
                    pesmaIzBaze.DuzinaSekunde = pesma.DuzinaSekunde;

                    DbManager.Instance.SacuvajPromene();
                }

                log.Info("Pesma sa id-em" + pesmaIzBaze.IdPesme + " je sacuvana!");
                return pesmaIzBaze;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public Pesma DobaviPesmu(Sesija sesija, int idPesme)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.DobaviPesmu(idPesme);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public void ObrisiPesmu(Sesija sesija, int idPesme)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                DbManager.Instance.ObrisiPesmu(idPesme);
                log.Info("Pesma sa id-em " + idPesme + " je obrisana!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public List<Pesma> DobaviPesmeZaPlejlistu(Sesija sesija, int idPlejliste)
        {
            // TODO
            throw new NotImplementedException();
        }

        public void ObrisiPesmeIzPlejliste(Sesija sesija, int idPlejliste)
        {
            // TODO
            throw new NotImplementedException();
        }
        #endregion Pesme
    }
}
