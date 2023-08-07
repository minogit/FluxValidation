using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class FQuantityType
    {
        /// <summary>
        /// SQLite PK
        /// ELB Protocol 2.0.0
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// QuantityType description bg
        /// </summary>
        //[Index("IX_QTypeUnq", 1, IsUnique = true)]
        public string QTypeBg { get; set; }

        /// <summary>
        /// Quantity type description eng
        /// </summary>
        //[Index("IX_QTypeUnq", 2, IsUnique = true)]
        public string QTypeEng { get; set; }

        /// <summary>
        /// QunatityType code
        /// </summary>
        //[Index("IX_QTypeUnq", 3, IsUnique = true)]
        public int Code { get; set; }

        /// <summary>
        /// Favorite 
        /// </summary>
        public byte Favorite { get; set; }
    }
}
