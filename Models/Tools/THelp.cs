
//using GeoAPI.Geometries;
 
using ScortelApi.ISS;
using ScortelApi.ISSN;
using ScortelApi.Models.AISDrv;
using ScortelApi.Models.IDPDrv;
using ScortelApi.Models.ScortelELB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
 

namespace ScortelApi.Tools
{
    public static class THelp
    {
        private static bool IsBitSet(byte b, byte nPos)
        {
            return new BitArray(new[] { b })[nPos];
        }

        public static bool CheckBits(byte b, byte bitpos)
        {
            //var res = IsBitSet(b, bitpos);


            if ((b & (1 << bitpos)) != 0)
            {
                // val 1
                return true;
            }
            else
            {
                return false;
            }

        }

        public static byte ResetBits(byte b, byte bitpos)
        {
            return b &= (byte)(~(1 << bitpos));
        }

        public static byte SetBits(byte b, byte bitpos)
        {
            return b |= (byte)(1 << bitpos);
        }

        /// <summary>
        /// Truncate double digit
        /// </summary>
        /// <param name="val"></param>
        /// <param name="prec"></param>
        /// <returns></returns>
        public static double DoublePrec(double val, int prec)
        {
            try
            {
                return Math.Truncate(val * 10000) / 10000;

            }
            catch (Exception)
            {
                return val;
            }
        }
 
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string SecondsToTimeStr(double seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            string str = time.ToString(@"hh\:mm\:ss\:fff");
            return str;
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static double ConvertKilobytesToMegabytes(long kilobytes)
        {
            return kilobytes / 1024f;
        }

        /// <summary>
        /// Format DateTime to ScortelElbProtocol date time format
        /// Seconds since 2018.01.01 00:00:00 UTC
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static uint ELBTimeFormat(DateTime dt)
        {
            System.DateTime dtDateTime = new DateTime(2018, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            if (dt <= dtDateTime)
                return 0;

            TimeSpan tspan = dt - dtDateTime;
            return (uint)tspan.TotalSeconds;
        }

        /// <summary>
        /// FormatScortelElbProtocol date time format to  DateTime UTC
        /// Seconds since 2018.01.01 00:00:00 UTC
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime FromELBTimeFormat(uint TimestampELB)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(2018, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return dtDateTime.AddSeconds(TimestampELB);
            }
            catch (Exception)
            {
                return new DateTime(2018, 01, 01);
            }
        }

        /// <summary>
        /// Convret ELB timestamp seconds since 2018,01,01 to datetime type
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime DateTimeFromELBTimestamp(long timestamp)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(2018, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return dtDateTime.AddSeconds(timestamp).ToLocalTime();
            }
            catch (Exception)
            {
                return new DateTime(2018, 01, 01);
            }
        }

        public static DateTime DateTimeFromELBTimestamp_utc(long timestamp)
        {
            try
            {
                var tmp = DateTime.Now;
                var utc = DateTime.UtcNow;
                var ts = tmp - utc;
                
                 
                System.DateTime dtDateTime = new DateTime(2018, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                //return dtDateTime.AddSeconds(timestamp).ToUniversalTime();
                return dtDateTime.AddSeconds(timestamp).AddSeconds(-ts.TotalSeconds); 
            }
            catch (Exception)
            {
                return new DateTime(2018, 01, 01);
            }
        }

        public static DateTime DateTimeFromELBTimestamp_tmp(long timestamp)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(2018, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return dtDateTime.AddSeconds(timestamp);
            }
            catch (Exception)
            {
                return new DateTime(2018, 01, 01);
            }
        }

        public static int CoordConvert(int grad, int min, int sec, bool isLat)
        {
            if (isLat)
            {
                if (Math.Abs(grad) > 90 || min > 60 || sec > 60)
                    return 0;
            }
            else
            {
                if (Math.Abs(grad) > 180 || min > 60 || sec > 60)
                    return 0;
            }
            var GradInMin = grad * 60; // in minutes
            var SecInMin = sec / 60.0; // in minutes
            var Coords = GradInMin + min + SecInMin;
            var Result = Math.Truncate(Coords * 1000) / 1000;
            var InMiliMin = Result * 1000;
            return (int)InMiliMin;
        }

        public static int CoordConvert(double grad, bool isLat)
        {
            if (isLat)
            {
                if (Math.Abs(grad) > 90)
                    return 0;
            }
            else
            {
                if (Math.Abs(grad) > 180)
                    return 0;
            }
            var GradInMin = grad * 60; // in minutes

            var Result = Math.Truncate(GradInMin * 1000) / 1000;
            var InMiliMin = Result * 1000;
            return (int)InMiliMin;
        }

        public static void IDP_Standard_Reports_Console_Visualization(ReturnMessage msg)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("MESSAGE DATA");
                Console.WriteLine("msgID: " + msg.ID + " msgUTC: " + msg.MessageUTC.ToString());
                Console.WriteLine("SIN: " + msg.Payload.SIN.ToString());
                Console.WriteLine("Paylod-Name1: " + msg.Payload.Fields[0].Name + " Paylod-Name: " + msg.Payload.Fields[0].Value);
                Console.WriteLine("Paylod-Name2: " + msg.Payload.Fields[1].Name + " Paylod-Name: " + msg.Payload.Fields[1].Value);
                Console.WriteLine("Paylod-Name3: " + msg.Payload.Fields[2].Name + " Paylod-Name: " + msg.Payload.Fields[2].Value);
                Console.WriteLine("Paylod-Name4: " + msg.Payload.Fields[3].Name + " Paylod-Name: " + msg.Payload.Fields[3].Value);
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("");
            }
            catch (Exception)
            {

            }
        }

        public static void ConsoleWriteLine(string txt)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString() + " - " + txt);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Find string 
        /// </summary>
        /// <param name="inputString">json responce string</param>
        /// <param name="nameToFindValue">searched string</param>
        /// <param name="isValueString">is searched value string {"value"} or int {value} </param>
        /// <returns></returns>
        public static string FindSingleStringValue(string inputString, string nameToFindValue, bool isValueString)
        {
            int posStart, posEnd;
            string tempString;
            string findSingleValue = String.Empty;
            if (inputString.Contains(nameToFindValue))
            {
                posStart = inputString.IndexOf(nameToFindValue);
                if (isValueString)
                {
                    tempString = inputString.Substring(posStart + nameToFindValue.Length + 2);
                    posEnd = tempString.IndexOf("\"");
                }
                else
                {
                    tempString = inputString.Substring(posStart + nameToFindValue.Length + 1);
                    posEnd = tempString.IndexOf(",");
                }

                findSingleValue = tempString.Substring(0, posEnd);
            }
            return findSingleValue;
        }

        public static string FindSingleNumberValue(string inputString, string nameToFindValue)
        {
            int posStart, posEnd;
            string tempString;
            string findSingleValue = String.Empty;
            if (inputString.Contains(nameToFindValue))
            {
                posStart = inputString.IndexOf(nameToFindValue);
                tempString = inputString.Substring(posStart + nameToFindValue.Length + 1);
                posEnd = tempString.IndexOf(",");
                findSingleValue = tempString.Substring(0, posEnd);
            }
            return findSingleValue;
        }

        public static long ToUnixTimestamp(this DateTime d)
        {
            var epoch = d - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)epoch.TotalSeconds;
        }

        #region AIS Export GeoJson

        public static string ExportGeoJson(List<AIS_MsgType_1_3> Msgs13)
        {
            try
            {
                string json = "";

                var header = "{ \"type\": \"FeatureCollection\", \"features\": [ ";
                json = header;
                string formater = "";

                //if (Msgs5.Count > 0)
                //{
                //    for (var i = 0; i < Msgs5.Count; i++)
                //    {

                //        DateTime tmpdt = (DateTime)Msgs5[i].RecordedTime;
                //        long time = THelp.ToUnixTimestamp(tmpdt);

                //        if (i == Msgs5.Count - 1)
                //        {
                //            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs5[i].Longitude + "," + Msgs5[i].Latitude + "] }, ";
                //            formater += " \"properties\": { \"MsgType\":\"" + Msgs5[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                //            formater += " \"RepeatIndicator\":\"" + Msgs5[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs5[i].MMSI + "\", ";
                //            formater += " \"NavigationStatus\":\"" + Msgs5[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs5[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs5[i].SpeedOverGround_SOG + "\", ";
                //            formater += " \"TimeStamp\":\"" + Msgs5[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs5[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs5[i].Spare + "\", ";
                //            formater += " \"RAIMFlag\":\"" + Msgs5[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs5[i].RadioStatus + "\", ";
                //            formater += " \"PositionAccuracy\":\"" + Msgs5[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs5[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs5[i].TrueHeading_HDG + "\" } } ";
                //        }
                //        else
                //        {
                //            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs5[i].Longitude + "," + Msgs5[i].Latitude + "] }, ";
                //            formater += " \"properties\": { \"MsgType\":\"" + Msgs5[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                //            formater += " \"RepeatIndicator\":\"" + Msgs5[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs5[i].MMSI + "\", ";
                //            formater += " \"NavigationStatus\":\"" + Msgs5[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs5[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs5[i].SpeedOverGround_SOG + "\", ";
                //            formater += " \"TimeStamp\":\"" + Msgs5[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs5[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs5[i].Spare + "\", ";
                //            formater += " \"RAIMFlag\":\"" + Msgs5[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs5[i].RadioStatus + "\", ";
                //            formater += " \"PositionAccuracy\":\"" + Msgs5[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs5[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs5[i].TrueHeading_HDG + "\" } }, ";
                //        }
                //        json += formater;
                //    }
                //}

                if (Msgs13.Count > 0)
                {
                    for (var i = 0; i < Msgs13.Count; i++)
                    {
                         
                        DateTime tmpdt = (DateTime)Msgs13[i].RecordedTime;
                        long time = THelp.ToUnixTimestamp(tmpdt);

                        if (i == Msgs13.Count - 1)
                        {
                            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].Longitude + "," + Msgs13[i].Latitude + "] }, ";
                            formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                            formater += " \"RepeatIndicator\":\"" + Msgs13[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].MMSI + "\", ";
                            formater += " \"NavigationStatus\":\"" + Msgs13[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].SpeedOverGround_SOG + "\", ";
                            formater += " \"TimeStamp\":\"" + Msgs13[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].Spare + "\", ";
                            formater += " \"RAIMFlag\":\"" + Msgs13[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].RadioStatus + "\", ";
                            formater += " \"PositionAccuracy\":\"" + Msgs13[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].TrueHeading_HDG + "\" } } ";
                        }
                        else
                        {
                            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].Longitude + "," + Msgs13[i].Latitude + "] }, ";
                            formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                            formater += " \"RepeatIndicator\":\"" + Msgs13[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].MMSI + "\", ";
                            formater += " \"NavigationStatus\":\"" + Msgs13[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].SpeedOverGround_SOG + "\", ";
                            formater += " \"TimeStamp\":\"" + Msgs13[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].Spare + "\", ";
                            formater += " \"RAIMFlag\":\"" + Msgs13[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].RadioStatus + "\", ";
                            formater += " \"PositionAccuracy\":\"" + Msgs13[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].TrueHeading_HDG + "\" } }, ";
                        }
                        json += formater;
                    }

                    json += " ]} ";
                }

                return json;
 
            }
            catch (Exception ex)
            {
                THelp.ConsoleWriteLine("ExportGeoJson ex: " + ex.Message);
                return "";
            }
        }

        public static string ExportGeoJson(List<AISCombined> Msgs13)
        {
            try
            {
                string json = "";

                var header = "{ \"type\": \"FeatureCollection\", \"features\": [ ";
                json = header;
                string formater = "";



                if (Msgs13.Count > 0)
                {
                    for (var i = 0; i < Msgs13.Count; i++)
                    {

                        DateTime tmpdt = (DateTime)Msgs13[i].n.RecordedTime;
                        long time = THelp.ToUnixTimestamp(tmpdt);

                        if (i == Msgs13.Count - 1)
                        {
                            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].n.Longitude + "," + Msgs13[i].n.Latitude + "] }, ";
                            formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].n.MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                            formater += " \"RepeatIndicator\":\"" + Msgs13[i].n.RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].n.MMSI + "\", ";
                            formater += " \"NavigationStatus\":\"" + Msgs13[i].n.NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].n.RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].n.SpeedOverGround_SOG + "\", ";
                            formater += " \"TimeStamp\":\"" + Msgs13[i].n.TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].n.ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].n.Spare + "\", ";
                            formater += " \"RAIMFlag\":\"" + Msgs13[i].n.RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].n.RadioStatus + "\", ";
                            formater += " \"PositionAccuracy\":\"" + Msgs13[i].n.PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].n.CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].n.TrueHeading_HDG + "\" ";
                            if (Msgs13[i].k.CallSign != "-")
                            {
                                formater += ", \"AISVersioin\":\"" + Msgs13[i].k.AISVersioin + "\", \"DTE\":\"" + Msgs13[i].k.DTE + "\", ";
                                formater += " \"Destination\":\"" + Msgs13[i].k.Destination + "\", \"DimensionToBow\":\"" + Msgs13[i].k.DimensionToBow + "\", \"DimensionToPort\":\"" + Msgs13[i].k.DimensionToPort + "\", ";
                                formater += " \"DimensionToStarboard\":\"" + Msgs13[i].k.DimensionToStarboard + "\", \"DimensionToStern\":\"" + Msgs13[i].k.DimensionToStern + "\", \"Draught\":\"" + Msgs13[i].k.Draught + "\", ";
                                formater += " \"ETA_day\":\"" + Msgs13[i].k.ETA_day + "\", \"ETA_hour\":\"" + Msgs13[i].k.ETA_hour + "\", \"ETA_minute\":\"" + Msgs13[i].k.ETA_minute + "\", ";
                                formater += " \"ETA_month\":\"" + Msgs13[i].k.ETA_month + "\", \"IMONumber\":\"" + Msgs13[i].k.IMONumber + "\", \"PositionFixType\":\"" + Msgs13[i].k.PositionFixType + "\", ";
                                formater += " \"ShipType\":\"" + Msgs13[i].k.ShipType + "\", \"VesselName\":\"" + Msgs13[i].k.VesselName + "\", \"CallSign\":\"" + Msgs13[i].k.CallSign + "\" "; //} } ";                                 
                                if (Msgs13[i].m != null)
                                {
                                    formater += ", \"VesselNameBG\":\"" + Msgs13[i].m.Regnum + "\" } } ";
                                }
                                else
                                {
                                    formater += "} } ";
                                }
                            }
                            else
                            {
                                formater += "} } ";
                            }                             
                        }
                        else
                        {
                            formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].n.Longitude + "," + Msgs13[i].n.Latitude + "] }, ";
                            formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].n.MsgType + "\", \"recordedtime\":\"" + time + "\", ";
                            formater += " \"RepeatIndicator\":\"" + Msgs13[i].n.RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].n.MMSI + "\", ";
                            formater += " \"NavigationStatus\":\"" + Msgs13[i].n.NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].n.RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].n.SpeedOverGround_SOG + "\", ";
                            formater += " \"TimeStamp\":\"" + Msgs13[i].n.TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].n.ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].n.Spare + "\", ";
                            formater += " \"RAIMFlag\":\"" + Msgs13[i].n.RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].n.RadioStatus + "\", ";
                            formater += " \"PositionAccuracy\":\"" + Msgs13[i].n.PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].n.CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].n.TrueHeading_HDG + "\" ";
                            if (Msgs13[i].k.CallSign != "-")
                            {
                                formater += ", \"AISVersioin\":\"" + Msgs13[i].k.AISVersioin + "\", \"DTE\":\"" + Msgs13[i].k.DTE + "\", ";
                                formater += " \"Destination\":\"" + Msgs13[i].k.Destination + "\", \"DimensionToBow\":\"" + Msgs13[i].k.DimensionToBow + "\", \"DimensionToPort\":\"" + Msgs13[i].k.DimensionToPort + "\", ";
                                formater += " \"DimensionToStarboard\":\"" + Msgs13[i].k.DimensionToStarboard + "\", \"DimensionToStern\":\"" + Msgs13[i].k.DimensionToStern + "\", \"Draught\":\"" + Msgs13[i].k.Draught + "\", ";
                                formater += " \"ETA_day\":\"" + Msgs13[i].k.ETA_day + "\", \"ETA_hour\":\"" + Msgs13[i].k.ETA_hour + "\", \"ETA_minute\":\"" + Msgs13[i].k.ETA_minute + "\", ";
                                formater += " \"ETA_month\":\"" + Msgs13[i].k.ETA_month + "\", \"IMONumber\":\"" + Msgs13[i].k.IMONumber + "\", \"PositionFixType\":\"" + Msgs13[i].k.PositionFixType + "\", ";
                                formater += " \"ShipType\":\"" + Msgs13[i].k.ShipType + "\", \"VesselName\":\"" + Msgs13[i].k.VesselName + "\", \"CallSign\":\"" + Msgs13[i].k.CallSign + "\" ";                                  
                                if (Msgs13[i].m != null)
                                {
                                    formater += ", \"VesselNameBG\":\"" + Msgs13[i].m.Regnum + "\" } }, ";
                                }
                                else
                                {
                                    formater += "} }, ";
                                }
                            }
                            else
                            {
                                formater += "} }, ";
                            }
                        }
                        json += formater;
                    }

                    json += " ]} ";
                }

                return json;

            }
            catch (Exception ex)
            {
                THelp.ConsoleWriteLine("ExportGeoJson ex: " + ex.Message);
                return "";
            }
        }
        public static string ExportGeoJson(List<VesselAISData> Msgs13)
        {
            return "";
            ////try
            ////{
            ////    string json = "";

            ////    var header = "{ \"type\": \"FeatureCollection\", \"features\": [ ";
            ////    json = header;
            ////    string formater = "";

                 

            ////    if (Msgs13.Count > 0)
            ////    {
            ////        for (var i = 0; i < Msgs13.Count; i++)
            ////        {

            ////            DateTime tmpdt = (DateTime)Msgs13[i].RecordedTime;
            ////            long time = THelp.ToUnixTimestamp(tmpdt);

            ////            if (i == Msgs13.Count - 1)
            ////            {                          
            ////                formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].Longitude + "," + Msgs13[i].Latitude + "] }, ";
            ////                formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
            ////                formater += " \"RepeatIndicator\":\"" + Msgs13[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].MMSI + "\", ";
            ////                formater += " \"NavigationStatus\":\"" + Msgs13[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].SpeedOverGround_SOG + "\", ";
            ////                formater += " \"TimeStamp\":\"" + Msgs13[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].Spare + "\", ";
            ////                formater += " \"RAIMFlag\":\"" + Msgs13[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].RadioStatus + "\", ";
            ////                formater += " \"PositionAccuracy\":\"" + Msgs13[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].TrueHeading_HDG + "\", ";
            ////                formater += " \"VesselNameBG\":\"" + Msgs13[i].VesselNameBG + "\", \"AISVersioin\":\"" + Msgs13[i].AISVersioin + "\", \"DTE\":\"" + Msgs13[i].DTE + "\", ";
            ////                formater += " \"Destination\":\"" + Msgs13[i].Destination + "\", \"DimensionToBow\":\"" + Msgs13[i].DimensionToBow + "\", \"DimensionToPort\":\"" + Msgs13[i].DimensionToPort + "\", ";
            ////                formater += " \"DimensionToStarboard\":\"" + Msgs13[i].DimensionToStarboard + "\", \"DimensionToStern\":\"" + Msgs13[i].DimensionToStern + "\", \"Draught\":\"" + Msgs13[i].Draught + "\", ";
            ////                formater += " \"ETA_day\":\"" + Msgs13[i].ETA_day + "\", \"ETA_hour\":\"" + Msgs13[i].ETA_hour + "\", \"ETA_minute\":\"" + Msgs13[i].ETA_minute + "\", ";
            ////                formater += " \"ETA_month\":\"" + Msgs13[i].ETA_month + "\", \"IMONumber\":\"" + Msgs13[i].IMONumber + "\", \"PositionFixType\":\"" + Msgs13[i].PositionFixType + "\", ";
            ////                formater += " \"ShipType\":\"" + Msgs13[i].ShipType + "\", \"VesselName\":\"" + Msgs13[i].VesselName + "\", \"CallSign\":\"" + Msgs13[i].CallSign + "\" } } ";
            ////            }
            ////            else
            ////            {
            ////                formater = "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Point\", \"coordinates\": [" + Msgs13[i].Longitude + "," + Msgs13[i].Latitude + "] }, ";
            ////                formater += " \"properties\": { \"MsgType\":\"" + Msgs13[i].MsgType + "\", \"recordedtime\":\"" + time + "\", ";
            ////                formater += " \"RepeatIndicator\":\"" + Msgs13[i].RepeatIndicator + "\", \"MMSI\":\"" + Msgs13[i].MMSI + "\", ";
            ////                formater += " \"NavigationStatus\":\"" + Msgs13[i].NavigationStatus + "\", \"RateOfTurn_ROT\":\"" + Msgs13[i].RateOfTurn_ROT + "\", \"SpeedOverGround_SOG\":\"" + Msgs13[i].SpeedOverGround_SOG + "\", ";
            ////                formater += " \"TimeStamp\":\"" + Msgs13[i].TimeStamp + "\", \"ManeuverIndicator\":\"" + Msgs13[i].ManeuverIndicator + "\", \"Spare\":\"" + Msgs13[i].Spare + "\", ";
            ////                formater += " \"RAIMFlag\":\"" + Msgs13[i].RAIMFlag + "\", \"RadioStatus\":\"" + Msgs13[i].RadioStatus + "\", ";
            ////                formater += " \"PositionAccuracy\":\"" + Msgs13[i].PositionAccuracy + "\", \"CourseOverGround_COG\":\"" + Msgs13[i].CourseOverGround_COG + "\", \"TrueHeading_HDG\":\"" + Msgs13[i].TrueHeading_HDG + "\", ";
            ////                formater += " \"VesselNameBG\":\"" + Msgs13[i].VesselNameBG + "\", \"AISVersioin\":\"" + Msgs13[i].AISVersioin + "\", \"DTE\":\"" + Msgs13[i].DTE + "\", ";
            ////                formater += " \"Destination\":\"" + Msgs13[i].Destination + "\", \"DimensionToBow\":\"" + Msgs13[i].DimensionToBow + "\", \"DimensionToPort\":\"" + Msgs13[i].DimensionToPort + "\", ";
            ////                formater += " \"DimensionToStarboard\":\"" + Msgs13[i].DimensionToStarboard + "\", \"DimensionToStern\":\"" + Msgs13[i].DimensionToStern + "\", \"Draught\":\"" + Msgs13[i].Draught + "\", ";
            ////                formater += " \"ETA_day\":\"" + Msgs13[i].ETA_day + "\", \"ETA_hour\":\"" + Msgs13[i].ETA_hour + "\", \"ETA_minute\":\"" + Msgs13[i].ETA_minute + "\", ";
            ////                formater += " \"ETA_month\":\"" + Msgs13[i].ETA_month + "\", \"IMONumber\":\"" + Msgs13[i].IMONumber + "\", \"PositionFixType\":\"" + Msgs13[i].PositionFixType + "\", ";
            ////                formater += " \"ShipType\":\"" + Msgs13[i].ShipType + "\", \"VesselName\":\"" + Msgs13[i].VesselName + "\", \"CallSign\":\"" + Msgs13[i].CallSign + "\" } }, ";
            ////            }
            ////            json += formater;
            ////        }

            ////        json += " ]} ";
            ////    }

            ////    return json;

            ////}
            ////catch (Exception ex)
            ////{
            ////    THelp.ConsoleWriteLine("ExportGeoJson ex: " + ex.Message);
            ////    return "";
            ////}
        }

        #endregion

       
        public static decimal ConvertELBCoordsToCoords(int coord, bool islat)
        {
            try
            {
                return new decimal(coord / 1000.0 / 60.0);
            }
            catch (Exception)
            {
                if (islat)
                    return 90;
                else
                    return 180;
            }
        }

        #region ISS old system 


        public static long PageNumEnq(int shortpagenum, long certnum)
        {
            //long longnum = (certnum * 10000000) + shortpagenum;
            long longnum = (certnum * 1000000) + shortpagenum;
            return longnum;
        }

        public static int PageNumDec(long longpagenum)
        {
            //var pn = longpagenum % 10000000;
            var pn = longpagenum % 1000000;

            return (int)pn;
        }

        public static long ClearCertificateNumber(string num)
        {
            long res = -1;
            try
            {
                num = GetNumbers(num);
                long.TryParse(num, out res);
            }
            catch (Exception)
            {

            }

            return res;
        }

        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }


        public static string ISSUser { get; set; }
        public static string ISSToken { get; set; }
        public static string ISSPass { get; set; }
        public static  string ISSUrl { get; set; }
        public static int ISSFgUpInterval { get; set; }
        public static int ISSCertUpInterval { get; set; }
        public static int ISSPermUpInterval { get; set; }
        public static string MethodFGears { get; set; }
        public static string MethodCerts { get; set; }
        public static string MethodPerms { get; set; }
        public static string MethodLogin { get; set; }
        public static string MethodFVName { get; set; }
        public static string MethodFVCFR { get; set; }
        public static string MethodFVMMSI { get; set; }
        public static string MethodFVAll { get; set; }
        public static string MethodISSPortDescList { get; set; }
        public static string MethodISSGearDescList { get; set; }
        public static string MethodISSFishList { get; set; }
        public static string MethodISSPresentDescList { get; set; }
        public static string MethodISSConditionDescList { get; set; }

        /// <summary>
        /// Init
        /// </summary>
        public static void Init()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                // ES log
            }
        }


        #endregion

        #region AES Encrypt Decrypt

        private static string AESKey = "FP453S93L5491WDL8725APLSE667D4W1";

        /// <summary>
        /// Encrypt string function
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(AESKey);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt string function
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(AESKey);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }

        #endregion

        #region UUID Genertor
        /// <summary>
        /// Generate Version 4 UUID
        /// </summary>
        /// <returns></returns>
        public static string Gen_UUID()
        {
            try
            {
                Guid myuuid = Guid.NewGuid();
                return myuuid.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }


        /// <summary>
        /// Convert string to GUID/UUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Guid Convert_StringToUUID(string guid)
        {
            try
            {
                return new Guid(guid);
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        #endregion

        #region Load Data from DB

 

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="cert1"></param>
        /// <param name="cert2"></param>
        /// <returns></returns>
        public static bool CheckEqualApiCerts(ApiCerts cert1, ApiCerts cert2)
        {
            try
            {
                return DeepCompare(cert1, cert2);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Compare two objects
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static bool DeepCompare(this object obj, object another)
        {           
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                if (property.Name == "Id")
                {
                }
                else if (property.Name == "Certificates")
                {
                    //ApiPerm_niss thisobj = (ApiPerm_niss)obj;
                    //ApiPerm_niss anotherobj = (ApiPerm_niss)another;
                    //var res = DeepCompare_ApiPerm_niss(thisobj, anotherobj);
                }
                else
                {

                    var objValue = property.GetValue(obj);
                    var anotherValue = property.GetValue(another);
                    if (objValue == null && anotherValue == null)
                    {
                    }
                    else
                    {
                        if (!objValue.Equals(anotherValue))
                            result = false;
                    }
                }
            }

            return result;
        }

        public static bool DeepCompare_Permit_niss(this Permit_niss obj, Permit_niss another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {

                if (property.Name == "Id")
                {
                }
                else if (property.Name == "Certificates")
                {
                    //if (obj.Certificates != null && obj.Certificates.Count > 0)
                    //{
                    //    foreach (var certniss in obj.Certificates)
                    //    {
                    //        foreach (var dbcert in another.Certificates)
                    //        {
                    //            var res = DeepCompare_Certificates_niss(certniss, dbcert);
                    //        }                            
                    //    }                       
                    //}                    
                }
                else
                {
                    var objValue = property.GetValue(obj);
                    var anotherValue = property.GetValue(another);
                    if (objValue == null && anotherValue == null)
                    {
                    }
                    else
                    {
                        if (!objValue.Equals(anotherValue))
                            result = false;
                    }
                }
            }

            return result;
        }

        //public static bool DeepCompare_Certificates_niss(this Certificate_niss obj, Certificate_niss another)
        //{
        //    if (ReferenceEquals(obj, another)) return true;
        //    if ((obj == null) || (another == null)) return false;
        //    //Compare two object's class, return false if they are difference
        //    if (obj.GetType() != another.GetType()) return false;

        //    var result = true;
        //    //Get all properties of obj
        //    //And compare each other
        //    foreach (var property in obj.GetType().GetProperties())
        //    {
        //        if (property.Name == "Id")
        //        {
        //        }
        //        else if (property.Name == "FishingGears")
        //        {
        //            //ApiPerm_niss thisobj = (ApiPerm_niss)obj;
        //            //ApiPerm_niss anotherobj = (ApiPerm_niss)another;
        //            //var res = DeepCompare(thisobj.Certificates, anotherobj.Certificates);
        //        }
        //        else if (property.Name == "Permit")
        //        { 
        //        }
        //        else
        //        {

        //            var objValue = property.GetValue(obj);
        //            var anotherValue = property.GetValue(another);
        //            if (objValue == null && anotherValue == null)
        //            {
        //            }
        //            else
        //            {
        //                if (!objValue.Equals(anotherValue))
        //                    result = false;
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public static bool DeepCompare_FishingGear_niss(this FishingGear_niss obj, FishingGear_niss another)
        //{
        //    if (ReferenceEquals(obj, another)) return true;
        //    if ((obj == null) || (another == null)) return false;
        //    //Compare two object's class, return false if they are difference
        //    if (obj.GetType() != another.GetType()) return false;

        //    var result = true;
        //    //Get all properties of obj
        //    //And compare each other
        //    foreach (var property in obj.GetType().GetProperties())
        //    {
        //        if (property.Name == "Id")
        //        {
        //        }
        //        else if (property.Name == "Certificates")
        //        {
        //            //ApiPerm_niss thisobj = (ApiPerm_niss)obj;
        //            //ApiPerm_niss anotherobj = (ApiPerm_niss)another;
        //            //var res = DeepCompare(thisobj.Certificates, anotherobj.Certificates);
        //        }
        //        else
        //        {

        //            var objValue = property.GetValue(obj);
        //            var anotherValue = property.GetValue(another);
        //            if (objValue == null && anotherValue == null)
        //            {
        //            }
        //            else
        //            {
        //                if (!objValue.Equals(anotherValue))
        //                    result = false;
        //            }
        //        }
        //    }

        //    return result;
        //}

 
        /// <summary>
        /// Struct
        /// </summary>
        public struct FOCERTSTRUCT {
            /// <summary>
            /// FO id
            /// </summary>
            public long foid;
            /// <summary>
            /// Fishing Certificate
            /// </summary>
            public string certnum;
        }
 
        #endregion

         

        #region ApiCert Check validity
        public static bool ApiCertCheckValidity(ApiCerts cert)
        {
            try
            {
                var dtnow = DateTime.Now;
                if (dtnow >= cert.udo_valid_from && dtnow <= cert.udo_valid_to)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Flux Trip ID Formatter
        /// <summary>
        /// Convert FVMS Trip ID to FLUX Trip ID Convention
        /// ISO3 code of the EU MS, dash, TRP, dash, 1 to 20 alphanumeric characters, including dashes and slashes
        /// ^[A-Z]{3}-TRP-[A-Za-z0-9-/]{1,20}$
        /// </summary>
        /// <param name="intrip"></param>
        /// <returns></returns>
        public static string FluxTripIdFormatter(string intrip)
        {
            //BGR - TRP - Zb2hNaeUy31--XM9shhEQ
            //zIZb2hNaeUy31_XM9shhEQ
            string formatedtripnum = "";
            try
            {
                int counter = 0;
                if (intrip.IndexOf("_") > 0)
                {
                    foreach (var ch in intrip)
                    {
                        if (ch == '_')
                        {
                            formatedtripnum += "--";
                            counter++;
                        }
                        else
                        {
                            formatedtripnum += ch;
                        }
                    }                    
                }
                else
                {
                    formatedtripnum = intrip;
                }
                 
                formatedtripnum = "BGR-TRP-" + formatedtripnum.Remove(0, 2 + counter);                
            }
            catch (Exception)
            {
                formatedtripnum = "";
                if (intrip.IndexOf("_") > 0)
                {
                     
                    formatedtripnum = intrip.Replace("_", "--");
                }
                else
                {
                    formatedtripnum = intrip;
                }
                formatedtripnum = "BGR-TRP-" + formatedtripnum.Remove(0, 2);
                
            }
            return formatedtripnum;
        }

        //public static string FluxTripIdReverseFormatter(string nissintrip)
        //{
        //    //BGR - TRP - Zb2hNaeUy31--XM9shhEQ
        //    //zIZb2hNaeUy31_XM9shhEQ
        //    string formatedtripnum = "";
        //    try
        //    {
        //        int counter = 0;
        //        if (nissintrip.IndexOf("--") > 0)
        //        {
        //            nissintrip = nissintrip.Replace("--", "_");
        //        }
        //        else
        //        {
        //            formatedtripnum = nissintrip;
        //        }

        //        formatedtripnum = "BGR-TRP-" + formatedtripnum.Remove(0, 2 + counter);
        //    }
        //    catch (Exception)
        //    {
        //        formatedtripnum = "";
        //        if (intrip.IndexOf("_") > 0)
        //        {

        //            formatedtripnum = intrip.Replace("_", "--");
        //        }
        //        else
        //        {
        //            formatedtripnum = intrip;
        //        }
        //        formatedtripnum = "BGR-TRP-" + formatedtripnum.Remove(0, 2);

        //    }
        //    return formatedtripnum;
        //}
        #endregion

 
    }
}
