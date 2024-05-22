using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
	[DataContract]
    public class PesmaOGG : Pesma
	{
		public PesmaOGG()
		{
			Format = FormatZapisa.OGG;
		}

        public override Pesma KlonirajPesmu()
        {
            PesmaOGG kopija = new PesmaOGG()
            {
                Autor = this.Autor,
                Naziv = this.Naziv,
                DuzinaMinute = this.DuzinaMinute,
                DuzinaSekunde = this.DuzinaSekunde
            };
            return kopija;
        }
    }
}
