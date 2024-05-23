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
    public interface IDataServis
    {
        // --- Plejliste ---
        [OperationContract]
        List<Plejlista> DobaviSvePlejliste(Sesija sesija);

        [OperationContract]
        Plejlista DodajPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO);

        [OperationContract]
        bool IzmeniPlejlistu(Sesija sesija, PlejlistaDTO plejlistaDTO);

        [OperationContract]
        void ObrisiPlejlistu(Sesija sesija, int idPlejliste);

        [OperationContract]
        Plejlista KlonirajPlejlistu(Sesija sesija, int idPlejliste);

        // --- Pesme ---
        [OperationContract]
        Pesma DodajPesmu(Sesija sesija, Pesma pesma);

        [OperationContract]
        Pesma DobaviPesmu(Sesija sesija, int idPesme);

        [OperationContract]
        List<Pesma> DobaviPesmeZaPlejlistu(Sesija sesija, int idPlejliste);

        [OperationContract]
        void ObrisiPesmu(Sesija sesija, int idPesme);

        [OperationContract]
        void ObrisiPesmeIzPlejliste(Sesija sesija, int idPlejliste);
    }
}
