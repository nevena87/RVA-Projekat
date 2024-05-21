using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Korisnik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdKorisnika { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public string Lozinka { get; set; }
        [DataMember]
        public TipKorisnika Tip { get; set; }

        public Korisnik()
        {
        }

        public Korisnik(string ime, string prezime, string korisnickoIme, string lozinka, TipKorisnika tip)
        {
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Tip = tip;
        }
    }
}
