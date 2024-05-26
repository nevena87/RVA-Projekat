using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    [KnownType(typeof(PesmaMP3))]
    [KnownType(typeof(PesmaWAV))]
    [KnownType(typeof(PesmaFLAC))]
    [KnownType(typeof(PesmaOGG))]
    public abstract class Pesma
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdPesme { get; set; }
        [DataMember]
        public string Autor { get; set; }
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public int DuzinaMinute { get; set; }
        [DataMember]
        public int DuzinaSekunde { get; set; }
        [DataMember]
        public FormatZapisa Format { get; set; }

        public Pesma() { }

        // Nebitno šta je ovde, svakako se radi override
        public virtual Pesma KlonirajPesmu()
        {
            return (Pesma)MemberwiseClone();
        }
    }
}
