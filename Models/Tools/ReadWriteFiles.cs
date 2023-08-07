using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScortelApi.Models.ScortelELB;

namespace ScortelApi.Tools
{
    /// <summary>
    /// запис и четене на файл съдържащ информация за МО
    /// </summary>
    public class ReadWriteFiles
    {

        private StreamWriter sw = null;
        private String mExePath = "";
        private FileStream mFStream;

        /// <summary>
        /// property
        /// </summary>
        public FileStream FStream
        {
            get
            {
                return mFStream;
            }
            set
            {
                mFStream = value;
            }
        }

        public ReadWriteFiles(String ExePath)
        {
            this.mExePath = ExePath;
        }

        /// <summary>
        /// проверка дали има файл и ако не го създава
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        public bool CheckFileExist(String sFileName/*, out FileStream fstream*/)
        {
            try
            {
                //fstream = File.Open(sFileName,FileMode.CreateNew,FileAccess.Write);
                this.mFStream = File.Open(mExePath + sFileName, FileMode.CreateNew, FileAccess.Write);
                // файлът не съществува и се създава


                return true;
            }
            catch
            {
                this.mFStream = null;
                this.mFStream = File.Open(mExePath + sFileName, FileMode.Truncate, FileAccess.Write);
                // файлът съществува                
                return false;
            }
        }

        /// <summary>
        /// запис на ред в текстовия файл
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="fstream"></param>
        /// <returns></returns>
        public bool WriteLineToFile(String txt, FileStream fstream)
        {
            try
            {
                sw = new StreamWriter(fstream, System.Text.Encoding.UTF8);
                if (txt.Length == 0)
                {
                    sw.Close();
                    sw = null;
                    // няма какво да записваме                    
                    return false;
                }
                else
                {
                    // запис на един ред
                    sw.WriteLine(txt);
                    sw.Close();
                    sw = null;
                    return true;
                }
            }
            catch
            {
                sw.Close();
                sw = null;
                return false;
            }
        }

        /// <summary>
        /// запис в текстов файл
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public bool WriteTextToFile(StringBuilder sb, FileStream fs)
        {
            try
            {
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.Write(sb.ToString());
                sw.Close();
                sw = null;
                return true;
            }
            catch
            {

                sw = null;
                return false;
            }
        }

        /// <summary>
        /// прочитане на текстов файл
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="count"></param>
        /// <returns>връща се масив от стрингове - редове</returns>
        public String[] ReadTextFromFile(String sFileName, out int count)
        {
            try
            {
                String[] strArr = new String[1000];
                try
                {
                    int counter = 0;
                    using (StreamReader sr = new StreamReader(mExePath + sFileName))
                    {
                        String line = "";                        
                        while ((line = sr.ReadLine()) != null)
                        {
                            strArr[counter] = line;
                            counter++;
                        }
                    }
                    count = counter;
                    return strArr;
                }
                catch
                {
                    count = 0;
                    return System.Array.Empty<String>(); //new String[0];  
                } // end inner try
            }
            catch
            {
                count = 0;
                return System.Array.Empty<String>(); //new String[0];
            }

        }

        /// <summary>
        /// парсване на стринга прочетен от всеки ред на текстовия файл
        /// </summary>
        /// <param name="textRow"></param>
        /// <returns></returns>
        private Vessel ParseFileString(String textRow)
        {
            //2;SIM_K1;True;NaviFleet
            try
            {
                String[] splitArr = textRow.Split(';');
                return new Vessel()
                {
                    //TODO:
                    Id = int.Parse(splitArr[0]),
                    moindex = int.Parse(splitArr[1]),
                    Regnum = splitArr[2],
                    Selected = bool.Parse(splitArr[3]),
                    typecode = splitArr[4],
                    NFRowID = int.Parse(splitArr[5]),
                    LastTime = DateTime.Parse(splitArr[6]),
                    EngState = int.Parse(splitArr[7]),
                    PrevRecDT = DateTime.Parse(splitArr[8]),
                    MoLastLat = double.Parse(splitArr[9]),
                    MoLastLong = double.Parse(splitArr[10]),
                    WialonId = splitArr[11]
                };

            }
            catch (Exception ex)
            {
                String err_str = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// прочитане на МО от файл
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="ExePath"></param>
        /// <returns></returns>
        public Vessel[] GetVesselsFromFile(String sFileName, String ExePath)
        {
            try
            {
                int count = 0;

                ReadWriteFiles FileOps = new ReadWriteFiles(ExePath);
                String[] tmpArr = FileOps.ReadTextFromFile(sFileName, out count);

                List<Vessel> ResArr = new List<Vessel>();

                for (int i = 0; i < count; i++)
                {
                    Vessel obj = new Vessel();
                    if (ParseFileString(tmpArr[i]) != null)
                    {
                        obj = ParseFileString(tmpArr[i]);
                        ResArr.Add(obj);
                    }
                    else
                    {
                        //String err = "dadsas";
                    }
                }
                return (Vessel[])ResArr.ToArray();
            }
            catch (Exception ex)
            {
                String err_str = ex.Message;
                return null;
            }
        }
    }
}
