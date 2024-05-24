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
    public class PesmaDTO
    {
        [DataMember]
        public string IdPesme { get; set; }
        [DataMember]
        public string Autor { get; set; }
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public int DuzinaMinute { get; set; }
        [DataMember]
        public int DuzinaSekunde { get; set; }
        [DataMember]
        public FormatZapisa Format { get; set; }
    }
}
