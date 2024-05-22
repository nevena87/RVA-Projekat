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
    public class PesmaFLAC : Pesma
	{
		public PesmaFLAC()
		{
			Format = FormatZapisa.FLAC;
		}

        public override Pesma KlonirajPesmu()
        {
            PesmaFLAC kopija = new PesmaFLAC()
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
