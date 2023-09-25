using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ScortelApi.Models.FLUX;
using ScortelApi.Models.FLUX.Noms;

namespace Validation.FluxDomainsValidation.FluxACDRDomainValidation
{
    class ACDRDomainValidation
    {
        public void ACDRDomainValidate(string strWorkPath)
        {

            #region ACDR Domain

            #region AcdrReport
            string filePathAcdrReport = strWorkPath + @"\FluxReports\ACDRReportFishingCategoryNew.xml";
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(filePathAcdrReport))
            {
                xmlDoc.Load(filePathAcdrReport);
            }

            XDocument xdoc = XDocument.Load(filePathAcdrReport);

            var stringReader = new StringReader(xdoc.ToString());
            var serializer = new XmlSerializer(typeof(FLUXACDRReportType));
            FLUXACDRReportType AcdrReportType = serializer.Deserialize(stringReader) as FLUXACDRReportType;
            #endregion AcdrReport

            #region AcdrMessageType - Response?
            string filePathAcdrResponse = strWorkPath + @"\FluxReports\SalesResponse.xml";
            xmlDoc = new XmlDocument();

            if (File.Exists(filePathAcdrResponse))
            {
                xmlDoc.Load(filePathAcdrResponse);
            }

            xdoc = XDocument.Load(filePathAcdrResponse);

            stringReader = new StringReader(xdoc.ToString());
            serializer = new XmlSerializer(typeof(FLUXACDRMessageType));
            FLUXACDRMessageType AcdrMessageType = serializer.Deserialize(stringReader) as FLUXACDRMessageType;
            #endregion AcdrMessageType - Response?

            #region ACDR Query and BRDEF - for deletion
            /*
            #region AcdrMessageType1 - no such message?
            string filePathAcdrQuery = strWorkPath + @"\FluxReports\SalesQuery.xml";
            xmlDoc = new XmlDocument();

            if (File.Exists(filePathAcdrQuery))
            {
                xmlDoc.Load(filePathAcdrQuery);
            }

            xdoc = XDocument.Load(filePathAcdrQuery);

            stringReader = new StringReader(xdoc.ToString());
            serializer = new XmlSerializer(typeof(FLUXACDRMessageType1));
            FLUXACDRMessageType1 AcdrMessageType1 = serializer.Deserialize(stringReader) as FLUXACDRMessageType1;
            #endregion AcdrMessageType1 - no such message?
            */

            /*
            #region read csv Sales BRules - no such file?
            string fPathSalesBRules = strWorkPath + @"\FluxReports\SALESBRDEF.csv";
            List<Sales_MDR_Sales_BR_Def> SalesBrDef = new List<Sales_MDR_Sales_BR_Def>();
            StreamReader reader = null;
            if (File.Exists(fPathSalesBRules))
            {
                reader = new StreamReader(File.OpenRead(fPathSalesBRules));
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count != 0)
                    {

                        var values = line.Split('#');
                        Sales_MDR_Sales_BR_Def def = new Sales_MDR_Sales_BR_Def();
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
                                    def.BR = values[11];
                                    break;
                                case 12:
                                    def.Level = values[12];
                                    break;
                                case 13:
                                    def.SubLevel = values[13];
                                    break;
                                case 14:
                                    def.Field = values[14];
                                    break;
                                case 15:
                                    def.EnMessage = values[15];
                                    break;
                            }
                        }
                        SalesBrDef.Add(def);
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

            #endregion read csv Sales BRules - no such file?
            */
            #endregion ACDR Query and BRDEF - for deletion

            #endregion ACDR Domain

            //AcdrReportType = null;
            //AcdrMessageType = null;
            //AcdrMessageType1 = null;

            if (AcdrReportType != null)
            {
                Console.WriteLine("AcdrReportType: " + AcdrReportType);
            }
        }
    }
}
