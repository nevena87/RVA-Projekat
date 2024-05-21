using Common.Dodatno;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfejsi
{
    [ServiceContract]
    public interface ILogovanjeServis
    {
        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        Sesija PrijaviSe(KorisnikZaLogovanje korisnik);

        [OperationContract]
        bool PostojiUBaziKorisnik(KorisnikZaLogovanje korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void OdjaviSe(Sesija sesija);
    }
}
