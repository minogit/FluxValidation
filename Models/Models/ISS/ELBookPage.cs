using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScortelApi.Models.ScortelELB;

namespace ScortelApi.Models.ISS
{
    public class ELBookPage
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreationDT { get; set; }
        public int CPage { get; set; }
        public string CPageNum { get; set; }
        public string CISSPageNum { get; set; }
        public int CISSPage { get; set; }
        public DateTime ISSUpdateDT { get; set; }
        public bool IsISSUpdated { get; set; }
        public bool IsHistory { get; set; }
        public ELBook ELBook { get; set; }
        public bool IsFluxRep { get; set; }
        public DateTime FluxRepDT { get; set; }
        public FTrip FTrip { get; set; } 
        // added for New ISS creaion page logic
        public long Certfg_id { get; set; } 
        public int Certfg_eye { get; set; }
        public int Certfg_descid { get; set; }

        // vessel
        // day reports
        // fos for the current elbook
        // trans
    }
}
