using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class KorisnikDTO
    {
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public TipKorisnika Tip { get; set; }
    }
}
