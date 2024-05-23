using Common.Dodatno;
using Common.Model;
using Common.ObjektiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfejsi
{
    [ServiceContract]
    public interface IKorisnikServis
    {
        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        KorisnikDTO DobaviInfoKorisnika(Sesija sesija);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void IzmeniInfoKorisnika(Sesija sesija, KorisnikDTO korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void DodajKorisnika(Sesija sesija, Korisnik korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void PromeniLozinku(Sesija sesija, string lozinka);
    }
}
