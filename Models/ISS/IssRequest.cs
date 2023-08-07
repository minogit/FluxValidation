using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScortelApi.ISS
{
    /// <summary>
    /// 
    /// </summary>
    public static class IssRequest
    {           
        private static HttpWebRequest request { get; set; }  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ISSUrll"></param>
        /// <param name="ISSToken"></param>
        /// <param name="MethodPerms"></param>
        /// <param name="AddPars"></param>
        /// <returns></returns>
        public static HttpWebRequest GetRequest(string ISSUrll, string ISSToken, string MethodPerms, string AddPars)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(ISSUrll + MethodPerms + AddPars);
                request.ContentType = "application/json";
                request.KeepAlive = true;
                request.Headers.Add("Authorization", "Bearer " + ISSToken);
                request.Method = "GET";
                request.Timeout = 50000;
                return request;
            }
            catch (Exception)
            {

                return null;
            }                           
        }
    }
}
