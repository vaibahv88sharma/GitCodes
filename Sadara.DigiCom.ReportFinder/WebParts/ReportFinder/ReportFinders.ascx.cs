using Microsoft.SharePoint;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;

namespace Sadara.DigiCom.ReportFinder.WebParts.ReportFinders
{
    [ToolboxItemAttribute(false)]
    public partial class ReportFinders : System.Web.UI.WebControls.WebParts.WebPart
    {
        #region OnInit


        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ReportFinders()
        {
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #endregion

        #region GlobalVariable
        //constant for geting requirement Details
        DataTable ODMSRequirementDataTable = new DataTable();
        //variable for getting driver Details
        DataTable ODMSDriverMappingDataTable = new DataTable();
        DataTable ODMSDriverDataTable = new DataTable();
        //variable for getting Application Details
        DataTable ODMSApplicationMappingDataTable = new DataTable();
        DataTable ODMSApplicationDataTable = new DataTable();
        //variable for getting ReqDiffCodeMapping
        DataTable ODMSReqDiffCodeMappingDataTable = new DataTable();
        DataTable ODMSReqDiffCodeDataTable = new DataTable();
        //variable for getting Value Mapping
        DataTable ODMSValueMappingDataTable = new DataTable();
        DataTable ODMSValueDataTable = new DataTable();
        //variable for getting CoordinationCode Mapping
        DataTable ODMSCoordinationCodeMappingDataTable = new DataTable();
        DataTable ODMSCoordinationCodeDataTable = new DataTable();

        //selected type

        #endregion

        #region Common method
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //get value from query string ODMS Request Reference ID
                //string QueryString_SelectedID = Convert.ToString(Page.Request.QueryString[Constants.QueryString_SelectedID]);    
                string requirementReferenceNameOrReference = RequirementReferenceHiddenField.Value;
                autocompleteTextBox.Text = requirementReferenceNameOrReference;
                LoadDataForAutoComInHiddenfield();
                if (!string.IsNullOrEmpty(requirementReferenceNameOrReference))
                {
                    string _RequirementReference = getODMSRequirementReferenceOnReqRefID(requirementReferenceNameOrReference);
                    loadMethodLogic(_RequirementReference);
                }

            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.PAGELOAD, Constants.ReportFinderWebPart, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        private void loadMethodLogic(string _RequirementReference)
        {

            //get RequirementReference from Requirement Reference list
            //  string RequirementReference = getODMSRequirementReferenceOnReqRefID(QueryString_SelectedID);
            string RequirementReference = _RequirementReference;
            //  RequirementReference = autocompleteTextBox.Text.Trim();
            if (string.IsNullOrEmpty(RequirementReference))
            {
                return;
            }
            //get requirement Details
            ODMSRequirementDataTable = getODMSRequirementDetils(RequirementReference);

            if (DriverDetailsRadioButton.Checked)
            {
                Constants.SelectedType = Constants.Selected_Type_Driver;
                //get Driver Mapping Details
                ODMSDriverMappingDataTable = getODMSDriverMappingDetail();
                //get Driver Details
                ODMSDriverDataTable = getODMSDriverListItems();
            }
            else if (ApplicationDetailsRadioButton.Checked)
            {
                Constants.SelectedType = Constants.Selected_Type_Application;
                //get application Mapping Details
                ODMSApplicationMappingDataTable = getODMSApplicationMappingListItems();
                //get application Details
                ODMSApplicationDataTable = getODMSApplicationListItems();
            }
            else if (ReqDiffCodeMappingRadioButton.Checked)
            {
                Constants.SelectedType = Constants.Selected_Type_ReqDiffCode;
                //get ODMSReq Diff Code Mapping Details
                ODMSReqDiffCodeMappingDataTable = getODMSReqDiffCodeMappingListItems();
                //get ODMSReq Diff Code Details
                ODMSReqDiffCodeDataTable = getODMSReqDiffCodeListItems();


            }
            else if (ValueCriteriaRadioButton.Checked)
            {
                Constants.SelectedType = Constants.Selected_Type_ValueCriteria;
                //get ODMSValue Mapping Details
                ODMSValueMappingDataTable = getODMSValueMappingListItems();
                //get ODMSValue Details
                ODMSValueDataTable = getODMSValueListItems();


            }
            else if (CoordinationCodeRadioButton.Checked)
            {
                Constants.SelectedType = Constants.Selected_Type_CoordinationCode;
                //get ODMSValue Mapping Details
                ODMSCoordinationCodeMappingDataTable = getODMSCoordinationCodeMappingListItems();
                //get ODMSValue Details
                ODMSCoordinationCodeDataTable = getODMSCoordinationCodeListItems();


            }
            else if (RequirementsOnlyRadioButton.Checked)
            {
                btnExportToExcel.Visible = true;
            }
            //bind data to requirement detail repeartor
            databindToRepeator();

        }
        #endregion

        #region ODMSRequest Reference Details
        private void LoadDataForAutoComInHiddenfield()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSRequirementReferenceListItems = new ReportFinderMethods().getAllODMSRequirementReference();

            if (ODMSRequirementReferenceListItems != null)
            {
                List<string> _AllRequirementReferenceList = (from SPListItem item in ODMSRequirementReferenceListItems.OfType<SPListItem>()
                                                             select Convert.ToString(item[Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE])).ToList();
                List<string> _AllRequirementReferenceNameList = (from SPListItem item in ODMSRequirementReferenceListItems.OfType<SPListItem>()
                                                                 select Convert.ToString(item[Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCENAME])).ToList();
                AllRequirementReferenceHiddenField.Value = string.Join("-", _AllRequirementReferenceList);
                AllRequirementReferenceNameHiddenField.Value = string.Join("-", _AllRequirementReferenceNameList);

            }

        }
        private string getODMSRequirementReferenceOnReqRefID(string requirementReferenceNameOrReference)
        {
            SPListItemCollection RequirementReferenceList = new ReportFinderMethods().getODMSRequirementReference(requirementReferenceNameOrReference);
            var RequirementReference = (from SPListItem item in RequirementReferenceList.OfType<SPListItem>()
                                        select item[Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE]).ToList().FirstOrDefault();
            return Convert.ToString(RequirementReference);
        }
        private DataTable getODMSRequirementDetils(string RequirementReference)
        {
            DataTable tempDT = new DataTable();
            //get ODMSRequirement List Details
            SPListItemCollection ODMSRequirementListItems = new ReportFinderMethods().getODMSRequirement(RequirementReference);
            if (ODMSRequirementListItems != null)
            {
                tempDT = ODMSRequirementListItems.GetDataTable();
            }
            return tempDT;

        }
        private void databindToRepeator()
        {
            try
            {
                RequirementRepeater.DataSource = ODMSRequirementDataTable;
                RequirementRepeater.DataBind();
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DATABINDTOREPEATOR, Constants.ReportFinderWebPart, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void RequirementRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            RepeaterItem _item = e.Item;
            if ((_item.ItemType == ListItemType.Item || _item.ItemType == ListItemType.AlternatingItem))
            {
                #region Display Driver Details
                if (DriverDetailsRadioButton.Checked)
                {
                    Literal RequirementNumberLiteral = e.Item.FindControl("RequirementNumberLiteral") as Literal;
                    Repeater RequirementSubDetailsRepeater = e.Item.FindControl("ApplicationDetailsRepeater") as Repeater;
                    HiddenField requirementKeyHiddenField = e.Item.FindControl("requirementKeyHiddenField") as HiddenField;
                    //Label DetailHeaderLabel = e.Item.FindControl("DetailHeaderLabel") as Label;
                    //DetailHeaderLabel.Text = "Driver Description";                 
                    RequirementSubDetailsRepeater.Visible = false;

                    DataTable DTRepeatorData = new DataTable();
                    DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_FirstColumn);
                    DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_SecondColumn);

                    if (ODMSDriverMappingDataTable != null && ODMSDriverMappingDataTable.Rows.Count > 0)
                    {
                        //get application IDs                   
                        List<string> driverIDs = new List<string>();
                        foreach (DataRow item in ODMSDriverMappingDataTable.Rows)
                        {
                            string RequirementKey = Convert.ToString(item[Constants.ODMSDriverMapping_FIELDS_RequirementKey]).Split('.').FirstOrDefault();
                            string Driverid = Convert.ToString(item[Constants.ODMSDriverMapping_FIELDS_DriverID]).Split('.').FirstOrDefault().Trim();

                            if (RequirementKey.Trim().Equals((requirementKeyHiddenField.Value.Trim())))
                            {
                                //check uniqueness in the list
                                if (!driverIDs.Contains(Driverid))
                                {
                                    //add to list
                                    driverIDs.Add(Driverid);
                                }
                            }
                        }
                        if (ODMSDriverDataTable != null && ODMSDriverDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow item in ODMSDriverDataTable.Rows)
                            {
                                string Driverid = Convert.ToString(item[Constants.ODMSDriver_FIELDS_DriverID]).Split('.').FirstOrDefault();
                                if (driverIDs.Contains(Driverid))
                                {
                                    DataRow row = DTRepeatorData.NewRow();
                                    row[Constants.DataTable_FIELDS_FirstColumn] = Convert.ToString(item[Constants.ODMSDriver_FIELDS_DriverDescription]);
                                    row[Constants.DataTable_FIELDS_SecondColumn] = string.Empty;
                                    DTRepeatorData.Rows.Add(row);
                                }
                            }
                            if (DTRepeatorData != null && DTRepeatorData.Rows.Count>0)
                            {
                                RequirementSubDetailsRepeater.Visible = true;
                                RequirementSubDetailsRepeater.DataSource = DTRepeatorData;
                                RequirementSubDetailsRepeater.DataBind();
                            }
                           

                        }
                        
                    }
                   
                }
                #endregion

                #region Display Application Details
                else if (ApplicationDetailsRadioButton.Checked)
                {
                    Literal RequirementNumberLiteral = e.Item.FindControl("RequirementNumberLiteral") as Literal;

                    Repeater ApplicationDetailsRepeater = e.Item.FindControl("ApplicationDetailsRepeater") as Repeater;
                    HiddenField requirementKeyHiddenField = e.Item.FindControl("requirementKeyHiddenField") as HiddenField;

                    ApplicationDetailsRepeater.Visible = false;
                    //get unique application ids from application mapping table
                    if (ODMSApplicationMappingDataTable != null && ODMSApplicationMappingDataTable.Rows.Count > 0)
                    {
                        //get application IDs                   
                        List<string> applicationIDs = new List<string>();
                        foreach (DataRow dr in ODMSApplicationMappingDataTable.Rows)
                        {
                            string requirementKey = Convert.ToString(dr[Constants.ODMSApplicationMapping_FIELDS_RequirementKey]).Split('.').FirstOrDefault().Trim();
                            string applicationid = Convert.ToString(dr[Constants.ODMSApplicationMapping_FIELDS_ApplicationID]).Split('.').FirstOrDefault().Trim();
                            //check requirement key datatable  in the hidden field 
                            if (requirementKey.Equals(requirementKeyHiddenField.Value.Trim()))
                            {
                                //check uniqueness in the list
                                if (!applicationIDs.Contains(applicationid))
                                {
                                    //add to list
                                    applicationIDs.Add(applicationid);
                                }

                            }
                        }
                        //create DT and add Coumn to Dt
                        DataTable DTRepeatorData = new DataTable();
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_FirstColumn);
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_SecondColumn);

                        if (ODMSApplicationDataTable != null && ODMSApplicationDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow item in ODMSApplicationDataTable.Rows)
                            {
                                string applicationid = Convert.ToString(item[Constants.ODMSApplication_FIELDS_ApplicationID]).Split('.').FirstOrDefault();
                                if (applicationIDs.Contains(applicationid))
                                {
                                    DataRow row = DTRepeatorData.NewRow();
                                    row[Constants.DataTable_FIELDS_FirstColumn] = Convert.ToString(item[Constants.ODMSApplication_FIELDS_ApplicationCategory]);
                                    row[Constants.DataTable_FIELDS_SecondColumn] = Convert.ToString(item[Constants.ODMSApplication_FIELDS_ApplicationDescription]);
                                    DTRepeatorData.Rows.Add(row);
                                }
                            }
                            if (DTRepeatorData != null && DTRepeatorData.Rows.Count > 0)
                            {
                                ApplicationDetailsRepeater.Visible = true;
                                ApplicationDetailsRepeater.DataSource = DTRepeatorData;
                                ApplicationDetailsRepeater.DataBind();
                            }

                        }
                    }

                }
                #endregion

                #region Display Req Diff Code Details
                else if (ReqDiffCodeMappingRadioButton.Checked)
                {
                    Literal RequirementNumberLiteral = e.Item.FindControl("RequirementNumberLiteral") as Literal;
                    Repeater DetailsRepeater = e.Item.FindControl("ApplicationDetailsRepeater") as Repeater;
                    HiddenField requirementKeyHiddenField = e.Item.FindControl("requirementKeyHiddenField") as HiddenField;

                    DetailsRepeater.Visible = false;



                    //get unique application ids from application mapping table
                    if (ODMSReqDiffCodeMappingDataTable != null && ODMSReqDiffCodeMappingDataTable.Rows.Count > 0)
                    {
                        //get application IDs                   
                        List<string> uniuqeMapIDs = new List<string>();
                        foreach (DataRow dr in ODMSReqDiffCodeMappingDataTable.Rows)
                        {
                            string requirementKey = Convert.ToString(dr[Constants.ODMSReqDiffCodeMapping_FIELDS_RequirementKey]).Split('.').FirstOrDefault().Trim();
                            string id = Convert.ToString(dr[Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID]).Split('.').FirstOrDefault().Trim();
                            //check requirement key datatable  in the hidden field 
                            if (requirementKey.Equals(requirementKeyHiddenField.Value.Trim()))
                            {
                                //check uniqueness in the list
                                if (!uniuqeMapIDs.Contains(id))
                                {
                                    //add to list
                                    uniuqeMapIDs.Add(id);
                                }

                            }
                        }
                        //create DT and add Coumn to Dt
                        DataTable DTRepeatorData = new DataTable();
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_FirstColumn);
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_SecondColumn);

                        if (ODMSReqDiffCodeDataTable != null && ODMSReqDiffCodeDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow item in ODMSReqDiffCodeDataTable.Rows)
                            {
                                string id = Convert.ToString(item[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID]).Split('.').FirstOrDefault();
                                if (uniuqeMapIDs.Contains(id))
                                {
                                    DataRow row = DTRepeatorData.NewRow();
                                    row[Constants.DataTable_FIELDS_FirstColumn] = Convert.ToString(item[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCode]);
                                    row[Constants.DataTable_FIELDS_SecondColumn] = Convert.ToString(item[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription]);
                                    DTRepeatorData.Rows.Add(row);
                                }
                            }
                            if (DTRepeatorData != null && DTRepeatorData.Rows.Count > 0)
                            {
                                DetailsRepeater.Visible = true;
                                DetailsRepeater.DataSource = DTRepeatorData;
                                DetailsRepeater.DataBind();
                            }

                        }
                    }

                }
                #endregion

                #region Display ODMS Value Details
                else if (ValueCriteriaRadioButton.Checked)
                {
                    Literal RequirementNumberLiteral = e.Item.FindControl("RequirementNumberLiteral") as Literal;
                    Repeater DetailsRepeater = e.Item.FindControl("ApplicationDetailsRepeater") as Repeater;
                    HiddenField requirementKeyHiddenField = e.Item.FindControl("requirementKeyHiddenField") as HiddenField;
                    DetailsRepeater.Visible = false;


                    //get unique application ids from application mapping table
                    if (ODMSValueMappingDataTable != null && ODMSValueMappingDataTable.Rows.Count > 0)
                    {
                        //get application IDs                   
                        List<string> uniuqeMapIDs = new List<string>();
                        foreach (DataRow dr in ODMSValueMappingDataTable.Rows)
                        {
                            string requirementKey = Convert.ToString(dr[Constants.ODMSValueMapping_FIELDS_RequirementKey]).Split('.').FirstOrDefault().Trim();
                            string id = Convert.ToString(dr[Constants.ODMSValueMapping_FIELDS_ValueKey]).Split('.').FirstOrDefault().Trim();
                            //check requirement key datatable  in the hidden field 
                            if (requirementKey.Equals(requirementKeyHiddenField.Value.Trim()))
                            {
                                //check uniqueness in the list
                                if (!uniuqeMapIDs.Contains(id))
                                {
                                    //add to list
                                    uniuqeMapIDs.Add(id);
                                }

                            }
                        }
                        //create DT and add Coumn to Dt
                        DataTable DTRepeatorData = new DataTable();
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_FirstColumn);
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_SecondColumn);

                        if (ODMSValueDataTable != null && ODMSValueDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow item in ODMSValueDataTable.Rows)
                            {
                                string id = Convert.ToString(item[Constants.ODMSValue_FIELDS_ValueKey]).Split('.').FirstOrDefault();
                                if (uniuqeMapIDs.Contains(id))
                                {
                                    DataRow row = DTRepeatorData.NewRow();
                                    row[Constants.DataTable_FIELDS_FirstColumn] = Convert.ToString(item[Constants.ODMSValue_FIELDS_Value]);
                                    row[Constants.DataTable_FIELDS_SecondColumn] = Convert.ToString(item[Constants.ODMSValue_FIELDS_ValueDescription]);
                                    DTRepeatorData.Rows.Add(row);
                                }
                            }
                            if (DTRepeatorData != null && DTRepeatorData.Rows.Count > 0)
                            {
                                DetailsRepeater.Visible = true;
                                DetailsRepeater.DataSource = DTRepeatorData;
                                DetailsRepeater.DataBind();
                            }

                        }
                    }

                }
                #endregion

                #region Display ODMS CoordinationCode Details
                else if (CoordinationCodeRadioButton.Checked)
                {
                    Literal RequirementNumberLiteral = e.Item.FindControl("RequirementNumberLiteral") as Literal;
                    Repeater DetailsRepeater = e.Item.FindControl("ApplicationDetailsRepeater") as Repeater;
                    HiddenField requirementKeyHiddenField = e.Item.FindControl("requirementKeyHiddenField") as HiddenField;

                    DetailsRepeater.Visible = false;


                    //get unique application ids from application mapping table
                    if (ODMSCoordinationCodeMappingDataTable != null && ODMSCoordinationCodeMappingDataTable.Rows.Count > 0)
                    {
                        //get application IDs                   
                        List<string> uniuqeMapIDs = new List<string>();
                        foreach (DataRow dr in ODMSCoordinationCodeMappingDataTable.Rows)
                        {
                            string requirementKey = Convert.ToString(dr[Constants.ODMSCoordinationCodeMapping_FIELDS_RequirementKey]).Split('.').FirstOrDefault().Trim();
                            string id = Convert.ToString(dr[Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode]).Split('.').FirstOrDefault().Trim();
                            //check requirement key datatable  in the hidden field 
                            if (requirementKey.Equals(requirementKeyHiddenField.Value.Trim()))
                            {
                                //check uniqueness in the list
                                if (!uniuqeMapIDs.Contains(id))
                                {
                                    //add to list
                                    uniuqeMapIDs.Add(id);
                                }

                            }
                        }
                        //create DT and add Coumn to Dt
                        DataTable DTRepeatorData = new DataTable();
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_FirstColumn);
                        DTRepeatorData.Columns.Add(Constants.DataTable_FIELDS_SecondColumn);

                        if (ODMSCoordinationCodeDataTable != null && ODMSCoordinationCodeDataTable.Rows.Count > 0)
                        {
                            foreach (DataRow item in ODMSCoordinationCodeDataTable.Rows)
                            {
                                string id = Convert.ToString(item[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey]).Split('.').FirstOrDefault();
                                if (uniuqeMapIDs.Contains(id))
                                {
                                    DataRow row = DTRepeatorData.NewRow();
                                    row[Constants.DataTable_FIELDS_FirstColumn] = Convert.ToString(item[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription]);
                                    row[Constants.DataTable_FIELDS_SecondColumn] = string.Empty;
                                    DTRepeatorData.Rows.Add(row);
                                }
                            }
                            if (DTRepeatorData != null && DTRepeatorData.Rows.Count > 0)
                            {
                                DetailsRepeater.Visible = true;
                                DetailsRepeater.DataSource = DTRepeatorData;
                                DetailsRepeater.DataBind();
                            }

                        }
                    }

                }
                #endregion

                #region Hide Add Requirements Details
                else if (RequirementsOnlyRadioButton.Checked)
                {
                    HyperLink AddDeleteHyperLink = e.Item.FindControl("AddDeleteHyperLink") as HyperLink;
                    AddDeleteHyperLink.Visible = false;
                }
                #endregion Hide Add Requirements Details

            }



        }
        protected void ApplicationDetailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Label _DetailHeaderLabel = (Label)e.Item.FindControl("DetailHeaderLabel");
                if (DriverDetailsRadioButton.Checked)
                {
                    _DetailHeaderLabel.Text = "Driver Details: ";
                }
                else if (ApplicationDetailsRadioButton.Checked)
                {
                    _DetailHeaderLabel.Text = "Application Details: ";
                }
                else if (ReqDiffCodeMappingRadioButton.Checked)
                {
                    _DetailHeaderLabel.Text = "Requirement Differentiation Code Details: ";
                }
                else if (ValueCriteriaRadioButton.Checked)
                {
                    _DetailHeaderLabel.Text = "Value Criteria Details: ";
                }
                else if (CoordinationCodeRadioButton.Checked)
                {
                    _DetailHeaderLabel.Text = "Coordination Code Details: ";
                }
               
            }
        }
        #endregion

        #region Driver Details

        /// <summary>
        /// get RequirementReference 
        /// </summary>
        /// <param name="requirementReferenceID"></param>       
        private DataTable getODMSDriverMappingDetail()
        {
            DataTable tempDT = new DataTable();
            if (ODMSRequirementDataTable != null && ODMSRequirementDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSDriverMappingListItemCollection(ODMSRequirementDataTable);
                if (ODMSODMSDriverMappingListItems != null)
                {
                    tempDT = ODMSODMSDriverMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }

        private DataTable getODMSDriverListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSDriverMappingDataTable != null && ODMSDriverMappingDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSDriverListItems = new ReportFinderMethods().getODMSDriverListItemCollection(ODMSDriverMappingDataTable);
                if (ODMSODMSDriverListItems != null)
                {
                    tempDT = ODMSODMSDriverListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        #region Application Details
        private DataTable getODMSApplicationMappingListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSRequirementDataTable != null && ODMSRequirementDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSApplicationMappingListItems = new ReportFinderMethods().getODMSApplicationMappingListItemCollection(ODMSRequirementDataTable);
                if (ODMSODMSApplicationMappingListItems != null)
                {
                    tempDT = ODMSODMSApplicationMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSApplicationListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSApplicationMappingDataTable != null && ODMSApplicationMappingDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSApplicationListItems = new ReportFinderMethods().getODMSApplicationListItemCollection(ODMSApplicationMappingDataTable);
                if (ODMSODMSApplicationListItems != null)
                {
                    tempDT = ODMSODMSApplicationListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        #region Req Def Code Details
        private DataTable getODMSReqDiffCodeMappingListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSRequirementDataTable != null && ODMSRequirementDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSReqDiffCodeMappingListItems = new ReportFinderMethods().getODMSReqDiffCodeMappingListItemCollection(ODMSRequirementDataTable);
                if (ODMSODMSReqDiffCodeMappingListItems != null)
                {
                    tempDT = ODMSODMSReqDiffCodeMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSReqDiffCodeListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSReqDiffCodeMappingDataTable != null && ODMSReqDiffCodeMappingDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSReqDiffCodeListItems = new ReportFinderMethods().getODMSReqDiffCodeListItemCollection(ODMSReqDiffCodeMappingDataTable);
                if (ODMSODMSReqDiffCodeListItems != null)
                {
                    tempDT = ODMSODMSReqDiffCodeListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        #region value details
        private DataTable getODMSValueMappingListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSRequirementDataTable != null && ODMSRequirementDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSValueMappingListItems = new ReportFinderMethods().getODMSValueMappingListItemCollection(ODMSRequirementDataTable);
                if (ODMSODMSValueMappingListItems != null)
                {
                    tempDT = ODMSODMSValueMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSValueListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSValueMappingDataTable != null && ODMSValueMappingDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSValueListItems = new ReportFinderMethods().getODMSValueListItemCollection(ODMSValueMappingDataTable);
                if (ODMSODMSValueListItems != null)
                {
                    tempDT = ODMSODMSValueListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        #region CoordinationCode details
        private DataTable getODMSCoordinationCodeMappingListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSRequirementDataTable != null && ODMSRequirementDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSCoordinationCodeMappingListItems = new ReportFinderMethods().getODMSCoordinationCodeMappingListItemCollection(ODMSRequirementDataTable);
                if (ODMSCoordinationCodeMappingListItems != null)
                {
                    tempDT = ODMSCoordinationCodeMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }

        private DataTable getODMSCoordinationCodeListItems()
        {
            DataTable tempDT = new DataTable();
            if (ODMSCoordinationCodeMappingDataTable != null && ODMSCoordinationCodeMappingDataTable.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSCoordinationCodeListItems = new ReportFinderMethods().getODMSCoordinationCodeListItemCollection(ODMSCoordinationCodeMappingDataTable);
                if (ODMSODMSCoordinationCodeListItems != null)
                {
                    tempDT = ODMSODMSCoordinationCodeListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        #endregion

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExporttoExcel(ODMSRequirementDataTable);
        }

        private void ExporttoExcel1(DataTable table)
        {
           /* Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;*/
        }
        private void ExporttoExcel(DataTable table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers


            HttpContext.Current.Response.Write("<TD ColSpan ='3'>Test</TD></TR>");
            HttpContext.Current.Response.Write("<TR>");
            int columnscount = 3;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                
                HttpContext.Current.Response.Write(table.Columns[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < columnscount; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }
}


