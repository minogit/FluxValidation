using System;
using System.Linq;
using ScortelApi.Models.FLUX;

namespace Validation.FluxDomainsValidation.FluxSalesDomainValidation
{
    class SalesResponseValidation
    {
        public void SalesResponseValidate(FLUXSalesResponseMessageType SalesResponse)
        {
            string valueResponseDocumentResponseCode = "";
            DateTime dateTimeFLUXResponseDocumentCreation = new DateTime();

            if (SalesResponse.FLUXResponseDocument != null)
            {
                //SALE-L00-00-0450, SALE-L01-00-0450, ALE-L01-00-0451, SALE-L03-00-0450
                #region SalesResponse.FLUXResponseDocument.ID
                //SALE-L00-00-0450
                //FLUX_Response_Document/Identification
                //Must be present
                if (SalesResponse.FLUXResponseDocument.ID != null)
                {
                    Console.WriteLine("SALE-L00-00-0450 | OK | SalesResponse.FLUXResponseDocument.ID provided");

                    //#Q As cardinality is up to one, take and validate the first element only
                    var salesResponseFLUXResponseDocumentID = SalesResponse.FLUXResponseDocument.ID.First();

                    //SALE-L01-00-0450
                    //FLUX_Response_Document/Identification
                    //Check attribute schemeID. Must be UUID.
                    if (salesResponseFLUXResponseDocumentID.schemeID?.ToString() == "UUID")
                    {
                        Console.WriteLine("SALE-L01-00-0450 | OK | SalesResponse.FLUXResponseDocument.ID.schemeID provided and == UUID");

                        Console.WriteLine("SALE-L01-00-0451 | TODO | Check if format of SalesResponse.FLUXResponseDocument.ID.Value meets the schemeID provided");
                        //SALE-L01-00-0451 - error
                        //FLUX_Response_Document/Identification
                        //Check Format. Must be according to the specified schemeID.
                        //Must be according to RFC4122 format for UUID.
                        //TODO: Check if format of SalesResponse.FLUXResponseDocument.ID.Value meets the schemeID provided

                        Console.WriteLine("SALE-L03-00-0450 | TODO | Check if SalesResponse.FLUXResponseDocument.ID.Value is unique");
                        //SALE-L03-00-0450 - error
                        //FLUX_Response_Document/Identification
                        //The identification must be unique and not already exist
                        //TODO: Check if SalesResponse.FLUXResponseDocument.ID.Value is unique
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0450 | ERROR | SalesResponse.FLUXResponseDocument.ID provided or != UUID");
                        //SALE-L01-00-0450 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0450 | ERROR | No SalesResponse.FLUXResponseDocument.ID provided");
                    //SALE-L00-00-0450 - error
                }
                #endregion SalesResponse.FLUXResponseDocument.ID

                //SALE-L00-00-0451, SALE-L01-00-0452, SALE-L01-00-0453, SALE-L03-00-0452
                #region SalesResponse.FLUXResponseDocument.ReferencedID
                //SALE-L00-00-0451
                //FLUX_Response_Document/Referenced Identification
                //Must be present
                if (SalesResponse.FLUXResponseDocument.ReferencedID != null)
                {
                    Console.WriteLine("SALE-L00-00-0451 | OK | SalesResponse.FLUXResponseDocument.ReferencedID provided");

                    Console.WriteLine("SALE-L01-00-0452 | TODO | Check if SalesResponse.FLUXResponseDocument.ReferencedID.schemeID is in FLUX_GP_MSG_ID list");
                    //SALE-L01-00-0452 - error
                    //FLUX_Response_Document/Referenced Identification
                    //Check attribute schemeID. Must be in the FLUX_GP_MSG_ID list
                    //TODO: Check if SalesResponse.FLUXResponseDocument.ReferencedID.schemeID is in FLUX_GP_MSG_ID list

                    Console.WriteLine("SALE-L01-00-0453 | TODO | Check if format of SalesResponse.FLUXResponseDocument.ReferencedID.Value meets the schemeID provided");
                    //SALE-L01-00-0453 - error
                    //FLUX_Response_Document/Referenced Identification
                    //Check Format. Must be according to the specified schemeID.
                    //Must be according to RFC4122 format for UUID.
                    //TODO: Check if format of SalesResponse.FLUXResponseDocument.ReferencedID.Value meets the schemeID provided

                    Console.WriteLine("SALE-L03-00-0452 | TODO | Check if SalesResponse.FLUXResponseDocument.ReferencedID.Value is a refference to a query or sales declaration");
                    //SALE-L03-00-0452 - error
                    //FLUX_Response_Document/Referenced Identification
                    //Must be a reference of a query or sales declaration
                    //TODO: Check if SalesResponse.FLUXResponseDocument.ReferencedID.Value is a refference to a query or sales declaration
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0451 | ERROR | No SalesResponse.FLUXResponseDocument.ReferencedID provided");
                    //SALE-L00-00-0451 - error
                }
                #endregion SalesResponse.FLUXResponseDocument.ReferencedID

                //SALE-L00-00-0452, SALE-L01-00-0454, SALE-L01-00-0455
                #region SalesResponse.FLUXResponseDocument.ResponseCode
                //SALE-L00-00-0452
                //FLUX_Response_Document/ResponseCode
                //Must be present.
                if (SalesResponse.FLUXResponseDocument.ResponseCode != null)
                {
                    Console.WriteLine("SALE-L00-00-0452 | OK | SalesResponse.FLUXResponseDocument.ResponseCode provided");

                    //SALE-L01-00-0454
                    //FLUX_Response_Document/ResponseCode
                    //Check attribute listID. Must be FLUX_GP_RESPONSE
                    if (SalesResponse.FLUXResponseDocument.ResponseCode.listID?.ToString() == "FLUX_GP_RESPONSE")
                    {
                        Console.WriteLine("SALE-L01-00-0454 | OK | SalesResponse.FLUXResponseDocument.ResponseCode.listID provided and == FLUX_GP_RESPONSE");

                        Console.WriteLine("SALE-L01-00-0455 | TODO | Check if SalesResponse.FLUXResponseDocument.ResponseCode.Value is code from FLUX_GP_RESPONSE list");
                        //SALE-L01-00-0455 - error
                        //FLUX_Response_Document/ResponseCode
                        //Check value. Code must be value of the specified code list in listID
                        //TODO: Check if SalesResponse.FLUXResponseDocument.ResponseCode.Value is code from the listID provided FLUX_GP_RESPONSE

                        valueResponseDocumentResponseCode = SalesResponse.FLUXResponseDocument.ResponseCode.Value.ToString();
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0454 | ERROR | No SalesResponse.FLUXResponseDocument.ResponseCode.listID provided or != FLUX_GP_RESPONSE");
                        //SALE-L01-00-0454 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0452 | ERROR | No SalesResponse.FLUXResponseDocument.ResponseCode provided");
                    //SALE-L00-00-0452 - error
                }
                #endregion SalesResponse.FLUXResponseDocument.ResponseCode

                //SALE-L00-00-0453, SALE-L01-00-0456, SALE-L03-00-0453
                #region SalesResponse.FLUXResponseDocument.CreationDateTime
                //SALE-L00-00-0453
                //FLUX_Response_Document/CreationDateTime
                //Check presence. Must be present.
                if (SalesResponse.FLUXResponseDocument.CreationDateTime != null)
                {
                    Console.WriteLine("SALE-L00-00-0453 | OK | SalesResponse.FLUXResponseDocument.CreationDateTime provided");

                    //SALE-L01-00-0456
                    //FLUX_Response_Document/CreationDateTime
                    //Check Format. Must be date in UTC according to ISO8601
                    //e.g. 2008-10- 31T15:07:38.6875 000Z.
                    //#Q Already returns DateTime, is there a better way to check format?
                    if (SalesResponse.FLUXResponseDocument.CreationDateTime.Item is DateTime)
                    {
                        Console.WriteLine("SALE-L01-00-0456 | OK | SalesResponse.FLUXResponseDocument.CreationDateTime.Item is DateTime");

                        dateTimeFLUXResponseDocumentCreation = SalesResponse.FLUXResponseDocument.CreationDateTime.Item;
                        DateTime dateTimeUtcNow = DateTime.UtcNow;

                        //SALE-L03-00-0453
                        //FLUX_Response_Document/CreationDateTime
                        //Date must be in the past.
                        if (DateTime.Compare(SalesResponse.FLUXResponseDocument.CreationDateTime.Item, dateTimeUtcNow) < 0)
                        {
                            Console.WriteLine("SALE-L03-00-0453 | OK | SalesResponse.FLUXResponseDocument.CreationDateTime.Item is in the past");
                        }
                        else
                        {
                            Console.WriteLine("SALE-L03-00-0453 | ERROR | SalesResponse.FLUXResponseDocument.CreationDateTime.Item is not in the past");
                            //SALE-L03-00-0453 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L01-00-0456 | ERROR | SalesResponse.FLUXResponseDocument.CreationDateTime.Item is not DateTime");
                        //SALE-L01-00-0456 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0453 | ERROR | No SalesResponse.FLUXResponseDocument.CreationDateTime provided");
                    //SALE-L00-00-0453 - error
                }
                #endregion SalesResponse.FLUXResponseDocument.CreationDateTime

                //SALE-L02-00-0450, SALE-L00-00-0470, SALE-L01-00-0470, SALE-L00-00-0471, SALE-L01-00-0471, SALE-L02-00-0470
                #region SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument
                if (valueResponseDocumentResponseCode != "" && valueResponseDocumentResponseCode != "OK")
                {
                    //SALE-L02-00-0450
                    //Validation Result Document
                    //Must be present if ResponseCode <> OK
                    //#Q Reading "<> OK" as "not equal to OK"
                    if (SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument != null)
                    {
                        Console.WriteLine("SALE-L02-00-0450 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument provided for ResponseCode != OK");

                        //#Q As cardinality is up to *, validate each element
                        foreach (var relatedValidationResultDocument in SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument)
                        {
                            //SALE-L00-00-0470
                            //Validation Result Document/Identification
                            //Must be present
                            if (relatedValidationResultDocument.ValidatorID != null)
                            {
                                Console.WriteLine("SALE-L00-00-0470 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.ValidatorID provided");

                                Console.WriteLine("SALE-L01-00-0470 | TODO | Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.ValidatorID.Value is code from schemeID");
                                //SALE-L01-00-0470 - error
                                //Validation Result Document/Identification
                                //Check code from the list schemeID
                                //TODO: Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.ValidatorID.Value is code from schemeID
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0470 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.ValidatorID provided");
                                //SALE-L00-00-0470 - error
                            }

                            //SALE-L00-00-0471
                            //Validation Result Document/Creation
                            //Must be present
                            if (relatedValidationResultDocument.CreationDateTime != null)
                            {
                                Console.WriteLine("SALE-L00-00-0471 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime provided");

                                //SALE-L01-00-0471
                                //Validation Result Document/Creation
                                //Check Format
                                //#Q Already returns DateTime, is there a better way to check format?
                                if (relatedValidationResultDocument.CreationDateTime.Item is DateTime)
                                {
                                    Console.WriteLine("SALE-L01-00-0471 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime.Item is DateTime");

                                    if (dateTimeFLUXResponseDocumentCreation != DateTime.MinValue)
                                    {
                                        //SALE-L02-00-0470
                                        //Validation Result Document/Creation, FLUX Response Document / Creation
                                        //The Creation must be <= to the Creation of the report
                                        //#Q Does "Creation of the report" means FLUX Response Document / Creation
                                        if (DateTime.Compare(relatedValidationResultDocument.CreationDateTime.Item, dateTimeFLUXResponseDocumentCreation) <= 0)
                                        {
                                            Console.WriteLine("SALE-L02-00-0470 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime.Item is before or equal to the creation of the report");
                                        }
                                        else
                                        {
                                            Console.WriteLine("SALE-L02-00-0470 | WARNING | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime.Item is after the creation of the report");
                                            //SALE-L02-00-0470 - warning
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("SalesResponse.FLUXResponseDocument.CreationDateTime.Item not set");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("SALE-L01-00-0471 | ERROR | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime.Item is not DateTime");
                                    //SALE-L01-00-0471 - error
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0471 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.CreationDateTime provided");
                                //SALE-L00-00-0471 - error
                            }

                            //SALE-L00-00-0472, SALE-L00-00-0480, SALE-L01-00-0480, SALE-L00-00-0481, SALE-L01-00-0481, SALE-L00-00-0482, SALE-L01-00-0482, SALE-L02-00-0480
                            #region SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis
                            //SALE-L00-00-0472
                            //Validation Quality Analysis
                            //Must be present if validation document
                            if (relatedValidationResultDocument.RelatedValidationQualityAnalysis != null)
                            {
                                Console.WriteLine("SALE-L00-00-0472 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis provided");

                                //#Q As cardinality is up to *, validate each element
                                foreach (var relatedValidationQualityAnalysis in relatedValidationResultDocument.RelatedValidationQualityAnalysis)
                                {
                                    //SALE-L00-00-0480
                                    //Validation QualityAnalysis/Identification
                                    //Must be present.
                                    if (relatedValidationQualityAnalysis.ID?.Value != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0480 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ID provided with Value");

                                        Console.WriteLine("SALE-L01-00-0480 | TODO | Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ID.Value is code from the listID provided");
                                        //SALE-L01-00-0480 - error
                                        //Validation QualityAnalysis/Identification
                                        //Check value. Code must be value of the specified code list in listID.
                                        //TODO: Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ID.Value is code from the listID provided
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0480 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ID provided with Value");
                                        //SALE-L00-00-0480 - error
                                    }

                                    //SALE-L00-00-0481
                                    //Validation QualityAnalysis/Level
                                    //Must be present.
                                    if (relatedValidationQualityAnalysis.LevelCode?.Value != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0481 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.LevelCode provided with Value");

                                        Console.WriteLine("SALE-L01-00-0481 | TODO | Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.LevelCode.Value is code from the listID provided");
                                        //SALE-L01-00-0481 - error
                                        //Validation QualityAnalysis/Level
                                        //Check Code. Must be in the list specified in listID.
                                        //TODO: Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.LevelCode.Value is code from the listID provided
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0481 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.LevelCode provided with Value");
                                        //SALE-L00-00-0481 - error
                                    }

                                    //SALE-L00-00-0482
                                    //Validation QualityAnalysis/Type
                                    //Must be present.
                                    if (relatedValidationQualityAnalysis.TypeCode?.Value != null)
                                    {
                                        Console.WriteLine("SALE-L00-00-0482 | OK | SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.TypeCode provided with Value");

                                        Console.WriteLine("SALE-L01-00-0482 | TODO | Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.TypeCode.Value is code from the listID provided");
                                        //SALE-L01-00-0482
                                        //Validation QualityAnalysis/Type
                                        //Check Code. Must be in the list specified in listID.
                                        //TODO: Check if SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.TypeCode.Value is code from the listID provided
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L00-00-0482 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.TypeCode provided with Value");
                                        //SALE-L00-00-0482 - error
                                    }

                                    //SALE-L02-00-0480
                                    //Validation QualityAnalysis/Referenced_ItemText
                                    //At least one occurrence if the Quality Analysis is in a Response message
                                    if (relatedValidationQualityAnalysis.ReferencedItem.Count() > 0)
                                    {
                                        Console.WriteLine("SALE-L02-00-0480 | OK | At least one SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ReferencedItem provided");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SALE-L02-00-0480 | WARNING | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis.ReferencedItem provided");
                                        //SALE-L02-00-0480 - warning
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("SALE-L00-00-0472 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis provided");
                                //SALE-L00-00-0472 - error
                            }
                            #endregion SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument.RelatedValidationQualityAnalysis
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L02-00-0450 | ERROR | No SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument provided for ResponseCode != OK");
                        //SALE-L02-00-0450 - error
                    }
                }
                #endregion SalesResponse.FLUXResponseDocument.RelatedValidationResultDocument

                //SALE-L00-00-0454, SALE-L00-00-0455, SALE-L00-00-0460, SALE-L01-00-0460, SALE-L03-00-0460
                #region SalesResponse.FLUXResponseDocument.RespondentFLUXParty
                //SALE-L00-00-0454
                //FLUX_Party
                //Must be present
                if (SalesResponse.FLUXResponseDocument.RespondentFLUXParty != null)
                {
                    Console.WriteLine("SALE-L00-00-0454 | OK | SalesResponse.FLUXResponseDocument.RespondentFLUXParty provided");

                    //SALE-L00-00-0455
                    //FLUX_Party
                    //Only one occurence
                    //#Q Is this check workable
                    if (!SalesResponse.FLUXResponseDocument.RespondentFLUXParty.GetType().IsArray)
                    {
                        Console.WriteLine("SALE-L00-00-0455 | OK | SalesResponse.FLUXResponseDocument.RespondentFLUXParty is not an Array = 1 occurance");
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0455 | ERROR | SalesResponse.FLUXResponseDocument.RespondentFLUXParty is an Array > 1 occurance");
                        //SALE-L00-00-0455 - error
                    }

                    //#Q As cardinality is up to one, take and validate the first element only
                    var respondentFLUXPartyID = SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID.First();

                    //SALE-L00-00-0460
                    //FLUXParty/Identification
                    //Must be present
                    if (respondentFLUXPartyID != null)
                    {
                        Console.WriteLine("SALE-L00-00-0460 | OK | SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID provided");

                        //SALE-L01-00-0460
                        //FLUXParty/Identification
                        //Check attribute listID. Must be FLUX_GP_PARTY
                        //#Q It is schemeID and not listID
                        if (respondentFLUXPartyID.schemeID?.ToString() == "FLUX_GP_PARTY")
                        {
                            Console.WriteLine("SALE-L01-00-0460 | OK | SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID.schemeID provided and == FLUX_GP_PARTY");

                            Console.WriteLine("SALE-L03-00-0460 | TODO | Check if SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID.Value is consistent with TL values");
                            //SALE-L03-00-0460 - warning
                            //FLUXParty/Identification
                            //Check if RespondentFLUXParty.ID is consistent with FLUX TL values.
                            //The party sending must be allowed to send the message
                            //TODO: Check if SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID.Value is consistent with TL values
                        }
                        else
                        {
                            Console.WriteLine("SALE-L01-00-0460 | ERROR | No SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID.schemeID provided or != FLUX_GP_PARTY");
                            //SALE-L01-00-0460 - error
                        }
                    }
                    else
                    {
                        Console.WriteLine("SALE-L00-00-0460 | ERROR | No SalesResponse.FLUXResponseDocument.RespondentFLUXParty.ID provided");
                        //SALE-L00-00-0460 - error
                    }
                }
                else
                {
                    Console.WriteLine("SALE-L00-00-0454 | ERROR | No SalesResponse.FLUXResponseDocument.RespondentFLUXParty provided");
                    //SALE-L00-00-0454 - error
                }
                #endregion SalesResponse.FLUXResponseDocument.RespondentFLUXParty
            }
            else
            {
                Console.WriteLine("No SalesResponse.FLUXResponseDocument provided");
            }
        }
    }
}
