using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Fish Presentation
    /// ELBProt 2.0.0
    /// </summary>
    public class FPresentation
    {
        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Presentation description bg
        /// </summary>
        //[Index("IX_PresentUnq", 1, IsUnique = true)]
        public string PresentBg { get; set; }

        /// <summary>
        /// Presentation description eng
        /// </summary>
        //[Index("IX_PresentUnq", 2, IsUnique = true)]
        public string PresentEng { get; set; }

        /// <summary>
        /// Presentation 3 alphnum code
        /// </summary>
        public string PresentCode { get; set; }

        /// <summary>
        /// Presentation code
        /// </summary>
        //[Index("IX_PresentUnq", 3, IsUnique = true)]
        public int Code { get; set; }

        /// <summary>
        /// Favorite 
        /// </summary>
        public byte Favorite { get; set; }

        public virtual ICollection<ConversionFactor> ConversionFactors { get; set; }
    }
}
