using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ScortelApi.Models.ScortelELB
{
    /// <summary>
    /// Position record
    /// size - 18 bytes??
    /// Adding fields with suffix Lite to represent class fields to SQLite (not supported unsigned types) 
    /// </summary>
    public class PosRec
    {
        private UInt32 timestamp;
        private Int64 timestamplite;
        private UInt16 speed;
        private int speedlite;
        private UInt16 course;
        private int courselite;


        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Timestamp of position
        /// </summary>
        [NotMapped]
        public UInt32 Timestamp
        {
            get { return timestamp; }
            set
            {
                timestamp = value;
                //TimestampLite = value;
            }
        }

        /// <summary>
        /// Timestamp of postition
        /// SQLite
        /// </summary>
        public Int64 TimestampLite
        {
            get { return timestamplite; }
            set
            {
                timestamplite = value;
                timestamp = (UInt32)value;
            }
        }

        /// <summary>
        /// Position Latitude
        /// </summary>
        public int Lat { get; set; }

        /// <summary>
        /// Position Longitude
        /// </summary>
        public int Lng { get; set; }

        /// <summary>
        /// GPS IDP speed
        /// </summary>
        [NotMapped]
        public UInt16 Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                speedlite = value;
            }
        }

        /// <summary>
        /// GPS IDP speed - SQLite
        /// </summary>
        public int SpeedLite
        {
            get { return speedlite; }
            set
            {
                speedlite = value;
                speed = (UInt16)value;
            }
        }

        /// <summary>
        /// GPS IDP course
        /// </summary>
        [NotMapped]
        public UInt16 Course
        {
            get { return course; }
            set
            {
                course = value;
                courselite = value;
            }
        }

        /// <summary>
        /// GPS IDP course
        /// SQLite
        /// </summary>
        public int CourseLite
        {
            get { return courselite; }
            set
            {
                courselite = value;
                course = (UInt16)value;
            }
        }

        /// <summary>
        /// Size of ELB Protocol PosRec data
        /// </summary>
        public byte PosRecSizeOf
        {
            get
            {
                int size = Marshal.SizeOf(Timestamp);
                size += Marshal.SizeOf(Lat);
                size += Marshal.SizeOf(Lng);
                size += Marshal.SizeOf(Speed);
                size += Marshal.SizeOf(Course);
                return (byte)size;
            }
        }

        /// <summary>
        /// Convert fields data to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] resp = new byte[16];
            byte[] time = BitConverter.GetBytes(Timestamp);
            Array.Copy(time, 0, resp, 0, time.Length);

            byte[] latarr = BitConverter.GetBytes(Lat);
            Array.Copy(latarr, 0, resp, 4, latarr.Length);

            byte[] lngarr = BitConverter.GetBytes(Lng);
            Array.Copy(lngarr, 0, resp, 8, lngarr.Length);

            byte[] sparr = BitConverter.GetBytes(Speed);
            Array.Copy(sparr, 0, resp, 12, sparr.Length);

            byte[] carr = BitConverter.GetBytes(Course);
            Array.Copy(carr, 0, resp, 14, carr.Length);

            return resp;
        }

        /// <summary>
        /// Decode PosRec data
        /// </summary>
        /// <param name="Data"></param>
        public void DecodePacket(byte[] Data)
        {

            try
            {
                int inx = 0;
                // uint timestamp - 4 bytes
                Timestamp = BitConverter.ToUInt32(Data, inx); //0
                inx += 4;

                // int latitude - 4 bytes
                Lat = BitConverter.ToInt32(Data, inx);
                inx += 4;

                // int longitude - 4 bytes
                Lng = BitConverter.ToInt32(Data, inx);
                inx += 4;

                // ushort speed - 2 bytes
                Speed = BitConverter.ToUInt16(Data, inx);
                inx += 2;

                // ushort course - 2 bytes
                Course = BitConverter.ToUInt16(Data, inx);
                inx += 2;
            }
            catch (Exception)
            {
                //TODO: log     
            }
        }

        public PosRec()
        { }
    }
}
