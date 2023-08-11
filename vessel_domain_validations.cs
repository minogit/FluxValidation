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
                                        //VESSEL-L00-00-0045
                                        //FLUX_Report Document/Type
                                        //ListId=FLUX_VESSEL_REPORT_TYPE
                                        if (Vesselreport.FLUXReportDocument.TypeCode.listID.ToString() == "FLUX_VESSEL_REPORT_TYPE")
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0045 - rejected
                                        }

                                        //VESSEL-L00-00-0008
                                        //FLUX_Report Document/Type
                                        //Code from the specified list
                                        //#Q What is the specified list?
                                        //TODO: Check DB nemeclature - FLUX_VESSEL_REPORT_TYPE if Vesselreport.FLUXReportDocument.TypeCode.Value exist
                                        //FLUX_VESSEL_REPORT_TYPE:
                                        //SUB
                                        //SUB - VCD
                                        //SUB - VED
                                        //SNAP - F
                                        //SNAP - L
                                        //SNAP - U
                                        if (Vesselreport.FLUXReportDocument.TypeCode.Value != null)
                                        {
                                            //
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0008 - rejected
                                        }
                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0009 - rejected
                                    }

                                    //VESSEL-L00-00-0006
                                    //FLUX_Report Document/Creation
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.CreationDateTime != null)
                                    {
                                        //VESSEL-L00-00-0007
                                        //FLUX_Report Document/Creation
                                        //Datetime format

                                        //#Q How to check format? Do we use TryFormat?
                                        //The check is useless. Original received value is in string format, which is converted automatically to Datetime type.
                                        //The rule states that we have to check string datetime value.
                                        if (Vesselreport.FLUXReportDocument.CreationDateTime != null)
                                        {

                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0007 - rejected
                                        }

                                        //VESSEL-L00-00-0093
                                        //FLUX_Report Document/Creation
                                        //Creation date not in the future
                                        DateTime currentUtcDateTime = DateTime.UtcNow;
                                        if (Vesselreport.FLUXReportDocument.CreationDateTime.Item <= currentUtcDateTime)
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0093 - rejected
                                        }
                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0006 - rejected
                                    }

                                    //VESSEL-L00-00-0011
                                    //FLUX_Report Document/Purpose
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.PurposeCode != null)
                                    {
                                        //VESSEL-L00-00-0046
                                        //FLUX_Report Document/Purpose
                                        //ListId= FLUX_GP_PURPOSE
                                        if (Vesselreport.FLUXReportDocument.PurposeCode.listID.ToString() == "FLUX_GP_PURPOSE")
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0046 - rejected
                                        }

                                        //VESSEL-L00-00-0010
                                        //FLUX_Report Document/Purpose
                                        //Value=9
                                        if (Vesselreport.FLUXReportDocument.PurposeCode.Value.ToString() == "9")
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0010 - rejected
                                        }
                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0011 - rejected
                                    }

                                    //VESSEL-L00-00-0013
                                    //FLUX_Party/Identification
                                    //Mandatory
                                    if (Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID != null)
                                    {
                                        //VESSEL-L00-00-0047
                                        //FLUX_Party/Identification
                                        //SchemeId= FLUX_GP_PARTY 
                                        foreach (var ownerFluxPartyId in Vesselreport.FLUXReportDocument.OwnerFLUXParty.ID)
                                        {
                                            if (ownerFluxPartyId.schemeID.ToString() == "FLUX_GP_PARTY")
                                            {
                                            }
                                            else
                                            {
                                                //VESSEL-L00-00-0047 - rejected
                                            }

                                            //VESSEL-L00-00-0012
                                            //FLUX_Party/Identification
                                            //Code from the specified list
                                            //#Q What is the specified list?
                                            //TODO: Check db nomenclature FLUX_GP_PARTY
                                            if (ownerFluxPartyId.Value.ToString() != "")
                                            {

                                            }
                                            else
                                            {
                                                //VESSEL-L00-00-0012 - rejected
                                            }
                                        }

                                    }
                                    else
                                    {
                                        //VESSEL-L00-00-0013 - rejected
                                    }

                                    //VESSEL-L00-00-0014

                                    //VESSEL-L00-00-0015
                                    //VESSEL-L00-00-0016
                                    //VESSEL-L00-00-0017
                                    //VESSEL-L00-00-0018
                                    //VESSEL-L00-00-0019
                                    //VESSEL-L00-00-0147
                                    //VESSEL-L00-00-0020
                                    //VESSEL-L00-00-0021
                                    //VESSEL-L00-00-0023
                                    //VESSEL-L00-00-0024

                                }

                                //#Q What is the specified list?
                                // Nomenclatures in db
                                //var specifiedList = new string[] { "specifiedVal" };

                                if (Vesselreport.VesselEvent != null)
                                {
                                    //#Q The VesselEvent is [] - should validate for every VesselEvent?
                                    // yes
                                    foreach (var vesselEvent in Vesselreport.VesselEvent)
                                    {
                                        //VESSEL-L00-00-0048
                                        //Vessel_Event/Type
                                        //ListId=VESSEL_EVENT
                                        //#Q Added TypeCode check if present to prevent listID exception if no TypeCode
                                        //yes                                       
                                        if (vesselEvent.TypeCode != null && vesselEvent.TypeCode.listID.ToString() == "VESSEL_EVENT")
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0048 - rejected
                                        }

                                        //VESSEL-L00-00-0068
                                        //Vessel_Event/Occurrence
                                        //Datetime format
                                        //#Q Added OccurrenceDateTime check if present to prevent Item exception if no OccurrenceDateTime
                                        //yes
                                        //#Q How to check DateTime format, with TryFormat?
                                        // useless, if Item exist the rule is satisfied
                                        if (vesselEvent.OccurrenceDateTime != null )
                                        {
                                        }
                                        else
                                        {
                                            //VESSEL-L00-00-0068 - rejected
                                        }

                                        if (vesselEvent.RelatedVesselTransportMeans != null)
                                        {
                                            foreach (var transportMeansId in vesselEvent.RelatedVesselTransportMeans.ID)
                                            {
                                                //#Q FOR EVERY OCCURANCE OF ID IN vesselEvent.RelatedVesselTransportMeans
                                                //yes

                                                //VESSEL-L00-00-0027
                                                //Vessel_Transport_Means/Identification
                                                //SchemeId=Code from the FLUX_VESSEL_ID_TYPE list

                                                var fluxVesselIdTypeList = new string[] { "REG_NBR", "CFR", "EXT_MARK", "example1", "example2" }; //#Q What is the FLUX_VESSEL_ID_TYPE list?

                                                bool isSchemeIdInFluxVesselIdTypeList = fluxVesselIdTypeList.Any(f => transportMeansId.schemeID.Contains(f));
                                                if (isSchemeIdInFluxVesselIdTypeList)
                                                {
                                                }
                                                else
                                                {
                                                    //VESSEL-L00-00-0027 - rejected
                                                }
                                            }

                                            foreach (var relatedVesselTransportMeansTypeCode in vesselEvent.RelatedVesselTransportMeans.TypeCode)
                                            {
                                                //VESSEL-L00-00-0146
                                                //Vessel_ Transport_ Means / Type
                                                //Mandatory value
                                                if (relatedVesselTransportMeansTypeCode != null)
                                                {
                                                    //VESSEL-L00-00-0050
                                                    //Vessel_ Transport_ Means / Type
                                                    //ListId= VESSEL_TYPE
                                                    if (relatedVesselTransportMeansTypeCode.listID.ToString() == "VESSEL_TYPE")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //VESSEL-L00-00-0050 - rejected
                                                    }

                                                    //VESSEL-L00-00-0025
                                                    //Vessel_ Transport_ Means / Type
                                                    //Code from the specified list

                                                    //#Q What is the specified list?
                                                    //TODO: check in db VESSEL_TYPE
                                                    if (relatedVesselTransportMeansTypeCode.Value != null)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //VESSEL-L00-00-0025 - rejected
                                                    }
                                                }
                                                else
                                                {
                                                    //VESSEL-L00-00-0146 - rejected
                                                }
                                            }

                                            //VESSEL-L00-00-0145
                                            //Vessel_Country/Identification
                                            //SchemeId=TERRITORY
                                            if (vesselEvent.RelatedVesselTransportMeans.RegistrationVesselCountry.ID.Value.ToString() == "TERRITORY")
                                            {

                                            }
                                            else
                                            {
                                                //VESSEL-L00-00-0145 - rejected
                                            }

                                            foreach (var relatedVesselTransportMeansSpecifiedRegistrationEvent in vesselEvent.RelatedVesselTransportMeans.SpecifiedRegistrationEvent)
                                            {
                                                //VESSEL-L00-00-0051
                                                //Registration_ Location /Type
                                                //ListId= FLUX_VESSEL_REGSTR_TYPE
                                                if (relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.TypeCode.listID.ToString() == "FLUX_VESSEL_REGSTR_TYPE")
                                                {

                                                }
                                                else
                                                {
                                                    //VESSEL-L00-00-0051 - rejected
                                                }

                                                //VESSEL-L00-00-0026
                                                //Registration_ Location /Type
                                                //Code from the specified list
                                                //TODO: check db - FLUX_VESSEL_REGSTR_TYPE

                                                //var isValueInSpecifiedList = specifiedList.Any(f => relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.TypeCode.Value.Contains(f));
                                                //if (isValueInSpecifiedList)
                                                //{
                                                //}
                                                //else
                                                //{
                                                //    //VESSEL-L00-00-0026 - rejected
                                                //}

                                                foreach (var relatedConstructionLocationId in relatedVesselTransportMeansSpecifiedRegistrationEvent.RelatedRegistrationLocation.ID)
                                                {
                                                    //VESSEL-L00-00-0053
                                                    //Registration_Location/Identification
                                                    //SchemeId= VESSEL_PORT
                                                    if (relatedConstructionLocationId.schemeID.ToString() != "VESSEL_PORT")
                                                    {

                                                    }
                                                    else
                                                    {
                                                        //VESSEL-L00-00-0053 - rejected
                                                    }
                                                }

                                            }

                                            //VESSEL-L00-00-0074
                                            //Construction_Event/Occurrence
                                            //Date type

                                            //#Q How to validate date format type?
                                            // if OccurrenceDateTime != null - the rule is satisfied
                                            
                                            if (vesselEvent.RelatedVesselTransportMeans.SpecifiedConstructionEvent.OccurrenceDateTime!= null)
                                            {

                                            }
                                            else
                                            {
                                                //VESSEL-L00-00-0074 - rejected
                                            }
                  

                                            foreach (var relatedVesselTransportMeansAttachedVesselEngine in vesselEvent.RelatedVesselTransportMeans.AttachedVesselEngine)
                                            {
                                                //VESSEL-L00-00-0056
                                                //Vessel_ Engine /Role
                                                //ListId= FLUX_VESSEL_ENGINE_ROLE
                                                if (relatedVesselTransportMeansAttachedVesselEngine.RoleCode.listID.ToString() == "FLUX_VESSEL_ENGINE_ROLE")
                                                {

                                                }
                                                else
                                                {
                                                    //VESSEL-L00-00-0056 - rejected
                                                }

                                                //VESSEL-L00-00-0033
                                                //Vessel_ Engine /Role
                                                //Code from the specified list
                                                //TODO: check db - FLUX_VESSEL_ENGINE_ROLE

                                                //var isValueInSpecifiedList = specifiedList.Any(f => relatedVesselTransportMeansAttachedVesselEngine.RoleCode.Value.Contains(f));
                                                //if (isValueInSpecifiedList)
                                                //{

                                                //}
                                                //else
                                                //{
                                                //    //VESSEL-L00-00-0033 - rejected
                                                //}

                                                //VESSEL-L00-00-0041
                                                //Vessel_Engine /Power
                                                //The unit must be KWT
                                                var powerMeasureUnitCode = relatedVesselTransportMeansAttachedVesselEngine.PowerMeasure.First(x => x.unitCode == "KWT");
                                                //VESSEL-L00-00-0075
                                                //Vessel_Engine /Power
                                                //Numerical value
                                                int numericValue;
                                                var isPowerMeasureNumeric = relatedVesselTransportMeansAttachedVesselEngine.PowerMeasure.First(x => int.TryParse(x.Value.ToString(), out numericValue));
                                                if (powerMeasureUnitCode != null && isPowerMeasureNumeric != null)
                                                {

                                                }
                                                else
                                                {
                                                    //VESSEL-L00-00-0041 - rejected
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                    }
                } 
            }
