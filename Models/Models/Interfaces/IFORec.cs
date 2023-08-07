using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface IFORec
    {
        byte Content { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        DateTime DisRecCrDT { get; set; }
        SEFORec EndFO { get; set; }
        List<FCRec> FCRecs { get; set; }
        byte FCRecsCount { get; set; }
        long Id { get; set; }
        JFRec JFRec { get; set; }
        SEFORec StartFO { get; set; }

        void CloseFO();
        void ExcludeDisRecCrDTL();
        void ExcludeEFO();
        void ExcludeJFRec();
        byte[] GetData();
        void IncludeDisRecCrDTL();
        void IncludeEFO();
        void IncludeJFRec();
        void OpenFO();
        void SetDataCorrection();
        void SetNoDataCorrection();
    }
}
