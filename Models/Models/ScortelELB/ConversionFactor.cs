 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public partial class ConversionFactor
    {
        [Key]
        public long Id { get; set; }


        //[Index("IX_CFactUnq", 1, IsUnique = true)]
        //[Key, ForeignKey("Species"), Column(Order = 0)]
        public int species_id { get; set; }

        //[Index("IX_CFactUnq", 2, IsUnique = true)]
        //[Key, ForeignKey("FPresentation"), Column(Order = 1)]
        public int presentation_id { get; set; }

        //[Index("IX_CFactUnq", 3, IsUnique = true)]
        //[Key, ForeignKey("FCondition"), Column(Order = 2)]
        
        public int condition_id { get; set; }
        public virtual Species Species { get; set; }
        public virtual FPresentation FPresentation { get; set; }
     
        public virtual FCondition FCondition { get; set; }

        public float Coefficient { get; set; }
        /// <summary>
        /// Fresh / frozen
        /// 0 - fresh
        /// 1 = frozen
        /// </summary>
        public byte Type { get; set; }
    }
}
