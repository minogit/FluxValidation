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
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0009 | REJECTED | No FLUXReportDocument.TypeCode or no FLUXReportDocument.TypeCode.Value provided");
                                        //VESSEL-L00-00-0009 - rejected
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
                                            }
                                        }
                                        else
                                        {
                                            System.Console.WriteLine("VESSEL-L00-00-0007 | REJECTED | No FLUXReportDocument.CreationDateTime.Item (DateTime) provided or it has default value");
                                            //VESSEL-L00-00-0007 - rejected
                                        }
                                    }
                                    else
                                    {
                                        System.Console.WriteLine("VESSEL-L00-00-0006 | REJECTED | No FLUXReportDocument.CreationDateTime provided");
                                        //VESSEL-L00-00-0006 - rejected
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