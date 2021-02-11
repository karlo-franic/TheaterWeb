using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KazalisteFranic.Model
{
    public class Akademija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }

        public virtual ICollection<Redatelj> Redatelji { get; set; }
        public virtual ICollection<Glumac> Glumci { get; set; }
    }
}
