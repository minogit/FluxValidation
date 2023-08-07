using ScortelApi.Tools;
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
    /// Port Depart or Arrive
    /// </summary>
    public class PortInOutRec
    {
        private UInt16 portid;
        private int portidlite;


        #region bit pos
        /// <summary>
        /// Not in user
        /// </summary>
        //private const byte InOut_bit_position = 1;
        private const byte Gen_bit_position = 2;
        private const byte Pos_bit_position = 0;
        #endregion

        /// <summary>
        /// SQLite PK
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>        
        /// Bit 2 – Status generation of record – 23.06.2019
        ///•	0 – auto generated
        ///•	1 – manual generated
        /// Bit 1 – In out port - not in use
        ///•	0 – arrive in Port
        ///•	1 – departure out Port
        /// Bit 0 – with/ without PosRec
        ///•	0 – with PosRec
        ///•	1 – without PosRec
        /// If GPS Position is included Timestamp field have to be excluded
        /// </summary>
        public byte Content { get; set; }

        /// <summary>
        /// Port id from list
        /// </summary>
        [NotMapped]
        public UInt16 PortId
        {
            get { return portid; }
            set
            {
                portid = value;
                portidlite = value;
            }
        }

        /// <summary>
        /// Port id from list
        /// SQLite
        /// </summary>
        //[Index("IX_PortInOutRecUnq", 1, IsUnique = true)]
        public int PortIdLite
        {
            get { return portidlite; }
            set
            {
                portidlite = value;
                portid = (UInt16)value;
            }
        }
        //[Index("IX_PortInOutRecUnq", 2, IsUnique = true)]
        public DateTime CreationDT { get; set; }

        /// <summary>
        /// GPS Position
        /// </summary>
        public PosRec Pos { get; set; } = new PosRec();

        #region server part
        /// <summary>
        /// Status of vessel position 
        /// 0 - arrive into port area
        /// 1 - leave port area
        /// </summary>
        public byte InOut { get; set; }

        /// <summary>
        /// use only after decoding
        /// </summary>
        public DateTime CurrentTime { get; set; }
        #endregion

        /// <summary>
        /// Convert class fields to byte array
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            int inx = 0;
            byte[] resp = new byte[21];

            // content - 1 byte
            resp[inx] = Content;
            inx += 1;

            // port id - 2 bytes
            byte[] portid = new byte[2] { 0, 0 };
            portid = BitConverter.GetBytes(PortId);
            Array.Copy(portid, 0, resp, inx, portid.Length);
            inx += Marshal.SizeOf(PortId);


            // position gps
            if (!THelp.CheckBits(Content, Pos_bit_position))
            {
                var posarr = Pos.GetData();
                Array.Copy(posarr, 0, resp, inx, posarr.Length);
                inx += posarr.Length;
            }

            Array.Resize(ref resp, inx);

            return resp;
        }

        /// <summary>
        ///  Decode PortInOut data
        /// </summary>
        /// <param name="Data"></param>
        public void DecodePacket(byte[] Data)
        {
            int inx = 0;
            // content - 1byte
            var cnt = Data[0];
            inx += 1;
            // portid - 2 bytes
            var portid = BitConverter.ToUInt16(Data, inx);
            inx += 2;

            if (!THelp.CheckBits(cnt, 0))
            {
                PosRec tmpPRec = new PosRec();
                byte[] PosArr = new byte[tmpPRec.PosRecSizeOf];
                Array.Copy(Data, inx, PosArr, 0, tmpPRec.PosRecSizeOf);

                tmpPRec.DecodePacket(PosArr);
                Pos = tmpPRec;
                CurrentTime = THelp.FromELBTimeFormat(Pos.Timestamp);

                #region Comments - old version
                ////// time - 4 bytes
                ////uint time = BitConverter.ToUInt32(Data, inx); //0
                ////DateTime dt = THelp.FromELBTimeFormat(time);

                ////tmpPRec.Timestamp = time;
                ////inx += 4;

                ////// lat - 4 bytes
                //////int lat = BitConverter.ToInt32(Data, 4);
                ////int lat = BitConverter.ToInt32(Data, inx);
                ////tmpPRec.Lat = lat;
                ////inx += 4;

                ////// lng - 4 bytes
                //////int lng = BitConverter.ToInt32(Data, 8);
                ////int lng = BitConverter.ToInt32(Data, inx);
                ////tmpPRec.Lng = lng;
                ////inx += 4;

                ////// speed - 2 bytes
                //////ushort speed = BitConverter.ToUInt16(Data, 12);
                ////ushort speed = BitConverter.ToUInt16(Data, inx);
                ////tmpPRec.
                ////inx += 2;
                ////// course - 2 bytes
                //////ushort course = BitConverter.ToUInt16(Data, 14);
                ////ushort course = BitConverter.ToUInt16(Data, inx);
                ////inx += 2;

                ////CurrentTime = dt;
                ////Latitude = lat;
                ////Longitude = lng;
                ////Speed = speed;
                ////Course = course;
                ////PortId = (ushort)portid;
                #endregion
            }

            // Port in / out activity
            if (!THelp.CheckBits(cnt, 1))
            {
                InOut = 0;
            }
            else
            {
                InOut = 1;
            }

            PortId = portid;
        }
    }
}
