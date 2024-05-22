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
    public class PesmaMP3 : Pesma
	{
		public PesmaMP3()
		{
			Format = FormatZapisa.MP3;
		}

        public override Pesma KlonirajPesmu()
        {
            PesmaMP3 kopija = new PesmaMP3()
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
