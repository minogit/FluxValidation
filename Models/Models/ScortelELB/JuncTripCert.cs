using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    public class JuncTripCert
    {
        [Key]
        public long Id { get; set; }

        public FTrip Ftrip { get; set; }

        public Certificate Certificate { get; set; }

        public string CPageNum { get; set; }
    }
}
