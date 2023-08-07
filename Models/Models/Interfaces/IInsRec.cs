using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface IInsRec
    {
        byte Content { get; set; }
        CountryCodeM49 Countries { get; set; }
        byte Country { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        DateTime DisRecCrDT { get; set; }
        long Id { get; set; }
        InsIdTypeRec InsIdTypeRec { get; set; }
        byte[] InspData { get; set; }
        string InspDataStr { get; set; }
        byte InspIdType { get; set; }
        PosRec Pos { get; set; }

        void ExcludeDisRecCrDTL();        
        void IncludeDisRecCrDTL();
        void SetDataCorrection();
        void SetNoDataCorrection();

        byte[] GetData();
    }
}
