using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserNotification
    {
        public int Userid { get; set; }
        public int Notificationid { get; set; }
        public int Id { get; set; }

        public TcNotifications Notification { get; set; }
        public TcUsers User { get; set; }
    }
}
