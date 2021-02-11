using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KazalisteFranic.Model
{
    public class Predstava
    {
        public int Id { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Required]
        public string Sadrzaj { get; set; }

        //DateTime i list izvedbi
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public int Cijena { get; set; }

        [ForeignKey(nameof(Redatelj))]
        public int RedateljId { get; set; }
        public Redatelj Redatelj { get; set; }

        public virtual ICollection<GlumacPredstava> GlumacPredstave { get; set; }
    }
}
