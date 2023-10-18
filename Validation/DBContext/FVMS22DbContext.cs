using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ScortelApi.Models.FLUX;
using ScortelApi.Models.FLUX.Noms;

namespace Validation.DBContext
{

    namespace MyConsoleApp.Models
    {
          
        public partial class FVMS22DbContext : DbContext
        {
            public FVMS22DbContext(DbContextOptions<FVMS22DbContext> options)
                    : base(options) { }
            #region FLUX Nomenclatures

            #region Common Nomenclatures
            /// <summary>
            /// FA, Vessel
            /// </summary>
            public virtual DbSet<MDR_Boolean_Type> MDR_Boolean_Type { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Conversion_Factor> MDR_Conversion_Factor { get; set; }
            /// <summary>
            /// ACDR, FA, Sales
            /// </summary>
            public virtual DbSet<MDR_FAO_Fishing_Area> MDR_FAO_Fishing_Area { get; set; }
            /// <summary>
            /// ACDR, FA, Sales, FLAP
            /// </summary>
            public virtual DbSet<MDR_FAO_species> MDR_FAO_species { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Fish_Freshness> MDR_Fish_Freshness { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Fish_Presentation> MDR_Fish_Presentation { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>        
            public virtual DbSet<MDR_Fish_Preservation> MDR_Fish_Preservation { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Fish_Size_Class> MDR_Fish_Size_Class { get; set; }
            /// <summary>
            /// FA, Sales, Vessel, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_Contact_Role> MDR_FLUX_Contact_Role { get; set; }
            /// <summary>
            /// FA, Sales, MDM
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Message_Id> MDR_FLUX_GP_Message_Id { get; set; }
            /// <summary>
            /// ACDR, FA, MDM, Sales, Vessel, VMS, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Party> MDR_FLUX_GP_Party { get; set; }
            /// <summary>
            /// ACDR, FA, Sales, Vessel, VMS, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Purpose> MDR_FLUX_GP_Purpose { get; set; }
            /// <summary>
            /// ACDR, FA, MDM, Sales, Vessel, VMS, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Response> MDR_FLUX_GP_Response { get; set; }
            /// <summary>
            /// ACDR, FA, MDM, Sales, Vessel, VMS, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Val_Level> MDR_FLUX_GP_Val_Level { get; set; }
            /// <summary>
            /// ACDR, FA, MDM, Sales, Vessel, VMS, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_GP_Val_Type> MDR_FLUX_GP_Val_Type { get; set; }
            /// <summary>
            /// FA, Sales, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_Location_Type> MDR_FLUX_Location_Type { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_FLUX_Process_Type> MDR_FLUX_Process_Type { get; set; }
            /// <summary>
            /// Vessel, FLAP
            /// </summary>
            public virtual DbSet<MDR_FLUX_Telecom_Use> MDR_FLUX_Telecom_Use { get; set; }
            /// <summary>
            /// FA, Sales, Vessel
            /// </summary>
            public virtual DbSet<MDR_FLUX_Unit> MDR_FLUX_Unit { get; set; }
            /// <summary>
            /// FA, Vessel, VMS
            /// </summary>
            public virtual DbSet<MDR_FLUX_Vessel_Id_Type> MDR_FLUX_Vessel_Id_Type { get; set; }
            /// <summary>
            /// ACDR, FA, FLAP, Vessel
            /// </summary>
            public virtual DbSet<MDR_Gear_Type> MDR_Gear_Type { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Gender> MDR_Gender { get; set; }
            /// <summary>
            /// FA, Sales, FLAP
            /// </summary>
            public virtual DbSet<MDR_Location> MDR_Location { get; set; }
            /// <summary>
            /// FA, Vessel
            /// </summary>
            public virtual DbSet<MDR_Member_State> MDR_Member_State { get; set; }
            /// <summary>
            /// FA, Vessel
            /// </summary>
            public virtual DbSet<MDR_RFMOs> MDR_RFMOs { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Stat_Rect> MDR_Stat_Rect { get; set; }
            /// <summary>
            /// ACDR, FA, FLAP, Sales, Vessel, VMS
            /// </summary>
            public virtual DbSet<MDR_Territory> MDR_Territory { get; set; }
            /// <summary>
            /// FA, Vessel
            /// </summary>
            public virtual DbSet<MDR_Vessel_Activity> MDR_Vessel_Activity { get; set; }
            /// <summary>
            /// Flap, Vessel
            /// </summary>
            public virtual DbSet<MDR_Vessel_Type> MDR_Vessel_Type { get; set; }
            /// <summary>
            /// FA, Sales
            /// </summary>
            public virtual DbSet<MDR_Weight_Measure_Type> MDR_Weight_Measure_Type { get; set; }
            #endregion

            #region ACDR
            public virtual DbSet<ACDR_MDR_ACDR_Catch_Status> ACDR_MDR_ACDR_Catch_Status { get; set; }
            public virtual DbSet<ACDR_MDR_CR_Fishing_Category> ACDR_MDR_CR_Fishing_Category { get; set; }
            public virtual DbSet<ACDR_MDR_CR_Land_Indicator> ACDR_MDR_CR_Land_Indicator { get; set; }
            public virtual DbSet<ACDR_MDR_CR_Report_Type> ACDR_MDR_CR_Report_Type { get; set; }
            public virtual DbSet<ACDR_MDR_CR_Sov_Waters> ACDR_MDR_CR_Sov_Waters { get; set; }
            public virtual DbSet<ACDR_MDR_CR_Unit> ACDR_MDR_CR_Unit { get; set; }
            public virtual DbSet<ACDR_MDR_Quota_Location> ACDR_MDR_Quota_Location { get; set; }
            public virtual DbSet<ACDR_MDR_Quota_Object> ACDR_MDR_Quota_Object { get; set; }
            #endregion

            #region FA
            public virtual DbSet<FA_MDR_Effort_Zone> FA_MDR_Effort_Zone { get; set; }
            public virtual DbSet<FA_MDR_FA_Bait_Type> FA_MDR_FA_Bait_Type { get; set; }
            public virtual DbSet<FA_MDR_FA_BFT_Size_Category> FA_MDR_FA_BFT_Size_Category { get; set; }
            public virtual DbSet<FA_MDR_FA_BR_Def> FA_MDR_FA_BR_Def { get; set; }
            public virtual DbSet<FA_MDR_FA_BR_EU> FA_MDR_FA_BR_EU { get; set; }
            public virtual DbSet<FA_MDR_FA_Catch_Type> FA_MDR_FA_Catch_Type { get; set; }
            public virtual DbSet<FA_MDR_FA_Characteristic> FA_MDR_FA_Characteristic { get; set; }
            public virtual DbSet<FA_MDR_FA_Device_Gear_Attachment> FA_MDR_FA_Device_Gear_Attachment { get; set; }
            public virtual DbSet<FA_MDR_FA_Fishery> FA_MDR_FA_Fishery { get; set; }
            public virtual DbSet<FA_MDR_FA_Gear_Characteristic> FA_MDR_FA_Gear_Characteristic { get; set; }
            public virtual DbSet<FA_MDR_FA_Gear_Problem> FA_MDR_FA_Gear_Problem { get; set; }
            public virtual DbSet<FA_MDR_FA_Gear_Recovery> FA_MDR_FA_Gear_Recovery { get; set; }
            public virtual DbSet<FA_MDR_FA_Gear_Role> FA_MDR_FA_Gear_Role { get; set; }
            public virtual DbSet<FA_MDR_FA_NEAFC_Stock> FA_MDR_FA_NEAFC_Stock { get; set; }
            public virtual DbSet<FA_MDR_FA_Query_Parameter> FA_MDR_FA_Query_Parameter { get; set; }
            public virtual DbSet<FA_MDR_FA_Query_Type> FA_MDR_FA_Query_Type { get; set; }
            public virtual DbSet<FA_MDR_FA_Reason_Arrival> FA_MDR_FA_Reason_Arrival { get; set; }
            public virtual DbSet<FA_MDR_FA_Reason_Departure> FA_MDR_FA_Reason_Departure { get; set; }
            public virtual DbSet<FA_MDR_FA_Reason_Discard> FA_MDR_FA_Reason_Discard { get; set; }
            public virtual DbSet<FA_MDR_FA_Reason_Entry> FA_MDR_FA_Reason_Entry { get; set; }
            public virtual DbSet<FA_MDR_FA_Trip_Id_Type> FA_MDR_FA_Trip_Id_Type { get; set; }
            public virtual DbSet<FA_MDR_FA_Vessel_Role> FA_MDR_FA_Vessel_Role { get; set; }
            public virtual DbSet<FA_MDR_FARM> FA_MDR_FARM { get; set; }
            public virtual DbSet<FA_MDR_Fish_Packaging> FA_MDR_Fish_Packaging { get; set; }
            public virtual DbSet<FA_MDR_Fishing_Trip_Type> FA_MDR_Fishing_Trip_Type { get; set; }
            public virtual DbSet<FA_MDR_FLAP_Id_Type> FA_MDR_FLAP_Id_Type { get; set; }
            public virtual DbSet<FA_MDR_FLUX_FA_FMC> FA_MDR_FLUX_FA_FMC { get; set; }
            public virtual DbSet<FA_MDR_FLUX_FA_Report_Type> FA_MDR_FLUX_FA_Report_Type { get; set; }
            public virtual DbSet<FA_MDR_FLUX_FA_Type> FA_MDR_FLUX_FA_Type { get; set; }
            public virtual DbSet<FA_MDR_FLUX_Location_Char> FA_MDR_FLUX_Location_Char { get; set; }
            public virtual DbSet<FA_MDR_GFCM_GSA> FA_MDR_GFCM_GSA { get; set; }
            public virtual DbSet<FA_MDR_Management_Area> FA_MDR_Management_Area { get; set; }
            public virtual DbSet<FA_MDR_Target_Species_Group> FA_MDR_Target_Species_Group { get; set; }
            public virtual DbSet<FA_MDR_UN_Data_Type> FA_MDR_UN_Data_Type { get; set; }
            public virtual DbSet<FA_MDR_Vessel_Storage_Type> FA_MDR_Vessel_Storage_Type { get; set; }
            #endregion

            #region FLAP
            public virtual DbSet<FLAP_MDR_Agreement_Type> FLAP_MDR_Agreement_Type { get; set; }
            public virtual DbSet<FLAP_MDR_Chartering_Type> FLAP_MDR_Chartering_Type { get; set; }
            public virtual DbSet<FLAP_MDR_FAR_Fish_Category> FLAP_MDR_FAR_Fish_Category { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Characteristic> FLAP_MDR_FLAP_Characteristic { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Coastal_Party> FLAP_MDR_FLAP_Coastal_Party { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Doc_Type> FLAP_MDR_FLAP_Doc_Type { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Flag_State> FLAP_MDR_FLAP_Flag_State { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Quota_type> FLAP_MDR_FLAP_Quota_type { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_Request_Purpose> FLAP_MDR_FLAP_Request_Purpose { get; set; }
            public virtual DbSet<FLAP_MDR_FLAP_WF_Steps> FLAP_MDR_FLAP_WF_Steps { get; set; }
            public virtual DbSet<FLAP_MDR_Vessel_Crew_Type> FLAP_MDR_Vessel_Crew_Type { get; set; }
            #endregion

            #region MDM
            public virtual DbSet<MDM_MDR_Data_Type> MDM_MDR_Data_Type { get; set; }
            public virtual DbSet<MDM_MDR_FLUX_MDR_Query_type> MDM_MDR_FLUX_MDR_Query_type { get; set; }
            public virtual DbSet<MDM_MDR_MDM_BR> MDM_MDR_MDM_BR { get; set; }
            public virtual DbSet<MDM_MDR_MDM_BR_Def> MDM_MDR_MDM_BR_Def { get; set; }
            #endregion

            #region Sales
            public virtual DbSet<Sales_MDR_Country_Currency> Sales_MDR_Country_Currency { get; set; }
            public virtual DbSet<Sales_MDR_Fish_Size_Category> Sales_MDR_Fish_Size_Category { get; set; }
            public virtual DbSet<Sales_MDR_FLUX_Sales_Party_Id_Type> Sales_MDR_FLUX_Sales_Party_Id_Type { get; set; }
            public virtual DbSet<Sales_MDR_FLUX_Sales_Party_Role> Sales_MDR_FLUX_Sales_Party_Role { get; set; }
            public virtual DbSet<Sales_MDR_FLUX_Sales_Query_Param> Sales_MDR_FLUX_Sales_Query_Param { get; set; }
            public virtual DbSet<Sales_MDR_FLUX_Sales_Query_Param_Role> Sales_MDR_FLUX_Sales_Query_Param_Role { get; set; }
            public virtual DbSet<Sales_MDR_FLUX_Sales_Type> Sales_MDR_FLUX_Sales_Type { get; set; }
            public virtual DbSet<Sales_MDR_Product_Destination> Sales_MDR_Product_Destination { get; set; }
            public virtual DbSet<Sales_MDR_Sales_BR> Sales_MDR_Sales_BR { get; set; }
            public virtual DbSet<Sales_MDR_Sales_BR_Def> Sales_MDR_Sales_BR_Def { get; set; }
            public virtual DbSet<Sales_MDR_Vehicle_Type> Sales_MDR_Vehicle_Type { get; set; }
            #endregion

            #region Vessel
            public virtual DbSet<Vessel_MDR_Comm_Equip_Type> Vessel_MDR_Comm_Equip_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Deck_Machinery_Type> Vessel_MDR_Deck_Machinery_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FishFinder_Equip_Type> Vessel_MDR_FishFinder_Equip_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Admin_Type> Vessel_MDR_FLUX_Vessel_Admin_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Equip_Type> Vessel_MDR_FLUX_Vessel_Equip_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Constr_Type> Vessel_MDR_FLUX_Vessel_Constr_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Dim_Type> Vessel_MDR_FLUX_Vessel_Dim_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Engine_Role> Vessel_MDR_FLUX_Vessel_Engine_Role { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Gear_Role> Vessel_MDR_FLUX_Vessel_Gear_Role { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Hist_Char> Vessel_MDR_FLUX_Vessel_Hist_Char { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Query_Param> Vessel_MDR_FLUX_Vessel_Query_Param { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Query_Type> Vessel_MDR_FLUX_Vessel_Query_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Regstr_Type> Vessel_MDR_FLUX_Vessel_Regstr_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Report_Type> Vessel_MDR_FLUX_Vessel_Report_Type { get; set; }
            public virtual DbSet<Vessel_MDR_FLUX_Vessel_Tech_Type> Vessel_MDR_FLUX_Vessel_Tech_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Navig_Equip_Type> Vessel_MDR_Navig_Equip_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Propeller_Type> Vessel_MDR_Propeller_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Storage_Type> Vessel_MDR_Storage_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_BR> Vessel_MDR_Vessel_BR { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_BR_Def> Vessel_MDR_Vessel_BR_Def { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Category> Vessel_MDR_Vessel_Category { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Event> Vessel_MDR_Vessel_Event { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Export_Type> Vessel_MDR_Vessel_Export_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Hull_Type> Vessel_MDR_Vessel_Hull_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Photo_Type> Vessel_MDR_Vessel_Photo_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Port> Vessel_MDR_Vessel_Port { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Public_Aid_Type> Vessel_MDR_Vessel_Public_Aid_Type { get; set; }
            public virtual DbSet<Vessel_MDR_Vessel_Segment> Vessel_MDR_Vessel_Segment { get; set; }
            public virtual DbSet<Vessel_MDR_VMS_Satellite_Oper> Vessel_MDR_VMS_Satellite_Oper { get; set; }
            #endregion

            /// <summary>
            /// 
            /// </summary>
            //public virtual DbSet<FLUXInnerMsg> FLUXReceivedMsg { get; set; }

            #region VMS
            public virtual DbSet<VMS_MDR_FLUX_Vessel_Position_Type> VMS_MDR_FLUX_Vessel_Position_Type { get; set; }
            public virtual DbSet<VMS_MDR_VMS_BR> VMS_MDR_VMS_BR { get; set; }
            public virtual DbSet<VMS_MDR_VMS_BR_Def> VMS_MDR_VMS_BR_Def { get; set; }
            public virtual DbSet<VMS_MDR_VMS_BR_Param> VMS_MDR_VMS_BR_Param { get; set; }
            #endregion
            #endregion

            public DbSet<FLUXInnerMsg> FLUXInnerMsg { get; set; }


        }
    }
}
