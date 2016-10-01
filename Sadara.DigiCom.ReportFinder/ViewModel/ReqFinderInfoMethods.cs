using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    class ReqFinderInfoMethods
    {        
        #region constant for datatable
        //constant For datatable
        public static string DataTable_FIELDS_ApplicationCategory = "ApplicationCategory_Lookup";
        public static string DataTable_FIELDS_DetailedDriverDescription = "DetailedDriverDescription";

        //public static string Page_DetailedReportURL = SPContext.Current.Web.Url + "/SitePages/DetailedReport.aspx?";        
        //public static string Page_ApplicationRequirementDetailsPageURL = SPContext.Current.Web.Url + "/SitePages/ApplicationRequirementDetails.aspx?";
        public static string Page_DetailedReportURL = SPContext.Current.Web.Url + "/Pages/DetailedReport.aspx?";
        public static string Page_ApplicationRequirementDetailsPageURL = SPContext.Current.Web.Url + "/Pages/ApplicationRequirementDetails.aspx?";

        #endregion

        #region Get all items of Value List
        public SPListItemCollection getODMSValueListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSValue];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueKey + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion

        #region Get all items of Application List
        
        public SPListItemCollection getODMSApplicationListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplication];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationCategory + "'/>",                                              
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion

        #region Get all items of CoOrdination List
        public SPListItemCollection getODMSCoOrdinationListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSCoordinationCode];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                                "<ViewFields>",
                                                "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription +"'/>",
                                                "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey + "'/>",
                                                "</ViewFields>",
                                              "</View>");
            objItems = list.GetItems(qryCourse);
            return objItems;
        }
        #endregion

        #region Get all items of ReqDiffCode List
        public SPListItemCollection getODMSReqDiffCodeListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSReqDiffCode];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                                "<ViewFields>",
                                                "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription + "'/>",
                                                "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID + "'/>",
                                                "</ViewFields>",
                                              "</View>");
            objItems = list.GetItems(qryCourse);
            return objItems;
        }
        #endregion

        #region Get all items of Driver List
        public SPListItemCollection getODMSDriverListDetailsAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSDriver];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DetailedDriverDescription + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion

        #region Get items of Requirement List
        public SPListItemCollection getODMSRequirementListItemCollection(string requirementKey)//Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENT];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_RequirementLevel_Lookup + "'/>",                                               
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "/>",
                                              "<Values>",
                                                requirementKey,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion

    }
}

