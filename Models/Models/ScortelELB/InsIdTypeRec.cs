using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// ELB Protocol 2.0.0
    /// </summary>
    public class InsIdTypeRec
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Inspector identification code
        /// </summary>
        //[Index("IX_InsIdTypeRecUnq", 1, IsUnique = true)]
        public int Code { get; set; }

        /// <summary>
        /// Inspector identification type bg
        /// </summary>
        //[Index("IX_InsIdTypeRecUnq", 2, IsUnique = true)]
        public string TypeBg { get; set; }

        public string TypeEng { get; set; }

        
    }
}
