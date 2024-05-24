using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.Komande
{
    public interface IUndoRedo
    {
        void Vrati();
        void Izvrsi();
    }
}
