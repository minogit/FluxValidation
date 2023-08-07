using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// ELBProtocol 2.0.0
    /// </summary>
    public class ZAccessLvl
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Access level
        /// </summary>
        //[Index("IX_ZAccessLvl_Unq", 1, IsUnique = true)]
        public int Lvl { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string DescEng { get; set; }

        public string DescBg { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        //[Index("IX_ZAccessLvl_Unq", 2, IsUnique = true)]
        public int Code { get; set; }
    }
}
