using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ScortelApi.Models.FLUX;
using System.Text.RegularExpressions;

namespace Validation.FluxDomainsValidation.FluxACDRDomainValidation
{
    class AcdrReportValidation
    {
        public void AcdrReportValidate(FLUXACDRMessageType AcdrReport)
        {
            #region Variables
            string valuePurposeCode = "";
            string valueReportTypeCode = "";
            string valueReportRegionalSpeciesCode = "";
            string valueReportRegionalAreaCode = "";
            string valueReportFishingCategoryCode = "";
            string valueReportFAOFishingGearCode = "";
            string valueReportTransportMeansId = "";
            string valueReportTransportMeansRegistrationVesselCountryId = "";
            string valueReportFAOIdentificationCode = "";
            string valueReportSovereigntyWaterCode = "";
            string valueReportLandingCountryCode = "";
            string valueReportCatchStatusCode = "";
            string valueReportCatchFAOSpeciesCode = "";
            string valueReportCatchUnitQuantity = "";
            string valueReportCatchWeightMeasure = "";
            string valueReportCatchUsageCode = "";

            DateTime dateTimeDelimitedPeriodStart = new DateTime();
            #endregion Variables

            #region AcdrReport.FLUXReportDocument
            if (AcdrReport.FLUXReportDocument != null)
            {
                #region AcdrReport.FLUXReportDocument.ID
                if (AcdrReport.FLUXReportDocument.ID != null)
                {
                    //#Q As cardinality is up to 1, take and validate the first element only
                    var reportDocumentId = AcdrReport.FLUXReportDocument.ID.First();

                    if (reportDocumentId.Value?.ToString() != "" && reportDocumentId.schemeID?.ToString() == "UUID")
                    {
                        Console.WriteLine("ACDR-L00-00-0011 | TODO | Check if ID UUID valid");
                        //ACDR-L00-00-0011 - error
                        //FLUX Report Document / Identification 
                        //The report identifier must be a valid UUID format 
                        //TODO: Check if ID UUID valid

                        Console.WriteLine("ACDR-L00-00-0012 | TODO | Check if ID UUID unique");
                        //ACDR-L00-00-0012 - error
                        //FLUX Report Document / Identification 
                        //The UUID is unique (he does not reference a report already received) 
                        //TODO: Check if ID UUID unique
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXReportDocument.ID.Value or schemeID provided or schemeId != UUID");
                    }
                }
                else
                {
                    Console.WriteLine("No AcdrReport.FLUXReportDocument.ID provided");
                }
                #endregion AcdrReport.FLUXReportDocument.ID

                #region AcdrReport.FLUXReportDocument.PurposeCode
                //ACDR-L00-00-0018 
                //FLUX Report Document / Purpose Code 
                //The purpose code is mandatory 
                if (AcdrReport.FLUXReportDocument.PurposeCode != null)
                {
                    Console.WriteLine("ACDR-L00-00-0018 | OK | AcdrReport.FLUXReportDocument.PurposeCode provided");

                    //ACDR-L00-00-0015 
                    //FLUX Report Document / Purpose Code 
                    //The listID must be FLUX_GP_PURPOSE 
                    if (AcdrReport.FLUXReportDocument.PurposeCode.listID?.ToString() == "FLUX_GP_PURPOSE")
                    {
                        Console.WriteLine("ACDR-L00-00-0015 | OK | AcdrReport.FLUXReportDocument.PurposeCode.listID provided and == FLUX_GP_PURPOSE");

                        //ACDR-L00-00-0019 
                        //FLUX Report Document / Purpose Code 
                        //Only value '9' and ‘5’ are valid 
                        if (AcdrReport.FLUXReportDocument.PurposeCode.Value?.ToString() == "5" || AcdrReport.FLUXReportDocument.PurposeCode.Value?.ToString() == "9")
                        {
                            Console.WriteLine("ACDR-L00-00-0019 | OK | AcdrReport.FLUXReportDocument.PurposeCode.Value provided and == 5 || 9");

                            valuePurposeCode = AcdrReport.FLUXReportDocument.PurposeCode.Value.ToString();
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L00-00-0019 | ERROR | No AcdrReport.FLUXReportDocument.PurposeCode.Value provided or != 5 || 9");
                            //ACDR-L00-00-0019 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("ACDR-L00-00-0015 | ERROR | No AcdrReport.FLUXReportDocument.PurposeCode.listID provided or != FLUX_GP_PURPOSE");
                        //ACDR-L00-00-0015 - error
                    }
                }
                else
                {
                    Console.WriteLine("ACDR-L00-00-0018 | ERROR | No AcdrReport.FLUXReportDocument.PurposeCode provided");
                    //ACDR-L00-00-0018 - error
                }
                #endregion AcdrReport.FLUXReportDocument.PurposeCode

                #region AcdrReport.FLUXReportDocument.ReferencedID
                if (AcdrReport.FLUXReportDocument.ReferencedID != null)
                {
                    if (AcdrReport.FLUXReportDocument.ReferencedID.Value?.ToString() != "" && AcdrReport.FLUXReportDocument.ReferencedID.schemeID?.ToString() == "UUID")
                    {
                        Console.WriteLine("ACDR-L00-00-0013 | TODO | Check if ReferencedID UUID valid");
                        //ACDR-L00-00-0013 - error
                        //FLUX Report Document / Referenced ID 
                        //The report identifier must be a valid UUID format 
                        //TODO: Check if ReferencedID UUID valid

                        Console.WriteLine("ACDR-L00-00-0014 | TODO | Check if ReferencedID UUID unique");
                        //ACDR-L00-00-0014  - error
                        //FLUX Report Document / Referenced ID 
                        //Referenced message must exist for reports with Action type='5' (correction)  
                        //#Q Is "Action type" == PusposeCode?
                        //TODO: Check if ReferencedID UUID unique
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXReportDocument.ReferencedID.Value or schemeID provided or schemeId != UUID");
                    }
                }
                else
                {
                    Console.WriteLine("No AcdrReport.FLUXReportDocument.ReferencedID provided");
                }
                #endregion AcdrReport.FLUXReportDocument.ReferencedID

                #region AcdrReport.FLUXReportDocument.CreationDateTime
                //ACDR-L00-00-0017
                //FLUX Report Document / Creation 
                //Datetime format 
                if (AcdrReport.FLUXReportDocument.CreationDateTime?.Item != null)
                {
                    Console.WriteLine("ACDR-L00-00-0017 | OK | AcdrReport.FLUXReportDocument.CreationDateTime format valid");
                }
                else
                {
                    Console.WriteLine("ACDR-L00-00-0017 | ERROR | AcdrReport.FLUXReportDocument.CreationDateTime format not valid");
                    //ACDR-L00-00-0017 - error
                }
                #endregion AcdrReport.FLUXReportDocument.CreationDateTime
            }
            else
            {
                Console.WriteLine("No AcdrReport.FLUXReportDocument provided");
            }
            #endregion AcdrReport.FLUXReportDocument

            #region AcdrReport.AggregatedCatchReportDocument
            if (AcdrReport.AggregatedCatchReportDocument != null)
            {
                #region AcdrReport.FLUXReportDocument.CreationDateTime
                if (AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod != null)
                {
                    #region AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime
                    //ACDR-L00-00-0020 
                    //Effective Delimited Period / Start Date Time 
                    //The Start Date Time must be present 
                    if (AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime != null)
                    {
                        Console.WriteLine("ACDR-L00-00-0020 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime provided");

                        //ACDR-L00-00-0022 
                        //Effective Delimited Period / Start Date Time 
                        //Datetime format 
                        if (AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is DateTime)
                        {
                            Console.WriteLine("ACDR-L00-00-0022 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is DateTime");
                            dateTimeDelimitedPeriodStart = AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item;

                            DateTime dateTimeUtcNow = DateTime.UtcNow;
                            //ACDR-L00-00-0027 
                            //Effective Delimited Period / Start Date Time, System date time
                            //Check if the reporting period is not in the future (compared to the system date) 
                            if (DateTime.Compare(AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item, dateTimeUtcNow) < 0)
                            {
                                Console.WriteLine("ACDR-L00-00-0027 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is not in the future");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L00-00-0027 | ERROR | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is in the future");
                                //ACDR-L00-00-0027 - error
                            }

                            //ACDR-L00-00-0028 
                            //Effective Delimited Period / Start Date Time 
                            //Report Start date must be >= 2017 (parameter) 
                            if (DateTime.Compare(AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item, new DateTime(2017, 1, 1)) >= 0)
                            {
                                Console.WriteLine("ACDR-L00-00-0028 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is not before 2017");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L00-00-0028 | ERROR | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is before 2017");
                                //ACDR-L00-00-0028 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L00-00-0022 | ERROR | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime.Item is not DateTime");
                            //ACDR-L00-00-0022 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("ACDR-L00-00-0020 | ERROR | No AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime provided");
                        //ACDR-L00-00-0020 - error
                    }
                    #endregion AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.StartDateTime

                    #region AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime
                    //ACDR-L00-00-0021 
                    //Effective Delimited Period / End Date Time 
                    //The End Date Time must be present 
                    if (AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime != null)
                    {
                        Console.WriteLine("ACDR-L00-00-0021 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime provided");

                        //ACDR-L00-00-0023 
                        //Effective Delimited Period / End Date Time 
                        //Datetime format 
                        if (AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime.Item is DateTime)
                        {
                            Console.WriteLine("ACDR-L00-00-0023 | OK | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime is DateTime");

                            //ACDR-L00-00-0024 
                            //Effective Delimited Period / Start Date Time, Effective Delimited Period / End Date Time
                            //Start Date cannot be later than End Date 
                            if (DateTime.Compare(dateTimeDelimitedPeriodStart, AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime.Item) <= 0)
                            {
                                Console.WriteLine("ACDR-L00-00-0024 | OK | StartDateTime is not after AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L00-00-0024 | ERROR | StartDateTime is after AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime");
                                //ACDR-L00-00-0024 - error
                            }

                            //ACDR-L00-00-0025 
                            //Effective Delimited Period / Start Date Time, Effective Delimited Period / End Date Time
                            //Start Date must be in the same month and year as End Date 
                            if (dateTimeDelimitedPeriodStart.Year == AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime.Item.Year && dateTimeDelimitedPeriodStart.Month == AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime.Item.Month)
                            {
                                Console.WriteLine("ACDR-L00-00-0025 | OK | StartDateTime Year and Month are equal to AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime Year and Month");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L00-00-0025 | ERROR | StartDateTime Year and Month are different from AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime Year and Month");
                                //ACDR-L00-00-0025 - error
                            }
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L00-00-0023 | ERROR | AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime is not DateTime");
                            //ACDR-L00-00-0023 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("ACDR-L00-00-0021 | ERROR | No AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime provided");
                        //ACDR-L00-00-0021 - error
                    }
                    #endregion AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod.EndDateTime
                }
                else
                {
                    Console.WriteLine("No AcdrReport.AggregatedCatchReportDocument.EffectiveDelimitedPeriod provided");
                }
                #endregion AcdrReport.FLUXReportDocument.CreationDateTime

                #region AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty
                if (AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty != null)
                {
                    if (AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID != null) {

                        //#Q As cardinality is up to 1, take and validate the first element only
                        var ownerFluxPartyId = AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID.First();

                        //ACDR-L00-00-0041 
                        //Owner FLUX Party / ID 
                        //The FLUX Party ID must be provided 
                        if (ownerFluxPartyId.Value != null)
                        {
                            Console.WriteLine("ACDR-L00-00-0041 | OK | AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID.Value provided");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L00-00-0041 | ERROR | No AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID.Value provided");
                            //ACDR-L00-00-0041 
                        }

                        //ACDR-L00-00-0040 
                        //Owner FLUX Party / ID 
                        //The schemeID must be ECR_RPT_CTRY 
                        if (ownerFluxPartyId.schemeID?.ToString() == "ECR_RPT_CTRY")
                        {
                            Console.WriteLine("ACDR-L00-00-0040 | OK | AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID.schemeID provided and == ECR_RPT_CTRY");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L00-00-0040 | ERROR | No AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID.schemeID provided or != ECR_RPT_CTRY");
                            //ACDR-L00-00-0040 
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty.ID provided");
                    }
                }
                else
                {
                    Console.WriteLine("No AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty provided");
                }
                #endregion AcdrReport.AggregatedCatchReportDocument.OwnerFLUXParty
            }
            else
            {
                Console.WriteLine("No AcdrReport.AggregatedCatchReportDocument provided");
            }
            #endregion AcdrReport.AggregatedCatchReportDocument

            #region AcdrReport.FLUXACDRReport
            if (AcdrReport.FLUXACDRReport != null)
            {
                foreach (var fluxAcdrReport in AcdrReport.FLUXACDRReport)
                {
                    #region AcdrReport.FLUXACDRReport.TypeCode
                    //ACDR-L02-00-0001 - Check mandatory data elements for specified report type - error
                    //ACDR-L01-00-0011 
                    //FLUX ACDR Report / Type Code 
                    //Report Type code is mandatory 
                    if (fluxAcdrReport.TypeCode != null)
                    {
                        Console.WriteLine("ACDR-L01-00-0011 | OK | AcdrReport.FLUXACDRReport.TypeCode provided");

                        //ACDR-L01-00-0010 
                        //FLUX ACDR Report / Type Code 
                        //The listID must be ECR_ACDR_RPT__TYPE 
                        //#Q Should it really be with two underscores?
                        if (fluxAcdrReport.TypeCode.listID?.ToString() == "ECR_ACDR_RPT__TYPE")
                        {
                            Console.WriteLine("ACDR-L01-00-0010 | OK | AcdrReport.FLUXACDRReport.TypeCode.listID provided and == ECR_ACDR_RPT__TYPE");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L01-00-0010 | ERROR | No AcdrReport.FLUXACDRReport.TypeCode provided or != ECR_ACDR_RPT__TYPE");
                            //ACDR-L01-00-0010 - error
                        }

                        if (fluxAcdrReport.TypeCode.Value?.ToString() != null)
                        {
                            valueReportTypeCode = fluxAcdrReport.TypeCode.Value.ToString();

                            Console.WriteLine("ACDR-L01-00-0012 | TODO | Check if AcdrReport.FLUXACDRReport.TypeCode.Value is in relevant List");
                            //ACDR-L01-00-0012 - error
                            //FLUX ACDR Report / Type Code 
                            //Report Type code must be in relevant List 
                            //TODO: Check if AcdrReport.FLUXACDRReport.TypeCode.Value is in relevant List
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.TypeCode.Value provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ACDR-L01-00-0011 | ERROR | No AcdrReport.FLUXACDRReport.TypeCode provided");
                        //ACDR-L01-00-0011 - error
                     
                        Console.WriteLine("ACDR-L02-00-0001 | ERROR | Mandatory AcdrReport.FLUXACDRReport.TypeCode not provided");
                        //ACDR-L02-00-0001 - Check mandatory data elements for specified report type - error
                    }
                    #endregion AcdrReport.FLUXACDRReport.TypeCode

                    #region AcdrReport.FLUXACDRReport.RegionalSpeciesCode
                    if (fluxAcdrReport.RegionalSpeciesCode != null)
                    {
                        //ACDR-L01-00-0013 
                        //FLUX ACDR Report / Regional Species Code 
                        //The listID must be QUOTA_OBJECT 
                        if (fluxAcdrReport.RegionalSpeciesCode.listID?.ToString() == "QUOTA_OBJECT")
                        {
                            Console.WriteLine("ACDR-L01-00-0013 | OK | AcdrReport.FLUXACDRReport.RegionalSpeciesCode.listID provided and == QUOTA_OBJECT");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L01-00-0013 | ERROR | No AcdrReport.FLUXACDRReport.RegionalSpeciesCode.listID provided or != QUOTA_OBJECT");
                            //ACDR-L01-00-0013 - error
                        }

                        if (fluxAcdrReport.RegionalSpeciesCode.Value != null)
                        {
                            valueReportRegionalSpeciesCode = fluxAcdrReport.RegionalSpeciesCode.Value.ToString();

                            Console.WriteLine("ACDR-L01-00-0014 | TODO | Check if AcdrReport.FLUXACDRReport.RegionalSpeciesCode.Value is in relevant List");
                            //ACDR-L01-00-0014 - error
                            //FLUX ACDR Report / Regional Species Code 
                            //Regional Species code must be in relevant List 
                            //TODO: Check if AcdrReport.FLUXACDRReport.RegionalSpeciesCode.Value is in relevant List
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.RegionalSpeciesCode.Value provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.RegionalSpeciesCode provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.RegionalSpeciesCode

                    #region AcdrReport.FLUXACDRReport.RegionalAreaCode
                    if (fluxAcdrReport.RegionalAreaCode != null)
                    {
                        //ACDR-L01-00-0015 
                        //FLUX ACDR Report / Regional Area Code  
                        //The listID must be QUOTA_LOCATION  
                        if (fluxAcdrReport.RegionalAreaCode.listID?.ToString() == "QUOTA_LOCATION")
                        {
                            Console.WriteLine("ACDR-L01-00-0015 | OK | AcdrReport.FLUXACDRReport.RegionalAreaCode.listID provided and == QUOTA_LOCATION");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L01-00-0015 | ERROR | No AcdrReport.FLUXACDRReport.RegionalAreaCode.listID provided or != QUOTA_LOCATION");
                            //ACDR-L01-00-0015 - error
                        }

                        if (fluxAcdrReport.RegionalAreaCode.Value != null)
                        {
                            valueReportRegionalAreaCode = fluxAcdrReport.RegionalAreaCode.Value.ToString();

                            Console.WriteLine("ACDR-L01-00-0016 | TODO | Check if AcdrReport.FLUXACDRReport.RegionalAreaCode.Value is in relevant List");
                            //ACDR-L01-00-0016 - error
                            //FLUX ACDR Report / Regional Area Code  
                            //Regional Area code must be in relevant List 
                            //TODO: Check if AcdrReport.FLUXACDRReport.RegionalAreaCode.Value is in relevant List
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.RegionalAreaCode.Value provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.RegionalAreaCode provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.RegionalAreaCode

                    #region AcdrReport.FLUXACDRReport.FishingCategoryCode
                    if (fluxAcdrReport.FishingCategoryCode != null)
                    {
                        //ACDR-L01-00-0017  
                        //FLUX ACDR Report / Fishing Category Code 
                        //The listID must be ECR_FISH_CATEGORY  
                        if (fluxAcdrReport.FishingCategoryCode.listID?.ToString() == "ECR_FISH_CATEGORY")
                        {
                            Console.WriteLine("ACDR-L01-00-0017 | OK | AcdrReport.FLUXACDRReport.FishingCategoryCode.listID provided and == ECR_FISH_CATEGORY");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L01-00-0017 | ERROR | No AcdrReport.FLUXACDRReport.FishingCategoryCode.listID provided or != ECR_FISH_CATEGORY");
                            //ACDR-L01-00-0017 - error
                        }

                        if (fluxAcdrReport.FishingCategoryCode.Value != null)
                        {
                            valueReportFishingCategoryCode = fluxAcdrReport.FishingCategoryCode.Value.ToString();

                            Console.WriteLine("ACDR-L01-00-0018 | TODO | Check if AcdrReport.FLUXACDRReport.FishingCategoryCode.Value is in relevant List");
                            //ACDR-L01-00-0018 - error
                            //FLUX ACDR Report / Fishing Category Code   
                            //Fishing Category code must be in relevant List 
                            //TODO: Check if AcdrReport.FLUXACDRReport.FishingCategoryCode.Value is in relevant List
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.FishingCategoryCode.Value provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.FishingCategoryCode provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.FishingCategoryCode

                    #region AcdrReport.FLUXACDRReport.FAOFishingGearCode
                    if (fluxAcdrReport.FAOFishingGearCode != null)
                    {
                        //ACDR-L01-00-0019 
                        //FLUX ACDR Report / FAO Fishing Gear Code 
                        //The listID must be GEAR_TYPE 
                        if (fluxAcdrReport.FAOFishingGearCode.listID?.ToString() == "GEAR_TYPE")
                        {
                            Console.WriteLine("ACDR-L01-00-0019 | OK | AcdrReport.FLUXACDRReport.FAOFishingGearCode.listID provided and == GEAR_TYPE");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L01-00-0019 | ERROR | No AcdrReport.FLUXACDRReport.FAOFishingGearCode.listID provided or != GEAR_TYPE");
                            //ACDR-L01-00-0019 - error
                        }

                        if (fluxAcdrReport.FAOFishingGearCode.Value != null)
                        {
                            valueReportFAOFishingGearCode = fluxAcdrReport.FAOFishingGearCode.Value.ToString();

                            Console.WriteLine("ACDR-L01-00-0020 | TODO | Check if AcdrReport.FLUXACDRReport.FAOFishingGearCode.Value is in relevant List");
                            //ACDR-L01-00-0020 - error
                            //FLUX ACDR Report / FAO Fishing Gear Code 
                            //Fishing Gear code must be in relevant List 
                            //TODO: Check if AcdrReport.FLUXACDRReport.FAOFishingGearCode.Value is in relevant List
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.FAOFishingGearCode.Value provided");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.FAOFishingGearCode provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.FAOFishingGearCode

                    #region AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans
                    if (fluxAcdrReport.SpecifiedVesselTransportMeans != null)
                    {
                        #region AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID
                        if (fluxAcdrReport.SpecifiedVesselTransportMeans.ID != null)
                        {
                            var specifiedVesselTransportMeansId = fluxAcdrReport.SpecifiedVesselTransportMeans.ID.First();

                            //ACDR-L01-00-0030 
                            //Specified Vessel Transport Means / ID 
                            //The code of the schemeID must be CFR 
                            if (specifiedVesselTransportMeansId.schemeID?.ToString() == "CFR")
                            {
                                Console.WriteLine("ACDR-L01-00-0030 | OK | AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID.schemeID provided and == CFR");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L01-00-0030 | ERROR | AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID.schemeID provided or != CFR");
                                //ACDR-L01-00-0030 - error
                            }

                            if (specifiedVesselTransportMeansId.Value != null)
                            {
                                valueReportTransportMeansId = specifiedVesselTransportMeansId.Value.ToString();
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID.Value provided");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID provided");
                        }
                        #endregion AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.ID

                        #region AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry
                        if (fluxAcdrReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry != null)
                        {
                            //ACDR-L01-00-0035 
                            //Registration Vessel Country / ID 
                            //The listID must be TERRITORY 
                            //#Q No listID present -> is it schemeID == TERRITORY?
                            if (fluxAcdrReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID?.schemeID?.ToString() == "TERRITORY")
                            {
                                Console.WriteLine("ACDR-L01-00-0035 | OK | AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID provided and == TERRITORY");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L01-00-0035 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.schemeID provided or != TERRITORY");
                                //ACDR-L01-00-0035 - error
                            }

                            if (fluxAcdrReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID?.Value != null)
                            {
                                valueReportTransportMeansRegistrationVesselCountryId = fluxAcdrReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.Value.ToString();

                                Console.WriteLine("ACDR-L01-00-0036 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.Value is in relevant list");
                                //ACDR-L01-00-0036 
                                //Registration Vessel Country / ID 
                                //The Vessel Country ID must be in relevant List 
                                //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.Value is in relevant list
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry.ID.Value provided");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry provided");
                        }
                        #endregion AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans.RegistrationVesselCountry
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.SpecifiedVesselTransportMeans

                    #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea
                    if (fluxAcdrReport.SpecifiedACDRReportedArea != null)
                    {
                        foreach (var specifiedAcdrReportArea in fluxAcdrReport.SpecifiedACDRReportedArea)
                        {
                            #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode
                            if (specifiedAcdrReportArea.FAOIdentificationCode != null)
                            {
                                //ACDR-L01-00-0040 
                                //Specified ACDR Reported Area / FAO Identification Code 
                                //The listID must be FAO_AREA 
                                if (specifiedAcdrReportArea.FAOIdentificationCode.listID?.ToString() == "FAO_AREA")
                                {
                                    Console.WriteLine("ACDR-L01-00-0040 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.listID provided and == FAO_AREA");
                                }
                                else
                                {
                                    Console.WriteLine("ACDR-L01-00-0040 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.listID provided or != FAO_AREA");
                                    //ACDR-L01-00-0040 - error 
                                }

                                if (specifiedAcdrReportArea.FAOIdentificationCode.Value != null)
                                {
                                    //#Q Is this FAO Area
                                    valueReportFAOIdentificationCode = specifiedAcdrReportArea.FAOIdentificationCode.Value.ToString();

                                    Console.WriteLine("ACDR-L01-00-0041 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.Value is in FAO Major Fishing Areas list");
                                    //ACDR-L01-00-0041 
                                    //Specified ACDR Reported Area / FAO Identification Code 
                                    //FAO Area should be in FAO Major Fishing Areas list 
                                    //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.Value is in FAO Major Fishing Areas list

                                    Console.WriteLine("ACDR-L01-00-0042 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.Value is provided detailed");
                                    //ACDR-L01-00-0042 
                                    //Specified ACDR Reported Area / FAO Identification Code 
                                    //FAO area must be given as the most detailed level possible 
                                    //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode.Value is provided detailed
                                }
                                else
                                {
                                    Console.WriteLine("No AcdrReport.FLUXACDRReport.FAOFishingGearCode.Value provided");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode provided");
                            }
                            #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.FAOIdentificationCode

                            #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode
                            if (specifiedAcdrReportArea.SovereigntyWaterCode != null)
                            {
                                //ACDR-L01-00-0043 
                                //Specified ACDR Reported Area / Sovereignty Water Code 
                                //The listID must be TERRITORY 
                                if (specifiedAcdrReportArea.SovereigntyWaterCode.listID?.ToString() == "TERRITORY")
                                {
                                    Console.WriteLine("ACDR-L01-00-0043 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode.listID provided and == TERRITORY");

                                }
                                else
                                {
                                    Console.WriteLine("ACDR-L01-00-0043 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode.listID provided or != TERRITORY");
                                    //ACDR-L01-00-0043 - error
                                }

                                if (specifiedAcdrReportArea.SovereigntyWaterCode.Value != null)
                                {
                                    valueReportSovereigntyWaterCode = specifiedAcdrReportArea.SovereigntyWaterCode.Value.ToString();

                                    //ACDR-L01-00-0045 
                                    //Specified ACDR Reported Area / Sovereignty Water Code 
                                    //Sovereignty Waters code ‘XEU’ is not allowed 
                                    if (specifiedAcdrReportArea.SovereigntyWaterCode.Value.ToString() == "XEU")
                                    {
                                        Console.WriteLine("ACDR-L01-00-0045 | ERROR | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode provided with Value == XEU");
                                        //ACDR-L01-00-0045 - error
                                    }
                                    else
                                    {
                                        Console.WriteLine("ACDR-L01-00-0045 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode provided with Value != XEU");

                                        Console.WriteLine("ACDR-L01-00-0044 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode.Value is in relevant list");
                                        //ACDR-L01-00-0044 
                                        //Specified ACDR Reported Area / Sovereignty Water Code 
                                        //Sovereignty Waters code must be in relevant List 
                                        //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode.Value is in relevant list
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode.Value provided");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode provided");
                            }
                            #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SovereigntyWaterCode

                            #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode
                            if (specifiedAcdrReportArea.LandingCountryCode != null)
                            {
                                //ACDR-L01-00-0046 
                                //Specified ACDR Reported Area / Landing Country Code
                                //The listID must be ECR_LAND_PLACE 
                                if (specifiedAcdrReportArea.LandingCountryCode.listID?.ToString() == "ECR_LAND_PLACE")
                                {
                                    Console.WriteLine("ACDR-L01-00-0046 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.listID provided and == ECR_LAND_PLACE");
                                }
                                else
                                {
                                    Console.WriteLine("ACDR-L01-00-0046 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.listID provided or != ECR_LAND_PLACE");
                                    //ACDR-L01-00-0046 - error
                                }

                                if (specifiedAcdrReportArea.LandingCountryCode.Value != null)
                                {
                                    valueReportLandingCountryCode = specifiedAcdrReportArea.LandingCountryCode.Value.ToString();

                                    Console.WriteLine("ACDR-L01-00-0047 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.Value is in relevant List");
                                    //ACDR-L01-00-0047 - error
                                    //Specified ACDR Reported Area / Landing Country Code 
                                    //Landing Country code must be in relevant List 
                                    //TODO: Check if 

                                    Console.WriteLine("ACDR-L01-00-0048 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.Value corresponds to a landlocked country");
                                    //ACDR-L01-00-0048 - warning
                                    //Specified ACDR Reported Area / Landing Country Code 
                                    //Check if Landing Country code corresponds to a landlocked country 
                                    //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.Value corresponds to a landlocked country
                                }
                                else
                                {
                                    Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode.Value provided");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode provided");
                            }
                            #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.LandingCountryCode

                            #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode
                            if (specifiedAcdrReportArea.CatchStatusCode != null)
                            {
                                //ACDR-L01-00-0049
                                //Specified ACDR Reported Area / Catch Status Code 
                                //The listID must be ECR_CATCH_STATUS 
                                if (specifiedAcdrReportArea.CatchStatusCode.listID?.ToString() == "ECR_CATCH_STATUS")
                                {
                                    Console.WriteLine("ACDR-L01-00-0049 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode.listID provided and == ECR_CATCH_STATUS");
                                }
                                else
                                {
                                    Console.WriteLine("ACDR-L01-00-0049 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode.listID provided or != ECR_CATCH_STATUS");
                                    //ACDR-L01-00-0049 - error
                                }

                                if (specifiedAcdrReportArea.CatchStatusCode.Value != null)
                                {
                                    valueReportCatchStatusCode = specifiedAcdrReportArea.LandingCountryCode.Value;

                                    Console.WriteLine("ACDR-L01-00-0050 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode.Value is in relevant list");
                                    //ACDR-L01-00-0050 
                                    //Specified ACDR Reported Area / Catch Status Code 
                                    //Catch Status code must be in relevant List 
                                    //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode.Value is in relevant list
                                }
                                else
                                {
                                    Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode.Value provided");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode provided");
                            }
                            #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.CatchStatusCode

                            #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch
                            if (specifiedAcdrReportArea.SpecifiedACDRCatch != null)
                            {
                                foreach (var specifiedAcdrCatch in specifiedAcdrReportArea.SpecifiedACDRCatch)
                                {
                                    #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode
                                    if (specifiedAcdrCatch.FAOSpeciesCode != null)
                                    {
                                        //ACDR-L01-00-0060 
                                        //Specified ACDR Catch / FAO Species Code 
                                        //The listID must be FAO_SPECIES 
                                        if (specifiedAcdrCatch.FAOSpeciesCode.listID?.ToString() == "FAO_SPECIES")
                                        {
                                            Console.WriteLine("ACDR-L01-00-0060 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode.listID provided and == FAO_SPECIES");
                                        }
                                        else
                                        {
                                            Console.WriteLine("ACDR-L01-00-0060 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode.listID provided or != FAO_SPECIES");
                                            //ACDR-L01-00-0060 - error
                                        }

                                        if (specifiedAcdrCatch.FAOSpeciesCode.Value != null)
                                        {
                                            valueReportCatchFAOSpeciesCode = specifiedAcdrCatch.FAOSpeciesCode.Value.ToString();

                                            Console.WriteLine("ACDR-L01-00-0061 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode.Value is in relevant list");
                                            //ACDR-L01-00-0061 
                                            //Specified ACDR Catch / FAO Species Code 
                                            //Species code must be in relevant List 
                                            //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode.Value is in relevant list
                                        }
                                        else
                                        {
                                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode.Value provided");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode provided");
                                    }
                                    #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode

                                    #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity
                                    if (specifiedAcdrCatch.UnitQuantity != null)
                                    {
                                        if (specifiedAcdrCatch.UnitQuantity.Value != null)
                                        {
                                            valueReportCatchUnitQuantity = specifiedAcdrCatch.UnitQuantity.Value.ToString();

                                            //ACDR-L01-00-0065 
                                            //Specified ACDR Catch / Unit Quantity 
                                            //The Unit Quantity must be a positive value 
                                            if (specifiedAcdrCatch.UnitQuantity.Value > 0)
                                            {
                                                Console.WriteLine("ACDR-L01-00-0065 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity.Value is a positive value");

                                                Console.WriteLine("ACDR-L01-00-0063 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity.Value is in the FLUX_UNIT list");
                                                //ACDR-L01-00-0063 
                                                //Specified ACDR Catch / Unit Quantity 
                                                //The Unit Quantity code must be in the FLUX_UNIT list
                                                //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity.Value is in the FLUX_UNIT list
                                            }
                                            else
                                            {
                                                Console.WriteLine("ACDR-L01-00-0065 | ERROR | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity.Value is not a positive value");
                                                //ACDR-L01-00-0065 - error
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity.Value provided");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity provided");
                                    }
                                    #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UnitQuantity

                                    #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure
                                    if (specifiedAcdrCatch.WeightMeasure != null)
                                    {
                                        //ACDR-L01-00-0070 
                                        //Specified ACDR Catch / Weight Measure 
                                        //Unit code for Weight Measure must be in metric tonnes (TNE). 
                                        if (specifiedAcdrCatch.WeightMeasure.unitCode?.ToString() == "TNE")
                                        {
                                            Console.WriteLine("ACDR-L01-00-0070 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.unitCode provided and == TNE");

                                        }
                                        else
                                        {
                                            Console.WriteLine("ACDR-L01-00-0070 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.unitCode provided or != TNE");
                                            //ACDR-L01-00-0070 - error 
                                        }

                                        if (specifiedAcdrCatch.WeightMeasure.Value != null)
                                        {
                                            valueReportCatchWeightMeasure = specifiedAcdrCatch.WeightMeasure.Value.ToString();

                                            //ACDR-L01-00-0069 
                                            //Specified ACDR Catch / Weight Measure 
                                            //The weight must be a positive value 
                                            if (specifiedAcdrCatch.WeightMeasure.Value > 0)
                                            {
                                                Console.WriteLine("ACDR-L01-00-0069 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.Value is a positive value");

                                                //#Q Counts zeros as significant decimal places -> will count 4 if value == 1.1000
                                                //int decimalPlacesCount = BitConverter.GetBytes(decimal.GetBits(specifiedAcdrCatch.WeightMeasure.Value)[3])[2];

                                                //#Q Does nоt count zeros as significant decimal places -> will count 1 if value == 1.1000
                                                int decimalPlacesCount = BitConverter.GetBytes(decimal.GetBits((decimal)(double)specifiedAcdrCatch.WeightMeasure.Value)[3])[2];

                                                //ACDR-L01-00-0071 
                                                //Specified ACDR Catch / Weight Measure 
                                                //The weight must be with no more than 3 decimals. 
                                                if (decimalPlacesCount <= 3)
                                                {
                                                    Console.WriteLine("ACDR-L01-00-0071 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.Value has up to 3 decimals");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("ACDR-L01-00-0071 | ERROR | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.Value has more than 3 decimals");
                                                    //ACDR-L01-00-0071 - error
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("ACDR-L01-00-0069 | ERROR | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.Value is not a positive value");
                                                //ACDR-L01-00-0069 - error
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure.Value provided");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure provided");
                                    }
                                    #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.WeightMeasure

                                    #region AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode
                                    if (specifiedAcdrCatch.UsageCode != null)
                                    {
                                        //ACDR-L01-00-0072 
                                        //Specified ACDR Catch / Usage Code 
                                        //The listID must be ECR_USAGE_IND 
                                        if (specifiedAcdrCatch.UsageCode.listID?.ToString() == "ECR_USAGE_IND ")
                                        {
                                            Console.WriteLine("ACDR-L01-00-0072 | OK | AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode.listID provided and == ECR_USAGE_IND");
                                        }
                                        else
                                        {
                                            Console.WriteLine("ACDR-L01-00-0072 | ERROR | No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode.listID provided or != ECR_USAGE_IND");
                                            //ACDR-L01-00-0072 - error
                                        }

                                        if (specifiedAcdrCatch.UsageCode.Value != null)
                                        {
                                            valueReportCatchUsageCode = specifiedAcdrCatch.UsageCode.Value.ToString();

                                            Console.WriteLine("ACDR-L01-00-0073 | TODO | Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode.Value is in relevant list");
                                            //ACDR-L01-00-0073 
                                            //Specified ACDR Catch / Usage Code 
                                            //Usage code must be in relevant List 
                                            //TODO: Check if AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode.Value is in relevant list
                                        }
                                        else
                                        {
                                            Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode.Value provided");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode provided");
                                    }
                                    #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.UsageCode
                                }
                            }
                            else
                            {
                                Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch provided");
                            }
                            #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch
                        }
                    }
                    else
                    {
                        Console.WriteLine("No AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea provided");
                    }
                    #endregion AcdrReport.FLUXACDRReport.SpecifiedACDRReportedArea

                    //ACDR-L02-00-0001 

                    //FLUX ACDR Report / Type Code 
                    //FLUX ACDR Report / Regional Species Code
                    //FLUX ACDR Report / Regional Area Code
                    //FLUX ACDR Report / Fishing Category Code
                    //Specified Vessel Transport Means / ID
                    //Specified Vessel Transport Means / Registration Vessel Country / ID
                    //FLUX ACDR Report / FAO Fishing Gear Code
                    //Specified ACDR Reported Area / FAO Identification Code
                    //Specified ACDR Reported Area / Sovereignty Water Code
                    //Specified ACDR Reported Area / Catch Status Code
                    //Specified ACDR Catch / FAO Species Code
                    //Quantifies Reported Catch / Unit Quantity
                    //Specified ACDR Catch / Weight Measure
                    //Specified ACDR Catch / Usage Code

                    //Check mandatory data elements for specified report type13 

                    //#Q FLUX_P1000-12_ACDR_domain_EU_Implementation_v5.2.pdf
                    //#Q 6.1.7.Data elements
                    switch (valueReportTypeCode)
                    {
                        case "ACDR-REGIONAL":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportTypeCode provided");
                            }

                            if (valueReportRegionalSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportRegionalSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportRegionalSpeciesCode provided");
                            }

                            if (valueReportRegionalAreaCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportRegionalAreaCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportRegionalAreaCode provided");
                            }

                            if (valueReportFAOIdentificationCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportFAOIdentificationCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportFAOIdentificationCode provided");
                            }

                            if (valueReportSovereigntyWaterCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportSovereigntyWaterCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportSovereigntyWaterCode provided");
                            }

                            if (valueReportCatchFAOSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportCatchFAOSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportCatchFAOSpeciesCode provided");
                            }

                            if (valueReportCatchWeightMeasure != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportCatchWeightMeasure provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportCatchWeightMeasure provided");
                            }

                            if (valueReportCatchUsageCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportCatchUsageCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportCatchUsageCode provided");
                            }

                            if (valueReportCatchStatusCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-REGIONAL | valueReportCatchStatusCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-REGIONAL | No valueReportCatchStatusCode provided");
                            }

                            //ACDR-L02-00-0002 
                            //FLUX ACDR Report / Type Code 
                            //FLUX_ ACDR Report / Regional Species Code
                            //Specified ACDR Reported Area / Sovereignty Water Code
                            //For ACDR-REGIONAL report type BFT, SBF and CJM Regional species codes are not allowed 
                            if (valueReportRegionalSpeciesCode != "" && valueReportRegionalSpeciesCode != "BFT" && valueReportRegionalSpeciesCode != "SBF" && valueReportRegionalSpeciesCode != "CJM")
                            {
                                Console.WriteLine("ACDR-L02-00-0002 | OK | ACDR-REGIONAL | valueReportCatchStatusCode provided and != BFT || SBF || CJM");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L02-00-0002 | ERROR | ACDR-REGIONAL | No valueReportRegionalSpeciesCode provided or not properly set or == BFT || SBF || CJM");
                                //ACDR-L02-00-0002 - error
                            }
                            break; 
                        case "ACDR-FISHING-GEAR":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportTypeCode provided");
                            }

                            if (valueReportRegionalSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportRegionalSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportRegionalSpeciesCode provided");
                            }

                            if (valueReportRegionalAreaCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportRegionalAreaCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportRegionalAreaCode provided");
                            }

                            if (valueReportFishingCategoryCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportFishingCategoryCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportFishingCategoryCode provided");
                            }

                            if (valueReportFAOFishingGearCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportFAOFishingGearCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportFAOFishingGearCode provided");
                            }

                            if (valueReportFAOIdentificationCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportFAOIdentificationCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportFAOIdentificationCode provided");
                            }

                            if (valueReportSovereigntyWaterCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportSovereigntyWaterCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportSovereigntyWaterCode provided");
                            }

                            if (valueReportCatchFAOSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportCatchFAOSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportCatchFAOSpeciesCode provided");
                            }

                            if (valueReportCatchWeightMeasure != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportCatchWeightMeasure provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportCatchWeightMeasure provided");
                            }

                            if (valueReportCatchUsageCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportCatchUsageCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportCatchUsageCode provided");
                            }

                            if (valueReportCatchStatusCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-GEAR | valueReportCatchStatusCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-GEAR | No valueReportCatchStatusCode provided");
                            }

                            //ACDR-L02-00-0005 
                            //FLUX ACDR Report / Type, Code FLUX ACDR Report / Regional Species Code
                            //For ACDR-FISHING_GEAR report only BFT is allowed as regional species code 
                            if (valueReportRegionalSpeciesCode != "" && valueReportRegionalSpeciesCode == "BFT")
                            {
                                Console.WriteLine("ACDR-L02-00-0005 | OK | ACDR-FISHING-GEAR | valueReportRegionalSpeciesCode provided and == BFT");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L02-00-0005 | ERROR | ACDR-FISHING-GEAR | No valueReportRegionalSpeciesCode provided or not properly set or != BFT");
                                //ACDR-L02-00-0005 - error
                            }

                            if (valueReportFAOFishingGearCode != "")
                            {
                                Console.WriteLine("ACDR-L02-00-0005 | TOTO | ACDR-FISHING-GEAR | Check if valueReportFAOFishingGearCode is valid for ICCAT");
                                //ACDR-L02-00-0015 - error
                                //FLUX ACDR Report / Type Code, FLUX ACDR Report / FAO Fishing Gear Code
                                //For ACDR-FISHING_GEAR reports, only FAO fishing gears with a corresponding ICCAT gear type are accepted 
                                //TODO: Check if valueReportFAOFishingGearCode is valid for ICCAT.
                            }
                            else
                            {
                                Console.WriteLine("No valueReportFAOFishingGearCode provided or not properly set");
                            }

                            break;
                        case "ACDR-VESSEL":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportTypeCode provided");
                            }

                            if (valueReportRegionalSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportRegionalSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportRegionalSpeciesCode provided");
                            }

                            if (valueReportRegionalAreaCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportRegionalAreaCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportRegionalAreaCode provided");
                            }

                            if (valueReportTransportMeansId != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportTransportMeansId provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportTransportMeansId provided");
                            }

                            if (valueReportTransportMeansRegistrationVesselCountryId != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportTransportMeansRegistrationVesselCountryId provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportTransportMeansRegistrationVesselCountryId provided");
                            }

                            if (valueReportFAOIdentificationCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportFAOIdentificationCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportFAOIdentificationCode provided");
                            }

                            if (valueReportSovereigntyWaterCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportSovereigntyWaterCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportSovereigntyWaterCode provided");
                            }

                            if (valueReportCatchFAOSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportCatchFAOSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportCatchFAOSpeciesCode provided");
                            }

                            if (valueReportCatchWeightMeasure != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportCatchWeightMeasure provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportCatchWeightMeasure provided");
                            }

                            if (valueReportCatchUsageCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportCatchUsageCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportCatchUsageCode provided");
                            }

                            if (valueReportCatchStatusCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-VESSEL | valueReportCatchStatusCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-VESSEL | No valueReportCatchStatusCode provided");
                            }

                            //ACDR-L02-00-0003 
                            //FLUX ACDR Report / Type Code, FLUX_ ACDR Report / Regional Species Code
                            //For ACDR-VESSEL report type only SBF and CJM are allowed as Regional species code 
                            if (valueReportRegionalSpeciesCode != "" && (valueReportRegionalSpeciesCode == "SBF" || valueReportRegionalSpeciesCode == "CJM"))
                            {
                                Console.WriteLine("ACDR-L02-00-0003 | OK | ACDR-VESSEL | valueReportRegionalSpeciesCode provided and == SBF || CJM");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L02-00-0003 | ERROR | ACDR-VESSEL | No valueReportRegionalSpeciesCode provided or not properly set or != SBF || CJM");
                                //ACDR-L02-00-0003 - error
                            }

                            break;
                        case "ACDR-FISHING-CAT":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportTypeCode provided");
                            }

                            if (valueReportFishingCategoryCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportFishingCategoryCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportFishingCategoryCode provided");
                            }

                            if (valueReportTransportMeansId != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportTransportMeansId provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportTransportMeansId provided");
                            }

                            if (valueReportTransportMeansRegistrationVesselCountryId != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportTransportMeansRegistrationVesselCountryId provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportTransportMeansRegistrationVesselCountryId provided");
                            }

                            if (valueReportFAOIdentificationCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportFAOIdentificationCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportFAOIdentificationCode provided");
                            }

                            if (valueReportSovereigntyWaterCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportSovereigntyWaterCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportSovereigntyWaterCode provided");
                            }

                            if (valueReportCatchFAOSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportCatchFAOSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportCatchFAOSpeciesCode provided");
                            }

                            if (valueReportCatchWeightMeasure != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportCatchWeightMeasure provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportCatchWeightMeasure provided");
                            }

                            if (valueReportCatchUsageCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportCatchUsageCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportCatchUsageCode provided");
                            }

                            if (valueReportCatchStatusCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-FISHING-CAT | valueReportCatchStatusCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-FISHING-CAT | No valueReportCatchStatusCode provided");
                            }
                            break;
                        case "ACDR-OTHER":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportTypeCode provided");
                            }

                            if (valueReportFAOIdentificationCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportFAOIdentificationCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportFAOIdentificationCode provided");
                            }

                            if (valueReportSovereigntyWaterCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportSovereigntyWaterCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportSovereigntyWaterCode provided");
                            }

                            if (valueReportCatchFAOSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportCatchFAOSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportCatchFAOSpeciesCode provided");
                            }

                            if (valueReportCatchWeightMeasure != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportCatchWeightMeasure provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportCatchWeightMeasure provided");
                            }

                            if (valueReportCatchUsageCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportCatchUsageCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportCatchUsageCode provided");
                            }

                            if (valueReportCatchStatusCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-OTHER | valueReportCatchStatusCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-OTHER | No valueReportCatchStatusCode provided");
                            }

                            //ACDR-L02-00-0004 
                            //FLUX ACDR Report / Type Code, Specified ACDR Catch / FAO Species Code
                            //For ACDR-OTHER report type BFT is not allowed as FAO species code 
                            if (valueReportCatchFAOSpeciesCode != "" && valueReportCatchFAOSpeciesCode != "BFT")
                            {
                                Console.WriteLine("ACDR-L02-00-0004 | OK | ACDR-OTHER | valueReportCatchFAOSpeciesCode provided and != BFT");
                            }
                            else
                            {
                                Console.WriteLine("ACDR-L02-00-0004 | ERROR | ACDR-OTHER | No valueReportCatchFAOSpeciesCode provided or not properly set or == BFT");
                                //ACDR-L02-00-0004 - error
                            }

                            break;
                        case "ACDR-EFFORT":
                            if (valueReportTypeCode != "")
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-EFFORT | valueReportTypeCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-EFFORT | No valueReportTypeCode provided");
                            }

                            if (valueReportRegionalSpeciesCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-EFFORT | valueReportRegionalSpeciesCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-EFFORT | No valueReportRegionalSpeciesCode provided");
                            }

                            if (valueReportRegionalAreaCode != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-EFFORT | valueReportRegionalAreaCode provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-EFFORT | No valueReportRegionalAreaCode provided");
                            }

                            if (valueReportCatchUnitQuantity != null)
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | OK | ACDR-EFFORT | valueReportCatchUnitQuantity provided");
                            }
                            else
                            {
                                Console.WriteLine(">> ACDR-L02-00-0001 | ERROR | ACDR-EFFORT | No valueReportCatchUnitQuantity provided");
                            }

                            //ACDR-L02-00-0014 
                            //FLUX ACDR Report / Type Code, Specified ACDR Catch / FAO Species Code, Specified ACDR Reported Area / FAO Identification Code, Specified ACDR Reported Area / Sovereignty Water Code, Specified ACDR Reported Area / Catch Status Code
                            //FAO Species, FAO Area, Sovereign Waters, Catch Status data element shall be in the ACDR-EFFORT report but content is ignored 
                            if (fluxAcdrReport.SpecifiedACDRReportedArea != null)
                            {
                                foreach (var reportACDRReportedArea in fluxAcdrReport.SpecifiedACDRReportedArea)
                                {
                                    if (reportACDRReportedArea.SpecifiedACDRCatch != null)
                                    {
                                        foreach (var reportACDRCatch in reportACDRReportedArea.SpecifiedACDRCatch)
                                        {
                                            if (reportACDRCatch.FAOSpeciesCode != null)
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0014 | OK | fluxAcdrReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode != null");
                                            }
                                            else
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0014 | WARNING | No fluxAcdrReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch.FAOSpeciesCode provided");
                                                //ACDR-L02-00-0014 - warning
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> No fluxAcdrReport.SpecifiedACDRReportedArea.SpecifiedACDRCatch provided");
                                    }

                                    if (reportACDRReportedArea.FAOIdentificationCode != null)
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | OK | fluxAcdrReport.SpecifiedACDRReportedArea.FAOIdentificationCode != null");
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | WARNING | No fluxAcdrReport.SpecifiedACDRReportedArea.FAOIdentificationCode provided");
                                        //ACDR-L02-00-0014 - warning
                                    }

                                    if (reportACDRReportedArea.SovereigntyWaterCode != null)
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | OK | fluxAcdrReport.SpecifiedACDRReportedArea.SovereigntyWaterCode != null");

                                        Console.WriteLine(">>> ACDR-L02-00-0020 | TODO | Check if SovereigntyWaterCode is consistent with the FAO area");
                                        //ACDR-L02-00-0020 
                                        //Specified ACDR Reported Area / Sovereignty Water Code, Specified ACDR Reported Area / FAO Identification
                                        //The Sovereignty Waters should be coherent with the FAO area 
                                        //TODO: Check if SovereigntyWaterCode is consistent with the FAO area

                                        if (reportACDRReportedArea.SovereigntyWaterCode?.Value == "N/A")
                                        {
                                            //ACDR-L02-00-0021 
                                            //Specified ACDR Reported Area / Sovereignty Water Code, Specified ACDR Reported Area / FAO Identification
                                            //Sovereignty waters code 'N/A' (Not Applicable') is only valid in the Mediterranean sea (Subarea 37.1, 37.2 or 37.3) 
                                            //#Q Is the string literally "N/A"
                                            if (reportACDRReportedArea.FAOIdentificationCode?.Value == "37.1" || reportACDRReportedArea.FAOIdentificationCode?.Value == "37.2" || reportACDRReportedArea.FAOIdentificationCode?.Value == "37.3")
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0021 | OK | reportACDRReportedArea.SovereigntyWaterCode.Value provided and == 37.1 || 37.2 || 37.3");
                                            }
                                            else
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0021 | ERROR | No reportACDRReportedArea.SovereigntyWaterCode.Value provided or != 37.1 || 37.2 || 37.3");
                                                //ACDR-L02-00-0021 - error
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | WARNING | No fluxAcdrReport.SpecifiedACDRReportedArea.SovereigntyWaterCode provided");
                                        //ACDR-L02-00-0014 - warning
                                    }

                                    if (reportACDRReportedArea.CatchStatusCode != null)
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | OK | fluxAcdrReport.SpecifiedACDRReportedArea.CatchStatusCode != null");

                                        if (reportACDRReportedArea.CatchStatusCode?.Value != "LAN")
                                        {
                                            //ACDR-L02-00-0022 
                                            //Specified ACDR Reported Area / Landing Country Code, Specified ACDR Reported Area / Catch Status Code
                                            //The Landing Country code shall be provided for the Catch Status LAN 
                                            if (reportACDRReportedArea.LandingCountryCode?.Value != null)
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0022 | OK | fluxAcdrReport.SpecifiedACDRReportedArea.LandingCountryCode provided for fluxAcdrReport.SpecifiedACDRReportedArea.CatchStatusCode == LAN");
                                            }
                                            else
                                            {
                                                Console.WriteLine(">>> ACDR-L02-00-0022 | ERROR | No fluxAcdrReport.SpecifiedACDRReportedArea.LandingCountryCode provided for fluxAcdrReport.SpecifiedACDRReportedArea.CatchStatusCode == LAN");
                                                //ACDR-L02-00-0022 - error
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(">>> ACDR-L02-00-0014 | WARNING | No fluxAcdrReport.SpecifiedACDRReportedArea.CatchStatusCode profided");
                                        //ACDR-L02-00-0014 - warning
                                    }
                                }
                            }
                           
                            break;
                        case "":
                           Console.WriteLine("No AcdrReport.FLUXACDRReport.TypeCode provided or not properly set");
                           break;
                        default:
                            Console.WriteLine("AcdrReport.FLUXACDRReport.TypeCode unknown");
                            break;
                    }
                                    
                    if (valueReportTypeCode == "ACDR-FISHING-GEAR")
                    {
                        //ACDR-L02-00-0013 
                        //FLUX ACDR Report / Type Code, FLUX ACDR Report / Fishing Category Code
                        //BFT_TARGET, BFT_SPORT and BFT_BYCATCH fishing category codes are the only ones accepted for report type ACDR-FISHING-GEAR 
                        if (valueReportFishingCategoryCode == "BFT_TARGET" || valueReportFishingCategoryCode == "BFT_SPORT" || valueReportFishingCategoryCode == "BFT_BYCATCH")
                        {
                            Console.WriteLine("ACDR-L02-00-0013 | OK | valueReportFishingCategoryCode provided == BFT_TARGET || BFT_SPORT || BFT_BYCATCH for report type == ACDR-FISHING-GEAR");
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L02-00-0013 | ERROR | valueReportFishingCategoryCode provided != BFT_TARGET || BFT_SPORT || BFT_BYCATCH for report type == ACDR-FISHING-GEAR");
                            //ACDR-L02-00-0013 - error
                        }
                    }
                    else
                    {
                        //ACDR-L02-00-0012 
                        //FLUX ACDR Report / Type Code, FLUX ACDR Report / Fishing Category Code
                        //BFT_TARGET, BFT_SPORT and BFT_BYCATCH fishing category codes are rejected if report type is not ACDR-FISHING-GEAR 
                        if (valueReportFishingCategoryCode == "BFT_TARGET" || valueReportFishingCategoryCode == "BFT_SPORT" || valueReportFishingCategoryCode == "BFT_BYCATCH")
                        {
                            Console.WriteLine("ACDR-L02-00-0012 | ERROR | valueReportFishingCategoryCode provided == BFT_TARGET || BFT_SPORT || BFT_BYCATCH for report type != ACDR-FISHING-GEAR");
                            //ACDR-L02-00-0012 - error
                        }
                        else
                        {
                            Console.WriteLine("ACDR-L02-00-0012 | OK | valueReportFishingCategoryCode provided != BFT_TARGET || BFT_SPORT || BFT_BYCATCH for report type != ACDR-FISHING-GEAR");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No AcdrReport.FLUXACDRReport provided");
            }
            #endregion AcdrReport.FLUXACDRReport
        }
    }
}
