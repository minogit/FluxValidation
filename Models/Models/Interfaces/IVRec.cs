using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface IVRec
    {
        byte[] CallSign { get; set; }
        string CallSignStr { get; set; }
        byte[] CFR { get; set; }
        string CFRStr { get; set; }
        List<CMRec> CMRecs { get; set; }
        byte Content_1 { get; set; }
        byte Content_2 { get; set; }
        byte Content_3 { get; set; }
        byte Country { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        byte[] Description { get; set; }
        string DescriptionStr { get; set; }
        DateTime? DisRecCrDT { get; set; }
        byte[] ExternalMark { get; set; }
        string ExternalMarkStr { get; set; }
        byte Favorite { get; set; }
        LPorts HPort { get; set; }
        long Id { get; set; }
        byte[] License { get; set; }
        string LicenseStr { get; set; }
        byte[] MMSI { get; set; }
        string MMSIStr { get; set; }
        byte Own { get; set; }
        byte[] RadioLic { get; set; }
        string RadioLicStr { get; set; }
        LPorts RPort { get; set; }
        byte[] RSSNr { get; set; }
        string RSSNrStr { get; set; }
        byte[] VesselCompanyAddr_EIK { get; set; }
        string VesselCompanyAddr_EIKStr { get; set; }
        float VesselDraft { get; set; }
        float VesselGrossTonage { get; set; }
        float VesselLen { get; set; }
        byte[] VesselName { get; set; }
        string VesselNameStr { get; set; }
        byte[] VesselOwner { get; set; }
        string VesselOwnerStr { get; set; }
        float VesselWidth { get; set; }

        void ExcludedCFR();
        void ExcludedCompanyAddress();
        void ExcludedDraft();
        void ExcludedExternalMark();
        void ExcludedHomePort();
        void ExcludeDisRecCrDTL();
        void ExcludedLength();
        void ExcludedLicense();
        void ExcludedMMSI();
        void ExcludedOwner();
        void ExcludedRadioLicense();
        void ExcludedRCallSign();
        void ExcludedRegPort();
        void ExcludedRSS();
        void ExcludedTonage();
        void ExcludedVesselCountry();
        void ExcludedVesselDescription();
        void ExcludedWidth();
        void ExcludeVesselName();
        byte[] GetData();
        void IncludedCFR();
        void IncludedCompanyAddress();
        void IncludedDraft();
        void IncludedExternalMark();
        void IncludedHomePort();
        void IncludeDisRecCrDTL();
        void IncludedLength();
        void IncludedLicense();
        void IncludedMMSI();
        void IncludedOwner();
        void IncludedRCallSign();
        void IncludedRegPort();
        void IncludedRSS();
        void IncludedTonage();
        void IncludedVesselCountry();
        void IncludedVesselDescription();
        void IncludedWidth();
        void IncludeRadioLicense();
        void IncludeVesselName();
        void SetDataCorrection();
        void SetNoDataCorrection();
    }
}
