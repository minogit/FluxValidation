using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public class OISSUpPageNum
    {
        /// <summary>
        /// 
        /// </summary>
        public byte Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FGCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FGEye { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ulong OISSPageNum { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[500];

            try
            {
                #region Content
                resp[inx] = Content;
                inx++;
                #endregion

                #region FGCode - string
                // FGCOde length = 3               
                resp[inx] = (byte)FGCode.Length;
                inx++;
                // FGCode data
                byte[] tmp = Encoding.UTF8.GetBytes(FGCode);
                Array.Copy(tmp, 0, resp, inx, tmp.Length);
                inx += tmp.Length;
                #endregion

                #region FGEye
                byte[] fgeyebyte = new byte[4] { 0, 0, 0, 0 };
                fgeyebyte = BitConverter.GetBytes(FGEye);
                Array.Copy(fgeyebyte, 0, resp, inx, fgeyebyte.Length);
                inx += Marshal.SizeOf(FGEye);
                #endregion

                //TODO: FG -> dimensions

                //TODO: FG -> License
  
                #region CPage
                byte[] cpagebyte = new byte[4] { 0, 0, 0, 0 };
                cpagebyte = BitConverter.GetBytes(CPage);
                Array.Copy(cpagebyte, 0, resp, inx, cpagebyte.Length);
                inx += Marshal.SizeOf(CPage);
                #endregion

                #region OISSPageNum
                byte[] oisspagenumbyte = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                oisspagenumbyte = BitConverter.GetBytes(OISSPageNum);
                Array.Copy(oisspagenumbyte, 0, resp, inx, oisspagenumbyte.Length);
                inx += Marshal.SizeOf(OISSPageNum);
                #endregion

                Array.Resize(ref resp, inx);
                return resp;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
