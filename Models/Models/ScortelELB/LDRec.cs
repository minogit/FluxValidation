using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    
    public class LDRec
    {
        /// <summary>
        /// SQLite PK id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// end fishing trip structure - creation date and time
        /// </summary>
        public DateTime ReceivedDT { get; set; }
      

        #region tripnumber
        /// <summary>
        /// Trip number from information system - number of page of fishing diary
        /// first byte is length of the field, if length is 0 - there is no data
        /// minimum field size 1 byte, maximum 255
        /// Data is encoded in Unicode
        /// Needed for data recreation at server side
        /// </summary>      
        public byte[] TripNumber { get; set; }

        public string TripNumberStr
        {
            get
            {
                if (TripNumber != null && TripNumber.Length != 1)
                {
                    return Encoding.UTF8.GetString(TripNumber, 1, TripNumber.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                //tripnumberstr = value;
                byte[] tmp = Encoding.UTF8.GetBytes(value);
                if (tmp.Length > 255)
                {
                }
                else
                {
                    byte len = (byte)tmp.Length;
                    byte[] tnum = new byte[len + 1];
                    tnum[0] = len;
                    Array.Copy(tmp, 0, tnum, 1, (int)len);
                    TripNumber = tnum;
                }
            }
        }
        #endregion

        #region Fo list
        public List<FORec> FORecs { get; set; }
        #endregion

        #region fc list
        public List<FCRec> FCRecs { get; set; }
        #endregion

        #region Pos
        /// <summary>
        /// Gps position part
        /// </summary>
        public PosRec Pos { get; set; }
        #endregion

        #region Sens
        /// <summary>
        /// Sens rec data
        /// </summary>
        public SensRec Sens { get; set; }
        #endregion

        #region FCRec -> FG
        public byte[] LDFGList { get; set; }
 
        public string LDFGListStr
        {
            get
            {
                if (LDFGList != null && LDFGList.Length != 1)
                {
                    return Encoding.UTF8.GetString(LDFGList, 1, LDFGList.Length - 1);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                byte[] tmp = Encoding.UTF8.GetBytes(value);
                if (tmp.Length > 255)
                {
                }
                else
                {
                    byte len = (byte)tmp.Length;
                    byte[] tnum = new byte[len + 1];
                    tnum[0] = len;
                    Array.Copy(tmp, 0, tnum, 1, (int)len);
                    LDFGList = tnum;
                }
            }
        }
        #endregion

        #region isArchived
        public byte IsArchived { get; set; }
        #endregion

        public byte[] GetBytes(PosRec pos)
        {
            try
            {
                byte[] resp = new byte[5000];
                int inx = 0;

                #region Day rec Trip number                               
                Array.Copy(TripNumber, 0, resp, inx, TripNumber.Length);
                inx += TripNumber.Length;
                #endregion


                // FCRecs count                                                
                resp[inx] = (byte)FCRecs.Count;
                inx += 1;

                // fishing catches
                foreach (var rec in FCRecs)
                {
                    byte[] fc = rec.GetData();
                    Array.Copy(fc, 0, resp, inx, fc.Length);
                    inx += fc.Length;
                }

                // position gps

                var posarr = Pos.GetData();
                Array.Copy(posarr, 0, resp, inx, posarr.Length);
                inx += posarr.Length;


                Array.Resize(ref resp, inx);
                return resp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
    }
}
