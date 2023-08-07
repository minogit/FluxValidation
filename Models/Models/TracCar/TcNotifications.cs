using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcNotifications
    {
        public TcNotifications()
        {
            TcGroupNotification = new HashSet<TcGroupNotification>();
            TcUserNotification = new HashSet<TcUserNotification>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Attributes { get; set; }
        public bool? Always { get; set; }
        public int? Calendarid { get; set; }
        public string Notificators { get; set; }

        public TcCalendars Calendar { get; set; }
        public ICollection<TcGroupNotification> TcGroupNotification { get; set; }
        public ICollection<TcUserNotification> TcUserNotification { get; set; }
    }
}
