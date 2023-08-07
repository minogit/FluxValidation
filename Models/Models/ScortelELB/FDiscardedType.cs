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
    public class FDiscardedType
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// DiscardedType description bg
        /// </summary>
        //[Index("IX_DiscTypeUnq", 1, IsUnique = true)]
        public string FDisBg { get; set; }

        /// <summary>
        /// DiscardedType description eng
        /// </summary>
        //[Index("IX_DiscTypeUnq", 2, IsUnique = true)]
        public string FDisEng { get; set; }

        /// <summary>
        /// DiscardedType code
        /// 0 - weight
        /// 1 - units
        /// </summary>
        //[Index("IX_DiscTypeUnq", 3, IsUnique = true)]
        public byte DiscardedCode { get; set; }

        /// <summary>
        /// Favorite 
        /// </summary>
        public byte Favorite { get; set; }
    }
}
