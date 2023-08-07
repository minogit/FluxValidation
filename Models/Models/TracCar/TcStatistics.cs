using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcStatistics
    {
        public int Id { get; set; }
        public DateTime Capturetime { get; set; }
        public int Activeusers { get; set; }
        public int Activedevices { get; set; }
        public int Requests { get; set; }
        public int Messagesreceived { get; set; }
        public int Messagesstored { get; set; }
        public string Attributes { get; set; }
        public int Mailsent { get; set; }
        public int Smssent { get; set; }
        public int Geocoderrequests { get; set; }
        public int Geolocationrequests { get; set; }
    }
}
