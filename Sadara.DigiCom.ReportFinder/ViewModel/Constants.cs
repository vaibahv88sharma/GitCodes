using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    public static class Constants
    {
        #region Constants for List Name

        public const string LIST_NAME_ODMSREQUIREMENTLEVEL = "ODMSRequirementLevel";
        public const string LIST_NAME_ODMSApplicationCategory = "ODMSApplicationCategory";
        //constants for list name                                  
        public const string LIST_NAME_ODMSREQUIREMENTREFERENCE = "ODMSRequirementReference";
        public const string LIST_NAME_ODMSREQUIREMENT = "ODMSRequirements";
        public const string LIST_NAME_ODMSDriverMapping = "ODMSDriverMapping";
        public const string LIST_NAME_ODMSApplicationMapping = "ODMSApplicationMapping";
        public const string LIST_NAME_ODMSApplication = "ODMSApplication";
        public const string LIST_NAME_ODMSReqDiffCode = "ODMSReqDiffCode";
        public const string LIST_NAME_ODMSReqDiffCodeMapping = "ODMSReqDiffCodeMapping";
        public const string LIST_NAME_ODMSValue = "ODMSValue";
        public const string LIST_NAME_ODMSValueMapping = "ODMSValueMapping";
        public const string LIST_NAME_ODMSCoordinationCode = "ODMSCoordinationCode";
        public const string LIST_NAME_ODMSCoordinationCodeMapping = "ODMSCoordinationCodeMapping";
        public const string LIST_NAME_ODMSDriver = "ODMSDriver";
        public const string LIST_NAME_ODMSGeography = "ODMSGeography";
        public const string LIST_NAME_ODMSDriverDescription = "ODMSDriverDescription";
        #endregion

        #region set  Global Value "dont make const"
        public static string Requirement_Value_ID = string.Empty;
        public static string SelectedType = string.Empty;
        #endregion

        #region ULS Logging
        public const string DIAGNOSTICAREANAME = "ODMS Report Finder";
        public const string WebPart = "WebPart";
        public const string ODMSREPORTFINDERLOGGINGSERVICE = "ODMS Report Finder Logging Service";

        #region ULSLogs
        public const string CATEGORYNAME = "500";
        public const string PAGELOAD = "Page_Load";
        public const string ReportFinderWebPart = "ReportFinderWebPart";
        public const string DATABINDTOREPEATOR = "DATABINDTOREPEATOR";
        public const string AddRequirementDeatils = "AddRequirementDeatils";
        public const string DATABINDTOGrid = "DATABINDTOGrid";
        public const string ADDMETHODE = "AddMethode";
        public const string DELETEMETHODE = "DeleteMethode";
        public const string LEFTNAVWBEPART = "LEFTNAVWBEPART";
        #endregion ULSLogs

        #endregion

        #region Constants For Query String

        //constants for query String
        public static string QueryString_SelectedID = "SelectedID";
        public static string QueryString_REQUIREMENTKEY = "REQUIREMENTKEY";
        public static string QueryString_REQUIREMENTREFERENCE = "REQUIREMENTREFERENCE";
        public static string QueryString_REQUIREMENTNUMBER = "REQUIREMENTNUMBER";
        public static string QueryString_APPLICATIONKEY = "APPLICATIONKEY";
        public static string QueryString_COORDINATIONCODEKEY = "COORDINATIONCODEKEY";
        public static string QueryString_Type = "Type";
        public static string Selected_Type_Driver = "DRIVER";
        public static string Selected_Type_Application = "APPLICATION";
        public static string Selected_Type_ReqDiffCode = "REQDIFFCODE";
        public static string Selected_Type_ValueCriteria = "VALUECRITERIA";
        public static string Selected_Type_CoordinationCode = "COORDINATIONCODE";
        public static string Page_RequirementPageDetailsURL = SPContext.Current.Web.Url + "/Pages/AddRequirementDeatils.aspx?";
        public static string Page_SeeRequirementDetailsURL = SPContext.Current.Web.Url + "/Pages/SeeDetails.aspx?";
        public static string Page_ApplicationRequirementDetailsURL = SPContext.Current.Web.Url + "/Pages/ApplicabilityReport.aspx?";
        public static string Page_CoordinationCodeRequirementDetailsURL = SPContext.Current.Web.Url + "/Pages/CoordinationCodeRequirementDetails.aspx?";

        #endregion

        #region constant for datatable
        //constant For datatable
        public const string DataTable_FIELDS_FirstColumn = "FirstColumn";
        public const string DataTable_FIELDS_SecondColumn = "SecondColumn";

        public const string DataTable_FIELDS_REQUIREMENTNUMBER = "REQUIREMENTNUMBER";
        public const string DataTable_FIELDS_REQUIREMENTKEY = "REQUIREMENTKEY";
        public const string DataTable_FIELDS_REQUIREMENTREFERENCE = "REQUIREMENTREFERENCE";
        public const string DataTable_FIELDS_ID = "ID";
        public const string DataTable_FIELDS_Name = "Name";
        public const string DataTable_FIELDS_DetailDescription = "DetailDescription";

        public const string DataTable_FIELDS_ODMSDriverListID = "ODMSDriverListID";

        public const string DataTable_FIELDS_Value = "Value";
        public const string DataTable_FIELDS_ValueId = "ValueId";
        public const string DataTable_FIELDS_ValueDescription = "ValueDescription";

        public const string DataTable_FIELDS_ApplicationCatigory = "ApplicationCatigory";
        public const string DataTable_FIELDS_ApplicationId = "ApplicationId";
        public const string DataTable_FIELDS_ApplicationDescription = "ValueDescription";
        //constant for Req Diff Code DT
        public const string DataTable_FIELDS_Geography = "Geography";
        public const string DataTable_FIELDS_ReqDiffCodeID = "ReqDiffCodeID";
        public const string DataTable_FIELDS_ReqDiffCodeDescription = "ReqDiffCodeDescription";





        #endregion

        #region Constants For All List Fields

        //Constants for ODMSDriverDescription
        public const string ODMSDriverDescription_FIELDS_DriverDescription = "DriverDescription";
        public const string ODMSDriverDescription_FIELDS_ID = "ID";

        //constants for list field
        public const string ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE = "RequirementReference";
        public const string ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCENAME = "RequirementReferenceName";
        public const string ODMSREQUIREMENTREFERENCE_FIELDS_ID = "ID";
        public const string ODMSREQUIREMENTREFERENCE_FIELDS_RequirementReferenceOwner = "RequirementReferenceOwner";
        public const string ODMSREQUIREMENTREFERENCE_FIELDS_Title = "Title";

        //constants for list fields ODMSRequirement
        public const string ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER = "RequirementNumber";
        public const string ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY = "RequirementKey";
        public const string ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE = "RequirementReference_Lookup";
        public const string ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION = "RequirementDescription";
        public const string ODMSREQUIREMENT_FIELDS_ImplementationInstructionsLink = "ImplementationInstructionsLink";
        public const string ODMSREQUIREMENT_FIELDS_ModelPracticesLink = "ModelPracticesLink";
        public const string ODMSREQUIREMENT_FIELDS_RelatedStandards = "RelatedStandards";
        public const string ODMSREQUIREMENT_FIELDS_RequirementLevel_Lookup = "RequirementLevel_Lookup";
        public const string ODMSREQUIREMENT_FIELDS_ID = "ID";

        //constants for list fields ODMSDriverMapping
        public const string ODMSDriverMapping_FIELDS_DriverDescription = "DriverID_x003a_DriverDescription";
        public const string ODMSDriverMapping_FIELDS_DriverID = "DriverKey_Lookup";
        public const string ODMSDriverMapping_FIELDS_RequirementKey = "RequirementKey_Lookup";
        public const string ODMSDriverMapping_FIELDS_Title = "Title";





        //constants for list fields ODMSDriver
        public const string ODMSDriver_FIELDS_DriverID = "DriverKey";
        public const string ODMSDriver_FIELDS_ID = "ID";

        public const string ODMSDriver_FIELDS_DriverDescription = "DriverDescription_Lookup";
        public const string ODMSDriver_FIELDS_DriverDescription_DisplayName = "Driver Description";

        public const string ODMSDriver_FIELDS_DetailedDriverDescription = "DetailedDriverDescription";
        public const string ODMSDriver_FIELDS_Title = "Title";

        //constants for list fields  ODMSApplicationMapping
        public const string ODMSApplicationMapping_FIELDS_RequirementKey = "RequirementKey_Lookup";
        public const string ODMSApplicationMapping_FIELDS_ApplicationID = "ApplicationKey_Lookup";
        public const string ODMSApplicationMapping_FIELDS_Title = "Title";
        public const string ODMSApplicationMapping_FIELDS_ApplicationDescription_Lookup = "ApplicationDescription_Lookup";







        //constants for list fields  ODMSApplication 
        public const string ODMSApplication_FIELDS_ApplicationID = "ApplicationKey";
        public const string ODMSApplication_FIELDS_ApplicationDescription = "ApplicationDescription";
        public const string ODMSApplication_FIELDS_ApplicationCategory = "ApplicationCategory_Lookup";
        public const string ODMSApplication_FIELDS_RequirementReference = "RequirementReference_Lookup";
        public const string ODMSApplication_FIELDS_TipText = "TipText";
        public const string ODMSApplication_FIELDS_Title = "Title";
        public const string ODMSApplication_FIELDS_ID = "ID";



        //constants for list fields  ODMSReqDiffCode 
        public const string ODMSReqDiffCode_FIELDS_ReqDiffCode = "ReqDiffCode";
        public const string ODMSReqDiffCode_FIELDS_ReqDiffCodeID = "ReqDiffCode";
        public const string ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription = "ReqDiffCodeDescription";
        public const string ODMSReqDiffCode_FIELDS_ID = "ID";


        //constants for list fields  ODMSReqDiffCodeMapping
        public const string ODMSReqDiffCodeMapping_FIELDS_RequirementKey = "RequirementKey_Lookup";
        public const string ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID = "ReqDiffCode_Lookup";

        // public const string ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeDescription_Lookup = "ReqDiffCodeDescription_Lookup";
        public const string ODMSReqDiffCodeMapping_FIELDS_Geography_Lookup = "Geography_Lookup";
        public const string ODMSReqDiffCodeMapping_FIELDS_RDKey = "RDKey";
        public const string ODMSReqDiffCodeMapping_FIELDS_ID = "ID";


        //constants for list fields  ODMSValue
        public const string ODMSValue_FIELDS_Value = "ValueCode";
        public const string ODMSValue_FIELDS_ValueDescription = "ValueDescription";
        public const string ODMSValue_FIELDS_ID = "ID";
        public const string ODMSValue_FIELDS_ValueKey = "ValueKey";


        //constants for list fields  ODMSValueMapping
        public const string ODMSValueMapping_FIELDS_ValueKey = "ValueKey_Lookup";
        public const string ODMSValueMapping_FIELDS_RequirementKey = "RequirementKey_Lookup";
        public const string ODMSValueMapping_FIELDS_ID = "ID";
        public const string ODMSValueMapping_FIELDS_Title = "Title";
        public const string ODMSValueMapping_FIELDS_ValueCode_Lookup = "ValueCode_Lookup";

        //constants for list fields  ODMSCoordinationCode
        public const string ODMSCoordinationCode_FIELDS_CoordinationCodeDescription = "CoordinationCodeDescription";
        public const string ODMSCoordinationCode_FIELDS_CoordinationCodeKey = "CoOrdinationKey";
        public const string ODMSCoordinationCode_FIELDS_Title = "Title";
        public const string ODMSCoordinationCode_FIELDS_ID = "ID";


        //constants for list fields  ODMSCoordinationCodeMapping
        public const string ODMSCoordinationCodeMapping_FIELDS_RequirementKey = "RequirementKey_Lookup";
        public const string ODMSCoordinationCodeMapping_FIELDS_CoordinationCode = "CoOrdinationCodeKey_Lookup";
        public const string ODMSCoordinationCodeMapping_FIELDS_ID = "ID";
        public const string ODMSCoordinationCodeMapping_FIELDS_CoordinationCodeDescription_Lookup = "CoordinationCodeDescription_Lookup";
        public const string ODMSCoordinationCodeMapping_FIELDS_Title = "Title";

        //constants for ODMSApplicationCategory
        public const string ODMSApplicationCategory_FIELDS_ApplicationCategory = "ApplicationCategory";
        public const string ODMSApplicationCategory_FIELDS_AppCatKey = "AppCatKey";
        public const string ODMSApplicationCategory_FIELDS_EvaluationQuestion = "EvaluationQuestion";
        public const string ODMSApplicationCategory_FIELDS_Title = "Title";

        //constants for ODMSGeography
        public const string ODMSGeography_FIELDS_Geography = "Geography";
        public const string ODMSGeography_FIELDS_Title = "Title";
        public const string ODMSGeography_FIELDS_ID = "ID";


        //constants for ODMSRequirementLevel
        public const string ODMSRequirementLevel_FIELDS_RequirementLevel = "RequirementLevel";
        public const string ODMSRequirementLevel_FIELDS_Title = "Title";
        #endregion

        #region LeftNavigation
        public const string LEFTNAV_HEADERSTRING = "<h2><i class=\"{0}\"></i>&nbsp<span>{1}</span></h2><hr/>";
        public const string LEFTNAV_TOPLEVELLISTOPENTAG = "<ul id=\"side-nav-menu\" class=\"side-nav-menu\">";
        public const string LEFTNAV_TOPLEVELLISTCLOSETAG = "</ul>";
        public const string LEFTNAV_TOPLEVELLISITEM = "<li><a href=\"{0}\">{1}<i class=\"fa fa-angle-down\"></i></a></li>";
        public const string LEFTNAV_TOPLEVELLISTITEMWITHCHILDREN = "<li><a href=\"{0}\" rel=\"{1}\">{2}<i class=\"fa fa-angle-down\"></i></a>";
        public const string LEFTNAV_DROPLISTOPENTAGS = "<div  id=\"{0}\" class=\"side-nav-drop\"><ul>";
        public const string LEFTNAV_DROPLISTCLOSETAGS = "</ul></div>";
        public const string LEFTNAV_DROPLISTITEM = "<li><a href=\"{0}\">{1}</a></li>";
        public const string LEFTNAV_DEFAULT_HEADER_ICON = "fa fa-briefcase";
        #endregion

        #region Termsets
        public const string TERMGROUP = "Sadara";
        public const string LEFTNAVTERMSET = "Departments";
        public const string URLPROPERTY = "_Sys_Nav_SimpleLinkUrl";
        public const string HOMETERMSET = "Home";
        public const string URLTERM = "URL";
        public const string NAVIGATIONGROUP = "Navigation";
        public const string NAVIGATIONTERMSET = "TopNavigation";
        public const string DEPARTMENTSDROPDOWN = "Departments";
        public const string DROPDOWNHEADING = "Business Lines & Departments";
        #endregion Termsets
    }
}
