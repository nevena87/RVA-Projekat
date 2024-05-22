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
    public abstract class Pesma
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPesme { get; set; }    // ostaviti ili obrisati?
        public string Autor { get; set; }
        public string Naziv { get; set; }
        public int DuzinaMinute { get; set; }
        public int DuzinaSekunde { get; set; }
        protected FormatZapisa Format { get; set; }
	}
}
