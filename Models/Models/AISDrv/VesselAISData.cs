using ScortelApi.Models.ScortelELB;
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
    public class VesselAISData
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FVMS_WialonID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RecordedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FVMS_VesselName { get; set; }

        // Foreihn keys
        /// <summary>
        /// 
        /// </summary>
        public long? VesselId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Vessel Vessel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? AIS_MSGTYPE_1_3_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual AIS_MsgType_1_3 AIS_MSGTYPE_1_3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? AIS_MSGTYPE_5_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual AIS_MsgType_5 AIS_MSGTYPE_5 { get; set; }

    }
}
