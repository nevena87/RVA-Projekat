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

        private static void InicijalniPodaciZaBazu()
        {
            if (!(DbManager.Instance.discoContext.Korisnici.Count() == 0))
            {
                return;
            }

            Korisnik admin = new Korisnik()
            {
                KorisnickoIme = "admin",
                Lozinka = "admin",
                Ime = "Nevena",
                Prezime = "Culibrk",
                Tip = TipKorisnika.Admin
            };

            DbManager.Instance.DodajKorisnika(admin);

            Console.WriteLine("Zavrseno");
        }
    }
}
