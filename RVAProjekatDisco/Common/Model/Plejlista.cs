using Common.Enumeracije;
using Common.ObjektiDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Plejlista
	{
		public string Naziv { get; set; }
		public string Autor { get; set; }
		public List<Pesma> ListaPesama {  get; set; }

		// Konstruktor
		public Plejlista(PlejlistaDTO pl)
		{
			Naziv = pl.Naziv;
			Autor = pl.Autor;
			ListaPesama = new List<Pesma>();
		}

		// Dodavanje pesme
		public void DodajPesmu(PesmaDTO pesma)
		{
			// Napravi fabriku
			Factory fabrika = new PesmaFactory();

			Pesma novaPesma;
			switch (pesma.Format)
			{
				case (FormatZapisa.MP3):
					novaPesma = fabrika.NapraviPesmu(FormatZapisa.MP3);
					break;
                case (FormatZapisa.WAV):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.WAV);
                    break;
                case (FormatZapisa.OGG):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.OGG);
                    break;
                case (FormatZapisa.FLAC):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.FLAC);
                    break;
                default:
					novaPesma = fabrika.NapraviPesmu(FormatZapisa.MP3);	// promeni ?
					break;
			}

			// Popuni polja
			novaPesma.Naziv = pesma.Naziv;
			novaPesma.Autor = pesma.Autor;
			novaPesma.DuzinaMinute = pesma.DuzinaMinute;
			novaPesma.DuzinaSekunde = pesma.DuzinaSekunde;

			// Dodaj novu pesmu u plejlistu
			ListaPesama.Add(novaPesma);
		}
    }
}
