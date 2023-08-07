using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class SFTRecFGRec
    {
        public long SFTRec_Id { get; set; }
        public SFTRec SFTRec { get; set; }

        public long FGRec_Id { get; set; }
        public FGRec FGRec { get; set; }
    }
}
