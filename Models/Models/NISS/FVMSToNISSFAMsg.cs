using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScortelApi.Models.FLUX;

namespace ScortelApi.Models.NISS
{
    /// <summary>
    /// Class for processing FA data from FV to NISS
    /// </summary>
    public class FVMSToNISSFAMsg
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
  
        /// <summary>
        /// DT of FA msg convertion and storage to DB
        /// </summary>
        public DateTime ConstructedDT { get; set; }

        /// <summary>
        /// XML Document UUID
        /// In case fvms to niss is it nessessary?
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// State of communication
        /// Sample
        /// Received from IDPDrv to FVMS
        /// 
        /// 0 - received from IDPDrv
        /// 1 - sent to NISS not acknowledged 
        /// 2 - received OK ack from NISS
        /// 3 - received NOK ack from NISS with error  
        /// 4 - received NOK ack from NISS without error        
        /// 5 - sent, elapsed timeout - no real response from NISS (check SendCounter if data have to be resend or marked as failed)
        /// 6 - failed
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// If State 
        /// </summary>
        public int SendCounter { get; set; }

        /// <summary>
        /// Storing to DB the FA data
        /// </summary>
        public virtual FLUXFAReportMessageType Data { get; set; }
    }
}
