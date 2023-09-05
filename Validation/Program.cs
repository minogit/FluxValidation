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
using ScortelApi.Tools;
using System.Text.RegularExpressions;

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
            bool FADomainDebug = true;
            List<string> SysList = new List<string>();
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

                FLUXResponseMessageType GenResponse = new FLUXResponseMessageType();


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


                faquery = null;


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

                FAresp = null;

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
                        IsFARD = true;
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

                    GenResponse.FLUXResponseDocument = new FLUXResponseDocumentType();
                    GenResponse.FLUXResponseDocument.ID = new IDType[] {
                        new IDType(){ 
                            schemeID = "UUID",
                            Value = THelp.Gen_UUID()
                        }
                    };

                    var refid = FAreport.FLUXReportDocument.ID.FirstOrDefault(x => x.schemeID == "UUID");
                    string refidstr = "";
                    if (refid != null)
                    {
                        refidstr = refid.Value;
                    }

                    GenResponse.FLUXResponseDocument.ReferencedID = new IDType()
                    {
                        Value = refidstr
                    };
                    GenResponse.FLUXResponseDocument.CreationDateTime = new DateTimeType()
                    {
                        Item = DateTime.UtcNow
                    };
                    GenResponse.FLUXResponseDocument.RespondentFLUXParty = new FLUXPartyType()
                    {
                        ID = new IDType[] {
                            new IDType(){
                                schemeID = "FLUX_GP_PARTY",
                                Value = "BGR"
                            }
                        }
                    };
                    GenResponse.FLUXResponseDocument.RelatedValidationResultDocument = new ValidationResultDocumentType[] {
                        new ValidationResultDocumentType(){ 
                            ValidatorID = new IDType(){
                                 schemeID = "FLUX_GP_PARTY",
                                Value = "BGR"
                            },
                            CreationDateTime = new DateTimeType() {
                                Item = DateTime.UtcNow
                            }
                        }
                    };
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
                                                SysList.Add("FA-L01-00-0003");
                                            }

                                            //4. FA-L03-00-0004 The identification must be unique and not already exis
                                            // check database for receved id

                                        }
                                        else
                                        {
                                            // FA-L00-00-0001 - error
                                            // FA-L01-00-0003 - error
                                            SysList.Add("FA-L00-00-0001");
                                            SysList.Add("FA-L01-00-0003");
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
                                                SysList.Add("FA-L01-00-0002");
                                            }
                                        }
                                        else
                                        {
                                            // FA-L01-00-0002 - error
                                            SysList.Add("FA-L01-00-0002");
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
                                        SysList.Add("FA-L03-00-0007");
                                        //var crule = FaBrDef.FirstOrDefault(x => x.Code == "FA-L03-00-0007");
                                        //GenResponse.FLUXResponseDocument.RelatedValidationResultDocument[0].RelatedValidationQualityAnalysis = new ValidationQualityAnalysisType[]
                                        //{
                                        //    new ValidationQualityAnalysisType(){
                                        //        ID = new IDType(){ 
                                        //            schemeID = "FA_BR",
                                        //            Value = crule.Code
                                        //        },
                                        //        LevelCode = new CodeType(){ 
                                        //            listID = "FLUX_GP_VALIDATION_LEVEL",
                                        //            Value = "L03"
                                        //        },
                                        //        TypeCode = new CodeType(){ 
                                        //            listID = "FLUX_GP_VALIDATION_TYPE",
                                        //            Value = "WAR"
                                        //        },
                                        //        Result = new TextType[] {
                                        //            new TextType() {
                                        //                languageID="GBR",
                                        //                Value = crule.ENMessage
                                        //            }
                                        //        }
                                        //        //,
                                        //        //ReferencedItem = new TextType[] { 
                                        //        //    new TextType(){ 

                                        //        //    }
                                        //        //}
                                        //    }
                                        //};
                                        ////<LevelCode listID="FLUX_GP_VALIDATION_LEVEL">L03</LevelCode>
                                        ////<TypeCode listID="FLUX_GP_VALIDATION_TYPE">WAR</TypeCode>
                                        ////<Result languageID="GBR">Exactly one departure declaration must be present.</Result>
                                        ////<ID schemeID="FA_BR">FA-L03-00-0655</ID>
                                        ////<ReferencedItem languageID="XPATH">((//*[local-name()='FLUXFAReportMessage']//*[local-name()='FAReportDocument'])[1]//*[local-name()='SpecifiedFishingActivity'])[1]//*[local-name()='TypeCode']</ReferencedItem>
                                    }
                                }
                                else
                                {
                                    // FA-L00-00-0005 - error
                                    // FA-L01-00-0006 - error
                                    SysList.Add("FA-L00-00-0005");
                                    SysList.Add("FA-L01-00-0006");
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
                                    SysList.Add("FA-L00-00-0008");
                                }
                                //Check attribute listID.Must be FLUX_GP_PURPOSE
                                if (pcode.listID != "FLUX_GP_PURPOSE")
                                {
                                    //FA-L01-00-0009 - error
                                    SysList.Add("FA-L01-00-0009");
                                }
                                //Check code.Must be value 9(original data)
                                if (pcode.Value != "9")
                                {
                                    //FA-L01-00-0010 - error
                                    SysList.Add("FA-L01-00-0010");
                                }
                            }
                            else
                            {
                                //FA-L00-00-0008 - error
                                SysList.Add("FA-L00-00-0008");
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
                                        SysList.Add("FA-L00-00-0011");
                                    }
                                    else
                                    {
                                        //2. FA-L01-00-0012 Check Format. Must be according to the specified schemeID.
                                        if (fdocrefid.Value.ToLower() != fdocrefid.Value)
                                        {
                                            // FA-L01-00-0012 - error
                                            SysList.Add("FA-L01-00-0012");
                                        }

                                        //3. FA-L03-00-0013  - The identification must exist for a FLUXFAQueryMessage
                                        //TODO: check in DB if there is previous FLUXFAQueryMessage with such UUID
                                    }
                                }
                                else
                                {
                                    // no previous document
                                    // FA-L00-00-0011 - error
                                    SysList.Add("FA-L00-00-0011");
                                }

                            }
                            else
                            {
                                //// FA-L00-00-0011 - error
                                //SysList.Add("FA-L00-00-0011");
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
                                            SysList.Add("FA-L01-00-0015");
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0015 - error
                                        SysList.Add("FA-L01-00-0015");
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
                                        SysList.Add("FA-L01-00-0627");
                                    }
                                    // 4. FA-L03-00-0016 - The party sending the message must be the same as the one from the
                                    // FR value of the FLUX TL envelope. Only the part before the first colon is to be
                                    // considered: Eg. ABC:something => only ABC refres to the party for the purpose of this rule.
                                }
                            }
                            else
                            {
                                // FA-L00-00-0014 - error
                                SysList.Add("FA-L00-00-0014");
                            }
                            #endregion
                            break;
                        case "FA-L00-00-0017":
                            #region
                            if (!IsFARD)
                            {
                                // FA-L00-00-0017 - error
                                SysList.Add("FA-L00-00-0017");
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
                                            SysList.Add("FA-L01-00-0021");
                                        }
                                    }
                                    else
                                    {
                                        // FA-L00-00-0020 - error
                                        SysList.Add("FA-L00-00-0020");
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
                                            SysList.Add("FA-L01-00-0022");
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0022 - error
                                        SysList.Add("FA-L01-00-0022");
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
                                                SysList.Add("FA-L00-00-0023");
                                            }
                                            else
                                            {
                                                //2. FA-L01-00-0024  - Identifier must comply to the schemeID rules
                                                //???
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //    // FA-L00-00-0023 - error
                                    //}
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
                                            SysList.Add("FA-L03-00-0027");
                                        }
                                    }
                                    else
                                    {
                                        //FA-L00-00-0025 - error
                                        SysList.Add("FA-L00-00-0025");
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
                                                    SysList.Add("FA-L01-00-0029");
                                                }
                                            }

                                            //3. FA-L01-00-0030 - Check Format. Must be according to the specified schemeID.
                                            //???
                                        }
                                    }
                                    else
                                    {
                                        // FA-L01-00-0028 - error
                                        SysList.Add("FA-L01-00-0028");
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
                                                    SysList.Add("FA-L01-00-0034");
                                                }
                                            }
                                            else
                                            {
                                                // FA-L00-00-0033 - error 
                                                SysList.Add("FA-L00-00-0033");
                                            }
                                        }
                                        else
                                        {
                                            // FA-L00-00-0032 - error 
                                            SysList.Add("FA-L00-00-0032");
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
                                                        SysList.Add("FA-L03-00-0035");
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
                                                            SysList.Add("FA-L01-00-0036");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // FA-L03-00-0035 - error
                                                    SysList.Add("FA-L03-00-0035");
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region  "FA-L00-00-0039", "FA-L00-00-0040", "FA-L03-00-0041"
                                    //FAReportDocument/RelatedFLUXReportDocument/CreationDateTime
                                    if (fardoc.RelatedFLUXReportDocument != null)
                                    {
                                        //1. FA-L00-00-0039 - Check presence. Must be present.
                                        if (fardoc.RelatedFLUXReportDocument.CreationDateTime != null)
                                        {

                                            //2. FA-L00-00-0040 -  Check Format. Must be according to the definition provided in 7.1(2)

                                            //3. FA-L03-00-0041 - Date must be in the past.
                                            TimeSpan ts_crdar = DateTime.UtcNow - fardoc.RelatedFLUXReportDocument.CreationDateTime.Item;
                                            if (ts_crdar.TotalSeconds > 10 * 60)
                                            {
                                                // FA-L03-00-0041 warning 
                                                SysList.Add("FA-L03-00-0041");
                                            }
                                        }
                                        else
                                        {
                                            // FA-L00-00-0039 - error 
                                            SysList.Add("FA-L00-00-0039");
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
                                                SysList.Add("FA-L02-00-0042");
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
                                                        SysList.Add("FA-L00-00-0043");
                                                    }
                                                    else
                                                    {
                                                        //2. FA-L00-00-0044 - Check attribute schemeID. Must be FLUX_GP_PARTY
                                                        if (ids.schemeID != "FLUX_GP_PARTY")
                                                        {
                                                            // FA-L00-00-0044 - error
                                                            SysList.Add("FA-L00-00-0044");
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
                                                                SysList.Add("FA-L00-00-0045");
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
                                                SysList.Add("FA-L02-00-0042");
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
                                                SysList.Add("FA-L03-00-0634");
                                            }

                                            // FA-L00-00-0068
                                            // Check presence. Must be present if VesselTransportMeans/RoleCode 
                                            // is PAIR_FISHING_PARTNER
                                            bool IsSvtmRCodePartner = false;
                                            if (fardoc.SpecifiedVesselTransportMeans.RoleCode != null)
                                            {
                                                if (fardoc.SpecifiedVesselTransportMeans.RoleCode.Value == "PAIR_FISHING_PARTNER")
                                                {
                                                    IsSvtmRCodePartner = true;
                                                }
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
                                                        SysList.Add("FA-L00-00-0620");
                                                        if (IsSvtmRCodePartner)
                                                        {
                                                            // FA-L00-00-0068 - error
                                                            SysList.Add("FA-L00-00-0068");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (scp.RoleCode == null)
                                                        {
                                                            // FA-L00-00-0621 - error
                                                            SysList.Add("FA-L00-00-0621");
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
                                                                    SysList.Add("FA-L00-00-0069");
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
                                                                    if (scprcode.Value != "MASTER" && scprcode.Value != "AGENT")
                                                                    {
                                                                        // FA-L02-00-0071 - error
                                                                        SysList.Add("FA-L02-00-0071");
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
                                                                        SysList.Add("FA-L00-00-0072");
                                                                        SysList.Add("FA-L00-00-0074");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(scpcp.GivenName.Value))
                                                                        {
                                                                            // FA-L00-00-0072 - error
                                                                            // FA-L00-00-0076 - error
                                                                            SysList.Add("FA-L00-00-0072");
                                                                            SysList.Add("FA-L00-00-0076");
                                                                        }
                                                                    }
                                                                    if (scpcp.FamilyName == null)
                                                                    {
                                                                        //FA-L00-00-0074 - error
                                                                        // FA-L00-00-0076 - error
                                                                        SysList.Add("FA-L00-00-0074");
                                                                        SysList.Add("FA-L00-00-0076");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(scpcp.FamilyName.Value))
                                                                        {
                                                                            //FA-L00-00-0074 - error
                                                                            // FA-L00-00-0076 - error
                                                                            SysList.Add("FA-L00-00-0074");
                                                                            SysList.Add("FA-L00-00-0076");
                                                                        }
                                                                    }
                                                                    // FA-L01-00-0077 - error
                                                                    SysList.Add("FA-L01-00-0077");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (scpcp.GivenName == null)
                                                                {
                                                                    // FA-L00-00-0072 - error
                                                                    // FA-L00-00-0076 - error
                                                                    SysList.Add("FA-L00-00-0072");
                                                                    SysList.Add("FA-L00-00-0076");
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(scpcp.GivenName.Value))
                                                                    {
                                                                        // FA-L00-00-0072 - error
                                                                        // FA-L00-00-0076 - error
                                                                        SysList.Add("FA-L00-00-0072");
                                                                        SysList.Add("FA-L00-00-0076");
                                                                    }
                                                                }
                                                                if (scpcp.FamilyName == null)
                                                                {
                                                                    //FA-L00-00-0074 - error
                                                                    // FA-L00-00-0076 - error
                                                                    SysList.Add("FA-L00-00-0074");
                                                                    SysList.Add("FA-L00-00-0076");
                                                                }
                                                                else
                                                                {
                                                                    if (string.IsNullOrEmpty(scpcp.FamilyName.Value))
                                                                    {
                                                                        //FA-L00-00-0074 - error
                                                                        // FA-L00-00-0076 - error
                                                                        SysList.Add("FA-L00-00-0074");
                                                                        SysList.Add("FA-L00-00-0076");
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
                                                                    SysList.Add("FA-L01-00-0081");
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
                                                                    SysList.Add("FA-L01-00-0083");
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
                                                                    SysList.Add("FA-L01-00-0085");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                // FA-L00-00-0084 - warning
                                                                SysList.Add("FA-L00-00-0084");
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
                                                                    SysList.Add("FA-L00-00-0087");
                                                                }

                                                                //FA-L01-00-0088
                                                                //StructuredAddress/CountryID
                                                                //Check code. Must be existing in the list specified in attribute schemeID
                                                                //TODO: Check DB nomeclature 
                                                            }
                                                            else
                                                            {
                                                                // FA-L00-00-0086 - warning
                                                                SysList.Add("FA-L00-00-0086");
                                                            }

                                                            //FA-L00-00-0089
                                                            //StructuredAddress/PlotIdentification
                                                            //Check presence. Should be present.
                                                            if (string.IsNullOrEmpty(ssa.PlotIdentification.Value))
                                                            {
                                                                //FA-L00-00-0089 - warning
                                                                SysList.Add("FA-L00-00-0089");
                                                            }

                                                            if (!ssa_pobox)
                                                            {
                                                                if (!ssa_street)
                                                                {
                                                                    //FA-L00-00-0080 - warning
                                                                    SysList.Add("FA-L00-00-0080");
                                                                }
                                                            }

                                                            if (!ssa_parea)
                                                            {
                                                                if (!ssa_irl)
                                                                {
                                                                    //FA-L00-00-0082 - error
                                                                    SysList.Add("FA-L00-00-0082");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L00-00-0078 - error
                                                        SysList.Add("FA-L00-00-0078");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FA-L00-00-0067 - error
                                                SysList.Add("FA-L00-00-0067");
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
                                                                SysList.Add("FA-L01-00-0231");
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
                                                        SysList.Add("FA-L00-00-0230");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // FA-L00-00-0046 - error
                                            SysList.Add("FA-L00-00-0046");
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
                                        bool IsFALandDec = false;
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
                                                SysList.Add("FA-L00-00-0047");
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
                                                            SysList.Add("FA-L00-00-0055");
                                                        }
                                                        else
                                                        {
                                                            //FA-L01-00-0056
                                                            //VesselTransportMeans/RoleCode
                                                            //Check attribute listID. Must be FA_VESSEL_ROLE
                                                            if (sfa_rvtm.RoleCode.listID != "FA_VESSEL_ROLE")
                                                            {
                                                                // FA-L01-00-0056 - error
                                                                SysList.Add("FA-L01-00-0056");
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
                                                                        SysList.Add("FA-L00-00-0079");
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
                                                                SysList.Add("FA-L00-00-0058");
                                                            }
                                                            else
                                                            {
                                                                // FA-L01-00-0059
                                                                //VesselTransportMeans/RegistrationVesselCountry/ID
                                                                //Check schemeID. Must be TERRITORY
                                                                if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "TERRITORY")
                                                                {
                                                                    // FA-L01-00-0059 - error
                                                                    SysList.Add("FA-L01-00-0059");
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
                                                                        SysList.Add("FA-L03-00-0631");
                                                                    }
                                                                }

                                                                // FA-L03-00-0632 
                                                                if (!IsRvtmIdCfr && IsRvtmIdIRCS && IsRvtmIdEM)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0632 - error
                                                                        SysList.Add("FA-L03-00-0632");
                                                                    }
                                                                }

                                                                //FA-L03-00-0635
                                                                if (IsRvtmIdIRCS && IsRvtmIdEM)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID != "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0635 - error
                                                                        SysList.Add("FA-L03-00-0635");
                                                                    }
                                                                }

                                                                // FA-L03-00-0632 
                                                                if (IsRvtmIdUVI && SfaTCTrans)
                                                                {
                                                                    if (sfa_rvtm.RegistrationVesselCountry.ID.schemeID == "MEMBER_STATE")
                                                                    {
                                                                        // FA-L03-00-0632 - error
                                                                        SysList.Add("FA-L03-00-0632");
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
                                                            SysList.Add("FA-L01-00-0091");
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
                                                                SysList.Add("FA-L00-00-0425");
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
                                                                SysList.Add("FA-L02-00-0443");
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
                                                                SysList.Add("FA-L02-00-0266");
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
                                                                SysList.Add("FA-L02-00-0267");
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
                                                                SysList.Add("FA-L02-00-0281");
                                                            }

                                                            //FA-L02-00-0284
                                                            //FishingActivity/ReasonCode
                                                            //If the activity is a DISCARD DECLARATION and the ReasonCode is provided, the listID must be FA_REASON_DISCARD. The value may not be QEX nor OTH
                                                            if (sfa.ReasonCode != null)
                                                            {
                                                                if (sfa.ReasonCode.listID != "FA_REASON_DISCARD")
                                                                {
                                                                    //FA-L02-00-0284 - error
                                                                    SysList.Add("FA-L02-00-0284");
                                                                }
                                                                else
                                                                {
                                                                    if (sfa.ReasonCode.Value == "QEX" || sfa.ReasonCode.Value == "OTH")
                                                                    {
                                                                        //FA-L02-00-0284 - error
                                                                        SysList.Add("FA-L02-00-0284");
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
                                                                    SysList.Add("FA-L02-00-0286");
                                                                }

                                                                //FA-L02-00-0287
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //If the activity is an area exit declaration or notification, one occurrence must be POSITION; another occurrence must be AREA.
                                                                var posarea = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "POSITION");
                                                                var areapos = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "AREA");
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
                                                                        SysList.Add("FA-L02-00-0288");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0287 - error
                                                                    SysList.Add("FA-L02-00-0287");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0286 - error
                                                                SysList.Add("FA-L02-00-0286");
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
                                                                SysList.Add("FA-L00-00-0291");
                                                            }
                                                            //FA-L02-00-0292
                                                            //FishingActivity/ReasonCode
                                                            //Check presence. Must be present if the activity is an arrival notification.
                                                            if (sfa.ReasonCode == null)
                                                            {
                                                                //FA-L02-00-0292 - error
                                                                SysList.Add("FA-L02-00-0292");
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0293
                                                                //FishingActivity/ReasonCode
                                                                //Check attribute listID. Must be FA_REASON_ARRIVAL if the activity is an arrival notification.
                                                                if (sfa.ReasonCode.listID != "FA_REASON_ARRIVAL")
                                                                {
                                                                    //FA-L02-00-0293 - error
                                                                    SysList.Add("FA-L02-00-0293");
                                                                }
                                                            }

                                                            //FA-L02-00-0294
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least one occurrence must be present if the activity is an arrival notification
                                                            if (sfa.RelatedFLUXLocation.Length < 1)
                                                            {
                                                                //FA-L02-00-0294 - error
                                                                SysList.Add("FA-L02-00-0294");
                                                            }
                                                            //FA-L02-00-0295
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //TypeCode must be LOCATION for at least one occurrence if the activity is an arrival notification.
                                                            var sfalocloc = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION");
                                                            if (sfalocloc == null)
                                                            {
                                                                //FA-L02-00-0295 - warning
                                                                SysList.Add("FA-L02-00-0295");
                                                            }

                                                            //FA-L02-00-0296
                                                            //FishingActivity/SpecifiedFACatch, FishingActivity/ReasonCode
                                                            //Check presence. Must be present if the activity is an arrival notification and ReasonCode is LAN (landing)
                                                            if (sfa.ReasonCode.Value == "LAN")
                                                            {
                                                                if (sfa.SpecifiedFACatch == null)
                                                                {
                                                                    //FA-L02-00-0296 - error
                                                                    SysList.Add("FA-L02-00-0296");
                                                                }

                                                                //FA-L02-00-0298
                                                                //FishingActivity/SpecifiedFACatch/TypeCode, FishingActivity/ReasonCode
                                                                //Must have at least one occurrence with TypeCode UNLOADED if the activity is an arrival notification and ReasonCode is LAN (and if SpecifiedFACatch entity provided)
                                                                var scunloaded = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "UNLOADED");
                                                                if (scunloaded == null)
                                                                {
                                                                    //FA-L02-00-0298 - error
                                                                    SysList.Add("FA-L02-00-0298");
                                                                }
                                                            }

                                                            //FA-L02-00-0297
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //"Must have at least one occurrence with TypeCode ONBOARD if the activity is an arrival notification (and if
                                                            //SpecifiedFACatch provided)"
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var sconboard = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "ONBOARD");
                                                                if (sconboard == null)
                                                                {
                                                                    //FA-L02-00-0297 - error
                                                                    SysList.Add("FA-L02-00-0297");
                                                                }
                                                            }

                                                            //FA-L02-00-0299
                                                            //FishingActivity/SpecifiedFishingTrip/SpecifiedDelimitedPeriod/StartDateTime
                                                            //Check presence. Must be present if the activity is an arrival notification.
                                                            if (sfa.SpecifiedFishingTrip != null)
                                                            {
                                                                if (sfa.SpecifiedFishingTrip.SpecifiedDelimitedPeriod != null)
                                                                {
                                                                    var stsdpst = sfa.SpecifiedFishingTrip.SpecifiedDelimitedPeriod.FirstOrDefault(x => x.StartDateTime != null);
                                                                    if (stsdpst == null)
                                                                    {
                                                                        //FA-L02-00-0299 - warning
                                                                        SysList.Add("FA-L02-00-0299");
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
                                                                SysList.Add("FA-L00-00-0301");
                                                            }

                                                            //FA-L01-00-0302
                                                            //FishingActivity/ReasonCode
                                                            //Check attribute listID. Must be FA_REASON_ARRIVAL if the activity is an arrival declaration
                                                            if (sfa.ReasonCode == null)
                                                            {
                                                                //FA-L01-00-0302 - error
                                                                SysList.Add("FA-L01-00-0302");
                                                            }
                                                            else
                                                            {
                                                                if (sfa.ReasonCode.listID != "FA_REASON_ARRIVAL")
                                                                {
                                                                    //FA-L01-00-0302 - error 
                                                                    SysList.Add("FA-L01-00-0302");
                                                                }
                                                            }

                                                            //FA-L00-00-0303
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least one occurrence must be present if the activity is an arrival notification
                                                            if (sfa.RelatedFLUXLocation.Length < 1)
                                                            {
                                                                //FA-L00-00-0303 - error
                                                                SysList.Add("FA-L00-00-0303");
                                                            }

                                                            //FA-L01-00-0304
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //TypeCode must be LOCATION for at least one occurrence if the activity is an arrival notification.
                                                            var sfalocloc = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION");
                                                            if (sfalocloc == null)
                                                            {
                                                                //FA-L01-00-0304 - warning
                                                                SysList.Add("FA-L01-00-0304");
                                                            }

                                                            //FA-L01-00-0305
                                                            //FishingActivity/SpecifiedFishingGear/RoleCode
                                                            //RoleCode must be ONBOARD if the activity is an arrival declaration (and SpecifiedFishingGear provided).
                                                            if (sfa.SpecifiedFishingGear != null)
                                                            {
                                                                var sfgrc = sfa.SpecifiedFishingGear.SelectMany(x => x.RoleCode).FirstOrDefault(x => x.Value == "ONBOARD");
                                                                if (sfgrc == null)
                                                                {
                                                                    //FA-L01-00-0305 - error
                                                                    SysList.Add("FA-L01-00-0305");
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
                                                                var sfafch = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode != null);
                                                                if (sfafch != null)
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0415 - error 
                                                                    SysList.Add("FA-L02-00-0415");
                                                                }
                                                            }

                                                            //FA-L02-00-0416
                                                            //FishingActivity/SpecifiedFLUXCharacteristic/TypeCode
                                                            //If the activity is an arrival declaration, check the listID. Must be FA_CHARACTERISTIC.
                                                            var sfspe = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.listID == "FA_CHARACTERISTIC");
                                                            if (sfspe == null)
                                                            {
                                                                //FA-L02-00-0416 - error
                                                                SysList.Add("FA-L02-00-0416");
                                                            }
                                                        }

                                                        if (sfa.TypeCode.Value == "LANDING" && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFALandDec = true;
                                                            //FA-L02-00-0311
                                                            //FishingActivity/RelatedFLUXLocation
                                                            //Check presence. At least 1 occurrence must be present if the activity is a landing declaration.
                                                            if (sfa.RelatedFLUXLocation != null)
                                                            {
                                                                if (sfa.RelatedFLUXLocation.Length < 1)
                                                                {
                                                                    //FA-L02-00-0311 - error
                                                                    SysList.Add("FA-L02-00-0311");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0311 - error
                                                                SysList.Add("FA-L02-00-0311");
                                                            }

                                                            //FA-L02-00-0312
                                                            //FishingActivity/RelatedFLUXLocation/TypeCode
                                                            //Must be LOCATION, POSITION or ADDRESS if the activity is a landing declaration.
                                                            var locposadd = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION" ||
                                                                                            x.TypeCode.Value == "POSITION" ||
                                                                                            x.TypeCode.Value == "ADDRESS");
                                                            if (locposadd == null)
                                                            {
                                                                //FA-L02-00-0312 - error
                                                                SysList.Add("FA-L02-00-0312");
                                                            }

                                                            //FA-L02-00-0313
                                                            //FishingActivity/SpecifiedFACatch/TypeCode
                                                            //There must be at least one occurrence with TypeCode UNLOADED if the activity is a landing declaration.
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var sfafctc = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "UNLOADED");
                                                                if (sfafctc == null)
                                                                {
                                                                    //FA-L02-00-0313 - error
                                                                    SysList.Add("FA-L02-00-0313");
                                                                }

                                                                //FA-L02-00-0314
                                                                //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation
                                                                //Check presence. At least one occurrence of location of catches must be present if the activity is a landing declaration.
                                                                var listsfafl = sfa.SpecifiedFACatch.SelectMany(x => x.SpecifiedFLUXLocation);
                                                                if (listsfafl == null || listsfafl.Count() < 1)
                                                                {
                                                                    //FA-L02-00-0314 - error
                                                                    SysList.Add("FA-L02-00-0314");
                                                                }

                                                                //FA-L02-00-0315
                                                                //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode
                                                                //If the activity is a landing declaration TypeCode must be AREA.
                                                                var sffarea = listsfafl.FirstOrDefault(x => x.TypeCode.Value == "AREA");
                                                                if (sffarea == null)
                                                                {
                                                                    //FA-L02-00-0315 - error
                                                                    SysList.Add("FA-L02-00-0315");
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
                                                                var lunloaded = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED" || x.TypeCode.Value == "UNLOADED");
                                                                if (lunloaded == null)
                                                                {
                                                                    //FA-L02-00-0445 - error
                                                                    SysList.Add("FA-L02-00-0445");
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
                                                                            SysList.Add("FA-L02-00-0328");
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
                                                                            var destloc = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.Value == "DESTINATION_LOCATION");
                                                                            if (destloc == null)
                                                                            {
                                                                                //FA-L02-00-0329 - error
                                                                                SysList.Add("FA-L02-00-0329");
                                                                            }
                                                                            else
                                                                            {
                                                                                var destlocloc = destloc.SpecifiedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION");
                                                                                if (destlocloc == null)
                                                                                {
                                                                                    //FA-L02-00-0329 - error
                                                                                    SysList.Add("FA-L02-00-0329");
                                                                                }
                                                                                else
                                                                                {
                                                                                    var destloclocid = destloc.SpecifiedFLUXLocation.FirstOrDefault(x => x.ID.Value == "LOCATION");
                                                                                    if (destloclocid == null)
                                                                                    {
                                                                                        //FA-L02-00-0329 - error
                                                                                        SysList.Add("FA-L02-00-0329");
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
                                                                    SysList.Add("FA-L02-00-0326");
                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0327
                                                                    //FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode
                                                                    //If the activity is a transhipment declaration the
                                                                    //SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode must be AREA
                                                                    var fcaflocarea = fcafloc.FirstOrDefault(x => x.TypeCode.Value == "AREA");
                                                                    if (fcaflocarea == null)
                                                                    {
                                                                        //FA-L02-00-0327 - error
                                                                        SysList.Add("FA-L02-00-0327");
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
                                                                    SysList.Add("FA-L02-00-0321");
                                                                }

                                                                //FA-L02-00-0322
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //If the activity is a transhipment declaration, the TypeCode value of at least one RelatedFLUXLocation must be LOCATION or POSITION.
                                                                var rfllocpos = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION" || x.TypeCode.Value == "POSITION");
                                                                if (rfllocpos == null)
                                                                {
                                                                    //FA-L02-00-0322 - error
                                                                    SysList.Add("FA-L02-00-0322");
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
                                                                var spfacatcunl = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "UNLOADED");
                                                                if (spfacatcunl != null)
                                                                {
                                                                    var vtmrec = sfa.RelatedVesselTransportMeans.FirstOrDefault(x => x.RoleCode.Value == "RECEIVER");
                                                                    if (vtmrec == null)
                                                                    {
                                                                        //FA-L02-00-0324 - error
                                                                        SysList.Add("FA-L02-00-0324");
                                                                    }
                                                                }

                                                                //FA-L02-00-0325
                                                                //"FishingActivity/RelatedVesselTransportMeans/RoleCode,
                                                                //FishingActivity / SpecifiedFACatch / TypeCode"
                                                                //If the activity is a transhipment declaration the RoleCode of the related vessel must be
                                                                //DONOR (donor vessel) if SpecifiedFACatch/TypeCode is LOADED
                                                                var spfacatcunl1 = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED");
                                                                if (spfacatcunl1 != null)
                                                                {
                                                                    var vtmrec1 = sfa.RelatedVesselTransportMeans.FirstOrDefault(x => x.RoleCode.Value == "DONOR");
                                                                    if (vtmrec1 == null)
                                                                    {
                                                                        //FA-L02-00-0325 - error
                                                                        SysList.Add("FA-L02-00-0325");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L02-00-0323 - error
                                                                SysList.Add("FA-L02-00-0323");
                                                            }
                                                            if (sfa.SpecifiedFLUXCharacteristic != null)
                                                            {
                                                                //FA-L00-00-0440
                                                                //FishingActivity/SpecifiedFLUXC haracteristic/TypeCode
                                                                //If the activity is a transhipment declaration, the attribute
                                                                //listID must be FA_CHARACTERISTIC
                                                                var sfafchfa = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.listID == "FA_CHARACTERISTIC");
                                                                if (sfafchfa == null)
                                                                {
                                                                    //FA-L00-00-0440 - error
                                                                    SysList.Add("FA-L00-00-0440");
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
                                                                var relunloaded = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED" || x.TypeCode.Value == "UNLOADED");
                                                                if (relunloaded == null)
                                                                {
                                                                    //FA-L02-00-0444 - error
                                                                    SysList.Add("FA-L02-00-0444");
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
                                                                            var rvtmrc = sfa.RelatedVesselTransportMeans.FirstOrDefault(x => x.RoleCode.Value == "RECEIVER");
                                                                            if (rvtmrc == null)
                                                                            {
                                                                                //FA-L02-00-0334 - error
                                                                                SysList.Add("FA-L02-00-0334");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            //FA-L02-00-0334 - error
                                                                            SysList.Add("FA-L02-00-0334");
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
                                                                    SysList.Add("FA-L02-00-0331");
                                                                }

                                                                //FA-L02-00-0332
                                                                //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                //Check attribute listID. If the activity is a transhipment or relocation notification, the RelatedFLUXLocation/TypeCode must be LOCATION or POSITION for at least one location.
                                                                var rrfllocpos = sfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "LOCATION" || x.TypeCode.Value == "POSITION");
                                                                if (rrfllocpos == null)
                                                                {
                                                                    //FA-L02-00-0332 - error
                                                                    SysList.Add("FA-L02-00-0332");
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
                                                                    SysList.Add("FA-L02-00-0342");
                                                                }


                                                            }
                                                            if (sfa.SpecifiedFLUXCharacteristic != null)
                                                            {
                                                                //FA-L02-00-0338
                                                                //"FishingActivity/SpecifiedFLUXCharacteristic/ValueQuantity,
                                                                //FishingActivity / SpecifiedFLUXCharacteristic / TypeCode"
                                                                //If the value of TypeCode is NB_CAGES_TOWED the value must be a positive integer number or zero (>=0)
                                                                var nbcagess = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.Value == "NB_CAGES_TOWED");
                                                                if (nbcagess != null)
                                                                {
                                                                    if (nbcagess.ValueQuantity != null)
                                                                    {
                                                                        if (nbcagess.ValueQuantity.Value < 0)
                                                                        {
                                                                            //FA-L02-00-0338 - warning
                                                                            SysList.Add("FA-L02-00-0338");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L02-00-0338 - warning
                                                                        SysList.Add("FA-L02-00-0338");
                                                                    }
                                                                }
                                                            }
                                                            if (sfa.SpecifiedFACatch != null)
                                                            {
                                                                var bftrunl = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED");
                                                                if (bftrunl != null)
                                                                {
                                                                    //FA-L02-00-0344
                                                                    //"FishingActivity/SpecifiedFACatch/SpecifiedFLUXLocation/TypeCode,
                                                                    //FishingActivity/TypeCode, 
                                                                    //SpecifiedFACatch/TypeCode"
                                                                    //SpecifiedFLUXLocation/TypeCode must be AREA if the activity is a transhipment notification and SpecifiedFACatch/TypeCode=LOADE D
                                                                    var sfalovload = sfa.SpecifiedFACatch.SelectMany(x => x.SpecifiedFLUXLocation).FirstOrDefault(x => x.TypeCode.Value == "AREA");
                                                                    if (sfalovload == null)
                                                                    {
                                                                        //FA-L02-00-0344 - error
                                                                        SysList.Add("FA-L02-00-0344");
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        if ((sfa.TypeCode.Value == "RELOCATION" || sfa.TypeCode.Value == "TRANSHIPMENT")
                                                            && fardoc.TypeCode.Value == "DECLARATION")
                                                        {
                                                            IsFATransOrRelocDec = true;
                                                        }
                                                         
                                                        if (IsFADiscard || IsFALandDec || IsFATransOrRelocDec)
                                                        {
                                                            //FA-L02-00-0622
                                                            //"FishingActivity/SpecifiedFACatch, 
                                                            //FishingActivity / TypeCode, 
                                                            //FAReportDocument / TypeCode"
                                                            //"Check presence. At least 1 occurrence must be present 
                                                            //if (1)[FishingActivity/ TypeCode = RELOCATION or TRANSHIPMENT] 
                                                            //Or(2)[FishingActivity / TypeCode = (DISCARD or LANDING) 
                                                            //and FAReportDocument/ TypeCode = DECLARATION"
                                                            if (sfa.SpecifiedFACatch == null)
                                                            {
                                                                //FA-L02-00-0622 - error
                                                                SysList.Add("FA-L02-00-0622");
                                                            }
                                                        }

                                                        if (IsFAAEnOrNot || IsFAFO || IsFAAEnOrNot)
                                                        {
                                                            //FA-L02-00-0112
                                                            //"FishingActivity/RelatedFishingActivity, 
                                                            //FishingActivity / TypeCode, 
                                                            //FAReportDocument / TypeCode"
                                                            //"Check presence. May only be present 
                                                            //if FishingActivity / TypeCode is FISHING_OPERATION(DECLARATION), 
                                                            //JOINT_FISHING_OPERATION(DECLARATION)
                                                            //and AREA_ENTRY(NOTIFICATION)"
                                                            if (sfa.RelatedFishingActivity == null)
                                                            {
                                                                //FA-L02-00-0112 - warning
                                                                SysList.Add("FA-L02-00-0112");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //FA-L00-00-0090 - error
                                                        SysList.Add("FA-L00-00-0090");
                                                    }
                                                }
                                                else
                                                {
                                                    //FA-L00-00-0090 - error
                                                    SysList.Add("FA-L00-00-0090");
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
                                                            SysList.Add("FA-L00-00-0236");
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
                                                            SysList.Add("FA-L02-00-0249");
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
                                                            SysList.Add("FA-L02-00-0258");
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
                                                                    SysList.Add("FA-L02-00-0259");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L02-00-0259 - error
                                                            SysList.Add("FA-L02-00-0259");
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
                                                            SysList.Add("FA-L00-00-0421");
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
                                                            SysList.Add("FA-L01-00-0105");
                                                        }
                                                        //FA-L01-00-0106
                                                        //FishingActivity/SpecifiedDelimitedPeriod/DurationMeasure
                                                        //Check attribute unitCode. Must be MIN (minutes)
                                                        if (sfadp.DurationMeasure.unitCode != "MIN")
                                                        {
                                                            //FA-L01-00-0106 - warning
                                                            SysList.Add("FA-L01-00-0106");
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
                                                                SysList.Add("FA-L02-00-0093");
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
                                                                        SysList.Add("FA-L02-00-0610");
                                                                    }

                                                                    //FA-L02-00-0548
                                                                    //"FishingActivity/RelatedFishingActivity/RelatedFLUXLocation/TypeCode, 
                                                                    //FishingActivity / RelatedFishingActivity / TypeCode"
                                                                    //"Check value of RelatedFishingActivity/RelatedFLUXL ocation/TypeCode. 
                                                                    //If RelatedFishingActivity/ TypeCode = GEAR_SHOT and the activity is
                                                                    //  fishing operation declaration, there must be at least one occurrence
                                                                    //  with TypeCode = POSITION"
                                                                    var sfarfapos = sfarfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "POSITION");
                                                                    if (sfarfapos == null)
                                                                    {
                                                                        //FA-L02-00-0548 - error
                                                                        SysList.Add("FA-L02-00-0548");
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
                                                                        SysList.Add("FA-L02-00-610");
                                                                    }
                                                                    //FA-L02-00-0549
                                                                    //"FishingActivity/RelatedFishingActivity/RelatedFLUXLocation/TypeCode, 
                                                                    //FishingActivity / RelatedFishingActivity / TypeCode"
                                                                    //"Check value of RelatedFishingActivity/RelatedFLUXL ocation/TypeCode 
                                                                    //If RelatedFishing Activity/TypeCode= GEAR_RETRIEVAL_SHOT and the 
                                                                    //activity is fishing operation declaration, there must be at least one 
                                                                    //occurrence with TypeCode=POSITION"
                                                                    var sfarfapos = sfarfa.RelatedFLUXLocation.FirstOrDefault(x => x.TypeCode.Value == "POSITION");
                                                                    if (sfarfapos == null)
                                                                    {
                                                                        //FA-L02-00-0549 - error
                                                                        SysList.Add("FA-L02-00-0549");
                                                                    }
                                                                }
                                                            }

                                                            if (IsTCodeGShot || IsTCodeGRetr)
                                                            {
                                                                //FA-L02-00-0649
                                                                //"FishingActivity/RelatedFishingActivity/RelatedFLUXLocation, 
                                                                //FishingActivity / RelatedFishingActivity / TypeCode"
                                                                //"Check presence. At least one occurrence must be present
                                                                //if RelatedFishingActivity / TypeCode = GEAR_SHOT or GEAR_RETRIEVAL,
                                                                //   the main activity is fishing operation declaration."
                                                                if (sfarfa.RelatedFLUXLocation != null && sfarfa.RelatedFLUXLocation.Length > 0)
                                                                {

                                                                }
                                                                else
                                                                {
                                                                    //FA-L02-00-0649 - error
                                                                    SysList.Add("FA-L02-00-0649");
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
                                                            SysList.Add("FA-L02-00-0096");
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
                                                                        SysList.Add("FA-L01-00-0120");
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
                                                                            SysList.Add("FA-L01-00-0122");
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
                                                                        SysList.Add("FA-L00-00-0124");
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L01-00-0125
                                                                        //GearCharacteristic/TypeCode
                                                                        //Check attribute listID. Must be FA_GEAR_CHARACTERISTIC
                                                                        if (gchars.TypeCode.listID != "FA_GEAR_CHARACTERISTIC")
                                                                        {
                                                                            //FA-L01-00-0125 - error
                                                                            SysList.Add("FA-L00-00-0125");
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
                                                                            SysList.Add("FA-L00-00-0128");
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
                                                                            SysList.Add("FA-L00-00-0129");
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
                                                                            SysList.Add("FA-L00-00-0130");
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
                                                                            //FA-L00-00-0131 - error.
                                                                            SysList.Add("FA-L00-00-0131");
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
                                                                            SysList.Add("FA-L00-00-0132");
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
                                                                        SysList.Add("FA-L01-00-0422");
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
                                                                            SysList.Add("FA-L01-00-0428");
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
                                                                        SysList.Add("FA-L02-00-0333");
                                                                    }

                                                                    //FA-L02-00-0335
                                                                    //"FishingActivity/SpecifiedFLUX haracteristic, 
                                                                    //FishingActivity/SpecifiedFACatch"
                                                                    //Check presence. Must be present if the activity is a relocation notification and SpecifiedFACatch/Species is BFT.
                                                                    if (sfa.SpecifiedFLUXCharacteristic == null)
                                                                    {
                                                                        //FA-L02-00-0335 - error
                                                                        SysList.Add("FA-L02-00-0335");
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
                                                                        var bftrelunl = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "UNLOADED");
                                                                        if (bftrelunl != null)
                                                                        {
                                                                            var nbcages = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.Value == "NB_CAGES_TOWED");
                                                                            if (nbcages == null)
                                                                            {
                                                                                //FA-L02-00-0336 - error
                                                                                SysList.Add("FA-L02-00-0336");
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
                                                                        var bftrunl = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED");
                                                                        if (bftrunl != null)
                                                                        {
                                                                            if (sfa.RelatedVesselTransportMeans != null)
                                                                            {
                                                                                var rvtmdonor = sfa.RelatedVesselTransportMeans.FirstOrDefault(x => x.RoleCode.Value == "DONOR");
                                                                                if (rvtmdonor == null)
                                                                                {
                                                                                    //FA-L02-00-0343 - error
                                                                                    SysList.Add("FA-L02-00-0343");
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                //FA-L02-00-0343 - error
                                                                                SysList.Add("FA-L02-00-0343");
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
                                                                        SysList.Add("FA-L02-00-0449");
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L02-00-0448
                                                                        //FishingActivity/SpecifiedFLAPDocument/ID, SpecifiedFACatch/SpeciesCode
                                                                        //If the activity is a relocation or transhipment declaration and
                                                                        //SpecifiedFACatch/SpeciesCode is BFT, the attribute schemeID of
                                                                        //SpecifiedFLAPDocument/ID must be ICCAT_AUTHORIZATION.
                                                                        var sfaflapid = sfa.SpecifiedFLAPDocument.FirstOrDefault(x => x.ID.schemeID == "ICCAT_AUTHORIZATION");
                                                                        if (sfaflapid == null)
                                                                        {
                                                                            //FA-L02-00-0448 - warning
                                                                            SysList.Add("FA-L02-00-0448");
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
                                                                        var sfarfa_sfclocid = sfarfa_sfc.SpecifiedFLUXLocation.FirstOrDefault(x => x.ID.schemeID == "FAO_AREA");
                                                                        if (sfarfa_sfclocid != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0184 - warning
                                                                                    SysList.Add("FA-L02-00-0184");
                                                                                }
                                                                            }
                                                                        }

                                                                        //FA-L02-00-0186
                                                                        //AAPProduct/UnitQuantity,
                                                                        //FishingActivity / TypeCode, 
                                                                        //FACatch / SpeciesCode, 
                                                                        //FACatch / SpecifiedFLUXLocation / ID
                                                                        //UnitQuantity must be present if FishingActivity/TypeCode is LANDING or TRANSHIPMENT and FACatch/SpeciesCode is BFT and at least one area has schemeID of FACatch/SpecifiedFLUXLocation.ID equal to GFCM_GSA
                                                                        var sfarfa_sfclocidgsa = sfarfa_sfc.SpecifiedFLUXLocation.FirstOrDefault(x => x.ID.schemeID == "GFCM_GSA");
                                                                        if (sfarfa_sfclocidgsa != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0184 - warning
                                                                                    SysList.Add("FA-L02-00-0184");
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
                                                                        var sfarfa_sfclocidgsa = sfarfa_sfc.SpecifiedFLUXLocation.FirstOrDefault(x => x.ID.schemeID == "GFCM_GSA");
                                                                        if (sfarfa_sfclocidgsa != null)
                                                                        {
                                                                            if (sfarfa_sfc.AppliedAAPProcess != null)
                                                                            {
                                                                                var aapunitq = sfarfa_sfc.AppliedAAPProcess.SelectMany(x => x.ResultAAPProduct).Select(x => x.UnitQuantity).ToList();
                                                                                if (aapunitq == null)
                                                                                {
                                                                                    //FA-L02-00-0187 - warning
                                                                                    SysList.Add("FA-L02-00-0187");
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                        }

                                                        if (sfa.SpecifiedFLUXCharacteristic != null)
                                                        {
                                                            //FA-L01-00-0113
                                                            //FishingActivity/SpecifiedFLUXC haracteristic/TypeCode
                                                            //"Check attribute listID. Must be FA_CHARACTERISTIC 
                                                            //if used in entity FishingActivity"
                                                            var sfasfctc = sfa.SpecifiedFLUXCharacteristic.FirstOrDefault(x => x.TypeCode.listID != "FA_CHARACTERISTIC ");
                                                            if (sfasfctc != null)
                                                            {
                                                                //FA-L01-00-0113 - error
                                                                SysList.Add("FA-L01-00-0113");
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
                                                                SysList.Add("FA-L00-00-0426");
                                                            }

                                                            //FA-L01-00-0429
                                                            //"FishingActivity/RelatedFishingActivity/SpecifiedFACatch/TypeCode,
                                                            //FishingActivity/RelatedFishingActivity/SpecifiedFACatch/SpeciesCode"
                                                            //Check code. At least one occurrence of ALLOCATED_TO_QUOTA must be present if the activity
                                                            //is a joint fishing operation declaration and SpeciesCode is BFT
                                                            var fsfac = sfarfa.SpecifiedFACatch.FirstOrDefault(x => x.SpeciesCode.Value == "BFT");
                                                            if (fsfac != null)
                                                            {
                                                                if (fsfac.TypeCode.Value != "ALLOCATED_TO_QUOTA")
                                                                {
                                                                    //FA-L01-00-0429 - error
                                                                    SysList.Add("FA-L01-00-0429");
                                                                }

                                                                ///FA-L01-00-0109
                                                                //"FishingActivity/ID, 
                                                                //FishingActivity / RelatedFishingActivity / SpecifiedFACatch / Species Code"
                                                                //Check attribute schemeID. Must be JFO if the activity is a joint fishing operation declaration and if SpeciesCode is BFT.
                                                                var fsfaid = sfa.ID.FirstOrDefault(x => x.schemeID == "JFO");
                                                                if (fsfaid == null)
                                                                {
                                                                    ///FA-L01-00-0109 - warning
                                                                    SysList.Add("FA-L01-00-0109");
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
                                                                SysList.Add("FA-L00-00-0424");
                                                            }
                                                        }
                                                    }

                                                    //FA-L01-00-0423
                                                    //FishingActivity/RelatedFishingActivity/TypeCode
                                                    //If a sub-activity is provided it must have TypeCode=RELOCATION if the main activity is a joint fishing operation declaration
                                                    if (IsFAJFO)
                                                    {
                                                        var frel = sfa.RelatedFishingActivity.FirstOrDefault(x => x.TypeCode.Value == "RELOCATION");
                                                        if (frel == null)
                                                        {
                                                            //FA-L01-00-0423 - error
                                                            SysList.Add("FA-L01-00-0423");
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
                                                            SysList.Add("FA-L02-00-0260");
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
                                                    SysList.Add("FA-L01-00-0094");

                                                    if (((Issfarfaodt) || (Issfarfadps && Issfarfadpe)) || (sfadps && sfadpe))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        //FA-L02-00-0095 - error
                                                        SysList.Add("FA-L02-00-0095");
                                                    }
                                                    //FA-L00-00-0234
                                                    //FishingActivity/OccurrenceDateTime
                                                    //Check presence. Must be present if the activity is a departure declaration.
                                                    if (IsFADeparture)
                                                    {
                                                        //FA-L00-00-0234 - error
                                                        SysList.Add("FA-L00-00-0234");
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
                                                            SysList.Add("FA-L01-00-0235");
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
                                                            SysList.Add("FA-L01-00-0247");
                                                        }
                                                        //FA-L02-00-0248
                                                        //FishingActivity/SpeciesTargetCode, FishingActivity/ReasonCode
                                                        //Check presence. If the activity is a area entry declaration SpeciesTargetCode must be present if ReasonCode is TRZ or FIS
                                                        if (sfa.ReasonCode.Value == "TRZ" || sfa.ReasonCode.Value == "FIS")
                                                        {
                                                            if (sfa.SpeciesTargetCode == null)
                                                            {
                                                                //FA-L02-00-0248 - error
                                                                SysList.Add("FA-L02-00-0248");
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
                                                        SysList.Add("FA-L00-00-0098");
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
                                                        SysList.Add("FA-L00-00-0100");
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
                                                            SysList.Add("FA-L02-00-0600");
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
                                                            SysList.Add("FA-L02-00-0601");
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
                                                        SysList.Add("FA-L01-00-0102");
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
                                                        SysList.Add("FA-L02-00-0256");
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
                                                    SysList.Add("FA-L02-00-0107");
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
                                                    SysList.Add("FA-L02-00-0108");
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
                                                        SysList.Add("FA-L00-00-0111");
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
                                                                SysList.Add("FA-L01-00-0102");
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
                                                                    SysList.Add("FA-L01-00-0122");
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
                                                                SysList.Add("FA-L00-00-0124");
                                                            }
                                                            else
                                                            {
                                                                //FA-L01-00-0125
                                                                //GearCharacteristic/TypeCode
                                                                //Check attribute listID. Must be FA_GEAR_CHARACTERISTIC
                                                                if (gchars.TypeCode.listID != "FA_GEAR_CHARACTERISTIC")
                                                                {
                                                                    //FA-L01-00-0125 - error
                                                                    SysList.Add("FA-L01-00-0125");
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
                                                                    SysList.Add("FA-L00-00-0128");
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
                                                                    SysList.Add("FA-L00-00-0129");
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
                                                                    SysList.Add("FA-L00-00-0130");
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
                                                                    SysList.Add("FA-L00-00-0131");
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
                                                                    SysList.Add("FA-L00-00-0132");
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
                                                            SysList.Add("FA-L01-00-0239");
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
                                                            SysList.Add("FA-L01-00-0104");
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
                                                            SysList.Add("FA-L02-00-0257");
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
                                                                SysList.Add("FA-L01-00-0136");
                                                            }
                                                            //FA-L01-00-0137
                                                            //GearProblem/TypeCode
                                                            //Check code. Must be existing in the list specified in attribute listID.
                                                            //TODO: check in DB
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0135 - error
                                                            SysList.Add("FA-L00-00-0135");
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
                                                                SysList.Add("FA-L01-00-0139");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0138 - error 
                                                            SysList.Add("FA-L00-00-0138");
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
                                                                    SysList.Add("FA-L01-00-0141");
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
                                                                        SysList.Add("FA-L00-00-0144");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0144 - error
                                                                    SysList.Add("FA-L00-00-0144");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0143 - error
                                                            SysList.Add("FA-L00-00-0143");
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
                                                                SysList.Add("FA-L00-00-0150");
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
                                                                SysList.Add("FA-L01-00-0151");
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
                                                                    SysList.Add("FA-L02-00-0240");
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
                                                            SysList.Add("FA-L00-00-0150");
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
                                                                SysList.Add("FA-L00-00-0153");
                                                            }
                                                            //FA-L01-00-0154
                                                            //FACatch/SpeciesCode
                                                            //Check attribute listID. Must be FAO_SPECIES.
                                                            if (sfafc.SpeciesCode.listID != "FAO_SPECIES")
                                                            {
                                                                //FA-L01-00-0154 - error
                                                                SysList.Add("FA-L01-00-0154");
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
                                                                SysList.Add("FA-L02-00-0156");
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
                                                                                SysList.Add("FA-L02-00-0156");
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
                                                                                SysList.Add("FA-L01-00-0176");
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
                                                                                SysList.Add("FA-L01-00-0178");
                                                                            }
                                                                            //FA-L01-00-0179
                                                                            //AAPProduct/PackagingUnitAverageWeightMeasure
                                                                            //Must be a strict positive number (>0)
                                                                            if (ares.PackagingUnitAverageWeightMeasure.Value <= 0)
                                                                            {
                                                                                //FA-L01-00-0179 - error
                                                                                SysList.Add("FA-L01-00-0179");
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
                                                                                SysList.Add("FA-L01-00-0180");
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
                                                                                SysList.Add("FA-L01-00-0183");
                                                                            }
                                                                            //FA-L01-00-0182
                                                                            //AAPProduct/WeightMeasure
                                                                            //Check attribute UnitCode. Must be KGM.
                                                                            if (ares.WeightMeasure.unitCode != "KGM")
                                                                            {
                                                                                //FA-L01-00-0182 - error
                                                                                SysList.Add("FA-L01-00-0182");
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
                                                                                SysList.Add("FA-L01-00-0185");
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
                                                                            SysList.Add("FA-L01-00-0172");
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
                                                                        SysList.Add("FA-L01-00-0174");
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
                                                                        SysList.Add("FA-L02-00-0441");
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
                                                                            SysList.Add("FA-L02-00-0156");
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
                                                                                SysList.Add("FA-L02-00-0410");
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
                                                                                SysList.Add("FA-L02-00-0411");
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
                                                                SysList.Add("FA-L01-00-0158");
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
                                                                SysList.Add("FA-L01-00-0160");
                                                            }
                                                            //FA-L01-00-0161
                                                            //FACatch/WeightMeasure
                                                            if (sfafc.WeightMeasure.Value < 0)
                                                            {
                                                                //FA-L01-00-0161 - error
                                                                SysList.Add("FA-L01-00-0161");
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
                                                                SysList.Add("FA-L01-00-0163");
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
                                                                        SysList.Add("FA-L01-00-0166");
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
                                                                    SysList.Add("FA-L01-00-0169");
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
                                                                SysList.Add("FA-L00-00-0641");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //FA-L00-00-0641 - error
                                                            SysList.Add("FA-L00-00-0641");
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
                                                            SysList.Add("FA-L02-00-0662");
                                                        }
                                                        if (IsSFLocIDSchGSACount > 1)
                                                        {
                                                            //FA-L02-00-0663 - error
                                                            SysList.Add("FA-L02-00-0663");
                                                        }
                                                        if (IsSFLocIDSchEZCount > 1)
                                                        {
                                                            //FA-L02-00-0663 - error
                                                            SysList.Add("FA-L02-00-0663");
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
                                                                    SysList.Add("FA-L01-00-0191");
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
                                                            SysList.Add("FA-L00-00-0190");
                                                        }

                                                        if (sfa.SpecifiedFishingTrip.TypeCode != null)
                                                        {
                                                            //FA-L01-00-0193
                                                            //FishingTrip/TypeCode
                                                            //Check attribute listID. Must be FISHING_TRIP_TYPE
                                                            if (sfa.SpecifiedFishingTrip.TypeCode.listID != "FISHING_TRIP_TYPE")
                                                            {
                                                                //FA-L01-00-0193 - error
                                                                SysList.Add("FA-L01-00-0193");
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
                                                                    SysList.Add("FA-L01-00-0196");
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
                                                                        SysList.Add("FA-L01-00-0237");
                                                                    }
                                                                    //FA-L01-00-0238
                                                                    //FishingActivity/RelatedFLUXLocation/TypeCode
                                                                    //A type of FLUXLocation of a particular type may only appear once if the activity is a departure declaration
                                                                    if (!DepTypeCode.Add(sfarfl.TypeCode.Value))
                                                                    {
                                                                        //FA-L01-00-0238 - error
                                                                        SysList.Add("FA-L01-00-0238");
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
                                                                        SysList.Add("FA-L02-00-0250");
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
                                                                                SysList.Add("FA-L02-00-0251");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //FA-L00-00-0195 - error
                                                                SysList.Add("FA-L00-00-0195");
                                                            }

                                                            //FA-L02-00-0198
                                                            //FLUXLocation/CountryID, FLUXLocation/TypeCode
                                                            //CountryID must be present if TypeCode is LOCATION or ADDRESS.
                                                            if (IsSFARFLLocOrAdd)
                                                            {
                                                                if (sfarfl.CountryID == null)
                                                                {
                                                                    //FA-L02-00-0198 - error
                                                                    SysList.Add("FA-L02-00-0198");
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
                                                                    SysList.Add("FA-L01-00-0199");
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
                                                                        SysList.Add("FA-L01-00-0202");
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
                                                                        SysList.Add("FA-L01-00-0202");
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
                                                                    SysList.Add("FA-L02-00-0201");
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
                                                                        SysList.Add("FA-L01-00-0209");
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0207 - error
                                                                    SysList.Add("FA-L00-00-0207");
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
                                                                        SysList.Add("FA-L01-00-0212");
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    //FA-L00-00-0210 - error
                                                                    SysList.Add("FA-L00-00-0210");
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
                                                                    SysList.Add("FA-L02-00-0206");
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
                                                                    SysList.Add("FA-L02-00-0215");
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
                                                                            SysList.Add("FA-L01-00-0216");
                                                                        }

                                                                        //FA-L01-00-0221
                                                                        //FLUXCharacteristic/TypeCode
                                                                        //Check the value of the code. Must be existing in the list specified in attribute listID
                                                                        //TODO: check db - FLUX_LOCATION_CHARACTERISTIC
                                                                    }
                                                                    else
                                                                    {
                                                                        //FA-L00-00-0220 - error
                                                                        SysList.Add("FA-L00-00-0220");
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
                                                                            SysList.Add("FA-L00-00-0223");
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
                                                                            SysList.Add("FA-L00-00-0224");
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
                                                                            SysList.Add("FA-L00-00-0225");
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
                                                                            SysList.Add("FA-L00-00-0225");
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
                                                                                SysList.Add("FA-L00-00-0227");
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
                                                                            SysList.Add("FA-L00-00-0228");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                    if (IsFAReloc)
                                                    {
                                                        var ftcunloaded = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "UNLOADED");
                                                        var ftcloaded = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "LOADED");
                                                        //FA-L02-00-0268
                                                        //"FishingActivity/RelatedVesselTransportMeans, 
                                                        //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                        //Check presence. Must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                        var fspbft = sfa.SpecifiedFACatch.FirstOrDefault(x => x.SpeciesCode.Value == "BFT");
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
                                                                    SysList.Add("FA-L02-00-0269");
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
                                                                        SysList.Add("FA-L02-00-0417");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (sfa.DestinationVesselStorageCharacteristic.ID.Value != "BFT-CAGE-ID")
                                                                        {
                                                                            //FA-L02-00-0417 - error
                                                                            SysList.Add("FA-L02-00-0417");
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
                                                                    SysList.Add("FA-L02-00-0273");
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
                                                                        SysList.Add("FA-L02-00-0275");
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
                                                                SysList.Add("FA-L02-00-0270");
                                                            }

                                                            //FA-L02-00-0276
                                                            //"FishingActivity/SpecifiedFACatch/DestinationFLUXLocation/TypeCode,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //DestinationFLUXLocation/TypeCode must be LOCATION if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var fsfcdlocloc = sfa.SpecifiedFACatch.SelectMany(x => x.DestinationFLUXLocation).Select(x => x.TypeCode.Value == "LOCATION").ToList();
                                                            if (fsfcdlocloc == null || fsfcdlocloc.Count == 0)
                                                            {
                                                                //FA-L02-00-0276 - error
                                                                SysList.Add("FA-L02-00-0276");
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
                                                                SysList.Add("FA-L02-00-0277");
                                                            }

                                                            //FA-L02-00-0430
                                                            //"FAReportDocument/RelatedReportID,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //RelatedReportID must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT
                                                            if (fardoc.RelatedReportID == null)
                                                            {
                                                                //FA-L02-00-0430 - warning
                                                                SysList.Add("FA-L02-00-0430");
                                                            }

                                                            //FA-L02-00-0442
                                                            //"FishingActivity/RelatedVesselTransportMeans/Name,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //Name of the related vessel must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var sfavtmname = sfa.RelatedVesselTransportMeans.Select(x => x.Name != null).ToList();
                                                            if (sfavtmname == null || sfavtmname.Count == 0)
                                                            {
                                                                //FA-L02-00-0442 - error
                                                                SysList.Add("FA-L02-00-0442");
                                                            }

                                                            //FA-L02-00-0446
                                                            //"FishingActivity/RelatedVesselTransportMeans/SpecifiedContactParty/RoleCode,
                                                            //FishingActivity / SpecifiedFACatch / SpeciesCode"
                                                            //Check presence. Must be present if the activity is a relocation declaration and SpecifiedFACatch/SpeciesCode is BFT.
                                                            var sfavtmscprc = sfa.RelatedVesselTransportMeans.SelectMany(x => x.SpecifiedContactParty).Select(x => x.RoleCode != null).ToList();
                                                            if (sfavtmscprc == null || sfavtmscprc.Count == 0)
                                                            {
                                                                //FA-L02-00-0446 - error
                                                                SysList.Add("FA-L02-00-0446");
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
                                                                SysList.Add("FA-L02-00-0447");
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
                                                                SysList.Add("FA-L02-00-0274");
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
                                                            SysList.Add("FA-L02-00-0283");
                                                        }
                                                    }

                                                    if (IsFAAExOrNot)
                                                    {
                                                        //FA-L02-00-0289
                                                        //FishingActivity/SpecifiedFACatch/TypeCode
                                                        //Must have at least one occurrence with TypeCode ONBOARD if the activity is an area exit declaration or notification
                                                        var aexitonboard = sfa.SpecifiedFACatch.FirstOrDefault(x => x.TypeCode.Value == "ONBOARD");
                                                        if (aexitonboard == null)
                                                        {
                                                            //FA-L02-00-0289 - error
                                                            SysList.Add("FA-L02-00-0289");
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
                                                        SysList.Add("FA-L02-00-0252");
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
                                                            SysList.Add("FA-L00-00-0646");
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

                                        //bool IsRCountryMState = false;
                                        //if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry != null)
                                        //{
                                        //    if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID != null)
                                        //    {
                                        //        if (fardoc.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID == "MEMBER_STATE")
                                        //        {
                                        //            IsRCountryMState = true;
                                        //        }
                                        //    }
                                        //}

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
                                            SysList.Add("FA-L00-00-0050");
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
                                                SysList.Add("FA-L02-00-0053");
                                                SysList.Add("FA-L01-00-0054");
                                            }
                                        }

                                        if (isvtm_schemeCFR)
                                        {
                                            //
                                            //TODO: check value is in MEMBER_STATE
                                            //FA-L03-00-0630 - error
                                        }
                                        else
                                        {
                                          
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
                                            var fqids = faquery.FAQuery.SubmitterFLUXParty.ID.FirstOrDefault(z => z.schemeID == "FLUX_GP_PARTY");
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
                                        var spartc = faquery.FAQuery.SimpleFAQueryParameter.FirstOrDefault(x => x.TypeCode.Value == "CONSOLIDATED");
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
                                        var spartc1 = faquery.FAQuery.SimpleFAQueryParameter.FirstOrDefault(x => x.TypeCode.Value == "VESSELID");
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
                                        var spartctripid = faquery.FAQuery.SimpleFAQueryParameter.FirstOrDefault(x => x.TypeCode.Value == "TRIPID");
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
                                            var spartctrip = faquery.FAQuery.SimpleFAQueryParameter.FirstOrDefault(x => x.TypeCode.Value == "TRIPID");
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
                                        var spartc2 = faquery.FAQuery.SimpleFAQueryParameter.FirstOrDefault(x => x.TypeCode.listID != "FA_QUERY_PARAMETER");
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
                                        var respdocid = FAresp.FLUXResponseDocument.ID.FirstOrDefault(x => x.schemeID == "UUID");
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

                                        //FA-L02-00-0368
                                        //"FLUXResponseDocument/ValidationResultDocument,
                                        //FLUXResponseDocument / ResponseCode"
                                        //At least one occurrence if ResponseCode <> OK
                                        if (FAresp.FLUXResponseDocument.ResponseCode.Value != "OK")
                                        {
                                            if (FAresp.FLUXResponseDocument.RelatedValidationResultDocument == null)
                                            {
                                                //FA-L02-00-0368 - error
                                            }
                                            else
                                            {
                                                if (FAresp.FLUXResponseDocument.RelatedValidationResultDocument.Length < 1)
                                                {
                                                    //FA-L02-00-0368 - error
                                                }
                                            }

                                            //FA-L02-00-0554
                                            //"ValidationResultDocument/ValidationQualityAnalysis,
                                            //FLUXResponseDocument / ResponseCode"
                                            //At least one occurrence must be present if ResponseCode<> OK
                                            var rcasys = FAresp.FLUXResponseDocument.RelatedValidationResultDocument
                                                .Select(x => x.RelatedValidationQualityAnalysis).ToList();
                      
                                            if (rcasys != null && rcasys.Count() > 1)
                                            {
                                                foreach (var asysitem in rcasys)
                                                {
                                                    foreach (var asys in asysitem)
                                                    {
                                                        //FA-L00-00-0397
                                                        //ValidationQualityAnalysis/ID
                                                        //Check presence. Must be present.
                                                        if (asys.ID == null)
                                                        {
                                                            //FA-L00-00-0397 - error
                                                        }
                                                        else
                                                        {
                                                            //FA-L01-00-0398
                                                            //ValidationQualityAnalysis/ID
                                                            //Check schemeID. Must be FA_BR.
                                                            if (asys.ID.schemeID != "FA_BR")
                                                            {
                                                                //FA-L01-00-0398 - error
                                                            }

                                                            //FA-L01-00-0399
                                                            //ValidationQualityAnalysis/ID
                                                            //Check value. Code must be value of the specified code list in listID.
                                                            //TODO: check db FA_BR value
                                                        }
                                                        //FA-L02-00-0400
                                                        //ValidationQualityAnalysis/LevelCode
                                                        //Check presence. Must be present.
                                                        if (asys.LevelCode == null)
                                                        {
                                                            //FA-L02-00-0400 - error
                                                        }
                                                        else
                                                        {
                                                            //FA-L01-00-0401
                                                            //ValidationQualityAnalysis/LevelCode
                                                            //Check listID. Must be FLUX_GP_VALIDATION_LEVEL.
                                                            if (asys.LevelCode.listID != "FLUX_GP_VALIDATION_LEVEL")
                                                            {
                                                                //FA-L01-00-0401 - error
                                                            }

                                                            //FA-L01-00-0402
                                                            //ValidationQualityAnalysis/LevelCode
                                                            //Check Code. Must be in the list specified in listID.
                                                            //TODO: check db list FLUX_GP_VALIDATION_LEVEL = value exist
                                                        }
                                                        if (asys.TypeCode != null)
                                                        {
                                                            //FA-L01-00-0403
                                                            //ValidationQualityAnalysis/TypeCode
                                                            //Check listID. Must be FLUX_GP_VALIDATION_TYPE.
                                                            if (asys.TypeCode.listID != "FLUX_GP_VALIDATION_TYPE")
                                                            {
                                                                //FA-L01-00-0403 - error
                                                            }

                                                            //FA-L01-00-0406
                                                            //ValidationQualityAnalysis/TypeCode
                                                            //Check value of TypeCode. Must be in the list specified in listID
                                                            //TODO: check db FLUX_GP_VALIDATION_TYPE

                                                            //FA-L01-00-0405
                                                            //"ValidationQualityAnalysis/ReferencedItem, 
                                                            //ValidationQualityAnalysis / TypeCode"
                                                            //At least one non-empty occurrence if TypeCode is ERR or WAR
                                                            if (asys.TypeCode.Value == "ERR" || asys.TypeCode.Value == "WAR")
                                                            {
                                                                if (asys.ReferencedItem != null && asys.ReferencedItem.Length > 0)
                                                                {
                                                                }
                                                                else
                                                                {
                                                                    //FA-L01-00-0405 - warning
                                                                }
                                                            }
                                                        }
                                                        //FA-L00-00-0404
                                                        //ValidationQualityAnalysis/Result
                                                        //Must be non-empty
                                                        if (asys.Result == null)
                                                        {
                                                            //FA-L00-00-0404 - warning
                                                        }
                                                        else
                                                        {
                                                            if (asys.Result.Length < 1)
                                                            {
                                                                //FA-L00-00-0404 - warning
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FA-L02-00-0554 - error
                                            }
                                        }
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
                                        //FA-L01-00-0390
                                        //FLUXResponseDocument/CreationDateTime
                                        //Check Format. Must be according to the definition provided in 7.1(2).
                                        //TODO: Check db  - if decoded correctly its ok / the original value is in string

                                        //FA-L01-00-0391
                                        //FLUXResponseDocument/CreationDateTime
                                        //Date must be in the past.
                                        if (FAresp.FLUXResponseDocument.CreationDateTime.Item < DateTime.UtcNow)
                                        {
                                            //FA-L01-00-0391 - warning
                                        }
                                    }
                                    //FA-L00-00-0553
                                    //FLUXResponseDocument/RespondentFLUXParty
                                    //Check presence. Must be present
                                    if (FAresp.FLUXResponseDocument.RespondentFLUXParty != null)
                                    {

                                    }
                                    else
                                    {
                                        //FA-L00-00-0553 - error
                                    }

                                    //FA-L00-00-0392
                                    //RespondentFLUXParty/ID
                                    //Check presence. Must be present
                                    if (FAresp.FLUXResponseDocument.RespondentFLUXParty != null)
                                    {
                                        if (FAresp.FLUXResponseDocument.RespondentFLUXParty.ID == null)
                                        {
                                            //FA-L00-00-0392 - error
                                        }
                                        else
                                        {
                                            //FA-L01-00-0393
                                            //RespondentFLUXParty/ID
                                            //Check attribute schemeID. Must be FLUX_GP_PARTY
                                            var fpidschemeid = FAresp.FLUXResponseDocument.RespondentFLUXParty.ID.
                                                FirstOrDefault(x => x.schemeID == "FLUX_GP_PARTY");
                                            if (fpidschemeid == null)
                                            {
                                                //FA-L01-00-0393 - error
                                            }
                                            else
                                            {
                                                //FA-L03-00-0394
                                                //RespondentFLUXParty/ID
                                                //Check if RespondentFLUXParty/ID is consistent with FLUX TL values.
                                                //The party sending the response must be the same as the one from the FR value of the
                                                //FLUX TL envelope. Only the part before the first colon is to be considered:
                                                //Eg. ABC:something => only ABC refres to the party for the purpose of this rule
                                                //TODO: check value is equal to FR
                                                //fpidschemeid.Value 
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //FA-L00-00-0392 - error
                                    }

                                    if (FAresp.FLUXResponseDocument.RelatedValidationResultDocument != null)
                                    {
                                        foreach (var rvd in FAresp.FLUXResponseDocument.RelatedValidationResultDocument)
                                        {
                                            //FA-L00-00-0395
                                            //ValidationResultDocument/ValidatorID
                                            //Check presence. Must be present.
                                            if (rvd.ValidatorID == null)
                                            {
                                                //FA-L00-00-0395 - error
                                            } else
                                            {
                                                //FA-L01-00-0396
                                                //ValidationResultDocument/ValidatorID
                                                //Check schemeID. Must be FLUX_GP_PARTY
                                                if (rvd.ValidatorID.schemeID != "FLUX_GP_PARTY")
                                                {
                                                    //FA-L01-00-0396 - error
                                                }

                                                //FA-L01-00-0555
                                                //ValidationResultDocument/ValidatorID
                                                //Check value. Must be value from the code list specified in schemeID.
                                                //TODO: check db codelist - FLUX_GP_PARTY
                                            }
                                            
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }


            bool VesselDomainDebug = false;
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
                                string currentFluxVesselReportType = "";  //SUB, SUB-VCD, SUB-VED, SNAP-F, SNAP-L, SNAP-U

                                bool hasCfr = false;
                                bool hasRegNbr = false;
                                bool hasExtMark = false;
                                bool hasUvi = false;

                                bool hasIrcsBool = false;
                                bool hasIrcsValue = false;
                                decimal valueLOA = 0;


                                #region FLUXReportDocument
                                if (Vesselreport.FLUXReportDocument != null)
                                {
                                    //VESSEL-L00-00-0001, VESSEL-L00-00-0002, VESSEL-L00-00-0003, VESSEL-L01-02-0001 VESSEL-L01-02-0002, VESSEL-L01-02-0003
                                    #region FLUXReportDocument.ID 
                                    //VESSEL-L00-00-0001
                                    //FLUX_Report Document/Identification
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.ID?.Length > 0 && Vesselreport.FLUXReportDocument.ID.All(a => a.Value != null))
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0001 | OK | FLUXReportDocument.ID and FLUXReportDocument.ID.Value provided for all ID tags");
                                        foreach (var FLUXReportDocumentID in Vesselreport.FLUXReportDocument.ID)
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0002 | TODO | Check UUID is valid");
                                            //VESSEL-L00-00-0002
                                            //FLUX_Report Document/Identification
                                            //The identifier must be a valid UUID format
                                            //TODO: check UUID is valid

                                            System.Console.WriteLine("VESSEL-L00-00-0003 | TODO | Check UUID is unique");
                                            //VESSEL-L00-00-0003
                                            //FLUX_Report Document/Identification
                                            //The UUID is unique (he does not reference a report already received)
                                            //TODO: Check db
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0001 | REJECTED | No FLUXReportDocument.ID provided or no FLUXReportDocument.ID.Value in 1 or more ID tags");
                                        //VESSEL-L00-00-0001 - rejected
                                        SysList.Add("VESSEL-L00-00-0001");
                                    }

                                    if (Vesselreport.FLUXReportDocument.OwnerFLUXParty?.ID != null)
                                    {
                                        foreach (var ownerFLUXPartyID in Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID)
                                        {
                                            //VESSEL-L01-02-0001
                                            //Country of Registration
                                            //Mandatory value
                                            if (ownerFLUXPartyID.Value != null)
                                            {
                                                System.Console.WriteLine("VESSEL-L01-02-0001 | OK | FLUXReportDocument.OwnerFLUXParty.ID.Value provided");

                                                System.Console.WriteLine("VESSEL-L01-02-0002 | TODO | Check DB - TERRITORY");
                                                //VESSEL-L01-02-0002
                                                //Country of Registration
                                                //Code from the TERRITORY code list
                                                //TODO: Check DB nomenclature - Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID.Value is from TERRITORY list
                                                
                                                System.Console.WriteLine("VESSEL-L01-02-0003 | TODO | Check DB - MEMBER_STATE - Should be the same as the country sending the message");
                                                //VESSEL-L01-02-0003
                                                //Country of Registration
                                                //Should be the same as the country sending the message
                                                //TODO: check DB nomenclature - Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID.Value - Should be the same as the country sending the message
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L01-02-0001 | REJECTED | No FLUXReportDocument.OwnerFLUXParty.ID.Value provided");
                                                //VESSEL-L01-02-0001
                                                SysList.Add("VESSEL-L01-02-0001");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("No FLUXReportDocument.OwnerFLUXParty.ID provided");
                                    }
                                    #endregion

                                    //VESSEL-L00-00-0009, VESSEL-L00-00-0045, VESSEL-L00-00-0008
                                    #region FLUXReportDocument.TypeCode
                                    //VESSEL-L00-00-0009
                                    //FLUX_Report Document/Type
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.TypeCode?.Value != null)
                                    {
                                        currentFluxVesselReportType = Vesselreport.FLUXReportDocument.TypeCode.Value.ToString();
                                        System.Console.WriteLine("VESSEL-L00-00-0009 | OK | FLUXReportDocument.TypeCode provided with FLUXReportDocument.TypeCode.Value");
                                        //VESSEL-L00-00-0045
                                        //FLUX_Report Document/Type
                                        //ListId=FLUX_VESSEL_REPORT_TYPE
                                        if (Vesselreport.FLUXReportDocument.TypeCode.listID?.ToString() == "FLUX_VESSEL_REPORT_TYPE")
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0045 | OK | FLUXReportDocument.TypeCode.listID == FLUX_VESSEL_REPORT_TYPE");
                                            System.Console.WriteLine("VESSEL-L00-00-0008 | TODO | Check DB - FLUX_VESSEL_REPORT_TYPE");
                                            //VESSEL-L00-00-0008
                                            //FLUX_Report Document/Type
                                            //Code from the specified list
                                            //FLUX_VESSEL_REPORT_TYPE:
                                            //SUB
                                            //SUB - VCD
                                            //SUB - VED
                                            //SNAP - F
                                            //SNAP - L
                                            //SNAP - U
                                            //TODO: Check DB nomenclature - FLUX_VESSEL_REPORT_TYPE for Vesselreport.FLUXReportDocument.TypeCode.Value
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0045 | REJECTED | No FLUXReportDocument.TypeCode.listID provided or FLUXReportDocument.TypeCode.listID != FLUX_VESSEL_REPORT_TYPE");
                                            //VESSEL-L00-00-0045 - rejected
                                            SysList.Add("VESSEL-L00-00-0045");
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0009 | REJECTED | No FLUXReportDocument.TypeCode or no FLUXReportDocument.TypeCode.Value provided");
                                        //VESSEL-L00-00-0009 - rejected
                                        SysList.Add("VESSEL-L00-00-0009");
                                    }
                                    #endregion

                                    //VESSEL-L00-00-0006, VESSEL-L00-00-0007, VESSEL-L00-00-0093
                                    #region FLUXReportDocument.CreationDateTime
                                    //VESSEL-L00-00-0006
                                    //FLUX_Report Document/Creation
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.CreationDateTime != null)
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0006 | OK | FLUXReportDocument.CreationDateTime provided");
                                        //VESSEL-L00-00-0007
                                        //FLUX_Report Document/Creation
                                        //Datetime format
                                        if (Vesselreport.FLUXReportDocument.CreationDateTime.Item != DateTime.MinValue)
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0007 | OK | FLUXReportDocument.CreationDateTime.Item (DateTime) provided with !default value");
                                            //VESSEL-L00-00-0093
                                            //FLUX_Report Document/Creation
                                            //Creation date not in the future
                                            DateTime currentUtcDateTime = DateTime.UtcNow;
                                            if (Vesselreport.FLUXReportDocument.CreationDateTime.Item <= currentUtcDateTime)
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0093 | OK | FLUXReportDocument.CreationDateTime.Item is not in the future");
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0093 | REJECTED | FLUXReportDocument.CreationDateTime.Item is in the future");
                                                //VESSEL-L00-00-0093 - rejected
                                                SysList.Add("VESSEL-L00-00-0093");
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0007 | REJECTED | No FLUXReportDocument.CreationDateTime.Item (DateTime) provided or it has default value");
                                            //VESSEL-L00-00-0007 - rejected
                                            SysList.Add("VESSEL-L00-00-0007");
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0006 | REJECTED | No FLUXReportDocument.CreationDateTime provided");
                                        //VESSEL-L00-00-0006 - rejected
                                        SysList.Add("VESSEL-L00-00-0006");
                                    }
                                    #endregion

                                    //VESSEL-L00-00-0011, VESSEL-L00-00-0046, VESSEL-L00-00-0010
                                    #region FLUXReportDocument.PurposeCode
                                    //VESSEL-L00-00-0011
                                    //FLUX_Report Document/Purpose
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.PurposeCode?.Value != null)
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0011 | OK | FLUXReportDocument.PurposeCode provided with FLUXReportDocument.PurposeCode.Value");
                                        //VESSEL-L00-00-0046
                                        //FLUX_Report Document/Purpose
                                        //ListId= FLUX_GP_PURPOSE
                                        if (Vesselreport.FLUXReportDocument.PurposeCode.listID?.ToString() == "FLUX_GP_PURPOSE")
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0046 | OK | FLUXReportDocument.PurposeCode.listID == FLUX_GP_PURPOSE");
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0046 | REJECTED | No FLUXReportDocument.PurposeCode.listID provided or FLUXReportDocument.PurposeCode.listID != FLUX_GP_PURPOSE");
                                            //VESSEL-L00-00-0046 - rejected
                                        }

                                        //VESSEL-L00-00-0010
                                        //FLUX_Report Document/Purpose
                                        //Value=9
                                        if (Vesselreport.FLUXReportDocument.PurposeCode.Value.ToString() == "9")
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0010 | OK | FLUXReportDocument.PurposeCode.Value == 9");
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0010 | REJECTED | FLUXReportDocument.PurposeCode.Value != 9");
                                            //VESSEL-L00-00-0010 - rejected
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0011 | REJECTED | No FLUXReportDocument.PurposeCode or no FLUXReportDocument.PurposeCode.Value provided");
                                        //VESSEL-L00-00-0011 - rejected
                                    }
                                    #endregion

                                    //VESSEL-L00-00-0013, VESSEL-L00-00-0047, VESSEL-L00-00-0012
                                    #region FLUXReportDocument.OwnerFLUXParty
                                    //VESSEL-L00-00-0013
                                    //FLUX_Party/Identification
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.OwnerFLUXParty?.ID?.Length > 0 && Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID.All(a => a.Value != null))
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0013 | OK | FLUXReportDocument.OwnerFLUXParty.ID with FLUXReportDocument.OwnerFLUXParty.ID.Value provided");
                                        foreach (var ownerFluxPartyId in Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID)
                                        {
                                            //VESSEL-L00-00-0047
                                            //FLUX_Party/Identification
                                            //SchemeId=FLUX_GP_PARTY 
                                            if (ownerFluxPartyId.schemeID?.ToString() == "FLUX_GP_PARTY")
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0047 | OK | FLUXReportDocument.OwnerFLUXParty.ID.schemeID == FLUX_GP_PARTY");
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0047 | REJECTED | No FLUXReportDocument.OwnerFLUXParty.ID.schemeID provided or FLUXReportDocument.OwnerFLUXParty.ID.schemeID != FLUX_GP_PARTY");
                                                //VESSEL-L00-00-0047 - rejected
                                            }

                                            System.Console.WriteLine("VESSEL-L00-00-0012 | TODO | Check DB - FLUX_GP_PARTY");
                                            //VESSEL-L00-00-0012
                                            //FLUX_Party/Identification
                                            //Code from the specified list
                                            //TODO: Check DB nomenclature - FLUX_GP_PARTY for Vesselreport.FLUXReportDocument.TypeCode.Value
                                        }

                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0013 | REJECTED | No FLUXReportDocument.OwnerFLUXParty or FLUXReportDocument.OwnerFLUXParty.ID with FLUXReportDocument.OwnerFLUXParty.ID.Value provided");
                                        //VESSEL-L00-00-0013 - rejected
                                    }
                                    #endregion
                                }
                                else
                                {
                                    System.Console.WriteLine("No FLUXReportDocument provided");
                                }
                                #endregion

                                #region VesselEvent
                                //VESSEL-L01-02-0041, VESSEL-L01-01-0008
                                //Event
                                //Mandatory value
                                if (Vesselreport.VesselEvent?.Length > 0)
                                {
                                    foreach (var vesselEvent in Vesselreport.VesselEvent)
                                    {
                                        //VESSEL-L00-00-0048, VESSEL-L01-01-0009, VESSEL-L00-00-0150
                                        #region VesselEvent.TypeCode
                                        if (vesselEvent.TypeCode?.Value != null)
                                        {
                                            System.Console.WriteLine("VESSEL-L01-01-0008 | OK | VesselEvent.TypeCode.Value provided");
                                            //VESSEL-L00-00-0048
                                            //Vessel_Event/Type
                                            //ListId=VESSEL_EVENT
                                            if (vesselEvent.TypeCode.listID?.ToString() == "VESSEL_EVENT")
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0048 | OK | VesselEvent.TypeCode.listID == VESSEL_EVENT");

                                                System.Console.WriteLine("VESSEL-L01-01-0009 | TODO | check DB - VESSEL_EVENT");
                                                //VESSEL-L01-01-0009
                                                //Event
                                                //Code from the specified list
                                                //TODO: check DB nomenclature - VESSEL_EVENT if Vesselreport.VesselEvent.TypeCode.Value exists
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0048 | REJECTED | No VesselEvent.TypeCode.listID provided or VesselEvent.TypeCode.listID != VESSEL_EVENT");
                                                //VESSEL-L00-00-0048 - rejected
                                            }

                                            //VESSEL-L00-00-0150
                                            //Vessel_Event/Type
                                            //Type code = MOD
                                            if (vesselEvent.TypeCode.Value.ToString() == "MOD")
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0150 | OK | VesselEvent.TypeCode.Value provided and == MOD");
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0150 | REJECTED | No VesselEvent.TypeCode.Value provided or != MOD");
                                                //VESSEL-L00-00-0150 - rejected
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("No VesselEvent.TypeCode or VesselEvent.TypeCode.Value provided");
                                        }
                                        #endregion

                                        //VESSEL-L01-01-0011, VESSEL-L01-02-0043, VESSEL-L00-00-0068, VESSEL-L01-02-0044
                                        #region VesselEvent.OccurrenceDateTime
                                        //VESSEL-L01-01-0011, VESSEL-L01-02-0043
                                        //Event Date
                                        //Mandatory value
                                        if (vesselEvent.OccurrenceDateTime?.Item != null)
                                        {
                                            System.Console.WriteLine("VESSEL-L01-01-0011 | OK | VesselEvent.OccurrenceDateTime with VesselEvent.OccurrenceDateTime.Item provided ");
                                            System.Console.WriteLine("VESSEL-L01-02-0043 | OK | VesselEvent.OccurrenceDateTime with VesselEvent.OccurrenceDateTime.Item provided ");

                                            //VESSEL-L00-00-0068
                                            //Vessel_Event/Occurrence
                                            //Datetime format
                                            if (vesselEvent.OccurrenceDateTime.Item != DateTime.MinValue)
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0068 | OK | VesselEvent.OccurrenceDateTime.Item provided with !default DateTime");

                                                //VESSEL-L01-02-0044
                                                //Event Date
                                                //Not in the future
                                                DateTime currentUtcDateTime = DateTime.UtcNow;
                                                if (vesselEvent.OccurrenceDateTime.Item <= currentUtcDateTime)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-02-0044 | OK | VesselEvent.OccurrenceDateTime.Item is not in the future");
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-02-0044 | REJECTED | VesselEvent.OccurrenceDateTime.Item is in the future");
                                                    //VESSEL-L01-02-0044 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0068 | REJECTED | No VesselEvent.OccurrenceDateTime.Item provided or default DateTime value");
                                                //VESSEL-L00-00-0068 - rejected
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L01-01-0011 | REJECTED | No VesselEvent.OccurrenceDateTime or VesselEvent.OccurrenceDateTime.Item provided");
                                            System.Console.WriteLine("VESSEL-L01-02-0043 | REJECTED | No VesselEvent.OccurrenceDateTime or VesselEvent.OccurrenceDateTime.Item provided");
                                            //VESSEL-L01-01-0011 - rejected
                                            //VESSEL-L01-02-0043 - rejected
                                        }
                                        #endregion

                                        #region VesselEvent.RelatedVesselTransportMeans
                                        if (vesselEvent.RelatedVesselTransportMeans != null)
                                        {
                                            //VESSEL-L00-00-0027, VESSEL-L01-01-0004, VESSEL-L01-01-0005, VESSEL-L01-01-0006, VESSEL-L01-01-0007, VESSEL-L01-01-0014, VESSEL-L01-01-0015,
                                            //VESSEL-L01-01-0016, VESSEL-L01-02-0009, VESSEL-L01-01-0017, VESSEL-L00-00-0069, VESSEL-L01-01-0108, VESSEL-L01-02-0005, VESSEL-L00-00-0070,
                                            //VESSEL-L01-01-0110, VESSEL-L01-00-0507, VESSEL-L01-01-0024, VESSEL-L01-02-0006, VESSEL-L01-00-0706, VESSEL-L01-00-0635
                                            #region VesselEvent.RelatedVesselTransportMeans.ID
                                            if (vesselEvent.RelatedVesselTransportMeans.ID?.Length > 0)
                                            {
                                                foreach (var relatedVesselTransportMeansId in vesselEvent.RelatedVesselTransportMeans.ID)
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0027 | TODO | Check DB - FLUX_VESSEL_ID_TYPE");
                                                    //VESSEL-L00-00-0027
                                                    //Vessel_Transport_Means/Identification
                                                    //SchemeId=Code from the FLUX_VESSEL_ID_TYPE list
                                                    //TODO: Check DB nomenclature - FLUX_VESSEL_ID_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.ID.schemeID
                                                    bool relatedTransportMeansIdSchemeIdInDb = true;
                                                    if (relatedTransportMeansIdSchemeIdInDb)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0027 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId in DB FLUX_VESSEL_ID_TYPE list");


                                                        //VESSEL-L01-01-0005
                                                        //CFR
                                                        //Mandatory value
                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "CFR" && relatedVesselTransportMeansId.Value?.Length > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0005 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == CFR and VesselEvent.RelatedVesselTransportMeans.ID.Value provided");
                                                            hasCfr = true;
                                                            //VESSEL-L01-01-0004
                                                            //CFR
                                                            //Length = 12 characters
                                                            if (relatedVesselTransportMeansId.Value.Length == 12)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0004 | OK | schemeID == CFR: VesselEvent.RelatedVesselTransportMeans.ID.Value.Length == 12");
                                                                //VESSEL-L01-01-0007
                                                                //CFR
                                                                //Should contain only A-Z and 0-9 characters
                                                                //#Q Regex used for start of string, 3 capital letters, followed by 9 numbers from 0 to 9 and end of string
                                                                if (Regex.Match(relatedVesselTransportMeansId.Value.ToString(), "^[A-Z]{3}[0-9]{9}$").Success)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0007 | OK | schemeID == CFR: VesselEvent.RelatedVesselTransportMeans.ID.Value match Regex");

                                                                    //VESSEL-L01-01-0006
                                                                    //CFR
                                                                    //The 3 first characters should be an ISO-3 code of a declaring country, excepted for Romania where the code in CFR is "ROM" and not "ROU".
                                                                    //Code from a list of reference: "MEMBER_STATE" code list
                                                                    //TODO: check DB nomenclature - MEMBER_STATE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.ID[schemaID=CFR].Value exists and is passed validation
                                                                    //#Q firstThreeCharactersOfCFR should be present in MEMBER_STATE list

                                                                    string firstThreeCharsOfCfr = relatedVesselTransportMeansId.Value.ToString().Substring(0, 3);
                                                                    //#Q If CFR starts with ROM, change the country code to ROU to search in MEMBER_STATE list
                                                                    if (firstThreeCharsOfCfr == "ROM")
                                                                    {
                                                                        firstThreeCharsOfCfr = "ROU";
                                                                    }

                                                                    System.Console.WriteLine("VESSEL-L01-01-0006 | TODO | check DB - MEMBER_STATE");

                                                                    bool firstThreeCharsOfCfrInDb = true;
                                                                    if (firstThreeCharsOfCfrInDb)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0006 | OK | schemeID == CFR: VesselEvent.RelatedVesselTransportMeans.ID.Value substring(3) in DB MEMBER_STATE list");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0006 | REJECTED | schemeID == CFR: VesselEvent.RelatedVesselTransportMeans.ID.Value substring(3) not in DB MEMBER_STATE list");
                                                                        //VESSEL-L01-01-0006 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0007 | REJECTED | schemeID == CFR: VesselEvent.RelatedVesselTransportMeans.ID.Value do not match Regex");
                                                                    //VESSEL-L01-01-0006 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0004 | REJECTED | schemeID == CFR:  VesselEvent.RelatedVesselTransportMeans.ID.Value.Length != 12");
                                                                //VESSEL-L01-01-0004 - rejected
                                                            }
                                                        }

                                                        //VESSEL-L01-01-0015
                                                        //Registration Number
                                                        //Should be provided
                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "REG_NBR" && relatedVesselTransportMeansId.Value?.Length > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0015 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == REG_NBR and VesselEvent.RelatedVesselTransportMeans.ID.Value provided");
                                                            hasRegNbr = true;
                                                            //VESSEL-L01-01-0014
                                                            //Registration Number
                                                            //Length <=14 characters max
                                                            if (relatedVesselTransportMeansId.Value.Length <= 14)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0014 | OK | VesselEvent.RelatedVesselTransportMeans.ID.Value <= 14");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0014 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.Value > 14");
                                                                //VESSEL-L01-01-0014 - rejected
                                                            }
                                                        }

                                                        //VESSEL-L01-01-0017
                                                        //External Marking
                                                        //Should be provided
                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "EXT_MARK" && relatedVesselTransportMeansId.Value?.Length > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0017 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == EXT_MARK and VesselEvent.RelatedVesselTransportMeans.ID.Value provided");
                                                            hasExtMark = true;
                                                            //VESSEL-L01-01-0016, VESSEL-L01-02-0009
                                                            //External Marking
                                                            //Length <=14 characters max
                                                            if (relatedVesselTransportMeansId.Value.Length <= 14)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0016 | OK | VesselEvent.RelatedVesselTransportMeans.ID.Value <= 14");
                                                                System.Console.WriteLine("VESSEL-L01-02-0009 | OK | VesselEvent.RelatedVesselTransportMeans.ID.Value <= 14");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0016 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.Value > 14");
                                                                System.Console.WriteLine("VESSEL-L01-02-0009 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.Value > 14");
                                                                //VESSEL-L01-01-0016 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "UVI" && relatedVesselTransportMeansId.Value != null)
                                                        {
                                                            hasUvi = true;
                                                            //VESSEL-L00-00-0069
                                                            //Vessel_Transport_Means/Identification & SchemeID=UVI
                                                            //Numerical value
                                                            if (int.TryParse(relatedVesselTransportMeansId.Value.ToString(), out _))
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0069 | OK | VesselEvent.RelatedVesselTransportMeans.ID.Value UVI is numeric");

                                                                //VESSEL-L01-01-0108, VESSEL-L01-02-0005
                                                                //UVI
                                                                //Length = 7 digits
                                                                if (relatedVesselTransportMeansId.Value.Length == 7)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0108 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == UVI with value provided and length == 7");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0005 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == UVI with value provided and length == 7");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0108 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == UVI with value provided or length != 7");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0005 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == UVI with value provided or length != 7");
                                                                    //VESSEL-L01-01-0108 - rejected
                                                                    //VESSEL-L01-02-0005 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0069 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.Value UVI is not numeric");
                                                                //VESSEL-L00-00-0069 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "MMSI" && relatedVesselTransportMeansId.Value != null)
                                                        {
                                                            //VESSEL-L00-00-0070
                                                            //Vessel_Transport_Means/Identifi cation & SchemeID=MMSI
                                                            //Numerical value
                                                            if (int.TryParse(relatedVesselTransportMeansId.Value.ToString(), out _))
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0070 | OK | VesselEvent.RelatedVesselTransportMeans.ID.Value MMSI is numeric");

                                                                //VESSEL-L01-01-0110
                                                                //MMSI
                                                                //Length = 7 digits
                                                                if (relatedVesselTransportMeansId.Value.Length == 7)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0110 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == MMSI with value provided and length == 9");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0110 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == MMSI with value provided or length != 9");
                                                                    //VESSEL-L01-01-0110 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0070 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.Value MMSI is not numeric");
                                                                //VESSEL-L00-00-0070 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "FFA" && relatedVesselTransportMeansId.Value?.Length > 0)
                                                        {
                                                            //VESSEL-L01-00-0507
                                                            //FFA vessel ID
                                                            //Length <= 12 characters max
                                                            if (relatedVesselTransportMeansId.Value?.Length <= 12)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0507 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == FFA with value provided and length <= 12");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0507 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == FFA with value provided or length > 12");
                                                                //VESSEL-L01-00-0507 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() == "IRCS")
                                                        {
                                                            if (relatedVesselTransportMeansId.Value?.Length > 0)
                                                            {
                                                                hasIrcsValue = true;
                                                                //VESSEL-L01-01-0024, VESSEL-L01-02-0006
                                                                //IRCS
                                                                //Length <= 7 characters max
                                                                if (relatedVesselTransportMeansId.Value.Length <= 7)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0024 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueIndicator.Item for IRCS provided and length <= 7");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0006 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueIndicator.Item for IRCS provided and length <= 7");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0024 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueIndicator.Item for IRCS provided or length > 7");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0006 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueIndicator.Item for IRCS provided or length > 7");
                                                                    //VESSEL-L01-01-0024 - rejected
                                                                    //VESSEL-L01-02-0006 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                hasIrcsValue = false;
                                                            }
                                                        }

                                                        //VESSEL-L01-00-0706
                                                        //Third Party Vessel identification
                                                        //Code from the specified list
                                                        //#Q Add SchemeID = code from the RFMO list on MDR check
                                                        if (relatedVesselTransportMeansId.schemeID?.ToString() != null && relatedVesselTransportMeansId.Value?.Length > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0706 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId is a code from RFMO list on MDR with value provided and length <= 13");

                                                            //VESSEL-L01-00-0635
                                                            //Third Party Vessel identification
                                                            //Length <= 12 characters max
                                                            //+) Modified: L01-00-0635 updated to allow 13 chars for the third party vessel ID. - 09/06/2022
                                                            if (relatedVesselTransportMeansId.Value?.Length <= 13)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0635 | OK | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == FFA with value provided and length <= 13");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0635 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == FFA with value provided or length > 13");
                                                                //VESSEL-L01-00-0635 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0027 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ID.SchemeId not in DB FLUX_VESSEL_ID_TYPE list ");
                                                        //VESSEL-L00-00-0027 - rejected
                                                    }
                                                }

                                                if (!hasCfr)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0005 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == CFR present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                                }
                                                if (!hasRegNbr)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0015 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == REG_NBR present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                                }
                                                if (!hasExtMark)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0017 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == EXT_MARK present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ID tag provided");
                                                //VESSEL-L01-01-0005 - rejected
                                                System.Console.WriteLine("VESSEL-L01-01-0005 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == CFR present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                                //VESSEL-L01-01-0015 - rejected
                                                System.Console.WriteLine("VESSEL-L01-01-0015 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == REG_NBR present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                                //VESSEL-L01-01-0017 - rejected
                                                System.Console.WriteLine("VESSEL-L01-01-0017 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ID.SchemeId == EXT_MARK present or with no VesselEvent.RelatedVesselTransportMeans.ID.Value");
                                            }
                                            #endregion

                                            //VESSEL-L01-01-0019, VESSEL-L01-01-0018, VESSEL-L01-02-0011
                                            #region VesselEvent.RelatedVesselTransportMeans.Name
                                            if (vesselEvent.RelatedVesselTransportMeans?.Name != null)
                                            {
                                                foreach (var relatedVesselTransportMeansName in vesselEvent.RelatedVesselTransportMeans.Name)
                                                {
                                                    //VESSEL-L01-01-0019
                                                    //Vessel Name
                                                    //Should be provided
                                                    if (relatedVesselTransportMeansName.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-01-0019 | OK | VesselEvent.RelatedVesselTransportMeans.Name.Value provided");
                                                        //VESSEL-L01-01-0018, VESSEL-L01-02-0011
                                                        //External Marking
                                                        //Length <= 40 characters max
                                                        if (relatedVesselTransportMeansName.Value.Length <= 40)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0018 | OK | VesselEvent.RelatedVesselTransportMeans.Name.Value.Length <= 40");
                                                            System.Console.WriteLine("VESSEL-L01-02-0011 | OK | VesselEvent.RelatedVesselTransportMeans.Name.Value.Length <= 40");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0018 | REJECTED | VesselEvent.RelatedVesselTransportMeans.Name.Value.Length > 40");
                                                            System.Console.WriteLine("VESSEL-L01-02-0011 | REJECTED | VesselEvent.RelatedVesselTransportMeans.Name.Value.Length > 40");
                                                            //VESSEL-L01-01-0018 - rejected
                                                            //VESSEL-L01-02-0011 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-01-0019 | WARNING | No VesselEvent.RelatedVesselTransportMeans.Name.Value provided");
                                                        //VESSEL-L01-01-0019 - warning
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.Name tag provided");
                                            }
                                            #endregion

                                            //VESSEL-L01-00-0502, VESSEL-L01-00-0636
                                            #region VesselEvent.RelatedVesselTransportMeans.SpeedMeasure
                                            if (vesselEvent.RelatedVesselTransportMeans?.SpeedMeasure != null)
                                            {
                                                //VESSEL-L01-00-0502
                                                //Vessel speed
                                                //no negative value
                                                if (vesselEvent.RelatedVesselTransportMeans?.SpeedMeasure.Value > 0)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-00-0502 | OK | VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.Value provided and positive");

                                                    //VESSEL-L01-00-0636
                                                    //Vessel speed
                                                    //Format: XXXXX.YY with 2 optional decimals
                                                    //#Q Format validation to be added
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpeedMeasure.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0636 | OK | VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.Value format XXXXX.YY valid");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0636 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.Value format XXXXX.YY not valid");
                                                        //VESSEL-L01-00-0636 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0019 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.Value provided or is negative");
                                                    //VESSEL-L01-00-0502 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpeedMeasure tag provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0072, VESSEL-L00-00-0095, VESSEL-L01-00-0505, VESSEL-L01-00-0637
                                            #region VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure
                                            //VESSEL-L00-00-0072
                                            //Vessel_Transport_Means/Speed
                                            //Numerical value
                                            //#Q Already returns a decimal, the check is if != null
                                            if (vesselEvent.RelatedVesselTransportMeans?.TrawlingSpeedMeasure != null)
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0072 | OK | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value (decimal) provided");

                                                //VESSEL-L00-00-0095
                                                //Vessel_Transport_Means/Trawling speed
                                                //For speed, the default unit must be KNT
                                                if (vesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.unitCode?.ToString() == "KNT")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0095 | OK | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.unitCode provided and == KNT");
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0095 | REJECTED | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.unitCode provided or != KNT");
                                                    //VESSEL-L00-00-0095 - rejected
                                                }

                                                //VESSEL-L01-00-0505
                                                //Trawling speed
                                                //no negative value
                                                if (vesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value > 0)
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-00-0505 | OK | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value provided and positive");

                                                    //VESSEL-L01-00-0637
                                                    //Trawling speed
                                                    //Format: XXXXX.YY with 2 optional decimals
                                                    //#Q Format validation to be added
                                                    if (vesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0637 | OK | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value format XXXXX.YY valid");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0637 | REJECTED | VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value format XXXXX.YY not valid");
                                                        //VESSEL-L01-00-0637 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-00-0505 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value provided or is negative");
                                                    //VESSEL-L01-00-0505 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0072 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.TrawlingSpeedMeasure.Value (decimal) provided");
                                                //VESSEL-L00-00-0072 - rejected
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0146, VESSEL-L00-00-0050, VESSEL-L00-00-0025
                                            #region VesselEvent.RelatedVesselTransportMeans.TypeCode
                                            if (vesselEvent.RelatedVesselTransportMeans.TypeCode != null)
                                            {
                                                foreach (var relatedVesselTransportMeansTypeCode in vesselEvent.RelatedVesselTransportMeans.TypeCode)
                                                {
                                                    //VESSEL-L00-00-0146
                                                    //Vessel_ Transport_ Means / Type
                                                    //Mandatory value
                                                    if (relatedVesselTransportMeansTypeCode.Value?.ToString() != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0146 | OK | VesselEvent.RelatedVesselTransportMeans.TypeCode provided");
                                                        //VESSEL-L00-00-0050
                                                        //Vessel_Transport_Means / Type
                                                        //ListId= VESSEL_TYPE
                                                        if (relatedVesselTransportMeansTypeCode.listID?.ToString() == "VESSEL_TYPE")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0050 | OK | VesselEvent.RelatedVesselTransportMeans.TypeCode.ListId == VESSEL_TYPE");
                                                            System.Console.WriteLine("VESSEL-L00-00-0025 | TODO | Check DB - VESSEL_TYPE");
                                                            //VESSEL-L00-00-0025
                                                            //Vessel_ Transport_ Means / Type
                                                            //Code from the specified list
                                                            //TODO: Check DB nomenclature - VESSEL_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.TypeCode.Value
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0050 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.TypeCode.ListId provided or != VESSEL_TYPE");
                                                            //VESSEL-L00-00-0050 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0146 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.TypeCode provided or with no Value");
                                                        //VESSEL-L00-00-0146 - rejected
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.TypeCode tag provided");
                                                System.Console.WriteLine("VESSEL-L00-00-0146 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.TypeCode provided");
                                                //VESSEL-L00-00-0146 - rejected
                                            }
                                            #endregion

                                            //VESSEL-L01-01-0001, VESSEL-L00-00-0145, VESSEL-L01-01-0002, VESSEL-L01-01-0003
                                            #region vesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry
                                            //VESSEL-L01-01-0001
                                            //Country of Registration
                                            //Mandatory value
                                            if (vesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry?.ID?.Value != null)
                                            {
                                                System.Console.WriteLine("VESSEL-L01-01-0001 | OK | VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry provided with ID.Value");

                                                //VESSEL-L00-00-0145
                                                //Vessel_Country/Identification
                                                //SchemeId=TERRITORY
                                                if (vesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID?.ToString() == "TERRITORY")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0145 | OK | VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountryID.schemeID == TERRITORY");

                                                    System.Console.WriteLine("VESSEL-L01-01-0002 | TODO | Check DB - MEMBER_STATE");
                                                    //VESSEL-L01-01-0002
                                                    //Country of Registration
                                                    //Code from the "MEMBER_STATE" code list
                                                    //TODO: check DB nomenclature - MEMBER_STATE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value exists

                                                    System.Console.WriteLine("VESSEL-L01-01-0002 | TODO | Check DB - MEMBER_STATE - Should be the same as the country sending the message");
                                                    //VESSEL-L01-01-0003
                                                    //Country of Registration
                                                    //Should be the same as the country sending the message
                                                    //TODO: check DB nomenclature - Vesselreport.VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value Should be the same as the country sending the message
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0145 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountryID.schemeID provided or != TERRITORY");
                                                    //VESSEL-L00-00-0145 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L01-01-0001 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry provided or with no ID or ID.Value");
                                                //VESSEL-L01-01-0001 - rejected
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0051, VESSEL-L00-00-0026, VESSEL-L00-00-0053, VESSEL-L00-00-0052, VESSEL-L01-01-0021, VESSEL-L01-00-0638
                                            #region VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent != null)
                                            {
                                                foreach (var relatedVesselTransportMeansSpecifiedRegistrationEvent in vesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent)
                                                {
                                                    if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation != null)
                                                    {
                                                        //VESSEL-L00-00-0051
                                                        //Registration_ Location /Type
                                                        //ListId= FLUX_VESSEL_REGSTR_TYPE
                                                        if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.TypeCode?.listID?.ToString() == "FLUX_VESSEL_REGSTR_TYPE")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0051 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.TypeCode.listID provided and == FLUX_VESSEL_REGSTR_TYPE");

                                                            System.Console.WriteLine("VESSEL-L00-00-0026 | TODO | Check DB - FLUX_VESSEL_REGSTR_TYPE");
                                                            //VESSEL-L00-00-0026
                                                            //Registration_ Location /Type
                                                            //Code from the specified list
                                                            //TODO: Check DB nomenclature - FLUX_VESSEL_REGSTR_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.TypeCode.Value
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0051 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.TypeCode.listID provided or != FLUX_VESSEL_REGSTR_TYPE");
                                                            //VESSEL-L00-00-0051 - rejected
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.ID?.Length > 0)
                                                        {
                                                            foreach (var relatedConstructionLocationId in relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.ID)
                                                            {
                                                                //VESSEL-L00-00-0053
                                                                //Registration_Location/Identification
                                                                //SchemeId= VESSEL_PORT
                                                                if (relatedConstructionLocationId.schemeID?.ToString() == "VESSEL_PORT")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0053 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.ID.schemeID provided and == VESSEL_PORT");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0053 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.ID.schemeID provided or != VESSEL_PORT");
                                                                    //VESSEL-L00-00-0053 - rejected
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.ID provided");
                                                        }

                                                        //VESSEL-L00-00-0052
                                                        //Registration_Location/Country
                                                        //ListId= TERRITORY
                                                        //#Q Cannot find listId in CountryID, but it has schemeID?
                                                        if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.CountryID?.schemeID?.ToString() == "TERRITORY")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0052 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.CountryID.schemeID provided and == TERRITORY");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0052 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.CountryID.schemeID provided or != TERRITORY");
                                                            //VESSEL-L00-00-0052 - rejected
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.Name != null)
                                                        {
                                                            foreach (var relatedRegistrationLocationName in relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.Name)
                                                            {
                                                                //VESSEL-L01-01-0021
                                                                //Place of Registration
                                                                //Should be provided
                                                                if (relatedRegistrationLocationName.Value != null)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0021 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.Name.Value provided");

                                                                    //VESSEL-L01-00-0638
                                                                    //Place of registration
                                                                    //Length <= 80 characters max
                                                                    if (relatedRegistrationLocationName.Value.Length <= 80)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0638 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.Name.Value provided and length <= 80");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0638 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.Name.Value provided or length > 80");
                                                                        //VESSEL-L01-00-0638 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0021 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.Name.Value provided");
                                                                    //VESSEL-L01-01-0021 - rejected
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation.Name tag provided");
                                                        }

                                                        //#Q What is the place of registration? PhysicalStructuredAddress?
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent.RelatedRegistrationLocation provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0074, VESSEL-L01-01-0066, VESSEL-L00-00-0054, VESSEL-L00-00-0034, VESSEL-L00-00-0055, VESSEL-L01-00-0644
                                            #region VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent != null)
                                            {
                                                //VESSEL-L00-00-0074
                                                //Construction_Event/Occurrence
                                                //Date type
                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime != null)
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0074 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item (DateTime) provided");

                                                    //VESSEL-L01-01-0066
                                                    //YoC
                                                    //>= lower limit : 1850 (Parameter YEAR_LOW from the VESSEL_BR_PARAMETER code list)
                                                    //#Q Refactor DadeTime check
                                                    //if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item >= YEAR_LOW)
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item >= DateTime.Parse("Jan 1, 1850"))
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-01-0066 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item (DateTime) >= YEAR_LOW (1850)");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-01-0066 | ERROR | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item (DateTime) not >= YEAR_LOW (1850)");
                                                        //VESSEL-L01-01-0066 - error
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0074 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime.Item (DateTime) provided");
                                                    //VESSEL-L00-00-0074 - rejected
                                                }

                                                //VESSEL-L00-00-0054
                                                //Construction_ Location /Type
                                                //ListId= FLUX_VESSEL_CONSTR_TYPE
                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation?.TypeCode?.listID?.ToString() == "FLUX_VESSEL_CONSTR_TYPE")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0054 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.TypeCode.listID provided and == FLUX_VESSEL_CONSTR_TYPE");

                                                    System.Console.WriteLine("VESSEL-L00-00-0054 | OK | Check DB - FLUX_VESSEL_CONSTR_TYPE");
                                                    //VESSEL-L00-00-0034
                                                    //Construction_ Location /Type
                                                    //Code from the specified list
                                                    //TODO: Check DB nomenclature FLUX_VESSEL_CONSTR_TYPE for VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.TypeCode.Value
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0054 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.TypeCode.listID provided or != FLUX_VESSEL_CONSTR_TYPE");
                                                    //VESSEL-L00-00-0054 - rejected
                                                }

                                                //VESSEL-L00-00-0055
                                                //Construction_Location/Country
                                                //ListId= TERRITORY
                                                //#Q Cannot find listID in CountryID, but it has schemeID?
                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation?.CountryID?.schemeID?.ToString() == "TERRITORY")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0055 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.CountryID.schemeID provided and == TERRITORY");

                                                    System.Console.WriteLine("VESSEL-L01-00-0644 | TODO | Check DB - TERRITORY");
                                                    //VESSEL-L01-00-0644
                                                    //Place of construction
                                                    //Code from the specified list
                                                    //TODO: Check DB nomenclature - vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.CountryID.Value in TERRITORY list
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0055 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.RelatedConstructionLocation.CountryID.schemeID provided or != TERRITORY");
                                                    //VESSEL-L00-00-0055 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent provided");
                                            }
                                            #endregion

                                            //VESSEL-L01-01-0051, VESSEL-L01-01-0050, VESSEL-L01-02-0039, VESSEL-L01-01-0052, VESSEL-L01-02-0040, VESSEL-L01-01-0055, VESSEL-L01-01-0054,
                                            //VESSEL-L01-01-0056, VESSEL-L00-00-0056, VESSEL-L00-00-0033, VESSEL-L00-00-0041, VESSEL-L00-00-0075, VESSEL-L00-00-0098, VESSEL-L01-00-0520,
                                            //VESSEL-L01-00-0521, VESSEL-L01-00-0522
                                            #region VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine
                                            if (vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine != null)
                                            {
                                                //VESSEL-L01-01-0051
                                                //Main Power
                                                //Should be provided
                                                if (vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Any(a => a.RoleCode?.Value == "MAIN"))
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0051 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.Value == MAIN");

                                                    foreach (var relatedVesselTransportMeansAttachedVesselEngineMain in vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Where(w => w.RoleCode?.Value == "MAIN"))
                                                    {
                                                        if (relatedVesselTransportMeansAttachedVesselEngineMain.PowerMeasure != null)
                                                        {
                                                            foreach (var attachedVesselEngineMainPowerMeasure in relatedVesselTransportMeansAttachedVesselEngineMain.PowerMeasure)
                                                            {
                                                                //VESSEL-L01-01-0050, VESSEL-L01-02-0039
                                                                //Main Power
                                                                //Format: XXXXX.YY with 2 optional decimals
                                                                //#Q Decimal format check - When accessing the value, the decimal separator is "," instead of ".", as it is in the xml document - how to match format?
                                                                //Regex.Match(relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure.Value.ToString(), "^d{1,5}(.[0-9]{1,2})?$").Success
                                                                if (attachedVesselEngineMainPowerMeasure.Value != null)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0050 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value format XXXXX.YY valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0039 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value format XXXXX.YY valid");

                                                                    //VESSEL-L01-01-0052, VESSEL-L01-02-0040
                                                                    //Main Power
                                                                    //>= lower limit : 0 &<= upper limit : 20000 (Parameters PWR_LOW and PWR_UP from the VESSEL_BR_PARAMETER code list)
                                                                    //#Q Refactor range validation
                                                                    //if (attachedVesselEngineMainPowerMeasure.Value >= PWR_LOW && attachedVesselEngineMainPowerMeasure.Value <= PWR_UP) 
                                                                    if (attachedVesselEngineMainPowerMeasure.Value >= 0 && attachedVesselEngineMainPowerMeasure.Value <= 20000)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0052 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value in range");
                                                                        System.Console.WriteLine("VESSEL-L01-02-0040 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value in range");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0052 | REJECTED | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value out of range");
                                                                        System.Console.WriteLine("VESSEL-L01-02-0040 | REJECTED | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value out of range");
                                                                        //VESSEL-L01-01-0052 - rejected
                                                                        //VESSEL-L01-02-0040 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0050 | REJECTED | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value format XXXXX.YY not valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0039 | REJECTED | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value out of range");
                                                                    //VESSEL-L01-02-0039 - rejected
                                                                    //VESSEL-L01-01-0050 - rejected
                                                                }
                                                            }
                                                        }
                                                        {
                                                            System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure provided");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0051 | ERROR | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.Value == MAIN provided");
                                                    //VESSEL-L01-01-0051 - ERROR
                                                }

                                                //VESSEL-L01-01-0055
                                                //Auxiliary Power
                                                //Should be provided
                                                if (vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Any(a => a.RoleCode?.Value == "AUX"))
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0055 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.Value == AUX provided");

                                                    foreach (var relatedVesselTransportMeansAttachedVesselEngineAux in vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Where(w => w.RoleCode?.Value == "AUX"))
                                                    {
                                                        if (relatedVesselTransportMeansAttachedVesselEngineAux.PowerMeasure != null)
                                                        {
                                                            foreach (var attachedVesselEngineAuxPowerMeasure in relatedVesselTransportMeansAttachedVesselEngineAux.PowerMeasure)
                                                            {
                                                                //VESSEL-L01-01-0054
                                                                //Auxiliary Power
                                                                //Format: XXXXX.YY with 2 optional decimals
                                                                //#Q Decimal format check - When accessing the value, the decimal separator is "," instead of ".", as it is in the xml document - how to match format?
                                                                //Regex.Match(relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure.Value.ToString(), "^d{1,5}(.[0-9]{1,2})?$").Success
                                                                if (attachedVesselEngineAuxPowerMeasure.Value != null)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0054 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value format XXXXX.YY valid");

                                                                    //VESSEL-L01-01-0056
                                                                    //Auxiliary Power
                                                                    //>= lower limit : 0 & <= upper limit : 20000 (Parameters PWR_LOW and PWR_UP from the VESSEL_BR_PARAMETER code list)
                                                                    if (attachedVesselEngineAuxPowerMeasure.Value >= 0 && attachedVesselEngineAuxPowerMeasure.Value <= 20000)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0056 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value in range");

                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0056 | ERROR | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value out of range");
                                                                        //VESSEL-L01-01-0056 - error
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0054 | REJECTED | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value format XXXXX.YY not valid");
                                                                    //VESSEL-L01-01-0054 - rejected
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L01-01-0055 | ERROR | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.Value == AUX provided");
                                                    //VESSEL-L01-01-0055 - ERROR
                                                }

                                                foreach (var relatedVesselTransportMeansAttachedVesselEngine in vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine)
                                                {
                                                    if (relatedVesselTransportMeansAttachedVesselEngine.RoleCode != null)
                                                    {
                                                        //VESSEL-L00-00-0056
                                                        //Vessel_ Engine /Role
                                                        //ListId= FLUX_VESSEL_ENGINE_ROLE
                                                        if (relatedVesselTransportMeansAttachedVesselEngine.RoleCode.listID?.ToString() == "FLUX_VESSEL_ENGINE_ROLE")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0056 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.listID == FLUX_VESSEL_ENGINE_ROLE");

                                                            System.Console.WriteLine("VESSEL-L00-00-0033 | TODO | Check DB - FLUX_VESSEL_ENGINE_ROLE");
                                                            //VESSEL-L00-00-0033
                                                            //Vessel_ Engine /Role
                                                            //Code from the specified list
                                                            //TODO: check DB nomenclature - FLUX_VESSEL_ENGINE_ROLE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.Value exists
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0056 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode.listID provided or != FLUX_VESSEL_ENGINE_ROLE");
                                                            //VESSEL-L00-00-0056 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.RoleCode provided");
                                                    }

                                                    if (relatedVesselTransportMeansAttachedVesselEngine.PowerMeasure != null)
                                                    {
                                                        foreach (var attachedVesselEnginePowerMeasure in relatedVesselTransportMeansAttachedVesselEngine.PowerMeasure)
                                                        {
                                                            //VESSEL-L00-00-0041
                                                            //Vessel_Engine/Power
                                                            //The unit must be KWT
                                                            if (attachedVesselEnginePowerMeasure.unitCode?.ToString() == "KWT")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0041 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.unitCode provided and == KWT");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0041 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.unitCode provided or != KWT");
                                                                //VESSEL-L00-00-0041 - rejected
                                                            }

                                                            //VESSEL-L00-00-0075
                                                            //Vessel_Engine /Power
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if there is a value
                                                            if (attachedVesselEnginePowerMeasure.Value.ToString() != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0075 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value (decimal) provided and != null");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0075 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure.Value (decimal) provided or == null");
                                                                //VESSEL-L00-00-0075 - rejected
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PowerMeasure provided");
                                                    }

                                                    //For fishing authorisation management (SUB or SUB-VED)
                                                    if (currentFluxVesselReportType == "SUB" || currentFluxVesselReportType == "SUB-VED")
                                                    {
                                                        //VESSEL-L00-00-0098
                                                        //Vessel_ Engine /Propulsion_type
                                                        //ListId = PROPELLER_TYPE
                                                        if (relatedVesselTransportMeansAttachedVesselEngine.PropulsionTypeCode?.listID?.ToString() == "PROPELLER_TYPE")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0098 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PropulsionTypeCode.listID provided and == PROPELLER_TYPE");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0098 | REJECTED | (SUB or SUB-VED) | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PropulsionTypeCode.listID provided or != PROPELLER_TYPE");
                                                            //VESSEL-L00-00-0098 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselTransportMeansAttachedVesselEngine.Manufacturer != null)
                                                    {
                                                        //VESSEL-L01-00-0520
                                                        //Engine Mark
                                                        //Length <= 50 characters max
                                                        if (relatedVesselTransportMeansAttachedVesselEngine.Manufacturer.Value?.Length <= 50)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0520 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Manufacturer.Value provided and length <= 50");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0520 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Manufacturer.Value provided or length > 50");
                                                            //VESSEL-L01-00-0520 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Manufacturer provided");
                                                    }

                                                    if (relatedVesselTransportMeansAttachedVesselEngine.Model != null)
                                                    {
                                                        //VESSEL-L01-00-0521
                                                        //Engine Model
                                                        //Length <= 50 characters max
                                                        if (relatedVesselTransportMeansAttachedVesselEngine.Model.Value?.Length <= 50)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0521 | OK | VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Model.Value provided and length <= 50");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0521 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Model.Value provided or length > 50");
                                                            //VESSEL-L01-00-0521 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.Model provided");
                                                    }

                                                    if (relatedVesselTransportMeansAttachedVesselEngine.PropulsionTypeCode != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0522 | TODO | Check DB - PROPELLER_TYPE");
                                                        //VESSEL-L01-00-0522
                                                        //Propeller Type
                                                        //Code from the specified list
                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PropulsionTypeCode.Value in PROPELLER_TYPE list
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine.PropulsionTypeCode provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0057, VESSEL-L00-00-0039, VESSEL-L00-00-0032, VESSEL-L00-00-0076, VESSEL-L01-01-0035, VESSEL-L01-02-0014, VESSEL-L01-01-0036,
                                            //VESSEL-L01-02-0015, VESSEL-L01-01-0038, VESSEL-L01-02-0018, VESSEL-L01-01-0039, VESSEL-L01-02-0019, VESSEL-L01-01-0041, VESSEL-L01-02-0025,
                                            //VESSEL-L01-01-0042, VESSEL-L01-02-0026, VESSEL-L01-01-0044, VESSEL-L01-02-0028, VESSEL-L01-01-0045, VESSEL-L01-02-0029, VESSEL-L01-01-0047,
                                            //VESSEL-L01-01-0048, VESSEL-L01-00-0693, VESSEL-L01-00-0691, VESSEL-L01-00-0692, VESSEL-L01-00-0696, VESSEL-L01-00-0694, VESSEL-L01-00-0695,
                                            //VESSEL-L01-00-0699, VESSEL-L01-00-0697, VESSEL-L01-00-0698, VESSEL-L01-00-0702, VESSEL-L01-00-0700, VESSEL-L01-00-0701, VESSEL-L01-00-0705,
                                            //VESSEL-L01-00-0703, VESSEL-L01-00-0704, VESSEL-L01-00-0524, VESSEL-L01-00-0645, VESSEL-L01-00-0527, VESSEL-L01-00-0646, VESSEL-L01-00-0530,
                                            //VESSEL-L01-00-0647, VESSEL-L01-00-0533, VESSEL-L01-00-0648, VESSEL-L01-00-0649, VESSEL-L01-00-0650
                                            #region VesselEventRelatedVesselTransportMeans.SpecifiedVesselDimension
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension != null)
                                            {
                                                foreach (var relatedVesselTransportMeansSpecifiedVesselDimension in vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension)
                                                {
                                                    //VESSEL-L00-00-0057
                                                    //Vessel_ Dimension /Type
                                                    //ListId= FLUX_VESSEL_DIM_TYPE
                                                    if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode?.listID?.ToString() == "FLUX_VESSEL_DIM_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0057 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.listID provided and == FLUX_VESSEL_DIM_TYPE");

                                                        System.Console.WriteLine("VESSEL-L00-00-0039 | TODO | Check - For Type, the unit must be the default value from the FLUX_VESSEL_DIM_TYPE");
                                                        //VESSEL-L00-00-0039
                                                        //Vessel_ Dimension /Type & Vessel_ Dimension /Value
                                                        //For Type, the unit must be the default value from the FLUX_VESSEL_DIM_TYPE
                                                        //TODO: Check - For Type, the unit must be the default value from the FLUX_VESSEL_DIM_TYPE

                                                        System.Console.WriteLine("VESSEL-L00-00-0032 | TODO | Check DB - FLUX_VESSEL_DIM_TYPE");
                                                        //VESSEL-L00-00-0032
                                                        //Vessel_ Dimension /Type
                                                        //Code from the specified list
                                                        //TODO: check DB nomenclature - FLUX_VESSEL_DIM_TYPE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value exists


                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "LOA")
                                                        {
                                                            //VESSEL-L00-00-0076
                                                            //Vessel_Dimension/Value
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if !null
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");

                                                                //VESSEL-L01-01-0035, VESSEL-L01-02-0014
                                                                //LOA
                                                                //Format: XXXX.YY with 2 optional decimals
                                                                //#Q Decimal format check - When accessing the value, the decimal separator is "," instead of ".", as it is in the xml document - how to match format?
                                                                //Regex.Match(relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure.Value.ToString(), "^d{1,4}(,[0-9]{1,2})?$").Success
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0035 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXX.YY");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0014 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXX.YY");

                                                                    //#Q IF FORMAT OK:
                                                                    System.Console.WriteLine("VESSEL-L01-01-0036 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0015 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");

                                                                    valueLOA = relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure.Value;
                                                                    //VESSEL-L01-01-0036, VESSEL-L01-02-0015
                                                                    //LOA
                                                                    //>= lower limit : 1 & <= upper limit : 200 (Parameters LEN_LOW and LEN_UP from the VESSEL_BR_PARAMETER code list
                                                                    //#Q Check value in range
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0035 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXX.YY not valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0015 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXX.YY not valid");
                                                                    //VESSEL-L01-01-0035 - rejected
                                                                    //VESSEL-L01-02-0015 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");
                                                                //VESSEL-L00-00-0076 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "LBP")
                                                        {

                                                            //VESSEL-L00-00-0076
                                                            //Vessel_Dimension/Value
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if !null
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");

                                                                //VESSEL-L01-01-0038, VESSEL-L01-02-0018
                                                                //LBP
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null && true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0038 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXX.YY");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0018 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXX.YY");

                                                                    //#Q IF FORMAT OK:
                                                                    System.Console.WriteLine("VESSEL-L01-01-0039 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0019 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    //VESSEL-L01-01-0039, VESSEL-L01-02-0019
                                                                    //LBP
                                                                    //>= lower limit : 1 & <= upper limit : 200 (Parameters LEN_LOW and LEN_UP from the VESSEL_BR_PARAMETER code list)
                                                                    //#Q Check value in range
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0038 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXX.YY not valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0018 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-01-0038 - rejected
                                                                    //VESSEL-L01-02-0018 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");
                                                                //VESSEL-L00-00-0076 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "GT")
                                                        {
                                                            //VESSEL-L00-00-0076
                                                            //Vessel_Dimension/Value
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if !null
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");

                                                                //VESSEL-L01-01-0041, VESSEL-L01-02-0025
                                                                //GT Tonnage
                                                                //Format: XXXXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0041 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0025 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                    //#Q IF FORMAT OK:
                                                                    System.Console.WriteLine("VESSEL-L01-01-0042 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0026 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    //VESSEL-L01-01-0042, VESSEL-L01-02-0026
                                                                    //GT Tonnage
                                                                    //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                    //#Q Check value in range
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0041 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0025 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                    //VESSEL-L01-01-0041 - rejected
                                                                    //VESSEL-L01-02-0025 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");
                                                                //VESSEL-L00-00-0076 - rejected
                                                            }

                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "TOTH")
                                                        {
                                                            //VESSEL-L00-00-0076
                                                            //Vessel_Dimension/Value
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if !null
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");

                                                                //VESSEL-L01-01-0044, VESSEL-L01-02-0028
                                                                //Other Tonnage
                                                                //Format: XXXXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0044 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0028 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                    //#Q IF FORMAT OK:
                                                                    System.Console.WriteLine("VESSEL-L01-01-0045 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0029 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    //VESSEL-L01-01-0045, VESSEL-L01-02-0029
                                                                    //Other Tonnage
                                                                    //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                    //#Q Check value in range
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0044 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                    System.Console.WriteLine("VESSEL-L01-02-0028 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                    //VESSEL-L01-01-0044 - rejected
                                                                    //VESSEL-L01-02-0028 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");
                                                                //VESSEL-L00-00-0076 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "GTS")
                                                        {
                                                            //VESSEL-L00-00-0076
                                                            //Vessel_Dimension/Value
                                                            //Numerical value
                                                            //#Q Already returns decimal, check is if !null
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");

                                                                //VESSEL-L01-01-0047
                                                                //GTs
                                                                //Format: XXXXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0047 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                    //#Q IF FORMAT OK:
                                                                    System.Console.WriteLine("VESSEL-L01-01-0048 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                    //VESSEL-L01-01-0048
                                                                    //GTs
                                                                    //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                    //#Q Check value in range
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0047 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value Numerical value provided");
                                                                //VESSEL-L00-00-0076 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "LRE")
                                                        {
                                                            //VESSEL-L01-00-0693
                                                            //LRE
                                                            //Only for non-fishing vessels
                                                            //#Q Procceed after non-fishing vessel check
                                                            if (true)
                                                            {
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                                {
                                                                    //VESSEL-L01-00-0691
                                                                    //LRE
                                                                    //Format: XXX.YY with 2 optional decimals
                                                                    //#Q Add decimal format check
                                                                    if (true)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0691 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXX.YY");

                                                                        //#Q IF FORMAT OK:
                                                                        System.Console.WriteLine("VESSEL-L01-00-0692 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                        //VESSEL-L01-00-0692
                                                                        //LRE
                                                                        //>= lower limit : 1 & <= upper limit : 200 (Parameters LEN_LOW and LEN_UP from the VESSEL_BR_PARAMETER code list)
                                                                        //#Q Check value in range
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0691 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXX.YY not valid");
                                                                        //VESSEL-L01-00-0691 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value value provided");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0693 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value == LRE, but vessel !non-fishing");
                                                                //VESSEL-L01-00-0693 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "LOTH")
                                                        {
                                                            //VESSEL-L01-00-0696
                                                            //Other length
                                                            //Only for non-fishing vessels
                                                            //#Q Procceed after non-fishing vessel check
                                                            if (true)
                                                            {
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                                {
                                                                    //VESSEL-L01-00-0694
                                                                    //Other length
                                                                    //Format: XXX.YY with 2 optional decimals
                                                                    //#Q Add decimal format check
                                                                    if (true)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0694 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXX.YY");

                                                                        //#Q IF FORMAT OK:
                                                                        System.Console.WriteLine("VESSEL-L01-00-0695 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                        //VESSEL-L01-00-0695
                                                                        //Other length
                                                                        //>= lower limit : 1 & <= upper limit : 200 (Parameters LEN_LOW and LEN_UP from the VESSEL_BR_PARAMETER code list)
                                                                        //#Q Check value in range
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0694 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXX.YY not valid");
                                                                        //VESSEL-L01-00-0694 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value value provided");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0696 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value == LOTH, but vessel !non-fishing");
                                                                //VESSEL-L01-00-0696 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "NT")
                                                        {
                                                            //VESSEL-L01-00-0699
                                                            //NT
                                                            //Only for non-fishing vessels
                                                            //#Q Procceed after non-fishing vessel check
                                                            if (true)
                                                            {
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                                {
                                                                    //VESSEL-L01-00-0697
                                                                    //NT
                                                                    //Format: XXXXX.YY with 2 optional decimals
                                                                    //#Q Add decimal format check
                                                                    if (true)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0697 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                        //#Q IF FORMAT OK:
                                                                        System.Console.WriteLine("VESSEL-L01-00-0698 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                        //VESSEL-L01-00-0698
                                                                        //NT
                                                                        //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                        //#Q Check value in range
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0697 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                        //VESSEL-L01-00-0697 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value value provided");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0699 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value == NT, but vessel !non-fishing");
                                                                //VESSEL-L01-00-0699 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "NRT")
                                                        {
                                                            //VESSEL-L01-00-0702
                                                            //NRT
                                                            //Only for non-fishing vessels
                                                            //#Q Procceed after non-fishing vessel check
                                                            if (true)
                                                            {
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                                {
                                                                    //VESSEL-L01-00-0700
                                                                    //NRT
                                                                    //Format: XXXXX.YY with 2 optional decimals
                                                                    //#Q Add decimal format check
                                                                    if (true)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0700 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                        //#Q IF FORMAT OK:
                                                                        System.Console.WriteLine("VESSEL-L01-00-0701 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                        //VESSEL-L01-00-0701
                                                                        //NRT
                                                                        //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                        //#Q Check value in range
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0700 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                        //VESSEL-L01-00-0700 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value value provided");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0702 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value == NRT, but vessel !non-fishing");
                                                                //VESSEL-L01-00-0702 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "CART")
                                                        {
                                                            //VESSEL-L01-00-0705
                                                            //CART
                                                            //Only for non-fishing vessels
                                                            //#Q Procceed after non-fishing vessel check
                                                            if (true)
                                                            {
                                                                if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value != null)
                                                                {
                                                                    //VESSEL-L01-00-0703
                                                                    //CART
                                                                    //Format: XXXXX.YY with 2 optional decimals
                                                                    //#Q Add decimal format check
                                                                    if (true)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0703 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided with valid format XXXXX.YY");

                                                                        //#Q IF FORMAT OK:
                                                                        System.Console.WriteLine("VESSEL-L01-00-0704 | TODO | Check VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value in range");
                                                                        //VESSEL-L01-00-0704
                                                                        //CART
                                                                        //>= lower limit : 0 & <= upper limit : 20000 (Parameters TON_LOW and TON_UP from the VESSEL_BR_PARAMETER code list)
                                                                        //#Q Check value in range
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0703 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value provided or format XXXXX.YY not valid");
                                                                        //VESSEL-L01-00-0703 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value value provided");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0705 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.Value == CART, but vessel !non-fishing");
                                                                //VESSEL-L01-00-0705 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "DEPTH")
                                                        {
                                                            //VESSEL-L01-00-0524
                                                            //Depth
                                                            //no negative value
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value > 0)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0524 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEPTH provided and positive");

                                                                //VESSEL-L01-00-0645
                                                                //Depth
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0645 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEPTH provided with valid format XXX.YY");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0645 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEPTH provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0645 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0524 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEPTH provided or negative");
                                                                //VESSEL-L01-00-0524 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "MDEPTH")
                                                        {
                                                            //VESSEL-L01-00-0527
                                                            //Moulded depth
                                                            //no negative value
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value > 0)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0527 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value MDEPTH provided and positive");

                                                                //VESSEL-L01-00-0646
                                                                //Moulded depth
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0646 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value MDEPTH provided with valid format XXX.YY");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0646 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value MDEPTH provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0646 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0527 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value MDEPTH provided or negative");
                                                                //VESSEL-L01-00-0527 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "DRAUGHT")
                                                        {
                                                            //VESSEL-L01-00-0530
                                                            //Draught
                                                            //no negative value
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value > 0)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0530 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DRAUGHT provided and positive");

                                                                //VESSEL-L01-00-0647
                                                                //Draught
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0647 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DRAUGHT provided with valid format XXX.YY");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0647 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DRAUGHT provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0647 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0530 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DRAUGHT provided or negative");
                                                                //VESSEL-L01-00-0530 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "BREADTH")
                                                        {
                                                            //VESSEL-L01-00-0533
                                                            //Breadth
                                                            //no negative value
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value > 0)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0533 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value BREADTH provided and positive");

                                                                //VESSEL-L01-00-0648
                                                                //Breadth
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0648 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value BREADTH provided with valid format XXX.YY");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0648 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value BREADTH provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0648 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0533 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value BREADTH provided or negative");
                                                                //VESSEL-L01-00-0533 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansSpecifiedVesselDimension.TypeCode.Value?.ToString() == "DEADW")
                                                        {
                                                            //VESSEL-L01-00-0649
                                                            //Deadweight
                                                            //no negative value
                                                            if (relatedVesselTransportMeansSpecifiedVesselDimension.ValueMeasure?.Value > 0)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0649 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEADW provided and positive");

                                                                //VESSEL-L01-00-0650
                                                                //Deadweight
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0650 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEADW provided with valid format XXX.YY");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0650 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEADW provided or format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0650 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0649 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.ValueMeasure.Value DEADW provided or negative");
                                                                //VESSEL-L01-00-0649 - rejected
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0057 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension.TypeCode.listID provided or != FLUX_VESSEL_DIM_TYPE");
                                                        //VESSEL-L00-00-0057 - rejected
                                                    }

                                                    //VESSEL-L00-00-0039
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselDimension provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0058, VESSEL-L01-01-0028, VESSEL-L01-01-0027, VESSEL-L00-00-0059, VESSEL-L00-00-0031, VESSEL-L01-01-0029, VESSEL-L01-01-0030,
                                            //VESSEL-L01-01-0032, VESSEL-L01-01-0031, VESSEL-L01-01-0033
                                            #region VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear
                                            if (vesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear != null)
                                            {
                                                foreach (var relatedVesselTransportMeansOnBoardFishingGear in vesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear)
                                                {
                                                    //VESSEL-L00-00-0058
                                                    //Fishing_ Gear /Type
                                                    //ListId= GEAR_TYPE
                                                    if (relatedVesselTransportMeansOnBoardFishingGear.TypeCode?.listID?.ToString() == "GEAR_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0058 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.listID provided and == GEAR_TYPE");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0058 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.listID provided or != GEAR_TYPE");
                                                        //VESSEL-L00-00-0058 - rejected
                                                    }

                                                    if (relatedVesselTransportMeansOnBoardFishingGear.RoleCode != null)
                                                    {
                                                        //VESSEL-L01-01-0028
                                                        //Main Fishing Gear
                                                        //Mandatory value
                                                        if (relatedVesselTransportMeansOnBoardFishingGear.RoleCode.Any(a => a.Value == "MAIN"))
                                                        {
                                                            foreach (var onBoardFishingGearRoleCodeMain in relatedVesselTransportMeansOnBoardFishingGear.RoleCode.Where(w => w.Value == "MAIN"))
                                                            {
                                                                //VESSEL-L01-01-0027
                                                                //Main Fishing Gear
                                                                //Length = 2 or 3 characters
                                                                if (relatedVesselTransportMeansOnBoardFishingGear.TypeCode?.Value?.Length == 2 || relatedVesselTransportMeansOnBoardFishingGear.TypeCode?.Value?.Length == 3)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0027 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value provided with length == 2 || 3");

                                                                    //VESSEL-L00-00-0059
                                                                    //Fishing_ Gear /Role
                                                                    //ListId= FLUX_VESSEL_GEAR_ROLE
                                                                    if (onBoardFishingGearRoleCodeMain.listID?.ToString() == "FLUX_VESSEL_GEAR_ROLE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0059 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode..listID provided and == FLUX_VESSEL_GEAR_ROLE");

                                                                        System.Console.WriteLine("VESSEL-L00-00-0031 | TODO | Check DB - FLUX_VESSEL_GEAR_ROLE");
                                                                        //VESSEL-L00-00-0031
                                                                        //Fishing_ Gear /Role
                                                                        //Code from the specified list
                                                                        //TODO: check DB nomenclature - FLUX_VESSEL_GEAR_ROLE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode.Value

                                                                        System.Console.WriteLine("VESSEL-L01-01-0027 | TODO | Check DB - GEAR_TYPE");
                                                                        //VESSEL-L01-01-0029
                                                                        //Main Fishing Gear
                                                                        //Code from the specified list
                                                                        //TODO: check DB nomenclature - GEAR_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value

                                                                        System.Console.WriteLine("VESSEL-L01-01-0027 | TODO | 'No gear' code not allowed");
                                                                        //VESSEL-L01-01-0030
                                                                        //Main Fishing Gear
                                                                        //"No gear" code not allowed
                                                                        //TODO: if Main Fishing Gear code is not "No gear"
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0059 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode.listID provided or != FLUX_VESSEL_GEAR_ROLE");
                                                                        //VESSEL-L00-00-0059 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0027 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value provided or with length != 2 || 3");
                                                                    //VESSEL-L01-01-0027 - rejected
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0028 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode provided with Value MAIN");
                                                            //VESSEL-L01-01-0028 - error
                                                        }

                                                        //VESSEL-L01-01-0032
                                                        //Subsidiary Fishing Gears
                                                        //Mandatory value (at least one)
                                                        if (relatedVesselTransportMeansOnBoardFishingGear.RoleCode.Any(a => a.Value == "AUX"))
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0032 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode provided with Value AUX");

                                                            foreach (var onBoardFishingGearRoleCodeAux in relatedVesselTransportMeansOnBoardFishingGear.RoleCode.Where(w => w.Value == "AUX"))
                                                            {
                                                                //VESSEL-L01-01-0031
                                                                //Subsidiary Fishing Gears
                                                                //Length = 2 or 3 characters
                                                                if (relatedVesselTransportMeansOnBoardFishingGear.TypeCode?.Value?.Length == 2 || relatedVesselTransportMeansOnBoardFishingGear.TypeCode?.Value?.Length == 3)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0031 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value provided with Length == 2 || 3");

                                                                    //VESSEL-L00-00-0059
                                                                    //Fishing_ Gear /Role
                                                                    //ListId= FLUX_VESSEL_GEAR_ROLE
                                                                    if (onBoardFishingGearRoleCodeAux.listID?.ToString() == "FLUX_VESSEL_GEAR_ROLE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0059 | OK | VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode..listID provided and == FLUX_VESSEL_GEAR_ROLE");

                                                                        System.Console.WriteLine("VESSEL-L00-00-0031 | TODO | Check DB - FLUX_VESSEL_GEAR_ROLE");
                                                                        //VESSEL-L00-00-0031
                                                                        //Fishing_ Gear /Role
                                                                        //Code from the specified list
                                                                        //TODO: check DB nomenclature - FLUX_VESSEL_GEAR_ROLE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode.Value

                                                                        System.Console.WriteLine("VESSEL-L01-01-0033 | TODO | Check DB - GEAR_TYPE");
                                                                        //VESSEL-L01-01-0033
                                                                        //Subsidiary Fishing Gears
                                                                        //Code from the specified list
                                                                        //TODO: check DB nomenclature - GEAR_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0059 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode.listID provided or != FLUX_VESSEL_GEAR_ROLE");
                                                                        //VESSEL-L00-00-0059 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0031 | OK | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.TypeCode.Value provided or with Length != 2 || 3");
                                                                    //VESSEL-L01-01-0031 - rejected
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0032 | ERROR | No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode provided with Value AUX");
                                                            //VESSEL-L01-01-0032 - error
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear.RoleCode provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.OnBoardFishingGear provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0071, VESSEL-L00-00-0094
                                            #region VesselEvent.RelatedVesselTransportMeans.Speed
                                            //VESSEL-L00-00-0071
                                            //Vessel_Transport_Means/Speed
                                            //Numerical value
                                            //#Q Already returns a decimal, the check is if != null
                                            if (vesselEvent.RelatedVesselTransportMeans.SpeedMeasure?.Value != null)
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0071 | OK | VesselEvent.RelatedVesselTransportMeans.Speed.Value (decimal) provided");

                                                //VESSEL-L00-00-0094
                                                //Vessel_Transport_Means/Speed
                                                //For speed, the default unit must be KNT
                                                if (vesselEvent.RelatedVesselTransportMeans.SpeedMeasure.unitCode?.ToString() == "KNT")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0094 | OK | VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.unitCode provided and == KNT");
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0094 | REJECTED | VesselEvent.RelatedVesselTransportMeans.SpeedMeasure.unitCode provided or != KNT");
                                                    //VESSEL-L00-00-0094 - rejected
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("VESSEL-L00-00-0071 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.Speed.Value (decimal) provided");
                                                //VESSEL-L00-00-0071 - rejected
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0060, VESSEL-L00-00-0028, VESSEL-L00-00-0113, VESSEL-L01-01-0111, VESSEL-L01-01-0022, VESSEL-L01-01-0023, VESSEL-L01-01-0025,
                                            //VESSEL-L01-01-0026, VESSEL-L01-01-0103, VESSEL-L01-01-0104, VESSEL-L01-01-0105, VESSEL-L01-01-0106, VESSEL-L00-00-0114, VESSEL-L01-00-0547,
                                            //VESSEL-L00-00-0115, VESSEL-L01-00-0548, VESSEL-L00-00-0116, VESSEL-L01-00-0663, VESSEL-L00-00-0117, VESSEL-L01-00-0549, VESSEL-L00-00-0118,
                                            //VESSEL-L01-00-0664, VESSEL-L01-00-0665, VESSEL-L01-00-0555, VESSEL-L01-00-0666, VESSEL-L01-00-0667, VESSEL-L01-00-0559, VESSEL-L01-00-0546,
                                            //VESSEL-L01-00-0571, VESSEL-L01-00-0572, VESSEL-L01-00-0573, VESSEL-L01-00-0574, VESSEL-L01-00-0575, VESSEL-L01-00-0576, VESSEL-L00-00-0119,
                                            //VESSEL-L00-00-0120, VESSEL-L01-00-0569, VESSEL-L01-00-0668, VESSEL-L00-00-0121, VESSEL-L00-00-0122, VESSEL-L01-00-0669, VESSEL-L01-00-0670,
                                            //VESSEL-L00-00-0123, VESSEL-L00-00-0124, VESSEL-L01-00-0566, VESSEL-L01-00-0671, VESSEL-L00-00-0125, VESSEL-L00-00-0126, VESSEL-L01-00-0563,
                                            //VESSEL-L01-00-0672, VESSEL-L00-00-0081, VESSEL-L00-00-0127, VESSEL-L01-00-0557, VESSEL-L01-00-0673, VESSEL-L00-00-0080, VESSEL-L00-00-0129,
                                            //VESSEL-L01-00-0561, VESSEL-L01-00-0675, VESSEL-L01-01-0012, VESSEL-L01-01-0013
                                            #region VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic
                                            if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic != null)
                                            {
                                                foreach (var relatedTransportMeansApplicableVesselEquipmentCharacteristic in vesselEvent.RelatedVesselTransportMeans?.ApplicableVesselEquipmentCharacteristic)
                                                {
                                                    //VESSEL-L00-00-0060
                                                    //Vessel_ Equipment_Characteristic/Type
                                                    //ListId= FLUX_VESSEL_EQUIP_ TYPE
                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.listID?.ToString() == "FLUX_VESSEL_EQUIP_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0060 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode.listID provided and == FLUX_VESSEL_EQUIP_TYPE");

                                                        System.Console.WriteLine("VESSEL-L00-00-0028 | TODO | Check DB - FLUX_VESSEL_EQUIP_TYPE");
                                                        //VESSEL-L00-00-0028
                                                        //Vessel_ Equipment_Characteristic/Type
                                                        //Code from the specified list
                                                        //TODO: Check DB nomenclature - FLUX_VESSEL_EQUIP_TYPE if VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode.Value

                                                        foreach (var applicableVesselEquipmentCharacteristic in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(e => e.TypeCode.Value.EndsWith("_IND")))
                                                        {
                                                            if (applicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in applicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0113
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId=BOOLEAN_TYPE for Type like '%_IND'
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.listID?.ToString() == "BOOLEAN_TYPE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0113 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.listID provided and == BOOLEAN_TYPE");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0113 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.listID provided or != BOOLEAN_TYPE");
                                                                        //VESSEL-L00-00-0113
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.listID provided");
                                                            }
                                                        }

                                                        //VESSEL-L01-01-0111
                                                        //IRCS Indicator
                                                        //Mandatory value
                                                        //#Q Is this the mandatory value the check is for or the VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueIndicator.Item?
                                                        if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.Value?.ToString() == "IRCS_IND").Count() > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0111 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode for IRCS provided");
                                                            foreach (var applicableVesselEquipmentCharacteristicIRCS in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "IRCS_IND" && w.ValueCode != null))
                                                            {
                                                                foreach (var IRCSValueCode in applicableVesselEquipmentCharacteristicIRCS.ValueCode)
                                                                {
                                                                    //VESSEL-L01-01-0022
                                                                    //IRCS Indicator
                                                                    //Length = 1 character
                                                                    if (IRCSValueCode.Value?.Length == 1)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0022 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value for IRCS provided and length == 1");

                                                                        System.Console.WriteLine("VESSEL-L01-01-0023 | TODO | Check DB - BOOLEAN_TYPE for IRCS");
                                                                        //VESSEL-L01-01-0023
                                                                        //IRCS Indicator
                                                                        //Code from a list of reference: "BOOLEAN_TYPE" code list
                                                                        //TODO: Check DB nomenclature - BOOLEAN_TYPE for VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value

                                                                        if (IRCSValueCode.Value?.ToString() == "Y")
                                                                        {
                                                                            hasIrcsBool = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            hasIrcsBool = false;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0022 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value for IRCS provided or length != 1");
                                                                        //VESSEL-L01-01-0022 - rejected
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0111 | ERROR | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode for IRCS provided");
                                                            //VESSEL-L01-01-0111 - error
                                                        }

                                                        if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "VMS_IND").Count() > 0)
                                                        {
                                                            foreach (var applicableVesselEquipmentCharacteristicVMS in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "VMS_IND" && w.ValueCode != null))
                                                            {
                                                                foreach (var VMSValueCode in applicableVesselEquipmentCharacteristicVMS.ValueCode)
                                                                {
                                                                    //VESSEL-L01-01-0025
                                                                    //VMS Indicator
                                                                    //Length = 1 character
                                                                    if (VMSValueCode.Value?.Length == 1)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0025 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value for VMS provided and length == 1");

                                                                        System.Console.WriteLine("VESSEL-L01-01-0026 | TODO | Check DB - BOOLEAN_TYPE for VMS");
                                                                        //VESSEL-L01-01-0026
                                                                        //VMS Indicator
                                                                        //Code from a list of reference: "BOOLEAN_TYPE" code list
                                                                        //TODO: Check DB nomenclature - BOOLEAN_TYPE for VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0025 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value for VMS provided or length != 1");
                                                                        //VESSEL-L01-01-0025 - rejected
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode for VMS provided");
                                                        }


                                                        if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "ERS_IND").Count() > 0)
                                                        {
                                                            foreach (var applicableVesselEquipmentCharacteristicERS in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "ERS_IND" && w.ValueCode != null))
                                                            {
                                                                foreach (var ERSValueCode in applicableVesselEquipmentCharacteristicERS.ValueCode)
                                                                {
                                                                    //VESSEL-L01-01-0103
                                                                    //ERS Indicator
                                                                    //Length = 1 character
                                                                    if (ERSValueCode.Value?.Length == 1)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0103 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided with length == 1");

                                                                        System.Console.WriteLine("VESSEL-L01-01-0104 | TODO | Check DB - BOOLEAN_TYPE");
                                                                        //VESSEL-L01-01-0104
                                                                        //ERS Indicator
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value in VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.listID
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0103 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or length != 1");
                                                                        //VESSEL-L01-01-0103 - rejected
                                                                    }
                                                                }
                                                            }
                                                        }


                                                        if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "AIS_IND").Count() > 0)
                                                        {
                                                            foreach (var applicableVesselEquipmentCharacteristicAIS in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Where(w => w.TypeCode?.listID?.ToString() == "AIS_IND"))
                                                            {
                                                                foreach (var AISValueCode in applicableVesselEquipmentCharacteristicAIS.ValueCode)
                                                                {
                                                                    //VESSEL-L01-01-0105
                                                                    //AIS Indicator
                                                                    //Length = 1 character
                                                                    if (AISValueCode.Value?.Length == 1)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0105 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided with length == 1");

                                                                        System.Console.WriteLine("VESSEL-L01-01-0106 | TODO | Check DB - BOOLEAN_TYPE");
                                                                        //VESSEL-L01-01-0106
                                                                        //AIS Indicator
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value in VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.listID
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-01-0105 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or length != 1");
                                                                        //VESSEL-L01-01-0105 - rejected
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "NAVIG_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0114
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId=NAVIG_EQUIP_TYPE For type = NAVIG_EQ
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.Value != null && relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID?.ToString() == "NAVIG_EQUIP_TYPE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0114 | OK | VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided and relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID == NAVIG_EQUIP_TYPE");

                                                                        System.Console.WriteLine("VESSEL-L01-01-0106 | TODO | Check DB - NAVIG_EQUIP_TYPE");
                                                                        //VESSEL-L01-00-0547
                                                                        //Navigation equipment type details
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value is from NAVIG_EQUIP_TYPE list
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0114 | REJECTED | No VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID != NAVIG_EQUIP_TYPE");
                                                                        //VESSEL-L00-00-0114 - rejeced
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided for NAVIG_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "COMM_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0115
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId= COMM_EQUIP_TYPE For type = COMM_EQ
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.Value != null && relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID?.ToString() == "COMM_EQUIP_TYPE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0115 | OK | VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided and relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID == COMM_EQUIP_TYPE");

                                                                        System.Console.WriteLine("VESSEL-L01-00-0548 | TODO | Check DB - COMM_EQUIP_TYPE");
                                                                        //VESSEL-L01-00-0548
                                                                        //Communication equipment details
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value is from NAVIG_EQUIP_TYPE list
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0115 | REJECTED | No VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID != COMM_EQUIP_TYPE");
                                                                        //VESSEL-L00-00-0115 - rejeced
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided for COMM_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "FISHFINDER_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0116
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId= FISHFINDER_EQUIP_TYPE For type = FISHFINDER_EQ
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.Value != null && relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID?.ToString() == "FISHFINDER_EQUIP_TYPE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0116 | OK | VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided and relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID == FISHFINDER_EQUIP_TYPE");

                                                                        System.Console.WriteLine("VESSEL-L01-00-0663 | TODO | Check DB - FISHFINDER_EQUIP_TYPE");
                                                                        //VESSEL-L01-00-0663
                                                                        //Fish finder equipment details
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value is from FISHFINDER_EQUIP_TYPE list
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0116 | REJECTED | No VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID != FISHFINDER_EQUIP_TYPE");
                                                                        //VESSEL-L00-00-0116 - rejeced
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided for FISHFINDER_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "DECK_MACHINERY")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0117
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId= DECK_MACHINERY_TYPE For type = DECK_MACHINERY
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.Value != null && relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID?.ToString() == "FISHFINDER_EQUIP_TYPE")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0117 | OK | VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided and relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID == DECK_MACHINERY_TYPE");

                                                                        System.Console.WriteLine("VESSEL-L01-00-0549 | TODO | Check DB - DECK_MACHINERY_TYPE");
                                                                        //VESSEL-L01-00-0549
                                                                        //Deck machinery details
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value is from DECK_MACHINERY_TYPE list
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0117 | REJECTED | No VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID != DECK_MACHINERY_TYPE");
                                                                        //VESSEL-L00-00-0117 - rejeced
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided for DECK_MACHINERY");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_SAT_OPER_C")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                                {
                                                                    //VESSEL-L00-00-0118
                                                                    //Vessel_ Equipment_Characteristic/ Type & Value
                                                                    //ListId= VMS_SATELLITE_OPERATOR For type = VMS_SAT_OPER_C
                                                                    if (applicableVesselEquipmentCharacteristicValueCode.Value != null && relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID?.ToString() == "VMS_SATELLITE_OPERATOR")
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0118 | OK | VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided and relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID == VMS_SATELLITE_OPERATOR");

                                                                        System.Console.WriteLine("VESSEL-L01-00-0664 | TODO | Check DB - VMS_SATELLITE_OPERATOR");
                                                                        //VESSEL-L01-00-0664
                                                                        //VMS satellite operator (code)
                                                                        //Code from the specified list
                                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value is from VMS_SATELLITE_OPERATOR list
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L00-00-0118 | REJECTED | No VesselEvent.RelatedTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode.listID != VMS_SATELLITE_OPERATOR");
                                                                        //VESSEL-L00-00-0118 - rejeced
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided for VMS_SAT_OPER_C");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "PROCESS_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0665
                                                                    //Fish processing equipment details
                                                                    //Length <= 300 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 300)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0665 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value PROCESS_EQ provided with length <= 300");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0665 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value PROCESS_EQ provided or length > 300");
                                                                        //VESSEL-L01-00-0665 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for PROCESS_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "PROCESS_TYPE")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0555
                                                                    //Fish processing line type
                                                                    //Length <= 100 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 100)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0555 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value PROCESS_TYPE provided with length <= 100");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0555 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value PROCESS_TYPE provided or length > 100");
                                                                        //VESSEL-L01-00-0555 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for PROCESS_TYPE");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "REFRIG_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0666
                                                                    //Refrigeration equipment details
                                                                    //Length <= 300 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 300)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0666 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value REFRIG_EQ provided with length <= 300");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0666 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value REFRIG_EQ provided or length > 300");
                                                                        //VESSEL-L01-00-0666 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for REFRIG_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "SAFETY_EQ")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0667
                                                                    //Safety equipment details
                                                                    //Length <= 300 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 300)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0667 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value SAFETY_EQ provided with length <= 300");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0667 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value SAFETY_EQ provided or length > 300");
                                                                        //VESSEL-L01-00-0667 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for SAFETY_EQ");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "HELICO_REG")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0559
                                                                    //Helicopter registration number
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0559 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value HELICO_REG provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0559 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value HELICO_REG provided or length > 50");
                                                                        //VESSEL-L01-00-0559 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for HELICO_REG");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "AIRC_REG")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0546
                                                                    //Aircraft registration number
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0546 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value AIRC_REG provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0546 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value AIRC_REG provided or length > 50");
                                                                        //VESSEL-L01-00-0546 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for AIRC_REG");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_MAN")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0571
                                                                    //VMS manufacturer name
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0571 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_MAN provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0571 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_MAN provided or length > 50");
                                                                        //VESSEL-L01-00-0571 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_MAN");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_MODEL")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0572
                                                                    //VMS model name
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0572 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_MODEL provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0572 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_MODEL provided or length > 50");
                                                                        //VESSEL-L01-00-0572 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_MODEL");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_SAT_OPER_T")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0573
                                                                    //VMS satellite operator (name)
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0573 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SERIAL_NBR provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0573 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SERIAL_NBR provided or length > 50");
                                                                        //VESSEL-L01-00-0573 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_SAT_OPER_T");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_SERIAL_NBR")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0574
                                                                    //VMS serial number
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0574 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SERIAL_NBR provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0574 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SERIAL_NBR provided or length > 50");
                                                                        //VESSEL-L01-00-0574 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_SERIAL_NBR");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_SOFT_VER")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0575
                                                                    //VMS software version
                                                                    //Length <= 50 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 50)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0575 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SOFT_VER provided with length <= 50");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0575 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_SOFT_VER provided or length > 50");
                                                                        //VESSEL-L01-00-0575 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_SOFT_VER");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "VMS_FEATURE")
                                                        {
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value != null)
                                                            {
                                                                foreach (var applicableVesselEquipmentCharacteristicValue in relatedTransportMeansApplicableVesselEquipmentCharacteristic.Value)
                                                                {
                                                                    //VESSEL-L01-00-0576
                                                                    //VMS features
                                                                    //Length <= 300 characters max
                                                                    if (applicableVesselEquipmentCharacteristicValue.Value?.Length <= 300)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0576 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_FEATURE provided with length <= 300");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0576 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value.Value VMS_FEATURE provided or length > 300");
                                                                        //VESSEL-L01-00-0576 - rejected
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.Value provided for VMS_FEATURE");
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "SKIFF_LGTH")
                                                        {
                                                            //VESSEL-L00-00-0119
                                                            //Vessel_Equipment/Value & Type = SKIFF_LGTH
                                                            //Numerical value
                                                            //#Q Already in type = SKIFF_LGTH and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0119 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for SKIFF_LGTH");

                                                                //VESSEL-L00-00-0120
                                                                //Vessel_Equipment/Value & Type = SKIFF_LGTH
                                                                //The unit must be MTR
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "MTR")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0120 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for SKIFF_LGTH and == MTR");

                                                                    //VESSEL-L01-00-0569
                                                                    //Support vessel skiff length
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0669 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value SKIFF_LGTH provided and positive");

                                                                        //VESSEL-L01-00-0668
                                                                        //Support vessel skiff length
                                                                        //Format: XXXXX.YY with 2 optional decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0668 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for SKIFF_LGTH with format XXXXX.YY valid ");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0668 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for SKIFF_LGTH format XXXXX.YY not valid");
                                                                            //VESSEL-L01-00-0668 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0569 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for SKIFF_LGTH is negative");
                                                                        //VESSEL-L01-00-0569 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0120 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.UnitCode provided for SKIFF_LGTH or != MTR");
                                                                    //VESSEL-L00-00-0120 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0119 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for SKIFF_LGTH");
                                                                //VESSEL-L00-00-0119 - rejected
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "SKIFF_PWR")
                                                        {
                                                            //VESSEL-L00-00-0121
                                                            //Vessel_Equipment/Value & Type = SKIFF_PWR
                                                            //Numerical value
                                                            //#Q Already in type = SKIFF_PWR and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0121 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for SKIFF_PWR");

                                                                //VESSEL-L00-00-0122
                                                                //Vessel_Equipment/Value & Type = SKIFF_PWR
                                                                //The unit must be KWT
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "KWT")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0122 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for SKIFF_PWR and == KWT");

                                                                    //VESSEL-L01-00-0669
                                                                    //Support vessel skiff engine power
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0669 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value SKIFF_PWR provided and positive");

                                                                        //VESSEL-L01-00-0670
                                                                        //Support vessel skiff engine power
                                                                        //Format: XXXXX.YY with 2 optional decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0670 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value SKIFF_PWR provided with XXXXX.YY valid format");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0670 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value SKIFF_PWR provided or format XXXXX.YY not valid");
                                                                            //VESSEL-L01-00-0670 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0669 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for SKIFF_PWR or is negative");
                                                                        //VESSEL-L01-00-0669 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0122 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCodde provided for SKIFF_PWR or != KWT");
                                                                    //VESSEL-L00-00-0122 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0121 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for SKIFF_PWR");
                                                                //VESSEL-L00-00-0121 - rejected
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "BOAT_LGTH")
                                                        {
                                                            //VESSEL-L00-00-0123
                                                            //Vessel_Equipment/Value & Type = BOAT_LGTH
                                                            //Numerical value
                                                            //#Q Already in type = BOAT_LGTH and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0123 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for BOAT_LGTH");

                                                                //VESSEL-L00-00-0124
                                                                //Vessel_Equipment/Value & Type = BOAT_LGTH
                                                                //The unit must be MTR
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "MTR")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0124 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for BOAT_LGTH and == MTR");

                                                                    //VESSEL-L01-00-0566
                                                                    //Speed boat length
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0566 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_LGTH provided and positive");

                                                                        //VESSEL-L01-00-0671
                                                                        //Speed boat length
                                                                        //Format: XXXXX.YY with 2 optional decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0671 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_LGTH provided with XXXXX.YY valid format");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0671 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_LGTH provided or format XXXXX.YY not valid");
                                                                            //VESSEL-L01-00-0671 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0566 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for BOAT_LGTH or is negative");
                                                                        //VESSEL-L01-00-0566 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0124 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCodde provided for BOAT_LGTH or != MTR");
                                                                    //VESSEL-L00-00-0124 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0123 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for BOAT_LGTH");
                                                                //VESSEL-L00-00-0123 - rejected
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "BOAT_PWR")
                                                        {
                                                            //VESSEL-L00-00-0125
                                                            //Vessel_Equipment/Value & Type = BOAT_PWR
                                                            //Numerical value
                                                            //#Q Already in type = BOAT_PWR and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0125 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for BOAT_PWR");

                                                                //VESSEL-L00-00-0126
                                                                //Vessel_Equipment/Value & Type = BOAT_PWR
                                                                //The unit must be KWT
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "KWT")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0124 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for BOAT_PWR and == KWT");

                                                                    //VESSEL-L01-00-0563
                                                                    //Speed boat engine power
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0563 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_PWR provided and positive");

                                                                        //VESSEL-L01-00-0672
                                                                        //Speed boat engine power
                                                                        //Format: XXXXX.YY with 2 optional decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0672 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_PWR provided with XXXXX.YY valid format");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0672 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value BOAT_PWR provided or format XXXXX.YY not valid");
                                                                            //VESSEL-L01-00-0672 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0563 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for BOAT_PWR or is negative");
                                                                        //VESSEL-L01-00-0563 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0126 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCodde provided for BOAT_PWR or != KWT");
                                                                    //VESSEL-L00-00-0126 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0125 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for BOAT_PWR");
                                                                //VESSEL-L00-00-0125 - rejected
                                                            }
                                                        }

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "FUEL_CAP")
                                                        {
                                                            //VESSEL-L00-00-0081
                                                            //Vessel_Equipment/Value & Type = FUEL_CAP
                                                            //Numerical value
                                                            //#Q Already in type = FUEL_CAP and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0081 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for FUEL_CAP");

                                                                //VESSEL-L00-00-0127
                                                                //Vessel_Equipment/Value & Type = FUEL_CAP
                                                                //The unit must be LTR
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "LTR")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0127 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for FUEL_CAP and == LTR");

                                                                    //VESSEL-L01-00-0557
                                                                    //Fuel tank capacity
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0557 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value FUEL_CAP provided and positive");

                                                                        //VESSEL-L01-00-0673
                                                                        //Fuel tank capacity
                                                                        //Format: XXXXX.YY with 2 optional decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0673 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value FUEL_CAP provided with XXXXX.YY valid format");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0673 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value FUEL_CAP provided or format XXXXX.YY not valid");
                                                                            //VESSEL-L01-00-0673 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0557 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for FUEL_CAP or is negative");
                                                                        //VESSEL-L01-00-0557 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0127 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCodde provided for FUEL_CAP or != LTR");
                                                                    //VESSEL-L00-00-0127 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0081 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for FUEL_CAP");
                                                                //VESSEL-L00-00-0081 - rejected
                                                            }
                                                        }

                                                        //+) number of fish holds element removed because it can be communicated in the Vessel Storage Characteristic - 23/09/2021 - VESSEL-L01-00-0554, VESSEL-L01-00-0674, VESSEL-L00-00-0079, VESSEL-L00-00-0128

                                                        if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.TypeCode?.Value?.ToString() == "LIGHTS_NBR")
                                                        {
                                                            //VESSEL-L00-00-0080
                                                            //Vessel_Equipment/Value & Type = LIGHTS_NBR
                                                            //Numerical value
                                                            //#Q Already in type = LIGHTS_NBR and value is of type decimal, check if Value present
                                                            if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure?.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0080 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure provided for LIGHTS_NBR");

                                                                //VESSEL-L00-00-0129
                                                                //Vessel_Equipment/Value & Type = LIGHTS_NBR
                                                                //The unit must be C62
                                                                if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode?.ToString() == "C62")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0129 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCode provided for FUEL_CAP and == C62");

                                                                    //VESSEL-L01-00-0561
                                                                    //Number of fishing lights
                                                                    //no negative value
                                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueQuantity?.Value > 0)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0561 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueQuantity.Value LIGHTS_NBR provided and positive");

                                                                        //VESSEL-L01-00-0675
                                                                        //Number of fishing lights
                                                                        //Format: XXXXX without decimals
                                                                        //#Q Add value format check
                                                                        if (true)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0675 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueQuantity.Value LIGHTS_NBR provided with XXXXX valid format");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0675 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueQuantity.Value LIGHTS_NBR provided or format XXXXX not valid");
                                                                            //VESSEL-L01-00-0675 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0561 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueQuantity provided for LIGHTS_NBR or is negative");
                                                                        //VESSEL-L01-00-0561 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0129 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.unitCodde provided for LIGHTS_NBR or != C62");
                                                                    //VESSEL-L00-00-0129 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0080 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueMeasure.Value provided for LIGHTS_NBR");
                                                                //VESSEL-L00-00-0080 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0060 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.TypeCode.listID provided or != FLUX_VESSEL_EQUIP_TYPE");
                                                        //VESSEL-L00-00-0060 - rejected
                                                    }

                                                    if (relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode != null)
                                                    {
                                                        foreach (var applicableVesselEquipmentCharacteristicValueCode in relatedTransportMeansApplicableVesselEquipmentCharacteristic.ValueCode)
                                                        {
                                                            //VESSEL-L01-01-0012
                                                            //License Indicator
                                                            //Length = 1 character
                                                            //#Q Added listID validation
                                                            if (applicableVesselEquipmentCharacteristicValueCode.Value?.Length == 1 && applicableVesselEquipmentCharacteristicValueCode.listID?.ToString() == "BOOLEAN_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0012 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value.Length == 1 and listID == BOOLEAN_TYPE");

                                                                System.Console.WriteLine("VESSEL-L01-01-0013 | TODO | Check DB - BOOLEAN_TYPE");
                                                                //VESSEL-L01-01-0013
                                                                //License Indicator
                                                                //Code from a list of reference: "BOOLEAN_TYPE" code list
                                                                //TODO: check DB nomenclature - BOOLEAN_TYPE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0012 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode.Value provided or Length != 1 or listID != BOOLEAN_TYPE");
                                                                //VESSEL-L01-01-0012 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic.ValueCode provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselEquipmentCharacteristic provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0061, VESSEL-L00-00-0029, VESSEL-L00-00-0082, VESSEL-L01-01-0059, VESSEL-L01-01-0061, VESSEL-L01-01-0060, VESSEL-L00-00-0131,
                                            //VESSEL-L00-00-0083, VESSEL-L01-00-0578, VESSEL-L01-00-0580, VESSEL-L00-00-0130, VESSEL-L00-00-0132, VESSEL-L00-00-0133
                                            #region VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic
                                            if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic != null)
                                            {
                                                foreach (var relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic)
                                                {
                                                    //VESSEL-L00-00-0061
                                                    //Vessel_ Administrative_Characteristic/Type
                                                    //ListId= FLUX_VESSEL_ADMIN_TYPE
                                                    if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode?.listID?.ToString() == "FLUX_VESSEL_ADMIN_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0061 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID provided and == FLUX_VESSEL_ADMIN_TYPE");

                                                        System.Console.WriteLine("VESSEL-L00-00-0029 | TODO | Check DB - FLUX_VESSEL_ADMIN_TYPE");
                                                        //VESSEL-L00-00-0029
                                                        //Vessel_ Administrative_Characteristic/Type
                                                        //Code from the specified list
                                                        //TODO: check DB nomenclature - FLUX_VESSEL_ADMIN_TYPE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.Value exists

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "EIS")
                                                        {
                                                            //VESSEL-L00-00-0082
                                                            //Vessel_ Administrative_Characteristic/Value & Type='EIS'
                                                            //Date type
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueDateTime?.Item is DateTime)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0082 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueDateTime.Item is DateTime");

                                                                //VESSEL-L01-01-0059
                                                                //EiS Year
                                                                //>= lower limit : 1850 (Parameter YEAR_LOW from the VESSEL_BR_PARAMETER code list)
                                                                //#Q Refactor DateTime check
                                                                //if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueDateTime?.Item >= YEAR_LOW)
                                                                if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueDateTime?.Item >= DateTime.Parse("Jan 1, 1850"))
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0059 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueDateTime.Item provided and >= YEAR_LOW (1850)");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-01-0059 | ERROR | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID provided or not >= YEAR_LOW");
                                                                    //VESSEL-L01-01-0059 - error
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0082 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueDateTime.Item is not DateTime");
                                                                //VESSEL-L00-00-0082 - rejected
                                                            }
                                                        }

                                                        //VESSEL-L01-01-0061
                                                        //Segment
                                                        //Should be provided
                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "SEG")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0061 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.Value provided for SEG");

                                                            //VESSEL-L01-01-0060
                                                            //Segment
                                                            //Length = 3 characters
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueCode?.Value?.Length == 3)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0060 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.Value provided and length == 3");

                                                                //VESSEL-L00-00-0131
                                                                //Vessel_ Administrative_Characteristic/Value & Type=SEG
                                                                //ListId= VESSEL_SEGMENT
                                                                //#Q Already there is a value with length 3, the check is only for listId
                                                                if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.listID?.ToString() == "VESSEL_SEGMENT")
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0131 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID == VESSEL_SEGMENT");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0131 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID != VESSEL_SEGMENT");
                                                                    //VESSEL-L00-00-0131 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-01-0060 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueCode.Value provided or length != 3");
                                                                //VESSEL-L01-01-0060 - rejected
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-01-0061 | Error | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.Value provided for SEG");
                                                            //VESSEL-L01-01-0061 - missing
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "PURCHASE_YEAR")
                                                        {
                                                            //VESSEL-L00-00-0083
                                                            //Vessel_Administrative_Characteristic/Value & Type='PURCHASE_YEAR'
                                                            //Date type
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueDateTime?.Item is DateTime)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0083 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID == VESSEL_EXPORT_TYPE");

                                                                //VESSEL-L01-00-0578
                                                                //Vessel purchase year
                                                                //Not in future
                                                                DateTime currentUtcDateTime = DateTime.UtcNow;
                                                                if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.ValueDateTime?.Item < currentUtcDateTime)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0578 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueDateTime.Item provided and not in the future");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0578 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.ValueDateTime.Item provided or in the future");
                                                                    //VESSEL-L01-00-0578 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0083 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID != VESSEL_EXPORT_TYPE");
                                                                //VESSEL-L00-00-0083 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "AUTH_NAME")
                                                        {
                                                            //VESSEL-L01-00-0580
                                                            //National authorisation name
                                                            //Length <= 300 characters max
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.Value?.Value?.Length <= 300)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0580 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and length <= 300");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0580 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or length > 300");
                                                                //VESSEL-L01-00-0580 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "LICENCE")
                                                        {
                                                            //VESSEL-L00-00-0130
                                                            //Vessel_ Administrative_Characteristic/Value & Type=LICENCE
                                                            //ListId= BOOLEAN_TYPE
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.Value?.Value != null && relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.listID?.ToString() == "BOOLEAN_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0130 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID == BOOLEAN_TYPE");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0130 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID != BOOLEAN_TYPE");
                                                                //VESSEL-L00-00-0130 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "EXPORT")
                                                        {
                                                            //VESSEL-L00-00-0132
                                                            //Vessel_ Administrative_Characteristic/Value & Type=EXPORT
                                                            //ListId= VESSEL_EXPORT_TYPE
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.Value?.Value != null && relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.listID?.ToString() == "VESSEL_EXPORT_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0132 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID == VESSEL_EXPORT_TYPE");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0132 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID != VESSEL_EXPORT_TYPE");
                                                                //VESSEL-L00-00-0132 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.Value?.ToString() == "AID")
                                                        {
                                                            //VESSEL-L00-00-0133
                                                            //Vessel_ Administrative_Characteristic/Value & Type=AID
                                                            //ListId= VESSEL_PUBLIC_AID_TYPE
                                                            if (relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.Value?.Value != null && relatedVesselTransportMeansApplicableVesselAdministrativeCharacteristic.TypeCode.listID?.ToString() == "VESSEL_PUBLIC_AID_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0133 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided and VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID == VESSEL_EXPORT_TYPE");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0133 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.Value.Value provided or VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID != VESSEL_EXPORT_TYPE");
                                                                //VESSEL-L00-00-0133 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0061 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic.TypeCode.listID provided or != FLUX_VESSEL_ADMIN_TYPE");
                                                        //VESSEL-L00-00-0061 - rejected
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselAdministrativeCharacteristic provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0063, VESSEL-L00-00-0035, VESSEL-L00-00-0144, VESSEL-L01-01-0079, VESSEL-L01-00-0633, VESSEL-L01-00-0685, VESSEL-L01-00-0686,
                                            //VESSEL-L01-00-0619, VESSEL-L01-00-0618, VESSEL-L01-01-0080, VESSEL-L01-01-0083, VESSEL-L01-01-0084, VESSEL-L01-01-0085, VESSEL-L01-01-0086,
                                            //VESSEL-L01-01-0087, VESSEL-L01-01-0088, VESSEL-L01-01-0089, VESSEL-L01-01-0100, VESSEL-L00-00-0064, VESSEL-L01-01-0102, VESSEL-L01-01-0067,
                                            //VESSEL-L01-01-0070, VESSEL-L01-01-0071, VESSEL-L01-01-0072, VESSEL-L01-01-0073, VESSEL-L01-01-0074, VESSEL-L01-01-0075, VESSEL-L01-01-0076,
                                            //VESSEL-L01-01-0077, VESSEL-L00-00-0064, VESSEL-L01-01-0078, VESSEL-L01-01-0079, VESSEL-L00-00-0036, VESSEL-L01-00-0689, VESSEL-L01-00-0688,
                                            //VESSEL-L00-00-0067, VESSEL-L01-00-0617, VESSEL-L01-00-0615, VESSEL-L01-00-0614, VESSEL-L01-00-0616, VESSEL-L01-00-0616, VESSEL-L01-00-0688,
                                            //VESSEL-L00-00-0144, VESSEL-L01-00-0687
                                            #region VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty != null)
                                            {
                                                foreach (var relatedVesselTransportMeansSpecifiedContactParty in vesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty)
                                                {
                                                    if (relatedVesselTransportMeansSpecifiedContactParty.RoleCode != null)
                                                    {
                                                        foreach (var specifiedContactPartyRoleCode in relatedVesselTransportMeansSpecifiedContactParty.RoleCode)
                                                        {
                                                            //VESSEL-L00-00-0063
                                                            //Contact_ Party /Role
                                                            //ListId= FLUX_CONTACT_ROLE
                                                            if (specifiedContactPartyRoleCode.listID?.ToString() == "FLUX_CONTACT_ROLE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0063 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode.listID provided and == FLUX_CONTACT_ROLE");

                                                                System.Console.WriteLine("VESSEL-L00-00-0035 | TODO | Check DB - FLUX_CONTACT_ROLE");
                                                                //VESSEL-L00-00-0035
                                                                //Contact_ Party /Role
                                                                //Code from the specified list
                                                                //TODO: check DB nomenclature - FLUX_CONTACT_ROLE for Vesselreport.VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode.Value

                                                                if (specifiedContactPartyRoleCode.Value == "OWNER")
                                                                {
                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedContactPerson != null)
                                                                    {
                                                                        foreach (var specifiedContactPartySpecifiedContactPerson in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedContactPerson)
                                                                        {
                                                                            //VESSEL-L01-01-0067
                                                                            //Owner Name
                                                                            //Length <= 100 characters
                                                                            string ownerName = specifiedContactPartySpecifiedContactPerson.GivenName.Value + specifiedContactPartySpecifiedContactPerson.MiddleName.Value + specifiedContactPartySpecifiedContactPerson.FamilyName.Value;
                                                                            if (ownerName.Length <= 100)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0067 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson provided and length <= 100");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0067 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson provided or length > 100");
                                                                                //VESSEL-L01-01-0067 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson OWNER provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress != null)
                                                                    {
                                                                        foreach (var specifiedContactPartySpecifiedStructuredAddress in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress)
                                                                        {

                                                                            //VESSEL-L01-01-0070
                                                                            //Street
                                                                            //Length <= 256 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value?.Length <= 256)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0070 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided and length <= 256");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0070 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided or length > 256");
                                                                                //VESSEL-L01-01-0070 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0071
                                                                            //Street box
                                                                            //Length <= 25 characters
                                                                            //#Q Is PostOfficeBox the StreetBox?
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value != null && specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value?.Length <= 25)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0071 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided and length <= 25");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0071 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided or length > 25");
                                                                                //VESSEL-L01-01-0071 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0072
                                                                            //City
                                                                            //Length <= 100 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.CityName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.CityName?.Value?.Length <= 100)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0072 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided and length <= 100");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0072 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided or length > 100");
                                                                                //VESSEL-L01-01-0072 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0073
                                                                            //Post code
                                                                            //Length <= 25 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value != null && specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value?.Length <= 25)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0073 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided and length <= 25");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0073 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided or length > 25");
                                                                                //VESSEL-L01-01-0073 - rejected
                                                                            }

                                                                            System.Console.WriteLine("VESSEL-L01-01-0074 | TODO | Check DB - TERRITORY OWNER");
                                                                            //VESSEL-L01-01-0074
                                                                            //Country of the owner
                                                                            //Code from the specified list
                                                                            //TODO: Check DB nomenclature - TERRITORY for specifiedContactPartySpecifiedStructuredAddress.CountryID.Value OWNER
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OWNER provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.TelephoneTelecommunicationCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyTelephoneTelecommunicationCommunication in relatedVesselTransportMeansSpecifiedContactParty.TelephoneTelecommunicationCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0075
                                                                            //Phone number
                                                                            //Length <= 30 characters
                                                                            if (specifiedContactPartyTelephoneTelecommunicationCommunication.CompleteNumber?.Value != null && specifiedContactPartyTelephoneTelecommunicationCommunication.CompleteNumber?.Value?.Length <= 30)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0075 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication.CompleteNumber.Value OWNER provided and length <= 30");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0075 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication.CompleteNumber.Value OWNER provided or length > 30");
                                                                                //VESSEL-L01-01-0075 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication OWNER provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.FaxTelecommunicationCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyFaxTelecommunicationCommunication in relatedVesselTransportMeansSpecifiedContactParty.FaxTelecommunicationCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0076
                                                                            //Fax number
                                                                            //Length <= 30 characters
                                                                            if (specifiedContactPartyFaxTelecommunicationCommunication.CompleteNumber?.Value != null && specifiedContactPartyFaxTelecommunicationCommunication.CompleteNumber?.Value?.Length <= 30)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0076 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication.CompleteNumber.Value OWNER provided and length <= 30");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0076 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication.CompleteNumber.Value OWNER provided or length > 30");
                                                                                //VESSEL-L01-01-0076 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication OWNER provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyURIEmailCommunication in relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0077
                                                                            //Email
                                                                            //Length <= 50 characters
                                                                            if (specifiedContactPartyURIEmailCommunication.URIID?.Value != null && specifiedContactPartyURIEmailCommunication.URIID?.Value?.Length <= 50)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0077 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value OWNER provided and length <= 50");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0077 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value OWNER provided or length > 50");
                                                                                //VESSEL-L01-01-0077 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication OWNER provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyNationalityCountryID in relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID)
                                                                        {
                                                                            //VESSEL-L00-00-0064
                                                                            //Contact_ Party /Nationality Country
                                                                            //ListId= TERRITORY 
                                                                            //#Q Cannot find listId in NationalityCountryID, but has schemeID?
                                                                            if (specifiedContactPartyNationalityCountryID.schemeID?.ToString() == "TERRITORY")
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L00-00-0063 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.schemeID OWNER provided and == TERRITORY");

                                                                                System.Console.WriteLine("VESSEL-L01-01-0078 | TODO | Check DB - TERRITORY OWNER");
                                                                                //VESSEL-L01-01-0078
                                                                                //Owner Nationality
                                                                                //Code from the specified list
                                                                                //TODO: Check DB nomenclature - TERRITORY for VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.Value OWNER
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L00-00-0064 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.schemeID OWNER provided or != TERRITORY");
                                                                                //VESSEL-L00-00-0064 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID OWNER provided");
                                                                    }


                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.ID?.Where(w => w.schemeID == "IMO").Count() > 0)
                                                                    {
                                                                        //VESSEL-L01-01-0079
                                                                        //IMO company number of the owner
                                                                        //Length = 7 characters
                                                                        if (relatedVesselTransportMeansSpecifiedContactParty.ID.Where(w => w.schemeID == "IMO").All(a => a.Value.Length == 7))
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-01-0079 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OWNER provided for all shemeID=IMO and length == 7");
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-01-0079 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OWNER provided for one or more shemeID=IMO or length != 7");
                                                                            //VESSEL-L01-01-0079 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OWNER provided for one or more shemeID=IMO or length != 7");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.RoleCode OWNER provided");
                                                                }

                                                                if (specifiedContactPartyRoleCode.Value == "OPERATOR")
                                                                {
                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedContactPerson != null)
                                                                    {
                                                                        foreach (var specifiedContactPartySpecifiedContactPerson in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedContactPerson)
                                                                        {

                                                                            //VESSEL-L01-01-0080
                                                                            //Owner Name
                                                                            //Length <= 100 characters
                                                                            string operatorName = specifiedContactPartySpecifiedContactPerson.GivenName.Value + specifiedContactPartySpecifiedContactPerson.MiddleName.Value + specifiedContactPartySpecifiedContactPerson.FamilyName.Value;
                                                                            if (operatorName.Length <= 100)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0080 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson OPERATOR provided and length <= 100");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0080 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson OPERATOR provided or length > 100");
                                                                                //VESSEL-L01-01-0080 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress != null)
                                                                    {
                                                                        foreach (var specifiedContactPartySpecifiedStructuredAddress in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress)
                                                                        {

                                                                            //VESSEL-L01-01-0083
                                                                            //Street
                                                                            //Length <= 256 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value?.Length <= 256)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0083 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided and length <= 256");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0083 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided or length > 256");
                                                                                //VESSEL-L01-01-0083 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0084
                                                                            //Street box
                                                                            //Length <= 25 characters
                                                                            //#Q Is PostOfficeBox the StreetBox?
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value != null && specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value?.Length <= 25)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0084 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided and length <= 25");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0084 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided or length > 25");
                                                                                //VESSEL-L01-01-0084 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0085
                                                                            //City
                                                                            //Length <= 100 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.CityName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.CityName?.Value?.Length <= 100)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0085 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided and length <= 100");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0085 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided or length > 100");
                                                                                //VESSEL-L01-01-0085 - rejected
                                                                            }

                                                                            //VESSEL-L01-01-0086
                                                                            //Post code
                                                                            //Length <= 25 characters
                                                                            if (specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value != null && specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value?.Length <= 25)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0086 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided and length <= 25");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0086 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided or length > 25");
                                                                                //VESSEL-L01-01-0086 - rejected
                                                                            }

                                                                            System.Console.WriteLine("VESSEL-L01-01-0087 | TODO | Check DB - TERRITORY OPERATOR");
                                                                            //VESSEL-L01-01-0087
                                                                            //Country of the owner
                                                                            //Code from the specified list
                                                                            //TODO: Check DB nomenclature - TERRITORY for specifiedContactPartySpecifiedStructuredAddress.CountryID.Value OPERATOR
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.TelephoneTelecommunicationCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyTelephoneTelecommunicationCommunication in relatedVesselTransportMeansSpecifiedContactParty.TelephoneTelecommunicationCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0088
                                                                            //Phone number
                                                                            //Length <= 30 characters
                                                                            if (specifiedContactPartyTelephoneTelecommunicationCommunication.CompleteNumber?.Value != null && specifiedContactPartyTelephoneTelecommunicationCommunication.CompleteNumber?.Value?.Length <= 30)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0088 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication.CompleteNumber.Value OPERATOR provided and length <= 30");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0088 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication.CompleteNumber.Value OPERATOR provided or length > 30");
                                                                                //VESSEL-L01-01-0088 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.TelephoneTelecommunicationCommunication OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.FaxTelecommunicationCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyFaxTelecommunicationCommunication in relatedVesselTransportMeansSpecifiedContactParty.FaxTelecommunicationCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0089
                                                                            //Fax number
                                                                            //Length <= 30 characters
                                                                            if (specifiedContactPartyFaxTelecommunicationCommunication.CompleteNumber?.Value != null && specifiedContactPartyFaxTelecommunicationCommunication.CompleteNumber?.Value?.Length <= 30)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0089 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication.CompleteNumber.Value OPERATOR provided and length <= 30");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0089 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication.CompleteNumber.Value OPERATOR provided or length > 30");
                                                                                //VESSEL-L01-01-0089 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.FaxTelecommunicationCommunication OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyURIEmailCommunication in relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication)
                                                                        {
                                                                            //VESSEL-L01-01-0100
                                                                            //Email
                                                                            //Length <= 50 characters
                                                                            if (specifiedContactPartyURIEmailCommunication.URIID?.Value != null && specifiedContactPartyURIEmailCommunication.URIID?.Value?.Length <= 50)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0100 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value OPERATOR provided and length <= 50");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0100 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value OPERATOR provided or length > 50");
                                                                                //VESSEL-L01-01-0100 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyNationalityCountryID in relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID)
                                                                        {
                                                                            //VESSEL-L00-00-0064
                                                                            //Contact_ Party /Nationality Country
                                                                            //ListId= TERRITORY 
                                                                            //#Q Cannot find listId in NationalityCountryID, but has schemeID?
                                                                            if (specifiedContactPartyNationalityCountryID.schemeID?.ToString() == "TERRITORY")
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L00-00-0063 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.schemeID provided and == TERRITORY");

                                                                                System.Console.WriteLine("VESSEL-L01-01-0101 | TODO | Check DB - TERRITORY OPERATOR");
                                                                                //VESSEL-L01-01-0101
                                                                                //Owner Nationality
                                                                                //Code from the specified list
                                                                                //TODO: Check DB nomenclature - TERRITORY for VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.Value OPERATOR
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L00-00-0064 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.schemeID OPERATOR provided or != TERRITORY");
                                                                                //VESSEL-L00-00-0064 - rejected
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID OPERATOR provided");
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.ID?.Where(w => w.schemeID == "IMO").Count() > 0)
                                                                    {
                                                                        //VESSEL-L01-01-0102
                                                                        //IMO company number of the operator
                                                                        //Length = 7 characters
                                                                        if (relatedVesselTransportMeansSpecifiedContactParty.ID.Where(w => w.schemeID == "IMO").All(a => a.Value?.Length == 7))
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-01-0102 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OPERATOR provided for all shemeID=IMO and length == 7");

                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-01-0102 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OPERATOR provided for one or more shemeID=IMO or length != 7");
                                                                            //VESSEL-L01-01-0102 - rejected
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value OPERATOR provided for one or more shemeID=IMO or length != 7");

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.RoleCode OPERATOR provided");
                                                                }

                                                                if (specifiedContactPartyRoleCode.Value == "MASTER")
                                                                {
                                                                    //VESSEL-L01-00-0619
                                                                    //Master name
                                                                    //Length <= 100 characters max
                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.Name?.Value?.Length <= 100)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0619 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value MASTER provided and length <= 100");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0619 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value MASTER provided or length > 100");
                                                                        //VESSEL-L01-00-0619 - rejected
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyNationalityCountryID in relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0618 | TODO | Check DB - TERRITORY");
                                                                            //VESSEL-L01-00-0618
                                                                            //Master Nationality
                                                                            //Code from the specified list
                                                                            //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.Value in TERRITORY list 
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID MASTER provided");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.RoleCode MASTER provided");
                                                                }

                                                                if (specifiedContactPartyRoleCode.Value == "AGENT")
                                                                {
                                                                    //VESSEL-L01-00-0685
                                                                    //Agent name
                                                                    //Length <= 100 characters max
                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.Name?.Value?.Length <= 100)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0685 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value AGENT provided and length <= 100");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0685 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value AGENT provided or length > 100");
                                                                        //VESSEL-L01-00-0685 - rejected
                                                                    }

                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID != null)
                                                                    {
                                                                        foreach (var specifiedContactPartyNationalityCountryID in relatedVesselTransportMeansSpecifiedContactParty.NationalityCountryID)
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L01-00-0686 | TODO | Check DB - TERRITORY");
                                                                            //VESSEL-L01-00-0686
                                                                            //Agent nationality
                                                                            //Code from the specified list
                                                                            //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID.Value in TERRITORY list 
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.NationalityCountryID MASTER provided");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.RoleCode AGENT provided");
                                                                }

                                                                if (specifiedContactPartyRoleCode.Value == "CONSTRUCT")
                                                                {
                                                                    //VESSEL-L01-00-0633
                                                                    //Construction company name
                                                                    //Length <= 100 characters max
                                                                    if (relatedVesselTransportMeansSpecifiedContactParty.Name?.Value?.Length <= 100)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0633 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value CONSTRUCT provided and length <= 100");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0633 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.Name.Value CONSTRUCT provided or length > 100");
                                                                        //VESSEL-L01-00-0633 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.RoleCode CONSTRUCT provided");
                                                                }

                                                                if (relatedVesselTransportMeansSpecifiedContactParty.ID?.Where(w => w.schemeID == "IMO").Count() > 0)
                                                                {
                                                                    foreach (var specifiedContactPartyID in relatedVesselTransportMeansSpecifiedContactParty.ID)
                                                                    {
                                                                        //VESSEL-L00-00-0144
                                                                        //Vessel_Event & ContactParty/Identification
                                                                        //Numerical value
                                                                        if (int.TryParse(specifiedContactPartyID.Value?.ToString(), out _))
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L00-00-0144 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value IMO provided and is numerical");

                                                                            //VESSEL-L01-01-0079
                                                                            //IMO company number of the owner
                                                                            //Length = 7 characters
                                                                            if (specifiedContactPartyID.Value?.Length == 7)
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0079 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value IMO provided and length == 7");
                                                                            }
                                                                            else
                                                                            {
                                                                                System.Console.WriteLine("VESSEL-L01-01-0079 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value IMO provided or length != 7");
                                                                                //VESSEL-L01-01-0079 - rejected
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            System.Console.WriteLine("VESSEL-L00-00-0144 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value IMO provided or is not numerical");
                                                                            //VESSEL-L00-00-0144
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value provided for one or more shemeID=IMO or length != 7");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0063 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode.listID provided or != FLUX_CONTACT_ROLE");
                                                                //VESSEL-L00-00-0063 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode provided");
                                                    }



                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedUniversalCommunication != null)
                                                    {
                                                        foreach (var specifiedContactPartySpecifiedUniversalCommunication in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedUniversalCommunication)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0036 | TODO | Check DB - XXX");
                                                            //VESSEL-L00-00-0036
                                                            //Contact_ Party /Universal_ Communication /Channel
                                                            //Code from the specified list
                                                            //TODO: check DB nomenclature - XXX for Vesselreport.VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedUniversalCommunication.ChannelCode.Value

                                                            //VESSEL-L01-00-0689
                                                            //Complete Number
                                                            //Length <= 50 characters
                                                            if (specifiedContactPartySpecifiedUniversalCommunication.CompleteNumber?.Value?.Length <= 50)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0689 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedUniversalCommunication.CompleteNumber.Value provided and length <= 50");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0689 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedUniversalCommunication.CompleteNumber.Value provided or length > 50");
                                                                //VESSEL-L01-00-0689 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedUniversalCommunication provided");
                                                    }

                                                    if (relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication != null)
                                                    {
                                                        foreach (var specifiedContactPartyURIEmailCommunication in relatedVesselTransportMeansSpecifiedContactParty.URIEmailCommunication)
                                                        {
                                                            //VESSEL-L01-00-0688
                                                            //Email address
                                                            //Length <= 50 characters
                                                            if (specifiedContactPartyURIEmailCommunication.URIID?.Value != null && specifiedContactPartyURIEmailCommunication.URIID?.Value?.Length <= 50)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0688 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value provided and length <= 50");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0688 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication.URIID.Value provided or length > 50");
                                                                //VESSEL-L01-00-0688 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.URIEmailCommunication provided");
                                                    }


                                                    if (relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress != null)
                                                    {
                                                        foreach (var specifiedContactPartySpecifiedStructuredAddress in relatedVesselTransportMeansSpecifiedContactParty.SpecifiedStructuredAddress)
                                                        {
                                                            //VESSEL-L00-00-0067
                                                            //Structured_Address/Country
                                                            //ListId= TERRITORY
                                                            //#Q Cannot find listId in CountryID, but has schemeID?
                                                            if (specifiedContactPartySpecifiedStructuredAddress.CountryID?.schemeID?.ToString() == "TERRITORY")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0067 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.CountryID.schemeID provided and == TERRITORY");

                                                                System.Console.WriteLine("VESSEL-L01-00-0617 | TODO | Check DB - TERRITORY");
                                                                //VESSEL-L01-00-0617
                                                                //Country of the address
                                                                //Code from the specified list
                                                                //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.CountryID.Value in TERRITORY list 
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0067 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.CountryID.schemeID provided or != TERRITORY");
                                                                //VESSEL-L00-00-0067 - rejected
                                                            }

                                                            //VESSEL-L01-00-0615
                                                            //City
                                                            //Length <= 100 characters
                                                            if (specifiedContactPartySpecifiedStructuredAddress.CityName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.CityName?.Value?.Length <= 100)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0615 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.CityName.Value provided and length <= 100");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0615 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.CityName.Value provided or length > 100");
                                                                //VESSEL-L01-00-0615 - rejected
                                                            }

                                                            //VESSEL-L01-00-0614
                                                            //Street box
                                                            //Length <= 25 characters
                                                            //#Q Is PostOfficeBox the StreetBox?
                                                            if (specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value != null && specifiedContactPartySpecifiedStructuredAddress.PostOfficeBox?.Value?.Length <= 25)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0614 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.PostOfficeBox.Value provided and length <= 25");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0614 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.PostOfficeBox.Value provided or length > 25");
                                                                //VESSEL-L01-00-0614 - rejected
                                                            }

                                                            //VESSEL-L01-00-0616
                                                            //Post code
                                                            //Length <= 25 characters
                                                            if (specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value != null && specifiedContactPartySpecifiedStructuredAddress.Postcode?.Value?.Length <= 25)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0616 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.Postcode.Value provided and length <= 25");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0616 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.Postcode.Value provided or length > 25");
                                                                //VESSEL-L01-00-0616 - rejected
                                                            }

                                                            //VESSEL-L01-00-0616
                                                            //Street name
                                                            //Length <= 256 characters
                                                            if (specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value?.Length <= 256)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0616 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.StreetName.Value provided and length <= 256");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0616 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.StreetName.Value provided or length > 256");
                                                                //VESSEL-L01-00-0616 - rejected
                                                            }

                                                            //VESSEL-L01-00-0688
                                                            //Email address
                                                            //Length <= 50 characters
                                                            if (specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value != null && specifiedContactPartySpecifiedStructuredAddress.StreetName?.Value?.Length <= 50)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0688 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.StreetName.Value provided and length <= 50");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0688 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress.StreetName.Value provided or length > 50");
                                                                //VESSEL-L01-00-0688 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedStructuredAddress provided");
                                                    }

                                                    if (relatedVesselTransportMeansSpecifiedContactParty.ID != null)
                                                    {
                                                        foreach (var specifiedContactPartyID in relatedVesselTransportMeansSpecifiedContactParty.ID)
                                                        {
                                                            if (specifiedContactPartyID.schemeID?.ToString() == "IMO")
                                                            {
                                                                //VESSEL-L00-00-0144
                                                                //Vessel_Event & ContactParty/Identification
                                                                //Numerical value
                                                                //#Q As we are already in the VesselEvent (so it is present), do we need additional check for it or chest a validation for numeric value?
                                                                if (int.TryParse(specifiedContactPartyID.Value?.ToString(), out _))
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0144 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value provided for shemeID=IMO and is numeric");

                                                                    //VESSEL-L01-00-0687
                                                                    //IMO company number
                                                                    //Length = 7 characters
                                                                    if (specifiedContactPartyID.Value?.Length == 7)
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0687 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value provided for shemeID=IMO and length == 7");
                                                                    }
                                                                    else
                                                                    {
                                                                        System.Console.WriteLine("VESSEL-L01-00-0687 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value provided for shemeID=IMO or length != 7");
                                                                        //VESSEL-L01-00-0687 - rejected
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L00-00-0144 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID.Value provided for shemeID=IMO or is not numeric");
                                                                    //VESSEL-L00-00-0144 - rejected
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty.ID provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedContactParty provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0062, VESSEL-L00-00-0030, VESSEL-L00-00-0134, VESSEL-L01-01-0057, VESSEL-L01-00-0690
                                            #region VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic
                                            if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic != null)
                                            {
                                                foreach (var relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic)
                                                {
                                                    //VESSEL-L00-00-0062
                                                    //Vessel_ Technical_Characteristic/Type
                                                    //ListId= FLUX_VESSEL_TECH_TYPE
                                                    if (relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.TypeCode?.listID?.ToString() == "FLUX_VESSEL_TECH_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0062 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.TypeCode.listID provided and == FLUX_VESSEL_TECH_TYPE");

                                                        System.Console.WriteLine("VESSEL-L00-00-0062 | TODO | Check DB - FLUX_VESSEL_TECH_TYPE");
                                                        //VESSEL-L00-00-0030
                                                        //Vessel_ Technical_Characteristic/Type
                                                        //Code from the specified list
                                                        //TODO: check DB nomenclature - FLUX_VESSEL_TECH_TYPE if Vesselreport.VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.TypeCode.Value exists

                                                        if (relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.ValueCode?.Value?.ToString() != "" && relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.TypeCode.Value?.ToString() == "HULL")
                                                        {
                                                            //VESSEL-L00-00-0134
                                                            //Vessel_ Technical_Characteristic/value & Type=HULL
                                                            //ListId= VESSEL_HULL_TYPE
                                                            if (relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.ValueCode.listID?.ToString() == "VESSEL_HULL_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0134 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.ValueCode and TypeCode.Value == HULL provided with ListId == VESSEL_HULL_TYPE");

                                                                System.Console.WriteLine("VESSEL-L01-01-0057 | TODO | Check DB - VESSEL_HULL_TYPE");
                                                                //VESSEL-L01-01-0057
                                                                //Hull Material
                                                                //Code from a list of reference: "VESSEL_HULL_TYPE" code list
                                                                //TODO: Check DB nomenclature for relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.ValueCode.Value - code from VESSEL_HULL_TYPE list
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0134 | OK | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.ValueCode and TypeCode.Value == HULL provided with ListId == VESSEL_HULL_TYPE");
                                                                //VESSEL-L00-00-0134 - rejected
                                                            }
                                                        }

                                                        if (relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.TypeCode.Value == "PROCESS_CLASS")
                                                        {
                                                            //VESSEL-L01-00-0690
                                                            //Processing class
                                                            //Length <= 300 characters max
                                                            if (relatedVesselTransportMeansApplicableVesselTechnicalCharacteristic.Value.Value.Length <= 300)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0690 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.Value.Value provided with length <= 300");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0690 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.Value.Value provided or length > 300");
                                                                //VESSEL-L01-00-0690 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0062 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic.TypeCode.listID provided or != FLUX_VESSEL_TECH_TYPE");
                                                        //VESSEL-L00-00-0062 - rejected
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselTechnicalCharacteristic provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0135, VESSEL-L01-00-0512, VESSEL-L00-00-0084, VESSEL-L00-00-0085, VESSEL-L01-00-0679, VESSEL-L00-00-0138, VESSEL-L01-00-0518,
                                            //VESSEL-L01-00-0680, VESSEL-L00-00-0086, VESSEL-L00-00-0139
                                            #region VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic
                                            if (vesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic != null)
                                            {
                                                //#Q How to check for each TypeCode = FISH_HOLD, FREEZ, STR_GEN, especially for capacity

                                                foreach (var relatedVesselTransportMeansApplicableVesselStorageCharacteristic in vesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic)
                                                {
                                                    if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.TypeCode != null)
                                                    {
                                                        foreach (var applicableVesselStorageCharacteristicTypeCode in relatedVesselTransportMeansApplicableVesselStorageCharacteristic.TypeCode)
                                                        {
                                                            //VESSEL-L00-00-0135
                                                            //Vessel_Storage_Char/Type
                                                            //ListId=STORAGE_TYPE
                                                            if (applicableVesselStorageCharacteristicTypeCode.listID?.ToString() == "STORAGE_TYPE")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0135 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TypeCode.listID provided and == STORAGE_TYPE");

                                                                System.Console.WriteLine("VESSEL-L01-00-0512 | TODO | Check DB - STORAGE_TYPE");
                                                                //VESSEL-L01-00-0512
                                                                //Storage Method
                                                                //Code from the specified list
                                                                //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.Value in STORAGE_TYPE list

                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0135 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TypeCode.listID provided or != STORAGE_TYPE");
                                                                //VESSEL-L00-00-0135 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TypeCode provided");
                                                    }

                                                    if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.CapacityValueMeasure != null)
                                                    {
                                                        foreach (var applicableVesselStorageCharacteristicCapacityValueMeasure in relatedVesselTransportMeansApplicableVesselStorageCharacteristic.CapacityValueMeasure)
                                                        {
                                                            //VESSEL-L00-00-0084
                                                            //Vessel_Storage_Char/Capacity_value
                                                            //Numerical value
                                                            //#Q Already decimal
                                                            if (applicableVesselStorageCharacteristicCapacityValueMeasure.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0084 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.CapacityValueMeasure.Value (decimal) provided and numerical");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0084 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.CapacityValueMeasure.Value (decimal) provided or not numerical");
                                                                //VESSEL-L00-00-0084 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.CapacityValueMeasure provided");
                                                    }

                                                    if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.TemperatureValueMeasure != null)
                                                    {
                                                        foreach (var applicableVesselStorageCharacteristicTemperatureValueMeasure in relatedVesselTransportMeansApplicableVesselStorageCharacteristic.TemperatureValueMeasure)
                                                        {
                                                            //VESSEL-L00-00-0085
                                                            //Vessel_Storage_Char/Temperatur e_value
                                                            //Numerical value
                                                            if (applicableVesselStorageCharacteristicTemperatureValueMeasure.Value != null)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0085 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.Value (decimal) provided");

                                                                //VESSEL-L01-00-0679
                                                                //Storage Temperature
                                                                //Format: XXX.YY with 2 optional decimals
                                                                //#Q Add decimal value format check
                                                                if (true)
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0679 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.Value (decimal) format XXX.YY valid");
                                                                }
                                                                else
                                                                {
                                                                    System.Console.WriteLine("VESSEL-L01-00-0679 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.Value (decimal) format XXX.YY not valid");
                                                                    //VESSEL-L01-00-0679 - rejected
                                                                }
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0085 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.Value (decimal) provided");
                                                                //VESSEL-L00-00-0085 - rejected
                                                            }

                                                            //VESSEL-L00-00-0138
                                                            //Vessel_Storage_Char/Temperatur e_value
                                                            //Numerical value
                                                            if (applicableVesselStorageCharacteristicTemperatureValueMeasure.unitCode == "CEL")
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0138 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.unitCode provided and == CEL");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L00-00-0138 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure.unitCode provided or != CEL");
                                                                //VESSEL-L00-00-0138 - rejected
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.TemperatureValueMeasure provided");
                                                    }

                                                    if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.UnitValueQuantity != null)
                                                    {
                                                        //VESSEL-L01-00-0518
                                                        //Storage Units
                                                        //no negative value
                                                        if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.UnitValueQuantity.Value > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0518 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value provided and > 0");

                                                            //VESSEL-L01-00-0680
                                                            //Storage Units
                                                            //Format: XXXXX without decimals
                                                            //#Q Add decimal value format check
                                                            if (true)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0680 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value format XXXXX valid");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0680 | REJECTED | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value format XXXXX not valid");
                                                                //VESSEL-L01-00-0680 - rejected
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0518 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value provided or < 0");
                                                            //VESSEL-L01-00-0518 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity provided");
                                                    }

                                                    if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.UnitValueQuantity != null)
                                                    {
                                                        //VESSEL-L00-00-0086
                                                        //Vessel_Storage_Char/Unit_value
                                                        //Numerical value
                                                        if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.UnitValueQuantity?.Value != null)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0086 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value (decimal) provided");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0086 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.Value (decimal) provided");
                                                            //VESSEL-L00-00-0086 - rejected
                                                        }

                                                        //VESSEL-L00-00-0139
                                                        //Vessel_Storage_Char/Unit_value
                                                        //The unit must be C62
                                                        if (relatedVesselTransportMeansApplicableVesselStorageCharacteristic.UnitValueQuantity?.unitCode?.ToString() == "C62")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0139 | OK | VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.unitCode provided and == C62");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0139 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity.unitCode provided or != C62");
                                                            //VESSEL-L00-00-0139 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic.UnitValueQuantity provided");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.ApplicableVesselStorageCharacteristic provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0140,VESSEL-L01-00-0681, VESSEL-L01-00-0519
                                            #region VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture
                                            if (vesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture != null)
                                            {
                                                foreach (var relatedVesselTransportMeansIllustrateFLUXPicture in vesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture)
                                                {
                                                    //VESSEL-L00-00-0140
                                                    //FLUX Picture/Type
                                                    //ListId=VESSEL_PHOTO_TYPE
                                                    if (relatedVesselTransportMeansIllustrateFLUXPicture.TypeCode?.listID?.ToString() == "VESSEL_PHOTO_TYPE")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0140 | OK | VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture.TypeCode.listID provided and == VESSEL_PHOTO_TYPE");

                                                        System.Console.WriteLine("VESSEL-L01-00-0681 | TODO | Check DB - VESSEL_PHOTO_TYPE");
                                                        //VESSEL-L01-00-0681
                                                        //Vessel photo type
                                                        //Code from the specified list
                                                        //TODO: Check DB nomenclature - VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture.TypeCode.Value is from VESSEL_PHOTO_TYPE list
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0140 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture.TypeCode.listID provided or != VESSEL_PHOTO_TYPE");
                                                        //VESSEL-L00-00-0140 - rejected
                                                    }

                                                    //VESSEL-L01-00-0519
                                                    //Vessel Photo
                                                    //max.size = 10Mb
                                                    //#Q How to check the picture max.size?
                                                    if (relatedVesselTransportMeansIllustrateFLUXPicture.DigitalImageBinaryObject != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0519 | OK | VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture.DigitalImageBinaryObject provided and max.size = 10Mb");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L01-00-0519 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture.DigitalImageBinaryObject provided or max.size != 10Mb");
                                                        //VESSEL-L01-00-0519 - rejected
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.IllustrateFLUXPicture provided");
                                            }
                                            #endregion

                                            //VESSEL-L00-00-0087, VESSEL-L01-00-0536, VESSEL-L01-00-0682, VESSEL-L00-00-0141, VESSEL-L00-00-0088, VESSEL-L01-00-0538, VESSEL-L01-00-0683,
                                            //VESSEL-L00-00-0142, VESSEL-L00-00-0089, VESSEL-L01-00-0540, VESSEL-L01-00-0684, VESSEL-L00-00-0143
                                            #region VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew != null)
                                            {
                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity != null)
                                                {
                                                    //VESSEL-L00-00-0087
                                                    //Vessel_Crew/Size
                                                    //Numerical value
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0087 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided");

                                                        //VESSEL-L01-00-0536
                                                        //Crew size
                                                        //no negative value
                                                        if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0536 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided and positive");

                                                            //VESSEL-L01-00-0682
                                                            //Crew size
                                                            //Format: XXX without decimal
                                                            //#Q Add decimal value format check
                                                            if (true)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0682 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided and format XXX valid");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0682 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided or format XXX not valid");
                                                                //VESSEL-L01-00-0682 - rejected
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0536 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided or negative");
                                                            //VESSEL-L01-00-0536 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0087 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.Value (decimal) provided");
                                                        //VESSEL-L00-00-0087 - rejected
                                                    }

                                                    //VESSEL-L00-00-0141
                                                    //Vessel_Crew/Size
                                                    //The unit must be C62
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.unitCode?.ToString() == "C62")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0141 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.unitCode provided and == C62");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0141 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity.unitCode provided or != C62");
                                                        //VESSEL-L00-00-0141 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MemberQuantity provided");
                                                }

                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity != null)
                                                {
                                                    //VESSEL-L00-00-0088
                                                    //Vessel_Crew/Maximum_size
                                                    //Numerical value
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0088 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided");

                                                        //VESSEL-L01-00-0538
                                                        //Crew size, maximum
                                                        //no negative value
                                                        if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0538 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided and positive");

                                                            //VESSEL-L01-00-0683
                                                            //Crew size, maximum
                                                            //Format: XXX without decimal
                                                            //#Q Add decimal value format check
                                                            if (true)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0683 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided and format XXX valid");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0683 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided or format XXX not valid");
                                                                //VESSEL-L01-00-0683 - rejected
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0538 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided or negative");
                                                            //VESSEL-L01-00-0538 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0088 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value (decimal) provided");
                                                        //VESSEL-L00-00-0088 - rejected
                                                    }

                                                    //VESSEL-L00-00-0142
                                                    //Vessel_Crew/Maximum_size
                                                    //The unit must be C62
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.unitCode?.ToString() == "C62")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0142 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.unitCode provided and == C62");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0142 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.unitCode provided or != C62");
                                                        //VESSEL-L00-00-0142 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity provided");
                                                }

                                                if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity != null)
                                                {
                                                    //VESSEL-L00-00-0089
                                                    //Vessel_Crew/Minimum_size
                                                    //Numerical value
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity?.Value != null)
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0089 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided");

                                                        //VESSEL-L01-00-0540
                                                        //Crew size, minimum
                                                        //no negative value
                                                        if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MaximumSizeQuantity.Value > 0)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0540 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided and positive");

                                                            //VESSEL-L01-00-0684
                                                            //Crew size, minimum
                                                            //Format: XXX without decimal
                                                            //#Q Add decimal value format check
                                                            if (true)
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0684 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided and format XXX valid");
                                                            }
                                                            else
                                                            {
                                                                System.Console.WriteLine("VESSEL-L01-00-0684 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided or format XXX not valid");
                                                                //VESSEL-L01-00-0684 - rejected
                                                            }
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0540 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided or negative");
                                                            //VESSEL-L01-00-0540 - rejected
                                                        }
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0089 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.Value (decimal) provided");
                                                        //VESSEL-L00-00-0089 - rejected
                                                    }

                                                    //VESSEL-L00-00-0143
                                                    //Vessel_Crew/Minimum_size
                                                    //The unit must be C62
                                                    if (vesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity?.unitCode?.ToString() == "C62")
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0143 | OK | VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.unitCode provided and == C62");
                                                    }
                                                    else
                                                    {
                                                        System.Console.WriteLine("VESSEL-L00-00-0143 | REJECTED | No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity.unitCode provided or != C62");
                                                        //VESSEL-L00-00-0143 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew.MinimumSizeQuantity provided");
                                                }
                                            }
                                            else
                                            {
                                                System.Console.WriteLine("No VesselEvent.RelatedVesselTransportMeans.SpecifiedVesselCrew provided");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("No VesselEvent.RelatedTransportMeans provided");
                                        }
                                        #endregion

                                        //VESSEL-L00-00-0096, VESSEL-L00-00-0097, VESSEL-L01-00-0639, VESSEL-L00-00-0073, VESSEL-L01-00-0640, VESSEL-L01-00-0641, VESSEL-L01-00-0642, VESSEL-L01-00-0643
                                        #region VesselEvent.RelatedVesselHistoricalCharacteristic
                                        if (vesselEvent.RelatedVesselHistoricalCharacteristic != null)
                                        {
                                            foreach (var relatedVesselHistoricalCharacteristic in vesselEvent.RelatedVesselHistoricalCharacteristic)
                                            {
                                                //VESSEL-L00-00-0096
                                                //Vessel Historical_Characteristic/ Type
                                                //ListId=FLUX_VESSEL_HIST_CHAR
                                                if (relatedVesselHistoricalCharacteristic.TypeCode.listID?.ToString() == "FLUX_VESSEL_HIST_CHAR")
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0096 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.TypeCode.listID provided and == FLUX_VESSEL_HIST_CHAR");

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "FLAG")
                                                    {
                                                        //VESSEL-L00-00-0097
                                                        //Vessel_Historical_ Characteristic /Value & Type = "FLAG"
                                                        //ListId=TERRITORY
                                                        if (relatedVesselHistoricalCharacteristic.ValueCode?.listID?.ToString() == "TERRITORY")
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0097 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.ValueCode.listID provided and == TERRITORY");

                                                            System.Console.WriteLine("VESSEL-L01-00-0639 | TODO | Check DB - FLAG");
                                                            //VESSEL-L01-00-0639
                                                            //Previous Flag State
                                                            //Code from the specified list
                                                            //TODO: Check DB nomenclature for VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value in FLAG list
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0097 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.ValueCode.listID provided or != TERRITORY");
                                                            //VESSEL-L00-00-0097 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "DATE")
                                                    {
                                                        //VESSEL-L00-00-0073
                                                        //Vessel_Historical_ Characteristic /Value & Type = "DATE"
                                                        //Date type
                                                        if (relatedVesselHistoricalCharacteristic.ValueDateTime?.Item is DateTime)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0073 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.ValueDateTime.Item provided and is DateTime");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L00-00-0073 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.ValueDateTime.Item provided or is not DateTime");
                                                            //VESSEL-L00-00-0073 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "IRCS")
                                                    {
                                                        //VESSEL-L01-00-0640
                                                        //Previous IRCS
                                                        //Length <= 7 characters max
                                                        if (relatedVesselHistoricalCharacteristic.Value?.Value?.Length <= 7)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0640 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value IRCS provided and length <= 7");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0640 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value IRCS provided or length > 7");
                                                            //VESSEL-L01-00-0640 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "VESSEL_NAME")
                                                    {
                                                        //VESSEL-L01-00-0641
                                                        //Previous Vessel Name
                                                        //Length <= 40 characters max
                                                        if (relatedVesselHistoricalCharacteristic.Value?.Value?.Length <= 40)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0641 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value VESSEL_NAME provided and length <= 40");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0641 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value VESSEL_NAME provided or length > 40");
                                                            //VESSEL-L01-00-0641 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "OWNER_NAME")
                                                    {
                                                        //VESSEL-L01-00-0642
                                                        //Previous Owner Name
                                                        //Length <= 100 characters max
                                                        if (relatedVesselHistoricalCharacteristic.Value?.Value?.Length <= 100)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0642 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value OWNER_NAME provided and length <= 100");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0642 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value OWNER_NAME provided or length > 100");
                                                            //VESSEL-L01-00-0642 - rejected
                                                        }
                                                    }

                                                    if (relatedVesselHistoricalCharacteristic.TypeCode.Value == "OWNER_ADDRESS")
                                                    {
                                                        //VESSEL-L01-00-0643
                                                        //Previous Owner Address
                                                        //Length <= 256 characters max
                                                        if (relatedVesselHistoricalCharacteristic.Value?.Value?.Length <= 256)
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0643 | OK | VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value OWNER_ADDRESS provided and length <= 256");
                                                        }
                                                        else
                                                        {
                                                            System.Console.WriteLine("VESSEL-L01-00-0643 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.Value.Value OWNER_ADDRESS provided or length > 256");
                                                            //VESSEL-L01-00-0643 - rejected
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    System.Console.WriteLine("VESSEL-L00-00-0096 | REJECTED | No VesselEvent.RelatedVesselHistoricalCharacteristic.TypeCode.listID provided or != FLUX_VESSEL_HIST_CHAR");
                                                    //VESSEL-L00-00-0096 - rejected
                                                }
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("No VesselEvent.RelatedVesselHistoricalCharacteristic provided");
                                        }
                                        #endregion
                                    }
                                }
                                else
                                {
                                    System.Console.WriteLine("VESSEL-L01-02-0041 | REJECTED | No VesselEvent provided");
                                    System.Console.WriteLine("VESSEL-L01-01-0008 | REJECTED | No VesselEvent provided");
                                    //VESSEL-L01-02-0041 - rejected
                                    //VESSEL-L01-01-0008 - rejected
                                }
                                #endregion

                                //VESSEL-L01-02-0007, VESSEL-L02-01-0008, VESSEL-L02-01-0007, VESSEL-L02-01-0010
                                #region Boolean Validations
                                //#Q Should this kind of validations be performed this way?

                                //VESSEL-L01-02-0007
                                //UVI & IRCS & registration number
                                //One of these identifiers is mandatory
                                if (hasUvi || hasIrcsValue || hasRegNbr)
                                {
                                    System.Console.WriteLine("VESSEL-L01-02-0007 | OK | At least one of UVI (" + hasUvi + ") || IRCS (" + hasIrcsValue + ") || REG_NBR (" + hasRegNbr + ") is provided");
                                }
                                else
                                {
                                    System.Console.WriteLine("VESSEL-L01-02-0007 | REJECTED | None of UVI (" + hasUvi + ") || IRCS (" + hasIrcsValue + ") || REG_NBR (" + hasRegNbr + ") is provided");
                                    //VESSEL-L01-02-0007 - rejected
                                }

                                if (hasIrcsBool)
                                    //VESSEL-L02-01-0008
                                    //IRCS Indicator & IRCS
                                    //If indicator is 'Y', value in IRCS
                                    if (hasIrcsValue)
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0008 | OK | Indicator is Y and IRCS value provided");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0008 | WARNING |  Indicator is Y, but no IRCS value provided");
                                        //VESSEL-L02-01-0008 - warning
                                    }
                                else
                                {
                                    //VESSEL-L02-01-0007
                                    //IRCS Indicator & IRCS
                                    //If indicator is 'N', no value in IRCS
                                    if (hasIrcsValue)
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0007 | WARNING | Indicator is N, but IRCS value provided");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0007 | OK | Indicator is N and no IRCS value provided");
                                        //VESSEL-L02-01-0007 - warning
                                    }
                                }

                                if (valueLOA >= 24)
                                {
                                    //VESSEL-L02-01-0010
                                    //IRCS & LOA
                                    //Mandatory for vessels >= 24m LOA
                                    if (hasIrcsValue)
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0010 | OK | IRCS value provided for LOA (" + valueLOA + ") >= 24m");
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L02-01-0010 | WARNING | No IRCS value provided for LOA (" + valueLOA + ") >= 24m");
                                        //VESSEL-L02-01-0010 - warning
                                    }
                                }
                                #endregion

                            }
                            else
                            {
                                System.Console.WriteLine("No Vesselreport provided");
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
