using ScortelApi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    //public class PortInOut
    //{

    //    /// <summary>
    //    /// Timestamp since 2018.01.01 00:00:00 UTC in seconds
    //    /// </summary>
    //    public uint Timestamp
    //    {
    //        get; set;
    //    }

    //    /// <summary>
    //    /// Latitude
    //    /// 166° 59’ 59’’ => 10020.983’ => 10020.983’ * 1000 => 10020983 miliminutes 
    //    /// 166 * 60 = 9960’
    //    /// 59’ = 59’
    //    /// 59’’ = 0.983’
    //    /// </summary>
    //    public int Latitude { get; set; }
    //    /// <summary>
    //    /// Longitude
    //    /// 166° 59’ 59’’ is converted to 1665959;
    //    ///-23° 12‘ 44‘‘ is converted      
    //    ///-231244
    //    /// </summary>
    //    public int Longitude { get; set; }
    //    public ushort Speed { get; set; }
    //    public ushort Course { get; set; }
    //    /// <summary>
    //    /// Id of port
    //    /// </summary>
    //    public ushort PortId { get; set; }
    //    /// <summary>
    //    /// Status of vessel position 
    //    /// 0 - arrive into port area
    //    /// 1 - leave port area
    //    /// </summary>
    //    public byte InOut { get; set; }

    //    /// <summary>
    //    /// use only after decoding
    //    /// </summary>
    //    public DateTime CurrentTime { get; set; }

    //    public DateTime ReceivedTimestamp { get; set; }


    //    /// <summary>
    //    /// constructor
    //    /// </summary>
    //    public PortInOut()
    //    {

    //    }

    //    public byte[] GetData()
    //    {
    //        // legth = 4 + 4 + 4 + 2 + 1 = 15 bytes
    //        byte[] resp = new byte[15];
    //        byte[] time = BitConverter.GetBytes(Timestamp);
    //        Array.Copy(time, 0, resp, 0, time.Length);
    //        byte[] lat = BitConverter.GetBytes(Latitude);
    //        Array.Copy(lat, 0, resp, 4, lat.Length);
    //        byte[] lng = BitConverter.GetBytes(Longitude);
    //        Array.Copy(lng, 0, resp, 8, lng.Length);
    //        byte[] speed = BitConverter.GetBytes(Speed);
    //        Array.Copy(speed, 0, resp, 12, speed.Length);
    //        byte[] course = BitConverter.GetBytes(Course);
    //        Array.Copy(course, 0, resp, 14, course.Length);
    //        byte[] portid = BitConverter.GetBytes(PortId);
    //        Array.Copy(portid, 0, resp, 16, portid.Length);
    //        byte[] inout = new byte[] { InOut };
    //        Array.Copy(inout, 0, resp, 18, inout.Length);

    //        return resp;
    //    }

    //    /// <summary>
    //    /// Decode PortInOut data
    //    /// </summary>
    //    /// <param name="Data"></param>
    //    public void DecodePacket(byte[] Data)
    //    {
    //        int inx = 0;
    //        // content - 1byte
    //        var cnt = Data[0];
    //        inx += 1;
    //        // portid - 2 bytes

    //        if (!THelp.CheckBits(cnt, 0))
    //        {
    //            var portid = BitConverter.ToInt16(Data, inx);
    //            inx += 2;
    //            // time - 4 bytes
    //            uint time = BitConverter.ToUInt32(Data, inx); //0
    //            DateTime dt = THelp.FromELBTimeFormat(time);
    //            inx += 4;
    //            // lat - 4 bytes
    //            //int lat = BitConverter.ToInt32(Data, 4);
    //            int lat = BitConverter.ToInt32(Data, inx);
    //            inx += 4;
    //            // lng - 4 bytes
    //            //int lng = BitConverter.ToInt32(Data, 8);
    //            int lng = BitConverter.ToInt32(Data, inx);
    //            inx += 4;
    //            // speed - 2 bytes
    //            //ushort speed = BitConverter.ToUInt16(Data, 12);
    //            ushort speed = BitConverter.ToUInt16(Data, inx);
    //            inx += 2;
    //            // course - 2 bytes
    //            //ushort course = BitConverter.ToUInt16(Data, 14);
    //            ushort course = BitConverter.ToUInt16(Data, inx);
    //            inx += 2;

    //            CurrentTime = dt;
    //            Latitude = lat;
    //            Longitude = lng;
    //            Speed = speed;
    //            Course = course;
    //            PortId = (ushort)portid;
    //        }



    //        if (!THelp.CheckBits(cnt, 1))
    //        {
    //            InOut = 0;
    //        }
    //        else
    //        {
    //            InOut = 1;
    //        }

    //        //InOut = inout;


    //        //ushort portid = BitConverter.ToUInt16(Data, 16);
    //        //byte inout = Data[Data.Length-1];

    //        //CurrentTime = dt;
    //        //Latitude = lat;
    //        //Longitude = lng;
    //        //Speed = speed;
    //        //Course = course;
    //        //PortId = portid;
    //        //InOut = inout;              
    //    }
    //}
}
