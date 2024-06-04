using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class PlejlistaIzmeniDTO
    {
        [DataMember]
        public int IdPlejliste { get; set; }
        [DataMember]
        public string NoviNaziv { get; set; }
        [DataMember]
        public string NoviAutor { get; set; }
        [DataMember]
        public int Verzija { get; set; }
        [DataMember]
        public List<Pesma> NovaListaPesama { get; set; }
        [DataMember]
        public bool Azurirano { get; set; }
    }
}
