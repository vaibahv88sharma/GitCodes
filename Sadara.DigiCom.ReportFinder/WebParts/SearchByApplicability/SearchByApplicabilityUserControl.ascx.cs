using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using Sadara.DigiCom.ReportFinder.ViewModel;
using Microsoft.SharePoint;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;

namespace Sadara.DigiCom.ReportFinder.WebParts.SearchByApplicability
{
    public partial class SearchByApplicabilityUserControl : UserControl
    {
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


        private void loadMethodLogic()
        {
            //get requirement Details
            ODMSApplicationCategoryDataTable = getODMSApplicationCategoryDetails();
            databindToRepeator();
        }

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

        private void databindToRepeator()
        {
            if (ODMSApplicationCategoryDataTable != null)
            {
                ApplicationCategoryRepeater.DataSource = ODMSApplicationCategoryDataTable;
                ApplicationCategoryRepeater.DataBind();
            }
        }

        protected void ApplicationCategoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        private void databindToSubDivRepeator()
        {
            if (ODMSRequirementDataTable != null)
            {
                RequirementRepeater.DataSource = ODMSRequirementDataTable;
                RequirementRepeater.DataBind();
            }
        }
    }
}
