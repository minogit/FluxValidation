using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUserCalendar
    {
        public int Userid { get; set; }
        public int Calendarid { get; set; }
        public int Id { get; set; }

        public TcCalendars Calendar { get; set; }
        public TcUsers User { get; set; }
    }
}
