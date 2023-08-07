using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// IDP  sensors data
    /// </summary>
    public class SensRec
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Analog1 - temp
        /// </summary>
        public int Analog1 { get; set; }
        /// <summary>
        /// Analog 2
        /// </summary>
        public int Analog2 { get; set; }
        /// <summary>
        /// Dig io
        /// </summary>
        public byte Dio { get; set; }

        public byte[] GetBytes()
        {
            try
            {
                int inx = 0;
                byte[] resp = new byte[20];

                byte[] analog1 = new byte[4] { 0, 0, 0, 0 };
                analog1 = BitConverter.GetBytes(Analog1);
                Array.Copy(analog1, 0, resp, inx, analog1.Length);
                inx += 4;

                byte[] analog2 = new byte[4] { 0, 0, 0, 0 };
                analog2 = BitConverter.GetBytes(Analog2);
                Array.Copy(analog2, 0, resp, inx, analog2.Length);
                inx += 4;

                resp[inx] = Dio;
                inx++;

                Array.Resize(ref resp, inx);
                return resp;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
