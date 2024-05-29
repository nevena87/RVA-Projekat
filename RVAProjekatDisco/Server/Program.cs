using Common.Enumeracije;
using Common.Model;
using log4net;
using Server.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            InicijalniPodaciZaBazu();

            OpenCloseMetode openClose = new OpenCloseMetode();

            openClose.Open();
            Console.WriteLine("Server je pokrenut...");

            Console.ReadKey();

            openClose.Close();
            Console.WriteLine("Server je zaustavljen...");
        }

        #region INICIJALNI PODACI
        private static void InicijalniPodaciZaBazu()
        {
            if (!(DbManager.Instance.discoContext.Korisnici.Count() == 0 &&
                DbManager.Instance.discoContext.Plejliste.Count() == 0 &&
                DbManager.Instance.discoContext.Pesme.Count() == 0))
            {
                return;
            }

            // Admin korisnik
            Korisnik admin = new Korisnik()
            {
                KorisnickoIme = "admin",
                Lozinka = "admin",
                Ime = "Nevena",
                Prezime = "Culibrk",
                Tip = TipKorisnika.Admin
            };

            DbManager.Instance.DodajKorisnika(admin);

            // Pesma
            PesmaMP3 pesmaMP3 = new PesmaMP3()
            {
                Naziv = "neka pesma",
                Autor = "neki autor",
                DuzinaMinute = 3,
                DuzinaSekunde = 12
            };
            DbManager.Instance.DodajPesmu(pesmaMP3);

            // Plejlista
            Plejlista plejlista = new Plejlista()
            {
                Naziv = "Muzika 2024",
                Autor = "Nevena"
            };
            plejlista.ListaPesama.Add(pesmaMP3);
            DbManager.Instance.DodajPlejlistu(plejlista);

            Console.WriteLine("Zavrseno");
        }
        #endregion
    }
}
