using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class FGDescription
    {
        [Key]
        public long Id { get; set; }
        public string FGType { get; set; }
        public string FGCode { get; set; }
        public string FGSize { get; set; }
        public string FGUsingCount { get; set; }
        public int Seq { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return FGCode + " - " + FGType; }
        }
    }
}
