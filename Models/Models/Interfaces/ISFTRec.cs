﻿using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface ISFTRec
    {
        int CompletionStatus { get; set; }
        string CompletionStatusStr { get; }
        byte Content { get; set; }
        DateTime CreationDT { get; set; }
        string CreationStatus { get; }
        DateTime DisRecCrDT { get; set; }
        FishingActivity FishingActivity { get; set; }
        long Id { get; set; }
        LPorts LPorts { get; set; }
        PosRec Pos { get; set; }
        DateTime ReceivedTimestamp { get; set; }
        int SentStatus { get; set; }
        string SentStatusStr { get; }
        ICollection<SFTRecFGRec> SFTRecFGRecs { get; set; }
        long Timestamp { get; set; }
        long TimestampLite { get; set; }
        string TimestampStr { get; set; }
        ZoneDefinition ZoneDefinitionEcon { get; set; }
        ZoneDefinition ZoneDefinitionStat { get; set; }

        void DecodePacket(byte[] Data);
        void ExcludeDisRecCrDTL();
        void ExcludeEconZone();
        void ExcludeGPSPos();
        void ExcludePortId();
        void ExcludeStatZone();
        byte[] GetData();
        void IncludeDisRecCrDTL();
        void IncludeEconZone();
        void IncludeGPSPos();
        void IncludePortId();
        void IncludeStatZone();
        void SetAutoGenerated();
        void SetDataCorrection();
        void SetManualGenerated();
        void SetNoDataCorrection();
    }
}
