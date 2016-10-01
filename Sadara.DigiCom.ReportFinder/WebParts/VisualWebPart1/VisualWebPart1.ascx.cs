using Sadara.DigiCom.ReportFinder.ViewModel;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace Sadara.DigiCom.ReportFinder.WebParts.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public partial class VisualWebPart1 : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public VisualWebPart1()
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
            _APPLICATIONTKEY = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTKEY]);
        }
    }
}
