using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.Interfaces
{
    public interface ICMRec
    {
        byte[] Address { get; set; }
        string AddressStr { get; set; }
        byte[] CellPhone { get; set; }
        string CellPhoneStr { get; set; }
        byte Content { get; set; }
        byte Content_2 { get; set; }
        DateTime CreationDT { get; set; }
        uint CreationDTL { get; set; }
        DateTime DisRecCrDT { get; set; }
        byte[] EMail { get; set; }
        string EMailStr { get; set; }
        byte Favorite { get; set; }
        byte[] Fax { get; set; }
        string FaxStr { get; set; }
        long Id { get; set; }
        byte[] MedicalInfo { get; set; }
        string MedicalInfoStr { get; set; }
        byte[] Name { get; set; }
        string NameStr { get; set; }
        byte Nationality { get; set; }
        byte[] Notes { get; set; }
        string NotesStr { get; set; }
        byte[] Position { get; set; }
        string PositionStr { get; set; }
        byte[] Username { get; set; }
        string UsernameStr { get; set; }
        byte[] Userpass { get; set; }
        string UserpassStr { get; set; }
        ushort ZipCode { get; set; }
        int ZipCodeLite { get; set; }

        void EcludeFax();
        void ExcludeAccessData();
        void ExcludeDisRecCrDTL();
        void ExcludeEmail();
        void ExcludeMedicalInfo();
        void ExcludeNationality();
        void ExcludeNotes();
        void ExcludeZipCode();        
        void IncludeAccessData();
        void IncludeDisRecCrDTL();
        void IncludeEmail();
        void IncludeFax();
        void IncludeMedicalInfo();
        void IncludeNationality();
        void IncludeNotes();
        void IncludeZipCode();
        void SetDataCorrection();
        void SetFullData();
        void SetMinimalInfo();
        void SetNoDataCorrection();

        byte[] GetData();
    }
}
