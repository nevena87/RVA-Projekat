using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class KorisnikZaLogovanje
    {
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public string Lozinka { get; set; }

        public KorisnikZaLogovanje()
        {
        }

        public KorisnikZaLogovanje(string korisnickoIme, string lozinka)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }
    }
}
