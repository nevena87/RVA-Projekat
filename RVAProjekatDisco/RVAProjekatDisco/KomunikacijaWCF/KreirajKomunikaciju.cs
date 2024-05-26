using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVAProjekatDisco.KomunikacijaWCF
{
    public class KreirajKomunikaciju
    {
        private static KomunikacijaWCF komunikacija = null;

        // Singleton pattern
        public static KomunikacijaWCF Komunikacija
        {
            get
            {
                if (komunikacija == null)
                {
                    komunikacija = new KomunikacijaWCF();
                }
                return komunikacija;
            }
        }
    }
}
