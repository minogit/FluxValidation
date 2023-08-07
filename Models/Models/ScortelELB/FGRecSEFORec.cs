using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class FGRecSEFORec
    {
        [Key]
        public long Id { get; set; }
        //[Key]
        //public long FGRec_Id { get; set; }
        public FGRec FGRec { get; set; }

        //public long SEFORec_Id { get; set; }
        public SEFORec SEFORec { get; set; }
    }
}
