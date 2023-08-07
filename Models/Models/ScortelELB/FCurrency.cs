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
    public class FCurrency
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Currency description bg
        /// </summary>
        //[Index("IX_CurrencyUnq", 1, IsUnique = true)]
        public string CurrencyBg { get; set; }

        /// <summary>
        /// Currency description eng
        /// </summary>
        //[Index("IX_CurrencyUnq", 2, IsUnique = true)]
        public string CurrencyEng { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        //[Index("IX_CurrencyUnq", 3, IsUnique = true)]
        public int Code { get; set; }

        /// <summary>
        /// Currency 3 alphanum code
        /// </summary>
        public string CodeStr { get; set; }

        public string Country { get; set; }

        public int Number { get; set; }

        /// <summary>
        /// Favorite 
        /// </summary>
        public byte Favorite { get; set; }
    }
}
