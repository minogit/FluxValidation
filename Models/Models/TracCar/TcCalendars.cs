using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcCalendars
    {
        public TcCalendars()
        {
            TcGeofences = new HashSet<TcGeofences>();
            TcNotifications = new HashSet<TcNotifications>();
            TcUserCalendar = new HashSet<TcUserCalendar>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string Attributes { get; set; }

        public ICollection<TcGeofences> TcGeofences { get; set; }
        public ICollection<TcNotifications> TcNotifications { get; set; }
        public ICollection<TcUserCalendar> TcUserCalendar { get; set; }
    }
}
