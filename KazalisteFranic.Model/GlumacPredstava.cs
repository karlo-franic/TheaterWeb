using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KazalisteFranic.Model
{
    public class GlumacPredstava
    {
        [Key]
        public int GlumacId { get; set; }
        public int PredstavaId { get; set; }

        public virtual Glumac Glumac { get; set; }
        public virtual Predstava Predstava { get; set; }
       
    }
}
