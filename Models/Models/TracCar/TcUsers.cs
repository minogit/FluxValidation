using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcUsers
    {
        public TcUsers()
        {
            TcUserAttribute = new HashSet<TcUserAttribute>();
            TcUserCalendar = new HashSet<TcUserCalendar>();
            TcUserCommand = new HashSet<TcUserCommand>();
            TcUserDevice = new HashSet<TcUserDevice>();
            TcUserDriver = new HashSet<TcUserDriver>();
            TcUserGeofence = new HashSet<TcUserGeofence>();
            TcUserGroup = new HashSet<TcUserGroup>();
            TcUserMaintenance = new HashSet<TcUserMaintenance>();
            TcUserNotification = new HashSet<TcUserNotification>();
            TcUserUserManageduser = new HashSet<TcUserUser>();
            TcUserUserUser = new HashSet<TcUserUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hashedpassword { get; set; }
        public string Salt { get; set; }
        public bool? Readonly { get; set; }
        public bool? Administrator { get; set; }
        public string Map { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Zoom { get; set; }
        public bool? Twelvehourformat { get; set; }
        public string Attributes { get; set; }
        public string Coordinateformat { get; set; }
        public bool? Disabled { get; set; }
        public DateTime? Expirationtime { get; set; }
        public int? Devicelimit { get; set; }
        public string Token { get; set; }
        public int? Userlimit { get; set; }
        public bool? Devicereadonly { get; set; }
        public string Phone { get; set; }
        public bool? Limitcommands { get; set; }
        public string Login { get; set; }
        public string Poilayer { get; set; }

        public ICollection<TcUserAttribute> TcUserAttribute { get; set; }
        public ICollection<TcUserCalendar> TcUserCalendar { get; set; }
        public ICollection<TcUserCommand> TcUserCommand { get; set; }
        public ICollection<TcUserDevice> TcUserDevice { get; set; }
        public ICollection<TcUserDriver> TcUserDriver { get; set; }
        public ICollection<TcUserGeofence> TcUserGeofence { get; set; }
        public ICollection<TcUserGroup> TcUserGroup { get; set; }
        public ICollection<TcUserMaintenance> TcUserMaintenance { get; set; }
        public ICollection<TcUserNotification> TcUserNotification { get; set; }
        public ICollection<TcUserUser> TcUserUserManageduser { get; set; }
        public ICollection<TcUserUser> TcUserUserUser { get; set; }
    }
}
