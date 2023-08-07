using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class FGTrademark
    {
        [Key]
        public long Id { get; set; }
        //[Index("IX_FGTrademarks_Unq", 1, IsUnique = true)]
        public string TrademarkEng { get; set; }

        public string TrademarkBg { get; set; }
        //[Index("IX_FGTrademarks_Unq", 2, IsUnique = true)]
        public int Code { get; set; }
    }
}
