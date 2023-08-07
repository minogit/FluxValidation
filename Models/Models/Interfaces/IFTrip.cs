using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface IFTrip
    {
        int CompletionStatus { get; set; }
        string CompletionStatusStr { get; }
        byte Content { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        string CreationStatus { get; }
        List<DIRec> DIRecs { get; set; }
        //ICollection<DIRec> DIRecs { get; set; }
        byte DIRecsCount { get; set; }
        DateTime DisRecCrDT { get; set; }
        EFTRec EFTRec { get; set; }
        string FTName { get; set; }
        long Id { get; set; }
        int SentStatus { get; set; }
        string SentStatusStr { get; }
        SFTRec SFTRec { get; set; }
        byte[] TripNumber { get; set; }
        string TripNumberStr { get; set; }

        void ExcludeDisRecCrDTL();
        byte[] GetData();
        void IncludeDisRecCrDTL();
        void SetAutoGeneration();
        void SetManualGeneration();
        void SetNoRecDataCorrection();
        void SetRandomTripNumber();
        void SetRealTripNumber();
        void SetRecDataCorrection();
    }
}
