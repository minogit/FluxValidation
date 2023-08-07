 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public static class ISSToolHelper
    {
        /// <summary>
        /// Convert ISS json timestamp with format yyyy/MM/dd, HH:mm:ss (2018 / 01 / 08, 14:13:47)
        /// into DateTime var
        /// </summary>
        /// <param name="isstime"></param>
        /// <returns></returns>
        public static DateTime? ISSTimeToDatetime(string isstime)
        {
            try
            {
                if (!string.IsNullOrEmpty(isstime))
                {
                    var isstimearr = isstime.Split(',');
                    var datetmp = isstimearr[0]; // trim
                    var time = isstimearr[1]; // trim

                    var datearr = datetmp.Split('/');
                    var year = datearr[0]; // trim
                    var month = datearr[1]; // trim
                    var date = datearr[2];

                    var timearr = time.Split(':');
                    var hour = timearr[0]; // trim
                    var min = timearr[1]; // trim
                    var sec = timearr[2];
                    // for tests only
                    var dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date), int.Parse(hour), int.Parse(min), int.Parse(sec));
                    return dt;

                } else
                {
                    return null;
                }                
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isstime"></param>
        /// <returns></returns>
        public static DateTime ISSTimeToDatetimeNotNullable(string isstime)
        {
            try
            {
                if (!string.IsNullOrEmpty(isstime))
                {
                    var isstimearr = isstime.Split(',');
                    var datetmp = isstimearr[0]; // trim
                    var time = isstimearr[1]; // trim

                    var datearr = datetmp.Split('/');
                    var year = datearr[0]; // trim
                    var month = datearr[1]; // trim
                    var date = datearr[2];

                    var timearr = time.Split(':');
                    var hour = timearr[0]; // trim
                    var min = timearr[1]; // trim
                    var sec = timearr[2];
                    // for tests only
                    var dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date), int.Parse(hour), int.Parse(min), int.Parse(sec));
                    return dt;

                }
                else
                {
                    return new DateTime(1970,01,01);
                }
            }
            catch (Exception)
            {
                return new DateTime(1970, 01, 01);
            }
        }
    }
}
