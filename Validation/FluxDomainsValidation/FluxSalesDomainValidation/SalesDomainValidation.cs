using ScortelApi.Models.FLUX;
using ScortelApi.Models.FLUX.Noms;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Validation.FluxDomainsValidation.FluxSalesDomainValidation;

namespace Validation
{
    class SalesDomainValidation
    {
        public void SalesDomainValidate(string strWorkPath)
        {

            #region Sales Domain

            #region SalesReport
            string filePathSalesReport = strWorkPath + @"\FluxReports\Samples_Sales_SN_Report_Message_Normal.xml";
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(filePathSalesReport))
            {
                xmlDoc.Load(filePathSalesReport);
            }

            XDocument xdoc = XDocument.Load(filePathSalesReport);
            string jsonText = JsonConvert.SerializeXNode(xdoc);
            //var sdas  = xdoc.ToString();
            var stringReader = new System.IO.StringReader(xdoc.ToString());
            var serializer = new XmlSerializer(typeof(FLUXSalesReportMessageType));
            FLUXSalesReportMessageType SalesReport = serializer.Deserialize(stringReader) as FLUXSalesReportMessageType;
            #endregion SalesReport

            #region SalesQuery
            string filePathSalesQuery = strWorkPath + @"\FluxReports\SalesQuery.xml";
            xmlDoc = new XmlDocument();

            if (File.Exists(filePathSalesQuery))
            {
                xmlDoc.Load(filePathSalesQuery);
            }

            xdoc = XDocument.Load(filePathSalesQuery);

            stringReader = new System.IO.StringReader(xdoc.ToString());
            serializer = new XmlSerializer(typeof(FLUXSalesQueryMessageType));
            FLUXSalesQueryMessageType SalesQuery = serializer.Deserialize(stringReader) as FLUXSalesQueryMessageType;
            #endregion SalesQuery

            #region SalesResponse
            string filePathSalesResponse = strWorkPath + @"\FluxReports\SalesResponse.xml";
            xmlDoc = new XmlDocument();

            if (File.Exists(filePathSalesResponse))
            {
                xmlDoc.Load(filePathSalesResponse);
            }

            xdoc = XDocument.Load(filePathSalesResponse);

            stringReader = new System.IO.StringReader(xdoc.ToString());
            serializer = new XmlSerializer(typeof(FLUXSalesResponseMessageType));
            FLUXSalesResponseMessageType SalesResponse = serializer.Deserialize(stringReader) as FLUXSalesResponseMessageType;
            #endregion SalesResponse

            #region read csv Sales BRules
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

            #endregion

            #endregion Sales Domain

            //SalesReport = null;
            //SalesQuery = null;
            //SalesResponse = null;

            foreach (var rule in SalesBrDef)
            {
                //#Q Changed switch(rule.Code) to switch(rule.BR) as there is nothing in col 2 in SALESBRDEF.csv and business rules codes are in column 11 - BR 
                switch (rule.BR)
                {
                    case "SALE-L00-00-0001":

                        //SALE-L00-00-0001, SALE-L02-00-0001
                        #region SalesReport
                        if (SalesReport != null) 
                        {
                            SalesReportValidation salesReportValidation = new SalesReportValidation();
                            salesReportValidation.SalesReportValidate(SalesReport: SalesReport);
                        }
                        else
                        {
                            Console.WriteLine("SalesReport == null");
                        }
                        #endregion SalesReport

                        break;

                    case "SALE-L00-00-0400":

                        #region SalesQuery
                        if (SalesQuery != null) 
                        {
                            SalesQueryValidation salesQueryValidation = new SalesQueryValidation();
                            salesQueryValidation.SalesQueryValidate(SalesQuery: SalesQuery);
                        }
                        else
                        {
                            Console.WriteLine("SalesQuery == null");
                        }
                        #endregion SalesQuery

                        break;

                    case "SALE-L00-00-0470":

                        #region SalesResponse
                        if (SalesResponse != null) 
                        {
                            SalesResponseValidation salesResponseValidation = new SalesResponseValidation();
                            salesResponseValidation.SalesResponseValidate(SalesResponse: SalesResponse);
                        }
                        else
                        {
                            Console.WriteLine("SalesResponse == null");
                        }
                        #endregion SalesResponse

                        break;

                }
            }
        }
    }
}
