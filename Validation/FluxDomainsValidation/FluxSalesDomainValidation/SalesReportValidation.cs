﻿using System;
using System.Collections.Generic;
using System.Linq;
using ScortelApi.Models.FLUX;
using System.Text.RegularExpressions;

namespace Validation.FluxDomainsValidation.FluxSalesDomainValidation
{
    class SalesReportValidation
    {
        public void SalesReportValidate(FLUXSalesReportMessageType SalesReport)
        {
            string valuePurposeCode = "";
            string valueSalesReportTypeCode = "";
            string valueClassCode = "";
            string valueUsageCode = "";
            string valueCFR = "";
            string valueVesselCountry = "";

            bool hasReferencedIdentification = false;
            bool isReportResultingFromQuery = false;

            DateTime dateTimeFluxReportDocumentCreation = new DateTime();
            DateTime dateTimeDelimitedPeriodStart = new DateTime();
            DateTime dateTimeSalesEventOccurance = new DateTime();

            List<decimal> listOfSalesPrices = new List<decimal>();

            #region SalesReport.FLUXReportDocument
            //SALE-L00-00-0001
            //FLUX_ ReportDocument
            //Must be present
            if (SalesReport.FLUXReportDocument != null)
            {
                Console.WriteLine("SALE-L00-00-0001 | OK | SalesReport.FLUXReportDocument provided");

                //SALE-L02-00-0001
                //FLUX_ ReportDocument
                //Only one report document
                //#Q Will this check satisfy the condition?
                if (SalesReport.FLUXReportDocument.GetType().IsArray == false)
                {
                    Console.WriteLine("SALE-L02-00-0001 | OK | SalesReport.FLUXReportDocument is not an array");

                    //SALE-L00-00-0010, SALE-L01-00-0010, SALE-L01-00-0011, SALE-L03-00-0010
                    #region SalesReport.FLUXReportDocument.ID
                    //SALE-L00-00-0010
                    //FLUX_ ReportDocument/Identification
                    //Must be present.
                    if (SalesReport.FLUXReportDocument.ID != null)
                    {
                        Console.WriteLine("SALE-L00-00-0010 | OK | SalesReport.FLUXReportDocument.ID provided");

                        foreach (var fluxReportDocumentId in SalesReport.FLUXReportDocument.ID)
                        {
                            //SALE-L01-00-0010
                            //FLUX_ ReportDocument/Identification
                            //Check attribute schemeID. Must be UUID.
                            if (fluxReportDocumentId.schemeID?.ToString() == "UUID")
                            {
                                Console.WriteLine("SALE-L01-00-0010 | OK | SalesReport.FLUXReportDocument.ID.schemeID provided and == UUID");

                                Console.WriteLine("SALE-L01-00-0011 | TODO | Check format - SalesReport.FLUXReportDocument.ID.Value - RFC4122");
                                //SALE-L01-00-0011 - error
                                //FLUX_ ReportDocument/Identification
                                //Check Format. Must be according to the specified schemeID.
                                //TODO: Check id UUID format - RFC4122

                                Console.WriteLine("SALE-L03-00-0010 | TODO | Check DB - SalesReport.FLUXReportDocument.ID.Value - unique");
                                //SALE-L03-00-0010 - warning
                                //FLUX_ ReportDocument/Identification
                                //The identification must be unique and not already exist
                                //If it exists already, the contents are considered identical and the message may be ignored by the receiving party.
                                //It is expected to return the same validation results.
                                //TODO: Check id UUID - unique
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0010 | ERROR | No SalesReport.FLUXReportDocument.ID.schemeID provided or != UUID");
                                //SALE-L01-00-0010 - error
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0010 | ERROR | No SalesReport.FLUXReportDocument.ID provided");
                        //SALE-L00-00-0010 - error
                    }
                    #endregion SalesReport.FLUXReportDocument.ID

                    //SALE-L00-00-0012, SALE-L01-00-0014, SALE-L01-00-0015, SALE-L01-00-0016, 
                    #region SalesReport.FLUXReportDocument.PurposeCode
                    //SALE-L00-00-0012
                    //FLUX_ ReportDocument/Purpose
                    //The code must be present
                    if (SalesReport.FLUXReportDocument.PurposeCode != null)
                    {
                        Console.WriteLine("SALE-L00-00-0012 | OK | SalesReport.FLUXReportDocument.PurposeCode provided");

                        //SALE-L01-00-0014
                        //FLUX_ ReportDocument/Purpose
                        //Check attribute listID. Must be FLUX_GP_PURPOSE
                        if (SalesReport.FLUXReportDocument.PurposeCode.listID?.ToString() == "FLUX_GP_PURPOSE")
                        {
                            Console.WriteLine("SALE-L01-00-0014 | OK | SalesReport.FLUXReportDocument.PurposeCode.listID provided and == FLUX_GP_PURPOSE");

                            Console.WriteLine("SALE-L01-00-0015 | TODO | Check DB - SalesReport.FLUXReportDocument.PurposeCode.Value - from FLUX_GP_PURPOSE list");
                            //SALE-L01-00-0015
                            //FLUX_ ReportDocument/Purpose
                            //Check code from the list listID.
                            //Todo: Check code - from FLUX_GP_PURPOSE

                            //SALE-L01-00-0016
                            //FLUX_ ReportDocument/Purpose
                            //Purpose code should not be 1 (cancellation)
                            if (SalesReport.FLUXReportDocument.PurposeCode.Value?.ToString() != "" && SalesReport.FLUXReportDocument.PurposeCode.Value?.ToString() != "1")
                            {
                                Console.WriteLine("SALE-L01-00-0016 | OK | SalesReport.FLUXReportDocument.PurposeCode.Value provided and != 1 (cancellation)");

                                valuePurposeCode = SalesReport.FLUXReportDocument.PurposeCode.Value.ToString();
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0016 | ERROR | No SalesReport.FLUXReportDocument.PurposeCode.Value provided or == 1 (cancellation)");
                                //SALE-L01-00-0016 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0014 | ERROR | No SalesReport.FLUXReportDocument.PurposeCode.listID provided or != FLUX_GP_PURPOSE");
                            //SALE-L01-00-0014 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0012 | ERROR | No SalesReport.FLUXReportDocument.PurposeCode provided");
                        //SALE-L00-00-0012 - error
                    }
                    #endregion SalesReport.FLUXReportDocument.PurposeCode

                    //SALE-L02-00-0010, SALE-L02-00-0011, SALE-L01-00-0008, SALE-L01-00-0009
                    #region SalesReport.FLUXReportDocument.ReferencedIdentification
                    if (valuePurposeCode == "3" || valuePurposeCode == "5")  //3: deletion; 5: correction
                    {
                        Console.WriteLine("SALE-L02-00-0010 | TODO | Check DB - SalesReport.FLUXReportDocument.ReferencedID.Value provided and of an existing Sales Report");
                        //SALE-L02-00-0010
                        //FLUX_ ReportDocument/Purpose, FLUX_ReportDocument/Referenced Identification
                        //If correction or deletion, the referenced identification must be provided and it must be the one of a an existing Sales Report message.
                        //#Q TODO: Check if SalesReport.FLUXReportDocument.ReferencedID.Value is one of an existing Sales Report

                        bool referencedIdValueIsExisting = true;
                        if (SalesReport.FLUXReportDocument.ReferencedID != null && referencedIdValueIsExisting)
                        {
                            Console.WriteLine("SALE-L02-00-0010 | OK | SalesReport.FLUXReportDocument.ReferencedID provided and of an existing Sales Report");

                            hasReferencedIdentification = true;
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0010 | WARNING | No SalesReport.FLUXReportDocument.ReferencedID provided or not of an existing Sales Report");
                            //SALE-L02-00-0010 - warning
                        }
                    }

                    //#Q Does 9 corresponds to creation?
                    if (valuePurposeCode == "9")  //9: original report
                    {
                        //SALE-L02-00-0011
                        //FLUX_ ReportDocument/Purpose, FLUX_ ReportDocument/ Referenced Identification
                        //If it is a new message (creation), the referenced identification, if provided, must be the one of a Query message

                        if (SalesReport.FLUXReportDocument.ReferencedID != null)
                        {
                            //#Q TODO: Check if the Referenced Identification is one of a Query message
                            Console.WriteLine("SALE-L02-00-0011 | TODO | Check DB - SalesReport.FLUXReportDocument.ReferencedID.Value provided and one of a Query message");

                            hasReferencedIdentification = true;
                        }
                    }

                    if (hasReferencedIdentification)
                    {
                        //SALE-L01-00-0008
                        //FLUX_ ReportDocument/Referenced Identification
                        //Check attribute schemeID. Must be UUID.
                        if (SalesReport.FLUXReportDocument.ReferencedID.schemeID?.ToString() == "UUID")
                        {
                            Console.WriteLine("SALE-L01-00-0008 | OK | SalesReport.FLUXReportDocument.ReferencedID.schemeID provided and == UUID");

                            Console.WriteLine("SALE-L01-00-0008 | TODO | Check format - SalesReport.FLUXReportDocument.ReferencedID.Value - RFC4122");
                            //SALE-L01-00-0009 - error
                            //FLUX_ ReportDocument/Referenced Identification
                            //Check Format. Must be according to the specified schemeID
                            //TODO: Check referenced id UUID format - RFC4122
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0008 | ERROR | No SalesReport.FLUXReportDocument.ReferencedID.schemeID provided or != UUID");
                            //SALE-L01-00-0008 - error
                        }
                    }
                    #endregion SalesReport.FLUXReportDocument.ReferencedIdentification

                    //SALE-L00-00-0011, SALE-L01-00-0012, SALE-L01-00-0013
                    #region SalesReport.FLUXReportDocument.CreationDateTime
                    //SALE-L00-00-0011
                    //FLUX_ ReportDocument/Creation
                    //Must be present.
                    if (SalesReport.FLUXReportDocument.CreationDateTime?.Item != null)
                    {
                        Console.WriteLine("SALE-L00-00-0011 | OK | SalesReport.FLUXReportDocument.CreationDateTime.Item provided");

                        //SALE-L01-00-0012
                        //FLUX_ ReportDocument
                        //Check Format. Must be date in UTC according to ISO8601
                        if (SalesReport.FLUXReportDocument.CreationDateTime.Item is DateTime)
                        {
                            Console.WriteLine("SALE-L01-00-0012 | OK | SalesReport.FLUXReportDocument.CreationDateTime.Item is DateTime");

                            //SALE-L01-00-0013
                            //FLUX_ ReportDocument
                            //Date must be in the past.
                            DateTime dateTimeUtcNow = DateTime.UtcNow;
                            if (DateTime.Compare(SalesReport.FLUXReportDocument.CreationDateTime.Item, dateTimeUtcNow) < 0)  // date1 is earlier than date2 => in the past
                            {
                                Console.WriteLine("SALE-L01-00-0013 | OK | SalesReport.FLUXReportDocument.CreationDateTime is in the past");

                                dateTimeFluxReportDocumentCreation = SalesReport.FLUXReportDocument.CreationDateTime.Item;
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0013 | ERROR | SalesReport.FLUXReportDocument.CreationDateTime is not in the past");
                                //SALE-L01-00-0013 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0012 | ERROR | SalesReport.FLUXReportDocument.CreationDateTime is not DateTime");
                            //SALE-L01-00-0012 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0011 | ERROR | No SalesReport.FLUXReportDocument.CreationDateTime.Item provided");
                        //SALE-L00-00-0011 - error
                    }
                    #endregion SalesReport.FLUXReportDocument.CreationDateTime

                    //SALE-L00-00-0013, SALE-L00-00-0014, SALE-L00-00-0020, SALE-L01-00-0017, SALE-L01-00-0020, SALE-L03-00-0012
                    #region SalesReport.FLUXReportDocument.OwnerFLUXParty
                    //SALE-L00-00-0013
                    //FLUXParty
                    //Must be present
                    if (SalesReport.FLUXReportDocument.OwnerFLUXParty != null)
                    {
                        Console.WriteLine("SALE-L00-00-0013 | OK | SalesReport.FLUXReportDocument.OwnerFLUXParty provided");

                        //SALE-L00-00-0014
                        //FLUXParty
                        //Only one party
                        //#Q Will this check satisfy the condition?
                        if (SalesReport.FLUXReportDocument.OwnerFLUXParty.GetType().IsArray == false)
                        {
                            Console.WriteLine("SALE-L00-00-0014 | OK | SalesReport.FLUXReportDocument.OwnerFLUXParty ia not an array");

                            //SALE-L00-00-0020
                            //FLUX_party/Identification
                            //Must be present
                            if (SalesReport.FLUXReportDocument.OwnerFLUXParty.ID != null)
                            {
                                Console.WriteLine("SALE-L00-00-0020 | OK | SalesReport.FLUXReportDocument.OwnerFLUXParty.ID provided");

                                //#Q As cardinality is 1, take and validate only the first element
                                var ownerPartyId = SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.First();
                                //SALE-L01-00-0017
                                //FLUX_party/Identification
                                //Check attribute schemeID. Must be FLUX_GP_PARTY
                                if (ownerPartyId.schemeID?.ToString() == "FLUX_GP_PARTY")
                                {
                                    Console.WriteLine("SALE-L01-00-0017 | OK | SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.schemaID provided and == FLUX_GP_PARTY");

                                    Console.WriteLine("SALE-L01-00-0020 | TODO | Check DB - SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.Value from FLUX_GP_PARTY list");
                                    //SALE-L01-00-0020 - error
                                    //FLUX_party/Identification
                                    //Check code from the list schemeD
                                    //TODO: Check DB - SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.Value from FLUX_GP_PARTY list

                                    Console.WriteLine("SALE-L03-00-0012 | TODO | Check DB if SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.Value is consistent with FLUX TL values");
                                    //SALE-L03-00-0012 - warning
                                    //FLUX_party/Identification
                                    //Check if OwnerFLUXParty.ID is consistent with FLUX TL values.
                                    //TODO: Check if 
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L01-00-0017 | ERROR | No SalesReport.FLUXReportDocument.OwnerFLUXParty.ID.schemaID provided or != FLUX_GP_PARTY");
                                    //SALE-L01-00-0017 - error
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0020 | ERROR | No SalesReport.FLUXReportDocument.OwnerFLUXParty.ID provided");
                                //SALE-L00-00-0020 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0014 | ERROR | SalesReport.FLUXReportDocument.OwnerFLUXParty ia an array");
                            //SALE-L00-00-0014
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0013 | ERROR | No SalesReport.FLUXReportDocument.OwnerFLUXParty provided");
                        //SALE-L00-00-0013 - error
                    }
                    #endregion SalesReport.FLUXReportDocument.OwnerFLUXParty
                }
                else
                {
                    Console.WriteLine("SALE-L02-00-0001 | ERROR | SalesReport.FLUXReportDocument is an array");
                    //SALE-L02-00-0001 - error
                }
            }
            else
            {
                Console.WriteLine("SALE-L00-00-0001 | ERROR | No SalesReport.FLUXReportDocument provided");
                //SALE-L00-00-0001 - error
            }
            #endregion SalesReport.FLUXReportDocument

            //SALE-L02-00-0003
            #region SalesReport.SalesReport
            if (SalesReport.SalesReport != null)
            {
                //#Q Does "If the report is not resulting from a query" means it is a original report => purpose code = 9?
                //#Q If so, will the validation be: if (valuePurposeCode != "3" || valuePurposeCode == "9"), as there cannot be 2 purpose codes at the same time?
                isReportResultingFromQuery = true;
                if (valuePurposeCode != "3" && !isReportResultingFromQuery == false)
                {
                    //SALE-L02-00-0003
                    //FLUX_ ReportDocument/Purpose, FLUX_ReportDocument/Referenced Identification
                    //One and only one occurrence of a sales report if purpose code is not for a 'delete' and if the report is not resulting from a query
                    if (SalesReport.SalesReport.Count() == 1)
                    {
                        Console.WriteLine("SALE-L02-00-0003 | OK | Exactly one SalesReport.SalesReport provided");
                    }
                    else
                    {
                        Console.WriteLine("SALE-L02-00-0003 | ERROR | No or more than one SalesReport.SalesReport provided");
                        //SALE-L02-00-0003 - error
                    }
                }

                if (valuePurposeCode == "9" && !hasReferencedIdentification)
                {
                    Console.WriteLine("SALE-L03-00-0030 | TODO | Check if the reference is unique");
                    //SALE-L03-00-0030
                    //Sales Document/Identification, FLUX Report_Document/ PurposeCode, FLUX Report_Document/Referenced Identification
                    //The reference must be unique for an original document (not resulting from a query)
                    //TODO: Check if the reference is unique [SalesReport.SalesReport.IncludedSalesDocument.First().ID]
                }

                //#Q "In the EU context, there is maximum one sales report by Sales Report message."
                //#Q As cardinality is 1, take and validate only the first element
                var salesReportSalesReport = SalesReport.SalesReport.First();

                //SALE-L00-00-0021, SALE-L01-00-0022, SALE-L03-00-0020
                #region SalesReport.SalesReport.ItemTypeCode
                //SALE-L00-00-0021
                //Sales Report/Item Type
                //Must be present
                if (salesReportSalesReport.ItemTypeCode != null)
                {
                    Console.WriteLine("SALE-L00-00-0021 | OK | SalesReport.SalesReport.ItemTypeCode provided");

                    Console.WriteLine("SALE-L01-00-0021 | TODO | Check DB - SalesReport.SalesReport.ItemTypeCode.Value from FLUX_SALES_TYPE list");
                    //SALE-L01-00-0021
                    //Sales Report/Item Type
                    //Check code from the list
                    //TODO: Check DB - SalesReport.SalesReport.ItemTypeCode.Value is from FLUX_SALES_TYPE list

                    //SALE-L01-00-0022 - error
                    //Sales Report/Item Type
                    //Only SN and TOD allowed
                    if (salesReportSalesReport.ItemTypeCode.Value?.ToString() == "SN" || salesReportSalesReport.ItemTypeCode.Value?.ToString() == "TOD")
                    {
                        Console.WriteLine("SALE-L01-00-0022 | OK | SalesReport.SalesReport.ItemTypeCode.Value provided and == SN || TOD");

                        valueSalesReportTypeCode = salesReportSalesReport.ItemTypeCode.Value?.ToString();

                        if (valuePurposeCode == "5")  //5: correction
                        {
                            Console.WriteLine("SALE-L03-00-0020 | TODO | Check if SalesReport.SalesReport.ItemTypeCode.Value == type of the referenced report");
                            //SALE-L03-00-0020 - error
                            //Sales Report/Item Type, FLUX Report Document/Purpose, FLUX Report Document/Referenced Identification
                            //In case of a correction, the item type must be the same as the item type of the referenced report
                            //TODO: Check if item type is the same as the item type of the referenced report
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0022 | ERROR | No SalesReport.SalesReport.ItemTypeCode.Value provided or != SN || TOD");
                        //SALE-L01-00-0022 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0021 | ERROR | No SalesReport.SalesReport.ItemTypeCode provided");
                    //SALE-L00-00-0021 - error
                }
                #endregion SalesReport.SalesReport.ItemTypeCode

                //SALE-L00-00-0022, SALE-L02-00-0020
                #region SalesReport.SalesReport.IncludedSalesDocument
                //SALE-L00-00-0022
                //Sales Report/Sales Document
                //Must be present
                if (salesReportSalesReport.IncludedSalesDocument != null)
                {
                    Console.WriteLine("SALE-L00-00-0022 | OK | SalesReport.SalesReport.IncludedSalesDocument provided");

                    //SALE-L02-00-0020
                    //Sales Report/Sales Document
                    //Only one sales document
                    if (salesReportSalesReport.IncludedSalesDocument.Count() == 1)
                    {
                        Console.WriteLine("SALE-L02-00-0020 | OK | Exactly one SalesReport.SalesReport.IncludedSalesDocument provided");
                    }
                    else
                    {
                        Console.WriteLine("SALE-L02-00-0020 | ERROR | More than one SalesReport.SalesReport.IncludedSalesDocument provided");
                        //SALE-L02-00-0020 - error
                    }

                    //#Q "In the EU context, there is only one sales document by Sales Report message."
                    //#Q As cardinality is 1, take and validate only the first element
                    var salesReportSalesDocument = salesReportSalesReport.IncludedSalesDocument.First();

                    //SALE-L00-00-0030, SALE-L01-00-0031, SALE-L01-00-0032
                    #region SalesReport.SalesDocument.IncludedSalesDocument.ID
                    //SALE-L00-00-0030
                    //Sales Document/Identification
                    //Must be present
                    if (salesReportSalesDocument.ID != null)
                    {
                        Console.WriteLine("SALE-L00-00-0030 | OK | SalesReport.SalesReport.IncludedSalesDocument.ID provided");

                        //#Q As cardinality is one, take and validate the first element only
                        var salesDocumentID = salesReportSalesDocument.ID.First();

                        string[] salesDocumentIdSubstrings = salesDocumentID.Value?.ToString().Trim().Split("-");
                        string salesDocumentIdNationalNumber = String.Join("-", salesDocumentID.Value?.ToString().Trim().Split("-").Skip(2));

                        Console.WriteLine("SALE-L01-00-0031 | TODO | Add SalesReport.SalesReport.IncludedSalesDocument.ID checks - sender/recipient");
                        //SALE-L01-00-0031
                        //Sales Document/Identification
                        //Check format of the part corresponding to the national number
                        //country specific. For the sender, this BR can be used to control the national format.
                        //For the recipient, it can verify the specific format of the sender if he knows it,
                        //otherwise it is to verify if that part has maximum 20 chars


                        if (salesDocumentIdNationalNumber.Length > 0 && salesDocumentIdNationalNumber.Length <= 20)
                        {
                            Console.WriteLine("SALE-L01-00-0031 | OK | SalesReport.SalesReport.IncludedSalesDocument.ID salesDocumentIdNationalNumber provided and length <= 20");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0031 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.ID salesDocumentIdNationalNumber provided or length > 20");
                            //SALE-L01-00-0031 - error
                        }

                        //SALE-L01-00-0032
                        //Sales Document/Identification
                        //Check format of the common part
                        //ISO-3 code + '-' + type of report
                        //TODO: Check if salesDocumentIdSubstrings[0] is an ISO-3 code
                        Console.WriteLine("SALE-L01-00-0032 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.ID salesDocumentIdSubstrings[0] is valid ISO-3 code");
                        bool isFirstSubstringIso3Code = true;
                        if (isFirstSubstringIso3Code && (salesDocumentIdSubstrings[1] == valueSalesReportTypeCode))
                        {
                            Console.WriteLine("SALE-L01-00-0032 | OK | SalesReport.SalesReport.IncludedSalesDocument.ID common part format valid");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0032 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.ID common part format not valid");
                            //SALE-L01-00-0032 - error
                        }

                        if (valuePurposeCode == "9")
                        {
                            Console.WriteLine("SALE-L03-00-0030 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.ID is unique");

                            //SALE-L03-00-0030 - error
                            //#Q Is its place here, does "the reference" means IncludedSalesDocument.ID?
                            //Sales Document/Identification, FLUX Report_Document/ PurposeCode, FLUX Report_Document/Referenced Identification
                            //The reference must be unique for an original document (not resulting from a query)
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0030 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.ID provided");
                        //SALE-L00-00-0030 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.ID

                    //SALE-L00-00-0031, SALE-L01-00-0033, SALE-L03-00-0031
                    #region SalesReport.SalesDocument.IncludedSalesDocument.CurrencyCode
                    //SALE-L00-00-0031
                    //Sales Document/Sales Currency
                    //Must be present
                    if (salesReportSalesDocument.CurrencyCode != null)
                    {
                        Console.WriteLine("SALE-L00-00-0031 | OK | SalesReport.SalesReport.CurrencyCode provided");

                        //SALE-L01-00-0033
                        //Sales Document/Sales Currency
                        //Check Code from the list listID
                        Console.WriteLine("SALE-L01-00-0033 | TODO | Check if SalesRepost.SalesReport.CurrencyCode.Value is from TERRITORY_CURR list");
                        bool isCodeFromSpecifiedList = true;
                        if (salesReportSalesDocument.CurrencyCode.listID?.ToString() == "TERRITORY_CURR" && isCodeFromSpecifiedList)
                        {
                            Console.WriteLine("SALE-L01-00-0033 | OK | SalesReport.SalesReport.CurrencyCode.listID provided and Value is from TERRITORY_CURR list");

                            Console.WriteLine("SALE-L03-00-0031 | TODO | Check if SalesReport.SalesReport.CurrencyCode.Value is official of the country at the time");
                            //SALE-L03-00-0031 - warning
                            //Sales Document/Sales Currency, Sales Event/ Occurrence, FLUX Location/ Country
                            //It must be an official currency of the country at that date
                            //TODO: Check if SalesReport.SalesReport.CurrencyCode.Value is official
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0033 | ERROR | No SalesReport.SalesReport.CurrencyCode.listID provided or Value is not from TERRITORY_CURR list");
                            //SALE-L01-00-0033 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0031 | ERROR | No SalesReport.SalesReport.CurrencyCode provided");
                        //SALE-L00-00-0031 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.CurrencyCode

                    //SALE-L01-00-0034, SALE-L03-00-0032
                    #region SalesReport.SalesDocument.IncludedSalesDocument.TransportDocumentID
                    if (salesReportSalesDocument.TransportDocumentID != null)
                    {
                        foreach (var salesDocumentTransportDocumentID in salesReportSalesDocument.TransportDocumentID)
                        {
                            Console.WriteLine("SALE-L01-00-0034 | TODO | Check format - SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID");
                            //SALE-L01-00-0034 - error
                            //Sales Document/Transport Document Identification
                            //Check format
                            //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID format

                            Console.WriteLine("SALE-L03-00-0032 | TODO | Check DB - SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID - if a reference is mentioned, it must be present in DB");
                            //SALE-L03-00-0032 - warning
                            //Sales Document/Transport Document Identification
                            //The reference must exist
                            //If a reference of a document is mentioned, that document must exist (meaning the reference must be known - already registered - in the system).
                            //But because some documents are still processed on paper and transmitted with delays, it can be known by the Administration after the reception and validation of the sales information.
                            //That is why the validation result is a warning (not a blocking issue)
                            //TODO: Check for reference - if mentioned, should be present in DB
                        }
                    }
                    else
                    {
                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID provided");
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.TransportDocumentID

                    //SALE-L01-00-0035, SALE-L03-00-0033
                    #region SalesReport.SalesDocument.IncludedSalesDocument.TakeoverDocumentID
                    if (salesReportSalesDocument.TakeoverDocumentID != null)
                    {
                        foreach (var salesDocumentTakeoverDocumentID in salesReportSalesDocument.TakeoverDocumentID)
                        {
                            Console.WriteLine("SALE-L01-00-0034 | TODO | Check format - SalesReport.SalesReport.IncludedSalesDocument.TakeoverDocumentID");
                            //SALE-L01-00-0035 - error
                            //Sales Document/Take Over Document Identification
                            //Check Format
                            //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID format

                            Console.WriteLine("SALE-L03-00-0033 | TODO | Check DB - SalesReport.SalesReport.IncludedSalesDocument.TakeoverDocumentID - if a reference is mentioned, it must be present in DB");
                            //SALE-L03-00-0033 - warning
                            //Sales Document/Take Over Document Identification
                            //The reference must exist
                            //Cfr note of SALEL03-00-0032  //#Q What does this mean?
                            //TODO: Check for reference - if mentioned, should be present in DB
                        }
                    }
                    else
                    {
                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.TakeoverDocumentID provided");
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.TakeoverDocumentID

                    //SALE-L01-00-0036, SALE-L03-00-0034
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SalesNoteID
                    if (salesReportSalesDocument.SalesNoteID != null)
                    {
                        foreach (var salesDocumentTakeoverDocumentID in salesReportSalesDocument.SalesNoteID)
                        {
                            Console.WriteLine("SALE-L01-00-0036 | TODO | Check format - SalesReport.SalesReport.IncludedSalesDocument.SalesNoteID");
                            //SALE-L01-00-0036 - error
                            //Sales Document/Sales Note identification
                            //Check Format
                            //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.TransportDocumentID format

                            Console.WriteLine("SALE-L03-00-0034 | TODO | Check DB - SalesReport.SalesReport.IncludedSalesDocument.SalesNoteID - if a reference is mentioned, it must be present in DB");
                            //SALE-L03-00-0034 - warning
                            //Sales Document/Sales Note identification
                            //The reference must exist
                            //Cfr note of SALEL03-00-0032  //#Q What does this mean?
                            //TODO: Check for reference - if mentioned, should be present in DB
                        }
                    }
                    else
                    {
                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SalesNoteID provided");
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SalesNoteID

                    //SALE-L00-00-0032, SALE-L02-00-0030, SALE-L01-00-0050, SALE-L03-00-0050, 
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesEvent
                    //SALE-L00-00-0032
                    //Sales Event
                    //Must exist
                    if (salesReportSalesDocument.SpecifiedSalesEvent != null)
                    {
                        Console.WriteLine("SALE-L00-00-0032 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent provided");

                        //SALE-L02-00-0030
                        //Sales Event
                        //Only one sales event
                        if (salesReportSalesDocument.SpecifiedSalesEvent.Count() == 1)
                        {
                            Console.WriteLine("SALE-L00-00-0032 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent Count == 1");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0032 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent Count != 1");
                            //SALE-L02-00-0030 - error
                        }

                        //#Q As cardinality iis 1, take and validate only the first element
                        var salesDocumentSpecifiedSalesEvent = salesReportSalesDocument.SpecifiedSalesEvent.First();

                        //SALE-L01-00-0050
                        //Sales Event/Occurrence
                        //Check format
                        if (salesDocumentSpecifiedSalesEvent.OccurrenceDateTime?.Item != null && salesDocumentSpecifiedSalesEvent.OccurrenceDateTime?.Item is DateTime)
                        {
                            Console.WriteLine("SALE-L01-00-0050 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent.OccurrenceDateTime.Item provided and DateTime");

                            dateTimeSalesEventOccurance = salesDocumentSpecifiedSalesEvent.OccurrenceDateTime.Item;
                            TimeSpan timeSpanMaximumDelay = new TimeSpan(24, 0, 0);
                            DateTime dateTimeSalesEventOccuranceWithDelay = dateTimeSalesEventOccurance.Add(timeSpanMaximumDelay);
                            if (valuePurposeCode == "9")  //#Q If the report is not resulting from a query
                            {
                                //SALE-L03-00-0050
                                //Sales Event/Occurrence, FLUX Report Document / Creation, FLUX Report Document / Referenced Identification
                                //If the report is not resulting from a query, the reception date (Creation) (by Market state) should not be later than 24h after the sale date/takeover date (Occurrence)
                                if (DateTime.Compare(dateTimeSalesEventOccuranceWithDelay, dateTimeFluxReportDocumentCreation) >= 0)  //dateTime1 is the same or later than dateTime2.
                                {
                                    Console.WriteLine("SALE-L03-00-0050 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent.OccurrenceDateTime.Item +24h delay is not before FLUXDocumentCreation");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L03-00-0050 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent.OccurrenceDateTime.Item +24h delay is before FLUXDocumentCreation");
                                    //SALE-L03-00-0050 - warning
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0050 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent.OccurrenceDateTime.Item provided or not DateTime");
                            //SALE-L01-00-0050 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0032 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesEvent provided");
                        //SALE-L00-00-0032 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesEvent

                    //SALE-L00-00-0033, SALE-L02-00-0031, SALE-L01-00-0042, SALE-L01-00-0043, SALE-L02-00-0044, SALE-L02-00-0041, SALE-L02-00-0042, SALE-L02-00-0043, SALE-L00-00-0040,
                    //SALE-L01-00-0041, SALE-L00-00-0041, SALE-L01-00-0040, SALE-L02-00-0040, SALE-L03-00-0041, SALE-L03-00-0040, SALE-L02-00-0045, SALE-L00-00-0095
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesParty
                    //SALE-L00-00-0033
                    //Sales Party
                    //Must exist
                    if (salesReportSalesDocument.SpecifiedSalesParty != null)
                    {
                        Console.WriteLine("SALE-L00-00-0033 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided");

                        //SALE-L02-00-0031
                        //Sales Party
                        //At least two occurrences
                        if (salesReportSalesDocument.SpecifiedSalesParty.Count() >= 2)
                        {
                            Console.WriteLine("SALE-L02-00-0031 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty Count >= 2");

                            //SALE-L01-00-0042
                            //Sales Party/Role
                            //SENDER role must exist
                            if (salesReportSalesDocument.SpecifiedSalesParty.Any(a => a.RoleCode?.First().Value?.ToString() == "SENDER"))
                            {
                                Console.WriteLine("SALE-L01-00-0042 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role SENDER");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0042 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role SENDER");
                                //SALE-L01-00-0042 - error
                            }

                            //SALE-L01-00-0043
                            //Sales Party/Role
                            //No code for CARRIER allowed  //#Q What does this means - there should not be a Carrier role?
                            if (salesReportSalesDocument.SpecifiedSalesParty.Any(a => a.RoleCode?.First().Value?.ToString() == "CARRIER"))
                            {
                                Console.WriteLine("SALE-L01-00-0043 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role CARRIER");
                                //SALE-L01-00-0043 - error
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0043 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role CARRIER");
                            }

                            //SALE-L02-00-0044
                            //Sales Party/Role
                            //No duplication of role allowed
                            if (salesReportSalesDocument.SpecifiedSalesParty.GroupBy(g => g.RoleCode?.First().Value).Where(w => w.Count() > 1).Count() == 0)
                            {
                                Console.WriteLine("SALE-L02-00-0044 | OK | No duplicate SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode provided");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L02-00-0044 | ERROR | Duplicate SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode provided");
                                //SALE-L02-00-0044 - error
                            }

                            if (valueSalesReportTypeCode == "SN")
                            {
                                //SALE-L02-00-0041
                                //Sales Party/Role
                                //PROVIDER role must exist for a sales note (SN)
                                if (salesReportSalesDocument.SpecifiedSalesParty.Any(a => a.RoleCode?.First().Value?.ToString() == "PROVIDER"))
                                {
                                    Console.WriteLine("SALE-L02-00-0041 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role PROVIDER");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L02-00-0041 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role PROVIDER");
                                    //SALE-L02-00-0041 - error
                                }

                                //SALE-L02-00-0042
                                //Sales Party/Role
                                //BUYER roles must exist for a sales note (SN)
                                if (salesReportSalesDocument.SpecifiedSalesParty.Any(a => a.RoleCode?.First().Value?.ToString() == "BUYER"))
                                {
                                    Console.WriteLine("SALE-L02-00-0042 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role BUYER");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L02-00-0042 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role BUYER");
                                    //SALE-L02-00-0042 - error
                                }
                            }
                            else if (valueSalesReportTypeCode == "TOD")
                            {
                                //SALE-L02-00-0043
                                //Sales Party/Role
                                //RECIPIENT role must exist for a Take-over doc
                                if (salesReportSalesDocument.SpecifiedSalesParty.Any(a => a.RoleCode?.First().Value?.ToString() == "RECIPIENT"))
                                {
                                    Console.WriteLine("SALE-L02-00-0043 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role RECIPIENT");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L02-00-0043 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided with role RECIPIENT");
                                    //SALE-L02-00-0043 - error
                                }
                            }

                            foreach (var salesDocumentSpecifiedSalesParty in salesReportSalesDocument.SpecifiedSalesParty)
                            {
                                string valueRoleCode = "";

                                //SALE-L00-00-0040
                                //Sales Party/Role
                                //Must be present
                                if (salesDocumentSpecifiedSalesParty.RoleCode != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0040 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode provided");

                                    //#Q As cardinality is up to 1, take and validate the first element only 
                                    var salesPartyRoleCode = salesDocumentSpecifiedSalesParty.RoleCode.First();

                                    if (salesPartyRoleCode.listID?.ToString() == "FLUX_SALES_PARTY_ROLE")
                                    {
                                        Console.WriteLine("SALE-L01-00-0041 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode.listID codes are from FLUX_SALES_PARTY_ROLE list");
                                        //SALE-L01-00-0041
                                        //Sales Party/Role
                                        //Check code from the list listID
                                        //TODO: Check if salesPartyRoleCode.listID code are from FLUX_SALES_PARTY_ROLE list
                                    }
                                    else
                                    {
                                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode.listID provided or != FLUX_SALES_PARTY_ROLE");
                                    }

                                    valueRoleCode = salesPartyRoleCode.Value?.ToString();
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0040 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.RoleCode provided");
                                    //SALE-L00-00-0040 - error
                                }

                                //SALE-L00-00-0041
                                //Sales Party/Name
                                //Must be present
                                if (salesDocumentSpecifiedSalesParty.Name != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0041 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.Name provided");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0041 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.Name provided");
                                    //SALE-L00-00-0041 - error
                                }

                                if (salesDocumentSpecifiedSalesParty.ID != null)
                                {
                                    Console.WriteLine("SALE-L01-00-0040 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID.schemeID codes are from FLUX_SALES_PARTY_ID_TYPE list");
                                    //SALE-L01-00-0040 - error
                                    //Sales Party/Identification
                                    //Check code from the list schemeID
                                    //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID.schemeID codes are from FLUX_SALES_PARTY_ID_TYPE list
                                }

                                if (valueRoleCode == "SENDER")
                                {
                                    //SALE-L02-00-0040
                                    //Sales Party/Identification, Sales Party/Role
                                    //Identification mandatory when role=SENDER
                                    if (salesDocumentSpecifiedSalesParty.ID != null)
                                    {
                                        Console.WriteLine("SALE-L02-00-0040 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID provided for role SENDER");

                                        //SALE-L03-00-0041
                                        //Sales Party/Identification, Sales Party/Role
                                        //If SENDER role, it should be a MS Identification (SchemeID=MS)
                                        if (salesDocumentSpecifiedSalesParty.ID.schemeID?.ToString() == "MS")
                                        {
                                            Console.WriteLine("SALE-L03-00-0041 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID with schemeID == MS provided for role SENDER");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L03-00-0041 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID with schemeID == MS provided for role SENDER");
                                            //SALE-L03-00-0041 - error
                                        }

                                        Console.WriteLine("SALE-L03-00-0040 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID.Value exists in country registered bodies list");
                                        //SALE-L03-00-0040 - error
                                        //Sales Party/Identification, Sales Party/Role
                                        //If SENDER role, the ID must exist in a list of registered bodies of the country
                                        //That list should be produced by each country for internal usage.
                                        //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID.Value exists in country registered bodies list
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0040 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.ID provided for role SENDER");
                                        //SALE-L02-00-0040 - error
                                    }
                                }
                                else if (valueRoleCode == "RECIPIENT")
                                {
                                    if (valueSalesReportTypeCode == "TOD")
                                    {
                                        //SALE-L02-00-0045
                                        //Sales Party/Role FLUX Organisation/Name
                                        //Name of organisation must be present for TOD if the role is RECIPIENT
                                        if (salesDocumentSpecifiedSalesParty.Name?.Value != null)
                                        {
                                            Console.WriteLine("SALE-L02-00-0045 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.Name provided for role RECIPIENT when type TOD");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0045 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty.Name provided for role RECIPIENT when type TOD");
                                            //SALE-L02-00-0045 - error
                                        }
                                    }
                                }

                                if (salesDocumentSpecifiedSalesParty.SpecifiedFLUXOrganization != null)
                                {
                                    //SALE-L00-00-0095
                                    //FLUX Organization/Name
                                    //Must be present
                                    if (salesDocumentSpecifiedSalesParty.SpecifiedFLUXOrganization.Name != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0095 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXOrganization.Name provided");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0095 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXOrganization.Name provided");
                                        //SALE-L00-00-0095 - error
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXOrganization provided");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0031 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty Count < 2");
                            //SALE-L02-00-0031 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0033 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesParty provided");
                        //SALE-L00-00-0033 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesParty

                    //SALE-L00-00-0034, SALE-L02-00-0032, SALE-L02-00-0050, SALE-L00-00-0100, SALE-L01-00-0101, SALE-L00-00-0101, SALE-L02-00-0100, SALE-L00-00-0110, SALE-L01-00-0110,
                    //SALE-L01-00-0111, SALE-L00-00-0102, SALE-L02-00-0101, SALE-L00-00-0120, SALE-L01-00-0120, SALE-L00-00-0122, SALE-L02-00-0121, SALE-L01-00-0150, SALE-L00-00-0150,
                    //SALE-L02-00-0151, SALE-L02-00-0150, SALE-L02-00-0152, SALE-L02-00-0160, SALE-L01-00-0160, SALE-L02-00-0161, SALE-L01-00-0161, SALE-L02-00-0162, SALE-L01-00-0162,
                    //SALE-L00-00-0123, SALE-L00-00-0130, SALE-L01-00-0130, SALE-L00-00-0103, SALE-L02-00-0102, SALE-L00-00-0170, SALE-L01-00-0170, SALE-L01-00-0171, SALE-L00-00-0104
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedFishingActivity
                    //SALE-L00-00-0034
                    //Fishing Activity
                    //Must exist
                    if (salesReportSalesDocument.SpecifiedFishingActivity != null)
                    {
                        Console.WriteLine("SALE-L00-00-0034 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity provided");

                        //SALE-L02-00-0032
                        //Fishing Activity
                        //Only one occurrence
                        if (salesReportSalesDocument.SpecifiedFishingActivity.Count() == 1)
                        {
                            Console.WriteLine("SALE-L02-00-0032 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity Count == 1");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0032 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity Count != 1");
                            //SALE-L02-00-0032 - error
                        }

                        //#Q As cardinality is up to one, take and validate the first element only
                        var salesDocumentSpecifiedFishingActivity = salesReportSalesDocument.SpecifiedFishingActivity.First();

                        //#Q Since cardinality of DelimitedPeriod is 1, take the First element for validation
                        dateTimeDelimitedPeriodStart = salesDocumentSpecifiedFishingActivity.SpecifiedDelimitedPeriod.First().StartDateTime.Item;
                        //SALE-L02-00-0050
                        //Sales Event/Occurrence, Delimited_Period/Start
                        //Sales Event date >= Start date (landing)
                        if (DateTime.Compare(dateTimeSalesEventOccurance, dateTimeDelimitedPeriodStart) >= 0)  //datetime1 is the same or after datetime2
                        {
                            Console.WriteLine("SALE-L02-00-0050 | OK | dateTimeSalesEventOccurance is after SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.Start");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0050 | ERROR | dateTimeSalesEventOccurance is before SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.Start");
                            //SALE-L02-00-0050 - error
                        }

                        //SALE-L00-00-0100
                        //Fishing Activity/Type
                        //Must be present
                        if (salesDocumentSpecifiedFishingActivity.TypeCode != null)
                        {
                            Console.WriteLine("SALE-L00-00-0100 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.TypeCode provided");

                            //SALE-L01-00-0101
                            //Fishing Activity/Type
                            //Check code: LAN
                            //There is no listID, just a code
                            if (salesDocumentSpecifiedFishingActivity.TypeCode.Value == "LAN")
                            {
                                Console.WriteLine("SALE-L01-00-0101 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.TypeCode.Value == LAN");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0101 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.TypeCode.Value != LAN");
                                //SALE-L01-00-0101 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0100 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.TypeCode provided");
                            //SALE-L00-00-0100 - error
                        }

                        //SALE-L00-00-0101
                        //Delimited_Period
                        //Must be present
                        if (salesDocumentSpecifiedFishingActivity.SpecifiedDelimitedPeriod != null)
                        {
                            Console.WriteLine("SALE-L00-00-0101 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod provided");

                            //SALE-L02-00-0100
                            //Delimited_Period
                            //Only one occurrence
                            if (salesDocumentSpecifiedFishingActivity.SpecifiedDelimitedPeriod.Count() == 1)
                            {
                                Console.WriteLine("SALE-L02-00-0100 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod Count == 1");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L02-00-0100 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod Count != 1");
                                //SALE-L02-00-0100 - error
                            }

                            //#Q As cardinality is up to one, take and validate the first element only
                            var specifiedFishingActivitySpecifiedDelimitedPeriod = salesDocumentSpecifiedFishingActivity.SpecifiedDelimitedPeriod.First();

                            //SALE-L00-00-0110
                            //Delimited Period/Start
                            //Must be present
                            if (specifiedFishingActivitySpecifiedDelimitedPeriod.StartDateTime != null)
                            {
                                Console.WriteLine("SALE-L00-00-0110 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.StartDateTime provided");

                                //SALE-L01-00-0110
                                //Delimited Period/Start
                                //Check format
                                if (specifiedFishingActivitySpecifiedDelimitedPeriod.StartDateTime.Item is DateTime)
                                {
                                    Console.WriteLine("SALE-L01-00-0110 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.StartDateTime.StartDateTime format OK");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L01-00-0110 | ERROR | Wrong SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.StartDateTime.Item format");
                                    //SALE-L01-00-0110 - error
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0110 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.StartDateTime provided");
                                //SALE-L00-00-0110 - warning
                            }

                            //SALE-L01-00-0111
                            //Delimited Period/End
                            //Check format
                            if (specifiedFishingActivitySpecifiedDelimitedPeriod.EndDateTime?.Item is DateTime)
                            {
                                Console.WriteLine("SALE-L01-00-0111 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.EndDateTime.Item format OK");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0111 | ERROR | Wrong SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod.EndDateTime.Item format");
                                //SALE-L01-00-0111
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0101 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedDelimitedPeriod provided");
                            //SALE-L00-00-0101 - error
                        }

                        //SALE-L00-00-0102
                        //Vessel_Tranport_Means
                        //Must be present
                        if (salesDocumentSpecifiedFishingActivity.RelatedVesselTransportMeans != null)
                        {
                            Console.WriteLine("SALE-L00-00-0102 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans provided");

                            //SALE-L02-00-0101
                            //Vessel_Tranport_Means
                            //Only one occurrence
                            if (salesDocumentSpecifiedFishingActivity.RelatedVesselTransportMeans.Count() == 1)
                            {
                                Console.WriteLine("SALE-L02-00-0101 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans Count == 1");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L02-00-0101 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans Count != 1");
                                //SALE-L02-00-0101 - error
                            }

                            //#Q As cardinality is up to one, take and validate the first element only
                            var specifiedFishingActivityRelatedVesselTransportMeans = salesDocumentSpecifiedFishingActivity.RelatedVesselTransportMeans.First();

                            //SALE-L00-00-0120
                            //Vessel Transport Means/Identification
                            //Must be present
                            if (specifiedFishingActivityRelatedVesselTransportMeans.ID?.First() != null)
                            {
                                Console.WriteLine("SALE-L00-00-0120 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.ID provided");

                                if (specifiedFishingActivityRelatedVesselTransportMeans.ID?.First().schemeID?.ToString() == "CFR")
                                {
                                    //SALE-L01-00-0120
                                    //Vessel Transport Means/Identification
                                    //Check format of the CFR
                                    if (Regex.Match(specifiedFishingActivityRelatedVesselTransportMeans.ID?.First().Value.ToString(), "^[A-Z]{3}[0-9]{9}$").Success)
                                    {
                                        Console.WriteLine("SALE-L01-00-0120 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.ID.Value a valid CFR");

                                        valueCFR = specifiedFishingActivityRelatedVesselTransportMeans.ID?.First().Value.ToString();
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L01-00-0120 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.ID.Value not a valid CFR");
                                        //SALE-L01-00-0120 - error
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0120 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.ID provided");
                                //SALE-L00-00-0120 - error
                            }

                            //SALE-L00-00-0122
                            //Contact Party
                            //Must be present
                            if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty != null)
                            {
                                Console.WriteLine("SALE-L00-00-0122 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty provided");

                                //SALE-L02-00-0121
                                //Contact Party
                                //At least one occurrence and maximum two
                                if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.Count() == 1 || specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.Count() == 2)
                                {
                                    Console.WriteLine("SALE-L02-00-0121 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty Count == 1 || 2");

                                    Console.WriteLine("SALE-L01-00-0150 | TODO | Check SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode.Value is from FLUX_CONTACT_ROLE list");
                                    //SALE-L01-00-0150
                                    //Contact Party/Role
                                    //Check code from the list listID
                                    //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode.Value is from FLUX_CONTACT_ROLE list

                                    //SALE-L00-00-0150
                                    //Contact Party/Role
                                    //Master or operator role must be present
                                    if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.Any(a => a.RoleCode.First().Value == "MASTER" || a.RoleCode.First().Value == "OPERATOR"))
                                    {
                                        Console.WriteLine("SALE-L00-00-0150 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode provided with Value == MASTER || OPERATOR");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0150 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode provided with Value == MASTER || OPERATOR");
                                        //SALE-L00-00-0150 - error
                                    }

                                    //SALE-L02-00-0151
                                    //Contact Party/Role
                                    //Codes other than master/operator are not allowed
                                    if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.Any(a => a.RoleCode.First().Value != "MASTER" || a.RoleCode.First().Value != "OPERATOR"))
                                    {
                                        Console.WriteLine("SALE-L02-00-0151 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode provided with Value != MASTER || OPERATOR");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0151 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode provided with Value != MASTER || OPERATOR");
                                        //SALE-L02-00-0151 - error
                                    }

                                    //SALE-L02-00-0150
                                    //Contact Party/Role
                                    //No duplicated role
                                    if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.GroupBy(g => g.RoleCode.First().Value).Where(w => w.Count() > 1).Count() == 0)
                                    {
                                        Console.WriteLine("SALE-L02-00-0121 | OK | No duplicates in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0121 | ERROR | Duplicates in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.RoleCode");
                                        //SALE-L02-00-0150 - error
                                    }

                                    //SALE-L02-00-0152
                                    //Contact Person
                                    //Must be present for each party
                                    if (specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty.All(a => a.SpecifiedContactPerson.First() != null))
                                    {
                                        Console.WriteLine("SALE-L02-00-0152 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson provided for each ContactParty");

                                        foreach (var relatedVesselTransportMeansSpecifiedContactParty in specifiedFishingActivityRelatedVesselTransportMeans.SpecifiedContactParty)
                                        {
                                            //#Q As cardinality is up to one, take and validate the first element only
                                            var specifiedContactPartySpecifiedContactPerson = relatedVesselTransportMeansSpecifiedContactParty.SpecifiedContactPerson.First();

                                            if (specifiedContactPartySpecifiedContactPerson.Alias?.Value == null)
                                            {
                                                //SALE-L02-00-0160
                                                //Specified Contact_Person/GivenNameText, Specified Contact_Person/ AliasText
                                                //Check presence of GivenName. Must be present if AliasText is not present.
                                                if (specifiedContactPartySpecifiedContactPerson.GivenName != null)
                                                {
                                                    Console.WriteLine("SALE-L02-00-0160 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.GivenName provided when Alias missing");

                                                    //SALE-L01-00-0160
                                                    //Specified Contact_Person/GivenNameText, Specified Contact_Person/ AliasText
                                                    //Non-empty GivenName
                                                    if (specifiedContactPartySpecifiedContactPerson.GivenName.Value != null)
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0160 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.GivenName provided non-empty");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0160 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.GivenName provided empty");
                                                        //SALE-L01-00-0160 - error
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("SALE-L02-00-0160 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson provided when Alias missing");
                                                    //SALE-L02-00-0160 - error
                                                }

                                                //SALE-L02-00-0161
                                                //Specified Contact_Person/FamilyNameText, Specified Contact_Person/ AliasText
                                                //Check presence of FamilyName. Must be present if AliasText is not present
                                                if (specifiedContactPartySpecifiedContactPerson.FamilyName != null)
                                                {
                                                    Console.WriteLine("SALE-L02-00-0161 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.FamilyName provided when Alias missing");

                                                    //SALE-L01-00-0161
                                                    //Specified Contact_Person/FamilyNameText, Specified Contact_Person/ AliasText
                                                    //Non-empty FamilyName
                                                    if (specifiedContactPartySpecifiedContactPerson.FamilyName.Value != null)
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0161 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.FamilyName provided non-empty");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0161 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.FamilyName provided empty");
                                                        //SALE-L01-00-0161 - error
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("SALE-L02-00-0161 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.FamilyName provided when Alias missing");
                                                    //SALE-L02-00-0161 - error
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.Alias provided");
                                            }

                                            if (specifiedContactPartySpecifiedContactPerson.GivenName?.Value == null && specifiedContactPartySpecifiedContactPerson.FamilyName?.Value == null)
                                            {
                                                //SALE-L02-00-0162
                                                //Specified Contact_Person/AliasText
                                                //Check presence. Must be present if GivenNameText or FamilyNameText is not present.
                                                if (specifiedContactPartySpecifiedContactPerson.Alias != null)
                                                {
                                                    Console.WriteLine("SALE-L02-00-0162 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.Alias provided when GivenName and FamilyName missing");

                                                    //SALE-L01-00-0162
                                                    //Specified Contact_Person/AliasText
                                                    //Non-empty
                                                    if (specifiedContactPartySpecifiedContactPerson.Alias.Value != null)
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0162 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.Alias provided non-empty");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("SALE-L01-00-0162 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.Alias provided empty");
                                                        //SALE-L01-00-0162 - error
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("SALE-L02-00-0162 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.Alias provided when GivenName and FamilyName missing");
                                                    //SALE-L02-00-0162 - error
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson.GivenName nor FamilyName provided");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0152 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty.SpecifiedContactPerson provided for each ContactParty");
                                        //SALE-L02-00-0152 - error
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L02-00-0121 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty Count != 1 || 2");
                                    //SALE-L02-00-0121 - error
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0122 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.SpecifiedContactParty provided");
                                //SALE-L00-00-0122 - error
                            }

                            //SALE-L00-00-0123
                            //Vessel Country
                            //Must be present
                            if (specifiedFishingActivityRelatedVesselTransportMeans.RegistrationVesselCountry != null)
                            {
                                Console.WriteLine("SALE-L00-00-0123 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry provided");

                                //SALE-L00-00-0130
                                //Vessel Country/Identification
                                //Must be present
                                if (specifiedFishingActivityRelatedVesselTransportMeans.RegistrationVesselCountry.ID != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0130 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry.ID provided");

                                    Console.WriteLine("SALE-L01-00-0130 | TODO | Check SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value code");
                                    //SALE-L01-00-0130
                                    //Vessel Country/Identification
                                    //Check code
                                    //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value code

                                    valueVesselCountry = specifiedFishingActivityRelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value.ToString();
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0130 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry.ID provided");
                                    //SALE-L00-00-0130 - error
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0123 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans.RegistrationVesselCountry provided");
                                //SALE-L00-00-0123 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0102 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedVesselTransportMeans provided");
                            //SALE-L00-00-0102 - error
                        }

                        //SALE-L00-00-0103
                        //Fishing_Trip
                        //Must be present
                        if (salesDocumentSpecifiedFishingActivity.SpecifiedFishingTrip != null)
                        {
                            Console.WriteLine("SALE-L00-00-0103 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip provided");

                            //SALE-L02-00-0102
                            //Fishing_Trip
                            //Only one occurrence
                            //#Q How [only one occurance] should be cheched, is this workable?
                            if (!salesDocumentSpecifiedFishingActivity.SpecifiedFishingTrip.GetType().IsArray)
                            {
                                Console.WriteLine("SALE-L02-00-0102 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip provided is not an array");

                                //SALE-L00-00-0170
                                //FishingTrip/ID
                                //Check presence.Must be present.
                                //At least one occurrence.
                                //#Q As cardinality is up to one, take and validate the first element only
                                if (salesDocumentSpecifiedFishingActivity.SpecifiedFishingTrip.ID.First() != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0170 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip.ID provided");

                                    Console.WriteLine("SALE-L01-00-0170 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip.ID.schemaID == EU_TRIP_ID");
                                    //SALE-L01-00-0170
                                    //FishingTrip/ID
                                    //Check attribute schemeID. Must be EU_TRIP_ID
                                    //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip.ID.schemaID == EU_TRIP_ID

                                    Console.WriteLine("SALE-L01-00-0171 | TODO | Check if format according to schemaID rules for SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip.ID");
                                    //SALE-L01-00-0171
                                    //FishingTrip/ID
                                    //Check format. Must be according to schemeID rules
                                    //TODO: Check format
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0170 | WARNING | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip.ID provided");
                                    //SALE-L00-00-0170 - warning
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L02-00-0102 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip provided is an array");
                                //SALE-L02-00-0102 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0103 | WARNING | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.SpecifiedFishingTrip provided");
                            //SALE-L00-00-0103 - warning
                        }

                        //SALE-L00-00-0104
                        //FLUX_Location
                        //Must be present
                        if (salesDocumentSpecifiedFishingActivity.RelatedFLUXLocation != null)
                        {
                            Console.WriteLine("SALE-L00-00-0104 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedFLUXLocation provided");

                            FLUXLocationValidationElement(entity: "SpecifiedFishingActivity", locationElement: salesReportSalesDocument.SpecifiedFLUXLocation.First());
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0104 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity.RelatedFLUXLocation provided");
                            //SALE-L00-00-0104 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0034 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFishingActivity provided");
                        //SALE-L00-00-0034 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedFishingActivity

                    //SALE-L00-00-0035, SALE-L02-00-0032
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedFLUXLocation
                    //SALE-L00-00-0035
                    //Flux Location
                    //Must exist
                    if (salesReportSalesDocument.SpecifiedFLUXLocation != null)
                    {
                        Console.WriteLine("SALE-L00-00-0035 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXLocation provided");

                        //SALE-L02-00-0032
                        //Flux Location
                        //Only one occurrence
                        //It is a market place or a storage location  //#Q How is this checked?
                        if (salesReportSalesDocument.SpecifiedFLUXLocation.Count() == 1)
                        {
                            Console.WriteLine("SALE-L02-00-0032 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXLocation Count == 1");
                            Console.WriteLine("SALE-L02-00-0032 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXLocation is market place or storage location");

                            FLUXLocationValidationElement(entity: "IncludedSalesDocument", locationElement: salesReportSalesDocument.SpecifiedFLUXLocation.First());
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0032 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXLocation Count != 1");
                            //SALE-L02-00-0032 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0035 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedFLUXLocation provided");
                        //SALE-L00-00-0035 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedFLUXLocation

                    //SALE-L00-00-0036, SALE-L02-00-0034
                    #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesBatch
                    //SALE-L00-00-0036
                    //Sales Batch
                    //Must exist
                    if (salesReportSalesDocument.SpecifiedSalesBatch != null)
                    {
                        Console.WriteLine("SALE-L00-00-0036 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch provided");

                        //SALE-L02-00-0034
                        //Sales Batch
                        //Only one occurrence
                        if (salesReportSalesDocument.SpecifiedSalesBatch.Count() == 1)
                        {
                            Console.WriteLine("SALE-L02-00-0034 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch Count == 1");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0034 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch Count != 1");
                            //SALE-L02-00-0034 - error
                        }

                        //#Q As cardinality is up to 1, take and validate contents of the first element only
                        var salesDocumentSalesBatch = salesReportSalesDocument.SpecifiedSalesBatch.First();

                        //SALE-L02-00-0055, SALE-L00-00-0060, SALE-L01-00-0060, SALE-L02-00-0060, SALE-L01-00-0062, SALE-L01-00-0063, SALE-L01-00-0064, SALE-L01-00-0065,
                        //SALE-L01-00-0061, SALE-L00-00-0061, SALE-L01-00-0067, SALE-L00-00-0062, SALE-L01-00-0071, SALE-L01-00-0070, SALE-L02-00-0070, SALE-L00-00-0070,
                        //SALE-L02-00-0071, SALE-L00-00-0063, SALE-L00-00-0065, SALE-L01-00-0071, SALE-L01-00-0070, SALE-L02-00-0070, SALE-L00-00-0070, SALE-L02-00-0071,
                        //SALE-L00-00-0063, SALE-L00-00-0065, SALE-L00-00-0080, SALE-L01-00-0080, SALE-L00-00-0081, SALE-L01-00-0082, SALE-L02-00-0081, SALE-L02-00-0080,
                        //SALE-L02-00-0090, SALE-L01-00-0090, SALE-L01-00-0091, SALE-L01-00-0092
                        #region SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct
                        //SALE-L02-00-0055
                        //AAP_Product
                        //At least one must exist
                        if (salesDocumentSalesBatch.SpecifiedAAPProduct.Count() >= 1)
                        {
                            Console.WriteLine("SALE-L02-00-0055 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct Count >= 1");

                            foreach (var salesBatchSpecifiedAAPProduct in salesDocumentSalesBatch.SpecifiedAAPProduct)
                            {
                                //SALE-L00-00-0060
                                //AAP Product/Species
                                //Must be present
                                if (salesBatchSpecifiedAAPProduct.SpeciesCode != null && salesBatchSpecifiedAAPProduct.SpeciesCode.Value?.ToString() != "")
                                {
                                    Console.WriteLine("SALE-L00-00-0060 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpeciesCode provided");

                                    Console.WriteLine("SALE-L01-00-0060 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpeciesCode.Value is from FAO_SPECIES list");
                                    //SALE-L01-00-0060
                                    //AAP Product/Species
                                    //Check code from the list listID
                                    //TODO: Check SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpeciesCode.Value is from FAO_SPECIES list
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0060 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpeciesCode Count provided");
                                    //SALE-L00-00-0060 - error
                                }

                                //SALE-L02-00-0060
                                //AAP Product/Unit, AAP Product/Weight
                                //One of the two must be available
                                if (salesBatchSpecifiedAAPProduct.WeightMeasure != null ^ salesBatchSpecifiedAAPProduct.UnitQuantity != null)
                                {
                                    Console.WriteLine("SALE-L02-00-0060 | OK | WeightMeasure or UnitQuantity in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct provided");

                                    if (salesBatchSpecifiedAAPProduct.WeightMeasure != null)
                                    {
                                        //SALE-L01-00-0062
                                        //AAP Product/Weight
                                        //Check unit code: must be KGM
                                        if (salesBatchSpecifiedAAPProduct.WeightMeasure.unitCode?.ToString() == "KGM")
                                        {
                                            Console.WriteLine("SALE-L01-00-0062 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.unitCode provided and == KGM");

                                            //SALE-L01-00-0063
                                            //AAP Product/Weight
                                            //Not a negative number
                                            if (salesBatchSpecifiedAAPProduct.WeightMeasure.Value > 0)
                                            {
                                                Console.WriteLine("SALE-L01-00-0063 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.Value is not a negative number");
                                            }
                                            //SALE-L01-00-0064
                                            //AAP Product/Weight
                                            //Weight = 0
                                            else if (salesBatchSpecifiedAAPProduct.WeightMeasure.Value == 0)
                                            {
                                                Console.WriteLine("SALE-L01-00-0064 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.Value == 0");
                                                //SALE-L01-00-0064 - warning
                                            }
                                            else
                                            {
                                                Console.WriteLine("SALE-L01-00-0063 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.Value is a negative number");
                                                //SALE-L01-00-0063 - error
                                            }

                                            //SALE-L01-00-0065
                                            //AAP Product/Weight
                                            //No more than 2 decimals
                                            if (BitConverter.GetBytes(decimal.GetBits(salesBatchSpecifiedAAPProduct.WeightMeasure.Value)[3])[2] <= 2)
                                            {
                                                Console.WriteLine("SALE-L01-00-0065 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.Value decimal places <= 2");
                                            }
                                            else
                                            {
                                                Console.WriteLine("SALE-L01-00-0065 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.Value decimal places > 2");
                                                //SALE-L01-00-0065 - warning
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0062 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.WeightMeasure.unitCode provided or != KGM");
                                            //SALE-L01-00-0062 - error
                                        }
                                    }

                                    if (salesBatchSpecifiedAAPProduct.UnitQuantity != null)
                                    {
                                        //SALE-L01-00-0061
                                        //AAP Product/Unit
                                        //Positive number
                                        if (salesBatchSpecifiedAAPProduct.UnitQuantity.Value > 0)
                                        {
                                            Console.WriteLine("SALE-L01-00-0061 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UnitQuantity.Value > 0");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0061 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UnitQuantity.Value <= 0");
                                            //SALE-L01-00-0061 - error
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L02-00-0060 | ERROR | Neither of WeightMeasure or UnitQuantity in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct provided");
                                    //SALE-L02-00-0060
                                }

                                //SALE-L00-00-0061
                                //AAP Product/Usage
                                //Must be present
                                if (salesBatchSpecifiedAAPProduct.UsageCode != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0061 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UsageCode provided");

                                    Console.WriteLine("SALE-L01-00-0067 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UsageCode.Value is from PROD_USAGE list");
                                    //SALE-L01-00-0067
                                    //AAP Product/Usage
                                    //Check code from the list listID
                                    //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UsageCode.Value is from PROD_USAGE list

                                    valueUsageCode = salesBatchSpecifiedAAPProduct.UsageCode.Value;
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0061 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.UsageCode provided");
                                    //SALE-L00-00-0061 - error
                                }

                                //SALE-L00-00-0062
                                //AAP_Process
                                //Must be present
                                if (salesBatchSpecifiedAAPProduct.AppliedAAPProcess != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0062 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess provided");

                                    //#Q As cardinality is up to 1, take and validate the first element only
                                    var specifiedAAPProductAppliedAAPProcess = salesBatchSpecifiedAAPProduct.AppliedAAPProcess.First();

                                    if (specifiedAAPProductAppliedAAPProcess.ConversionFactorNumeric != null)
                                    {
                                        //SALE-L01-00-0071
                                        //AAP Process/Conversion factor
                                        //Positive number
                                        if (specifiedAAPProductAppliedAAPProcess.ConversionFactorNumeric.Value > 0)
                                        {
                                            Console.WriteLine("SALE-L01-00-0071 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.ConversionFactorNumeric.Value > 0");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0071 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.ConversionFactorNumeric.Value <= 0");
                                            //SALE-L01-00-0071 - error
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.ConversionFactorNumeric provided");
                                    }

                                    //#Q Added cardinality check - min 2, max 3 for TypeCode
                                    if (specifiedAAPProductAppliedAAPProcess.TypeCode != null && (specifiedAAPProductAppliedAAPProcess.TypeCode.Count() == 2 || specifiedAAPProductAppliedAAPProcess.TypeCode.Count() == 3))
                                    {
                                        foreach (var appliedAAPProcessTypeCode in specifiedAAPProductAppliedAAPProcess.TypeCode)
                                        {
                                            Console.WriteLine("SALE-L01-00-0070 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode.Value is from listID");
                                            //SALE-L01-00-0070
                                            //AAP Process/Type
                                            //Check code from the list listID
                                            //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode.Value is from listID
                                            //FISH_FRESHNESS, FISH_PRESENTATION, FISH_PRESERVATION [+ Value]
                                            //ListID code from the FLUX_PROCESS_TYPE list
                                        }

                                        //SALE-L02-00-0070
                                        //AAP Process/Type
                                        //No duplicated type codes
                                        if (specifiedAAPProductAppliedAAPProcess.TypeCode.GroupBy(g => g.Value).Where(w => w.Count() > 1).Count() == 0)
                                        {
                                            Console.WriteLine("SALE-L02-00-0070 | OK | No duplicates in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode.Value");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0070 | ERROR | Duplicates in SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode.Value");
                                            //SALE-L02-00-0070 - error
                                        }

                                        //SALE-L00-00-0070
                                        //AAP Process/Type
                                        //Freshness must be present
                                        if (specifiedAAPProductAppliedAAPProcess.TypeCode.Any(a => a.listID?.ToString() == "FISH_FRESHNESS"))
                                        {
                                            Console.WriteLine("SALE-L00-00-0070 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode for freshness provided");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L00-00-0070 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode for freshness provided");
                                            //SALE-L00-00-0070 - error
                                        }

                                        //SALE-L02-00-0071
                                        //AAP Process/Type
                                        //Presentation & preservation must be present
                                        if (specifiedAAPProductAppliedAAPProcess.TypeCode.Any(a => a.listID?.ToString() == "FISH_PRESENTATION") && specifiedAAPProductAppliedAAPProcess.TypeCode.Any(a => a.listID?.ToString() == "FISH_PRESERVATION"))
                                        {
                                            Console.WriteLine("SALE-L02-00-0071 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode for presentation and preservation provided");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0071 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.TypeCode for presentation and preservation provided");
                                            //SALE-L02-00-0071 - error
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess.ConversionFactorNumeric provided or count != 2 || 3");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0062 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess provided");
                                    //SALE-L00-00-0062 - error
                                }

                                //SALE-L00-00-0063
                                //Size_Distribution
                                //Must be present
                                if (salesBatchSpecifiedAAPProduct.SpecifiedSizeDistribution != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0063 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution provided");

                                    //SALE-L00-00-0080
                                    //Size Distribution/Class
                                    //Must be present
                                    if (salesBatchSpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0080 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode provided");

                                        //#Q As cardinality is up to one, take and validate the first element only
                                        var specifiedSizeDistributionClassCode = salesBatchSpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode.First();

                                        Console.WriteLine("SALE-L01-00-0080 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode.Value is from FISH_SIZE_CLASS list");
                                        //SALE-L01-00-0080
                                        //Size Distribution/Class
                                        //Check code from the list listID
                                        //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode.Value is from FISH_SIZE_CLASS list

                                        valueClassCode = specifiedSizeDistributionClassCode.Value;
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0080 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.ClassCode provided");
                                        //SALE-L00-00-0080 - error
                                    }

                                    //SALE-L00-00-0081
                                    //Size Distribution/Category
                                    //Must be present
                                    if (salesBatchSpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0081 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode provided");

                                        Console.WriteLine("SALE-L01-00-0082 | TODO | Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode is from FISH_SIZE_CATEGORY list");
                                        //SALE-L01-00-0082
                                        //Size Distribution/Category
                                        //Check code from the list listID
                                        //TODO: Check if SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode is from FISH_SIZE_CATEGORY list

                                        if (valueClassCode == "BMS")
                                        {
                                            //SALE-L02-00-0081
                                            //Size Distribution/Class, Size Distribution/Category
                                            //If BMS species, the category should be N/A
                                            if (salesBatchSpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode.Value?.ToString() == "N/A")
                                            {
                                                Console.WriteLine("SALE-L02-00-0081 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode.Value == N/A provided for Class BMS");
                                            }
                                            else
                                            {
                                                Console.WriteLine("SALE-L02-00-0081 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode.Value != N/A provided for Class BMS");
                                                //SALE-L02-00-0081 - error
                                            }

                                            //SALE-L02-00-0080
                                            //AAP_Product/Usage, Size Distribution/Class
                                            //Usage must be for non direct human consumption if BMS species
                                            //#Q X to be replaced with the code for non direct human consumption
                                            if (valueUsageCode != "X")
                                            {
                                                Console.WriteLine("SALE-L02-00-0080 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.UsageCode.Value != X provided for Class BMS");
                                            }
                                            else
                                            {
                                                Console.WriteLine("SALE-L02-00-0080 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.UsageCode.Value == X provided for Class BMS");
                                                //SALE-L02-00-0080 - warning
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0081 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution.CategoryCode provided");
                                        //SALE-L00-00-0081 - error
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0063 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.SpecifiedSizeDistribution provided");
                                    //SALE-L00-00-0063 - error
                                }

                                if (salesBatchSpecifiedAAPProduct.TotalSalesPrice != null)
                                {
                                    //As cardinality is up to one, take and validate the first element only
                                    var specifiedAAPProductTotalSalesPrice = salesBatchSpecifiedAAPProduct.TotalSalesPrice.First();

                                    if (valueSalesReportTypeCode == "SN")
                                    {
                                        //SALE-L02-00-0090
                                        //Sales_Report/ItemType Sales Price/Charge
                                        //Mandatory for products of sales notes
                                        if (specifiedAAPProductTotalSalesPrice.Value != null)
                                        {
                                            Console.WriteLine("SALE-L02-00-0090 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value provided for Sales Note");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0090 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.AppliedAAPProcess provided for Sales Note");
                                            //SALE-L02-00-0090 - error
                                        }
                                    }

                                    //SALE-L01-00-0090
                                    //Sales Price/Charge
                                    //Positive or zero value
                                    if (specifiedAAPProductTotalSalesPrice.Value >= 0)
                                    {
                                        Console.WriteLine("SALE-L01-00-0090 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value is positive or zero");

                                        listOfSalesPrices.Add(specifiedAAPProductTotalSalesPrice.Value);

                                        //SALE-L01-00-0091
                                        //Sales Price/Charge
                                        //Charge = 0
                                        if (specifiedAAPProductTotalSalesPrice.Value == 0)
                                        {
                                            Console.WriteLine("SALE-L01-00-0091 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value != 0");

                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0091 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value == 0");
                                            //SALE-L01-00-0091 - warning
                                        }

                                        //SALE-L01-00-0092
                                        //Sales Price/Charge
                                        //No more than 2 decimals
                                        if (BitConverter.GetBytes(decimal.GetBits(specifiedAAPProductTotalSalesPrice.Value)[3])[2] <= 2)
                                        {
                                            Console.WriteLine("SALE-L01-00-0092 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value decimal places <= 2");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0092 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value decimal places > 2");
                                            //SALE-L01-00-0092 - warning
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L01-00-0090 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value is negative");
                                        //SALE-L01-00-0090 - error
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice provided");
                                }

                                //SALE-L00-00-0065
                                //FLUX_location
                                //Must be present
                                if (salesBatchSpecifiedAAPProduct.OriginFLUXLocation != null)
                                {
                                    Console.WriteLine("SALE-L00-00-0065 | OK | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.OriginFLUXLocation provided");

                                    foreach (var specifiedAAPProductOriginFLUXLocation in salesBatchSpecifiedAAPProduct.OriginFLUXLocation)
                                    {
                                        FLUXLocationValidationElement(entity: "SpecifiedAAPProduct", locationElement: specifiedAAPProductOriginFLUXLocation);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L00-00-0065 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.OriginFLUXLocation provided");
                                    //SALE-L00-00-0065 - error
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0055 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct Count < 1");
                            //SALE-L02-00-0055 - error
                        }
                        #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0036 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch provided");
                        //SALE-L00-00-0036 - error
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.SpecifiedSalesBatch

                    //SALE-L01-00-0090, SALE-L01-00-0091, SALE-L01-00-0092, SALE-L02-00-0092
                    #region SalesReport.SalesDocument.IncludedSalesDocument.TotalSalesPrice
                    //#Q Mainly the same as for every SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice
                    if (salesReportSalesDocument.TotalSalesPrice != null)
                    {
                        //As cardinality is up to one, take and validate the first element only
                        var salesReportSalesDocumentTotalSalesPrice = salesReportSalesDocument.TotalSalesPrice.First();

                        //SALE-L01-00-0090
                        //Sales Price/Charge
                        //Positive or zero value
                        if (salesReportSalesDocumentTotalSalesPrice.Value >= 0)
                        {
                            Console.WriteLine("SALE-L01-00-0090 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value is positive or zero");

                            decimal valueTotalSalesPrice = salesReportSalesDocumentTotalSalesPrice.Value;
                            decimal sumOfProductSalesPrices = listOfSalesPrices.Sum();

                            //SALE-L01-00-0091
                            //Sales Price/Charge
                            //Charge = 0
                            if (salesReportSalesDocumentTotalSalesPrice.Value == 0)
                            {
                                Console.WriteLine("SALE-L01-00-0091 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value != 0");

                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0091 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value == 0");
                                //SALE-L01-00-0091 - warning
                            }

                            //SALE-L01-00-0092
                            //Sales Price/Charge
                            //No more than 2 decimals
                            if (BitConverter.GetBytes(decimal.GetBits(salesReportSalesDocumentTotalSalesPrice.Value)[3])[2] <= 2)
                            {
                                Console.WriteLine("SALE-L01-00-0092 | OK | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value decimal places <= 2");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L01-00-0092 | WARNING | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value decimal places > 2");
                                //SALE-L01-00-0092 - warning
                            }

                            //SALE-L02-00-0092
                            //Sales Price/Charge
                            //Total price, if present, must be the total of the product prices
                            if (valueTotalSalesPrice == sumOfProductSalesPrices)
                            {
                                Console.WriteLine("SALE-L02-00-0092 | OK | SalesReport.SalesReport.IncludedSalesDocument.TotalSalesPrice.Value == Sum of all Product Sales Price");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L02-00-0092 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.TotalSalesPrice.Value == Sum of all Product Sales Price");
                                //SALE-L02-00-0092 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0090 | ERROR | SalesReport.SalesReport.IncludedSalesDocument.SpecifiedSalesBatch.SpecifiedAAPProduct.TotalSalesPrice.Value is negative");
                            //SALE-L01-00-0090 - error
                        }
                    }
                    else
                    {
                        if (valueSalesReportTypeCode == "SN")
                        {
                            Console.WriteLine("No SalesReport.SalesReport.IncludedSalesDocument.TotalSalesPrice provided for Sales Note");
                        }
                    }
                    #endregion SalesReport.SalesDocument.IncludedSalesDocument.TotalSalesPrice
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0022 | ERROR | No SalesReport.SalesReport.IncludedSalesDocument provided");
                    //SALE-L00-00-0022 - error
                }
                #endregion SalesReport.SalesReport.IncludedSalesDocument
            }
            else
            {
                Console.WriteLine("No SalesReport.FLUXReportDocument provided");
            }
            #endregion SalesReport.SalesReport

            //SALE-L03-00-0130, SALE-L03-00-0110
            #region Boolean Validations
            if (valueCFR != "" && valueVesselCountry != "" && dateTimeDelimitedPeriodStart != DateTime.MinValue)
            {
                Console.WriteLine("SALE-L03-00-0130 | TODO | Check if vessel with CFR [valueCFR] was under the flag state [valueVesselCountry] at landing date [dateTimeDelimitedPeriodStart]");
                //SALE-L03-00-0130
                //Vessel Transport Means/Identification, Vessel Country/ Identification, Delimited / Period / Start
                //If CFR, the vessel should be in the EU fleet under the flag state at landing date
                //TODO: Check if vessel with CFR was under the flag state at landing date
            }

            if (valuePurposeCode == "9" && dateTimeFluxReportDocumentCreation != DateTime.MinValue && dateTimeDelimitedPeriodStart != DateTime.MinValue && valueSalesReportTypeCode == "TOD")  //#Q Not resulting from a query
            {
                Console.WriteLine("SALE-L03-00-0130 | TODO | Check if TOD reception date is in 24 after the landing declaration date");
                //SALE-L03-00-0110
                //FLUX Report Document/Creation, FLUX Report Document / Referenced Identification, FishingActivity / SpecifiedDelimitedPeriod
                //If the report is not resulting from a query, the reception date (by the Take-Over state) should not be later than 24h after the landing declaration date 
                //For take-over document (TOD) That's the maximum delay. Another rule should check if received under 24h under some conditions (financial turnover). 
                //TODO: Check if reception date is in 24 after the landing declaration date
            }
            #endregion Boolean Validations
        }

        //SALE-L00-00-0180, SALE-L01-00-0180, SALE-L01-00-0181, SALE-L02-00-0180, SALE-L02-00-0181, SALE-L02-00-0182, SALE-L01-00-0182, SALE-L01-00-0183, SALE-L02-00-0183, SALE-L01-00-0184,
        //SALE-L01-00-0185, SALE-L01-00-0186, SALE-L01-00-0187, SALE-L02-00-0188, SALE-L02-00-0184, SALE-L02-00-0185, SALE-L02-00-0186, SALE-L02-00-0187, SALE-L00-00-0190, SALE-L01-00-0190,
        //SALE-L01-00-0191, SALE-L00-00-0191, SALE-L01-00-0192, SALE-L01-00-0193, SALE-L00-00-0200, SALE-L00-00-0201, SALE-L01-00-0200, SALE-L00-00-0202, SALE-L02-00-0200
        private static void FLUXLocationValidationElement(string entity, FLUXLocationType locationElement)
        {
            string valueLocationTypeCode = "";

            //entity: SalesDocument, SpecifiedAAPProduct or SpecifiedFishingActivity
            //cardinality: SalesDocument [1-1], FishingActivity [1-1], AAPProduct [1-*]
            Console.WriteLine("########### FLUXLocation Validation Start ###########");
            Console.WriteLine("#### ENTITY: " + entity);

            if (locationElement != null)
            {
                //SALE-L00-00-0180
                //FLUX_ Location/TypeCode
                //Check presence. Must be present.
                if (locationElement.TypeCode?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0180 | OK | FLUXLocation.TypeCode provided");

                    //SALE-L01-00-0180
                    //FLUX_ Location/TypeCode
                    //Check attribute listID. Must be FLUX_LOCATION_TYPE
                    if (locationElement.TypeCode.listID?.ToString() == "FLUX_LOCATION_TYPE")
                    {
                        Console.WriteLine("#### SALE-L01-00-0180 | OK | FLUXLocation.TypeCode.listID provided and == FLUX_LOCATION_TYPE");

                        Console.WriteLine("#### SALE-L01-00-0181 | TODO | Check if FLUXLocation.TypeCode.Value provided is in FLUX_LOCATION_TYPE list");
                        //SALE-L01-00-0181
                        //FLUX_ Location/TypeCode
                        //Check code. Must be existing in the list specified in attribute listID
                        //TODO: Check if FLUXLocation.TypeCode.Value is in FLUX_LOCATION_TYPE list

                        valueLocationTypeCode = locationElement.TypeCode.Value.ToString();

                        if (entity == "SpecifiedAAPProduct")
                        {
                            //SALE-L02-00-0180
                            //FLUX_ Location, AAP_product
                            //It must not be a LOCATION code for AAP products
                            if (locationElement.TypeCode.Value.ToString() != "LOCATION")
                            {
                                Console.WriteLine("#### SALE-L02-00-0180 | OK | FLUXLocation.TypeCode.Value != LOCATION for SpecifiedAAPProduct entity");
                            }
                            else
                            {
                                Console.WriteLine("#### SALE-L02-00-0180 | ERROR | FLUXLocation.TypeCode.Value == LOCATION for SpecifiedAAPProduct entity");
                                //SALE-L02-00-0180 - error
                            }

                            //SALE-L01-00-0187
                            //AAP_Product and FLUX_ Location/Identification
                            //FAO_AREA code is mandatory
                            if (locationElement.ID?.schemeID?.ToString() == "FAO_AREA")
                            {
                                Console.WriteLine("#### SALE-L01-00-0187 | OK | FLUXLocation.ID.schemeID == FAO_AREA for SpecifiedAAPProduct entity");

                                Console.WriteLine("#### SALE-L02-00-0188 | TODO | Check if only one FLUXLocation.ID.schemeID == FAO_AREA per product");
                                //SALE-L02-00-0188
                                //AAP_Product and FLUX_ Location/Identification
                                //Only one FAO_AREA code per product
                                //#Q How is this checked, as locationElement.ID is not an Array => there is only one ID, whatever its schemeID?
                            }
                            else
                            {
                                Console.WriteLine("#### SALE-L01-00-0187 | ERROR | FLUXLocation.ID.schemeID != FAO_AREA for SpecifiedAAPProduct entity");
                                //SALE-L01-00-0187 - error
                            }
                        }
                        else if (entity == "SpecifiedFishingActivity")
                        {
                            //SALE-L02-00-0181
                            //FLUX_ Location, Fishing_Activity
                            //It must be a LOCATION code for the landing
                            if (locationElement.TypeCode.Value.ToString() == "LOCATION")
                            {
                                Console.WriteLine("#### SALE-L02-00-0181 | OK | FLUXLocation.TypeCode.Value == LOCATION for SpecifiedFishingActivity entity");
                            }
                            else
                            {
                                Console.WriteLine("#### SALE-L02-00-0181 | ERROR | FLUXLocation.TypeCode.Value != LOCATION for SpecifiedFishingActivity entity");
                                //SALE-L02-00-0181 - error
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L01-00-0180 | ERROR | No FLUXLocation.TypeCode.listID provided or != FLUX_LOCATION_TYPE");
                        //SALE-L01-00-0180 - error
                    }
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0180 | OK | No FLUXLocation.TypeCode provided");
                    //SALE-L00-00-0180 - error
                }
            }
            else
            {
                Console.WriteLine("#### No FLUXLocation provided");
            }

            if (valueLocationTypeCode != "")
            {
                if (valueLocationTypeCode == "LOCATION" || valueLocationTypeCode == "ADDRESS")
                {
                    //SALE-L02-00-0182
                    //FLUX_ Location/Country, FLUX_ Location/TypeCode
                    //Check presence of the country. Must be present if Type is LOCATION or ADDRESS.
                    if (locationElement.CountryID?.Value != null)
                    {
                        Console.WriteLine("#### SALE-L02-00-0182 | OK | FLUXLocation.CountryID.Value provided for TypeCode == LOCATION || ADDRESS");

                        //SALE-L01-00-0182
                        //FLUX_ Location/Country
                        //Check attribute schemeID. Must be TERRITORY
                        if (locationElement.CountryID.schemeID?.ToString() == "TERRITORY")
                        {
                            Console.WriteLine("#### SALE-L01-00-0182 | OK | FLUXLocation.CountryID.schemeID provided and == TERRITORY");

                            Console.WriteLine("#### SALE-L01-00-0183 | TODO | Check if locationElement.CountryID.Value is in TERRITORY list");
                            //SALE-L01-00-0183
                            //FLUX_ Location/Country
                            //Check code. Must be existing in the list specified in attribute schemeID
                            //TODO: Check if locationElement.CountryID.Value is in TERRITORY list\

                            if (valueLocationTypeCode == "LOCATION")
                            {
                                Console.WriteLine("#### SALE-L02-00-0184 | TODO | Check if locationElement.ID.Value is in FLUXLocation.CountryID.Value, for Type == LOCATION");
                                //SALE-L02-00-0184
                                //FLUX_ Location/Identification, FLUX_ Location/Country
                                //If LOCATION code, the place must be in the country
                                //TODO: Check if locationElement.ID.Value is in FLUXLocation.CountryID.Value, for Type == LOCATION
                            }
                        }
                        else
                        {
                            Console.WriteLine("#### SALE-L01-00-0182 | ERROR | No FLUXLocation.CountryID.schemeID provided or != TERRITORY");
                            //SALE-L01-00-0182 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L02-00-0182 | ERROR | No FLUXLocation.CountryID.Value provided for TypeCode == LOCATION || ADDRESS");
                        //SALE-L02-00-0182 - error
                    }
                }

                if (valueLocationTypeCode != "POSITION" && valueLocationTypeCode != "ADDRESS")
                {
                    //SALE-L02-00-0183
                    //FLUX_ Location/Identification, FLUX_ Location/TypeCode
                    //Check presence. Must be present, unless type is "POSITION" or "ADDRESS"
                    if (locationElement.ID?.Value != null)
                    {
                        Console.WriteLine("#### SALE-L02-00-0183 | OK | FLUXLocation.ID.Value provided for TypeCode != POSITION || ADDRESS");

                        //SALE-L01-00-0184
                        //FLUX_ Location/Identification
                        //Check attribute schemeID. In case Type= "AREA": must be FAO_ AREA, , TERRITORY. In case Type= "LOCATION": must be LOCATION, FARM
                        if (valueLocationTypeCode == "AREA")
                        {
                            if (locationElement.ID.schemeID?.ToString() == "FAO_AREA" || locationElement.ID.schemeID?.ToString() == "TERRITORY")
                            {
                                Console.WriteLine("#### SALE-L01-00-0184 | OK | FLUXLocation.ID.schemeID provided for TypeCode AREA and == FAO_AREA || TERRITORY");

                                if (locationElement.ID.schemeID?.ToString() == "FAO_AREA")
                                {
                                    Console.WriteLine("#### SALE-L01-00-0186 | TODO | Check if FLUXLocation.ID.Value is in FLUXLocation.ID.schemeID list provided");
                                    //SALE-L01-00-0186 - error
                                    //FLUX_ Location/Identification
                                    //Check code. Must be existing in the list specified in attribute schemeID
                                    //TODO: Check if FLUXLocation.ID.Value is in FLUXLocation.ID.schemeID list provided

                                    Console.WriteLine("#### SALE-L01-00-0185 | TODO | Check if more precise area is mentioned");
                                    //SALE-L01-00-0185 - warning
                                    //FLUX_ Location/Identification
                                    //If FAO_ AREA code, the more precise area must be mentioned
                                    //In the FAO_AREA list, the terminal indicator is set to 'Y' for such areas
                                    //TODO: Check if more precise area is mentioned
                                }
                            }
                            else
                            {
                                Console.WriteLine("#### SALE-L01-00-0184 | ERROR | No FLUXLocation.ID.schemeID provided for TypeCode AREA or != FAO_AREA || TERRITORY");
                                //SALE-L01-00-0184 - error
                            }
                        }
                        else if (valueLocationTypeCode == "LOCATION")
                        {
                            if (locationElement.ID.schemeID?.ToString() == "LOCATION" || locationElement.ID.schemeID?.ToString() == "FARM")
                            {
                                Console.WriteLine("#### SALE-L01-00-0184 | OK | FLUXLocation.ID.schemeID provided for TypeCode LOCATION and == LOCATION || FARM");

                                Console.WriteLine("#### SALE-L01-00-0186 | TODO | Check if FLUXLocation.ID.Value is in FLUXLocation.ID.schemeID list provided");
                                //SALE-L01-00-0186 - error
                                //FLUX_ Location/Identification
                                //Check code. Must be existing in the list specified in attribute schemeID
                                //TODO: Check if FLUXLocation.ID.Value is in FLUXLocation.ID.schemeID list provided
                            }
                            else
                            {
                                Console.WriteLine("#### SALE-L01-00-0184 | ERROR | No FLUXLocation.ID.schemeID provided for TypeCode LOCATION or != LOCATION || FARM");
                                //SALE-L01-00-0184 - error
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L02-00-0183 | ERROR | No FLUXLocation.CountryID.Value provided for TypeCode != POSITION || ADDRESS");
                        //SALE-L02-00-0183 - error
                    }
                }

                if (valueLocationTypeCode == "POSITION")
                {
                    //SALE-L02-00-0185
                    //FLUX_ Location/TypeCode, FLUX, Geographical Coordinate
                    //Check presence of the FLUX Geographical Coordinate. Must be present if TypeCode in Flux Location = POSITION.
                    if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate != null)
                    {
                        Console.WriteLine("#### SALE-L02-00-0185 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate provided for TypeCode POSITION");
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L02-00-0185 | ERROR | No FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate provided for TypeCode POSITION");
                        //SALE-L02-00-0185 - error
                    }

                    if (locationElement.ID == null && locationElement.PhysicalStructuredAddress == null)
                    {
                        //SALE-L02-00-0186
                        //FLUX Geographical Coordinate/Latitude, FLUX Geographical, Coordinate/ Longitude, FLUX_ Location/ Identification
                        //Latitude & longitude must be provided if there is no identification and no address
                        if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure?.Value != null && locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure?.Value != null)
                        {
                            Console.WriteLine("#### SALE-L02-00-0186 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value and LongitudeMeasure.Value provided for TypeCode POSITION when ID or Address missing");
                        }
                        else
                        {
                            Console.WriteLine("#### SALE-L02-00-0186 | ERROR | No FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value and LongitudeMeasure.Value provided for TypeCode POSITION when ID or Address missing");
                            //SALE-L02-00-0186 - error
                        }
                    }
                }

                if (valueLocationTypeCode == "ADDRESS")
                {
                    //SALE-L02-00-0187
                    //Physical StructuredAddress, FLUX_ Location/TypeCode
                    //Check presence. Must be present if location Type is ADDRESS.
                    if (locationElement.PhysicalStructuredAddress != null)
                    {
                        Console.WriteLine("#### SALE-L02-00-0187 | OK | FLUXLocation.PhysicalStructuredAddress provided for TypeCode ADDRESS");
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L02-00-0187 | ERROR | No FLUXLocation.PhysicalStructuredAddress provided for TypeCode ADDRESS");
                        //SALE-L02-00-0187 - error
                    }
                }
            }

            if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate != null)
            {
                //SALE-L00-00-0190
                //FLUX Geographical Coordinate/LatitudeMeasure
                //Check presence. Must be present
                if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0190 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value provided");

                    //SALE-L01-00-0190
                    //FLUX Geographical Coordinate/LatitudeMeasure
                    //Check format. Must be number.
                    //#Q Already returns decimal, is there a better way to check if it is a number?
                    if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value is decimal)
                    {
                        Console.WriteLine("#### SALE-L01-00-0190 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value is decimal");

                        //SALE-L01-00-0191
                        //FLUX Geographical Coordinate/LatitudeMeasure
                        //Check range. Must be between -90 and 90
                        //Boundaries follow the EPSG definition.
                        if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value >= -90 && locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value <= 90)
                        {
                            Console.WriteLine("#### SALE-L01-00-0191 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value in range [-90, 90]");
                        }
                        else
                        {
                            Console.WriteLine("#### SALE-L01-00-0191 | ERROR | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value not in range [-90, 90]");
                            //SALE-L01-00-0191 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L01-00-0190 | ERROR | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value is not decimal");
                        //SALE-L01-00-0190 - error
                    }
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0190 | ERROR | No FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LatitudeMeasure.Value provided");
                    //SALE-L00-00-0190 - error
                }

                //SALE-L00-00-0191
                //FLUX Geographical Coordinate/LongitudeMeasure
                //Check presence. Must be present.
                if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0191 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value provided");

                    //SALE-L01-00-0192
                    //FLUX Geographical Coordinate/LongitudeMeasure
                    //Check format. Must be number.
                    //#Q Already returns decimal, is there a better way to check if it is a number?
                    if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value is decimal)
                    {
                        Console.WriteLine("#### SALE-L01-00-0192 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value is decimal");

                        //SALE-L01-00-0193
                        //FLUX Geographical Coordinate/LongitudeMeasure
                        //Check range. Must be between -180 and 180
                        //Boundaries follow the EPSG definition.
                        if (locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value >= -180 && locationElement.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value <= 180)
                        {
                            Console.WriteLine("#### SALE-L01-00-0193 | OK | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value in range [-180, 180]");
                        }
                        else
                        {
                            Console.WriteLine("#### SALE-L01-00-0193 | ERROR | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value not in range [-180, 180]");
                            //SALE-L01-00-0193 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L01-00-0192 | ERROR | FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value is not decimal");
                        //SALE-L01-00-0192 - error
                    }
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0191 | ERROR | No FLUXLocation.SpecifiedPhysicalFLUXGeographicalCoordinate.LongitudeMeasure.Value provided");
                    //SALE-L00-00-0191 - error
                }
            }

            if (locationElement.PhysicalStructuredAddress != null)
            {
                //SALE-L00-00-0200
                //Structured Address/City Name
                //Must be present
                if (locationElement.PhysicalStructuredAddress.CityName?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0200 | OK | FLUXLocation.PhysicalStructuredAddress.CityName.Value provided");
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0200 | ERROR | No FLUXLocation.PhysicalStructuredAddress.CityName.Value provided");
                    //SALE-L00-00-0200 - error
                }

                //SALE-L00-00-0201
                //Structured Address/Country
                //Must be present
                if (locationElement.PhysicalStructuredAddress.CountryID?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0201 | OK | FLUXLocation.PhysicalStructuredAddress.CountryID.Value provided");

                    Console.WriteLine("#### SALE-L01-00-0200 | TODO | Check if locationElement.PhysicalStructuredAddress.CountryID.Value is from schemeID list");
                    //SALE-L01-00-0200
                    //Structured Address/Country
                    //Check code from the list schemeID
                    //TODO: Check if locationElement.PhysicalStructuredAddress.CountryID.Value is from schemeID list

                    //SALE-L02-00-0200
                    //FLUX Location/Country, Structured Address/Country
                    //Should be the same value
                    if (locationElement.PhysicalStructuredAddress.CountryID.Value?.ToString() == locationElement.CountryID?.Value?.ToString())
                    {
                        Console.WriteLine("#### SALE-L02-00-0200 | OK | FLUXLocation.PhysicalStructuredAddress.CountryID.Value provided == FLUXLocation.CountryID.Value");
                    }
                    else
                    {
                        Console.WriteLine("#### SALE-L02-00-0200 | WARNING | FLUXLocation.PhysicalStructuredAddress.CountryID.Value provided != FLUXLocation.CountryID.Value");
                        //SALE-L02-00-0200 - warning
                    }
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0201 | WARNING | No FLUXLocation.PhysicalStructuredAddress.CountryID.Value provided");
                    //SALE-L00-00-0201 - warning
                }

                //SALE-L00-00-0202
                //Structured Address/Street Name
                //Must be present
                if (locationElement.PhysicalStructuredAddress.StreetName?.Value != null)
                {
                    Console.WriteLine("#### SALE-L00-00-0202 | OK | FLUXLocation.PhysicalStructuredAddress.StreetName.Value provided");
                }
                else
                {
                    Console.WriteLine("#### SALE-L00-00-0202 | WARNING | FLUXLocation.PhysicalStructuredAddress.StreetName.Value provided");
                    //SALE-L00-00-0202 - warning
                }
            }

            Console.WriteLine("########### FLUXLocation Validation End ###########");
        }
    }
}
