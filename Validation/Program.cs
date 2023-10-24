using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Validation.DBContext.MyConsoleApp.Models;
using Validation.FluxDomainsValidation.FluxACDRDomainValidation;
using Validation.FluxDomainsValidation.FluxFADomainValidation;
using Validation.FluxDomainsValidation.FluxFLAPDomainValidation;
using Validation.FluxDomainsValidation.FluxISDomainValidation;
using Validation.FluxDomainsValidation.FluxMDMDomainValidation;
using Validation.FluxDomainsValidation.FluxSalesDomainValidation;
using Validation.FluxDomainsValidation.FluxVesselDomainValidation;
using Validation.FluxDomainsValidation.FluxVPDomainValidation;

namespace Validation
{
    class Program
    {
        static void Main(string[] args)
        {

            //// UUID Check is valid
            /////^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$
            //if (System.Text.RegularExpressions.Regex.Match("d0979e76-8950-42bb-9bfe-1831def8ac5a", "^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$").Success)
            //{ 
            //}

            var IARADbOptions = new DbContextOptionsBuilder<FVMS22DbContext>()
                .UseNpgsql("Host=95.43.208.107;Port=8432;Database=FVMS22DB;Username=postgres;Password=pl@$Tic_iaR@")
                .Options;

            using (var mContext = new FVMS22DbContext(IARADbOptions))
            {
                // Само пример за ползване
                // var res = mContext.MDR_FLUX_GP_Party.ToList();
                // Пример
                //var res1 = mContext.FLUXInnerMsg.OrderByDescending(x => x.ReceivedFDT).Where(x => x.StateISS == 13).ToList();
                // Достъп до номенклатура

                /*
                var nom1 = mContext.MDR_Member_State.FirstOrDefault(x => x.Country == "BGR");
                if (nom1 != null)
                {
                    // ok
                }
                else
                {
                    // няма такъв елемент в номенклатурата
                }
                */

                string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                //This will strip just the working path name:
                //C:\Program Files\MyApplication
                string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

                //Switch Off FA Domain debug
                bool FADomainDebug = false;
                List<string> SysList = new List<string>();
                if (FADomainDebug)
                {
                    FADomainValidation faDomainValidation = new FADomainValidation();
                    faDomainValidation.FADomainValidate(strWorkPath: strWorkPath, SysList: SysList, mContext: mContext);
                }

                bool VesselDomainDebug = true;
                if (VesselDomainDebug)
                {
                    VesselDomainValidation vesselDomainValidation = new VesselDomainValidation();
                    vesselDomainValidation.VesselDomainValidate(strWorkPath: strWorkPath, mContext: mContext);
                }

                bool SalesDomainDebug = false;
                if (SalesDomainDebug)
                {
                    SalesDomainValidation salesDomainValidation = new SalesDomainValidation();
                    salesDomainValidation.SalesDomainValidate(strWorkPath: strWorkPath);
                }

                bool acdrDomainDebug = false;
                if (acdrDomainDebug)
                {
                    AcdrDomainValidation acdrDomainValidation = new AcdrDomainValidation();
                    acdrDomainValidation.ACDRDomainValidate(strWorkPath: strWorkPath);
                }

                bool flapDomainDebug = false;
                if (flapDomainDebug)
                {
                    FLAPDomainValidation flapDomainValidation = new FLAPDomainValidation();
                    flapDomainValidation.FLAPDomainValidate(strWorkPath: strWorkPath);
                }

                bool isDomainDebug = false;
                if (isDomainDebug)
                {
                    ISDomainValidation isDomainValidation = new ISDomainValidation();
                    isDomainValidation.ISDomainValidate(strWorkPath: strWorkPath);
                }

                bool mdmDomainDebug = false;
                if (mdmDomainDebug)
                {
                    MDMDomainValidation mdmDomainValidation = new MDMDomainValidation();
                    mdmDomainValidation.MDMDomainValidate(strWorkPath: strWorkPath);
                }

                bool vpDomainDebug = false;
                if (vpDomainDebug)
                {
                    VPDomainValidation vpDomainValidation = new VPDomainValidation();
                    vpDomainValidation.VPDomainValidate(strWorkPath: strWorkPath);
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

            }// end using DBContext
        }
    }
}