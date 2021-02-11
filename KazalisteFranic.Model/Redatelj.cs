using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KazalisteFranic.Model
{
    public class Redatelj
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public char Spol { get; set; }

        [ForeignKey(nameof(Diplomirao))]
        public int AkademijaId { get; set; }
        public Akademija Diplomirao { get; set; }

        public string FullIme => $"{Ime} {Prezime}";

        public virtual ICollection<Predstava> Predstave { get; set; }
    }
}
