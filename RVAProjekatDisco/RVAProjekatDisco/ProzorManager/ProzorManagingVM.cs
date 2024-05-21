using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.WindowManager
{
    public class ProzorManagingVM
    {
        public IProzorManager ProzorManager { get; private set; }
        public ProzorManagingVM(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
        }
    }
}
