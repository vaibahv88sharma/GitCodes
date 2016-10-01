using Microsoft.SharePoint;
using Microsoft.SharePoint.Mobile.Controls;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Web.UI.WebControls;

namespace Sadara.DigiCom.ReportFinder.WebParts.AddRequirementDeatils
{
    [ToolboxItemAttribute(false)]
    public partial class AddRequirementDeatils : WebPart
    {
        #region OnInit
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AddRequirementDeatils()
        {
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        #endregion

        #region GlobalVariable
        DataTable DriverDetail = new DataTable();
        DataTable DriverDescription = new DataTable();
        DataTable DriverMappingDetail = new DataTable();
        Dictionary<string, string> DriverDetailDictionary = new Dictionary<string, string>();


        DataTable ReqDiffCodeDetailDT = new DataTable();
        DataTable GeographyDetailDT = new DataTable();
        DataTable ReqDiffCodeMappingDetailDT = new DataTable();



        DataTable CoordinationCodeDetail = new DataTable();
        DataTable CoordinationCodeMappingDetail = new DataTable();
        Dictionary<string, string> CoordinationCodeDictionary = new Dictionary<string, string>();

        DataTable ValueDetail = new DataTable();
        DataTable ValueMappingDetail = new DataTable();
        Dictionary<string, string> ValueDictionary = new Dictionary<string, string>();
        DataTable UnAssignedValueDetail = new DataTable();

        DataTable ApplicationDetail = new DataTable();
        DataTable ApplicationMappingDetail = new DataTable();
        Dictionary<string, string> ApplicationDictionary = new Dictionary<string, string>();
        DataTable UnAssignedApplicationDetail = new DataTable();

        string _REQUIREMENTNUMBER = string.Empty;
        string _REQUIREMENTKEY = string.Empty;
        string _REQUIREMENTREFERENCE = string.Empty;
        string _REQUIREMENTDESCRIPTION = string.Empty;
        string _SelectedType = string.Empty;
        //global varible for column name'
        string ColumnName = string.Empty;
        #endregion

        #region common  methods
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //get Query string values
                // _REQUIREMENTNUMBER = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTNUMBER]);
                _REQUIREMENTKEY = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTKEY]);
                // _REQUIREMENTREFERENCE = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTREFERENCE]);
                _SelectedType = Convert.ToString(Page.Request[Constants.QueryString_Type]);


                if (_SelectedType != null && _REQUIREMENTKEY != null)
                {
                    _SelectedType = _SelectedType.ToUpper();
                    setREQUIREMENDetails();
                    RequirementDescriptionLabel1.Text = _REQUIREMENTDESCRIPTION;
                    REQUIREMENTREFERENCELabel.Text = _REQUIREMENTREFERENCE;
                    REQUIREMENTNUMBERLabel.Text = _REQUIREMENTNUMBER;
                    if (!Page.IsPostBack)
                    {
                        BindData(_REQUIREMENTKEY);
                        viewDataGrid(_SelectedType);
                    }
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.PAGELOAD, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }


        }
        private void setREQUIREMENDetails()
        {
            if (_REQUIREMENTKEY != null)
            {
                SPListItemCollection requirementListItems = new ReportFinderMethods().getSingleODMSRequirement(_REQUIREMENTKEY);
                if (requirementListItems != null)
                {
                    foreach (SPListItem items in requirementListItems)
                    {
                        Constants.Requirement_Value_ID = Convert.ToString(items[Constants.ODMSREQUIREMENT_FIELDS_ID]);
                        _REQUIREMENTNUMBER = Convert.ToString(items[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER]);
                        _REQUIREMENTREFERENCE = Convert.ToString(items[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE]).Split(";#".ToArray()).LastOrDefault();
                        _REQUIREMENTDESCRIPTION = Convert.ToString(items[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION]);
                        break;
                    }

                }
            }
        }
        private void viewDataGrid(string type)
        {
            if (type.Equals(Constants.Selected_Type_Driver))
            {
                AddRequirementGridView.Visible = true;
            }
            else if (type.Equals(Constants.Selected_Type_CoordinationCode))
            {
                CoordinationCodeGridView.Visible = true;
            }
            else if (type.Equals(Constants.Selected_Type_ValueCriteria))
            {
                ViewODMSValueGridView.Visible = true;
            }
            else if (type.Equals(Constants.Selected_Type_Application))
            {
                ApplicationViewGridView.Visible = true;
            }
            else if (type.Equals(Constants.Selected_Type_ReqDiffCode))
            {
                ODMSReqDiffCodeGridView.Visible = true;
            }

        }
        private void BindData(string _REQUIREMENTKEY)
        {
            try
            {
                DataTable requirementDT = new DataTable();
                requirementDT.Columns.Add(Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY);

                if (_REQUIREMENTKEY != null)
                {
                    DataRow de = requirementDT.NewRow();
                    de[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                    requirementDT.Rows.Add(de);
                }

                if (_SelectedType.Equals(Constants.Selected_Type_Driver))
                {
                    //get driver mapping Detail
                    DriverMappingDetail = getODMSDriverMappingDetail(requirementDT);
                    List<string> uniqueDriverIDs = new List<string>();

                    //get unique driver IDs
                    if (DriverMappingDetail != null && DriverMappingDetail.Rows.Count > 0)
                    {
                        foreach (DataRow applicationRow in DriverMappingDetail.Rows)
                        {
                            string val = Convert.ToString(applicationRow[Constants.ODMSDriverMapping_FIELDS_DriverID]).Split('.').FirstOrDefault();
                            if (!uniqueDriverIDs.Contains(val))
                            {
                                uniqueDriverIDs.Add(val);
                            }
                        }
                    }

                    DriverDetail = getODMSDriverDetailItens();
                    DriverDescription = getODMSDriverDescriptions();

                    AddRequirementGridView.DataSource = GetDataTableToBind(uniqueDriverIDs);
                    AddRequirementGridView.DataBind();

                }
                else if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
                {
                    //get driver mapping Detail
                    ReqDiffCodeMappingDetailDT = getODMSReqDiffCodeMappingDetail(requirementDT);

                    ReqDiffCodeDetailDT = getODMSReqDiffCodeAllItens();
                    GeographyDetailDT = getODMSGeographyAllItens();

                    ODMSReqDiffCodeGridView.DataSource = GetReqDiffCodeDataTableToBind();
                    ODMSReqDiffCodeGridView.DataBind();

                }
                else if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                {
                    //get Coordination mapping Detail
                    DataTable CoordinationMappingDetail = getODMSCoordinationcodeMappingDetail(requirementDT);
                    List<string> uniqueCoordinationcodeIDs = new List<string>();
                    //get unique driver IDs
                    if (CoordinationMappingDetail != null && CoordinationMappingDetail.Rows.Count > 0)
                    {
                        foreach (DataRow Row in CoordinationMappingDetail.Rows)
                        {
                            string val = Convert.ToString(Row[Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode]).Split('.').FirstOrDefault();
                            if (!uniqueCoordinationcodeIDs.Contains(val))
                            {
                                uniqueCoordinationcodeIDs.Add(val);
                            }
                        }
                    }
                    CoordinationCodeDetail = getODMSCoordinationcodeDetailAllItens();

                    CoordinationCodeGridView.DataSource = GetCoordinationCodeDataTableToBind(uniqueCoordinationcodeIDs);
                    CoordinationCodeGridView.DataBind();
                }
                else if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
                {
                    //get Coordination mapping Detail
                    DataTable ValueMappingDetail = getODMSValueMappingDetail(requirementDT);
                    List<string> uniqueODMSValuesIDs = new List<string>();
                    //get unique driver IDs
                    if (ValueMappingDetail != null && ValueMappingDetail.Rows.Count > 0)
                    {
                        foreach (DataRow Row in ValueMappingDetail.Rows)
                        {
                            string val = Convert.ToString(Row[Constants.ODMSValueMapping_FIELDS_ValueKey]).Split('.').FirstOrDefault();
                            if (!uniqueODMSValuesIDs.Contains(val))
                            {
                                uniqueODMSValuesIDs.Add(val);
                            }
                        }
                    }
                    ValueDetail = getODMSValueDetailAllItens();
                    ViewODMSValueGridView.DataSource = GetODMSValueDataTableToBind(uniqueODMSValuesIDs);
                    ViewODMSValueGridView.DataBind();
                }
                else if (_SelectedType.Equals(Constants.Selected_Type_Application))
                {
                    //get Coordination mapping Detail
                    DataTable ApplicationMappingDetail = getODMSApplicationMappingDetail(requirementDT);
                    List<string> uniqueODMSApplicationIDs = new List<string>();
                    //get unique driver IDs
                    if (ApplicationMappingDetail != null && ApplicationMappingDetail.Rows.Count > 0)
                    {
                        foreach (DataRow Row in ApplicationMappingDetail.Rows)
                        {
                            string val = Convert.ToString(Row[Constants.ODMSApplicationMapping_FIELDS_ApplicationID]).Split('.').FirstOrDefault();
                            if (!uniqueODMSApplicationIDs.Contains(val))
                            {
                                uniqueODMSApplicationIDs.Add(val);
                            }
                        }
                    }
                    ApplicationDetail = getODMSApplicationDetailAllItens();
                    ApplicationViewGridView.DataSource = GetODMSApplicatinDataTableToBind(uniqueODMSApplicationIDs);
                    ApplicationViewGridView.DataBind();
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DATABINDTOGrid, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }


        }
        #endregion

        #region get Driver Details
        private DataTable GetDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_DetailDescription);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ODMSDriverListID);


            if (_SelectedType.Equals(Constants.Selected_Type_Driver))
            {
                if (DriverDetail != null && DriverDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in DriverDetail.Rows)
                    {
                        string DriverID = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverID]);
                        string DriverDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverDescription]);
                        string DetailedDriverDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DetailedDriverDescription]);
                        string ODMSDriverListID = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_ID]);
                        if (_uniqueDriverIDs.Contains(DriverID))
                        {
                            DataRow newrow = RequirementDataTable.NewRow();
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                            newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                            newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                            newrow[Constants.DataTable_FIELDS_DetailDescription] = DetailedDriverDescription;
                            newrow[Constants.DataTable_FIELDS_ODMSDriverListID] = ODMSDriverListID;
                            RequirementDataTable.Rows.Add(newrow);
                        }
                        else
                        {
                            DriverDetailDictionary.Add(DriverID, DriverDescription);
                        }

                    }
                }
            }


            return RequirementDataTable;
        }
        private DataTable getODMSDriverDetailItens()
        {
            DataTable tempDT = new DataTable();
            if (DriverMappingDetail != null)
            {
                SPListItemCollection ODMSODMSDriverListItems = new ReportFinderMethods().getODMSDriverListItemCollection(DriverMappingDetail);
                if (ODMSODMSDriverListItems != null)
                {
                    tempDT = ODMSODMSDriverListItems.GetDataTable();
                }

            }
            return tempDT;

        }
        private DataTable getODMSDriverDescriptions()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSDriverDescriptionListItems = new ReportFinderMethods().getODMSDriverDescriptionListItemCollection();
            if (ODMSODMSDriverDescriptionListItems != null)
            {
                tempDT = ODMSODMSDriverDescriptionListItems.GetDataTable();
            }
            return tempDT;

        }
        private DataTable getODMSDriverMappingDetail(DataTable requirementDT)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSDriverMappingListItemCollection(requirementDT);
                if (ODMSODMSDriverMappingListItems != null)
                {
                    tempDT = ODMSODMSDriverMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        protected void Add(object sender, EventArgs e)
        {
            try
            {

                Control control = null;
                if (AddRequirementGridView.FooterRow != null)
                {
                    control = AddRequirementGridView.FooterRow;
                }
                else
                {
                    control = AddRequirementGridView.Controls[0].Controls[0];
                }

                string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;
                string DriverDetailDescription = (control.FindControl("RequirementDescrtiptionLabelTextBox") as System.Web.UI.WebControls.TextBox).Text;
                DropDownList _DriverDropDownList = (control.FindControl("RequirementTypeDropDownList") as System.Web.UI.WebControls.DropDownList);
                if (_SelectedType.Equals(Constants.Selected_Type_Driver))
                {
                    int DriverKey = 1;
                    //find max driver key
                    SPListItemCollection DriverList = Utility.GetMaxKeyFromList(Constants.LIST_NAME_ODMSDriver, Constants.ODMSDriver_FIELDS_DriverID);
                    foreach (SPListItem item in DriverList)
                    {
                        DriverKey = Convert.ToInt32(Convert.ToString(item[Constants.ODMSDriver_FIELDS_DriverID])) + 1;
                        break;
                    }
                    //check entry exists in ODMSDriver
                    SPListItemCollection DuplicateDriverList = new ReportFinderMethods().GetDuplicateDriver(_DriverDropDownList.SelectedItem.Text, DriverDetailDescription);

                    if (DuplicateDriverList == null || DuplicateDriverList.Count == 0)
                    {
                        //add entry in driver
                        int ID = new ReportFinderMethods().AddODMSDriverToDriver(DriverKey.ToString(), Convert.ToString(_DriverDropDownList.SelectedItem.Text), Convert.ToString(_DriverDropDownList.SelectedItem.Value), DriverDetailDescription);
                        //add entry in Driver Mapping
                        new ReportFinderMethods().AddODMSDriver(Convert.ToString(_REQUIREMENTKEYHiddenField), DriverKey.ToString(), ID);
                    }
                    else
                    {
                        ErrorLabel.Text = "Duplicate driver details";
                    }
                }
                //add record to list
                Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }

        }
        protected void AddRequirementGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (AddRequirementGridView.EditIndex == e.Row.RowIndex) //GET THE ROW TO BE EDITED
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DropDownList RequirementTypeDropDownList = (DropDownList)e.Row.FindControl("RequirementTypeDropDownList");


                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");

                    if (_SelectedType.Equals(Constants.Selected_Type_Driver))
                    {

                        RequirementTypeDropDownList.DataSource = DriverDescription;
                        RequirementTypeDropDownList.DataTextField = Constants.ODMSDriverDescription_FIELDS_DriverDescription;
                        RequirementTypeDropDownList.DataValueField = Constants.ODMSDriverDescription_FIELDS_ID;
                        RequirementTypeDropDownList.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;

                }
                else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    DropDownList RequirementTypeDropDownList = (DropDownList)e.Row.FindControl("RequirementTypeDropDownList");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");

                    if (_SelectedType.Equals(Constants.Selected_Type_Driver))
                    {
                        RequirementTypeDropDownList.DataSource = DriverDescription;
                        RequirementTypeDropDownList.DataTextField = Constants.ODMSDriverDescription_FIELDS_DriverDescription;
                        RequirementTypeDropDownList.DataValueField = Constants.ODMSDriverDescription_FIELDS_ID;
                        RequirementTypeDropDownList.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;

                }
            }
        }
        protected void AddRequirementGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteRow")
                {
                    //incase you need the row index 
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);
                    System.Web.UI.WebControls.HiddenField _REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("REQUIREMENTKEYHiddenField");
                    System.Web.UI.WebControls.HiddenField _DriverIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    System.Web.UI.WebControls.HiddenField _ODMSDriverListIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("ODMSDriverListIDHiddenField");

                    if (_SelectedType.Equals(Constants.Selected_Type_Driver))
                    {
                        //delete entry from Driver
                        new ReportFinderMethods().DeleteODMSDriverFromDriverList(_ODMSDriverListIDHiddenField.Value);
                        //delete entry from driver Mapping
                        //  int Prod_Id = Convert.ToInt32(e.CommandArgument);
                        new ReportFinderMethods().DeleteODMSDriver(Convert.ToString(_REQUIREMENTKEYHiddenField.Value), Convert.ToString(_DriverIDHiddenField.Value));
                    }
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DELETEMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }

        }
        #endregion

        #region REQ Diff Code
        private DataTable GetReqDiffCodeDataTableToBind()
        {
            DataTable ReqDiffCodeDataTable = new DataTable();
            ReqDiffCodeDataTable.Columns.Add();
            ReqDiffCodeDataTable.Columns.Add();
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);

            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_ReqDiffCodeDescription);
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_ReqDiffCodeID);
            ReqDiffCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_Geography);

            if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
            {   //get unique driver IDs
                if (ReqDiffCodeMappingDetailDT != null && ReqDiffCodeMappingDetailDT.Rows.Count > 0)
                {
                    foreach (DataRow applicationRow in ReqDiffCodeMappingDetailDT.Rows)
                    {

                        string Geography = Convert.ToString(applicationRow[Constants.ODMSReqDiffCodeMapping_FIELDS_Geography_Lookup]);
                        string ID = Convert.ToString(applicationRow[Constants.ODMSReqDiffCodeMapping_FIELDS_ID]);
                        string ReqDiffCodeID = Convert.ToString(applicationRow[Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID]);
                        string ReqDiffCodeDescription = string.Empty;
                        if (ReqDiffCodeDetailDT != null)
                        {
                            //get Req Diff Code Description
                            ReqDiffCodeDescription = (from DataRow dr in ReqDiffCodeDetailDT.Rows
                                                      where Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID]).Trim().Equals(ReqDiffCodeID.Trim())
                                                      select Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription])).FirstOrDefault();
                        }
                        DataRow newrow = ReqDiffCodeDataTable.NewRow();

                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;


                        newrow[Constants.DataTable_FIELDS_ReqDiffCodeDescription] = ReqDiffCodeDescription;
                        newrow[Constants.DataTable_FIELDS_ReqDiffCodeID] = ReqDiffCodeID;
                        newrow[Constants.DataTable_FIELDS_Geography] = Geography;
                        newrow[Constants.DataTable_FIELDS_ID] = ID;
                        ReqDiffCodeDataTable.Rows.Add(newrow);
                    }
                }
            }

            return ReqDiffCodeDataTable;
        }
        private DataTable getODMSReqDiffCodeAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSReqDiffCodeAllListItems = new ReportFinderMethods().getODMSReqDiffCodeListAllItemCollection();
            if (ODMSReqDiffCodeAllListItems != null)
            {
                tempDT = ODMSReqDiffCodeAllListItems.GetDataTable();
            }
            return tempDT;

        }
        private DataTable getODMSReqDiffCodeMappingDetail(DataTable requirementDT)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSReqDiffCodeMappingListItems = new ReportFinderMethods().getODMSReqDiffCodeMappingListItemCollection(requirementDT);
                if (ODMSODMSReqDiffCodeMappingListItems != null)
                {
                    tempDT = ODMSODMSReqDiffCodeMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSGeographyAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSGeographyAllListItems = new ReportFinderMethods().getODMSGeographyListAllItemCollection();
            if (ODMSGeographyAllListItems != null)
            {
                tempDT = ODMSGeographyAllListItems.GetDataTable();
            }
            return tempDT;

        }
        protected void ODMSReqDiffCodeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GET THE ROW TO BE EDITED
            if (ODMSReqDiffCodeGridView.EditIndex == e.Row.RowIndex)
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    DropDownList GeographyDropDownList = (DropDownList)e.Row.FindControl("GeographyDropDownList");
                    DropDownList CompliancePlaneCategoryDropDownList = (DropDownList)e.Row.FindControl("CompliancePlaneCategoryDropDownList");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");

                    if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
                    {
                        if (GeographyDetailDT != null)
                        {
                            GeographyDropDownList.DataSource = GeographyDetailDT;
                            GeographyDropDownList.DataTextField = Constants.ODMSGeography_FIELDS_Geography;
                            GeographyDropDownList.DataValueField = Constants.ODMSGeography_FIELDS_ID;
                            GeographyDropDownList.DataBind();
                        }
                        if (ReqDiffCodeDetailDT != null)
                        {


                            CompliancePlaneCategoryDropDownList.DataSource = ReqDiffCodeDetailDT;
                            CompliancePlaneCategoryDropDownList.DataTextField = Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID;
                            CompliancePlaneCategoryDropDownList.DataValueField = Constants.ODMSReqDiffCode_FIELDS_ID;
                            CompliancePlaneCategoryDropDownList.DataBind();
                        }
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;
                }
                else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    DropDownList GeographyDropDownList = (DropDownList)e.Row.FindControl("GeographyDropDownList");
                    DropDownList CompliancePlaneCategoryDropDownList = (DropDownList)e.Row.FindControl("CompliancePlaneCategoryDropDownList");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");

                    if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
                    {
                        if (GeographyDetailDT != null)
                        {
                            GeographyDropDownList.DataSource = GeographyDetailDT;
                            GeographyDropDownList.DataTextField = Constants.ODMSGeography_FIELDS_Geography;
                            GeographyDropDownList.DataValueField = Constants.ODMSGeography_FIELDS_ID;
                            GeographyDropDownList.DataBind();
                        }
                        if (ReqDiffCodeDetailDT != null)
                        {
                            CompliancePlaneCategoryDropDownList.DataSource = ReqDiffCodeDetailDT;
                            CompliancePlaneCategoryDropDownList.DataTextField = Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID;
                            CompliancePlaneCategoryDropDownList.DataValueField = Constants.ODMSReqDiffCode_FIELDS_ID;
                            CompliancePlaneCategoryDropDownList.DataBind();
                        }
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;
                }
            }
        }
        protected void AddReqDiffCode(object sender, EventArgs e)
        {
            try
            {
                Control control = null;
                if (ODMSReqDiffCodeGridView.FooterRow != null)
                {
                    control = ODMSReqDiffCodeGridView.FooterRow;
                }
                else
                {
                    control = ODMSReqDiffCodeGridView.Controls[0].Controls[0];
                }

                string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;

                DropDownList _GeographyDropDownList = (control.FindControl("GeographyDropDownList") as System.Web.UI.WebControls.DropDownList);
                DropDownList _CompliancePlaneCategoryDropDownList = (control.FindControl("CompliancePlaneCategoryDropDownList") as System.Web.UI.WebControls.DropDownList);
                System.Web.UI.WebControls.Label ErrorLabel = (control.FindControl("ErrorLabel") as System.Web.UI.WebControls.Label);
                if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
                {
                    string GeographyID = Convert.ToString(_GeographyDropDownList.SelectedValue);
                    string Geography = Convert.ToString(_GeographyDropDownList.SelectedItem.Text);

                    string ReqDiffCodeID = Convert.ToString(_CompliancePlaneCategoryDropDownList.SelectedItem.Value);
                    string ReqDiffCode = Convert.ToString(_CompliancePlaneCategoryDropDownList.SelectedItem.Text);

                    //add entry in ReqDiffcode mapping
                    SPListItemCollection ReqDiffcodeitems = new ReportFinderMethods().GetODMSReqDiffMappingMapping(_REQUIREMENTKEYHiddenField, Geography, ReqDiffCode);
                    if (ReqDiffcodeitems.Count == 0)
                    {
                        //add entry in Driver Mapping
                        new ReportFinderMethods().AddODMSReqDiffCodeMapping(_REQUIREMENTKEYHiddenField, GeographyID, Geography, ReqDiffCodeID, ReqDiffCode);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "Entry already exists.";
                    }
                }
                //add record to list
                Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void ODMSReqDiffCodeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteRow")
                {
                    //incase you need the row index 
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);
                    System.Web.UI.WebControls.HiddenField _REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("REQUIREMENTKEYHiddenField");
                    System.Web.UI.WebControls.HiddenField _ReqDiffCodeMappingIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_ReqDiffCode))
                    {
                        //  int Prod_Id = Convert.ToInt32(e.CommandArgument);
                        new ReportFinderMethods().DeleteODMSReqDiffCodeMapping(Convert.ToString(_ReqDiffCodeMappingIDHiddenField.Value));
                    }
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DELETEMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        #endregion

        #region Coordinationcode Details
        private DataTable GetCoordinationCodeDataTableToBind(List<string> uniqueCoordinationcodeIDs)
        {
            DataTable CoordinationCodeDataTable = new DataTable();
            CoordinationCodeDataTable.Columns.Add();
            CoordinationCodeDataTable.Columns.Add();
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            CoordinationCodeDataTable.Columns.Add(Constants.DataTable_FIELDS_DetailDescription);

            if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
            {
                if (CoordinationCodeDetail != null && CoordinationCodeDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in CoordinationCodeDetail.Rows)
                    {
                        string CoordinationCodeID = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey]);
                        string ID = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_ID]);
                        string CoordinationCodeDescription = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription]);

                        if (uniqueCoordinationcodeIDs.Contains(CoordinationCodeID))
                        {
                            DataRow newrow = CoordinationCodeDataTable.NewRow();
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                            newrow[Constants.DataTable_FIELDS_ID] = CoordinationCodeID;
                            newrow[Constants.DataTable_FIELDS_Name] = CoordinationCodeDescription;
                            newrow[Constants.DataTable_FIELDS_DetailDescription] = string.Empty;
                            CoordinationCodeDataTable.Rows.Add(newrow);
                        }
                        else
                        {
                            string val = CoordinationCodeDescription;
                            CoordinationCodeDictionary.Add(ID + "-" + CoordinationCodeID, val);
                        }

                    }
                }
            }


            return CoordinationCodeDataTable;
        }
        private DataTable getODMSCoordinationcodeMappingDetail(DataTable requirementDT)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSCoordinationcodeMappingListItems = new ReportFinderMethods().getODMSCoordinationCodeMappingListItemCollection(requirementDT);
                if (ODMSCoordinationcodeMappingListItems != null)
                {
                    tempDT = ODMSCoordinationcodeMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSCoordinationcodeDetailAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSCoordinationcodeListItems = new ReportFinderMethods().getODMSCoordinationcodeListAllItemCollection();
            if (ODMSODMSCoordinationcodeListItems != null)
            {
                tempDT = ODMSODMSCoordinationcodeListItems.GetDataTable();
            }
            return tempDT;

        }
        protected void CoordinationCodeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (CoordinationCodeGridView.EditIndex == e.Row.RowIndex) //GET THE ROW TO BE EDITED
            {

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    GridView CoordinationCodeTypeGridView = (GridView)e.Row.FindControl("CoordinationCodeTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                    {
                        //List<string> Drivername = Utility.GetChoiceFieldValues(Constants.LIST_NAME_ODMSDriver, Constants.ODMSDriver_FIELDS_DriverDescription_DisplayName);


                        CoordinationCodeTypeGridView.DataSource = CoordinationCodeDictionary;
                        CoordinationCodeTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;

                }
                else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    GridView CoordinationCodeTypeGridView = (GridView)e.Row.FindControl("CoordinationCodeTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                    {
                        //List<string> Drivername = Utility.GetChoiceFieldValues(Constants.LIST_NAME_ODMSDriver, Constants.ODMSDriver_FIELDS_DriverDescription_DisplayName);
                        // var data = CoordinationCodeDictionary.Select(x => new List<object> { x.Key, x.Value });
                        CoordinationCodeTypeGridView.DataSource = CoordinationCodeDictionary;
                        CoordinationCodeTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;
                }
            }
        }
        protected void CoordinationCodeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteRow")
                {
                    //incase you need the row index 
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);

                    System.Web.UI.WebControls.HiddenField _REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("REQUIREMENTKEYHiddenField");
                    System.Web.UI.WebControls.HiddenField _CordinationIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                    {
                        //  int Prod_Id = Convert.ToInt32(e.CommandArgument);
                        new ReportFinderMethods().DeleteODMSCoordinationCodeMapping(Convert.ToString(_REQUIREMENTKEYHiddenField.Value), Convert.ToString(_CordinationIDHiddenField.Value));
                    }
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DELETEMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void CoordinationCodeTypeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Footer")
                {

                    Control control = null;
                    if (CoordinationCodeGridView.FooterRow != null)
                    {
                        control = CoordinationCodeGridView.FooterRow;
                    }
                    else
                    {
                        control = CoordinationCodeGridView.Controls[0].Controls[0];
                    }


                    string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;
                    //  DropDownList _CoordinationCodeDownList = (control.FindControl("CoordinationCodeDropDownList") as System.Web.UI.WebControls.DropDownList);
                    string CoordinationCodeIDs = Convert.ToString(e.CommandArgument);
                    if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                    {
                        var CCID = CoordinationCodeIDs.Split('-').FirstOrDefault();
                        var CoordinationCode = CoordinationCodeIDs.Split('-').LastOrDefault();
                        //add entry in Driver Mapping
                        new ReportFinderMethods().AddODMSCoordinationMapping(Convert.ToString(_REQUIREMENTKEYHiddenField), CoordinationCode, CCID);
                    }
                    //add record to list
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void AddCordinationCode(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Control control = null;
                if (CoordinationCodeGridView.FooterRow != null)
                {
                    control = CoordinationCodeGridView.FooterRow;
                }
                else
                {
                    control = CoordinationCodeGridView.Controls[0].Controls[0];
                }

                string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;
                //  DropDownList _CoordinationCodeDownList = (control.FindControl("CoordinationCodeDropDownList") as System.Web.UI.WebControls.DropDownList);
                string CoordinationCodeIDs = Convert.ToString(e.CommandArgument);
                if (_SelectedType.Equals(Constants.Selected_Type_CoordinationCode))
                {
                    var CCID = CoordinationCodeIDs.Split('-').FirstOrDefault();
                    var CoordinationCode = CoordinationCodeIDs.Split('-').LastOrDefault();
                    //add entry in Driver Mapping
                    new ReportFinderMethods().AddODMSCoordinationMapping(Convert.ToString(_REQUIREMENTKEYHiddenField), CoordinationCode, CCID);
                }
                //add record to list
                Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        #endregion

        #region get ODMSValue Details
        private DataTable GetODMSValueDataTableToBind(List<string> uniqueValueIDs)
        {
            DataTable valueDeatailsDataTable = new DataTable();
            valueDeatailsDataTable.Columns.Add();
            valueDeatailsDataTable.Columns.Add();
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            valueDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_DetailDescription);


            //add column for UnAssigned Value Detail to requirement     
            UnAssignedValueDetail.Columns.Add(Constants.DataTable_FIELDS_ID);
            UnAssignedValueDetail.Columns.Add(Constants.DataTable_FIELDS_Value);
            UnAssignedValueDetail.Columns.Add(Constants.DataTable_FIELDS_ValueId);
            UnAssignedValueDetail.Columns.Add(Constants.DataTable_FIELDS_ValueDescription);

            if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
            {
                if (ValueDetail != null && ValueDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in ValueDetail.Rows)
                    {
                        string ValueID = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueKey]);
                        string ID = Convert.ToString(dr[Constants.ODMSValueMapping_FIELDS_ID]);
                        string ValueDescription = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueDescription]);
                        string Value = Convert.ToString(dr[Constants.ODMSValue_FIELDS_Value]);

                        if (uniqueValueIDs.Contains(ValueID))
                        {
                            DataRow newrow = valueDeatailsDataTable.NewRow();
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                            newrow[Constants.DataTable_FIELDS_ID] = ValueID;
                            newrow[Constants.DataTable_FIELDS_Name] = Value;
                            newrow[Constants.DataTable_FIELDS_DetailDescription] = ValueDescription;
                            valueDeatailsDataTable.Rows.Add(newrow);
                        }
                        else
                        {
                            DataRow newrow = UnAssignedValueDetail.NewRow();
                            newrow[Constants.DataTable_FIELDS_ID] = ID;
                            newrow[Constants.DataTable_FIELDS_Value] = Value;
                            newrow[Constants.DataTable_FIELDS_ValueDescription] = ValueDescription;
                            newrow[Constants.DataTable_FIELDS_ValueId] = ValueID;
                            UnAssignedValueDetail.Rows.Add(newrow);
                        }

                    }
                }
            }
            return valueDeatailsDataTable;
        }
        private DataTable getODMSValueMappingDetail(DataTable requirementDT)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSValueMappingListItems = new ReportFinderMethods().getODMSValueMappingListItemCollection(requirementDT);
                if (ODMSValueMappingListItems != null)
                {
                    tempDT = ODMSValueMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSValueDetailAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSValueListItems = new ReportFinderMethods().getODMSValueListAllItemCollection();
            if (ODMSODMSValueListItems != null)
            {
                tempDT = ODMSODMSValueListItems.GetDataTable();
            }
            return tempDT;
        }
        protected void ViewODMSValueGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ViewODMSValueGridView.EditIndex == e.Row.RowIndex) //GET THE ROW TO BE EDITED
            {

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    GridView ODMSValueTypeGridView = (GridView)e.Row.FindControl("ODMSValueTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
                    {
                        ODMSValueTypeGridView.DataSource = UnAssignedValueDetail;
                        ODMSValueTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;

                }
                else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    GridView ODMSValueTypeGridView = (GridView)e.Row.FindControl("ODMSValueTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
                    {
                        ODMSValueTypeGridView.DataSource = UnAssignedValueDetail;
                        ODMSValueTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;
                }
            }
        }
        protected void ViewODMSValueGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteRow")
                {
                    //incase you need the row index 
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);

                    System.Web.UI.WebControls.HiddenField _REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("REQUIREMENTKEYHiddenField");
                    System.Web.UI.WebControls.HiddenField _ValueIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
                    {
                        //  int Prod_Id = Convert.ToInt32(e.CommandArgument);
                        new ReportFinderMethods().DeleteODMSValueMapping(Convert.ToString(_REQUIREMENTKEYHiddenField.Value), Convert.ToString(_ValueIDHiddenField.Value));
                    }
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DELETEMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void ODMSValueTypeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Footer")
                {

                    Control control = null;
                    if (ViewODMSValueGridView.FooterRow != null)
                    {
                        control = ViewODMSValueGridView.FooterRow;
                    }
                    else
                    {
                        control = ViewODMSValueGridView.Controls[0].Controls[0];
                    }
                    string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;
                    //  DropDownList _CoordinationCodeDownList = (control.FindControl("CoordinationCodeDropDownList") as System.Web.UI.WebControls.DropDownList);
                    GridViewRow row = ((GridViewRow)((Button)e.CommandSource).NamingContainer);
                    System.Web.UI.WebControls.HiddenField IDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    System.Web.UI.WebControls.HiddenField ValueIDField = (System.Web.UI.WebControls.HiddenField)row.FindControl("ValueIDField");
                    string CoordinationCodeIDs = Convert.ToString(e.CommandArgument);
                    if (_SelectedType.Equals(Constants.Selected_Type_ValueCriteria))
                    {

                        //add entry in Driver Mapping
                        new ReportFinderMethods().AddODMSValueMapping(Convert.ToString(_REQUIREMENTKEYHiddenField), ValueIDField.Value, IDHiddenField.Value);
                    }
                    //add record to list
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        #endregion

        #region ODMSApplication Details
        private DataTable GetODMSApplicatinDataTableToBind(List<string> uniqueApplicationIDs)
        {
            DataTable ApplicationDeatailsDataTable = new DataTable();
            ApplicationDeatailsDataTable.Columns.Add();
            ApplicationDeatailsDataTable.Columns.Add();
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            ApplicationDeatailsDataTable.Columns.Add(Constants.DataTable_FIELDS_DetailDescription);


            //add column for UnAssigned Value Detail to requirement     
            UnAssignedApplicationDetail.Columns.Add(Constants.DataTable_FIELDS_ID);
            UnAssignedApplicationDetail.Columns.Add(Constants.DataTable_FIELDS_ApplicationCatigory);
            UnAssignedApplicationDetail.Columns.Add(Constants.DataTable_FIELDS_ApplicationDescription);
            UnAssignedApplicationDetail.Columns.Add(Constants.DataTable_FIELDS_ApplicationId);

            if (_SelectedType.Equals(Constants.Selected_Type_Application))
            {
                if (ApplicationDetail != null && ApplicationDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in ApplicationDetail.Rows)
                    {
                        string ApplicationID = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationID]);
                        string ID = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ID]);
                        string ApplicationDescription = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationDescription]);
                        string ApplicationCatigory = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationCategory]);

                        if (uniqueApplicationIDs.Contains(ApplicationID))
                        {
                            DataRow newrow = ApplicationDeatailsDataTable.NewRow();
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                            newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                            newrow[Constants.DataTable_FIELDS_ID] = ApplicationID;
                            newrow[Constants.DataTable_FIELDS_Name] = ApplicationCatigory;
                            newrow[Constants.DataTable_FIELDS_DetailDescription] = ApplicationDescription;
                            ApplicationDeatailsDataTable.Rows.Add(newrow);
                        }
                        else
                        {
                            DataRow newrow = UnAssignedApplicationDetail.NewRow();
                            newrow[Constants.DataTable_FIELDS_ID] = ID;
                            newrow[Constants.DataTable_FIELDS_ApplicationCatigory] = ApplicationCatigory;
                            newrow[Constants.DataTable_FIELDS_ApplicationDescription] = ApplicationDescription;
                            newrow[Constants.DataTable_FIELDS_ApplicationId] = ApplicationID;
                            UnAssignedApplicationDetail.Rows.Add(newrow);
                        }

                    }
                }
            }
            return ApplicationDeatailsDataTable;
        }
        private DataTable getODMSApplicationMappingDetail(DataTable requirementDT)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSApplicationMappingListItems = new ReportFinderMethods().getODMSApplicationMappingListItemCollection(requirementDT);
                if (ODMSApplicationMappingListItems != null)
                {
                    tempDT = ODMSApplicationMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }
        private DataTable getODMSApplicationDetailAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSApplicationListItems = new ReportFinderMethods().getODMSApplicationListAllItemCollection();
            if (ODMSODMSApplicationListItems != null)
            {
                tempDT = ODMSODMSApplicationListItems.GetDataTable();
            }
            return tempDT;
        }
        protected void ApplicationViewGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ApplicationViewGridView.EditIndex == e.Row.RowIndex) //GET THE ROW TO BE EDITED
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    GridView ODMSApplicationTypeGridView = (GridView)e.Row.FindControl("ODMSApplicationTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_Application))
                    {
                        ODMSApplicationTypeGridView.DataSource = UnAssignedApplicationDetail;
                        ODMSApplicationTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;

                }
                else if (e.Row.RowType == DataControlRowType.EmptyDataRow)
                {
                    GridView ODMSApplicationTypeGridView = (GridView)e.Row.FindControl("ODMSApplicationTypeGridView");
                    System.Web.UI.WebControls.HiddenField REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("REQUIREMENTKEYHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_Application))
                    {
                        ODMSApplicationTypeGridView.DataSource = UnAssignedApplicationDetail;
                        ODMSApplicationTypeGridView.DataBind();
                    }
                    REQUIREMENTKEYHiddenField.Value = _REQUIREMENTKEY;
                }
            }
        }
        protected void ApplicationViewGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteRow")
                {
                    //incase you need the row index 
                    int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    GridViewRow row = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer);

                    System.Web.UI.WebControls.HiddenField _REQUIREMENTKEYHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("REQUIREMENTKEYHiddenField");
                    System.Web.UI.WebControls.HiddenField _ApplicationIDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    if (_SelectedType.Equals(Constants.Selected_Type_Application))
                    {
                        //  int Prod_Id = Convert.ToInt32(e.CommandArgument);
                        new ReportFinderMethods().DeleteODMSApplicationMapping(Convert.ToString(_REQUIREMENTKEYHiddenField.Value), Convert.ToString(_ApplicationIDHiddenField.Value));
                    }
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.DELETEMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        protected void ODMSApplicationTypeGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Footer")
                {

                    Control control = null;
                    if (ApplicationViewGridView.FooterRow != null)
                    {
                        control = ApplicationViewGridView.FooterRow;
                    }
                    else
                    {
                        control = ApplicationViewGridView.Controls[0].Controls[0];
                    }
                    string _REQUIREMENTKEYHiddenField = (control.FindControl("REQUIREMENTKEYHiddenField") as System.Web.UI.WebControls.HiddenField).Value;
                    GridViewRow row = ((GridViewRow)((Button)e.CommandSource).NamingContainer);
                    System.Web.UI.WebControls.HiddenField IDHiddenField = (System.Web.UI.WebControls.HiddenField)row.FindControl("IDHiddenField");
                    System.Web.UI.WebControls.HiddenField ApplicationIDField = (System.Web.UI.WebControls.HiddenField)row.FindControl("ApplicationIDField");
                    string CoordinationCodeIDs = Convert.ToString(e.CommandArgument);
                    if (_SelectedType.Equals(Constants.Selected_Type_Application))
                    {
                        //add entry in Driver Mapping
                        new ReportFinderMethods().AddODMSApplicationMapping(Convert.ToString(_REQUIREMENTKEYHiddenField), ApplicationIDField.Value, IDHiddenField.Value);
                    }
                    //add record to list
                    Page.Response.Redirect(Page.Request.Url.AbsoluteUri);
                }
            }
            catch (Exception Exp)
            {
                //Logging the error in ULS LOGS
                ODMSLogging.LogError(Constants.CATEGORYNAME, Constants.ADDMETHODE, Constants.AddRequirementDeatils, Exp.Message + "------" + Exp.StackTrace + "------" + Exp.ToString() + "-----" + Exp.InnerException);
                ScriptLiteral.Text = "Error occured in page.";

            }
        }
        #endregion












    }
}











