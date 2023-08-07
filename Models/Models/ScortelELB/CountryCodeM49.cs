using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// UN - Standard country or area codes for statistical user M49
    /// Country ISO Alpha 3 codes
    /// ELB Protocol 2.0.0
    /// </summary>
    public class CountryCodeM49
    {
        /// <summary>
        /// SQL PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Country or area description
        /// </summary>
        //[Index("IX_CountryCodeM49Unq", 1, IsUnique = true)]
        public string CountryOrArea { get; set; }

        /// <summary>
        /// M49 code
        /// </summary>
        public string M49 { get; set; }

        /// <summary>
        /// Country ISO alpha 3 code
        /// </summary>
        //[Index("IX_CountryCodeM49Unq", 2, IsUnique = true)]
        public string ISOAlpha3Code { get; set; }
 
    }
}
