using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface IJFRec
    {
        byte Content { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        DateTime DisRecCrDT { get; set; }
        long Id { get; set; }
        PosRec Pos { get; set; }
        List<VRec> VRecs { get; set; }

        void ExcludeDisRecCrDTL();
        byte[] GetData();
        void IncludeDisRecCrDTL();
        void SetDataCorrection();
        void SetNoDataCorrection();
    }
}
