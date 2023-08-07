using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface ITransRec
    {
        byte Content { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        DateTime DisRecCrDT { get; set; }
        ushort EconZone { get; set; }
        List<FCRec> FCRecs { get; set; }
        long Id { get; set; }
        //PosRec Pos { get; set; }
        ushort StatZone { get; set; }
        ZoneDefinition ZoneDefinitionEcon { get; set; }
        ZoneDefinition ZoneDefinitionStat { get; set; }

        void ExcludeDisRecCrDTL();
        void ExcludeEconZone();
        void ExcludePos();
        void ExcludeStatZone();       
        void IncludeDisRecCrDTL();
        void IncludeEconZone();
        void IncludePos();
        void IncludeStatZone();
        void SetDataCorrection();
        void SetNoDataCorrection();
        void VesselRoleDonor();
        void VesselRoleReceiving();
        byte[] GetData();
    }
}
