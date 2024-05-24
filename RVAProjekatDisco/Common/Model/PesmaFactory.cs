using Common.Enumeracije;

namespace Common.Model
{
    public class PesmaFactory : Factory
    {
        public override Pesma NapraviPesmu(FormatZapisa fz)
        {
            switch (fz)
            {
                case FormatZapisa.MP3: return new PesmaMP3();
                case FormatZapisa.WAV: return new PesmaWAV();
                case FormatZapisa.FLAC: return new PesmaFLAC();
                case FormatZapisa.OGG: return new PesmaOGG();
                default: return null;
            }
        }
    }
}
