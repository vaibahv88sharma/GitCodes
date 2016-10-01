using Microsoft.SharePoint;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace Sadara.DigiCom.ReportFinder.WebParts.FilteredRequirements
{
    [ToolboxItemAttribute(false)]
    public partial class FilteredRequirements : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public FilteredRequirements()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region GlobalVariable
        string _APPLICATIONTKEY = string.Empty;
        DataTable ODMSRequirementsDataTable = new DataTable();
        #endregion GlobalVariable
        protected void Page_Load(object sender, EventArgs e)
        {
            _APPLICATIONTKEY = Convert.ToString(Page.Request[Constants.QueryString_APPLICATIONKEY]);
            //_APPLICATIONTKEY = "297"; //4
            BindData(_APPLICATIONTKEY);
        }

        private void BindData(string _APPLICATIONTKEY)
        {
            //get requirement Details
            ODMSRequirementsDataTable = getODMSRequirementDetails(_APPLICATIONTKEY);
            databindToRepeator();
        }

        private void databindToRepeator()
        {
            if (ODMSRequirementsDataTable != null)
            {
                RequirementRepeater.DataSource = ODMSRequirementsDataTable;
                RequirementRepeater.DataBind();
            }
        }

        private DataTable getODMSRequirementDetails(string applicationKey)
        {
            DataTable tempDT = new DataTable();
            //get ODMSRequirement List Details
            SPListItemCollection ODMSApplicationMappingList = new FilteredRequirementsMethods().geODMStApplicationMapping(applicationKey);

            if (ODMSApplicationMappingList != null)
            {
                tempDT = ODMSApplicationMappingList.GetDataTable();
            }

            SPListItemCollection ODMSRequirementsListItems = new FilteredRequirementsMethods().getODMSRequirementsListItemCollection(tempDT);
            if (ODMSRequirementsListItems != null)
            {
                tempDT = ODMSRequirementsListItems.GetDataTable();
            }
            return tempDT;
        }
    }
}
