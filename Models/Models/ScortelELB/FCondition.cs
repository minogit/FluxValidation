using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Fish condition
    /// ELBProt 2.0.0
    /// </summary>
    public class FCondition
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Condition description bg
        /// </summary>
        //[Index("IX_CondUnq", 1, IsUnique = true)]
        public string PresentBg { get; set; }

        /// <summary>
        /// Condition description eng
        /// </summary>
        //[Index("IX_CondUnq", 2, IsUnique = true)]
        public string PresentEng { get; set; }

        /// <summary>
        /// Condition code
        /// </summary>
        //[Index("IX_CondUnq", 3, IsUnique = true)]
        public int Code { get; set; }

        public string CodeAlpha { get; set; }

        /// <summary>
        /// Favorite 
        /// </summary>
        public byte Favorite { get; set; }

        public virtual ICollection<ConversionFactor> ConversionFactors { get; set; }
    }
}
