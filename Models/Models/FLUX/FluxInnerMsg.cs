using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ScortelApi.Models.FLUX
{
    /// <summary>
    /// Class for Flux data - receive and transmition, states of communication etc.
    /// </summary>
    public class FLUXInnerMsg
    {
        #region Consts
        #region StateISS consts
        /// <summary>
        /// 0  = default
        /// </summary>
        public const int StateISS_Default = 0;

        /// <summary>
        ///  1  - received from LocalFlux  -> sent to ISS - FluxLocalResp - Ok 200
        /// </summary>
        public const int StateISS_Received_from_LocalFlux_sent_to_ISS_FluxLocalResp_Ok_200 = 1;

        /// <summary>
        /// 17  - received from FVMS  =/=> not sent to ISS
        /// </summary>
        public const int StateISS_Received_from_FVMS_not_sent_to_ISS_yet = 17;

        /// <summary>
        /// 18  - received from FVMS -> sent to ISS - Ok 200
        /// </summary>
        public const int StateISS_Received_from_FVMS_sent_to_ISS_Ok_200 = 18;
        #endregion
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 0 - From ISS
        /// 1 - From Local Flux
        /// 2 - From FVMS // test purposes
        /// 3 - From IDPDrv
        /// 4 - From Postman or other test client
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// The name of current flux domain
        /// Sample: VesselDomain, FADomain, SalesDomain, ACDRDomain, ISDomain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Received Datetime at IF/ScortelApi for further processing
        /// </summary>
        public DateTime ReceivedFDT { get; set; }
        /// <summary>
        /// Retranslate to next direction datetime
        /// </summary>
        public DateTime SubmitDT { get; set; }

        /// <summary>
        /// XML Document UUID
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// FLUX ON number
        /// </summary>
        public string? ON { get; set; }

        /// <summary>
        /// Response of query in both directions - place UUID of Query/ in case submission - null
        /// </summary>
        public string? ReferencedUUID { get; set; }

        /// <summary>
        /// If needed
        /// </summary>
        public string? ReferencedON { get; set; }

        /// ISS State of communication / Received from Local Flux sent to ISS      
        /// 0  = default
        /// 1  - received from LocalFlux  -> sent to ISS - FluxLocalResp - Ok 200 
        /// 2  - received from LocalFlux  -> sent to ISS - FluxLocalResp - Error
        /// 3  - received from LocalFlux  -> sent to ISS - FluxLocalResp - No response or elapsed timeout
        /// 4  - received from LocalFlux  -> sent to ISS - FluxLocalResp - No connection to ISS
        /// 5  - received from LocalFlux  -> sent to ISS - FLuxResponse/ FluxVesselResponse/ Data from XEU/Local flux - Ok 200   
        /// 6  - received from LocalFlux  -> sent to ISS - FLuxResponse/ FluxVesselResponse/ Data from XEU/Local flux - Error
        /// 7  - received from LocalFlux  -> sent to ISS - FLuxResponse/ FluxVesselResponse/ Data from XEU/Local flux - No response or elapsed timeout
        /// 8  - received from LocalFlux  -> sent to ISS - FLuxResponse/ FluxVesselResponse/ Data from XEU/Local flux - No connection 
        /// 9  - received from LocalFlux  -> sent to ISS - Query from Xeu/ Local Flux - Ok 200
        /// 10  - received from LocalFlux  -> sent to ISS - Query from Xeu/ Local Flux - Error
        /// 11  - received from LocalFlux  -> sent to ISS - Query from Xeu/ Local Flux - No response or elapsed timeout
        /// 12  - received from LocalFlux  -> sent to ISS - Query from Xeu/ Local Flux - No connection to ISS
        /// 13  - received from LocalFlux  -> sent to ISS - info packet from Xeu/ Local Flux - Ok 200
        /// 14  - received from LocalFlux  -> sent to ISS - info packet from Xeu/ Local Flux - Error
        /// 15  - received from LocalFlux  -> sent to ISS - info packet from Xeu/ Local Flux - No response or elapsed timeout
        /// 16  - received from LocalFlux  -> sent to ISS - info packet from Xeu/ Local Flux - No connection to ISS
        /// 17  - received from FVMS  =/=> not sent to ISS
        /// 18  - received from FVMS -> sent to ISS - Ok 200
        /// 19  - received from FVMS -> sent to ISS - Error
        /// 20  - received from FVMS -> sent to ISS - No Response or elapsed timeout
        /// 21  - received from FVMS -> sent to ISS - No Connection
        /// -> todo: check 22  - received from LocalFlux  -> not send to ISS yet 
        /// </summary>
        public int StateISS { get; set; }

        /// <summary>
        /// Response status code from ISS 
        /// </summary>
        public int StateISSRespCode { get; set; }

        /// <summary>
        /// State Flux
        /// LocalFlux State of communication   / Received from ISS sent to Local Flux     
        /// Local Flux
        /// 0   - default
        /// 1   - received from ISS -> sent to local flux, accepted from flux node with ACK 200 
        /// 2   - received from ISS -> sent to local flux, not accepted from flux node - Error / possible wrong XML
        /// 3   - received from ISS -> sent to local flux,  no response on submit or elapsed timeout
        /// 4   - received from ISS -> sent to local flux,  no connection
        /// 5   - received from ISS -> not send yet
        /// 6   - received from Postman -> not send yet
        /// 7   - received from Postman -> sent to local flux, accepted from flux node with ACK 200 
        /// 8   - received from Postman -> sent to local flux, not accepted from flux node - Error / possible wrong XML
        /// 9   - received from Postman -> sent to local flux,  no response on submit or elapsed timeout
        /// 10  - received from Postman -> sent to local flux,  no connection
        /// XEU
        /// 11  - received from ISS -> sent to local flux, not accepted from XEU with ack 201
        /// 12  - received from ISS -> sent to local flux, not accepted from XEU node NOK
        /// 13  - received from ISS -> sent to local flux, not accepted from XEU node WOK   
        /// 
        /// </summary>
        public int StateFlux { get; set; }

        /// <summary>
        /// Response status code from localFlux
        /// </summary>
        public int StateFluxRespCode { get; set; }

        /// <summary>
        /// Response from local flux node on submit
        /// </summary>
        public virtual FluxLocalResp ReceivedLFAck { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<long> ResponseAckIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<long> ResponseDataIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "xml")]
        public string XEUResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public XDocument DetailsXEUResponse
        {
            get { return XEUResponse != null ? XDocument.Parse(XEUResponse) : null; }
            set { XEUResponse = value.ToString(); }
        }

        /// <summary>
        /// The content of xml data
        /// </summary>   
        [Column(TypeName = "xml")]
        public string Payload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public XDocument DetailsPayload
        {
            get { return Payload != null ? XDocument.Parse(Payload) : null; }
            set { Payload = value.ToString(); }
        }



    }

    public  static class DomainEnum{
        public const string VesselDomain = "VesselDomain";
        public const string FADomain = "FADomain";
        public const string SalesDomain = "SalesDomain";
        public const string ACDRDomain = "ACDRDomain";
        public const string ISDomain = "ISDomain";
        public const string FLAPDomain = "FLAPDomain";
        public const string FLUXResponse = "FLUXResponse";
        public const string MDMDomain = "MDMDomain";
        public const string VesselPosition = "VesselPosition";
        public const string EmulatedSalesDomain = "EmulatedSalesDomain";
    }
}
