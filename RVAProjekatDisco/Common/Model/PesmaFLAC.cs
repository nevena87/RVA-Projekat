using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class PesmaFLAC : Pesma
	{
		public PesmaFLAC()
		{
			Format = FormatZapisa.FLAC;
		}
	}
}
