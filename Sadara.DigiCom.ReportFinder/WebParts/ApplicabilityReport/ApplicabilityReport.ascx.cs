using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using Sadara.DigiCom.ReportFinder.ViewModel;
using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Sadara.DigiCom.ReportFinder.WebParts.ApplicabilityReport
{
    [ToolboxItemAttribute(false)]
    public partial class ApplicabilityReport : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ApplicabilityReport()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region Global Variable

        DataTable ODMSApplicationCategoryDataTable = new DataTable();
        DataTable ODMSRequirementDataTable = new DataTable();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                loadMethodLogic();
            }
        }

        #region Logic to Load Data from Lists and bind to Repeater
        private void loadMethodLogic()
        {
            //get requirement Details
            ODMSApplicationCategoryDataTable = getODMSApplicationCategoryDetails();
            databindToRepeator();
        }
        #endregion

        #region Get Application Category Details
        private DataTable getODMSApplicationCategoryDetails()
        {
            DataTable tempDT = new DataTable();
            //get ODMSRequirement List Details
            SPListItemCollection ODMSApplicationCategoryListItems = new ApplicabilityReportMethods().getODMSApplicationCategory();
            if (ODMSApplicationCategoryListItems != null)
            {
                tempDT = ODMSApplicationCategoryListItems.GetDataTable();
            }
            return tempDT;
        }
        #endregion

        #region Get Application Details
        private DataTable getODMSApplicationDetails(string ApplicationCategory)
        {
            DataTable tempDT = new DataTable();
            //get ODMSRequirement List Details
            SPListItemCollection ODMSApplicationListItems = new ApplicabilityReportMethods().getODMSApplication(ApplicationCategory);
            if (ODMSApplicationListItems != null)
            {
                tempDT = ODMSApplicationListItems.GetDataTable();
            }
            return tempDT;
        }
        #endregion

        #region Get Requirement ListItems
        private DataTable getODMSRequirementListItems(List<string> applicationIDs)
        {
            DataTable tempDT = new DataTable();
            if (applicationIDs != null && applicationIDs.Count > 0)
            {
                SPListItemCollection ODMSODMSRequirementsListItems = new ApplicabilityReportMethods().getODMSRequirements(applicationIDs);
                if (ODMSODMSRequirementsListItems != null)
                {
                    tempDT = ODMSODMSRequirementsListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        #region Bind Data to Repeater
        private void databindToRepeator()
        {
            if (ODMSApplicationCategoryDataTable != null)
            {
                ApplicationCategoryRepeater.DataSource = ODMSApplicationCategoryDataTable;
                ApplicationCategoryRepeater.DataBind();
            }
        }

        protected void ApplicationCategoryRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            RepeaterItem _item = e.Item;
            if ((_item.ItemType == ListItemType.Item) || (_item.ItemType == ListItemType.AlternatingItem))
            {

                #region Display Driver Details

                Literal ApplicationCategoryLiteral = e.Item.FindControl("ApplicationCategoryLiteral") as Literal;
                Repeater ApplicationRepeater = e.Item.FindControl("ApplicationRepeater") as Repeater;
               
                DataTable DTRepeatorData = getODMSApplicationDetails(ApplicationCategoryLiteral.Text.ToString());

                if (DTRepeatorData != null)
                {
                    ApplicationRepeater.DataSource = DTRepeatorData;
                    ApplicationRepeater.DataBind();
                }

                #endregion
            }
        }
        private void databindToSubDivRepeator()
        {
            if (ODMSRequirementDataTable != null)
            {
                RequirementRepeater.DataSource = ODMSRequirementDataTable;
                RequirementRepeater.DataBind();
            }
        }
        #endregion

        #region Show Applicability Report on Button Click
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            //get application IDs                   
            List<string> applicationIDs = new List<string>();
            foreach (RepeaterItem i in ApplicationCategoryRepeater.Items)
            {
                Repeater childRepeater = (Repeater)i.FindControl("ApplicationRepeater");

                foreach (RepeaterItem ii in childRepeater.Items)
                {
                    //Retrieve the state of the CheckBox

                    HiddenField hiddenEmail = (HiddenField)ii.FindControl("HiddenFieldApplicationKey");
                    CheckBox cb = (CheckBox)ii.FindControl("selectApplicability");
                    if (cb.Checked)
                    {
                        //Retrieve the value associated with that CheckBox                        
                        applicationIDs.Add(hiddenEmail.Value.ToString());

                    }
                }
            }

            chkboxCount.Text = applicationIDs.Count.ToString();
            ODMSRequirementDataTable = getODMSRequirementListItems(applicationIDs);
            databindToSubDivRepeator();
            MainDiv.Visible = false;
        }
        #endregion

    }
}
