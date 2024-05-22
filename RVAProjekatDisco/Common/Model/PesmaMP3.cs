using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class PesmaMP3 : Pesma
	{
		public PesmaMP3()
		{
			Format = FormatZapisa.MP3;
		}
	}
}
