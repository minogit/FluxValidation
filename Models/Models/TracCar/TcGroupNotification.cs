using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcGroupNotification
    {
        public int Groupid { get; set; }
        public int Notificationid { get; set; }
        public int Id { get; set; }

        public TcGroups Group { get; set; }
        public TcNotifications Notification { get; set; }
    }
}
