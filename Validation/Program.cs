﻿using System;
using ScortelApi.Models.FLUX;
using ScortelApi.Models.FLUX.Noms;
using ScortelApi;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Xml.Schema; 
using ScortelApi.ISSN;

namespace Validation
{
    class Program
    {
        static void Main(string[] args)
        {
        

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            //Switch Off FA Domain debug
            bool FADomainDebug = false;
            if (FADomainDebug)
            {
                #region FA Domain
                #region FAReport
                string filePathFAReport = strWorkPath + @"\FluxReports\FAReport1.xml";
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(filePathFAReport))
            {
                xmlDoc.Load(filePathFAReport);
            }

            XDocument xdoc = XDocument.Load(filePathFAReport);
            string jsonText = JsonConvert.SerializeXNode(xdoc);
            //var sdas  = xdoc.ToString();
            var stringReader = new System.IO.StringReader(xdoc.ToString());
            var serializer = new XmlSerializer(typeof(FLUXFAReportMessageType));
            FLUXFAReportMessageType FAreport = serializer.Deserialize(stringReader) as FLUXFAReportMessageType;


            #region check for valid xml -> 
            // using (FileStream stream = File.OpenRead(xsdFilepath))
            //{
            //    XmlReaderSettings settings = new XmlReaderSettings();

            //    XmlSchema schema = XmlSchema.Read(stream, OnXsdSyntaxError);
            //    settings.ValidationType = ValidationType.Schema;
            //    settings.Schemas.Add(schema);
            //    settings.ValidationEventHandler += OnXmlSyntaxError;

            //    using (XmlReader validator = XmlReader.Create(xmlPath, settings))
            //    {
            //        // Validate the entire xml file
            //        while (validator.Read()) ;
            //    }
            //}
            //// The OnXmlSyntaxError function will be called when a syntax error occur.
            #endregion

            #region read csv FA BRules
            string fPathFABRules = strWorkPath + @"\FluxReports\FABRDEF.csv";
            List<FA_MDR_FA_BR_Def> FaBrDef = new List<FA_MDR_FA_BR_Def>();
            StreamReader reader = null;
            if (File.Exists(fPathFABRules))
            {
                reader = new StreamReader(File.OpenRead(fPathFABRules));
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (count != 0)
                    {
                       
                        var values = line.Split('#');
                        FA_MDR_FA_BR_Def def = new FA_MDR_FA_BR_Def();
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
                                        def.UpdatedOn = new DateTime(1970,1,1);
                                    }
                                  
                                    break;
                                case 9:
                                    def.BRLevel = values[9];
                                    break;
                                case 10:
                                    def.BRSubLevel = values[10];
                                    break;
                                case 11:
                                    def.Field = values[11];
                                    break;
                                case 12 :
                                    def.ENMessage = values[12];
                                    break;
                            
                            }

                            
                        }
                        FaBrDef.Add(def);
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }

            #endregion

            #endregion

                #region FAQuery
                string filePathFAQuery = strWorkPath + @"\FluxReports\FAQuery3.xml";
                xmlDoc = new XmlDocument();

                if (File.Exists(filePathFAQuery))
                {
                    xmlDoc.Load(filePathFAQuery);
                }

                 xdoc = XDocument.Load(filePathFAQuery);
             
                stringReader = new System.IO.StringReader(xdoc.ToString());
                 serializer = new XmlSerializer(typeof(FLUXFAQueryMessageType));
                FLUXFAQueryMessageType faquery = serializer.Deserialize(stringReader) as FLUXFAQueryMessageType;





                #endregion

                #region FAResponse
                string filePathFAResp = strWorkPath + @"\FluxReports\FAResp.xml";
                xmlDoc = new XmlDocument();

                if (File.Exists(filePathFAResp))
                {
                    xmlDoc.Load(filePathFAResp);
                }

                xdoc = XDocument.Load(filePathFAResp);

                stringReader = new System.IO.StringReader(xdoc.ToString());
                serializer = new XmlSerializer(typeof(FLUXResponseMessageType));
                FLUXResponseMessageType FAresp = serializer.Deserialize(stringReader) as FLUXResponseMessageType;


                #endregion

            #endregion

                //XElement element = xdoc.XPathSelectElement("FLUXFAReportMessage");

                #region prev checks
                bool IsFFRMT = false;
                bool IsFRD = false;
                bool IsFRD_Id = false;
                bool IsFRD_CreationDT = false;
                bool IsFRD_PCode = false;
                bool IsFRD_RefId = false;
                bool IsFRD_OFP = false;
                bool IsFRD_OFP_Id = false;
                string FRD_OFP_IdValue = "";
                //
                bool IsFARD = false;
                bool IsFAQuaery = false;
                bool IsFAResp = false;
                bool IsVReport = false;

                //bool IsFARD_TCode = false;
                //bool IsFARD_RFluxRD = false;
                //bool IsFARD_RFluxRD_PCode = false;
                if (FAreport != null)
                {
                    IsFFRMT = true;
                    if (FAreport.FLUXReportDocument != null)
                    {
                        IsFRD = true;
                        if (FAreport.FLUXReportDocument.ID != null)
                        {
                            IsFRD_Id = true;
                        }
                        if (FAreport.FLUXReportDocument.CreationDateTime != null)
                        {
                            IsFRD_CreationDT = true;
                        }
                        if (FAreport.FLUXReportDocument.PurposeCode != null)
                        {
                            IsFRD_PCode = true;
                        }
                        if (FAreport.FLUXReportDocument.ReferencedID != null)
                        {
                            IsFRD_RefId = true;
                        }
                        if (FAreport.FLUXReportDocument.OwnerFLUXParty != null)
                        {
                            IsFRD_OFP = true;
                            if (FAreport.FLUXReportDocument.OwnerFLUXParty.ID != null)
                            {
                                IsFRD_OFP_Id = true;
                            }
                        }
                    }                 
                }
                if (faquery != null)
                {
                    IsFAQuaery = true;
                }
                if (FAresp != null)
                {
                    IsFAResp = true;
                }
                //if (vreport != null)
                //{
                //    IsVReport = true;
                //}

                //var L0List = FaBrDef.Where(x => x.Code.IndexOf("L00") > 0).ToList();
                //var L1List = FaBrDef.Where(x => x.Code.IndexOf("L01") > 0).ToList();
                //var L2List = FaBrDef.Where(x => x.Code.IndexOf("L02") > 0).ToList();
                //var L3List = FaBrDef.Where(x => x.Code.IndexOf("L03") > 0).ToList();
                #endregion

                //TODO: zarejdaneto na pravilata da stava pri startirane na api i da se dyrvat kato struktura w pametta
            
                foreach (var rule in FaBrDef)
                {
                    switch (rule.Code)
                    {
                        case "FA-L00-00-0000":
                            #region Check XML Schemma - FAReportMessageType
                            using (FileStream stream = File.OpenRead(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\FLUXFAReportMessage_3p1.xsd"))
                            {

                                XmlReaderSettings settings = new XmlReaderSettings();

                                //XmlSchema schema = XmlSchema.Read(stream, OnXsdSyntaxError);
                                XmlSchema schema = XmlSchema.Read(stream, Settings_ValidationEventHandler);
                                settings.ValidationType = ValidationType.Schema;
                                settings.Schemas.Add(schema);

                                FileStream stream1 = File.OpenRead(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\ReusableAggregateBusinessInformationEntity_20p0.xsd");
                                XmlSchema schema1 = XmlSchema.Read(stream1, Settings_ValidationEventHandler);
                                settings.Schemas.Add(schema1);

                                FileStream stream2 = File.OpenRead(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\UnqualifiedDataType_20p0.xsd");
                                XmlSchema schema2 = XmlSchema.Read(stream2, Settings_ValidationEventHandler);
                                settings.Schemas.Add(schema2);

                                FileStream stream3 = File.OpenRead(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\QualifiedDataType_20p0.xsd");
                                XmlSchema schema3 = XmlSchema.Read(stream3, Settings_ValidationEventHandler);
                                settings.Schemas.Add(schema3);


                                FileStream stream4 = File.OpenRead(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\codelist_standard_UNECE_CommunicationMeansTypeCode_D16A.xsd");
                                XmlSchema schema4 = XmlSchema.Read(stream4, Settings_ValidationEventHandler);
                                settings.Schemas.Add(schema4);

                                settings.ValidationEventHandler += Settings_ValidationEventHandler;

                                using (XmlReader validator = XmlReader.Create(@"D:\Projects\Projects\Fishery\Projects\Api\ScortelApi\Validation\bin\Debug\net5.0\FluxReports\FAReport1.xml", settings))
                                {
                                    // Validate the entire xml file
                                    while (validator.Read()) ;

                                }
                            }
                            #endregion
                            break; ;
                        case "FA-L00-00-0001":
                            //case "FA-L01-00-0002":
                            //case "FA-L01-00-0003":
                            //case "FA-L03-00-0004":
                            #region
                            if (IsFRD)
                            {
                                if (IsFRD_Id)
                                {
                                    // FA-L00-00-0001 Check presence. Must be present.
                                    IDType[] idtype = FAreport.FLUXReportDocument.ID;
                                    foreach (var idtmp in idtype)
                                    {
                                        //1. check if id has value
                                        if (!string.IsNullOrEmpty(idtmp.Value))
                                        {
                                            //FA-L00-00-0001 - ok
                                            // for debug
                                            //string checkid = idtmp.Value;

                                            //3. check case insensitive ???
                                            if (idtmp.Value.ToLower() == idtmp.Value)
                                            {
                                                // FA-L01-00-0003 - ok
                                            }
                                            else
                                            {
                                                // FA-L01-00-0003 - error
                                            }

                                            //4. FA-L03-00-0004 The identification must be unique and not already exis
                                            // check database for receved id

                                        }
                                        else
                                        {
                                            // FA-L00-00-0001 - error
                                            // FA-L01-00-0003 - error
                                        }

                                        //2. check schemma
                                        if (!string.IsNullOrEmpty(idtmp.schemeID))
                                        {

                                            string checkid_schemma = idtmp.schemeID;
                                            if (checkid_schemma == "UUID")
                                            {
                                                // FA-L01-00-0002 - ok
                                            }
                                            else
                                            {
                                                // FA-L01-00-0002 - error
                                            }
                                        }
                                        else
                                        {
                                            // FA-L01-00-0002 - error
                                        }
                                    }
                                }
                                else
                                {
                                    //error
                                }
                            }
                            else
                            {
                                // error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0005":
                            //case "FA-L01-00-0006":
                            //case "FA-L03-00-0007":
                            #region
                            if (IsFRD_CreationDT)
                            {
                                var crdatetime = FAreport.FLUXReportDocument.CreationDateTime;
                                //1. Check presence. Must be present.
                                if (crdatetime.Item != null)
                                {
                                    // FA-L00-00-0005 - ok

                                    //2. FA-L01-00-0006 Check Format. Must be according to the definition provided in 7.1(2). ???
                                    // 2023-07-18T17:05:43.373Z
                                    // FA-L01-00-0006 - ok

                                    //3.FA-L03-00-0007 Date must be in the past.
                                    DateTime dtutc = DateTime.UtcNow;
                                    TimeSpan ts = dtutc - crdatetime.Item;
                                    if (ts.TotalSeconds > 10 * 60)
                                    {
                                        // FA-L03-00-0007 - warning
                                    }
                                }
                                else
                                {
                                    // FA-L00-00-0005 - error
                                    // FA-L01-00-0006 - error
                                }
                            }
                            else
                            {
                                // FA-L00-00-0005 - error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0008":
                            //case "FA-L01-00-0009":
                            //case "FA-L01-00-0010":
                            #region
                            //FLUXFAReportMessage/FLUXReportDocument/PurposeCode
                            if (IsFRD_PCode)
                            {
                                var pcode = FAreport.FLUXReportDocument.PurposeCode;
                                //Check presence. Must be present.
                                if (string.IsNullOrEmpty(pcode.Value))
                                {
                                    //FA-L00-00-0008 - error
                                }
                                //Check attribute listID.Must be FLUX_GP_PURPOSE
                                if (pcode.listID != "FLUX_GP_PURPOSE")
                                {
                                    //FA-L01-00-0009 - error
                                }
                                //Check code.Must be value 9(original data)
                                if (pcode.Value != "9")
                                {
                                    //FA-L01-00-0010 - error
                                }
                            }
                            else
                            {
                                //FA-L00-00-0008 - error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0011":
                            //case "FA-L01-00-0012":
                            //case "FA-L03-00-0013":
                            #region
                            if (IsFRD_RefId)
                            {
                                //FLUXFAReportMessage/FLUXReportDocument/ReferencedID
                                var fdocrefid = FAreport.FLUXReportDocument.ReferencedID;
                                //1. FA-L00-00-0011 Check attribute schemeID.Must be UUID.
                                if (fdocrefid != null)
                                {
                                    /// previous document
                                    if (fdocrefid.schemeID != "UUID")
                                    {
                                        // FA-L00-00-0011 - error
                                    }
                                    else
                                    {
                                        //2. FA-L01-00-0012 Check Format. Must be according to the specified schemeID.
                                        if (fdocrefid.Value.ToLower() != fdocrefid.Value)
                                        {
                                            // FA-L01-00-0012 - error
                                        }

                                        //3. FA-L03-00-0013  - The identification must exist for a FLUXFAQueryMessage
                                        //TODO: check in DB if there is previous FLUXFAQueryMessage with such UUID
                                    }
                                }
                                else
                                {
                                    // no previous document
                                    // FA-L00-00-0011 - error
                                }

                            }
                            else
                            {
                                // FA-L00-00-0011 - error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0014":
                            //case "FA-L01-00-0015":
                            //case "FA-L01-00-0627":
                            //case "FA-L03-00-0016":
                            #region
                            //FLUXFAReportMessage/FLUXReportDocument/OwnerFLUXParty/ID 

                            if (IsFRD_OFP && IsFRD_OFP_Id)
                            {
                                // 1. FA-L00-00-0014 - Check presence. Must be present - ok

                                foreach (var ids in FAreport.FLUXReportDocument.OwnerFLUXParty.ID)
                                {
                                    // 2. FA-L01-00-0015 - Check attribute schemeID. Must be FLUX_GP_PARTY
                                    if (!string.IsNullOrEmpty(ids.schemeID))
                                    {
                                        if (ids.schemeID != "FLUX_GP_PARTY")
                                        {
                                            // FA-L01-00-0015 - error
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0015 - error
                                    }
                                    // 3. FA-L01-00-0627 - "Check ID value. Must be existing in the list specified in 
                                    //attribute schemeID."

                                    FRD_OFP_IdValue = ids.Value;
                                    if (!string.IsNullOrEmpty(ids.Value))
                                    {
                                        //TODO: check db nomenclatures
                                    }
                                    else
                                    {
                                        // FA-L01-00-0627 - error
                                    }
                                    // 4. FA-L03-00-0016 - The party sending the message must be the same as the one from the
                                    // FR value of the FLUX TL envelope. Only the part before the first colon is to be
                                    // considered: Eg. ABC:something => only ABC refres to the party for the purpose of this rule.
                                }
                            }
                            else
                            {
                                // FA-L00-00-0014 - error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0017":
                            #region
                            if (!IsFARD)
                            {
                                // FA-L00-00-0017 - error
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0020":
                            //case "FA-L01-00-0021":
                            //case "FA-L01-00-0022":
                            #region
                            //FAReportDocument/TypeCode, 
                            //FAReportDocument / RelatedFLUXReportDocument / PurposeCode
                            if (IsFARD)
                            {
                                //1. FA-L00-00-0020 - Check presence. Must be present in case of a new report or a correction.
                                foreach (var fardoc in FAreport.FAReportDocument)
                                {
                                    if (fardoc.TypeCode != null)
                                    {
                                        // ok

                                        //2. "FA-L01-00-0021":
                                        //FAReportDocument/TypeCode
                                        //Check attribute listID. Must be FLUX_FA_REPORT_TYPE
                                        if (fardoc.TypeCode.listID != null && fardoc.TypeCode.listID == "FLUX_FA_REPORT_TYPE")
                                        {
                                            //FA-L01-00-0021 - ok 

                                        }
                                        else
                                        {
                                            //FA-L01-00-0021 - error  
                                        }
                                    }
                                    else
                                    {
                                        // FA-L00-00-0020 - error
                                    }

                                    //3. "FA-L01-00-0022 - Check code. Must be existing in the list specified in attribute listID.
                                    if (fardoc.RelatedFLUXReportDocument != null && fardoc.RelatedFLUXReportDocument.PurposeCode != null)
                                    {
                                        if (fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "9" ||
                                            fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "5")
                                        {
                                            // FA-L01-00-0022 - ok
                                        }
                                        else
                                        {
                                            //FA-L01-00-0022 - error
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0022 - error
                                    }

                                    #region "FA-L00-00-0023", "FA-L01-00-0024"
                                    //1. FA-L00-00-0023 Check presence of attribute schemeID. Must be present
                                    if (fardoc.RelatedReportID != null)
                                    {
                                        foreach (var rrid in fardoc.RelatedReportID)
                                        {
                                            if (string.IsNullOrEmpty(rrid.schemeID))
                                            {
                                                // FA-L00-00-0023 - error
                                            }
                                            else
                                            {
                                                //2. FA-L01-00-0024  - Identifier must comply to the schemeID rules
                                                //???
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // FA-L00-00-0023 - error
                                    }
                                    #endregion

                                    #region "FA-L00-00-0025", "FA-L01-00-0026", "FA-L03-00-0027"
                                    //FAReportDocument/AcceptanceDateTime

                                    if (fardoc.AcceptanceDateTime != null)
                                    {
                                        //1. FA-L00-00-0025 - Check presence. Must be present.  

                                        //2. FA-L01-00-0026 - Check Format. Must be according to the definition provided in 7.1(2)
                                        //   YYYY-MM-DDThh:mm:ss[.000000]Z10;


                                        //3. "FA-L03-00-0027 - Date must be in the past
                                        TimeSpan ts_acc = new TimeSpan();
                                        ts_acc = DateTime.UtcNow - fardoc.AcceptanceDateTime.Item;
                                        if (ts_acc.TotalSeconds > 10 * 60)
                                        {
                                            // FA-L03-00-0027 - warning
                                        }
                                    }
                                    else
                                    {
                                        //FA-L00-00-0025 - error
                                    }
                                    #endregion

                                    #region "FA-L01-00-0028", "FA-L01-00-0029", "FA-L01-00-0030"
                                    // 1. FA-L01-00-0028 - Check presence. Must be present.
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        // FAReportDocument/RelatedFLUXReportDocument/ID
                                        //2. FA-L01-00-0029 - Check attribute schemeID. Must be UUID.
                                        if (fardoc.RelatedFLUXReportDocument.ID != null)
                                        {
                                            foreach (var ids in fardoc.RelatedFLUXReportDocument.ID)
                                            {
                                                if (ids.schemeID != "UUID")
                                                {
                                                    // FA-L01-00-0029 - error 
                                                }
                                            }

                                            //3. FA-L01-00-0030 - Check Format. Must be according to the specified schemeID.
                                            //???
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0028 - error
                                    }
                                    #endregion

                                    #region FA-L00-00-0032, FA-L01-00-0033, FA-L01-00-0034
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        //1. FAReportDocument/RelatedFLUXReportDocument/PurposeCode
                                        // Check attribute listID. Must be FLUX_GP_PURPOSE
                                        if (fardoc.RelatedFLUXReportDocument.PurposeCode != null)
                                        {
                                            // ok FA-L00-00-0032
                                            //2. FA-L01-00-0033 - Check attribute listID. Must be FLUX_GP_PURPOSE
                                            if (fardoc.RelatedFLUXReportDocument.PurposeCode.listID == "FLUX_GP_PURPOSE")
                                            {
                                                // ok FA-L00-00-0033
                                                //3. FA-L01-00-0034 - Check code. Must be existing in the list specified in attribute listID
                                                if (fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "9" ||
                                                    fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "5")
                                                {
                                                    // ok FA-L01-00-0034
                                                }
                                                else
                                                {
                                                    // error FA-L01-00-0034
                                                }
                                            }
                                            else
                                            {
                                                // FA-L00-00-0033 - error 
                                            }
                                        }
                                        else
                                        {
                                            // FA-L00-00-0032 - error 
                                        }
                                    }
                                    #endregion

                                    #region FA-L03-00-0035, FA-L01-00-0036, "FA-L01-00-0037, FA-L03-00-0038
                                    // FAReportDocument/RelatedFLUXReportDocument/ReferencedID, 
                                    // FAReportDocument / RelatedFLUXReportDocument / PurposeCode

                                    //Check presence. Must be present if correction, deletion or cancellation of an earlier report. PurposeCode = 1, 3 or 5.
                                    //9	Original
                                    //1	Cancellation
                                    //5	Replace
                                    //3	Delete
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        //1. FA-L03-00-0035  
                                        // Check presence. Must be present if correction, deletion or cancellation of an earlier report. PurposeCode = 1, 3 or 5.
                                        if (fardoc.RelatedFLUXReportDocument.PurposeCode != null)
                                        {

                                            //3. FA-L01-00-0034 - Check code. Must be existing in the list specified in attribute listID
                                            if (fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "1" ||
                                                fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "3" ||
                                                fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "5")
                                            {
                                                if (fardoc.RelatedFLUXReportDocument.ReferencedID != null)
                                                {
                                                    if (string.IsNullOrEmpty(fardoc.RelatedFLUXReportDocument.ReferencedID.Value))
                                                    {
                                                        // FA-L03-00-0035 - error
                                                    }
                                                    else
                                                    {
                                                        //2. FA-L01-00-0036 - Check attribute schemeID. Must be UUID.
                                                        if (fardoc.RelatedFLUXReportDocument.ReferencedID.schemeID == "UUID")
                                                        {
                                                            //3. FA-L01-00-0037 - Check Format. Must be according to the specified schemeID.
                                                            // ???

                                                            //4. FA-L03-00-0038 - Must be an identifier of an accepted report
                                                            // TODO: Check in DB for report with equal UUID
                                                        }
                                                        else
                                                        {
                                                            // FA-L01-00-0036 - error
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // FA-L03-00-0035 - error
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region  "FA-L00-00-0039", "FA-L00-00-0040", "FA-L00-00-0041"
                                    //FAReportDocument/RelatedFLUXReportDocument/CreationDateTime
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        //1. FA-L00-00-0039 - Check presence. Must be present.
                                        if (fardoc.RelatedFLUXReportDocument.CreationDateTime != null)
                                        {

                                            //2. FA-L00-00-0040 -  Check Format. Must be according to the definition provided in 7.1(2)

                                            //3. FA-L00-00-0041 - Date must be in the past.
                                            TimeSpan ts_crdar = DateTime.UtcNow - fardoc.RelatedFLUXReportDocument.CreationDateTime.Item;
                                            if (ts_crdar.TotalSeconds > 10 * 60)
                                            {
                                                // FA-L00-00-0041 warning 
                                            }
                                        }
                                        else
                                        {
                                            // FA-L00-00-0039 - error 
                                        }
                                    }
                                    #endregion

                                    #region FA-L02-00-0042
                                    //FAReportDocument/AcceptanceDateTime, 
                                    //FAReportDocument/RelatedFLUXReportDocument/CreationDateTime
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        if (fardoc.RelatedFLUXReportDocument.CreationDateTime != null &&
                                            fardoc.AcceptanceDateTime != null)
                                        {
                                            //Acceptance date/time must be before Creation date/time
                                            if (fardoc.AcceptanceDateTime.Item < fardoc.RelatedFLUXReportDocument.CreationDateTime.Item)
                                            {
                                                // ok FA-L02-00-0042
                                            }
                                            else
                                            {
                                                // FA-L02-00-0042 - error
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FA-L00-00-0043, FA-L00-00-0044, FA-L01-00-0628, FA-L00-00-0045
                                    //FAReportDocument/RelatedFLUXReportDocument/OwnerFLUXParty/ID
                                    //1. Check presence. Must be present
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        if (fardoc.RelatedFLUXReportDocument.OwnerFLUXParty != null)
                                        {
                                            if (fardoc.RelatedFLUXReportDocument.OwnerFLUXParty.ID != null)
                                            {
                                                foreach (var ids in fardoc.RelatedFLUXReportDocument.OwnerFLUXParty.ID)
                                                {
                                                    if (string.IsNullOrEmpty(ids.Value))
                                                    {
                                                        // FA-L00-00-0043 - error
                                                    }
                                                    else
                                                    {
                                                        //2. FA-L00-00-0044 - Check attribute schemeID. Must be FLUX_GP_PARTY
                                                        if (ids.schemeID != "FLUX_GP_PARTY")
                                                        {
                                                            // FA-L00-00-0044 - error
                                                        }
                                                        // 3. FA-L01-00-0628 - Check ID value. Must be existing in the list specified in 
                                                        //                     attribute schemeID.
                                                        // TODO: check in db FLUX_GP_PARTY

                                                        // 4. FA-L00-00-0045 - Check if FAReportDocument/RelatedFLUXReportDocument
                                                        // /OwnerFLUXParty/ID (owner of the report) is consistent with FLUXFAReportMessage/FLUXReport Document/OwnerFLUXParty/ID (party sending the message) .
                                                        if (IsFRD)
                                                        {
                                                            if (FRD_OFP_IdValue != ids.Value)
                                                            {
                                                                //FA-L00-00-0045  - error
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            //Acceptance date/time must be before Creation date/time
                                            if (fardoc.AcceptanceDateTime.Item < fardoc.RelatedFLUXReportDocument.CreationDateTime.Item)
                                            {
                                                // ok FA-L02-00-0042
                                            }
                                            else
                                            {
                                                // FA-L02-00-0042 - error
                                            }
                                        }
                                    }

                                    #endregion

                                    #region FA-L00-00-0046, FA-L00-00-0047, FA-L00-00-0055, FA-L01-00-0056, FA-L01-00-0057, FA-L00-00-0058, FA-L01-00-0059, FA-L01-00-0060, FA-L03-00-0631
                                    //VesselTransportMeans/RoleCode
                                    //"FAReportDocument/SpecifiedVesselTransportMeans, 
                                    // FAReportDocument / PurposeCode"

                                    //SpecifiedVesselTransportMeans must be present, 
                                    //unless deletion or cancellation report.
                                    // if If purposeCode 9 or 5, this entity is mandatory
                                    if (fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "9" ||
                                        fardoc.RelatedFLUXReportDocument.PurposeCode.Value == "5")
                                    {
                                        if (fardoc.SpecifiedVesselTransportMeans != null)
                                        {
                                            bool IsSvtmIRCS = false;
                                            bool IsSvtmEM = false;
                                            bool IsSvtmCFR = false;
                                            bool IsSvtmRCMState = false;
                                            foreach (var svtmid in fardoc.SpecifiedVesselTransportMeans.ID)
                                            {
                                                //FA-L03-00-0634
                                                //At least 2 occurrences of SpecifiedVesselTransportMeans/ID must be present; 
                                                //One with schemeID = IRCS and one with schemeID = EXT_MARK if
                                                //SpecifiedVesselTransportMeans / RegistrationVesselCountry / ID is not on the code list MEMBER_STATE.
                                                if (svtmid.schemeID == "IRCS")
                                                {
                                                    IsSvtmIRCS = true;
                                                }
                                                if (svtmid.schemeID == "EXT_MARK")
                                                {
                                                    IsSvtmEM = true;
                                                }
                                            }
                                            if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry != null)
                                            {
                                                if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID != null)
                                                {
                                                    if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID == "MEMBER_STATE")
                                                    {
                                                        IsSvtmRCMState = true;
                                                    }
                                                }
                                            }
                                            if (!IsSvtmRCMState && IsSvtmIRCS && IsSvtmEM)
                                            {
                                            }
                                            else
                                            {
                                                //FA-L03-00-0634 - error
                                            }

                                            // FA-L00-00-0068
                                            // Check presence. Must be present if VesselTransportMeans/RoleCode 
                                            // is PAIR_FISHING_PARTNER
                                            bool IsSvtmRCodePartner = false;
                                            if (fardoc.SpecifiedVesselTransportMeans.RoleCode.Value == "PAIR_FISHING_PARTNER")
                                            {
                                                IsSvtmRCodePartner = true;
                                            }

                                            //FA-L00-00-0067
                                            //VesselTransportMeans/SpecifiedContactParty
                                            //"Check presence. Must be present if used in entity 
                                            //FAReportDocument / SpecifiedVesselTransportMeans"
                                            if (fardoc.SpecifiedVesselTransportMeans.SpecifiedContactParty != null)
                                            {
                                                //"FA-L00-00-0620":
                                                //VesselTransportMeans/SpecifiedContactParty/SpecifiedContactPerson
                                                //"Check presence. Must be present if used in entity 
                                                //FAReportDocument / SpecifiedVesselTransportMeans"
                                                foreach (var scp in fardoc.SpecifiedVesselTransportMeans.SpecifiedContactParty)
                                                {
                                                    if (scp.SpecifiedContactPerson == null)
                                                    {
                                                        // FA-L00-00-0620 - error

                                                        if (IsSvtmRCodePartner)
                                                        {
                                                            // FA-L00-00-0068 - error
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (scp.RoleCode == null)
                                                        {
                                                            // FA-L00-00-0621 - error
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0069
                                                            //VesselTransportMeans/SpecifiedContactParty/RoleCode
                                                            //Check listID. Must be FLUX_CONTACT_ROLE
                                                            foreach (var scprcode in scp.RoleCode)
                                                            {
                                                                if (scprcode.listID != "FLUX_CONTACT_ROLE")
                                                                {
                                                                    //FA-L00-00-0069 - error
                                                                }
                                                                else
                                                                {
                                                                    //FA-L01-00-0070
                                                                    //VesselTransportMeans/SpecifiedContactParty/RoleCode
                                                                    //Check code. Must be existing in the list specified in attribute listID

                                                                    // Check in DB nomenclature of  scprcode.Value

                                                                    //FA-L02-00-0071
                                                                    //VesselTransportMeans/SpecifiedContactParty/RoleCode
                                                                    //Must be MASTER or AGENT if used in entity 
                                                                    //FAReportDocument / SpecifiedVesselTra nsportMeans
                                                                    if (scprcode.Value != "MASTER" || scprcode.Value != "AGENT")
                                                                    {
                                                                        // FA-L02-00-0071 - error
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        //FA-L00-00-0072
                                                        //VesselTransportMeans/SpecifiedContactParty/SpecifiedContactPerson/GivenName, 
                                                        //VesselTransport Means/SpecifiedContactParty/SpecifiedContactPerson/Alias
                                                        //Check presence. Must be present if AliasText is not present.
                                                        //FA-L00-00-0074
                                                        //VesselTransportMeans/SpecifiedContactParty/SpecifiedContactPerson/FamilyName, 
                                                        //VesselTransportMeans/SpecifiedContactParty/SpecifiedContactPerson/Alias
                                                        //Check presence. Must be present if Alias is not present
                                                        foreach (var scpcp in scp.SpecifiedContactPerson)
                                                        {
                                                            if (scpcp.Alias.Value != null)
                                                            {
                                                                if (string.IsNullOrEmpty(scpcp.Alias.Value))
                                                                {
                                                                    if (scpcp.GivenName == null)
                                                                    {
                                                                        // FA-L00-00-0072 - error
                                                                        // FA-L00-00-0076 - error
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(scpcp.GivenName.Value))
                                                                        {
                                                                            // FA-L00-00-0072 - error
                                                                            // FA-L00-00-0076 - error
                                                                        }
                                                                    }
                                                                    if (scpcp.FamilyName == null)
                                                                    {
                                                                        //FA-L00-00-0074 - error
                                                                        // FA-L00-00-0076 - error
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(scpcp.FamilyName.Value))
                                                                        {
                                                                            //FA-L00-00-0074 - error
                                                                            // FA-L00-00-0076 - error
                                                                        }
                                                                    }
                                                                    // FA-L01-00-0077 - error
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (scpcp.GivenName == null)
                                                                {
                                                                    // FA-L00-00-0072 - error
                                                                    // FA-L00-00-0076 - error
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(scpcp.GivenName.Value))
                                                                    {
                                                                        // FA-L00-00-0072 - error
                                                                        // FA-L00-00-0076 - error
                                                                    }
                                                                }
                                                                if (scpcp.FamilyName == null)
                                                                {
                                                                    //FA-L00-00-0074 - error
                                                                    // FA-L00-00-0076 - error
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(scpcp.FamilyName.Value))
                                                                    {
                                                                        //FA-L00-00-0074 - error
                                                                        // FA-L00-00-0076 - error
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //FA-L00-00-0078
                                                    //VesselTransportMeans/SpecifiedContactParty/SpecifiedStructuredAddress
                                                    //Check presence. Must be present if used in entity 
                                                    //FAReportDocument/SpecifiedVesselTransportMeans/SpecifiedContactParty
                                                    if (scp.SpecifiedStructuredAddress != null)
                                                    {
                                                        //FA-L00-00-0080
                                                        //StructuredAddress/Streetname, StructuredAddress/PostOfficeBox
                                                        //Check presence. Must be present unless PostOfficeBox is present and not empty.                                                    
                                                        foreach (var ssa in scp.SpecifiedStructuredAddress)
                                                        {
                                                            bool ssa_street = false;
                                                            bool ssa_pobox = false;
                                                            bool ssa_parea = false;
                                                            bool ssa_irl = false;
                                                            if (ssa.StreetName != null)
                                                            {
                                                                //FA-L01-00-0081
                                                                //StructuredAddress/Streetname
                                                                //Non-empty
                                                                if (!string.IsNullOrEmpty(ssa.StreetName.Value))
                                                                {
                                                                    ssa_street = true;
                                                                }
                                                                else
                                                                {
                                                                    //FA-L01-00-0081 - error
                                                                }
                                                            }

                                                            if (ssa.PostOfficeBox != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(ssa.PostOfficeBox.Value))
                                                                {
                                                                    ssa_pobox = true;
                                                                }
                                                            }

                                                            //FA-L00-00-0082
                                                            //StructuredAddress/PostalArea
                                                            //Check presence. Must be present, except if CountryID = IRL (Ireland)
                                                            if (ssa.PostalArea != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(ssa.PostalArea.Value))
                                                                {
                                                                    ssa_parea = true;
                                                                }
                                                                else
                                                                {
                                                                    //FA-L01-00-0083
                                                                    //StructuredAddress/PostalArea
                                                                    //Non-empty

                                                                    //FA-L01-00-0083 - error
                                                                }
                                                            }

                                                            //FA-L00-00-0084
                                                            //StructuredAddress/CityName
                                                            //Check presence. Must be present
                                                            //FA-L01-00-0085
                                                            //Non-empty
                                                            if (ssa.CityName != null)
                                                            {
                                                                if (string.IsNullOrEmpty(ssa.CityName.Value))
                                                                {
                                                                    //FA-L01-00-0085 - error
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // FA-L00-00-0084 - warning
                                                            }

                                                            //FA-L00-00-0086
                                                            //StructuredAddress/CountryID
                                                            //Check presence. Must be present
                                                            if (ssa.CountryID != null)
                                                            {
                                                                if (ssa.CountryID.Value == "IRL")
                                                                {
                                                                    ssa_irl = true;
                                                                }
                                                                //FA-L00-00-0087
                                                                if (ssa.CountryID.schemeID != "TERRITORY")
                                                                {
                                                                    //FA-L00-00-0087 - error
                                                                }

                                                                //FA-L01-00-0088
                                                                //StructuredAddress/CountryID
                                                                //Check code. Must be existing in the list specified in attribute schemeID
                                                                //TODO: Check DB nomeclature 
                                                            }
                                                            else
                                                            {
                                                                // FA-L00-00-0086 - warning
                                                            }

                                                            //FA-L00-00-0089
                                                            //StructuredAddress/PlotIdentification
                                                            //Check presence. Should be present.
                                                            if (string.IsNullOrEmpty(ssa.PlotIdentification.Value))
                                                            {
                                                                //FA-L00-00-0089 - warning
                                                            }

                                                            if (!ssa_pobox)
                                                            {
                                                                if (!ssa_street)
                                                                {
                                                                    //FA-L00-00-0080 - warning
                                                                }
                                                            }

                                                            if (!ssa_parea)
                                                            {
                                                                if (!ssa_irl)
                                                                {
                                                                    //FA-L00-00-0082 - error
                                                                }
                                                            }


                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L00-00-0078 - error
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FA-L00-00-0067 - error
                                            }

                                            //FA-L00-00-0230
                                            //VesselStorageCharacteristic/TypeCode
                                            //Check presence. Must be present.
                                            if (fardoc.SpecifiedVesselTransportMeans.ApplicableVesselStorageCharacteristic != null)
                                            {
                                                foreach (var avsc in fardoc.SpecifiedVesselTransportMeans.ApplicableVesselStorageCharacteristic)
                                                {
                                                    if (avsc.TypeCode != null)
                                                    {
                                                        //FA-L01-00-0231
                                                        //VesselStorageCharacteristic/TypeCode
                                                        //Check attribute listID. Must be VESSEL_STORAGE_TYPE
                                                        foreach (var avsctc in avsc.TypeCode)
                                                        {
                                                            if (avsctc.listID != "VESSEL_STORAGE_TYPE")
                                                            {
                                                                //FA-L01-00-0231 - error
                                                            }

                                                            //FA-L01-00-0232
                                                            //VesselStorageCharacteristic/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: check db ???
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L00-00-0230 - error
                                                    }
                                                }
                                            }


                                        }
                                        else
                                        {
                                            // FA-L00-00-0046 - error
                                        }

                                        bool IsFADeparture = false;
                                        bool IsFAAEnOrNot = false;
                                        bool IsFAFO = false;
                                        bool IsFAJFO = false;
                                        bool IsFAReloc = false;
                                        bool IsFADiscard = false;
                                        bool IsFAAExOrNot = false;
                                        bool IsFAArrNot = false;
                                        bool IsFAArrDec = false;
                                        bool IsFAArr = false;
                                        bool IsFALand = false;
                                        bool IsFATrans = false;
                                        bool IsFATransOrRelocNot = false;
                                        bool IsFATransOrRelocDec = false;
                                        // FA-L00-00-0047
                                        //FAReportDocument/SpecifiedFishingActivity
                                        //Check presence. Must be present, unless deletion or cancellation report.
                                        foreach (var sfa in fardoc.SpecifiedFishingActivity)
                                        {
                                            if (sfa == null)
                                            {
                                                // FA-L00-00-0047 - error
                                            }
                                            else
                                            {
                                                //FA-L03-00-0631
                                                bool SfaTCDifRelAndTrans = false;
                                                if ((sfa.TypeCode.Value != "RELOCATION") || (sfa.TypeCode.Value != "TRANSHIPMENT"))
                                                {
                                                    SfaTCDifRelAndTrans = true;
                                                }
                                                // FA-L03-00-0632 
                                                bool SfaTCTrans = false;
                                                if (sfa.TypeCode.Value == "TRANSHIPMENT")
                                                {
                                                    SfaTCTrans = true;
                                                }
                                                bool IsLandOrTrans = false;
                                                if (sfa.TypeCode.Value == "LANDING" || sfa.TypeCode.Value == "TRANSHIPMENT")
                                                {
                                                    IsLandOrTrans = true;
                                                }

                                                #region  FA-L00-00-0055, FA-L01-00-0056, FA-L01-00-0057, FA-L00-00-0058, FA-L01-00-0059, FA-L01-00-0060, FA-L03-00-0632, FA-L03-00-0635
                                                //VesselTransportMeans/RoleCode
                                                // "Check presence. Must be present if used in entity 
                                                //SpecifiedFishingActivity / RelatedVesselTransportMeans."
                                                bool IsVTMRCodePair = false;
                                                if (sfa.RelatedVesselTransportMeans != null)
                                                {
                                                    foreach (var sfa_rvtm in sfa.RelatedVesselTransportMeans)
                                                    {
                                                        if (sfa_rvtm.RoleCode == null)
                                                        {
                                                            // FA-L00-00-0055 - error
                                                        }
                                                        else
                                                        {
                                                            //FA-L01-00-0056
                                                            //VesselTransportMeans/RoleCode
                                                            //Check attribute listID. Must be FA_VESSEL_ROLE
                                                            if (sfa_rvtm.RoleCode.listID != "FA_VESSEL_ROLE")
                                                            {
                                                                // FA-L01-00-0056 - error
                                                            }

                                                            // FA-L01-00-0057  
                                                            //VesselTransportMeans/RoleCode
                                                            // Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: check DB FA_VESSEL_ROLE ==  sfa_rvtm.RoleCode.Value

                                                            if (sfa_rvtm.RoleCode.Value == "PAIR_FISHING_PARTNER")
                                                            {
                                                                IsVTMRCodePair = true;
                                                            }
                                                        }

                                                        //FA-L00-00-0079
                                                        //"RelatedVesselTransportMeans/SpecifiedContactParty/SpecifiedStructuredAddress, 
                                                        //FishingActivity / RelatedVesselTransportMeans / RoleCode"
                                                        //SpecifiedStructuredAddress must be present if used in entity FishingActivity/RelatedVesselTransport Means
                                                        //and RelatedVesselTransportMeans/ RoleCode is PAIR_FISHING_PARTNER
                                                        if (IsVTMRCodePair)
                                                        {
                                                            if (sfa_rvtm.SpecifiedContactParty != null)
                                                            {
                                                                foreach (var sfa_rvtm_cparty in sfa_rvtm.SpecifiedContactParty)
                                                                {
                                                                    if (sfa_rvtm_cparty.SpecifiedStructuredAddress == null)
                                                                    {
                                                                        //FA-L00-00-0079 - error
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // FA-L00-00-0058
                                                        //VesselTransportMeans/RegistrationVesselCountry/ID
                                                        //Check presence. Must be present
                                                        if (sfa_rvtm.RegistrationVesselCountry != null)
                                                        {
                                                            if (sfa_rvtm.RegistrationVesselCountry.ID == null)
                                                            {
                                                                // FA-L00-00-0058 - error
                                                            }
                                                            else
                                                            {
                                                                // FA-L01-00-0059
                                                                //VesselTransportMeans/RegistrationVesselCountry/ID
                                                                //Check schemeID. Must be TERRITORY
                                                                if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "TERRITORY")
                                                                {
                                                                    // FA-L01-00-0059 - error
                                                                }
                                                                //"FA-L01-00-0060":
                                                                //VesselTransportMeans/RegistrationVesselCountry/ID
                                                                //Check code. Must be existing in the list specified in attribute schemeID
                                                                // TODO: check in DB - find which nomenclature

                                                                //FA-L03-00-0631
                                                                bool IsRvtmIdCfr = false;
                                                                bool IsRvtmIdIRCS = false;
                                                                bool IsRvtmIdEM = false;
                                                                bool IsRvtmIdUVI = false;
                                                                foreach (var rvtmid in sfa_rvtm.ID)
                                                                {
                                                                    if (rvtmid.schemeID == "CFR")
                                                                    {
                                                                        IsRvtmIdCfr = true;
                                                                    }
                                                                    if (rvtmid.schemeID == "IRCS")
                                                                    {
                                                                        IsRvtmIdIRCS = true;
                                                                    }
                                                                    if (rvtmid.schemeID == "EXT_MARK")
                                                                    {
                                                                        IsRvtmIdEM = true;
                                                                    }
                                                                    if (rvtmid.schemeID == "IMO")
                                                                    {
                                                                        IsRvtmIdUVI = true;
                                                                    }
                                                                }
                                                                if (SfaTCDifRelAndTrans && IsRvtmIdCfr)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0631 - error
                                                                    }
                                                                }

                                                                // FA-L03-00-0632 
                                                                if (!IsRvtmIdCfr && IsRvtmIdIRCS && IsRvtmIdEM)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0632 - error
                                                                    }
                                                                }

                                                                //FA-L03-00-0635
                                                                if (IsRvtmIdIRCS && IsRvtmIdEM)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0635 - error
                                                                    }
                                                                }

                                                                // FA-L03-00-0632 
                                                                if (IsRvtmIdUVI && SfaTCTrans)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID == "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0632 - error
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                #endregion

                                                //FA-L00-00-0090
                                                //FishingActivity/TypeCode
                                                //Check presence. Must be present.
                                                //FA-L01-00-0091
                                                //Check attribute listID. Must be FLUX_FA_TYPE
                                                //FA-L01-00-0092
                                                //Check code. Must be existing in the list specified in attribute listID
                                                //FA-L02-00-0093
                                                //If used as sub-activity the type can only be GEAR_SHOT, GEAR_RETRIEVAL, RELOCATION, START_ACTIVITY
                                                if (sfa.TypeCode != null)
                                                {
                                                    if (!string.IsNullOrEmpty(sfa.TypeCode.Value))
                                                    {
                                                        if (sfa.TypeCode.listID != "FLUX_FA_TYPE")
                                                        {
                                                            //FA-L01-00-0091 - error
                                                        }

                                                        //FA-L01-00-0092
                                                        //Check code. Must be existing in the list specified in attribute listID
                                                        //TODO: check DB nomenclature FLUX_FA_TYPE
                                                        if (sfa.TypeCode.Value == "DEPARTURE")
                                                        {
                                                            IsFADeparture = true;
                                                        }

                                                        if (sfa.TypeCode.Value == "AREA_ENTRY")
                                                        {
                                                            IsFAAEnOrNot = true;
                                                        }

                                                        if (sfa.TypeCode.Value == "FISHING_OPERATION")
                                                        {
                                                            IsFAFO = true;
                                                        }
                                                        if (sfa.TypeCode.Value == "JOINT_FISHING_OPERATION")
                                                        {
                                                            IsFAJFO = true;

                                                            //FA-L00-00-0425
                                                            //FAReportDocument/SpecifiedVesselTransportMeans/RoleCode
                                                            //Check presence. Must be present if the activity is a joint fishing operation declaration
                                                            if (fardoc.SpecifiedVesselTransportMeans.RoleCode == null)
                                                            {
                                                                //FA-L00-00-0425 - error
                                                            }
                                                        }
                                                        if (sfa.TypeCode.Value == "RELOCATION" && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFAReloc = true;

                                                            //FA-L02-00-0443
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //Check presence. At least 1 occurrence of TypeCode=LOADED or UNLOADED must be present if the activity is a relocation declaration.
                                                            var fsfatc = sfa.SpecifiedFACatch.Where(x => x.TypeCode.Value == "LOADED" || x.TypeCode.Value == "UNLOADED").ToList();
                                                            if (fsfatc != null && fsfatc.Count >= 1)
                                                            {

                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0443 - error
                                                            }
                                                            //FA-L02-00-0266
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least 1 occurrence must be present if the activity is a relocation declaration
                                                            if (sfa.RelatedFLUXLocation != null && sfa.RelatedFLUXLocation.Length >= 1)
                                                            {
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0266 - error
                                                            }
                                                            //FA-L02-00-0267
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //Must be POSITION if the activity is a relocation declaration
                                                            var fsfarfl = sfa.RelatedFLUXLocation.Where(x => x.TypeCode.Value == "POSITION").ToList();
                                                            if (fsfarfl != null && fsfarfl.Count >= 0)
                                                            { }
                                                            else
                                                            {
                                                                //FA-L02-00-0267 - error
                                                            }



                                                        }

                                                        if (sfa.TypeCode.Value == "DISCARD")
                                                        {
                                                            IsFADiscard = true;

                                                            //FA-L02-00-0281
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least 1 occurrence must be present if the activity is a discard declaration.
                                                            if (sfa.RelatedFLUXLocation == null)
                                                            {
                                                                //FA-L02-00-0281 - error
                                                            }

                                                            //FA-L02-00-0284
                                                            //FishingActivity/ReasonCode
                                                            //If the activity is a DISCARD DECLARATION and the ReasonCode is provided, the listID must be FA_REASON_DISCARD. The value may not be QEX nor OTH
                                                            if (sfa.ReasonCode != null)
                                                            {
                                                                if (sfa.ReasonCode.listID != "FA_REASON_DISCARD")
                                                                {
                                                                    //FA-L02-00-0284 - error
                                                                }
                                                                else
                                                                {
                                                                    if (sfa.ReasonCode.Value == "QEX" || sfa.ReasonCode.Value == "OTH")
                                                                    {
                                                                        //FA-L02-00-0284 - error
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (sfa.TypeCode.Value == "AREA_EXIT")
                                                        {
                                                            IsFAAExOrNot = true;

                                                            //FA-L02-00-0286
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least 2 occurrences must be present if the activity is an area exit declaration or notification.
                                                            if (sfa.RelatedFLUXLocation != null)
                                                            {
                                                                if (sfa.RelatedFLUXLocation.Length < 2)
                                                                {
                                                                    //FA-L02-00-0286 - error
                                                                }

                                                                //FA-L02-00-0287
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //If the activity is an area exit declaration or notification, one occurrence must be POSITION; another occurrence must be AREA.
                                                                var posarea = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "POSITION");
                                                                var areapos = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "AREA");
                                                                if (posarea != null && areapos != null)
                                                                {
                                                                    //FA-L02-00-0288
                                                                    //"FishingActivity/RelatedFLUXLocation/ID, 
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode, 
                                                                    //FishingActivity/FAReportDocument/TypeCode"
                                                                    //Check schemeID. If the RelatedFLUXLocation/TypeCode is AREA the schemeID of RelatedFLUXLocation/ID must be EFFORT_ZONE if the activity is an area exit declaration (FAReportDocument/TypeCode=DECL ARATION).
                                                                    if (areapos.ID.schemeID != "EFFORT_ZONE")
                                                                    {
                                                                        //FA-L02-00-0288 - warning
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0287 - error                                                                
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0286 - error
                                                            }


                                                        }

                                                        if (sfa.TypeCode.Value == "ARRIVAL" && fardoc.TypeCode.Value == "NOTIFICATION")
                                                        {
                                                            IsFAArrNot = true;

                                                            //FA-L00-00-0291
                                                            //FishingActivity/OccurrenceDateTime
                                                            //Check presence. Must be present if the activity is an arrival notification
                                                            if (sfa.OccurrenceDateTime == null)
                                                            {
                                                                //FA-L00-00-0291 - error
                                                            }
                                                            //FA-L02-00-0292
                                                            //FishingActivity/ReasonCode
                                                            //Check presence. Must be present if the activity is an arrival notification.
                                                            if (sfa.ReasonCode == null)
                                                            {
                                                                //FA-L02-00-0292 - error
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0293
                                                                //FishingActivity/ReasonCode
                                                                //Check attribute listID. Must be FA_REASON_ARRIVAL if the activity is an arrival notification.
                                                                if (sfa.ReasonCode.listID != "FA_REASON_ARRIVAL")
                                                                {
                                                                    //FA-L02-00-0293 - error
                                                                }
                                                            }

                                                            //FA-L02-00-0294
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least one occurrence must be present if the activity is an arrival notification
                                                            if (sfa.RelatedFLUXLocation.Length < 1)
                                                            {
                                                                //FA-L02-00-0294 - error
                                                            }
                                                            //FA-L02-00-0295
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //TypeCode must be LOCATION for at least one occurrence if the activity is an arrival notification.
                                                            var sfalocloc = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION");
                                                            if (sfalocloc == null)
                                                            {
                                                                //FA-L02-00-0295 - warning
                                                            }

                                                            //FA-L02-00-0296
                                                            //FishingActivity/SpecifiedFACatch, FishingActivity/ReasonCode
                                                            //Check presence. Must be present if the activity is an arrival notification and ReasonCode is LAN (landing)
                                                            if (sfa.ReasonCode.Value == "LAN")
                                                            {
                                                                if (sfa.SpecifiedFACatch == null)
                                                                {
                                                                    //FA-L02-00-0296 - error
                                                                }

                                                                //FA-L02-00-0298
                                                                //FishingActivity/SpecifiedFACatch/TypeCode, FishingActivity/ReasonCode
                                                                //Must have at least one occurrence with TypeCode UNLOADED if the activity is an arrival notification and ReasonCode is LAN (and if SpecifiedFACatch entity provided)
                                                                var scunloaded = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "UNLOADED");
                                                                if (scunloaded == null)
                                                                {
                                                                    //FA-L02-00-0298 - error
                                                                }
                                                            }

                                                            //FA-L02-00-0297
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //"Must have at least one occurrence with TypeCode ONBOARD if the activity is an arrival notification (and if
                                                            //SpecifiedFACatch provided)"
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var sconboard = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "ONBOARD");
                                                                if (sconboard == null)
                                                                {
                                                                    //FA-L02-00-0297 - error
                                                                }
                                                            }

                                                            //FA-L02-00-0299
                                                            //FishingActivity/SpecifiedFishingTrip/SpecifiedDelimitedPeriod/StartDateTime
                                                            //Check presence. Must be present if the activity is an arrival notification.
                                                            if (sfa.SpecifiedFishingTrip != null)
                                                            {
                                                                if (sfa.SpecifiedFishingTrip.SpecifiedDelimitedPeriod != null)
                                                                {
                                                                    var stsdpst = sfa.SpecifiedFishingTrip.SpecifiedDelimitedPeriod.First(x => x.StartDateTime != null);
                                                                    if (stsdpst == null)
                                                                    {
                                                                        //FA-L02-00-0299 - warning
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (sfa.TypeCode.Value == "ARRIVAL" && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFAArrDec = true;

                                                            //FA-L00-00-0301
                                                            //FishingActivity/OccurrenceDateTime
                                                            //Check presence. Must be present if the activity is an arrival notification
                                                            if (sfa.OccurrenceDateTime == null)
                                                            {
                                                                //FA-L00-00-0301 - error
                                                            }

                                                            //FA-L01-00-0302
                                                            //FishingActivity/ReasonCode
                                                            //Check attribute listID. Must be FA_REASON_ARRIVAL if the activity is an arrival declaration
                                                            if (sfa.ReasonCode == null)
                                                            {
                                                                //FA-L01-00-0302 - error
                                                            }
                                                            else
                                                            {
                                                                if (sfa.ReasonCode.listID != "FA_REASON_ARRIVAL")
                                                                {
                                                                    //FA-L01-00-0302 - error 
                                                                }
                                                            }

                                                            //FA-L00-00-0303
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least one occurrence must be present if the activity is an arrival notification
                                                            if (sfa.RelatedFLUXLocation.Length < 1)
                                                            {
                                                                //FA-L00-00-0303 - error
                                                            }

                                                            //FA-L01-00-0304
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //TypeCode must be LOCATION for at least one occurrence if the activity is an arrival notification.
                                                            var sfalocloc = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION");
                                                            if (sfalocloc == null)
                                                            {
                                                                //FA-L01-00-0304 - warning
                                                            }

                                                            //FA-L01-00-0305
                                                            //FishingActivity/SpecifiedFishingGear/RoleCode
                                                            //RoleCode must be ONBOARD if the activity is an arrival declaration (and SpecifiedFishingGear provided).
                                                            if (sfa.SpecifiedFishingGear != null)
                                                            {
                                                                var sfgrc = sfa.SpecifiedFishingGear.SelectMany(x => x.RoleCode).First(x => x.Value == "ONBOARD");
                                                                if (sfgrc == null)
                                                                {
                                                                    //FA-L01-00-0305 - error
                                                                }
                                                            }

                                                            //FA-L03-00-0306
                                                            //"FishingActivity/TypeCode,
                                                            //FishingActivity/FAReportDocument/TypeCode,
                                                            //FishingActivity/FishingTrip/ID,
                                                            //FAReportDocument/RelatedFLUXReportDocument/PurposeCode"
                                                            //Only one ARRIVAL DECLARATION may exist for one trip. Only original reports (PurposeCode=9) should be considered.

                                                            //TODO: Check DB or Technologica

                                                            //FA-L02-00-0415
                                                            //"FishingActivity/SpecifiedFLUXCharacteristic/TypeCode,
                                                            //FishingActivity/SpecifiedFLUXCharacteristic/ValueDateTime, 
                                                            //FishingActivity/ReasonCode"
                                                            //If the activity is an arrival declaration check presence of
                                                            //SpecifiedFLUXCharacteristic/TypeCode. It must be present and equal
                                                            //to START_DATETIME_LANDING if ReasonCode is LAN (landing).
                                                            //ValueDateTime must be according to the definition provided in 7.1(2)                                                        
                                                            if (sfa.ReasonCode.Value == "LAN")
                                                            {
                                                                var sfafch = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode != null);
                                                                if (sfafch != null)
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0415 - error 
                                                                }
                                                            }

                                                            //FA-L02-00-0416
                                                            //FishingActivity/SpecifiedFLUXCharacteristic/TypeCode
                                                            //If the activity is an arrival declaration, check the listID. Must be FA_CHARACTERISTIC.
                                                            var sfspe = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode.listID != "FA_CHARACTERISTIC");
                                                            if (sfspe == null)
                                                            {
                                                                //FA-L02-00-0416 - error
                                                            }
                                                        }

                                                        if (sfa.TypeCode.Value == "LANDING" && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            //FA-L02-00-0311
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least 1 occurrence must be present if the activity is a landing declaration.
                                                            if (sfa.RelatedFLUXLocation != null)
                                                            {
                                                                if (sfa.RelatedFLUXLocation.Length < 1)
                                                                {
                                                                    //FA-L02-00-0311 - error
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0311 - error
                                                            }

                                                            //FA-L02-00-0312
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //Must be LOCATION, POSITION or ADDRESS if the activity is a landing declaration.
                                                            var locposadd = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION" ||
                                                                                            x.TypeCode.Value == "POSITION" ||
                                                                                            x.TypeCode.Value == "ADDRESS");
                                                            if (locposadd == null)
                                                            {
                                                                //FA-L02-00-0312 - error
                                                            }

                                                            //FA-L02-00-0313
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //There must be at least one occurrence with TypeCode UNLOADED if the activity is a landing declaration.
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var sfafctc = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "UNLOADED");
                                                                if (sfafctc == null)
                                                                {
                                                                    //FA-L02-00-0313 - error
                                                                }

                                                                //FA-L02-00-0314
                                                                //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation
                                                                //Check presence. At least one occurrence of location of catches must be present if the activity is a landing declaration.
                                                                var listsfafl = sfa.SpecifiedFACatch.SelectMany(x => x.SpecifiedFLUXLocation);
                                                                if (listsfafl == null || listsfafl.Count() < 1)
                                                                {
                                                                    //FA-L02-00-0314 - error
                                                                }

                                                                //FA-L02-00-0315
                                                                //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode
                                                                //If the activity is a landing declaration TypeCode must be AREA.
                                                                var sffarea = listsfafl.First(x => x.TypeCode.Value == "AREA");
                                                                if (sffarea == null)
                                                                {
                                                                    //FA-L02-00-0315 - error
                                                                }
                                                            }
                                                        }

                                                        if (sfa.TypeCode.Value == "TRANSHIPMENT" && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFATrans = true;
                                                            //FA-L02-00-0445
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //Check presence. At least 1 occurrence of TypeCode = LOADED or UNLOADED must be present if the activity is a transhipment declaration
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var lunloaded = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED" || x.TypeCode.Value == "UNLOADED");
                                                                if (lunloaded == null)
                                                                {
                                                                    //FA-L02-00-0445 - error
                                                                }
                                                                else
                                                                {
                                                                    if (lunloaded.TypeCode.Value == "UNLOADED")
                                                                    {
                                                                        //FA-L02-00-0328
                                                                        //"FishingActivity/SpecifiedFLUXCharacteristic, 
                                                                        //FishingActivity/SpecifiedFACatch/TypeCode"
                                                                        //Check presence.Must be present if the activity is a transhipment
                                                                        //declaration and SpecifiedFACatch/TypeCode = UNLOADED.
                                                                        if (sfa.SpecifiedFLUXCharacteristic == null)
                                                                        {
                                                                            //FA-L02-00-0328 - error
                                                                        }

                                                                        //FA-L02-00-0329
                                                                        //"FishingActivity/SpecifiedFLUXCharacteristic/TypeCode,
                                                                        //FishingActivity/SpecifiedFACatch/TypeCode,
                                                                        //FishingActivity/SpecifiedFLUXCharacteristic/SpecifiedFLUXLocation/ID,
                                                                        //FishingActivity/SpecifiedFLUXCharacteristic/SpecifiedFLUXLocation/TypeCode,
                                                                        //FishingActivity/RelatedVesselTransportMeans"
                                                                        //If the activity is a transhipment declaration and SpecifiedFACatch/TypeCode = UNLOADED
                                                                        //and RelatedVesselTransportMeans is present Then (1) SpecifiedFLUX_ Characteristic/TypeCode
                                                                        //must be DESTINATION_LOCATION and (2) SpecifiedFLUX_ Characteristic/SpecifiedFLUXLocation/TypeCode
                                                                        //must be LOCATION and (3) SchemeID of SpecifiedFLUX_ Characteristic/SpecifiedFLUX_ Location/ID must
                                                                        //be LOCATION
                                                                        if (sfa.RelatedVesselTransportMeans != null)
                                                                        {
                                                                            var destloc = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode.Value == "DESTINATION_LOCATION");
                                                                            if (destloc == null)
                                                                            {
                                                                                //FA-L02-00-0329 - error
                                                                            }
                                                                            else
                                                                            {
                                                                                var destlocloc = destloc.SpecifiedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION");
                                                                                if (destlocloc == null)
                                                                                {
                                                                                    //FA-L02-00-0329 - error
                                                                                }
                                                                                else
                                                                                {
                                                                                    var destloclocid = destloc.SpecifiedFLUXLocation.First(x => x.ID.Value == "LOCATION");
                                                                                    if (destloclocid == null)
                                                                                    {
                                                                                        //FA-L02-00-0329 - error
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                //FA-L02-00-0326
                                                                //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation
                                                                //Check presence. At least one occurrence must be present if the
                                                                //activity is a transhipment declaration.
                                                                var fcafloc = sfa.SpecifiedFACatch.SelectMany(x => x.SpecifiedFLUXLocation);
                                                                if (fcafloc == null || fcafloc.Count() < 1)
                                                                {
                                                                    //FA-L02-00-0326 - error
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0327
                                                                    //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode
                                                                    //If the activity is a transhipment declaration the
                                                                    //SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode must be AREA
                                                                    var fcaflocarea = fcafloc.First(x => x.TypeCode.Value == "AREA");
                                                                    if (fcaflocarea == null)
                                                                    {
                                                                        //FA-L02-00-0327 - error
                                                                    }
                                                                }
                                                            }
                                                            if (sfa.RelatedFLUXLocation != null)
                                                            {
                                                                //FA-L02-00-0321
                                                                //FishingActivity/RelatedFLUXLocation
                                                                //Check presence. At least 1 occurrence must be present if the activity is a transhipment declaration.
                                                                if (sfa.RelatedFLUXLocation.Length < 1)
                                                                {
                                                                    //FA-L02-00-0321 - error
                                                                }

                                                                //FA-L02-00-0322
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //If the activity is a transhipment declaration, the TypeCode value of at least one RelatedFLUXLocation must be LOCATION or POSITION.
                                                                var rfllocpos = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION" || x.TypeCode.Value == "POSITION");
                                                                if (rfllocpos == null)
                                                                {
                                                                    //FA-L02-00-0322 - error
                                                                }
                                                            }
                                                            //FA-L02-00-0323
                                                            //FishingActivity/RelatedVesselTransportMeans
                                                            //Check presence. Must be present if the activity is a transhipment declaration.
                                                            if (sfa.RelatedVesselTransportMeans != null)
                                                            {
                                                                //FA-L02-00-0324
                                                                //"FishingActivity/RelatedVesselTransportMeans/RoleCode,
                                                                //FishingActivity/SpecifiedFACatch/TypeCode"
                                                                //If the activity is a transhipment declaration the RoleCode of the
                                                                //related vessel must be RECEIVER (receiving vessel) if SpecifiedFACatch/TypeCode is UNLOADED
                                                                var spfacatcunl = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "UNLOADED");
                                                                if (spfacatcunl != null)
                                                                {
                                                                    var vtmrec = sfa.RelatedVesselTransportMeans.First(x => x.RoleCode.Value == "RECEIVER");
                                                                    if (vtmrec == null)
                                                                    {
                                                                        //FA-L02-00-0324 - error
                                                                    }
                                                                }

                                                                //FA-L02-00-0325
                                                                //"FishingActivity/RelatedVesselTransportMeans/RoleCode,
                                                                //FishingActivity / SpecifiedFACatch / TypeCode"
                                                                //If the activity is a transhipment declaration the RoleCode of the related vessel must be
                                                                //DONOR (donor vessel) if SpecifiedFACatch/TypeCode is LOADED
                                                                var spfacatcunl1 = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED");
                                                                if (spfacatcunl1 != null)
                                                                {
                                                                    var vtmrec1 = sfa.RelatedVesselTransportMeans.First(x => x.RoleCode.Value == "DONOR");
                                                                    if (vtmrec1 == null)
                                                                    {
                                                                        //FA-L02-00-0325 - error
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0323 - error
                                                            }
                                                            if (sfa.SpecifiedFLUXCharacteristic != null)
                                                            {
                                                                //FA-L00-00-0440
                                                                //FishingActivity/SpecifiedFLUXC haracteristic/TypeCode
                                                                //If the activity is a transhipment declaration, the attribute
                                                                //listID must be FA_CHARACTERISTIC
                                                                var sfafchfa = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode.listID == "FA_CHARACTERISTIC");
                                                                if (sfafchfa == null)
                                                                {
                                                                    //FA-L00-00-0440 - error
                                                                }
                                                            }
                                                        }

                                                        if ((sfa.TypeCode.Value == "RELOCATION" || sfa.TypeCode.Value == "TRANSHIPMENT")
                                                            && fardoc.TypeCode.Value == "NOTIFICATION")
                                                        {
                                                            IsFATransOrRelocNot = true;

                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                //FA-L02-00-0444
                                                                //FishingActivity/SpecifiedFACatch/TypeCode
                                                                //Check presence. At least 1 occurrence of TypeCode= LOADED or UNLOADED
                                                                //must be present if the activity is a transhipment or relocation
                                                                //notification
                                                                var relunloaded = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED" || x.TypeCode.Value == "UNLOADED");
                                                                if (relunloaded == null)
                                                                {
                                                                    //FA-L02-00-0444 - error
                                                                }
                                                                else
                                                                {
                                                                    if (relunloaded.TypeCode.Value == "UNLOADED")
                                                                    {
                                                                        //FA-L02-00-0334
                                                                        //"FishingActivity/RelatedVesselTransportMeans/RoleCode,
                                                                        //SpecifiedFACatch / TypeCode"
                                                                        //If the activity is a transhipment or relocation notification,
                                                                        //RoleCode must be RECEIVER if SpecifiedFACatch/TypeCode is UNLOADED
                                                                        if (sfa.RelatedVesselTransportMeans != null)
                                                                        {
                                                                            var rvtmrc = sfa.RelatedVesselTransportMeans.First(x => x.RoleCode.Value == "RECEIVER");
                                                                            if (rvtmrc == null)
                                                                            {
                                                                                //FA-L02-00-0334 - error
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //FA-L02-00-0334 - error
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (sfa.RelatedFLUXLocation != null)
                                                            {
                                                                //FA-L02-00-0331
                                                                //FishingActivity/RelatedFLUXLocation
                                                                //Check presence. At least 1 occurrence must be present if the activity is a transhipment declaration.
                                                                if (sfa.RelatedFLUXLocation.Length < 1)
                                                                {
                                                                    //FA-L02-00-0331 - error
                                                                }

                                                                //FA-L02-00-0332
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //Check attribute listID. If the activity is a transhipment or relocation notification, the RelatedFLUXLocation/TypeCode must be LOCATION or POSITION for at least one location.
                                                                var rrfllocpos = sfa.RelatedFLUXLocation.First(x => x.TypeCode.Value == "LOCATION" || x.TypeCode.Value == "POSITION");
                                                                if (rrfllocpos == null)
                                                                {
                                                                    //FA-L02-00-0332 - error
                                                                }
                                                            }
                                                            if (sfa.TypeCode.Value == "TRANSHIPMENT")
                                                            {
                                                                //FA-L02-00-0342
                                                                //"FishingActivity/RelatedVesselTransportMeans,
                                                                //FishingActivity/TypeCode,
                                                                //FAReportDocument/TypeCode"
                                                                //RelatedVesselTransportMeans must be present if the activity is a transhipment notification
                                                                if (sfa.RelatedVesselTransportMeans == null)
                                                                {
                                                                    //FA-L02-00-0342 - error
                                                                }


                                                            }
                                                            if (sfa.SpecifiedFLUXCharacteristic != null)
                                                            {
                                                                //FA-L02-00-0338
                                                                //"FishingActivity/SpecifiedFLUXCharacteristic/ValueQuantity,
                                                                //FishingActivity / SpecifiedFLUXCharacteristic / TypeCode"
                                                                //If the value of TypeCode is NB_CAGES_TOWED the value must be a positive integer number or zero (>=0)
                                                                var nbcagess = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode.Value == "NB_CAGES_TOWED");
                                                                if (nbcagess != null)
                                                                {
                                                                    if (nbcagess.ValueQuantity != null)
                                                                    {
                                                                        if (nbcagess.ValueQuantity.Value < 0)
                                                                        {
                                                                            //FA-L02-00-0338 - warning
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L02-00-0338 - warning
                                                                    }
                                                                }
                                                            }
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var bftrunl = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED");
                                                                if (bftrunl != null)
                                                                {
                                                                    //FA-L02-00-0344
                                                                    //"FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode,
                                                                    //FishingActivity/TypeCode, 
                                                                    //SpecifiedFACatch/TypeCode"
                                                                    //SpecifiedFLUXLocation/TypeCode must be AREA if the activity is a transhipment notification and SpecifiedFACatch/TypeCode=LOADE D
                                                                    var sfalovload = sfa.SpecifiedFACatch.SelectMany(x => x.SpecifiedFLUXLocation).First(x => x.TypeCode.Value == "AREA");
                                                                    if (sfalovload == null)
                                                                    {
                                                                        //FA-L02-00-0344 - error
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        if ((sfa.TypeCode.Value == "RELOCATION" || sfa.TypeCode.Value == "TRANSHIPMENT")
                                                            && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFATransOrRelocDec = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L00-00-0090 - error
                                                    }
                                                }
                                                else
                                                {
                                                    //FA-L00-00-0090 - error
                                                }

                                                bool iSsfarfl = false;
                                                if (sfa.RelatedFLUXLocation != null)
                                                {
                                                    iSsfarfl = true;
                                                    foreach (var sfarfl in sfa.RelatedFLUXLocation)
                                                    {

                                                    }
                                                    //FA-L00-00-0236
                                                    //FishingActivity/RelatedFLUXLocation
                                                    //Check presence. At least one occurrence must be present if the activity is a departure declaration.
                                                    if (IsFADeparture)
                                                    {
                                                        if (sfa.RelatedFLUXLocation.Length < 1)
                                                        {
                                                            //FA-L00-00-0236 - error
                                                        }
                                                    }
                                                    //FA-L02-00-0249
                                                    //FishingActivity/RelatedFLUXLocation
                                                    //Check presence. At least 2 occurrences must be present if the activity is an area entry declaration or notification and no RelatedFishingActivity is present
                                                    if (IsFAAEnOrNot)
                                                    {
                                                        if (sfa.RelatedFLUXLocation.Length < 2)
                                                        {
                                                            //FA-L02-00-0249 - error
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //FA-L02-00-0096 - error

                                                    //FA-L02-00-0258
                                                    //"FishingActivity/RelatedFLUXLocation,
                                                    //FishingActivity / RelatedFishingActivity"
                                                    //Check presence. Must be present if the activity is a fishing operation declaration and no RelatedFishingActivity is present
                                                    if (IsFAFO)
                                                    {
                                                        if (sfa.RelatedFishingActivity == null || sfa.RelatedFishingActivity.Length == 0)
                                                        {
                                                            //FA-L02-00-0258 - error
                                                        }

                                                        //FA-L02-00-0259
                                                        //FishingActivity/RelatedFLUXLocation, RelatedFishingActivity/RelatedFLUXLocation
                                                        //Check presence. Must be present if the activity is a fishing operation declaration and at least one RelatedFishingActivity is present without RelatedFLUXLocation
                                                        if (sfa.RelatedFishingActivity != null)
                                                        {
                                                            foreach (var rfa in sfa.RelatedFishingActivity)
                                                            {
                                                                if (rfa.RelatedFLUXLocation == null)
                                                                {
                                                                    //FA-L02-00-0259 - error
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L02-00-0259 - error
                                                        }
                                                    }

                                                    if (IsFAJFO)
                                                    {

                                                    }

                                                    //FA-L00-00-0421
                                                    //FishingActivity/RelatedFLUXLocation
                                                    //Check presence. At least one occurrence must be present if the activity is a
                                                    //joint fishing operation declaration.
                                                    if (IsFAJFO)
                                                    {
                                                        if (sfa.RelatedFLUXLocation == null || sfa.RelatedFLUXLocation.Length == 0)
                                                        {
                                                            //FA-L00-00-0421 - error
                                                        }
                                                    }
                                                }

                                                bool sfadps = false;
                                                bool sfadpe = false;
                                                if (sfa.SpecifiedDelimitedPeriod != null)
                                                {
                                                    foreach (var sfadp in sfa.SpecifiedDelimitedPeriod)
                                                    {
                                                        if (sfadp.StartDateTime != null)
                                                        {
                                                            sfadps = true;
                                                        }
                                                        if (sfadp.EndDateTime != null)
                                                        {
                                                            sfadpe = true;
                                                        }

                                                        //FA-L01-00-0105
                                                        //FishingActivity/SpecifiedDelimitedPeriod/DurationMeasure
                                                        //Must be a positive number or zero (>=0)
                                                        if (sfadp.DurationMeasure.Value < 0)
                                                        {
                                                            //FA-L01-00-0105 - error
                                                        }
                                                        //FA-L01-00-0106
                                                        //FishingActivity/SpecifiedDelimitedPeriod/DurationMeasure
                                                        //Check attribute unitCode. Must be MIN (minutes)
                                                        if (sfadp.DurationMeasure.unitCode != "MIN")
                                                        {
                                                            //FA-L01-00-0106 - warning
                                                        }
                                                    }
                                                }

                                                bool Issfarfaodt = false;
                                                bool Issfarfadps = false;
                                                bool Issfarfadpe = false;
                                                bool Issfarfarlfl = false;
                                                bool Issfarfasfteip = false;
                                                bool IsTCodeGShot = false;
                                                bool IsTCodeGRetr = false;
                                                #region RelatedFishingActivity
                                                if (sfa.RelatedFishingActivity != null)
                                                {
                                                    foreach (var sfarfa in sfa.RelatedFishingActivity)
                                                    {
                                                        if (sfarfa.TypeCode != null)
                                                        {
                                                            //FA-L02-00-0093
                                                            //If used as sub-activity the type can only be GEAR_SHOT, GEAR_RETRIEVAL, RELOCATION, START_ACTIVITY
                                                            if (sfarfa.TypeCode.Value != "GEAR_SHOT" || sfarfa.TypeCode.Value != "GEAR_RETRIEVAL" ||
                                                                sfarfa.TypeCode.Value != "RELOCATION" || sfarfa.TypeCode.Value != "START_ACTIVITY")
                                                            {
                                                                //FA-L02-00-0093 - warning
                                                            }

                                                            if (sfarfa.TypeCode.Value == "GEAR_SHOT")
                                                            {
                                                                IsTCodeGShot = true;
                                                                //FA-L02-00-0610
                                                                //FishingActivity/RelatedFishingActivity/TypeCode, FishingActivity/TypeCode
                                                                //If the activity is a fishing operation declaration and the RelatedFishingActivity/TypeCode is either
                                                                //GEAR_SHOT or GEAR_RETRIEVAL, then the RelatedFishingActivity shall not contain SpecifiedFACatch entities
                                                                if (IsFAFO)
                                                                {
                                                                    if (sfarfa.SpecifiedFACatch != null)
                                                                    {
                                                                        //FA-L02-00-0610 - error
                                                                    }
                                                                }
                                                            }
                                                            if (sfarfa.TypeCode.Value == "GEAR_RETRIEVAL")
                                                            {
                                                                IsTCodeGRetr = true;
                                                                //FA-L02-00-0610
                                                                //FishingActivity/RelatedFishingActivity/TypeCode, FishingActivity/TypeCode
                                                                //If the activity is a fishing operation declaration and the RelatedFishingActivity/TypeCode is either
                                                                //GEAR_SHOT or GEAR_RETRIEVAL, then the RelatedFishingActivity shall not contain SpecifiedFACatch entities
                                                                if (IsFAFO)
                                                                {
                                                                    if (sfarfa.SpecifiedFACatch != null)
                                                                    {
                                                                        //FA-L02-00-0610 - error
                                                                    }
                                                                }
                                                            }



                                                        }
                                                        if (sfarfa.OccurrenceDateTime != null)
                                                        {
                                                            Issfarfaodt = true;
                                                        }
                                                        if (sfarfa.SpecifiedDelimitedPeriod != null)
                                                        {
                                                            foreach (var sdp in sfarfa.SpecifiedDelimitedPeriod)
                                                            {
                                                                if (sdp.StartDateTime != null)
                                                                {
                                                                    Issfarfadps = true;
                                                                }
                                                                if (sdp.EndDateTime != null)
                                                                {
                                                                    Issfarfadpe = true;
                                                                }
                                                            }
                                                        }
                                                        if (sfarfa.RelatedFLUXLocation != null)
                                                        {
                                                            Issfarfarlfl = true;
                                                        }
                                                        else
                                                        {
                                                            //FA-L02-00-0096 - error
                                                        }

                                                        if (sfarfa.SpecifiedFishingTrip != null)
                                                        {
                                                            Issfarfasfteip = true;
                                                        }

                                                        #region Specified Fishing Gear

                                                        if (sfarfa.SpecifiedFishingGear != null)
                                                        {
                                                            foreach (var sfagear in sfa.SpecifiedFishingGear)
                                                            {
                                                                //FA-L01-00-0120
                                                                //FishingGear/TypeCode
                                                                //Check attribute listID. Must be GEAR_TYPE
                                                                if (sfagear.TypeCode != null)
                                                                {
                                                                    if (sfagear.TypeCode.listID != "GEAR_TYPE")
                                                                    {
                                                                        //FA-L01-00-0120 - error
                                                                    }
                                                                    //FA-L01-00-0121
                                                                    //FishingGear/TypeCode
                                                                    //Check code. Must be existing in the list specified in attribute listID
                                                                    //TODO: Check in DB if exist sfagear.TypeCode.Value
                                                                }

                                                                //FA-L01-00-0122
                                                                //FishingGear/RoleCode
                                                                //Check attribute listID. Must be FA_GEAR_ROLE
                                                                if (sfagear.RoleCode != null)
                                                                {
                                                                    foreach (var sfagearrcode in sfagear.RoleCode)
                                                                    {
                                                                        if (sfagearrcode.listID != "FA_GEAR_ROLE")
                                                                        {
                                                                            //FA-L01-00-0122 - error
                                                                        }

                                                                        //FA-L01-00-0134
                                                                        //FishingGear/RoleCode
                                                                        //Check code. Must be existing in the list specified in attribute listID
                                                                        //TODO: Check DB
                                                                    }
                                                                }

                                                                //FA-L02-00-0123
                                                                //FishingGear/ApplicableGearCharacteristic, FishingGear/TypeCode
                                                                //ApplicableGearCharacteristic must be present if FishingGear/TypeCode
                                                                //requires specific characteristics to be reported. 
                                                                //Only applicable if used in entity FishingActivity
                                                                //TODO: Connect 
                                                                string gcpars = "";
                                                                GearCh.GetGearCharsRequiredValidation(out gcpars, sfagear.TypeCode.Value);

                                                                foreach (var gchars in sfagear.ApplicableGearCharacteristic)
                                                                {
                                                                    //FA-L00-00-0124
                                                                    //GearCharacteristic/TypeCode
                                                                    //Check presence. Must be present.
                                                                    if (gchars.TypeCode == null || gchars.TypeCode.Value == "")
                                                                    {
                                                                        //FA-L00-00-0124 - error
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0125
                                                                        //GearCharacteristic/TypeCode
                                                                        //Check attribute listID. Must be FA_GEAR_CHARACTERISTIC
                                                                        if (gchars.TypeCode.listID != "FA_GEAR_CHARACTERISTIC")
                                                                        {
                                                                            //FA-L01-00-0125 - error
                                                                        }

                                                                        //FA-L01-00-0126
                                                                        //GearCharacteristic/TypeCode
                                                                        //Check the value of the code. Must be existing in the list specified 
                                                                        //in attribute listID 
                                                                        //TODO: check list
                                                                    }

                                                                    //FA-L00-00-0128
                                                                    //GearCharacteristic / ValueMeasure
                                                                    //If UN_DATA_TYPE for the characteristic (specified in 
                                                                    //GearCharacteristic / TypeCode) is MEASURE or NUMBER, 
                                                                    //ValueMeasure must be present and have a value
                                                                    if (gchars.ValueMeasure != null)
                                                                    {
                                                                        if (gchars.ValueMeasure.Value.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0128 - error
                                                                        }

                                                                        //FA-L00-00-0133
                                                                        //GearCharacteristic/ValueMeasure
                                                                        //Check attribute unitCode. The unitCode is defined in the list FLUX_UNIT. 
                                                                        //Use the value as specified in Annex 14.1.
                                                                        //TODO: Check in DB - gchars.ValueMeasure.unitCode
                                                                    }
                                                                    //FA-L00-00-0129
                                                                    //GearCharacteristic/ValueIndicator
                                                                    //If UN_DATA_TYPE for the characteristic (specified 
                                                                    //in GearCharacteristic / TypeCode) is BOOLEAN, 
                                                                    //ValueIndicator must be present and have a value.
                                                                    if (gchars.ValueIndicator != null)
                                                                    {
                                                                        if (gchars.ValueIndicator.Item == "")
                                                                        {
                                                                            //FA-L00-00-0129 - error
                                                                        }
                                                                    }
                                                                    //FA-L00-00-0130
                                                                    //GearCharacteristic/ValueCode
                                                                    //If UN_DATA_TYPE for the characteristic (specified in 
                                                                    //GearCharacteristic/TypeCode) is CODE, 
                                                                    //ValueCode must be present and have a value
                                                                    if (gchars.ValueCode != null)
                                                                    {
                                                                        if (gchars.ValueCode.Value == "")
                                                                        {
                                                                            //FA-L00-00-0130 - error
                                                                        }
                                                                        else
                                                                        {
                                                                            //FA-L03-00-0145
                                                                            //GearCharacteristic/ValueCode
                                                                            //Check presence of attribute listID. Must be present and have a value 
                                                                            //of an existing code list in MDR.
                                                                            //TODO: Check DB

                                                                            //FA-L01-00-0146
                                                                            //GearCharacteristic/ValueCode
                                                                            //Check value. Must be existing on the MDR code list specified in listID.
                                                                            //TODO: Check DB
                                                                        }
                                                                    }
                                                                    //FA-L00-00-0131
                                                                    //GearCharacteristic / Value
                                                                    //If UN_DATA_TYPE for the characteristic (specified in 
                                                                    //GearCharacteristic/TypeCode) is TEXT, 
                                                                    //Value must be present and non-empty.
                                                                    if (gchars.Value != null)
                                                                    {
                                                                        if (gchars.Value.Value == "")
                                                                        {
                                                                            //FA-L00-00-0131 - error
                                                                        }
                                                                    }
                                                                    //FA-L00-00-0132
                                                                    //GearCharacteristic / ValueQuantity
                                                                    //If UN_DATA_TYPE for the characteristic (specified in 
                                                                    //GearCharacteristic/TypeCode) is QUANTITY, 
                                                                    //ValueQuantity must be present and non-empty.
                                                                    if (gchars.ValueQuantity != null)
                                                                    {
                                                                        if (gchars.ValueQuantity.Value.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0132 - error
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        #endregion

                                                        #region SpecifiedFACatch
                                                        foreach (var sfarfa_sfc in sfarfa.SpecifiedFACatch)
                                                        {
                                                            //FA-L01-00-0422
                                                            //"FishingActivity/RelatedFLUXLocation/TypeCode, 
                                                            //FishingActivity / RelatedFishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //At least one occurrence of RelatedFLUXLocation/TypeCode must be POSITION if the activity is a joint fishing operation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                            if (sfarfa_sfc.SpeciesCode.Value == "BFT")
                                                            {
                                                                if (IsFAJFO)
                                                                {
                                                                    var frfl = sfa.RelatedFLUXLocation.Where(x => x.TypeCode.Value == "POSITION").ToList();
                                                                    if (frfl == null || frfl.Count == 0)
                                                                    {
                                                                        //FA-L01-00-0422 - error
                                                                    }

                                                                    //FA-L01-00-0428
                                                                    //FishingActivity/RelatedFishingActivity/SpecifiedFACatch/TypeCode
                                                                    //Check attribute listID. Must be FA_CATCH_TYPE if the activity is a
                                                                    //joint fishing operation declaration.
                                                                    if (sfarfa_sfc.TypeCode != null)
                                                                    {
                                                                        if (sfarfa_sfc.TypeCode.listID != "FA_CATCH_TYPE")
                                                                        {
                                                                            //FA-L01-00-0428 - error
                                                                        }
                                                                    }
                                                                }
                                                                if (IsFATransOrRelocNot)
                                                                {
                                                                    //FA-L02-00-0333
                                                                    //"FishingActivity/RelatedVesselTransportMeans,
                                                                    //SpecifiedFACatch / SpeciesCode"
                                                                    //Check presence. Must be present if the activity is a transhipment
                                                                    //or relocation notification and SpecifiedFACatch/SpeciesCode is BFT.
                                                                    if (sfa.RelatedVesselTransportMeans == null)
                                                                    {
                                                                        //FA-L02-00-0333 - error
                                                                    }

                                                                    //FA-L02-00-0335
                                                                    //"FishingActivity/SpecifiedFLUX haracteristic, 
                                                                    //FishingActivity/SpecifiedFACatch"
                                                                    //Check presence. Must be present if the activity is a relocation notification and SpecifiedFACatch/Species is BFT.
                                                                    if (sfa.SpecifiedFLUXCharacteristic == null)
                                                                    {
                                                                        //FA-L02-00-0335 - error
                                                                    }
                                                                    //FA-L02-00-0336
                                                                    //"FishingActivity/SpecifiedFLUXCharacteristic/TypeCode,
                                                                    //FishingActivity/TypeCode,
                                                                    //FishingActivity/SpecifiedFACatch/TypeCode,
                                                                    //FishingActivity/SpecifiedFACatchTypeCode/SpeciesCode"
                                                                    //If the activity is a relocation notification
                                                                    //(FishingActivity/TypeCode = RELOCATION) and SpeciesCode=BFT and
                                                                    //SpecifiedFACatchTypeCode=UNLOAD ED, then at least one
                                                                    //SpecifiedFLUXCharacteristic/TypeCode must be NB_CAGES_TOWED
                                                                    if (sfa.TypeCode.Value == "RELOCATION")
                                                                    {
                                                                        var bftrelunl = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "UNLOADED");
                                                                        if (bftrelunl != null)
                                                                        {
                                                                            var nbcages = sfa.SpecifiedFLUXCharacteristic.First(x => x.TypeCode.Value == "NB_CAGES_TOWED");
                                                                            if (nbcages == null)
                                                                            {
                                                                                //FA-L02-00-0336 - error
                                                                            }
                                                                        }
                                                                    }
                                                                    if (sfa.TypeCode.Value == "TRANSHIPMENT")
                                                                    {
                                                                        //FA-L02-00-0343
                                                                        //"FishingActivity/RelatedVesselTransportMeans/RoleCode,
                                                                        //FishingActivity/TypeCode,
                                                                        //FishingActivity/SpecifiedFACatch/SpeciesCode,
                                                                        //FishingActivity/SpecifiedFACatch/TypeCode"
                                                                        //RelatedVesselTransportMeans/RoleCod e must be DONOR if
                                                                        //the activity is a transhipment notification and
                                                                        //SpecifiedFACatch/SpeciesCode=BFT and SpecifiedFACatch.TypeCode=LOADE D
                                                                        var bftrunl = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED");
                                                                        if (bftrunl != null)
                                                                        {
                                                                            if (sfa.RelatedVesselTransportMeans != null)
                                                                            {
                                                                                var rvtmdonor = sfa.RelatedVesselTransportMeans.First(x => x.RoleCode.Value == "DONOR");
                                                                                if (rvtmdonor == null)
                                                                                {
                                                                                    //FA-L02-00-0343 - error
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                //FA-L02-00-0343 - error
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                if (IsFATransOrRelocDec)
                                                                {
                                                                    //FA-L02-00-0449
                                                                    //FishingActivity/SpecifiedFLAPDocument, SpecifiedFACatch/SpeciesCode
                                                                    //Check presence. Must be present if the activity is a relocation or
                                                                    //transhipment declaration and SpecifiedFACatch/SpeciesCode = BFT
                                                                    if (sfa.SpecifiedFLAPDocument == null)
                                                                    {
                                                                        //FA-L02-00-0449 - warning
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L02-00-0448
                                                                        //FishingActivity/SpecifiedFLAPDocument/ID, SpecifiedFACatch/SpeciesCode
                                                                        //If the activity is a relocation or transhipment declaration and
                                                                        //SpecifiedFACatch/SpeciesCode is BFT, the attribute schemeID of
                                                                        //SpecifiedFLAPDocument/ID must be ICCAT_AUTHORIZATION.
                                                                        var sfaflapid = sfa.SpecifiedFLAPDocument.First(x => x.ID.schemeID == "ICCAT_AUTHORIZATION");
                                                                        if (sfaflapid == null)
                                                                        {
                                                                            //FA-L02-00-0448 - warning
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            if (sfarfa_sfc.SpeciesCode.Value == "SAL")
                                                            {
                                                                if (IsLandOrTrans)
                                                                {

                                                                    if (sfarfa_sfc.SpecifiedFLUXLocation != null)
                                                                    {
                                                                        //FA-L02-00-0184
                                                                        //AAPProduct/UnitQuantity,
                                                                        //FishingActivity / TypeCode, 
                                                                        //FACatch / SpeciesCode, 
                                                                        //FACatch / SpecifiedFLUXLocation / ID
                                                                        //UnitQuantity must be present if FishingActivity/TypeCode is LANDING or TRANSHIPMENT and FACatch/SpeciesCode is SAL and the schemeID of FACatch/SpecifiedFLUXLocation/ID is FAO_AREA and ID value matching 27.3.d (and any sub-divisions)
                                                                        var sfarfa_sfclocid = sfarfa_sfc.SpecifiedFLUXLocation.First(x => x.ID.schemeID == "FAO_AREA");
                                                                        if (sfarfa_sfclocid != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0184 - warning
                                                                                }
                                                                            }
                                                                        }

                                                                        //FA-L02-00-0186
                                                                        //AAPProduct/UnitQuantity,
                                                                        //FishingActivity / TypeCode, 
                                                                        //FACatch / SpeciesCode, 
                                                                        //FACatch / SpecifiedFLUXLocation / ID
                                                                        //UnitQuantity must be present if FishingActivity/TypeCode is LANDING or TRANSHIPMENT and FACatch/SpeciesCode is BFT and at least one area has schemeID of FACatch/SpecifiedFLUXLocation.ID equal to GFCM_GSA
                                                                        var sfarfa_sfclocidgsa = sfarfa_sfc.SpecifiedFLUXLocation.First(x => x.ID.schemeID == "GFCM_GSA");
                                                                        if (sfarfa_sfclocidgsa != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0184 - warning
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                            }

                                                            if (sfarfa_sfc.SpeciesCode.Value == "SWO")
                                                            {
                                                                if (IsLandOrTrans)
                                                                {
                                                                    if (sfarfa_sfc.SpecifiedFLUXLocation != null)
                                                                    {
                                                                        //FA-L02-00-0187
                                                                        //AAPProduct/UnitQuantity,
                                                                        //FishingActivity / TypeCode, 
                                                                        //FACatch / SpeciesCode, 
                                                                        //FACatch / SpecifiedFLUXLocation / ID
                                                                        //UnitQuantity must be present if FishingActivity/TypeCode is LANDING or TRANSHIPMENT and FACatch/SpeciesCode is SWO and at least one area has schemeID of FACatch/SpecifiedFLUXLocation.ID equal to GFCM_GSA
                                                                        var sfarfa_sfclocidgsa = sfarfa_sfc.SpecifiedFLUXLocation.First(x => x.ID.schemeID == "GFCM_GSA");
                                                                        if (sfarfa_sfclocidgsa != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0187 - warning
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }



                                                        //FA-L00-00-0426
                                                        //FishingActivity/RelatedFishingActivity/SpecifiedFACatch
                                                        //Check presence. At least one occurrence must be present if the activity
                                                        //is a joint fishing operation declaration.
                                                        if (IsFAJFO)
                                                        {
                                                            if (sfarfa.SpecifiedFACatch.Length < 1)
                                                            {
                                                                //FA-L00-00-0426 - error
                                                            }

                                                            //FA-L01-00-0429
                                                            //"FishingActivity/RelatedFishingActivity/SpecifiedFACatch/TypeCode,
                                                            //FishingActivity/RelatedFishingActivity/SpecifiedFACatch/SpeciesCode"
                                                            //Check code. At least one occurrence of ALLOCATED_TO_QUOTA must be present if the activity
                                                            //is a joint fishing operation declaration and SpeciesCode is BFT
                                                            var fsfac = sfarfa.SpecifiedFACatch.First(x => x.SpeciesCode.Value == "BFT");
                                                            if (fsfac != null)
                                                            {
                                                                if (fsfac.TypeCode.Value != "ALLOCATED_TO_QUOTA")
                                                                {
                                                                    //FA-L01-00-0429 - error
                                                                }

                                                                ///FA-L01-00-0109
                                                                //"FishingActivity/ID, 
                                                                //FishingActivity / RelatedFishingActivity / SpecifiedFACatch / Species Code"
                                                                //Check attribute schemeID. Must be JFO if the activity is a joint fishing operation declaration and if SpeciesCode is BFT.
                                                                var fsfaid = sfa.ID.First(x => x.schemeID == "JFO");
                                                                if (fsfaid == null)
                                                                {
                                                                    ///FA-L01-00-0109 - warning
                                                                }
                                                            }

                                                        }

                                                        #endregion

                                                        //FA-L00-00-0424
                                                        //FishingActivity/RelatedFishingActivity/RelatedVesselTransportMeans
                                                        //Check presence. Must be present if the activity is a joint fishing operation declaration.
                                                        if (IsFAJFO)
                                                        {
                                                            if (sfarfa.RelatedVesselTransportMeans == null)
                                                            {
                                                                //FA-L00-00-0424 - error
                                                            }
                                                        }
                                                    }

                                                    //FA-L01-00-0423
                                                    //FishingActivity/RelatedFishingActivity/TypeCode
                                                    //If a sub-activity is provided it must have TypeCode=RELOCATION if the main activity is a joint fishing operation declaration
                                                    if (IsFAJFO)
                                                    {
                                                        var frel = sfa.RelatedFishingActivity.First(x => x.TypeCode.Value == "RELOCATION");
                                                        if (frel == null)
                                                        {
                                                            //FA-L01-00-0423 - error
                                                        }
                                                    }



                                                    //FA-L02-00-0260
                                                    //FishingActivity/RelatedFishingActivity/TypeCode, FishingActivity/TypeCode
                                                    //If the activity is a fishing operation declaration the
                                                    //FishingActivity/RelatedFishingActivity/ TypeCode must be either GEAR_SHOT
                                                    //or GEAR_RETRIEVAL if RelatedFishingActivity is present
                                                    if (IsFAFO)
                                                    {
                                                        if (IsTCodeGShot && IsTCodeGRetr)
                                                        {
                                                        }
                                                        else
                                                        {
                                                            //FA-L02-00-0260 - error
                                                        }
                                                    }
                                                }
                                                #endregion

                                                //FA-L01-00-0094
                                                //FishingActivity/OccurrenceDateTime
                                                //Check Format. Must be according to the definition provided in 7.1
                                                if (sfa.OccurrenceDateTime != null)
                                                {
                                                    // check format
                                                    // the structure is changed from string to datetime - class level
                                                }
                                                else
                                                {
                                                    //FA-L01-00-0094 - error

                                                    if (((Issfarfaodt) || (Issfarfadps && Issfarfadpe)) || (sfadps && sfadpe))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        //FA-L02-00-0095 - error
                                                    }
                                                    //FA-L00-00-0234
                                                    //FishingActivity/OccurrenceDateTime
                                                    //Check presence. Must be present if the activity is a departure declaration.
                                                    if (IsFADeparture)
                                                    {
                                                        //FA-L00-00-0234 - error
                                                    }
                                                }

                                                //FA-L01-00-0097
                                                //FishingActivity/ReasonCode
                                                //Check code. Must be existing in the list specified in attribute listID
                                                if (sfa.ReasonCode != null)
                                                {
                                                    //TODO: Check in DB nomenclature  sfa.ReasonCode.Value

                                                    //FA-L01-00-0235
                                                    //FishingActivity/ReasonCode
                                                    //Check attribute listID. Must be FA_REASON_DEPARTURE if the activity is a departure declaration
                                                    if (IsFADeparture)
                                                    {
                                                        if (sfa.ReasonCode.listID != "FA_REASON_DEPARTURE")
                                                        {
                                                            //FA-L01-00-0235 - error
                                                        }
                                                    }
                                                    //FA-L01-00-0247
                                                    //FishingActivity/ReasonCode
                                                    //Check attribute listID. Must be FA_REASON_ENTRY if the activity is an area entry declaration.
                                                    if (IsFAAEnOrNot)
                                                    {
                                                        if (sfa.ReasonCode.listID != "FA_REASON_ENTRY")
                                                        {
                                                            //FA-L01-00-0247 - error
                                                        }
                                                        //FA-L02-00-0248
                                                        //FishingActivity/SpeciesTargetCode, FishingActivity/ReasonCode
                                                        //Check presence. If the activity is a area entry declaration SpeciesTargetCode must be present if ReasonCode is TRZ or FIS
                                                        if (sfa.ReasonCode.Value == "TRZ" || sfa.ReasonCode.Value == "FIS")
                                                        {
                                                            if (sfa.SpeciesTargetCode == null)
                                                            {
                                                                //FA-L02-00-0248 - error
                                                            }
                                                        }
                                                    }
                                                }

                                                //FA-L00-00-0098
                                                //FishingActivity/FisheryTypeCode
                                                //Check attribute listID. Must be FA_FISHERY
                                                if (sfa.FisheryTypeCode != null)
                                                {
                                                    if (sfa.FisheryTypeCode.listID != "FA_FISHERY")
                                                    {
                                                        //FA-L00-00-0098 - error

                                                        //FA-L01-00-0099
                                                        //FishingActivity/FisheryTypeCode
                                                        //Check code. Must be existing in the list specified in attribute listID
                                                        //TODO: Check DB nomeclature
                                                    }
                                                }

                                                //FA-L00-00-0100
                                                //FishingActivity/SpeciesTargetCode
                                                //Check attribute listID. Must be FAO_SPECIES or TARGET_SPECIES_GROUP
                                                if (sfa.SpeciesTargetCode != null)
                                                {
                                                    if (sfa.SpeciesTargetCode.listID != "FAO_SPECIES" ||
                                                        sfa.SpeciesTargetCode.listID != "TARGET_SPECIES_GROUP")
                                                    {
                                                        //FA-L00-00-0100 - error
                                                    }

                                                    //FA-L02-00-0600
                                                    //FishingActivity/SpeciesTargetCode, 
                                                    //FishingActivity/TypeCode, 
                                                    //FAReportDocument/TypeCode
                                                    //Check attribute listID. Must be TARGET_SPECIES_GROUP 
                                                    //if FishingActivity / TypeCode = AREA_ENTRY
                                                    //and FAReportDocument/ TypeCode = DECLARATION
                                                    if (fardoc.TypeCode.Value == "DECLARATION" &&
                                                        sfa.TypeCode.Value == "AREA_ENTRY")
                                                    {
                                                        if (sfa.SpeciesTargetCode.listID != "TARGET_SPECIES_GROUP")
                                                        {
                                                            //FA-L02-00-0600 - error
                                                        }
                                                    }

                                                    //FA-L02-00-0601
                                                    //FishingActivity/SpeciesTargetCode, 
                                                    //FishingActivity/TypeCode, 
                                                    //FAReportDocument/TypeCode
                                                    //Check attribute listID. Must be FAO_SPECIES 
                                                    //if FishingActivity/TypeCode = (FISHING_OPERATION, 
                                                    //JOINT_FISHING_OPERATION or AREA_EXIT) 
                                                    //and FAReportDocument/TypeCode = DECLARATION
                                                    if (fardoc.TypeCode.Value == "DECLARATION" &&
                                                        (sfa.TypeCode.Value == "FISHING_OPERATION" ||
                                                        sfa.TypeCode.Value == "JOINT_FISHING_OPERATION" ||
                                                        sfa.TypeCode.Value == "AREA_EXIT"))
                                                    {
                                                        if (sfa.SpeciesTargetCode.listID != "FAO_SPECIES")
                                                        {
                                                            //FA-L02-00-0601 - error
                                                        }
                                                    }

                                                    //FA-L01-00-0101
                                                    //FishingActivity/SpeciesTargetCode
                                                    //Check code. Must be existing in the list specified in attribute listID
                                                    //TODO: Check DB nomenclature
                                                }

                                                //FA-L01-00-0102
                                                //FishingActivity/VesselRelatedActivityCode
                                                //Check attribute listID. Must be VESSEL_ACTIVITY
                                                if (sfa.VesselRelatedActivityCode != null)
                                                {
                                                    if (sfa.VesselRelatedActivityCode.Value != "VESSEL_ACTIVITY")
                                                    {
                                                        //FA-L01-00-0102 - error
                                                    }

                                                    //FA-L01-00-0103
                                                    //FishingActivity/VesselRelatedActivityCode
                                                    //Check code. Must be existing in the list specified in attribute listID
                                                    //TODO: Check DB Nomenclature                                                
                                                }
                                                else
                                                {
                                                    //FA-L02-00-0256
                                                    //FishingActivity/VesselRelatedActivityCode
                                                    //Check presence. Must be present if the activity is a fishing operation declaration.
                                                    if (IsFAFO)
                                                    {
                                                        //FA-L02-00-0256 - error
                                                    }
                                                }

                                                //FA-L02-00-0107
                                                //FishingActivity/SpecifiedDelimitedPeriod/StartDateTime, 
                                                //FishingActivity/TypeCode, 
                                                //FAReportDocument/TypeCode
                                                //StartDateTime must be present if FishingActivity/TypeCode is LANDING 
                                                //or TRANSHIPMENT declaration
                                                if (IsLandOrTrans && !sfadps)
                                                {
                                                    //FA-L02-00-0107 - error
                                                }

                                                //FA-L02-00-0108
                                                //FishingActivity/SpecifiedDelimitedPeriod/EndDateTime, 
                                                //FishingActivity/TypeCode,
                                                //FAReportDocument/TypeCode 
                                                //EndDateTime must be present if FishingActivity/TypeCode is LANDING 
                                                //or TRANSHIPMENT declaration
                                                if (IsLandOrTrans && !sfadpe)
                                                {
                                                    //FA-L02-00-0108 - error
                                                }

                                                //FA-L00-00-0111
                                                //FishingActivity/SpecifiedFishingTrip
                                                //Check presence. Must be present unless used 
                                                //as RelatedFishingActivity/SpecifiedFishingTrip.
                                                if (sfa.SpecifiedFishingTrip != null)
                                                {

                                                }
                                                else
                                                {
                                                    if (!Issfarfasfteip)
                                                    {
                                                        //FA-L00-00-0111 - error
                                                    }
                                                }

                                                #region Specified Fishing Gear
                                                bool IsFADepOnBoardGear = false;
                                                bool IsFAFORCodeDeployed = false;
                                                if (sfa.SpecifiedFishingGear != null)
                                                {
                                                    foreach (var sfagear in sfa.SpecifiedFishingGear)
                                                    {
                                                        //FA-L01-00-0120
                                                        //FishingGear/TypeCode
                                                        //Check attribute listID. Must be GEAR_TYPE
                                                        if (sfagear.TypeCode != null)
                                                        {
                                                            if (sfagear.TypeCode.listID != "GEAR_TYPE")
                                                            {
                                                                //FA-L01-00-0120 - error
                                                            }
                                                            //FA-L01-00-0121
                                                            //FishingGear/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: Check in DB if exist sfagear.TypeCode.Value
                                                        }

                                                        //FA-L01-00-0122
                                                        //FishingGear/RoleCode
                                                        //Check attribute listID. Must be FA_GEAR_ROLE
                                                        if (sfagear.RoleCode != null)
                                                        {
                                                            foreach (var sfagearrcode in sfagear.RoleCode)
                                                            {
                                                                if (sfagearrcode.listID != "FA_GEAR_ROLE")
                                                                {
                                                                    //FA-L01-00-0122 - error
                                                                }

                                                                //FA-L01-00-0134
                                                                //FishingGear/RoleCode
                                                                //Check code. Must be existing in the list specified in attribute listID
                                                                //TODO: Check DB

                                                                //FA-L01-00-0239
                                                                if (sfagearrcode.Value == "ONBOARD")
                                                                {
                                                                    IsFADepOnBoardGear = true;
                                                                }

                                                                if (sfagearrcode.Value == "DEPLOYED")
                                                                {
                                                                    IsFAFORCodeDeployed = true;
                                                                }
                                                            }
                                                        }

                                                        //FA-L02-00-0123
                                                        //FishingGear/ApplicableGearCharacteristic, FishingGear/TypeCode
                                                        //ApplicableGearCharacteristic must be present if FishingGear/TypeCode
                                                        //requires specific characteristics to be reported. 
                                                        //Only applicable if used in entity FishingActivity
                                                        //TODO: Connect 
                                                        string gcpars = "";
                                                        GearCh.GetGearCharsRequiredValidation(out gcpars, sfagear.TypeCode.Value);

                                                        foreach (var gchars in sfagear.ApplicableGearCharacteristic)
                                                        {
                                                            //FA-L00-00-0124
                                                            //GearCharacteristic/TypeCode
                                                            //Check presence. Must be present.
                                                            if (gchars.TypeCode == null || gchars.TypeCode.Value == "")
                                                            {
                                                                //FA-L00-00-0124 - error
                                                            }
                                                            else
                                                            {
                                                                //FA-L01-00-0125
                                                                //GearCharacteristic/TypeCode
                                                                //Check attribute listID. Must be FA_GEAR_CHARACTERISTIC
                                                                if (gchars.TypeCode.listID != "FA_GEAR_CHARACTERISTIC")
                                                                {
                                                                    //FA-L01-00-0125 - error
                                                                }

                                                                //FA-L01-00-0126
                                                                //GearCharacteristic/TypeCode
                                                                //Check the value of the code. Must be existing in the list specified 
                                                                //in attribute listID 
                                                                //TODO: check list
                                                            }

                                                            //FA-L00-00-0128
                                                            //GearCharacteristic / ValueMeasure
                                                            //If UN_DATA_TYPE for the characteristic (specified in 
                                                            //GearCharacteristic / TypeCode) is MEASURE or NUMBER, 
                                                            //ValueMeasure must be present and have a value
                                                            if (gchars.ValueMeasure != null)
                                                            {
                                                                if (gchars.ValueMeasure.Value.ToString() == "")
                                                                {
                                                                    //FA-L00-00-0128 - error
                                                                }

                                                                //FA-L00-00-0133
                                                                //GearCharacteristic/ValueMeasure
                                                                //Check attribute unitCode. The unitCode is defined in the list FLUX_UNIT. 
                                                                //Use the value as specified in Annex 14.1.
                                                                //TODO: Check in DB - gchars.ValueMeasure.unitCode
                                                            }
                                                            //FA-L00-00-0129
                                                            //GearCharacteristic/ValueIndicator
                                                            //If UN_DATA_TYPE for the characteristic (specified 
                                                            //in GearCharacteristic / TypeCode) is BOOLEAN, 
                                                            //ValueIndicator must be present and have a value.
                                                            if (gchars.ValueIndicator != null)
                                                            {
                                                                if (gchars.ValueIndicator.Item == "")
                                                                {
                                                                    //FA-L00-00-0129 - error
                                                                }
                                                            }
                                                            //FA-L00-00-0130
                                                            //GearCharacteristic/ValueCode
                                                            //If UN_DATA_TYPE for the characteristic (specified in 
                                                            //GearCharacteristic/TypeCode) is CODE, 
                                                            //ValueCode must be present and have a value
                                                            if (gchars.ValueCode != null)
                                                            {
                                                                if (gchars.ValueCode.Value == "")
                                                                {
                                                                    //FA-L00-00-0130 - error
                                                                }
                                                                else
                                                                {
                                                                    //FA-L03-00-0145
                                                                    //GearCharacteristic/ValueCode
                                                                    //Check presence of attribute listID. Must be present and have a value 
                                                                    //of an existing code list in MDR.
                                                                    //TODO: Check DB

                                                                    //FA-L01-00-0146
                                                                    //GearCharacteristic/ValueCode
                                                                    //Check value. Must be existing on the MDR code list specified in listID.
                                                                    //TODO: Check DB
                                                                }
                                                            }
                                                            //FA-L00-00-0131
                                                            //GearCharacteristic / Value
                                                            //If UN_DATA_TYPE for the characteristic (specified in 
                                                            //GearCharacteristic/TypeCode) is TEXT, 
                                                            //Value must be present and non-empty.
                                                            if (gchars.Value != null)
                                                            {
                                                                if (gchars.Value.Value == "")
                                                                {
                                                                    //FA-L00-00-0131 - error
                                                                }
                                                            }
                                                            //FA-L00-00-0132
                                                            //GearCharacteristic / ValueQuantity
                                                            //If UN_DATA_TYPE for the characteristic (specified in 
                                                            //GearCharacteristic/TypeCode) is QUANTITY, 
                                                            //ValueQuantity must be present and non-empty.
                                                            if (gchars.ValueQuantity != null)
                                                            {
                                                                if (gchars.ValueQuantity.Value.ToString() == "")
                                                                {
                                                                    //FA-L00-00-0132 - error
                                                                }
                                                            }
                                                        }

                                                    }
                                                    //FA-L01-00-0239
                                                    //FishingActivity/SpecifiedFishingGear/RoleCode
                                                    //If the activity is a departure declaration any SpecifiedFishing Gear must have RoleCode = ONBOARD
                                                    if (IsFADeparture)
                                                    {
                                                        if (!IsFADepOnBoardGear)
                                                        {
                                                            //FA-L01-00-0239
                                                        }
                                                    }

                                                    //FA-L01-00-0104
                                                    //FishingActivity/OperationsQuantity
                                                    //Must be a positive integer number or zero (>=0)
                                                    if (sfa.OperationsQuantity != null)
                                                    {
                                                        if (sfa.OperationsQuantity.Value < 0)
                                                        {
                                                            //FA-L01-00-0104 - error
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L02-00-0257
                                                        //"FishingActivity/OperationsQuantity, 
                                                        //FishingActivity / SpecifiedFishingGear / RoleCode"
                                                        //Check presence. Must be present if the activity is a fishing operation
                                                        //declaration and SpecifiedFishingGear/RoleCode is DEPLOYED.
                                                        if (IsFAFORCodeDeployed && IsFAFO)
                                                        {
                                                            //FA-L02-00-0257 - warning
                                                        }
                                                    }
                                                }

                                                #endregion

                                                #region GearProblem
                                                if (sfa.SpecifiedGearProblem != null)
                                                {
                                                    foreach (var sfagp in sfa.SpecifiedGearProblem)
                                                    {
                                                        //FA-L00-00-0135
                                                        //GearProblem/TypeCode
                                                        //Check presence. Must be present.
                                                        if (sfagp.TypeCode != null)
                                                        {
                                                            //FA-L01-00-0136
                                                            //GearProblem/TypeCode
                                                            //Check attribute listID. Must be FA_GEAR_PROBLEM.
                                                            if (sfagp.TypeCode.listID != "FA_GEAR_PROBLEM")
                                                            {
                                                                //FA-L01-00-0136 - error
                                                            }
                                                            //FA-L01-00-0137
                                                            //GearProblem/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID.
                                                            //TODO: check in DB
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0135 - error
                                                        }
                                                        //FA-L00-00-0138
                                                        //GearProblem/AffectedQuantity
                                                        //Check presence. Must be present
                                                        if (sfagp.AffectedQuantity != null)
                                                        {
                                                            //FA-L01-00-0139
                                                            //GearProblem/AffectedQuantity
                                                            //Must be a strict positive integer number. (>0)
                                                            if (sfagp.AffectedQuantity.Value <= 0)
                                                            {
                                                                //FA-L01-00-0139 - error
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0138 - error 
                                                        }
                                                        //FA-L00-00-0140
                                                        //GearProblem/RecoveryMeasureCode
                                                        //Check presence. Must be present
                                                        if (sfagp.RecoveryMeasureCode != null)
                                                        {
                                                            //FA-L01-00-0141
                                                            //GearProblem/RecoveryMeasureCode
                                                            //Check attribute listID. Must be FA_GEAR_RECOVERY.
                                                            foreach (var gprmc in sfagp.RecoveryMeasureCode)
                                                            {
                                                                if (gprmc.listID != "FA_GEAR_RECOVERY")
                                                                {
                                                                    //FA-L01-00-0141 - error
                                                                }
                                                                else
                                                                {
                                                                    //FA-L01-00-0142
                                                                    //GearProblem/RecoveryMeasureCode
                                                                    //Check code. Must be existing in the list specified in attribute listID
                                                                    //TODO: check DB
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0140 - error
                                                        }
                                                        //FA-L00-00-0143
                                                        //GearProblem/SpecifiedFLUXLocation
                                                        //Check presence. Must be present.
                                                        if (sfagp.SpecifiedFLUXLocation != null)
                                                        {
                                                            //FA-L00-00-0144
                                                            //GearProblem/SpecifiedFLUXLocation/TypeCode
                                                            //Must be POSITION
                                                            foreach (var gploc in sfagp.SpecifiedFLUXLocation)
                                                            {
                                                                if (gploc.TypeCode != null)
                                                                {
                                                                    if (gploc.TypeCode.listID != "POSITION")
                                                                    {
                                                                        //FA-L00-00-0144 - error
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0144 - error
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0143 - error
                                                        }
                                                    }
                                                }
                                                #endregion

                                                #region FACatch
                                                //FA-L00-00-0150
                                                //FACatch/TypeCode
                                                //Check presence. Must be present
                                                bool IsSCodeBFT = false;
                                                bool IsFACatchTCodeOnboard = false;
                                                if (sfa.SpecifiedFACatch != null)
                                                {
                                                    foreach (var sfafc in sfa.SpecifiedFACatch)
                                                    {
                                                        #region TypeCpde
                                                        //FA-L00-00-0150
                                                        //FACatch/TypeCode
                                                        //Check presence. Must be present
                                                        if (sfafc.TypeCode != null)
                                                        {
                                                            if (sfafc.TypeCode.Value == "")
                                                            {
                                                                //FA-L00-00-0150 - error
                                                            }
                                                            else
                                                            {

                                                            }
                                                            // FA-L01-00-0151
                                                            //FACatch/TypeCode
                                                            //Check attribute listID. Must be FA_CATCH_TYPE
                                                            if (sfafc.TypeCode.listID != "FA_CATCH_TYPE")
                                                            {
                                                                // FA-L01-00-0151 - error
                                                            }

                                                            //FA-L01-00-0152
                                                            //FACatch/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: Check DB

                                                            //FA-L02-00-0240
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //If the activity is a departure declaration any SpecifiedFACatch/TypeCode must be ONBOARD
                                                            if (IsFADeparture)
                                                            {
                                                                if (sfafc.TypeCode.Value != "ONBOARD")
                                                                {
                                                                    //FA-L02-00-0240 - error
                                                                }
                                                            }

                                                            if (sfafc.TypeCode.Value == "ONBOARD")
                                                            {
                                                                IsFACatchTCodeOnboard = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0150 - error
                                                        }
                                                        #endregion

                                                        #region SpeciesCode
                                                        //FA-L00-00-0153
                                                        //FACatch/SpeciesCode
                                                        //Check presence. Must be present.
                                                        if (sfafc.SpeciesCode != null)
                                                        {
                                                            if (sfafc.SpeciesCode.Value == "")
                                                            {
                                                                //FA-L00-00-0153 - error
                                                            }
                                                            //FA-L01-00-0154
                                                            //FACatch/SpeciesCode
                                                            //Check attribute listID. Must be FAO_SPECIES.
                                                            if (sfafc.SpeciesCode.listID != "FAO_SPECIES")
                                                            {
                                                                //FA-L01-00-0154 - error
                                                            }
                                                            //FA-L01-00-0155
                                                            //FACatch/SpeciesCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: Check DB

                                                            if (sfafc.SpeciesCode.Value == "BFT")
                                                            {
                                                                IsSCodeBFT = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0153 - error
                                                        }
                                                        #endregion

                                                        #region Unit, Weight, AppliedAAPProcess
                                                        //FA-L02-00-0156
                                                        //FACatch/UnitQuantity, 
                                                        //FACatch/WeightMeasure, 
                                                        //FACatch/AppliedAAPProcess/ResultAAPProduct/WeightMeasure
                                                        //UnitQuantity must be present if live weight nor product weight 
                                                        //are present. For info: (Live)weight: FACatch / WeightMeasure;
                                                        //(Live)nb of fish: FACatch / UnitQuantity;
                                                        //(Product)weight;
                                                        //FACatch / AppliedAAPProcess / ResultA APProduct / WeightMeasure
                                                        bool IsSfaFcWeight = false;
                                                        bool IsAAPWeight = false;
                                                        if (sfafc.WeightMeasure != null)
                                                        {
                                                            if (sfafc.UnitQuantity == null)
                                                            {
                                                                //FA-L02-00-0156 - error
                                                            }
                                                        }
                                                        if (sfafc.AppliedAAPProcess != null)
                                                        {
                                                            foreach (var aap in sfafc.AppliedAAPProcess)
                                                            {
                                                                if (aap.ResultAAPProduct != null)
                                                                {
                                                                    foreach (var ares in aap.ResultAAPProduct)
                                                                    {
                                                                        if (ares.WeightMeasure != null)
                                                                        {
                                                                            if (sfafc.UnitQuantity == null)
                                                                            {
                                                                                //FA-L02-00-0156 - error
                                                                            }
                                                                        }

                                                                        //FA-L01-00-0176
                                                                        //AAPProduct/PackagingTypeCode
                                                                        //Check attribute listID. Must be FISH_PACKAGING
                                                                        if (ares.PackagingTypeCode != null)
                                                                        {
                                                                            if (ares.PackagingTypeCode.listID != "FISH_PACKAGING")
                                                                            {
                                                                                //FA-L01-00-0176 - error
                                                                            }
                                                                            //FA-L01-00-0177
                                                                            //AAPProduct/PackagingTypeCode
                                                                            //Check code. Must be existing in the list specified in attribute listID
                                                                            //TODO: check db
                                                                        }
                                                                        //FA-L01-00-0178
                                                                        //AAPProduct/PackagingUnitAverageWeightMeasure
                                                                        //Check attribute UnitCode. Must be KGM.
                                                                        if (ares.PackagingUnitAverageWeightMeasure != null)
                                                                        {
                                                                            if (ares.PackagingUnitAverageWeightMeasure.unitCode != "KGM")
                                                                            {
                                                                                //FA-L01-00-0178 - error
                                                                            }
                                                                            //FA-L01-00-0179
                                                                            //AAPProduct/PackagingUnitAverageWeightMeasure
                                                                            //Must be a strict positive number (>0)
                                                                            if (ares.PackagingUnitAverageWeightMeasure.Value <= 0)
                                                                            {
                                                                                //FA-L01-00-0179 - error
                                                                            }
                                                                        }
                                                                        //FA-L01-00-0180
                                                                        //AAPProduct/PackagingUnitQuantity
                                                                        //Must be a strict positive integer(page 2) number (>0)
                                                                        if (ares.PackagingUnitQuantity != null)
                                                                        {
                                                                            if (ares.PackagingUnitQuantity.Value <= 0)
                                                                            {
                                                                                //FA-L01-00-0180 - error
                                                                            }
                                                                        }
                                                                        //FA-L00-00-0181
                                                                        //AAPProduct/WeightMeasure,
                                                                        //FishingActivity/TypeCode, 
                                                                        //FACatch/UnitQuantity, 
                                                                        //FACatch/WeightMeasure
                                                                        //AAPProduct/WeightMeasure must be present
                                                                        if (ares.WeightMeasure != null)
                                                                        {
                                                                            //FA-L01-00-0183
                                                                            //AAPProduct/WeightMeasure
                                                                            //Must be a strict positive number (>0)
                                                                            if (ares.WeightMeasure.Value <= 0)
                                                                            {
                                                                                //FA-L01-00-0183 - error
                                                                            }
                                                                            //FA-L01-00-0182
                                                                            //AAPProduct/WeightMeasure
                                                                            //Check attribute UnitCode. Must be KGM.
                                                                            if (ares.WeightMeasure.unitCode != "KGM")
                                                                            {
                                                                                //FA-L01-00-0182 - error
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //FA-L00-00-0181 - error
                                                                        }
                                                                        //FA-L01-00-0185
                                                                        //AAPProduct/UnitQuantity
                                                                        //Must be a strict positive number(> 0)
                                                                        if (ares.UnitQuantity != null)
                                                                        {
                                                                            if (ares.UnitQuantity.Value <= 0)
                                                                            {
                                                                                //FA-L01-00-0185 - error
                                                                            }
                                                                        }
                                                                        else
                                                                        {

                                                                        }
                                                                    }
                                                                }

                                                                if (aap.TypeCode != null)
                                                                {
                                                                    foreach (var aaptc in aap.TypeCode)
                                                                    {
                                                                        //FA-L01-00-0172
                                                                        //AAPProcess/TypeCode
                                                                        //The value of the listID attribute must be on the list FLUX_PROCESS_TYPE.
                                                                        if (aaptc.listID != "FLUX_PROCESS_TYPE")
                                                                        {
                                                                            //FA-L01-00-0172 - error
                                                                        }
                                                                        //FA-L01-00-0173
                                                                        //AAPProcess/TypeCode
                                                                        //Check code. Must be existing in the list specified in attribute listID
                                                                        //TODO: check db
                                                                    }
                                                                }

                                                                if (aap.ConversionFactorNumeric != null)
                                                                {
                                                                    //FA-L01-00-0174
                                                                    //AAPProcess/ConversionFactorNumeric
                                                                    //Must be a either equal to 0 or greater or equal to 1 (>=1)

                                                                    if (aap.ConversionFactorNumeric.Value == 0 || aap.ConversionFactorNumeric.Value >= 1)
                                                                    {

                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0174 - warning
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0441
                                                                    //AAPProcess/ConversionFactorNumeric, FACatch/SpeciesCode
                                                                    //ConversionFactorNumeric must be present if SpeciesCode=BFT.
                                                                    if (IsSCodeBFT)
                                                                    {
                                                                        //FA-L02-00-0441 - error
                                                                    }
                                                                }

                                                            }
                                                        }

                                                        if (!IsLandOrTrans)
                                                        {
                                                            //FA-L02-00-0157
                                                            //FACatch/UnitQuantity, 
                                                            //FishingActivity/TypeCode, 
                                                            //FACatch/SpeciesCode, 
                                                            //FishingActivity/RelatedFLUXLocation/ID
                                                            //UnitQuantity must be present if FishingActivity/TypeCode is not 
                                                            //LANDING nor TRANSHIPMENT and FACatch/SpeciesCode = SAL 
                                                            //and the schemeID of FishingActivity/RelatedFLUXLocation/ID 
                                                            //is FAO_AREA and the ID value is 27.3.d (and any sub-divisions)
                                                            if (sfafc.SpeciesCode.Value == "SAL")
                                                            {
                                                                foreach (var rfloc in sfa.RelatedFLUXLocation)
                                                                {
                                                                    if (rfloc.ID.Value.IndexOf("27.3.d") > -1 && rfloc.ID.schemeID == "FAO_AREA")
                                                                    {
                                                                        if (sfafc.UnitQuantity == null)
                                                                        {
                                                                            //FA-L02-00-0156 - warning
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //FA-L02-00-0410
                                                            //FACatch/UnitQuantity, 
                                                            //FishingActivity/TypeCode, 
                                                            //FACatch/SpeciesCode, 
                                                            //FishingActivity/RelatedFLUXLocation/ID
                                                            //UnitQuantity must be present if FishingActivity/TypeCode is not 
                                                            //LANDING nor TRANSHIPMENT and FACatch/SpeciesCode is BFT and 
                                                            //at least one area has schemeID of FishingActivity/RelatedFLUXLocation/ID 
                                                            //equal to GFCM_GSA and a valid value
                                                            if (sfafc.SpeciesCode.Value == "BFT")
                                                            {
                                                                foreach (var rfloc in sfa.RelatedFLUXLocation)
                                                                {
                                                                    if (rfloc.ID.schemeID == "GFCM_GSA")
                                                                    {
                                                                        if (rfloc.ID.Value != null)
                                                                        {
                                                                            if (sfafc.UnitQuantity == null)
                                                                            {
                                                                                //FA-L02-00-0410 - warning
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //FA-L02-00-0411
                                                            //FACatch/UnitQuantity, 
                                                            //FishingActivity/TypeCode, 
                                                            //FACatch/SpeciesCode, 
                                                            //FishingActivity/RelatedFLUXLocation/ID
                                                            //UnitQuantity must be present if FishingActivity/TypeCode is not 
                                                            //LANDING nor TRANSHIPMENT and FACatch/SpeciesCode is SWO and 
                                                            //at least one area has schemeID of FishingActivity/RelatedFLUXLocation/ID 
                                                            //equal to GFCM_GSA and valid value.
                                                            if (sfafc.SpeciesCode.Value == "SWO")
                                                            {
                                                                foreach (var rfloc in sfa.RelatedFLUXLocation)
                                                                {
                                                                    if (rfloc.ID.schemeID == "GFCM_GSA")
                                                                    {
                                                                        if (rfloc.ID.Value != null)
                                                                        {
                                                                            if (sfafc.UnitQuantity == null)
                                                                            {
                                                                                //FA-L02-00-0411 - warning
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }

                                                        //FA-L01-00-0158
                                                        //FACatch/UnitQuantity, FishingActivity/TypeCode
                                                        //Must be positive integer number or zero (>=0)
                                                        if (sfafc.UnitQuantity != null)
                                                        {
                                                            if (sfafc.UnitQuantity.Value < 0)
                                                            {
                                                                //FA-L01-00-0158 - error
                                                            }
                                                        }

                                                        //FA-L01-00-0160
                                                        //FACatch/WeightMeasure
                                                        //Check attribute UnitCode. Must be KGM (kilograms)
                                                        if (sfafc.WeightMeasure != null)
                                                        {
                                                            if (sfafc.WeightMeasure.unitCode != "KGM")
                                                            {
                                                                //FA-L01-00-0160 - error
                                                            }
                                                            //FA-L01-00-0161
                                                            //FACatch/WeightMeasure
                                                            if (sfafc.WeightMeasure.Value < 0)
                                                            {
                                                                //FA-L01-00-0161 - error
                                                            }
                                                        }

                                                        //FA-L01-00-0163
                                                        //FACatch/WeighingMeansCode
                                                        //Check attribute listID. Must be WEIGHT_MEANS
                                                        if (sfafc.WeighingMeansCode != null)
                                                        {
                                                            if (sfafc.WeighingMeansCode.listID != "WEIGHT_MEANS")
                                                            {
                                                                //FA-L01-00-0163 - error
                                                            }
                                                            //FA-L01-00-0164
                                                            //FACatch/WeighingMeansCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: check db
                                                        }

                                                        if (sfafc.SpecifiedSizeDistribution != null)
                                                        {
                                                            if (sfafc.SpecifiedSizeDistribution.ClassCode != null)
                                                            {
                                                                foreach (var ssdcc in sfafc.SpecifiedSizeDistribution.ClassCode)
                                                                {
                                                                    //FA-L01-00-0166
                                                                    //FACatch/SpecifiedSizeDistribution/ClassCode
                                                                    //Check attribute listID. Must be FISH_SIZE_CLASS
                                                                    if (ssdcc.listID != "FISH_SIZE_CLASS")
                                                                    {
                                                                        //FA-L01-00-0166 - error
                                                                    }
                                                                    //FA-L01-00-0167
                                                                    //FACatch/SpecifiedSizeDistribution/ClassCode
                                                                    //Check code. Must be existing in the list specified in attribute listID
                                                                    //TODO: check db
                                                                }
                                                            }
                                                            //FA-L01-00-0169
                                                            //FACatch/SpecifiedSizeDistribution/CategoryCode
                                                            //Check attribute listID. Must be FA_BFT_SIZE_CATEGORY
                                                            if (sfafc.SpecifiedSizeDistribution.CategoryCode != null)
                                                            {
                                                                if (sfafc.SpecifiedSizeDistribution.CategoryCode.listID != "FA_BFT_SIZE_CATEGORY")
                                                                {
                                                                    //FA-L01-00-0169 - error
                                                                }
                                                                //FA-L02-00-0170
                                                                //FACatch/SpecifiedSizeDistribution/CategoryCode
                                                                //Check code. Must be existing in the list specified in attribute listID, valid for the species provided
                                                                //TODO: check db
                                                            }
                                                        }

                                                        #endregion

                                                        #region UsedFishing Gear
                                                        //FA-L00-00-0641
                                                        //FACatch/UsedFishingGear
                                                        //Check presence. At most one occurrence.
                                                        if (sfafc.UsedFishingGear != null)
                                                        {
                                                            if (sfafc.UsedFishingGear.Length < 1)
                                                            {
                                                                //FA-L00-00-0641 - error
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0641 - error
                                                        }
                                                        #endregion

                                                        #region Specified Flux Location
                                                        bool IsSFLocIDSch = false;
                                                        int IsSFLocIDSchGSACount = 0;
                                                        int IsSFLocIDSchEZCount = 0;
                                                        if (sfafc.SpecifiedFLUXLocation != null)
                                                        {

                                                            foreach (var sfl in sfafc.SpecifiedFLUXLocation)
                                                            {
                                                                //FA-L02-00-0662
                                                                //FACatch/SpecifiedFLUXLocation/ID
                                                                //Check attribute schemeID. At most one occurrence can be FAO_AREA for all
                                                                //SpecifiedFLUXLocations to this FishingActivity entity (excluding subactivities).
                                                                if (sfl.ID != null)
                                                                {
                                                                    if (sfl.ID.schemeID == "FAO_AREA")
                                                                    {
                                                                        IsSFLocIDSch = true;
                                                                    }
                                                                    //FA-L02-00-0663
                                                                    //FACatch/SpecifiedFLUXLocation/ID
                                                                    //Check attribute schemeID. At most one occurrence can be GFCM_GSA for
                                                                    //all SpecifiedFLUXLocations to this FishingActivity entity (excluding subactivities).
                                                                    if (sfl.ID.schemeID == "GFCM_GSA")
                                                                    {
                                                                        IsSFLocIDSchGSACount++;
                                                                    }
                                                                    //FA-L02-00-0664
                                                                    //FACatch/SpecifiedFLUXLocation/ID
                                                                    //Check attribute schemeID. At most one occurrence can be EFFORT_ZONE for all
                                                                    //SpecifiedFLUXLocations to this FishingActivity entity (excluding subactivities).
                                                                    if (sfl.ID.schemeID == "EFFORT_ZONE")
                                                                    {
                                                                        IsSFLocIDSchEZCount++;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (!IsSFLocIDSch)
                                                        {
                                                            //FA-L02-00-0662 - error
                                                        }
                                                        if (IsSFLocIDSchGSACount > 1)
                                                        {
                                                            //FA-L02-00-0663 - error
                                                        }
                                                        if (IsSFLocIDSchEZCount > 1)
                                                        {
                                                            //FA-L02-00-0663 - error
                                                        }
                                                        #endregion



                                                    }
                                                    #region Fishing trip

                                                    if (sfa.SpecifiedFishingTrip != null)
                                                    {
                                                        //FA-L00-00-0190
                                                        //FishingTrip/ID
                                                        //Check presence. Must be present.
                                                        if (sfa.SpecifiedFishingTrip.ID != null)
                                                        {
                                                            foreach (var sfaftid in sfa.SpecifiedFishingTrip.ID)
                                                            {
                                                                //FA-L01-00-0191
                                                                //FishingTrip/ID
                                                                //Check attribute schemeID. Must be EU_TRIP_ID
                                                                if (sfaftid.schemeID != "EU_TRIP_ID")
                                                                {
                                                                    //FA-L01-00-0191 - error
                                                                }
                                                                //FA-L01-00-0192
                                                                //FishingTrip/ID
                                                                //Check format. Must be according to schemeID rules.
                                                                // TODO: Check scheme -> FLUX_FA.MDR_FA_Trip_Id_Type
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0190 - error
                                                        }

                                                        if (sfa.SpecifiedFishingTrip.TypeCode != null)
                                                        {
                                                            //FA-L01-00-0193
                                                            //FishingTrip/TypeCode
                                                            //Check attribute listID. Must be FISHING_TRIP_TYPE
                                                            if (sfa.SpecifiedFishingTrip.TypeCode.listID != "FISHING_TRIP_TYPE")
                                                            {
                                                                //FA-L01-00-0193 - error
                                                            }
                                                            //FA-L01-00-0194
                                                            //FishingTrip/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID
                                                            //TODO: check in db -FLUX_FA.MDR_Fishing_Trip_Type
                                                        }
                                                    }

                                                    #endregion

                                                    #region Flux Location
                                                    HashSet<string> DepTypeCode = new HashSet<string>();
                                                    HashSet<string> AEnNotTypeCode = new HashSet<string>();
                                                    if (sfa.RelatedFLUXLocation != null)
                                                    {
                                                        bool IsSFARFLLocOrAdd = false;
                                                        bool IsSFARFLPosOrAdd = false;
                                                        bool IsSFARFLArea = false;
                                                        bool IsSFARFLLoc = false;
                                                        bool IsSFARFLPos = false;
                                                        bool IsSFARFLTCode = false;
                                                        foreach (var sfarfl in sfa.RelatedFLUXLocation)
                                                        {
                                                            //FA-L00-00-0195
                                                            //FLUXLocation/TypeCode
                                                            //Check presence. Must be present.
                                                            if (sfarfl.TypeCode != null)
                                                            {
                                                                IsSFARFLTCode = true;
                                                                //FA-L01-00-0196
                                                                //FLUXLocation/TypeCode
                                                                //Check attribute listID. Must be FLUX_LOCATION_TYPE
                                                                if (sfarfl.TypeCode.listID != "FLUX_LOCATION_TYPE")
                                                                {
                                                                    //FA-L01-00-0196 - error
                                                                }
                                                                //FA-L01-00-0197
                                                                //FLUXLocation/TypeCode
                                                                //Check code. Must be existing in the list specified in attribute listID
                                                                //TODO: check db - MDR_FLUX_Location_Type

                                                                //FA-L02-00-0198
                                                                if (sfarfl.TypeCode.Value == "LOCATION" || sfarfl.TypeCode.Value == "ADDRESS")
                                                                {
                                                                    IsSFARFLLocOrAdd = true;
                                                                }
                                                                if (sfarfl.TypeCode.Value == "POSITION" || sfarfl.TypeCode.Value == "ADDRESS")
                                                                {
                                                                    IsSFARFLPosOrAdd = true;
                                                                }
                                                                if (sfarfl.TypeCode.Value == "AREA")
                                                                {
                                                                    IsSFARFLArea = true;
                                                                    AEnNotTypeCode.Add(sfarfl.TypeCode.Value);
                                                                }
                                                                if (sfarfl.TypeCode.Value == "LOCATION")
                                                                {
                                                                    IsSFARFLLoc = true;
                                                                }
                                                                if (sfarfl.TypeCode.Value == "POSITION")
                                                                {
                                                                    IsSFARFLPos = true;
                                                                    AEnNotTypeCode.Add(sfarfl.TypeCode.Value);
                                                                }


                                                                if (IsFADeparture)
                                                                {
                                                                    //FA-L01-00-0237
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                    //If the activity is a departure declaration at least one RelatedFLUX Location/TypeCode must be LOCATION or POSITION
                                                                    if (IsSFARFLPos || IsSFARFLLoc)
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0237 - warning
                                                                    }
                                                                    //FA-L01-00-0238
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                    //A type of FLUXLocation of a particular type may only appear once if the activity is a departure declaration
                                                                    if (!DepTypeCode.Add(sfarfl.TypeCode.Value))
                                                                    {
                                                                        //FA-L01-00-0238 - error
                                                                    }
                                                                }
                                                                if (IsFAAEnOrNot)
                                                                {
                                                                    //FA-L02-00-0250
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                    //If the activity is an area entry declaration or notification, one occurrence must be POSITION; another occurrence must be AREA.
                                                                    if (AEnNotTypeCode.Contains("AREA") && AEnNotTypeCode.Contains("POSITION"))
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L02-00-0250 - error
                                                                    }

                                                                    //FA-L02-00-0251
                                                                    //"FishingActivity/RelatedFLUXLocation/ID, 
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode, 
                                                                    //FishingActivity/FAReportDocument/TypeCode"
                                                                    //Check schemeID. If the RelatedFLUXLocation/TypeCode is AREA the
                                                                    //schemeID of RelatedFLUXLocation/ID must be EFFORT_ZONE if the
                                                                    //activity is an area entry declaration
                                                                    //(FAReportDocument/TypeCode=DECL ARATION).
                                                                    if (IsSFARFLArea)
                                                                    {
                                                                        if (sfarfl.ID.schemeID != "EFFORT_ZONE")
                                                                        {
                                                                            if (fardoc.TypeCode.Value == "DECLARATION")
                                                                            {
                                                                                //FA-L02-00-0251 - warning
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L00-00-0195 - error
                                                            }

                                                            //FA-L02-00-0198
                                                            //FLUXLocation/CountryID, FLUXLocation/TypeCode
                                                            //CountryID must be present if TypeCode is LOCATION or ADDRESS.
                                                            if (IsSFARFLLocOrAdd)
                                                            {
                                                                if (sfarfl.CountryID == null)
                                                                {
                                                                    //FA-L02-00-0198 - error
                                                                }
                                                            }

                                                            if (sfarfl.CountryID != null)
                                                            {
                                                                //FA-L01-00-0199
                                                                //FLUXLocation/CountryID
                                                                //Check attribute schemeID. Must be TERRITORY.
                                                                if (sfarfl.CountryID.schemeID != "TERRITORY")
                                                                {
                                                                    //FA-L01-00-0199 - error
                                                                }

                                                                //FA-L01-00-0200
                                                                //FLUXLocation/CountryID
                                                                //Check code. Must be existing in the list specified in attribute schemeID.
                                                                //TODO: check db - MDR_Territory
                                                            }

                                                            if (sfarfl.ID != null)
                                                            {
                                                                //FA-L01-00-0203
                                                                //FLUXLocation/ID
                                                                //Check value. Must be existing in the list specified in attribute schemeID
                                                                //TODO: check db - ??

                                                                //FA-L01-00-0202
                                                                //FLUXLocation/ID, FLUXLocation/TypeCode
                                                                //Check attribute schemeID of ID. In case TypeCode= AREA the schemeID must be FAO_AREA,
                                                                //STAT_RECTANGLE, TERRITORY, EFFORT_ZONE, GFCM_GSA, MANAGEMENT_AREA.
                                                                //In case TypeCode= LOCATION: the schemeID must be LOCATION or FARM
                                                                if (IsSFARFLArea)
                                                                {
                                                                    List<string> tmparea = new List<string>() {
                                                                    "FAO_AREA", "STAT_RECTANGLE", "TERRITORY", "EFFORT_ZONE", "GFCM_GSA", "MANAGEMENT_AREA"
                                                                };
                                                                    if (tmparea.IndexOf(sfarfl.ID.schemeID) < 0)
                                                                    {
                                                                        // //FA-L01-00-0202 - error
                                                                    }
                                                                }
                                                                if (IsSFARFLLoc)
                                                                {
                                                                    List<string> tmparea = new List<string>() {
                                                                    "LOCATION", "FARM"
                                                                };
                                                                    if (tmparea.IndexOf(sfarfl.ID.schemeID) < 0)
                                                                    {
                                                                        //FA-L01-00-0202 - error
                                                                    }
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0201
                                                                //FLUXLocation/ID, FLUXLocation/TypeCode
                                                                //ID must be present, unless TypeCode is POSITION or ADDRESS
                                                                if (!IsSFARFLPosOrAdd)
                                                                {
                                                                    //FA-L02-00-0201 - error
                                                                }
                                                            }

                                                            if (sfarfl.RegionalFisheriesManagementOrganizationCode != null)
                                                            {
                                                                //FA-L01-00-0204
                                                                //FLUXLocation/RegionalFisheriesManagementOrganisationCode
                                                                //Check attribute listID. Must be RFMO.
                                                                if (sfarfl.RegionalFisheriesManagementOrganizationCode.listID != "RFMO")
                                                                {

                                                                }
                                                                //FA-L01-00-0205
                                                                //FLUXLocation/RegionalFisheriesManagementOrganisationCode
                                                                //Check value. Must be existing in the list specified in attribute listID
                                                                //TODO: check db - MDR_RFMOs
                                                            }
                                                            if (sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate != null)
                                                            {
                                                                //FA-L00-00-0207
                                                                //FLUXLocation/SpecifiedPhysicalFLUXGeographicalCoordinate/LatitudeMeasure
                                                                //Check presence. Must be present.
                                                                if (sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure != null)
                                                                {
                                                                    //FA-L01-00-0209
                                                                    //FLUXLocation/SpecifiedPhysicalFLUXGeographicalCoordinate/LatitudeMeasure
                                                                    //Must be a number with at least 1 decimal position between -90.0 and 90.0 included.
                                                                    if (sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value >= -90 &&
                                                                       sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value <= 90)
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0209 - error
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0207 - error
                                                                }
                                                                //FA-L00-00-0210
                                                                //FLUXLocation/SpecifiedPhysicalFLUXGeographicalCoordinate/LongitudeMeasure
                                                                //Check presence. Must be present.
                                                                if (sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure != null)
                                                                {
                                                                    //FA-L01-00-0212
                                                                    //FLUXLocation/SpecifiedPhysicalFLUXGeographicalCoordinate/LongitudeMeasure
                                                                    //Must be a number with at least 1 decimal position between -180.0 and 180.0 included.
                                                                    if (sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value >= -180 &&
                                                                       sfarfl.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value <= 180)
                                                                    {
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0212 - error
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0210 - error
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0206
                                                                //"FLUXLocation/SpecifiedPhysicalFLUXGeographicalCoordinate, 
                                                                //FLUXLocation / TypeCode"
                                                                //SpecifiedPhysicalFLUXGeographicalCo ordinate must be present if FLUXLocation/TypeCode=POSITION.
                                                                if (IsSFARFLPos)
                                                                {
                                                                    //FA-L02-00-0206 - error
                                                                }
                                                            }
                                                            if (IsSFARFLTCode)
                                                            {
                                                                //FA-L02-00-0215
                                                                //PhysicalStructuredAddress, FLUXLocation/TypeCode
                                                                //Check presence. Must be present if location TypeCode is ADDRESS.
                                                                if (sfarfl.PhysicalStructuredAddress == null)
                                                                {
                                                                    //FA-L02-00-0215 - error
                                                                }
                                                            }

                                                            if (sfarfl.ApplicableFLUXCharacteristic != null)
                                                            {
                                                                foreach (var afch in sfarfl.ApplicableFLUXCharacteristic)
                                                                {

                                                                    //FA-L00-00-0220
                                                                    //FLUXCharacteristic/TypeCode
                                                                    //Check presence. Must be present.
                                                                    if (afch.TypeCode != null)
                                                                    {
                                                                        //FA-L01-00-0216
                                                                        //ApplicableFLUXCharacteristic/TypeCode
                                                                        //Check attribute listID. Must be FLUX_LOCATION_CHARACTERIST IC if used in entity FLUXLocation
                                                                        if (afch.TypeCode.listID != "FLUX_LOCATION_CHARACTERIST")
                                                                        {
                                                                            //FA-L01-00-0216 - error
                                                                        }

                                                                        //FA-L01-00-0221
                                                                        //FLUXCharacteristic/TypeCode
                                                                        //Check the value of the code. Must be existing in the list specified in attribute listID
                                                                        //TODO: check db - FLUX_LOCATION_CHARACTERISTIC
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L00-00-0220 - error
                                                                    }

                                                                    //FA-L00-00-0223
                                                                    //FLUXCharacteristic/ValueMeasure
                                                                    //If UN_DATA_TYPE for the characteristic
                                                                    //(specified in FLUXCharacteristic/TypeCode) is MEASURE or NUMBER,
                                                                    //ValueMeasure must be present and have a value.

                                                                    if (afch.ValueMeasure != null)
                                                                    {
                                                                        if (afch.ValueMeasure.Value.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0223 - error
                                                                        }

                                                                        //FA-L00-00-0229
                                                                        //FLUXCharacteristic/ValueMeasure
                                                                        //Check attribute unitCode. The unitCode is defined in the list FLUX_UNIT.
                                                                        //TODO: Check in DB - afch.ValueMeasure.unitCode
                                                                    }

                                                                    //FA-L00-00-0224
                                                                    //FLUXCharacteristic/ValueDateTime
                                                                    //If UN_DATA_TYPE for the characteristic (specified in
                                                                    //FLUXCharacteristic/TypeCode) is DATETIME, ValueDateTime
                                                                    //must be present.
                                                                    if (afch.ValueDateTime != null)
                                                                    {
                                                                        if (afch.ValueDateTime.Item.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0224 - error
                                                                        }
                                                                    }

                                                                    //FA-L00-00-0225
                                                                    //FLUXCharacteristic/ValueIndicator
                                                                    //If UN_DATA_TYPE for the characteristic (specified in
                                                                    //FLUXCharacteristic/TypeCode) is BOOLEAN,
                                                                    //ValueIndicator must be present and have a value.
                                                                    if (afch.ValueIndicator != null)
                                                                    {
                                                                        if (afch.ValueIndicator.Item.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0225 - error
                                                                        }
                                                                    }

                                                                    //FA-L00-00-0225
                                                                    //FLUXCharacteristic/ValueIndicator
                                                                    //If UN_DATA_TYPE for the characteristic (specified in
                                                                    //FLUXCharacteristic/TypeCode) is BOOLEAN,
                                                                    //ValueIndicator must be present and have a value.
                                                                    if (afch.ValueIndicator != null)
                                                                    {
                                                                        if (afch.ValueIndicator.Item.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0225 - error
                                                                        }
                                                                    }

                                                                    //FA-L00-00-0226
                                                                    //FLUXCharacteristic/ValueCode
                                                                    //If UN_DATA_TYPE for the characteristic (specified in
                                                                    //FLUXCharacteristic/TypeCode) is CODE, ValueCode must be
                                                                    //present and have a value.
                                                                    if (afch.ValueCode != null)
                                                                    {
                                                                        if (afch.ValueCode.Value.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0226 - error
                                                                        }
                                                                        if (afch.ValueCode.listID != "")
                                                                        {
                                                                            //FA-L03-00-0147
                                                                            //FLUXCharacteristic/ValueCode
                                                                            //Check presence of attribute listID. Must be present and have a value of an existing code list in MDR.

                                                                            //FA-L01-00-0148
                                                                            //FLUXCharacteristic/ValueCode
                                                                            //Check value. Must be existing on the MDR code list specified in listID.

                                                                        }
                                                                    }

                                                                    //FA-L00-00-0227
                                                                    //FLUXCharacteristic/Value
                                                                    //If UN_DATA_TYPE for the characteristic (specified in FLUXCharacteristic/TypeCode)
                                                                    //is TEXT, Value must be present and nonempty
                                                                    if (afch.Value != null)
                                                                    {
                                                                        foreach (var afchv in afch.Value)
                                                                        {
                                                                            if (afchv.Value == "")
                                                                            {
                                                                                //FA-L00-00-0227 - error
                                                                            }
                                                                        }
                                                                    }

                                                                    //FA-L00-00-0228
                                                                    //FLUXCharacteristic/ValueQuantity
                                                                    //If UN_DATA_TYPE for the characteristic (specified in
                                                                    //FLUXCharacteristic/TypeCode) is QUANTITY,
                                                                    //ValueQuantity must be present and have a value.
                                                                    if (afch.ValueQuantity != null)
                                                                    {
                                                                        if (afch.ValueQuantity.Value.ToString() == "")
                                                                        {
                                                                            //FA-L00-00-0228 - error
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                    if (IsFAReloc)
                                                    {
                                                        var ftcunloaded = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "UNLOADED");
                                                        var ftcloaded = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "LOADED");
                                                        //FA-L02-00-0268
                                                        //"FishingActivity/RelatedVesselTransportMeans, 
                                                        //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                        //Check presence. Must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                        var fspbft = sfa.SpecifiedFACatch.First(x => x.SpeciesCode.Value == "BFT");
                                                        if (fspbft != null)
                                                        {
                                                            if (sfa.RelatedVesselTransportMeans == null)
                                                            {
                                                                //FA-L02-00-0268 - error
                                                            }

                                                            //FA-L02-00-0269
                                                            //"FishingActivity/DestinationVesselStorageCharacteristic, 
                                                            //SpecifiedFACatch/TypeCode, 
                                                            //FishingActivity/SpecifiedFACatch/SpeciesCode"
                                                            //Check presence. Must be present if the activity is a relocation declaration and
                                                            //SpecifiedFACatch/TypeCode is UNLOADED and SpecifiedFACatch/SpeciesCode is BFT.=                                                        
                                                            if (ftcunloaded != null)
                                                            {
                                                                if (sfa.DestinationVesselStorageCharacteristic == null)
                                                                {
                                                                    //FA-L02-00-0269 - error
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0417
                                                                    //"FishingActivity/DestinationVesselStorageCharacteristic/ID, 
                                                                    //FishingActivity/SpecifiedFACatch/TypeCode, 
                                                                    //FishingActivity/SpecifiedFACatch/SpeciesCode"
                                                                    //Check presence. Must be present if the activity is a relocation declaration and SpecifiedFACatch/TypeCode is
                                                                    //UNLOADED and SpecifiedFACatch/SpeciesCode is BFT. The schemeID must be BFT-CAGE-ID
                                                                    if (sfa.DestinationVesselStorageCharacteristic.ID == null)
                                                                    {
                                                                        //FA-L02-00-0417 - error
                                                                    }
                                                                    else
                                                                    {
                                                                        if (sfa.DestinationVesselStorageCharacteristic.ID.Value != "BFT-CAGE-ID")
                                                                        {
                                                                            //FA-L02-00-0417 - error
                                                                        }
                                                                    }
                                                                }

                                                                //FA-L02-00-0273
                                                                //"FishingActivity/RelatedVesselTransportMeans/RoleCode, 
                                                                //FishingActivity / SpecifiedFACatch / TypeCode"
                                                                //RelatedVesselTransportMeans/RoleCod e must be RECEIVER (receiving vessel) if the activity is a relocation declaration and SpecifiedFACatch/TypeCode is UNLOADED
                                                                var fsfarvtmrc = sfa.RelatedVesselTransportMeans.Select(x => x.RoleCode.Value == "RECEIVER").ToList();
                                                                if (fsfarvtmrc != null)
                                                                {
                                                                    //FA-L02-00-0273 - error
                                                                }

                                                                //FA-L02-00-0275
                                                                //"FishingActivity/SpecifiedFACatch/DestinationFLUXLocation, 
                                                                //FishingActivity/RelatedVesselTransportMeans, 
                                                                //FishingActivity/SpecifiedFACatch/TypeCode, 
                                                                //FishingActivity/SpecifiedFACatch/SpeciesCode"
                                                                //DestinationFLUXLocation must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                                //and SpecifiedFACatch/TypeCode is UNLOADED and RelatedVesselTransportMeans is present
                                                                if (sfa.RelatedVesselTransportMeans != null)
                                                                {
                                                                    var sfadestexist = sfa.SpecifiedFACatch.Select(x => x.DestinationFLUXLocation != null).ToList();
                                                                    if (sfadestexist == null || sfadestexist.Count == 0)
                                                                    {
                                                                        //FA-L02-00-0275 - error
                                                                    }
                                                                }
                                                            }

                                                            //FA-L02-00-0270
                                                            //"FishingActivity/RelatedVesselTransportMeans/ID, 
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //Check attribute schemeID. There must be at least one occurrence of ID with schemeID=ICCAT if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                            var fsfarvtmid = sfa.RelatedVesselTransportMeans.SelectMany(x => x.ID).Select(x => x.schemeID == "ICCAT").ToList();
                                                            if (fsfarvtmid == null)
                                                            {
                                                                //FA-L02-00-0270 - error
                                                            }

                                                            //FA-L02-00-0276
                                                            //"FishingActivity/SpecifiedFACatch/DestinationFLUXLocation/TypeCode,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //DestinationFLUXLocation/TypeCode must be LOCATION if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var fsfcdlocloc = sfa.SpecifiedFACatch.SelectMany(x => x.DestinationFLUXLocation).Select(x => x.TypeCode.Value == "LOCATION").ToList();
                                                            if (fsfcdlocloc == null || fsfcdlocloc.Count == 0)
                                                            {
                                                                //FA-L02-00-0276 - error
                                                            }

                                                            //FA-L02-00-0277
                                                            //"FishingActivity/SpecifiedFACatch/DestinationFLUXLocation/ID,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //"Check attribute schemeID. Must be FARM if the activity is a relocation declaration and
                                                            //SpecifiedFACatch/Species is BFT. The
                                                            //value must be from the list specified in
                                                            //the schemeID."
                                                            var fsfcdlocfarm = sfa.SpecifiedFACatch.SelectMany(x => x.DestinationFLUXLocation).Select(x => x.ID.schemeID == "FARM").ToList();
                                                            if (fsfcdlocloc == null || fsfcdlocloc.Count == 0)
                                                            {
                                                                //FA-L02-00-0277 - error
                                                            }

                                                            //FA-L02-00-0430
                                                            //"FAReportDocument/RelatedReportID,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //RelatedReportID must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                            if (fardoc.RelatedReportID == null)
                                                            {
                                                                //FA-L02-00-0430 - warning
                                                            }

                                                            //FA-L02-00-0442
                                                            //"FishingActivity/RelatedVesselTransportMeans/Name,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //Name of the related vessel must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var sfavtmname = sfa.RelatedVesselTransportMeans.Select(x => x.Name != null).ToList();
                                                            if (sfavtmname == null || sfavtmname.Count == 0)
                                                            {
                                                                //FA-L02-00-0442 - error
                                                            }

                                                            //FA-L02-00-0446
                                                            //"FishingActivity/RelatedVesselTransportMeans/SpecifiedContactParty/RoleCode,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //Check presence. Must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var sfavtmscprc = sfa.RelatedVesselTransportMeans.SelectMany(x => x.SpecifiedContactParty).Select(x => x.RoleCode != null).ToList();
                                                            if (sfavtmscprc == null || sfavtmscprc.Count == 0)
                                                            {
                                                                //FA-L02-00-0446 - error
                                                            }

                                                            //FA-L02-00-0447
                                                            //"FishingActivity/RelatedVesselTransportMeans/SpecifiedContactParty/RoleCode,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //The listID of RoleCode must be FLUX_CONTACT_ROLE and the value must be MASTER if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                            var sfavtmscprcmaster = sfa.RelatedVesselTransportMeans.SelectMany(x => x.SpecifiedContactParty)
                                                                                .SelectMany(x => x.RoleCode)
                                                                                .Select(x => x.Value == "MASTER" && x.listID == "FLUX_CONTACT_ROLE")
                                                                                .ToList();
                                                            if (sfavtmscprcmaster == null || sfavtmscprcmaster.Count == 0)
                                                            {
                                                                //FA-L02-00-0447 - error
                                                            }
                                                        }

                                                        if (ftcloaded != null)
                                                        {
                                                            //FA-L02-00-0274
                                                            //"FishingActivity/RelatedVesselTransportMeans/RoleCode, 
                                                            //SpecifiedFACatch / TypeCode"
                                                            //RelatedVesselTransportMeans/RoleCod e must be DONOR (donor vessel) if the activity is a relocation declaration and SpecifiedFACatch.TypeCode is LOADED
                                                            var fsfarvtmrcdonor = sfa.RelatedVesselTransportMeans.Select(x => x.RoleCode.Value == "DONOR").ToList();
                                                            if (fsfarvtmrcdonor == null)
                                                            {
                                                                //FA-L02-00-0274 - error
                                                            }
                                                        }
                                                    }

                                                    if (IsFADiscard)
                                                    {
                                                        //FA-L02-00-0283
                                                        //FishingActivity/SpecifiedFACatch/TypeCode
                                                        //Must have at least one occurrence with Type DISCARDED or DEMINIMIS if the activity is a discard declaration.
                                                        var sfascdisc = sfa.SpecifiedFACatch.Select(x => x.TypeCode.Value == "DISCARDED" || x.TypeCode.Value == "DEMINIMIS").ToList();
                                                        if (sfascdisc == null || sfascdisc.Count == 0)
                                                        {
                                                            //FA-L02-00-0283 - error
                                                        }
                                                    }

                                                    if (IsFAAExOrNot)
                                                    {
                                                        //FA-L02-00-0289
                                                        //FishingActivity/SpecifiedFACatch/TypeCode
                                                        //Must have at least one occurrence with TypeCode ONBOARD if the activity is an area exit declaration or notification
                                                        var aexitonboard = sfa.SpecifiedFACatch.First(x => x.TypeCode.Value == "ONBOARD");
                                                        if (aexitonboard == null)
                                                        {
                                                            //FA-L02-00-0289 - error
                                                        }
                                                    }
                                                }

                                                //FA-L02-00-0252
                                                //FishingActivity/SpecifiedFACatch/TypeCode
                                                //Must have at least one occurrence with TypeCode ONBOARD if the activity is an area entry declaration or notification.
                                                if (IsFAAEnOrNot)
                                                {
                                                    if (!IsFACatchTCodeOnboard)
                                                    {
                                                        //FA-L02-00-0252 - error
                                                    }
                                                }

                                                #endregion

                                                #region Specified FLAP document
                                                if (sfa.SpecifiedFLAPDocument != null)
                                                {
                                                    foreach (var sfaflap in sfa.SpecifiedFLAPDocument)
                                                    {
                                                        //FA-L00-00-0646
                                                        //FLAPDocument/ID
                                                        //Check presence. Must be present.
                                                        if (sfaflap.ID != null)
                                                        {
                                                            //FA-L01-00-0647
                                                            //FLAPDocument/ID
                                                            //Check attribute schemeID. Must be a valid value from code list FLAP_ID_TYPE
                                                            //TODO" check db

                                                            //FA-L01-00-0648
                                                            //FLAPDocument/ID
                                                            //Check ID value. Must be according to the format defined for the schemeID as specified in list FLAP_ID_TYPE.
                                                            //TODO: check db
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0646 - er
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FA-L00-00-0050, FA-L01-00-0051, FA-L01-00-0052, FA-L01-00-0636, FA-L02-00-0053, FA-L01-00-0054, FA-L03-00-0630
                                    //VesselTransportMeans/ID
                                    //Check presence. At least one identifier must be present.
                                    if (fardoc.SpecifiedVesselTransportMeans != null)
                                    {

                                        //FA-L03-00-0630
                                        //"SpecifiedVesselTransportMeans/ID,
                                        //SpecifiedVesselTransportMeans / RegistrationVesselCountry / ID"

                                        //"SpecifiedVesselTransportMeans/ID with schemeID=CFR 
                                        //(and value = CFR nb) must be present if SpecifiedVesselTransportMeans / RegistrationVesselCountry / ID is on the code list MEMBER_STATE."
                                        bool IsRCountryMState = false;
                                        if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry != null)
                                        {
                                            if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID != null)
                                            {
                                                if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID == "MEMBER_STATE")
                                                {
                                                    IsRCountryMState = true;
                                                }
                                            }
                                        }

                                        bool isvtm_id = false;
                                        bool isvtm_schemeids = false;
                                        bool isvtm_schemeIRCS = false;
                                        bool isvtm_schemeEM = false;
                                        bool isvtm_schemeCFR = false;
                                        foreach (var ids in fardoc.SpecifiedVesselTransportMeans.ID)
                                        {
                                            if (!string.IsNullOrEmpty(ids.Value))
                                            {
                                                isvtm_id = true;
                                            }

                                            // 2. FA-L01-00-0051
                                            //VesselTransportMeans/ID
                                            // "Check schemeID. SchemeIDs must be present in 
                                            // the list FLUX_VESSEL_ID_TYPE"
                                            //TODO: check in DB FLUX_VESSEL_ID_TYPE == ids.schemeID

                                            // 3. FA-L01-00-0052
                                            //VesselTransportMeans/ID
                                            //Check Format. Must be according to the specified schemeID

                                            // 4. FA-L01-00-0636
                                            //VesselTransportMeans/ID
                                            // "Check value of ID. If schemeID=UVI, the checkbit 
                                            //  in the ID value must be valid."
                                            //Specifications of the IMO (UVI) number and calculation of the checkbit are described in annex 14.5.
                                            string vtm_scheme = ids.schemeID;
                                            if (!string.IsNullOrEmpty(vtm_scheme))
                                            {
                                                if (vtm_scheme == "UVI")
                                                {
                                                    // Calculate
                                                }

                                                // 5. FA-L02-00-0053
                                                //VesselTransportMeans/ID
                                                //"If schemeID=IRCS, the schemeID=EXT_MARK must be present as well.
                                                // If schemeID = EXT_MARK, the schemeID = IRCS must also be present."
                                                if (vtm_scheme == "IRCS")
                                                {
                                                    isvtm_schemeIRCS = true;
                                                }
                                                if (vtm_scheme == "EXT_MARK")
                                                {
                                                    isvtm_schemeEM = true;
                                                }
                                                if (vtm_scheme == "CFR")
                                                {
                                                    isvtm_schemeCFR = true;
                                                }
                                            }
                                        }

                                        if (!isvtm_id)
                                        {
                                            // FA-L00-00-0050 - error
                                        }

                                        // 6. FA-L01-00-0054
                                        //VesselTransportMeans/ID
                                        // At least CFR or IRCS must be present
                                        if (isvtm_schemeCFR)
                                        {
                                        }
                                        else
                                        {
                                            if (isvtm_schemeIRCS && isvtm_schemeEM)
                                            {
                                            }
                                            else
                                            {
                                                // FA-L02-00-0053 - error
                                                // FA-L01-00-0054 - error
                                            }
                                        }

                                        if (IsRCountryMState && isvtm_schemeCFR)
                                        {
                                        }
                                        else
                                        {
                                            // FA-L03-00-0630 - error
                                        }
                                    }

                                    #endregion

                                    #region 
                                    #endregion
                                }
                            }
                            #endregion
                            break;

                        case "FA-L03-00-0062":
                        case "FA-L03-00-0064":
                        case "FA-L03-00-0065":
                        case "FA-L03-00-0637":
                        case "FA-L03-00-0638":
                            //TECHNOLOGICA
                            break;
                        case "FA-L00-00-0350":
                            if (IsFAQuaery)
                            {
                                if (faquery.FAQuery != null)
                                {
                                    //FA-L00-00-0350
                                    //FAQuery/TypeCode
                                    //Check presence. Must be present.
                                    if (faquery.FAQuery.TypeCode != null)
                                    {
                                        if (faquery.FAQuery.TypeCode.Value == "")
                                        {
                                            //FA-L00-00-0350 - error
                                        }
                                        else
                                        {
                                            //FA-L02-00-0361
                                            //FAQuery/SpecifiedDelimitedPeriod, FAQuery/TypeCode
                                            //Check presence. Must be present if FAQuery/TypeCode is VESSEL
                                            if (faquery.FAQuery.TypeCode.Value == "VESSEL")
                                            {
                                                if (faquery.FAQuery.SpecifiedDelimitedPeriod != null)
                                                {
                                                    //FA-L02-00-0361 - error
                                                }
                                            }
                                        }
                                        //FA-L01-00-0351
                                        //FAQuery/TypeCode
                                        //Check attribute listID. Must be FA_QUERY_TYPE
                                        if (faquery.FAQuery.TypeCode.listID != "FA_QUERY_TYPE")
                                        {
                                            //FA-L01-00-0351 - error
                                        }
                                        //FA-L01-00-0352
                                        //FAQuery/TypeCode
                                        //Check code. Must be existing in the list specified in attribute listID
                                        string fqtcodelistid = faquery.FAQuery.TypeCode.listID;
                                        // TODO: check db 
                                    }
                                    else
                                    {
                                        //FA-L00-00-0350 - error
                                    }

                                    //FA-L00-00-0353
                                    //FAQuery/ID
                                    //Check attribute schemeID. Must be UUID
                                    if (faquery.FAQuery.ID != null)
                                    {
                                        if (faquery.FAQuery.ID.schemeID != "UUID")
                                        {
                                            //FA-L00-00-0353 - error
                                        }
                                        //FA-L01-00-0354
                                        //FAQuery/ID
                                        //Identifier must comply to the schemeID rules.
                                        //TODO: how to check

                                        //FA-L03-00-0650
                                        //FAQuery/ID
                                        //The identification must be unique and not already exist
                                        //TODO: check db for uuid
                                    }
                                    else
                                    {
                                        //FA-L00-00-0353 - error
                                    }

                                    //FA-L00-00-0355
                                    //FAQuery/SubmittedDateTime
                                    //Check presence. Must be present.
                                    if (faquery.FAQuery.SubmittedDateTime != null)
                                    {
                                        //FA-L01-00-0356
                                        //FAQuery/SubmittedDateTime
                                        //Check Format. Must be according to the definition provided in 7.1(2)
                                        //TODO: check format the model has chenged from string to datetime

                                        //FA-L03-00-0357
                                        //FAQuery/SubmittedDateTime
                                        //Date must be in the past.
                                        DateTime dt = DateTime.UtcNow;
                                        if (dt <= faquery.FAQuery.SubmittedDateTime.Item)
                                        {
                                            //FA-L03-00-0357 - warning
                                        }
                                    }
                                    else
                                    {
                                        //FA-L00-00-0355 - error
                                    }
                                    if (faquery.FAQuery.SubmitterFLUXParty != null)
                                    {

                                    }
                                    else
                                    {
                                        //FA-L00-00-0358
                                        //FAQuery/SubmitterFLUXParty/ID
                                        //Check presence. Must be present

                                        //FA-L00-00-0358
                                        if (faquery.FAQuery.SubmitterFLUXParty.ID != null)
                                        {
                                            //FA-L01-00-0359
                                            //FAQuery/SubmitterFLUXParty/ID
                                            //Check attribute schemeID. Must be FLUX_GP_PARTY
                                            var fqids = faquery.FAQuery.SubmitterFLUXParty.ID.First(z => z.schemeID == "FLUX_GP_PARTY");
                                            if (fqids == null)
                                            {
                                                //FA-L01-00-0359 - error
                                            }
                                            else
                                            {
                                                //FA-L03-00-0360
                                                //FAQuery/SubmitterFLUXParty/ID
                                                //Check if SubmitterFLUXParty / ID is consistent with FLUX TL envelope values.
                                                //The party sending the message must be the same as the one from the FR value
                                                //of the FLUX TL envelope. Only the part before the first colon is to be
                                                //considered: Eg. ABC:something => only ABC refres to the party for
                                                //the purpose of this rule
                                                //TODO: ???

                                                //FA-L02-00-0651
                                                //FAQuery/SubmitterFLUXParty/ID
                                                //Check value of ID. Must be existing on the list specified in the schemeID.
                                                //TODO: ???
                                            }


                                        }
                                        else
                                        {
                                            //FA-L00-00-0358 - error
                                        }
                                    }

                                    if (faquery.FAQuery.SpecifiedDelimitedPeriod != null)
                                    {
                                        //FA-L00-00-0362
                                        //FAQuery/SpecifiedDelimitedPeriod/StartDateTime
                                        //Check presence. Must be present if SpecifiedDelimitedPeriod is present.
                                        if (faquery.FAQuery.SpecifiedDelimitedPeriod.StartDateTime == null)
                                        {
                                            //FA-L00-00-0362 - error
                                        }
                                        else
                                        {
                                            // FA-L01-00-0363
                                            //FAQuery/SpecifiedDelimitedPeriod/StartDateTime
                                            //Check Format. Must be according to the definition provided in 7.1(2)
                                            //TODO: check format
                                        }
                                        //FA-L00-00-0364
                                        //FAQuery/SpecifiedDelimitedPeriod/EndDateTime
                                        //Check presence. Must be present if SpecifiedDelimitedPeriod is present.
                                        if (faquery.FAQuery.SpecifiedDelimitedPeriod.EndDateTime == null)
                                        {
                                            //FA-L00-00-0364 - error
                                        }
                                        else
                                        {
                                            //FA-L01-00-0365
                                            //FAQuery/SpecifiedDelimitedPeriod/EndDateTime
                                            //Check Format. Must be according to the definition provided in 7.1(2)
                                            //TODO: check format
                                        }
                                    }
                                    if (faquery.FAQuery.SimpleFAQueryParameter != null)
                                    {
                                        //FA-L02-00-0366
                                        //FAQuery/SimpleFAQueryParameter
                                        //Check presence. 2 occurrences must be present.
                                        if (faquery.FAQuery.SimpleFAQueryParameter.Length < 2)
                                        {
                                            //FA-L02-00-0366 - error
                                        }

                                        //FA-L01-00-0367
                                        //FAQuery/SimpleFAQueryParameter/TypeCode
                                        //Exactly one occurrence must be value CONSOLIDATED.
                                        var spartc = faquery.FAQuery.SimpleFAQueryParameter.First(x => x.TypeCode.Value == "CONSOLIDATED");
                                        if (spartc == null)
                                        {
                                            //FA-L01-00-0367 - error
                                        }
                                        else
                                        {
                                            //FA-L02-00-0378
                                            //FAQueryParameter/ValueCode, FAQueryParameter/TypeCode
                                            //Check presence. Must be present if TypeCode is CONSOLIDATED
                                            if (spartc.ValueID != null)
                                            {
                                                //FA-L01-00-0379
                                                //FAQueryParameter/ValueCode
                                                //Check value. Must be Y or N.
                                                if (spartc.ValueID.Value == "Y" || spartc.ValueID.Value == "N")
                                                {
                                                }
                                                else
                                                {
                                                    //FA-L01-00-0379 - error
                                                }
                                                //FA-L02-00-0652
                                                //FAQueryParameter/ValueCode, FAQueryParameter/TypeCode
                                                //Check attribute listID. Must be BOOLEAN_TYPE if TypeCode is CONSOLIDATED
                                                if (spartc.ValueID.schemeID != "BOOLEAN_TYPE")
                                                {
                                                    //FA-L02-00-0652 - error
                                                }
                                            }
                                            else
                                            {
                                                //FA-L02-00-0378 - error
                                            }
                                        }

                                        //FA-L00-00-0375
                                        //FAQuery/SimpleFAQueryParameter/TypeCode
                                        //At most one occurrence with TypeCode VESSELID
                                        var spartc1 = faquery.FAQuery.SimpleFAQueryParameter.First(x => x.TypeCode.Value == "VESSELID");
                                        if (spartc1 == null)
                                        {
                                            //FA-L00-00-0375- error
                                        }
                                        else
                                        {
                                            //FA-L02-00-0372
                                            //FAQueryParameter/ValueID, FAQueryParameter/TypeCode
                                            //Check presence. Must be present if Type is VESSELID or TRIPID
                                            if (spartc1.ValueID == null)
                                            {
                                                //FA-L02-00-0372 - error
                                                //FA-L02-00-0373 - error
                                            }
                                            else
                                            {
                                                //FA-L02-00-0373
                                                //FAQueryParameter/ValueID, FAQueryParameter/TypeCode
                                                //Check schemeID. Must be value from the list FLUX_VESSEL_ID_TYPE if TypeCode is VESSELID
                                                //TODO: check in db FLUX_VESSEL_ID_TYPE = spartc1.ValueID.Value

                                                //FA-L02-00-0374
                                                //FAQueryParameter/ValueID, FAQueryParameter/TypeCode
                                                //Check schemeID. Must be value CFR or IRCS29 if Type is VESSELID
                                                if (spartc1.ValueID.schemeID == "CFR" || spartc1.ValueID.schemeID == "IRCS")
                                                {
                                                }
                                                else
                                                {
                                                    //FA-L02-00-0374 - error
                                                }

                                            }

                                        }
                                        var spartctripid = faquery.FAQuery.SimpleFAQueryParameter.First(x => x.TypeCode.Value == "TRIPID");
                                        if (spartctripid != null)
                                        {
                                            if (spartctripid.ValueID == null)
                                            {
                                                //FA-L02-00-0372 - error
                                            }
                                        }


                                        //FA-L02-00-0371
                                        //FAQueryParameter/TypeCode, FAQuery/TypeCode
                                        //Must be value VESSELID if FAQuery/TypeCode is VESSEL or must be value TRIPID if FAQuery/TypeCode is TRIP
                                        if (faquery.FAQuery.TypeCode.Value == "VESSEL")
                                        {
                                            if (spartc1 == null)
                                            {
                                                //FA-L02-00-0371 - error
                                            }
                                        }
                                        if (faquery.FAQuery.TypeCode.Value == "TRIP")
                                        {
                                            var spartctrip = faquery.FAQuery.SimpleFAQueryParameter.First(x => x.TypeCode.Value == "TRIPID");
                                            if (spartctrip == null)
                                            {
                                                //FA-L02-00-0371 - error
                                            }
                                            else
                                            {
                                                //FA-L02-00-0376
                                                //FAQueryParameter/ValueID, FAQueryParameter/TypeCode
                                                //Check schemeID. Must be value EU_TRIP_ID if TypeCode is TRIPID
                                                if (spartctrip.ValueID != null)
                                                {
                                                    if (spartctrip.ValueID.schemeID != "EU_TRIP_ID")
                                                    {
                                                        //FA-L02-00-0376 - error
                                                    }
                                                }
                                            }
                                        }
                                        //FA-L01-00-0369
                                        //FAQueryParameter/TypeCode
                                        //Check attribute listID. Must be FA_QUERY_PARAMETER
                                        var spartc2 = faquery.FAQuery.SimpleFAQueryParameter.First(x => x.TypeCode.listID != "FA_QUERY_PARAMETER");
                                        if (spartc2 != null)
                                        {

                                        }
                                        //FA-L01-00-0370
                                        //FAQueryParameter/TypeCode
                                        //Check code. Must be existing in the list specified in attribute listID
                                        //TODO: check in db


                                    }

                                }
                            }
                            break;
                        case "FA-L00-00-0380":
                            if (IsFAResp)
                            {
                                if (FAresp.FLUXResponseDocument != null)
                                {
                                    if (FAresp.FLUXResponseDocument.ID != null)
                                    {
                                        //FA-L00-00-0380
                                        //FLUXResponseDocument/ID
                                        //Check attribute schemeID. Must be UUID
                                        var respdocid = FAresp.FLUXResponseDocument.ID.First(x => x.schemeID == "UUID");
                                        if (respdocid == null)
                                        {
                                            //FA-L00-00-0380 - error
                                        }
                                        else
                                        {
                                            //FA-L01-00-0381
                                            //FLUXResponseDocument/ID
                                            //Check Format. Must be according to the specified schemeID.
                                            //Must be according to RFC4122 format for UUID. Check is case insensitive.
                                            //TODO: check RFC4122
                                            // bool isValid = Guid.TryParse(inputString, out guidOutput)

                                            //FA-L03-00-0382
                                            //FLUXResponseDocument/ID
                                            //The identification must be unique and not already exist.
                                            //TODO: check db if there already a UUID
                                        }
                                    }
                                    if (FAresp.FLUXResponseDocument.ReferencedID != null)
                                    {
                                        //FA-L00-00-0383
                                        //FLUXResponseDocument/ReferencedID
                                        //Check attribute schemeID. Must be a valid value from code list FLUX_GP_MSG_ID.
                                        //TODO: check value if in FLUX_GP_MSG_ID.
                                        //resp.FLUXResponseDocument.ReferencedID.schemeID 

                                        //FA-L01-00-0384
                                        //FLUXResponseDocument/ReferencedID
                                        //Check Format. Must be according to the specified schemeID.
                                        //string title = "STRING";
                                        //bool contains = title.IndexOf("string", StringComparison.OrdinalIgnoreCase) >= 0;

                                        //FA-L03-00-0385
                                        //FLUXResponseDocument/ReferencedID
                                        //The identification must exist for a FLUXFAReportMessage or for a FLUXFAQuery message
                                        //TODO: check in database for reverenced FAReport or FAQuery

                                    }
                                    //FA-L00-00-0386
                                    //FLUXResponseDocument/ResponseCode
                                    //Check presence. Must be present
                                    if (FAresp.FLUXResponseDocument.ResponseCode == null)
                                    {
                                        //FA-L00-00-0386 - error
                                    }
                                    else
                                    {
                                        //FA-L02-00-0387
                                        //FLUXResponseDocument/ResponseCode
                                        //Check attribute listID. Must be FLUX_GP_RESPONSE
                                        if (FAresp.FLUXResponseDocument.ResponseCode.listID != "FLUX_GP_RESPONSE")
                                        {
                                            //FA-L02-00-0387 - error
                                        }

                                        //FA-L02-00-0388
                                        //FLUXResponseDocument/ResponseCode
                                        //Check value. Code must be value of the specified code list in listID
                                        //TODO: Check DB listid
                                    }
                                    //FA-L00-00-0389
                                    //FLUXResponseDocument/CreationDateTime
                                    //Check presence. Must be present.
                                    if (FAresp.FLUXResponseDocument.CreationDateTime == null)
                                    {
                                        //FA-L00-00-0389 - error
                                    }
                                    else
                                    {
                                        
                                    }
                                }
                            }
                            break;
                    }
                }
            }


            bool VesselDomainDebug = true;
            if (VesselDomainDebug)
            {
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
                FLUXReportVesselInformationType Vesselreport = serializer.Deserialize(stringReader) as FLUXReportVesselInformationType;
 
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

 
                #endregion

                foreach (var rule in VesselBrDef)
                {
                    switch (rule.Code) 
                    {
                        case "VESSEL-L00-00-0001":
                           
                            if (Vesselreport != null)
                            {
                                if (Vesselreport.FLUXReportDocument != null)
                                {
                                    //VESSEL-L00-00-0001
                                    //FLUX_Report Document/Identification
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.ID != null)
                                    {
                                        //VESSEL-L00-00-0002
                                        //FLUX_Report Document/Identification
                                        //The identifier must be a valid UUID format
                                        //TODO: check UUID is valid

                                        //VESSEL-L00-00-0003
                                        //FLUX_Report Document/Identification
                                        //The UUID is unique (he does not reference a report already received)
                                        //TODO: Check db
                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0001 - rejected
                                    }

                                    //VESSEL-L00-00-0009
                                    //FLUX_Report Document/Type
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.TypeCode != null)
                                    {
                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0009 - rejected
                                    }


                                    if (Vesselreport.FLUXReportDocument.CreationDateTime.Item <= DateTime.UtcNow)
                                    {
                                        //VESSEL-L00-00-0093 - rejected
                                    }
                                }
                            }


                            break;
                    }
                }
            }





            #region Commented XML XPATH Methods

            ////XmlNamespaceManager namespaces = new XmlNamespaceManager(xmlDoc.NameTable);
            ////namespaces.AddNamespace("ns", "urn:un:unece:uncefact:data:standard:FLUXFAReportMessage:3");
            ////namespaces.AddNamespace("ns1", "urn:un:unece:uncefact:data:standard:ReusableAggregateBusinessInformationEntity:20"); 

            ////foreach (var rule in FaBrDef)
            ////{
            ////    List<string> multiplefields = rule.Field.Split(',').ToList();

            ////    foreach (var multypart in multiplefields)
            ////    {
            ////        List<string> fieldpart = multypart.Trim().Split('/').ToList();
            ////        string cpath = "";
            ////        foreach (var sfield in fieldpart)
            ////        {
            ////            // /*[local-name()='FLUXFAReportMessage']/*[local-name()='FLUXReportDocument']/*[local-name()='ID']
            ////            cpath += "/*[local-name()='" + sfield + "']";
            ////        }
            ////        if (fieldpart != null)
            ////        {
            ////            //XmlNode idNode = xmlDoc.SelectSingleNode("/*[local-name()='FLUXFAReportMessage']/*[local-name()='FLUXReportDocument']/*[local-name()='ID']");
            ////            XmlNode idNode = xmlDoc.SelectSingleNode(cpath);
            ////            if (idNode != null)
            ////            {
            ////                string msgID = idNode.InnerText;
            ////            }
            ////            else
            ////            {
            ////                if (cpath.IndexOf("FLUXFAReportMessage") < 0 && cpath.IndexOf("FLUXFAQueryMessage") < 0 && cpath.IndexOf("FLUXResponseMessage") < 0)
            ////                {
            ////                    if (cpath.IndexOf("FAReportDocument") < 0)
            ////                    {
            ////                        if (cpath.IndexOf("VesselTransportMeans") < 0)
            ////                        {
            ////                            if (cpath.IndexOf("FishingActivity") > 0)
            ////                            {
            ////                                cpath = cpath.Replace("FishingActivity", "SpecifiedFishingActivity");
            ////                            }

            ////                            cpath = "/*[local-name()='FLUXFAReportMessage']/*[local-name()='FAReportDocument']" + cpath;
            ////                            cpath = cpath.Replace("VesselTransportMeans", "SpecifiedVesselTransportMeans");
            ////                            XmlNode idNode2 = xmlDoc.SelectSingleNode(cpath);
            ////                            if (idNode2 != null)
            ////                            {
            ////                                string msgID1 = idNode2.InnerText;
            ////                            }
            ////                            else
            ////                            {

            ////                            }
            ////                        }
            ////                        else
            ////                        {
            ////                            if (cpath.IndexOf("RelatedVesselTransportMens") < 0)
            ////                            {
            ////                                cpath = cpath.Replace("VesselTransportMeans", "SpecifiedVesselTransportMeans");
            ////                            }
            ////                            cpath = "/*[local-name()='FLUXFAReportMessage']/*[local-name()='FAReportDocument']" + cpath;
            ////                            XmlNode idNode3 = xmlDoc.SelectSingleNode(cpath);
            ////                            if (idNode3 != null)
            ////                            {
            ////                                string msgID3 = idNode3.InnerText;
            ////                            }
            ////                            else
            ////                            {

            ////                            }
            ////                        }
            ////                    }
            ////                    else
            ////                    {
            ////                        cpath = "/*[local-name()='FLUXFAReportMessage']" + cpath;
            ////                        XmlNode idNode1 = xmlDoc.SelectSingleNode(cpath);
            ////                        if (idNode1 != null)
            ////                        {
            ////                            string msgID1 = idNode1.InnerText;
            ////                        }
            ////                        else
            ////                        {

            ////                        }

            ////                        ////  ne raboti za relatedfluxreportdocument i negovite poleta, za l20 -> ima prazni simvoli
            ////                        /////*[local-name()='FLUXFAReportMessage']/*[local-name()=' FAReportDocument']/*[local-name()='RelatedFLUXReportDocument']/*[local-name()='PurposeCode']
            ////                        //XmlNode idNode2 = xmlDoc.SelectSingleNode("/*[local-name()='RelatedFLUXReportDocument']/*[local-name()='PurposeCode']");
            ////                        //if (idNode2 != null)
            ////                        //{
            ////                        //    string msgID2 = idNode2.InnerText;
            ////                        //}
            ////                        //else
            ////                        //{
            ////                        //}
            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }

            ////}

            ////XmlNode dataAttribute = xmlDoc.SelectSingleNode("/FLUXFAReportMessage/FLUXReportDocument/ID");


            //////FLUXFAReportMessage/FLUXReportDocument/ID
            //////XmlNodeList nodeList = xmlDoc.GetElementsByTagName("FLUXFAReportMessage");
            ////XmlNodeList nodeList = xmlDoc.GetElementsByTagName("FLUXFAReportMessage/FLUXReportDocument/ID");
            ////foreach (XmlNode node in nodeList)
            ////{
            ////    XmlNodeList chnodeList = node.ChildNodes;
            ////    foreach (XmlNode chnode in chnodeList)
            ////    {

            ////    }
            ////}
            #endregion

            #region Commented xml sample
            //string xmljson = "{";
            //xmljson += "\"ENV\": {";
            //xmljson += "     \"MSG\": {";
            //xmljson += "        \"FLUXFAQueryMessage\": {";
            //xmljson += "           \"FAQuery\": {";
            //xmljson += "                \"ID\": \"91a9f5ff-6187-411e-a5af-6cc18e46f07e\",";
            //xmljson += "                    \"SubmittedDateTime\": {";
            //xmljson += "                         \"DateTime\": \"2023-07-17T08:55:49.782Z\"";
            //xmljson += "                     },";
            //xmljson += "                \"TypeCode\": \"VESSEL\",";
            //xmljson += "                \"SpecifiedDelimitedPeriod\": {";
            //xmljson += "                    \"StartDateTime\": {";
            //xmljson += "                        \"DateTime\": \"2022-10-01T00:00:00.000Z\"";
            //xmljson += "                    },";
            //xmljson += "                     \"EndDateTime\": {";
            //xmljson += "                        \"DateTime\": \"2022-10-31T23:59:59.000Z\"";
            //xmljson += "                    }";
            //xmljson += "                },";
            //xmljson += "                \"SubmitterFLUXParty\": {";
            //xmljson += "                    \"ID\": \"SRC\"";
            //xmljson += "                 },";
            //xmljson += "                 \"SimpleFAQueryParameter\": [";
            //xmljson += "                 {";
            //xmljson += "                    \"TypeCode\": \"VESSELID\",";
            //xmljson += "                    \"ValueID\": \"IRCS1\"";
            //xmljson += "                 },";
            //xmljson += "                 {";
            //xmljson += "                    \"TypeCode\": \"CONSOLIDATED\",";
            //xmljson += "                    \"ValueCode\": \"Y\"";
            //xmljson += "                 }";
            //xmljson += "                 ]";
            //xmljson += "          }"; // faquery
            //xmljson += "        }";   // fluxfa message
            //xmljson += "     }"; //msg
            //xmljson += "  }"; // enve
            //xmljson += " }"; //json


            //string farep = "{ ";
            //farep += ""\"ENV\": { ";
            //farep += "    \"MSG\": {";
            //farep += "        \"FLUXFAReportMessage\": {";
            //farep += "            "FLUXReportDocument": {
            //farep += "                "ID": "2e0ac7ad-f037-4b5d-962a-008a7396c708",
            //farep += " "CreationDateTime": {
            //farep += "                    "DateTime": "2023-07-17T09:34:09.975Z"
            //farep += " },
            //farep += " "PurposeCode": 9,
            //farep += " "Purpose": "FLUX-FA-EU-711302",
            //farep += " "OwnerFLUXParty": {
            //farep += "                    "ID": "SRC"
            //farep += " }
            //farep += "            },
            //farep += " "FAReportDocument": {
            //farep += "                "TypeCode": "DECLARATION",
            //farep += " "AcceptanceDateTime": {
            //farep += "                    "DateTime": "2023-07-17T04:34:09.975Z"
            //farep += " },
            //farep += " "RelatedFLUXReportDocument": {
            //farep += "                    "ID": "f0ba50da-040a-4c89-aee5-1db3d0de321c",
            //farep += " "CreationDateTime": {
            //farep += "                        "DateTime": "2023-07-17T06:34:09.975Z"
            //farep += " },
            //farep += " "PurposeCode": 9,
            //farep += " "OwnerFLUXParty": {
            //farep += "                        "ID": "SRC"
            //farep += " }
            //farep += "                },
            //farep += " "SpecifiedFishingActivity": {
            //farep += "                    "TypeCode": "TRANSHIPMENT",
            //farep += " "SpecifiedFACatch": [
            //farep += "  {
            //farep += "                        "SpeciesCode": "COD",
            //farep += "    "WeightMeasure": 500,
            //farep += "    "TypeCode": "LOADED",
            //farep += "    "SpecifiedSizeDistribution": {
            //farep += "                            "ClassCode": "LSC"
            //farep += "    },
            //farep += "    "AppliedAAPProcess": {
            //farep += "                            "TypeCode": [
            //farep += "                              "GUT",
            //farep += "        "FRO"
            //farep += "      ],
            //farep += "      "ConversionFactorNumeric": 1.1,
            //farep += "      "ResultAAPProduct": {
            //farep += "                                "WeightMeasure": 525.8,
            //farep += "        "PackagingUnitQuantity": 50,
            //farep += "        "PackagingTypeCode": "BOX",
            //farep += "        "PackagingUnitAverageWeightMeasure": 10.05
            //farep += "      }
            //farep += "                        },
            //farep += "    "SpecifiedFLUXLocation": [
            //farep += "      {
            //farep += "                            "TypeCode": "AREA",
            //farep += "        "ID": "ESP",
            //farep += "        "RegionalFisheriesManagementOrganizationCode": "NEAFC"
            //farep += "      },
            //farep += "      {
            //farep += "                            "TypeCode": "AREA",
            //farep += "        "ID": "27.9.b.2",
            //farep += "        "RegionalFisheriesManagementOrganizationCode": "NEAFC"
            //farep += "      }
            //farep += "    ]
            //farep += "  },
            //farep += "  {
            //farep += "                        "SpeciesCode": "COD",
            //farep += "    "WeightMeasure": 2000,
            //farep += "    "TypeCode": "ONBOARD",
            //farep += "    "SpecifiedSizeDistribution": {
            //farep += "                            "ClassCode": "LSC"
            //farep += "    },
            //farep += "    "SpecifiedFLUXLocation": {
            //farep += "                            "TypeCode": "AREA",
            //farep += "      "ID": "27.9.b.2"
            //farep += "    }
            //farep += "                    }
            //farep += "],
            //farep += ""RelatedFLUXLocation": [
            //farep += "  {
            //farep += "                         "TypeCode": "AREA",
            //farep += "    "ID": "NEAFC_RA"
            //farep += "  },
            //farep += "  {
            //farep += "                        "TypeCode": "POSITION",
            //farep += "    "SpecifiedPhysicalFLUXGeographicalCoordinate": {
            //farep += "                            "LongitudeMeasure": -14.16,
            //farep += "      "LatitudeMeasure": 46.78
            //farep += "    }
            //farep += "                    }
            //farep += "],
            //farep += ""SpecifiedDelimitedPeriod": {
            //farep += "                        "StartDateTime": {
            //farep += "                            "DateTime": "2023-07-15T10:34:09.975Z"
            //farep += "                        },
            //farep += "  "EndDateTime": {
            //farep += "                            "DateTime": "2023-07-16T10:34:09.975Z"
            //farep += "  }
            //farep += "                    },
            //farep += " "SpecifiedFishingTrip": {
            //farep += "                        "ID": "SRC-TRP-TTT20230717123409975"
            //farep += " },
            //farep += " "RelatedVesselTransportMeans": {
            //farep += "                        "ID": "CYP123456789",
            //farep += "  "RoleCode": "DONOR",
            //farep += "  "RegistrationVesselCountry": {
            //farep += "                            "ID": "CYP"
            //farep += "  },
            //farep += "  "SpecifiedContactParty": {
            //farep += "                            "RoleCode": "MASTER",
            //farep += "    "SpecifiedStructuredAddress": {
            //farep += "                                "StreetName": "ABC",
            //farep += "      "CityName": "CABOURG",
            //farep += "      "CountryID": "ESP",
            //farep += "      "PlotIdentification": 17,
            //farep += "      "PostalArea": 14390
            //farep += "    },
            //farep += "    "SpecifiedContactPerson": {
            //farep += "                                "GivenName": "John",
            //farep += "      "FamilyName": "Doe",
            //farep += "      "Alias": "Captain Jack"
            //farep += "    }
            //farep += "                        }
            //farep += "                    }
            //farep += "                },
            //farep += " "SpecifiedVesselTransportMeans": {
            //farep += "                    "ID": "SVN123456789",
            //farep += " "RoleCode": "RECEIVER",
            //farep += " "RegistrationVesselCountry": {
            //farep += "                        "ID": "SVN"
            //farep += " },
            //farep += " "SpecifiedContactParty": {
            //farep += "                        "RoleCode": "MASTER",
            //farep += "  "SpecifiedStructuredAddress": {
            //farep += "                            "StreetName": "XYZ",
            //farep += "    "CityName": "CABOURG",
            //farep += "    "CountryID": "ESP",
            //farep += "    "PlotIdentification": 789,
            //farep += "    "PostalArea": 14390
            //farep += "  },
            //farep += "  "SpecifiedContactPerson": {
            //farep += "                            "GivenName": "Julia",
            //farep += "    "FamilyName": "X",
            //farep += "    "Alias": "Master Julia"
            //farep += "  }
            //farep += "                    }
            //farep += "                }
            //farep += "            }
            //farep += "        }
            //farep += "    }
            //farep += "}
            //farep += "}"

            #endregion
        }

        private static void Settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            var error = e.Severity;
            var msg = e.Message;
        }
    }
}
