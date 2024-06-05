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

namespace Server.Servisi
{
    public class DataServis : IDataServis
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataServis));

        #region Plejliste
        public Plejlista DuplirajPlejlistu(Sesija sesija, int idPlejliste)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Plejlista plejlistaZaKloniranje = DbManager.Instance.VratiPlejlistu(idPlejliste);
                Plejlista kloniranaPlejlista = plejlistaZaKloniranje.KlonirajPlejlistu();

                List<Pesma> kopijaPesmeUPlejlisti = kloniranaPlejlista.ListaPesama;

                kloniranaPlejlista.IdPlejliste = 0;
                kloniranaPlejlista.Naziv += "-kopija";

                Plejlista dupliranaPlejlista = DbManager.Instance.DodajPlejlistu(kloniranaPlejlista);

                log.Info("Klonirana plejlista sa id-em: " + dupliranaPlejlista.IdPlejliste);

                return dupliranaPlejlista;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Error: " + ex.Detail.Poruka);
                return null;
            }
        }

        public List<Plejlista> VratiPlejliste(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                List<Plejlista> plejliste = DbManager.Instance.VratiSvePlejliste();
                List<Plejlista> kloniranePlejliste = new List<Plejlista>(plejliste.Count);

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

        public bool IzmeniPlejlistu(Sesija sesija, PlejlistaIzmeniDTO plejlistaIzmeniDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Plejlista izmenjenaPlejlista = DbManager.Instance.VratiPlejlistu(plejlistaIzmeniDTO.IdPlejliste);
                if (!plejlistaIzmeniDTO.Azurirano && plejlistaIzmeniDTO.Verzija != izmenjenaPlejlista.Verzija)
                {
                    return false;
                }
                if (plejlistaIzmeniDTO.NoviNaziv != null)
                {
                    izmenjenaPlejlista.Naziv = plejlistaIzmeniDTO.NoviNaziv;
                }
                if (plejlistaIzmeniDTO.NoviAutor != null)
                {
                    izmenjenaPlejlista.Autor = plejlistaIzmeniDTO.NoviAutor;
                }
                if (plejlistaIzmeniDTO.NovaListaPesama != null)
                {
                    if (izmenjenaPlejlista.ListaPesama != null)
                    {
                        DbManager.Instance.ObrisiPesmuIzPlejliste(izmenjenaPlejlista.IdPlejliste);
                    }

                    PesmaFactory fabrika = new PesmaFactory();
                    foreach (var item in plejlistaIzmeniDTO.NovaListaPesama)
                    {
                        Pesma pesma = fabrika.NapraviPesmu(item.Format);
                        pesma.Naziv = item.Naziv;
                        pesma.Autor = item.Autor;
                        pesma.DuzinaMinute = item.DuzinaMinute;
                        pesma.DuzinaSekunde = item.DuzinaSekunde;
                        
                        izmenjenaPlejlista.ListaPesama.Add(pesma);
                    }
                }
                ++izmenjenaPlejlista.Verzija;
                DbManager.Instance.SacuvajPromene();
                log.Info("Plejlista sa id-em " + izmenjenaPlejlista.IdPlejliste + " je izmenjena");

                Plejlista p = DbManager.Instance.VratiPlejlistu(izmenjenaPlejlista.IdPlejliste);

                return true;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return false;
            }
        }

        public Plejlista KreirajPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Plejlista novaPlejlista = new Plejlista()
                {
                    Naziv = plejlistaDTO.Naziv,
                    Autor = plejlistaDTO.Autor,
                    Verzija = 1
                };

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
        #endregion Plejliste

        #region Pesme
        public Pesma VratiPesmu(Sesija sesija, int idPesme)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.VratiPesmuPrekoId(idPesme);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public List<Pesma> VratiPesme(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                List<Pesma> pesmeiIzBaze = DbManager.Instance.VratiSvePesme();
                List<Pesma> pomocna = new List<Pesma>();

                foreach (var item in pesmeiIzBaze)
                {
                    bool pronadjenDuplikat = false;
                    foreach (var item1 in pomocna)
                    {
                        if (item.Naziv == item1.Naziv && item.Autor == item1.Autor && item.DuzinaMinute == item1.DuzinaMinute &&
                            item.DuzinaSekunde == item1.DuzinaSekunde && item1.Format == item1.Format)
                        {
                            pronadjenDuplikat = true;
                            break;
                        }
                    }
                    if (!pronadjenDuplikat)
                    {
                        pomocna.Add(item);
                    }
                }

                return pomocna;
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
                log.Info("Pesma sa id-em " + idPesme + " je obrisan!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public int DodajPesmu(Sesija sesija, Pesma pesma)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Pesma pesmaPostoji = DbManager.Instance.VratiPesmuPrekoId(pesma.IdPesme);
                // AKo ne postoji, samo se upisuje u bazu
                if (pesmaPostoji == null)
                {
                    pesmaPostoji = DbManager.Instance.DodajPesmu(pesma);
                }
                // Ako postoji, onda se ažurira postojeća pesma
                else
                {
                    pesmaPostoji.Naziv = pesma.Naziv;
                    pesmaPostoji.Autor = pesma.Autor;
                    pesmaPostoji.DuzinaMinute = pesma.DuzinaMinute;
                    pesmaPostoji.DuzinaSekunde = pesma.DuzinaSekunde;

                    DbManager.Instance.SacuvajPromene();
                }

                log.Info("Pesma sa id-em" + pesmaPostoji.IdPesme + " je sacuvan!");
                return pesmaPostoji.IdPesme;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return -1;
            }
        }
        #endregion Pesme
    }
}
