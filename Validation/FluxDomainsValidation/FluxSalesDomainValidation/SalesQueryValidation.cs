using System;
using System.Linq;
using ScortelApi.Models.FLUX;
using System.Text.RegularExpressions;


namespace Validation.FluxDomainsValidation.FluxSalesDomainValidation
{
    class SalesQueryValidation
    {
        public void SalesQueryValidate(FLUXSalesQueryMessageType SalesQuery)
        {
            string valueSalesQueryTypeCode = "";
            string valueFLUXPartyID = "";

            DateTime dateTimeDelimitedPeriodStartValue = new DateTime();
            DateTime dateTimeDelimitedPeriodEndValue = new DateTime();

            if (SalesQuery.SalesQuery != null)
            {
                //SALE-L00-00-0400, SALE-L01-00-0400, SALE-L03-00-0400
                #region SalesQuery.SalesQuery.ID
                //SALE-L00-00-0400
                //Sales Query/Identification
                //Must be present
                if (SalesQuery.SalesQuery.ID != null)
                {
                    Console.WriteLine("SALE-L00-00-0400 | OK | SalesQuery.SalesQuery.ID provided");

                    if (SalesQuery.SalesQuery.ID.schemeID?.ToString() == "UUID" && SalesQuery.SalesQuery.ID.Value != null)
                    {
                        Console.WriteLine("SALE-L01-00-0400 | TODO | Check format of SalesQuery.SalesQuery.ID.Value to meet UUID");
                        //SALE-L01-00-0400
                        //Sales Query/Identification
                        //Check Format : UUID
                        //TODO: Check format of SalesQuery.SalesQuery.ID.Value to meet UUID

                        Console.WriteLine("SALE-L03-00-0400 | TODO | Check if SalesQuery.SalesQuery.ID.Value is unique");
                        //SALE-L03-00-0400
                        //Sales Query/Identification
                        //The reference must be unique
                        //TODO: Check if SalesQuery.SalesQuery.ID.Value is unique
                    }
                    else
                    {
                        Console.WriteLine("No SalesQuery.SalesQuery.ID.schemeID provided or != UUID or ID has no Value");
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0400 | ERROR | No SalesQuery.SalesQuery provided");
                    //SALE-L00-00-0400 - error
                }
                #endregion SalesQuery.SalesQuery.ID

                //SALE-L00-00-0401, SALE-L01-00-0401
                #region SalesQuery.SalesQuery.SubmittedDateTime
                //SALE-L00-00-0401
                //Sales Query/Submitted
                //Must be present
                if (SalesQuery.SalesQuery.SubmittedDateTime != null)
                {
                    Console.WriteLine("SALE-L00-00-0401 | OK | SalesQuery.SalesQuery.SubmittedDateTime provided");

                    //SALE-L01-00-0401
                    //Sales Query/Submitted
                    //Check format
                    if (SalesQuery.SalesQuery.SubmittedDateTime.Item is DateTime)
                    {
                        Console.WriteLine("SALE-L01-00-0401 | OK | SalesQuery.SalesQuery.SubmittedDateTime.Item is DateTime");
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0401 | ERROR | SalesQuery.SalesQuery.SubmittedDateTime.Item is DateTime");
                        //SALE-L00-00-0401 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0401 | ERROR | No SalesQuery.SalesQuery.SubmittedDateTime provided");
                    //SALE-L00-00-0400 - error
                }
                #endregion SalesQuery.SalesQuery.SubmittedDateTime

                //SALE-L00-00-0402, SALE-L01-00-0403, SALE-L01-00-0402
                #region SalesQuery.SalesQuery.TypeCode
                //SALE-L00-00-0402
                //Sales Query/Type
                //Must be present
                if (SalesQuery.SalesQuery.TypeCode?.Value != null)
                {
                    Console.WriteLine("SALE-L00-00-0402 | OK | SalesQuery.SalesQuery.TypeCode provided with Value");

                    //SALE-L01-00-0403
                    //Sales Query/Type
                    //The listID must be FLUX_SALES_ TYPE
                    if (SalesQuery.SalesQuery.TypeCode.listID?.ToString() == "FLUX_SALES_TYPE")
                    {
                        Console.WriteLine("SALE-L01-00-0403 | OK | SalesQuery.SalesQuery.TypeCode.listID provided and == FLUX_SALES_TYPE");

                        Console.WriteLine("SALE-L01-00-0402 | TODO | Check if SalesQuery.SalesQuery.TypeCode.Value from FLUX_SALES_TYPE list");
                        //SALE-L01-00-0402
                        //Sales Query/Type
                        //Check code from the list listID
                        //TODO: Check if SalesQuery.SalesQuery.TypeCode.Value from FLUX_SALES_TYPE list

                        valueSalesQueryTypeCode = SalesQuery.SalesQuery.TypeCode.ToString();
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0403 | ERROR | No SalesQuery.SalesQuery.TypeCode.listID provided or != FLUX_SALES_TYPE");
                        //SALE-L01-00-0403 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0402 | ERROR | No SalesQuery.SalesQuery.TypeCode provided or with no Value");
                    //SALE-L00-00-0402 - error
                }
                #endregion SalesQuery.SalesQuery.TypeCode

                //SALE-L02-00-0401, SALE-L02-00-0402, SALE-L00-00-0410, SALE-L01-00-0411, SALE-L01-00-0410, SALE-L03-00-0410 
                #region SalesQuery.SalesQuery.SubmitterFLUXParty
                //SALE-L02-00-0401
                //Flux_Party
                //Must be present
                if (SalesQuery.SalesQuery.SubmitterFLUXParty != null)
                {
                    Console.WriteLine("SALE-L02-00-0401 | OK | SalesQuery.SalesQuery.SubmitterFLUXParty provided");

                    //SALE-L02-00-0402
                    //Flux_Party
                    //Only one occurrence
                    //#Q How [only one occurance] should be cheched, is this workable?
                    if (!SalesQuery.SalesQuery.SubmitterFLUXParty.GetType().IsArray)
                    {
                        Console.WriteLine("SALE-L02-00-0402 | OK | SalesQuery.SalesQuery.SubmitterFLUXParty is not an Array = 1 occurance");

                        if (SalesQuery.SalesQuery.SubmitterFLUXParty.ID != null)
                        {
                            //SALE-L00-00-0410
                            //FLUX_Party/Identification
                            //Must be present
                            //#Q As cardinality is up to one, take and validate the first element only
                            var submitterFLUXPartyID = SalesQuery.SalesQuery.SubmitterFLUXParty.ID.First();

                            if (submitterFLUXPartyID.Value != null)
                            {
                                Console.WriteLine("SALE-L00-00-0410 | OK | SalesQuery.SalesQuery.SubmitterFLUXParty.ID provided with Value");

                                Console.WriteLine("SALE-L01-00-0410 | TODO | Check if SalesQuery.SalesQuery.SubmitterFLUXParty.ID.Value from FLUX_GP_PARTY list");
                                //SALE-L01-00-0410
                                //FLUX_Party/Identification
                                //Check Code from the list schemeID
                                //TODO: Chech if SalesQuery.SalesQuery.SubmitterFLUXParty.ID.Value from FLUX_GP_PARTY list

                                valueFLUXPartyID = submitterFLUXPartyID.Value.ToString();

                                //SALE-L01-00-0411
                                //#Q missing in FLUX_P1000-5_Sales_domain_EU_Implementation
                                //FLUX_Party/Identification
                                //Check attribute schemeID. Must be FLUX_GP_PARTY
                                if (submitterFLUXPartyID.schemeID?.ToString() == "FLUX_GP_PARTY")
                                {
                                    Console.WriteLine("SALE-L01-00-0411 | OK | SalesQuery.SalesQuery.SubmitterFLUXParty.ID.schemaID provided and == FLUX_GP_PARTY");
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L01-00-0411 | ERROR | No SalesQuery.SalesQuery.SubmitterFLUXParty.ID.schemaID provided or != FLUX_GP_PARTY");
                                    //SALE-L01-00-0411 - error
                                }

                                //SALE-L03-00-0410 
                                //#Q missing in FLUX_P1000-5_Sales_domain_EU_Implementation
                                //FLUX_Party/Identification
                                //Check if OwnerFLUXParty.ID is consistent with FLUX TL values.
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0410 | ERROR | No SalesQuery.SalesQuery.SubmitterFLUXParty.ID provided or with no Value");
                                //SALE-L00-00-0410 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("No SalesQuery.SalesQuery.SubmitterFLUXParty.ID provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L02-00-0402 | ERROR | No SalesQuery.SalesQuery.SubmitterFLUXParty is an Array > 1 occurances");
                        //SALE-L02-00-0402 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L02-00-0401 | ERROR | No SalesQuery.SalesQuery.SubmitterFLUXParty provided");
                    //SALE-L02-00-0401 - error
                }
                #endregion SalesQuery.SalesQuery.SubmitterFLUXParty

                //SALE-L02-00-0403, SALE-L00-00-0420, SALE-L01-00-0420, SALE-L00-00-0421, SALE-L01-00-0421, SALE-L03-00-0420, SALE-L02-00-0420
                #region SalesQuery.SalesQuery.SpecifiedDelimitedPeriod
                //SALE-L02-00-0403
                //Delimired_Period
                //Only one occurrence
                //#Q How [only one occurance] should be cheched, is this workable?
                if (SalesQuery.SalesQuery.SpecifiedDelimitedPeriod != null && !SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.GetType().IsArray)
                {
                    Console.WriteLine("SALE-L02-00-0403 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod provided and is not an Array = 1 occurance");

                    //SALE-L00-00-0420
                    //Delimited Period/Start
                    //Must be present
                    if (SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime != null)
                    {
                        Console.WriteLine("SALE-L00-00-0420 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime provided");

                        //SALE-L01-00-0420
                        //Delimited Period/Start
                        //Check format
                        if (SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item is DateTime)
                        {
                            Console.WriteLine("SALE-L01-00-0420 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item is DateTime");

                            dateTimeDelimitedPeriodStartValue = SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item;
                            DateTime dateTimeUtcNowSystemDate = DateTime.UtcNow;

                            //SALE-L03-00-0420
                            //Delimited Period/Start
                            //StartDate >= SystemDate – 3 years
                            //Past data limited to three years from the current date
                            if (DateTime.Compare(dateTimeDelimitedPeriodStartValue, dateTimeUtcNowSystemDate.AddYears(-3)) >= 0)
                            {
                                Console.WriteLine("SALE-L03-00-0420 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item is after dateTimeUtcNowSystemDate - 3 years");
                            }
                            else
                            {
                                Console.WriteLine("SALE-L03-00-0420 | ERROR | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item is before dateTimeUtcNowSystemDate - 3 years");
                                //SALE-L03-00-0420 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0420 | ERROR | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime.Item is not DateTime");
                            //SALE-L01-00-0420 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0420 | ERROR | No SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime provided");
                        //SALE-L00-00-0420 - error
                    }

                    //SALE-L00-00-0421
                    //Delimited Period/End
                    //Must be present
                    if (SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime != null)
                    {
                        Console.WriteLine("SALE-L00-00-0421 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime provided");

                        //SALE-L01-00-0421
                        //Delimited Period/End
                        //Check format
                        if (SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime.Item is DateTime)
                        {
                            Console.WriteLine("SALE-L01-00-0421 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime is DateTime");

                            dateTimeDelimitedPeriodEndValue = SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime.Item;
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0421 | ERROR | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime is not DateTime");
                            //SALE-L01-00-0421 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0421 | ERROR | No SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime provided");
                        //SALE-L00-00-0421 - error
                    }

                    if (dateTimeDelimitedPeriodStartValue != DateTime.MaxValue && dateTimeDelimitedPeriodEndValue != DateTime.MinValue)
                    {
                        //SALE-L02-00-0420
                        //Delimited Period/Start, Delimited Period / End
                        //Start <= End
                        if (DateTime.Compare(dateTimeDelimitedPeriodStartValue, dateTimeDelimitedPeriodEndValue) <= 0)
                        {
                            Console.WriteLine("SALE-L02-00-0420 | OK | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime <= SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L02-00-0420 | ERROR | SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime > SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime");
                            //SALE-L02-00-0420 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("No SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.StartDateTime or SalesQuery.SalesQuery.SpecifiedDelimitedPeriod.EndDateTime provided or not properly set");
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L02-00-0403 | ERROR | No SalesQuery.SalesQuery.SpecifiedDelimitedPeriod or is an Array > 1 occurance");
                    //SALE-L02-00-0403 - error
                }
                #endregion SalesQuery.SalesQuery.SpecifiedDelimitedPeriod

                //SALE-L00-00-0404, SALE-L00-00-0430, SALE-L01-00-0430, SALE-L01-00-0431, SALE-L02-00-0430, SALE-L01-00-0432, SALE-L00-00-0431
                #region SalesQuery.SalesQuery.SimpleSalesQueryParameter
                //SALE-L00-00-0404
                //Sales_Query parameter
                //Must be present
                if (SalesQuery.SalesQuery.SimpleSalesQueryParameter != null)
                {
                    Console.WriteLine("SALE-L00-00-0404 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter provided");

                    //#Q As cardinality is up to *, validate each element
                    foreach (var salesQuerySimpleSalesQueryParameter in SalesQuery.SalesQuery.SimpleSalesQueryParameter)
                    {
                        //SALE-L00-00-0430
                        //Sales Query Parameter/Type
                        //Must be present
                        if (salesQuerySimpleSalesQueryParameter.TypeCode?.Value != null)
                        {
                            Console.WriteLine("SALE-L00-00-0430 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.TypeCode provided with Value");

                            Console.WriteLine("SALE-L01-00-0430 | TODO | Check if SalesQuery.SalesQuery.SimpleSalesQueryParameter.TypeCode.Value from FLUX_SALES_QUERY_PARAM list");
                            //SALE-L01-00-0430
                            //Sales Query Parameter/Type
                            //Check code from the list listID
                            //TODO: Check if salesQuerySimpleSalesQueryParameter.TypeCode.Value from FLUX_SALES_QUERY_PARAM list

                            if (salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "FLAG" || salesQuerySimpleSalesQueryParameter.TypeCode.Value?.ToString() == "ROLE" || salesQuerySimpleSalesQueryParameter.TypeCode.Value?.ToString() == "PLACE")
                            {
                                if (salesQuerySimpleSalesQueryParameter.ValueCode?.Value != null && salesQuerySimpleSalesQueryParameter.ValueCode?.listID?.ToString() == "FLUX_SALES_QUERY_ROLE")
                                {
                                    Console.WriteLine("SALE-L01-00-0431 | TODO | Check code SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value");
                                    //SALE-L01-00-0431
                                    //Sales Query Parameter/Type, Sales Query Parameter/Value
                                    //If Type = FLAG/ROLE/PLACE, check code of the value
                                    //TODO: Check code salesQuerySimpleSalesQueryParameter.ValueCode.Value
                                }
                                else
                                {
                                    Console.WriteLine("No SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value or listID provided or listID != FLUX_SALES_QUERY_ROLE");
                                }

                                if (salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "FLAG")
                                {
                                    //SALE-L02-00-0430
                                    //Sales Query Parameter/Type, FLUX_Party
                                    //Type=FLAG only for international organization
                                    //If the type is present but there is no value, the query is for all flag states
                                    if (salesQuerySimpleSalesQueryParameter.ValueCode?.Value?.ToString() != null)
                                    {
                                        Console.WriteLine("SALE-L02-00-0430 | TODO | Flag state provided, Check if international organization");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0430 | NOTE | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.listID == FLUX_SALES_QUERY_ROLE, but no Value => Query is for all flag states");
                                    }
                                }

                                if (salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "ROLE")
                                {
                                    //SALE-L02-00-0431
                                    //Sales Query Parameter/Type, Sales Query Parameter / Values, FLUX_Party / Identification
                                    //Type=ROLE, the values FLAG or LAND only for MS. INT for international organization
                                    //International organizations are identified by an iso-3 code starting with 'X'

                                    if (valueFLUXPartyID != "" && valueFLUXPartyID.StartsWith("X"))  //#Q means it is international organization
                                    {
                                        if (salesQuerySimpleSalesQueryParameter.ValueCode?.Value?.ToString() == "INT")
                                        {
                                            Console.WriteLine("SALE-L02-00-0431 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value == INT provided for internationl organization");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0431 | ERROR | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value != INT provided for internationl organization");
                                            //SALE-L02-00-0431 - error
                                        }
                                    }
                                    else  //#Q for MS 
                                    {
                                        if (salesQuerySimpleSalesQueryParameter.ValueCode?.Value?.ToString() == "FLAG" || salesQuerySimpleSalesQueryParameter.ValueCode?.Value?.ToString() == "LAND")
                                        {
                                            Console.WriteLine("SALE-L02-00-0431 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value == FLAG || LAND provided for MS");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0431 | ERROR | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueCode.Value != FLAG || LAND provided for MS");
                                            //SALE-L02-00-0431 - error
                                        }
                                    }
                                }
                            }
                            else if (salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "VESSEL")
                            {
                                if (salesQuerySimpleSalesQueryParameter.ValueID?.schemeID?.ToString() == "CFR")
                                {
                                    //SALE-L01-00-0432
                                    //Sales Query Parameter/Type, Sales Query Parameter/Value
                                    //If Type=VESSEL, check format of CFR
                                    //If the type is present but there is no value for CFR, the query is for all fishing vessels
                                    if (salesQuerySimpleSalesQueryParameter.ValueID.Value?.ToString() != null)
                                    {
                                        //#Q Regex used for start of string, 3 capital letters, followed by 9 numbers from 0 to 9 and end of string
                                        if (Regex.Match(salesQuerySimpleSalesQueryParameter.ValueID.Value.ToString(), "^[A-Z]{3}[0-9]{9}$").Success)
                                        {
                                            Console.WriteLine("SALE-L01-00-0432 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value is a valid CFR");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L01-00-0432 | ERROR | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value is not a valid CFR");
                                            //SALE-L01-00-0432 - error
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L01-00-0432 | NOTE | No SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.schemeID == CFR, but no Value => Query is for all fishing vessels");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value or schemeID provided or schemeID != CFR");
                                }

                                //SALE-L03-00-0430 - error
                                //Sales Query Parameter/Type, Sales Query Parameter / Value, FLUX Party/ Identification, Delimited Period/ Start
                                //If TYPE=VESSEL and if vessel identifiers are given, the flag of each vessel must be the country of the requester at start date
                                //TODO: Check if the flag of each vessel is the country of the requester at start date
                                if (salesQuerySimpleSalesQueryParameter.ValueID?.Value?.ToString() != null)
                                {
                                    //type of query param (if VESSEL):  salesQuerySimpleSalesQueryParameter.TypeCode.Value
                                    //id of the vessel:                 salesQuerySimpleSalesQueryParameter.ValueID.Value
                                    //start date:                       dateTimeDelimitedPeriodStartValue
                                    //country of the requester:         valueFLUXPartyID

                                    Console.WriteLine("SALE-L03-00-0430 | TODO | Check if the flag of each vessel is the country of the requester at start date");
                                }
                                else
                                {
                                    Console.WriteLine("No SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value provided for TypeCode == VESSEL");
                                }
                            }
                            else if (salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "SALES_ID" || salesQuerySimpleSalesQueryParameter.TypeCode.Value.ToString() == "TRIP_ID")
                            {
                                var valueSalesQueryParameterValueID = salesQuerySimpleSalesQueryParameter.ValueID?.Value?.ToString() != null ? salesQuerySimpleSalesQueryParameter.ValueID?.Value?.ToString() : "";

                                if (valueSalesQueryParameterValueID != "")
                                {
                                    string[] salesQueryParameterValueIDSubstrings = valueSalesQueryParameterValueID.Trim().Split("-");
                                    string salesQueryParameterValueIDNationalNumber = String.Join("-", valueSalesQueryParameterValueID.Trim().Split("-").Skip(2));

                                    Console.WriteLine("SALE-L01-00-0433 | TODO | Add SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value checks for format of the part corresponding to the national number - salesQueryParameterValueIDNationalNumber");
                                    //SALE-L01-00-0433
                                    //Sales Query Parameter/Type, Sales Query Parameter / Values, FLUX_Party / Identification
                                    //If Type = SALES_ID or TRIP_ID, check format of the part corresponding to the national number
                                    //country specific The requester knows his national format(s). As the sender must send validated data,
                                    //this BR can be used to control the national formats if he knows it otherwise it is to verify if that part has max 20 chars.
                                    if (salesQueryParameterValueIDNationalNumber.Length > 0 && salesQueryParameterValueIDNationalNumber.Length <= 20)
                                    {
                                        Console.WriteLine("SALE-L01-00-0433 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value salesQueryParameterValueIDNationalNumber provided and length <= 20");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L01-00-0433 | ERROR | No  SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value salesQueryParameterValueIDNationalNumber provided or length > 20");
                                        //SALE-L01-00-0433 - error
                                    }

                                    //SALE-L01-00-0434
                                    //Sales Query Parameter/Type, Sales Query Parameter / Values
                                    //If Type = SALES_ID or TRIP_ID, check format of the common part
                                    //ISO-3 code + '-' + type of report
                                    //TODO: Check if salesQueryParameterValueIDSubstrings[0] is an ISO-3 code
                                    Console.WriteLine("SALE-L01-00-0434 | TODO | Check if SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value salesQueryParameterValueIDSubstrings[0] is valid ISO-3 code");
                                    bool isFirstSubstringIso3Code = true;
                                    if (isFirstSubstringIso3Code && valueSalesQueryTypeCode != "" && (salesQueryParameterValueIDSubstrings[1] == valueSalesQueryTypeCode))
                                    {
                                        Console.WriteLine("SALE-L01-00-0434 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value common part format valid");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L01-00-0434 | ERROR | SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID.Value common part format not valid");
                                        //SALE-L01-00-0434 - error
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No SalesQuery.SalesQuery.SimpleSalesQueryParameter.ValueID with Value provided for TypeCode == SALES_ID || TRIP_ID");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("SALE-L00-00-0430 | ERROR | No SalesQuery.SalesQuery.SimpleSalesQueryParameter.TypeCode provided or with no Value");
                            //SALE-L00-00-0430 - error
                        }
                    }

                    //SALE-L00-00-0431
                    //Sales Query Parameter/Type, Sales Query Parameter/Value
                    //A role must exist
                    if (SalesQuery.SalesQuery.SimpleSalesQueryParameter.Any(a => a.TypeCode?.Value?.ToString() == "ROLE"))
                    {
                        Console.WriteLine("SALE-L00-00-0431 | OK | SalesQuery.SalesQuery.SimpleSalesQueryParameter.TypeCode provided with Value == ROLE");
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0431 | ERROR | No SalesQuery.SalesQuery.SimpleSalesQueryParameter.TypeCode provided with Value == ROLE");
                        //SALE-L00-00-0431 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0404 | ERROR | No SalesQuery.SalesQuery.SimpleSalesQueryParameter provided");
                    //SALE-L00-00-0404 - error
                }
                #endregion SalesQuery.SalesQuery.SimpleSalesQueryParameter
            }
            else
            {
                Console.WriteLine("No SalesQuery.SalesQuery provided");
            }
        }
    }
}
