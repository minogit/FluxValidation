using ScortelApi.Models.FLUX;
using ScortelApi.Models.ScortelELB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScortelApi.ISSN
{
    /// <summary>
    /// FLUX Gear characteristics handler
    /// Used in creation Reports in FADomain
    /// </summary>
    public static class GearCh
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requiredpars"></param>
        /// <param name="fg"></param>
        /// <param name="certnum"></param>
        /// <returns></returns>
        public static List<GearCharacteristicType> GetGearCharsRequired(out string requiredpars, FGRec fg, string certnum, Dictionary<CertFG, string> certfglist)
        {
            string code = fg.FGDesc.FGCode;
            requiredpars = "";
            List<CertFG> listfg = new List<CertFG>();
            CertFG foundCertFG = null;
            foreach (var item in certfglist)
            {
                if (item.Value == certnum)
                {
                    if (item.Key.FGDesc.FGCode == code)
                    {
                        if (item.Key.GearEyeLite == fg.GearEyeLite)
                        {
                            foundCertFG = item.Key;
                            break;
                            #region Commented
                            //if (item.Key.GearHeightLite == null)
                            //{
                            //    if (fg.GearHeightLite == 0)
                            //    {
                            //        foundCertFG = item.Key;
                            //        if (item.Key.GearLengthLite == null)
                            //        {
                            //            if (fg.GearLengthLite == 0)
                            //            {
                            //                foundCertFG = item.Key;
                            //                break;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            if (item.Key.GearLengthLite == fg.GearLengthLite)
                            //            {
                            //                foundCertFG = item.Key;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (item.Key.GearHeightLite == fg.GearHeightLite)
                            //    {
                            //        foundCertFG = item.Key;
                            //        if (item.Key.GearLengthLite == null)
                            //        {
                            //            if (fg.GearLengthLite == 0)
                            //            {
                            //                foundCertFG = item.Key;
                            //                break;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            if (item.Key.GearLengthLite == fg.GearLengthLite)
                            //            {
                            //                foundCertFG = item.Key;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}
                            #endregion
                        }
                    }
                }
            }
             
            List<GearCharacteristicType> gearchars = new List<GearCharacteristicType>();
            GearCharacteristicType chartype;
            GearCharacteristicType certchar;
            switch (code)
            {
                #region Trawl nets
                case "TBB":
                case "PUK":
                    #region TBB
                    requiredpars = "ME;GM;GN;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearLengthLite = 0;
                        decimal tmpGearLengthLiteDec = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec =  foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite = 0;
                                tmpGearLengthLiteDec = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite = 0;
                            tmpGearLengthLiteDec = 0;
                        }
                        //chartype.ValueMeasure.Value = tmpGearLengthLite;
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec;


                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumericCount = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount = 0;
                            }

                        }
                        catch (Exception)
                        {
                            tmpNumericCount = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearLengthLite = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite;


                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumericCount = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                     
                    #endregion
                    return gearchars;                   
                case "OTB":
                case "OT":
                case "OTP":
                case "PTB":
                case "PT":
                case "TB":
                case "TBN":
                case "TBS":
                case "PUL":
                    #region OTB
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    // requiredpars = "ME;GM;MT";
                    requiredpars = "ME;GM;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite1 = 0;
                        decimal tmpGearLengthLiteDec1 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite1 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec1 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite1 = 0;
                                tmpGearLengthLiteDec1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite1 = 0;
                            tmpGearLengthLiteDec1 = 0;
                        }
                        //chartype.ValueMeasure.Value = tmpGearLengthLite1;
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented

                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }

                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite1 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite1 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;                   
                case "OTT":
                    #region OTT
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;GM;GN;MT";
                    requiredpars = "ME;GM;GN;GD";
                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite2 = 0;
                        decimal tmpGearLengthLiteDec2 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite2 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec2 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite2 = 0;
                                tmpGearLengthLiteDec2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite2 = 0;
                            tmpGearLengthLiteDec2 = 0;
                        }
                        //chartype.ValueMeasure.Value = tmpGearLengthLite2;
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount1 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount1 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount1 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }

                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite2 = 0;
                        
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite2 = (int)fg.GearLengthLite;
                               
                            }
                            else
                            {
                                tmpGearLengthLite2 = 0;
                               
                            }
                        }
                        catch (Exception)
                        {
                             tmpGearLengthLite2 = 0;
                            
                        }
                         chartype.ValueMeasure.Value = tmpGearLengthLite2;
                       
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount1 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount1 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount1 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;                
                case "OTM":                
                case "PTM":                    
                case "TM":                     
                case "TMS":                     
                case "TSP":
                    #region OTM
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;MT";
                    requiredpars = "ME;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                    
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }

                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";

                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;

                case "TX":
                    #region TX
                    requiredpars = "ME;GD";
                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Seine nets
                case "SDN":
                case "SSC":
                case "SPR":
                case "SX":
                case "SV":
                case "SB":
                    #region SDN
                    requiredpars = "ME;GM;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite3 = 0;
                        decimal tmpGearLengthLiteDec3 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite3 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec3 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite3 = 0;
                                tmpGearLengthLiteDec3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite3 = 0;
                            tmpGearLengthLiteDec3 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec3; //tmpGearLengthLite3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite3 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite3 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite3 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;                 
                #endregion

                #region Surrounding nets
                case "PS":
                case "PS1":
                case "PS2":
                case "LA":
                case "SUX":
                    #region PS
                    requiredpars = "ME;GM;HE;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite4 = 0;
                        decimal tmpGearLengthLiteDec4 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite4 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec4 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite4 = 0;
                                tmpGearLengthLiteDec4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite4 = 0;
                            tmpGearLengthLiteDec4 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec4;//tmpGearLengthLite4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearHeightLite = 0;
                        decimal tmpGearHeightLiteDec = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec = foundCertFG.GearHeightDecimal;
                                    
                            }
                            else
                            {
                                //tmpGearHeightLite = 0;
                                tmpGearHeightLiteDec = 0; 
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite = 0;
                            tmpGearHeightLiteDec = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec; // tmpGearHeightLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite4 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite4 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite4 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearHeightLite = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;                                 
                #endregion

                #region Dredges
                case "DRB":
                    #region Dredges
                    requiredpars = "GM;GN;GD";
                    if (foundCertFG != null)
                    {
                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearHeightLite1 = 0;
                        decimal tmpGearHeightLiteDec1 = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite1 = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec1 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearHeightLite1 = 0;
                                tmpGearHeightLiteDec1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite1 = 0;
                            tmpGearHeightLiteDec1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec1;// tmpGearHeightLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount2 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount2 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount2 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearHeightLite1 = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite1 = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount2 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount2 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount2 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Gillnets & entangling nets
                case "GN":                
                case "GNS":                    
                case "GTN":                     
                case "GTR":                   
                case "GNF":                    
                case "GEN":
                    #region Gilnets
                    if (foundCertFG != null)
                    {
                        #region Gilnets and entangling nets
                        requiredpars = "ME;GM;HE;NL;NN;QG;GD";

                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearLengthLite5 = 0;
                        decimal tmpGearLengthLiteDec5 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite5 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec5 =  foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite5 = 0;
                                tmpGearLengthLiteDec5 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite5 = 0;
                            tmpGearLengthLiteDec5 = 0;
                        }
                        //chartype.ValueMeasure.Value = tmpGearLengthLite5;
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec5;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearHeightLite2 = 0;
                        decimal tmpGearHeightLiteDec2 = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite2 = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec2 =  foundCertFG.GearHeightDecimal;
                            }
                            else
                            {
                                //tmpGearHeightLite2 = 0;
                                tmpGearHeightLiteDec2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite2 = 0;
                            tmpGearHeightLiteDec2 = 0;
                        }
                        //chartype.ValueMeasure.Value = tmpGearHeightLite2;
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec2;

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Nominal Length  - NL
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NL"
                            }
                        };
                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MTR";
                        // FG field - not implemented - Nominal length of one net in the fleet
                        decimal tmpNumberOfLines1 = 0;
                        try
                        {
                            if (foundCertFG.NetNominalLengthLite != null)
                            {
                                //tmpNumberOfLines1 = (int)foundCertFG.NetNominalLengthLite;
                                tmpNumberOfLines1 = foundCertFG.NetNominalLengthLite;
                            }
                            else
                            {
                                tmpNumberOfLines1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpNumberOfLines1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Number of nets in the fleet  - NN
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NN"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG not implemented - no field - Number of the nets in the fleet
                        int tmpNetsCountInFleetLite = 0;
                        try
                        {
                            if (foundCertFG.NetsCountInFleetLite != null)
                            {
                                tmpNetsCountInFleetLite = (int)foundCertFG.NetsCountInFleetLite;
                            }
                            else
                            {
                                tmpNetsCountInFleetLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNetsCountInFleetLite = 0;
                        }

                        chartype.ValueQuantity.Value = tmpNetsCountInFleetLite;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear qunatity of gear - QG
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "QG"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG - not implemented - quantity of gear onboard
                        chartype.ValueQuantity.Value = foundCertFG.CountLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region Gilnets and entangling nets
                        requiredpars = "ME;GM;HE;NL;NN;QG;GD";

                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearLengthLite5 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite5 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite5 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite5 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite5;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearHeightLite2 = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite2 = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite2 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite2;

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear nominal length  - NL
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NL"
                            }
                        };
                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MTR";
                        // FG field - not implemented - Nominal length of one net in the fleet
                        int tmpNumberOfLines1 = 0;
                        try
                        {
                            if (fg.NetNominalLengthLite != null)
                            {
                                tmpNumberOfLines1 = (int)fg.NetNominalLengthLite;
                            }
                            else
                            {
                                tmpNumberOfLines1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpNumberOfLines1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Number of nets in the fleet  - NN
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NN"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG not implemented - no field - Number of the nets in the fleet
                        int tmpNetsCountInFleetLite = 0;
                        try
                        {
                            if (fg.NetsCountInFleetLite != null)
                            {
                                tmpNetsCountInFleetLite = (int)fg.NetsCountInFleetLite;
                            }
                            else
                            {
                                tmpNetsCountInFleetLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNetsCountInFleetLite = 0;
                        }

                        chartype.ValueQuantity.Value = tmpNetsCountInFleetLite;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear qunatity of gear - QG
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "QG"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG - not implemented - quantity of gear onboard
                        chartype.ValueQuantity.Value = fg.CountLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion

                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion
                #region Hook & lines
                case "LHP":                    
                case "LHM":                   
                case "LLS":                    
                case "LLD":                    
                case "LL":
                    #region Hook and lines
                    requiredpars = "GN;NI;GD";

                    if (foundCertFG != null)
                    {
                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount4 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount4 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount4 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Number of lines - NI
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "NI"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumberOfLines = 0;
                        try
                        {
                            if (foundCertFG.LinesCountLite != null)
                            {
                                tmpNumberOfLines = (int)foundCertFG.LinesCountLite;
                            }
                            else
                            {
                                tmpNumberOfLines = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumberOfLines;

                        gearchars.Add(chartype);

                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount4 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount4 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount4 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Number of lines - NI
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "NI"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumberOfLines = 0;
                        try
                        {
                            if (fg.LinesCountLite != null)
                            {
                                tmpNumberOfLines = (int)fg.LinesCountLite;
                            }
                            else
                            {
                                tmpNumberOfLines = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumberOfLines;

                        gearchars.Add(chartype);

                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion

                    return gearchars;

                #endregion
                default:
                    return null;
            }
        }

        public static List<GearCharacteristicType> GetGearCharsRequiredLand(out string requiredpars, FGRec fg, string certnum, List<CertFG> certfglist)
        {
            string code = fg.FGDesc.FGCode;
            requiredpars = "";
            List<GearCharacteristicType> gearchars = new List<GearCharacteristicType>();
            GearCharacteristicType chartype;
            GearCharacteristicType certchar;


            CertFG foundCertFG = null;
            foreach (var item in certfglist)
            {
                 
                if (item.FGDesc.FGCode == code)
                {
                    if (item.GearEyeLite == fg.GearEyeLite)
                    {
                        foundCertFG = item;
                        break;
                    #region Commented
                    //if (item.Key.GearHeightLite == null)
                    //{
                    //    if (fg.GearHeightLite == 0)
                    //    {
                    //        foundCertFG = item.Key;
                    //        if (item.Key.GearLengthLite == null)
                    //        {
                    //            if (fg.GearLengthLite == 0)
                    //            {
                    //                foundCertFG = item.Key;
                    //                break;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (item.Key.GearLengthLite == fg.GearLengthLite)
                    //            {
                    //                foundCertFG = item.Key;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (item.Key.GearHeightLite == fg.GearHeightLite)
                    //    {
                    //        foundCertFG = item.Key;
                    //        if (item.Key.GearLengthLite == null)
                    //        {
                    //            if (fg.GearLengthLite == 0)
                    //            {
                    //                foundCertFG = item.Key;
                    //                break;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (item.Key.GearLengthLite == fg.GearLengthLite)
                    //            {
                    //                foundCertFG = item.Key;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion
                    }
                }
                 
            }

            switch (code)
            {
                #region Trawl nets
                case "TBB":
                case "PUK":
                    #region TBB
                    requiredpars = "ME;GM;GN;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearLengthLite = 0;
                        decimal tmpGearLengthLiteDec = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite = 0;
                                tmpGearLengthLiteDec = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite = 0;
                            tmpGearLengthLiteDec = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec; //tmpGearLengthLite;


                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumericCount = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearLengthLite = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite;


                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumericCount = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                case "OTB":
                case "OT":
                case "OTP":
                case "PTB":
                case "PT":
                case "TB":
                case "TBN":
                case "TBS":
                case "PUL":
                    #region OTB
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    // requiredpars = "ME;GM;MT";
                    requiredpars = "ME;GM;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite1 = 0;
                        decimal tmpGearLengthLiteDec1 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite1 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec1 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite1 = 0;
                                tmpGearLengthLiteDec1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite1 = 0;
                            tmpGearLengthLiteDec1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec1; // tmpGearLengthLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite1 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite1 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                case "OTT":
                    #region OTT
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;GM;GN;MT";
                    requiredpars = "ME;GM;GN;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite2 = 0;
                        decimal tmpGearLengthLiteDec2 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite2 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec2 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite2 = 0;
                                tmpGearLengthLiteDec2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite2 = 0;
                            tmpGearLengthLiteDec2 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec2; // tmpGearLengthLite2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount1 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount1 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount1 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }

                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {


                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite2 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite2 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite2 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount1 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount1 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount1 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                case "OTM":
                case "PTM":
                case "TM":
                case "TMS":
                case "TSP":
                    #region OTM
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;MT";
                    requiredpars = "ME;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        string trmodel = "";
                        if (!String.IsNullOrEmpty(foundCertFG.TrawlModel))
                        {
                            trmodel = foundCertFG.TrawlModel;
                        }

                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = trmodel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Model of trawl - MT - Commented
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "MT"
                            },
                            Value = new TextType()
                            {
                                languageID = "GBR",
                                Value = fg.TrawlModel
                            }
                        };
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;

                case "TX":
                    #region TX
                    requiredpars = "ME;GD";
                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Seine nets
                case "SDN":
                case "SSC":
                case "SPR":
                case "SX":
                case "SV":
                case "SB":
                    #region SDN
                    requiredpars = "ME;GM;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite3 = 0;
                        decimal tmpGearLengthLiteDec3 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite3 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec3 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite3 = 0;
                                tmpGearLengthLiteDec3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite3 = 0;
                            tmpGearLengthLiteDec3 = 0;  
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec3; // tmpGearLengthLite3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite3 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite3 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite3 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Surrounding nets
                case "PS":
                case "PS1":
                case "PS2":
                case "LA":
                case "SUX":
                    #region PS
                    requiredpars = "ME;GM;HE;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearLengthLite4 = 0;
                        decimal tmpGearLengthLiteDec4 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite4 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec4 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite4 = 0;
                                tmpGearLengthLiteDec4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            // tmpGearLengthLite4 = 0;
                            tmpGearLengthLiteDec4 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec4; // tmpGearLengthLite4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearHeightLite = 0;
                        decimal tmpGearHeightLiteDec = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec = foundCertFG.GearHeightDecimal;
                            }
                            else
                            {
                                //tmpGearHeightLite = 0;
                                tmpGearHeightLiteDec = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite = 0;
                            tmpGearHeightLiteDec = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec; //tmpGearHeightLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearLengthLite4 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite4 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite4 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearHeightLite = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Dredges
                case "DRB":
                    #region Dredges
                    requiredpars = "GM;GN;GD";

                    if (foundCertFG != null)
                    {
                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearHeightLite1 = 0;
                        decimal tmpGearHeightLiteDec1 = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite1 = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec1 = foundCertFG.GearHeightDecimal;
                            }
                            else
                            {
                                //tmpGearHeightLite1 = 0;
                                tmpGearHeightLiteDec1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite1 = 0;
                            tmpGearHeightLiteDec1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec1; // tmpGearHeightLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount2 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount2 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount2 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearHeightLite1 = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite1 = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount2 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount2 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount2 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount2;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Gillnets & entangling nets
                case "GN":
                case "GNS":
                case "GTN":
                case "GTR":
                case "GNF":
                case "GEN":
                    #region Gilnets and entangling nets
                    requiredpars = "ME;GM;HE;NL;NN;QG;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        //int tmpGearLengthLite5 = 0;
                        decimal tmpGearLengthLiteDec5 = 0;
                        try
                        {
                            if (foundCertFG.GearLengthLite != null)
                            {
                                //tmpGearLengthLite5 = (int)foundCertFG.GearLengthLite;
                                tmpGearLengthLiteDec5 = foundCertFG.GearLengthDecimal;
                            }
                            else
                            {
                                //tmpGearLengthLite5 = 0;
                                tmpGearLengthLiteDec5 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearLengthLite5 = 0;
                            tmpGearLengthLiteDec5 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLiteDec5; // tmpGearLengthLite5;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        //int tmpGearHeightLite2 = 0;
                        decimal tmpGearHeightLiteDec2 = 0;
                        try
                        {
                            if (foundCertFG.GearHeightLite != null)
                            {
                                //tmpGearHeightLite2 = (int)foundCertFG.GearHeightLite;
                                tmpGearHeightLiteDec2 = foundCertFG.GearHeightDecimal;
                            }
                            else
                            {
                                //tmpGearHeightLite2 = 0;
                                tmpGearHeightLiteDec2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpGearHeightLite2 = 0;
                            tmpGearHeightLiteDec2 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLiteDec2;//tmpGearHeightLite2;

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Nominal Length  - NL
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NL"
                            }
                        };
                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MTR";
                        // FG field - not implemented - Nominal length of one net in the fleet
                        //int tmpNumberOfLines1 = 0;
                        decimal tmpNumberOfLinesDec1 = 0;
                        try
                        {
                            if (foundCertFG.NetNominalLengthLite != null)
                            {
                                //tmpNumberOfLines1 = (int)foundCertFG.NetNominalLengthLite;
                                tmpNumberOfLinesDec1 = foundCertFG.NetNominalLengthLite;
                            }
                            else
                            {
                                //tmpNumberOfLines1 = 0;
                                tmpNumberOfLinesDec1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            //tmpNumberOfLines1 = 0;
                            tmpNumberOfLinesDec1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpNumberOfLinesDec1; // tmpNumberOfLines1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Number of nets in the fleet  - NN
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NN"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG not implemented - no field - Number of the nets in the fleet
                        int tmpNetsCountInFleetLite = 0;
                        try
                        {
                            if (foundCertFG.NetsCountInFleetLite != null)
                            {
                                tmpNetsCountInFleetLite = (int)foundCertFG.NetsCountInFleetLite;
                            }
                            else
                            {
                                tmpNetsCountInFleetLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNetsCountInFleetLite = 0;
                        }

                        chartype.ValueQuantity.Value = tmpNetsCountInFleetLite;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear qunatity of gear - QG
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "QG"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG - not implemented - quantity of gear onboard
                        chartype.ValueQuantity.Value = foundCertFG.CountLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions length/ width - GM
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GM"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";

                        int tmpGearLengthLite5 = 0;
                        try
                        {
                            if (fg.GearLengthLite != null)
                            {
                                tmpGearLengthLite5 = (int)fg.GearLengthLite;
                            }
                            else
                            {
                                tmpGearLengthLite5 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearLengthLite5 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearLengthLite5;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions height - HE
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "HE"
                        };
                        chartype.ValueMeasure = new MeasureType();

                        chartype.ValueMeasure.unitCode = "MTR";
                        int tmpGearHeightLite2 = 0;
                        try
                        {
                            if (fg.GearHeightLite != null)
                            {
                                tmpGearHeightLite2 = (int)fg.GearHeightLite;
                            }
                            else
                            {
                                tmpGearHeightLite2 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpGearHeightLite2 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpGearHeightLite2;

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Nominal length - NL
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NL"
                            }
                        };
                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MTR";
                        // FG field - not implemented - Nominal length of one net in the fleet
                        int tmpNumberOfLines1 = 0;
                        try
                        {
                            if (fg.NetNominalLengthLite != null)
                            {
                                tmpNumberOfLines1 = (int)fg.NetNominalLengthLite;
                            }
                            else
                            {
                                tmpNumberOfLines1 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines1 = 0;
                        }
                        chartype.ValueMeasure.Value = tmpNumberOfLines1;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear Number of nets in the fleet  - NN
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "NN"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG not implemented - no field - Number of the nets in the fleet
                        int tmpNetsCountInFleetLite = 0;
                        try
                        {
                            if (fg.NetsCountInFleetLite != null)
                            {
                                tmpNetsCountInFleetLite = (int)fg.NetsCountInFleetLite;
                            }
                            else
                            {
                                tmpNetsCountInFleetLite = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNetsCountInFleetLite = 0;
                        }

                        chartype.ValueQuantity.Value = tmpNetsCountInFleetLite;
                        gearchars.Add(chartype);
                        #endregion

                        #region Gear qunatity of gear - QG
                        chartype = new GearCharacteristicType()
                        {
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "QG"
                            }
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        // FG - not implemented - quantity of gear onboard
                        chartype.ValueQuantity.Value = fg.CountLite;

                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Traps
                case "FPO":
                    #region Traps
                    requiredpars = "ME;GN;GD";

                    if (foundCertFG != null)
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (foundCertFG.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = foundCertFG.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount3 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount3 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount3 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Mesh size EYE - ME
                        chartype = new GearCharacteristicType();
                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "ME"
                        };

                        chartype.ValueMeasure = new MeasureType();
                        chartype.ValueMeasure.unitCode = "MMT";
                        if (fg.GearEyeLite > 0)
                        {
                            //chartype.ValueMeasure.Value = fg.GearEyeLite * 10;
                            chartype.ValueMeasure.Value = fg.GearEyeLite;
                        }
                        else
                        {
                            chartype.ValueMeasure.Value = 0;
                        }

                        gearchars.Add(chartype);
                        #endregion

                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount3 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount3 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount3 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount3 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount3;
                        gearchars.Add(chartype);
                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion
                    return gearchars;
                #endregion

                #region Hook & lines
                case "LHP":
                case "LHM":
                case "LLS":
                case "LLD":
                case "LL":
                    #region Hook and lines
                    requiredpars = "GN;NI;GD";

                    if (foundCertFG != null)
                    {
                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount4 = 0;
                        try
                        {
                            if (foundCertFG.NumericCountLite != null)
                            {
                                tmpNumericCount4 = (int)foundCertFG.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount4 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Number of lines - NI
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "NI"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumberOfLines = 0;
                        try
                        {
                            if (foundCertFG.LinesCountLite != null)
                            {
                                tmpNumberOfLines = (int)foundCertFG.LinesCountLite;
                            }
                            else
                            {
                                tmpNumberOfLines = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumberOfLines;

                        gearchars.Add(chartype);

                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    else
                    {
                        #region Gear dimensions by number - GN
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "GN"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";
                        int tmpNumericCount4 = 0;
                        try
                        {
                            if (fg.NumericCountLite != null)
                            {
                                tmpNumericCount4 = (int)fg.NumericCountLite;
                            }
                            else
                            {
                                tmpNumericCount4 = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumericCount4 = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumericCount4;
                        gearchars.Add(chartype);
                        #endregion

                        #region Number of lines - NI
                        chartype = new GearCharacteristicType();

                        chartype.TypeCode = new CodeType()
                        {
                            listID = "FA_GEAR_CHARACTERISTIC",
                            Value = "NI"
                        };
                        chartype.ValueQuantity = new QuantityType();
                        chartype.ValueQuantity.unitCode = "C62";

                        int tmpNumberOfLines = 0;
                        try
                        {
                            if (fg.LinesCountLite != null)
                            {
                                tmpNumberOfLines = (int)fg.LinesCountLite;
                            }
                            else
                            {
                                tmpNumberOfLines = 0;
                            }
                        }
                        catch (Exception)
                        {
                            tmpNumberOfLines = 0;
                        }
                        chartype.ValueQuantity.Value = tmpNumberOfLines;

                        gearchars.Add(chartype);

                        #endregion

                        #region Certnumber between FVMS and ISS replace GD (Gear description) with certificate number
                        certchar = new GearCharacteristicType()
                        {
                            // DB Nomenclature - FLUX_FA.MDR_FA_Gear_Characteristic
                            TypeCode = new CodeType()
                            {
                                listID = "FA_GEAR_CHARACTERISTIC",
                                Value = "GD"
                            },
                            Value = new TextType()
                            {
                                Value = certnum,
                                languageID = "BGR"
                            }
                        };
                        gearchars.Add(certchar);
                        #endregion
                    }
                    #endregion

                    return gearchars;

                #endregion
                default:
                    return null;
            }
        }

        public static void GetGearCharsRequiredValidation(out string requiredpars, string code)
        {
            
            requiredpars = "";
            List<CertFG> listfg = new List<CertFG>();
            CertFG foundCertFG = null;
          

            List<GearCharacteristicType> gearchars = new List<GearCharacteristicType>();
            GearCharacteristicType chartype;
            GearCharacteristicType certchar;
            switch (code)
            {
                #region Trawl nets
                case "TBB":
                case "PUK":
                    #region TBB
                    requiredpars = "ME;GM;GN;GD";
                    #endregion
                    break;
                 
                case "OTB":
                case "OT":
                case "OTP":
                case "PTB":
                case "PT":
                case "TB":
                case "TBN":
                case "TBS":
                case "PUL":
                    #region OTB
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    // requiredpars = "ME;GM;MT";
                    requiredpars = "ME;GM;GD";
                    #endregion
                    break;
                case "OTT":
                    #region OTT
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;GM;GN;MT";
                    requiredpars = "ME;GM;GN;GD";
                    #endregion
                    break;
                case "OTM":
                case "PTM":
                case "TM":
                case "TMS":
                case "TSP":
                    #region OTM
                    // GM or MT must provided - FLUX FA DOMAIN documentationn
                    //requiredpars = "ME;MT";
                    requiredpars = "ME;GD";
                    #endregion
                    break;
                case "TX":
                    #region TX
                    requiredpars = "ME;GD";

                    #endregion
                    break;
                #endregion

                #region Seine nets
                case "SDN":
                case "SSC":
                case "SPR":
                case "SX":
                case "SV":
                case "SB":
                    #region SDN
                    requiredpars = "ME;GM;GD";

                    #endregion
                    break;
                #endregion

                #region Surrounding nets
                case "PS":
                case "PS1":
                case "PS2":
                case "LA":
                case "SUX":
                    #region PS
                    requiredpars = "ME;GM;HE;GD";

                    #endregion
                    break;
                #endregion

                #region Dredges
                case "DRB":
                    #region Dredges
                    requiredpars = "GM;GN;GD";

                    #endregion
                    break;
                #endregion

                #region Gillnets & entangling nets
                case "GN":
                case "GNS":
                case "GTN":
                case "GTR":
                case "GNF":
                case "GEN":
                    #region Gilnets
                    requiredpars = "ME;GM;HE;NL;NN;QG;GD";

                    #endregion
                    break;
                #endregion
                #region Hook & lines
                case "LHP":
                case "LHM":
                case "LLS":
                case "LLD":
                case "LL":
                    #region Hook and lines
                    requiredpars = "GN;NI;GD";

                    #endregion
                    break;
                #endregion
                default:
                    break;
            }
        }
    }
}
