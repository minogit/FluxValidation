using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// ELBProt 2.0.0
    /// </summary>
    public class ZoneType
    {
        [Key]
        public long Id { get; set; }

        //[Index("IX_ZoneType_Unq", 1, IsUnique = true)]
        public byte Type { get; set; }

        public string DescEng { get; set; }

        public string DescBg { get; set; }

        //[Index("IX_ZoneType_Unq", 2, IsUnique = true)]
        public int Code { get; set; }
    }
}
