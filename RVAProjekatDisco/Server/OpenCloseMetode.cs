using Server.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class OpenCloseMetode
    {
        private ServiceHost logovanjeService;
        private ServiceHost korisnikService;
        private ServiceHost dataService;

        public OpenCloseMetode() { }

        public void Open()
        {
            logovanjeService = new ServiceHost(typeof(LogovanjeServis));
            korisnikService = new ServiceHost(typeof(KorisnikServis));
            dataService = new ServiceHost(typeof(DataServis));

            logovanjeService.Open();
            korisnikService.Open();
            dataService.Open();
        }

        public void Close()
        {
            logovanjeService.Close();
            korisnikService.Close();
            dataService.Close();
        }
    }
}
