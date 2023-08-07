 
 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class PackSeq
    {
        [Key]
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public byte CSeq { get; set; }

        
    }
}
