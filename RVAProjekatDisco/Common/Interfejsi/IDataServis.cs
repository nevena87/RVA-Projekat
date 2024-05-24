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
    public interface IDataServis
    {
        // --- Plejliste ---
        [OperationContract]
        List<Plejlista> VratiPlejliste(Sesija sesija);

        [OperationContract]
        Plejlista KreirajPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO);

        [OperationContract]
        bool IzmeniPlejlistu(Sesija sesija, PlejlistaIzmeniDTO plejlistaIzmeniDTO);

        [OperationContract]
        void ObrisiPlejlistu(Sesija sesija, int idPlejliste);

        [OperationContract]
        Plejlista DuplirajPlejlistu(Sesija sesija, int idPlejliste);

        // --- Pesme ---
        [OperationContract]
        int DodajPesmu(Sesija sesija, Pesma pesma);

        [OperationContract]
        Pesma VratiPesmu(Sesija sesija, int idPesme);

        [OperationContract]
        List<Pesma> VratiPesme(Sesija sesija);

        [OperationContract]
        void ObrisiPesmu(Sesija sesija, int idPesme);

        /*[OperationContract]
        void ObrisiPesmuIzPlejliste(Sesija sesija, int idPlejliste);*/
    }
}
