using System;
using System.Collections.Generic;

namespace ScortelApi
{
    public partial class TcCommands
    {
        public TcCommands()
        {
            TcDeviceCommand = new HashSet<TcDeviceCommand>();
            TcGroupCommand = new HashSet<TcGroupCommand>();
            TcUserCommand = new HashSet<TcUserCommand>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool? Textchannel { get; set; }
        public string Attributes { get; set; }

        public ICollection<TcDeviceCommand> TcDeviceCommand { get; set; }
        public ICollection<TcGroupCommand> TcGroupCommand { get; set; }
        public ICollection<TcUserCommand> TcUserCommand { get; set; }
    }
}
