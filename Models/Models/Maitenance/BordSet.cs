using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Maitenance
{
    public partial class BordSet
    {
        [Key]
        public long Id { get; set; }
        public string Category { get; set; }
        public string ListBC { get; set; }
        public string Item { get; set; }
        public int GPRSAnt { get; set; }
        public string SatMod { get; set; }
        public string SatModSN { get; set; }
        public string SatModIMEI { get; set; }
        public int ModCable { get; set; }
        public int ModShr { get; set; }
        public int SatMKIT { get; set; }
        public string BK { get; set; }
        public string BKIMEI { get; set; }
        public string BKSN { get; set; }
        public int BKCradle { get; set; }
        public string NRES { get; set; }
        public string NRESSN { get; set; }
        public string AIS { get; set; }
        public string AISSN { get; set; }
        public int AISGPS { get; set; }
        public int AISVHF { get; set; }
        public string AISGPSSN { get; set; }
        public string AISGPSPPSN { get; set; }
        public string RFIDRes { get; set; }
        public string RFIDTag { get; set; }
        public string RFIDSens { get; set; }
        public string IDPSim { get; set; }
        public int PanicBtn { get; set; }
        public int IsActive { get; set; }

        //public long VesselId { get; set; }
        //public Vessel Vessel { get; set; }
    }
}
