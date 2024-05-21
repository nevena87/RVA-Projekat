using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dodatno
{
    [DataContract]
    public class Sesija
    {
        [DataMember]
        public string IdSesije { get; set; }
    }
}
