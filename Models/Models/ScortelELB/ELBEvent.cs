using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class ELBEvent
    {
        [Key]
        public long Id { get; set; }

        public DateTime EvtTimestamp { get; set; }

        public DateTime EvtRecTimestamp { get; set; }

        public string PackTypeStr { get; set; }

        public byte PackDesc { get; set; }

        public long ConnId { get; set; }

        public Vessel Ves { get; set; }
    }
}
