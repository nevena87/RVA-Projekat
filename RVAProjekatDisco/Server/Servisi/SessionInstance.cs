using Common.Dodatno;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Servisi
{
    public class SessionInstance
    {
        public Korisnik KorisnikSesije { get; set; }
        public Sesija SesijaCookie { get; set; }
    }
}
