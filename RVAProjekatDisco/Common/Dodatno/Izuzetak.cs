using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dodatno
{
    [DataContract]
    public class Izuzetak
    {
        [DataMember]
        public string Poruka { get; set; }

        public Izuzetak() { }

        public Izuzetak(string poruka)
        {
            Poruka = poruka;
        }
    }
}
