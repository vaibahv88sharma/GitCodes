using Microsoft.SharePoint;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace Sadara.DigiCom.ReportFinder.WebParts.CoordinationCodeReport
{
    [ToolboxItemAttribute(false)]
    public partial class CoordinationCodeReport : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public CoordinationCodeReport()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #region Global Variable

        DataTable ODMSCoordinationCodeDataTable = new DataTable();
        DataTable ODMSRequirementDataTable = new DataTable();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadMethodLogic();
            }
        }

        private void loadMethodLogic()
        {           
            //get requirement Details
            ODMSCoordinationCodeDataTable = getODMSCoordinationCodeDetails();
            databindToRepeator();
        }

        private void databindToRepeator()
        {
            if (ODMSCoordinationCodeDataTable != null)
            {
                CoordinationCodeRepeater.DataSource = ODMSCoordinationCodeDataTable;
                CoordinationCodeRepeater.DataBind();
            }
        }

        private DataTable getODMSCoordinationCodeDetails()
        {
            DataTable tempDT = new DataTable();
            //get ODMSRequirement List Details
            SPListItemCollection ODMSCoordinationCodeListItems = new ApplicabilityReportMethods().getODMSCoordinationCode();
            if (ODMSCoordinationCodeListItems != null)
            {
                tempDT = ODMSCoordinationCodeListItems.GetDataTable();
            }
            return tempDT;
        }
    }
}
