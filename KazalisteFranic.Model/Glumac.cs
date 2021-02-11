using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KazalisteFranic.Model
{
    public class Glumac
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public string Citat { get; set; }
        public char Spol { get; set; }

        [ForeignKey(nameof(Diplomirao))]
        public int AkademijaId { get; set; }
        public Akademija Diplomirao { get; set; }

        public string FullIme => $"{Ime} {Prezime}";

        public virtual ICollection<GlumacPredstava> GlumacPredstave { get; set; }
    }
}
