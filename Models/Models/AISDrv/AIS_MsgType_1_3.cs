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
    public class AIS_MsgType_1_3
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
        public string NavigationStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RateOfTurn_ROT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SpeedOverGround_SOG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PositionAccuracy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CourseOverGround_COG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TrueHeading_HDG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TimeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ManeuverIndicator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Spare { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RAIMFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RadioStatus { get; set; }
        #endregion

        #region FVMS2.0 data
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordedTime { get; set; }
        //public string FVMS_VesselName { get; set; }
        //public string FVMS_WialonID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? StatusTransWialonIPS { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public DateTime SentWialonIPS { get; set; }

        #endregion
    }
}
