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
	public class PesmaWAV : Pesma
	{
		public PesmaWAV()
		{
			Format = FormatZapisa.WAV;
		}

        public override Pesma KlonirajPesmu()
        {
            PesmaWAV kopija = new PesmaWAV()
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
