using System;
using System.Collections.Generic;
using ScortelApi.Models.FLUX;
using ScortelApi.Models.FLUX.Noms;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Validation.FluxDomainsValidation.FluxVesselDomainValidation
{
    class VesselDomainValidation
    {
        public void VesselDomainValidate(string strWorkPath)
        {

            #region Vessel Domain

            #region VesselReport
            string filePathVesselReport = strWorkPath + @"\FluxReports\VesselReport.xml";
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(filePathVesselReport))
            {
                xmlDoc.Load(filePathVesselReport);
            }

            XDocument xdoc = XDocument.Load(filePathVesselReport);
            string jsonText = JsonConvert.SerializeXNode(xdoc);
            //var sdas  = xdoc.ToString();
            var stringReader = new System.IO.StringReader(xdoc.ToString());
            var serializer = new XmlSerializer(typeof(FLUXReportVesselInformationType));
            FLUXReportVesselInformationType VesselReport = serializer.Deserialize(stringReader) as FLUXReportVesselInformationType;
            #endregion VesselReport

            #region read csv Vessel BRules
            string fPathVesselBRules = strWorkPath + @"\FluxReports\VESSELBRDEF.csv";
            List<Vessel_MDR_Vessel_BR_Def> VesselBrDef = new List<Vessel_MDR_Vessel_BR_Def>();
            StreamReader reader = null;
            if (File.Exists(fPathVesselBRules))
            {
                reader = new StreamReader(File.OpenRead(fPathVesselBRules));
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count != 0)
                    {

                        var values = line.Split('#');
                        Vessel_MDR_Vessel_BR_Def def = new Vessel_MDR_Vessel_BR_Def();
                        for (int i = 0; i < values.Length; i++)
                        {

                            switch (i)
                            {
                                case 0: // id
                                    int idt = 0;
                                    int.TryParse(values[0], out idt);
                                    def.ID = idt;
                                    break;
                                case 1: // code
                                    def.Code = values[1];
                                    break;
                                case 2: // endescription
                                    def.EnDescription = values[2];
                                    break;
                                case 3: //
                                    def.ValidFrom = DateTime.Parse(values[3]);
                                    break;
                                case 4: // 
                                    def.ValidTo = DateTime.Parse(values[4]);
                                    break;
                                case 5:
                                    def.CreatedBy = values[5];
                                    break;
                                case 6:
                                    def.UpdatedBy = values[6];
                                    break;
                                case 7:
                                    def.CreatedOn = DateTime.Parse(values[7]);
                                    break;
                                case 8:
                                    try
                                    {
                                        def.UpdatedOn = DateTime.Parse(values[8]);
                                    }
                                    catch (Exception)
                                    {
                                        def.UpdatedOn = new DateTime(1970, 1, 1);
                                    }

                                    break;
                                case 9:
                                    try
                                    {
                                        def.startdate = values[9];
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    break;
                                case 10:
                                    def.enddate = values[10];
                                    break;
                                case 11:
                                    def.brlevel = values[11];
                                    break;
                                case 12:
                                    def.BRSubLevel = values[12];
                                    break;
                                case 13:
                                    def.Field = values[13];
                                    break;
                                case 14:
                                    def.EnMessage = values[14];
                                    break;
                                case 15:
                                    def.brlevel = values[15];
                                    break;
                            }


                        }
                        VesselBrDef.Add(def);
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

            #endregion

            #endregion Vessel Domain


            foreach (var rule in VesselBrDef)
            {
                switch (rule.Code)
                {
                    case "VESSEL-L00-00-0001":

                        if (VesselReport != null)
                        {
                            VesselReportValidation vesselReportValidation = new VesselReportValidation();
                            vesselReportValidation.VesselReportValidate(VesselReport: VesselReport);
                        }
                        else
                        {
                            System.Console.WriteLine("No VesselReport provided");
                        }

                        break;
                }
            }
        }
    }
}
