using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScortelApi.Models.ScortelELB;
using ScortelApi.ISS;

namespace ScortelApi.Models.ISS
{
    public class ELBook
    {
        [Key]
        public long Id { get; set; }
        public string ELBookNum { get; set; }
        public DateTime CreationDT { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int StartPage { get; set; }         
        public int EndPage { get; set; }
        public int CPage { get; set; }
        public string CPageNum { get; set; }
        public string CISSPageNum { get; set; }
        //public int CISSPage { get; set; }      
        public int CISSPage { get; set; }
        public bool IsHistory { get; set; }
        public Certificate Certificate { get; set; }
       
        // can be omitted and to find vessel through certificate -> permit
        public Vessel Vessel { get; set; }
        //public DateTime ISSUpdateDT { get; set; }
        //public bool IsISSUpdated { get; set; }
        //public FTrip FTrip { get; set; }
        //public ELBookPage ELBookPage { get; set; }
    }

    public class ELBPages
    {

        public ELBook Ebook { get; set; }

        public List<ApiPageCreation> PCList { get; set; }
        public Dictionary<long, PageCreationResp> PageDict { get; set; }

    }
}
