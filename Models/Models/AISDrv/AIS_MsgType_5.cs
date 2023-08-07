using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.AISDrv
{
    /// <summary>
    /// 
    /// </summary>
    public class AIS_MsgType_5
    {
        #region AIS data
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RepeatIndicator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MMSI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AISVersioin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IMONumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CallSign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VesselName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DimensionToBow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DimensionToStern { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DimensionToPort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DimensionToStarboard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PositionFixType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ETA_month { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ETA_day { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ETA_hour { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ETA_minute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Draught { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DTE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Spare { get; set; }
        #endregion

        #region FVMS2.0 data
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordedTime { get; set; }
        //public string FVMS_VesselName { get; set; }
        //public string FVMS_WialonID { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public AIS_MsgType_5()
        {
            MsgType = "";
            RepeatIndicator = "";
            MMSI = "";
            AISVersioin = "";
            IMONumber = "";
            CallSign = "-";
            VesselName = "";
            ShipType = "";
            DimensionToBow = "";
            DimensionToStern = "";
            DimensionToPort = "";
            DimensionToStarboard = "";
            PositionFixType = "";
            ETA_month = "";
            ETA_day = "";
            ETA_hour = "";
            ETA_minute = "";
            Draught = "";
            Destination = "";
            DTE = "";
            Spare = "";
        }
    }
}
