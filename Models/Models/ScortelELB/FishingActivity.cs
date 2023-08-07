using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Fishing activity
    /// ELBProtocol 2.0.0
    /// </summary>
    public class FishingActivity
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        //[Index("IX_FActivityUnq", IsUnique = true)]
        public string Activity { get; set; }

        public string ActivityEng { get; set; }

        public int Code { get; set; }

        public byte Favorite { get; set; }
    }
}
