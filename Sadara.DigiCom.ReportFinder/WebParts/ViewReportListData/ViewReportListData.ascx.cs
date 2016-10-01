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

namespace Sadara.DigiCom.ReportFinder.WebParts.ViewReportListData
{
    [ToolboxItemAttribute(false)]
    public partial class ViewReportListData : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ViewReportListData()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region GlobalVariable
        DataTable DriverDetail = new DataTable();
        Dictionary<string, string> DriverDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ValueDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ApplicationDetailDictionary = new Dictionary<string, string>();
        string _REQUIREMENTNUMBER = string.Empty;
        string _REQUIREMENTKEY = string.Empty;
        string _REQUIREMENTREFERENCE = string.Empty;
        string _SelectedType = string.Empty;
        //global varible for column name'
        string ColumnName = string.Empty;


        DataTable ValueDetail = new DataTable();
        DataTable ApplicationDetail = new DataTable();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //get Query string values
            _REQUIREMENTNUMBER = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTNUMBER]);
            _REQUIREMENTKEY = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTKEY]);
            _REQUIREMENTREFERENCE = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTREFERENCE]);
            _SelectedType = Convert.ToString(Page.Request[Constants.QueryString_Type]);

            // BindData(_REQUIREMENTKEY);
            _REQUIREMENTNUMBER = "A.01b";
            _REQUIREMENTKEY = "2";
            _REQUIREMENTREFERENCE = "ODMS 05.01";

            #region Generic BindData Function (Caal the same for displaying each list)

            // Pass Value Mapping List :
                BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSValue, Constants.LIST_NAME_ODMSValueMapping);

            // Pass Application Mapping List :
                BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSApplication, Constants.LIST_NAME_ODMSApplicationMapping);

            //Pass CoOrdination Mapping List
            //BindData(_REQUIREMENTKEY, CoOrdination List, CoOrdination Mapping List);
            #endregion
        }

        private void BindData(string _REQUIREMENTKEY, string listName, string lookUpListName)
        {
            DataTable requirementDT = new DataTable();
            requirementDT.Columns.Add(Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY);

            if (_REQUIREMENTKEY != null)
            {
                DataRow de = requirementDT.NewRow();
                de[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                requirementDT.Rows.Add(de);
            }

            //get Mapping List detail

            /*DataTable MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
            if (listName == Constants.LIST_NAME_ODMSValue)
            {
                ValueDetail = getODMSDetailAllItems(listName);
            }
            else if (listName == Constants.LIST_NAME_ODMSApplication)
            {                
                ApplicationDetail = getODMSDetailAllItems(listName);
            }*/

            if (listName == Constants.LIST_NAME_ODMSValue)
            {
                DataTable MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ValueDetail = getODMSDetailAllItems(listName);
                ViewValueListData.DataSource = GetValueDataTableToBind(UniqueID(MappingDetail, Constants.LIST_NAME_ODMSValue));
                ViewValueListData.DataBind();
            }
            else if (listName == Constants.LIST_NAME_ODMSApplication)
            {
                DataTable MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ApplicationDetail = getODMSDetailAllItems(listName);
                ViewApplicationListData.DataSource = GetApplicationDataTableToBind(UniqueID(MappingDetail, Constants.LIST_NAME_ODMSApplication));
                ViewApplicationListData.DataBind();
            }                        
        }


        #region Bind Application Data
        private DataTable GetApplicationDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_Value_Name);
            if (ApplicationDetail != null && ApplicationDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in ApplicationDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationDescription]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_Application_Name] = DriverDescription;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        ApplicationDetailDictionary.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        #endregion

        #region Bind Value Data
        private DataTable GetValueDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_Value_Name);
            if (ValueDetail != null && ValueDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in ValueDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueKey]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueDescription]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_Value_Name] = DriverDescription;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        ValueDetailDictionary.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        #endregion

        /* private DataTable GetDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (DriverDetail != null && DriverDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in DriverDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverDescription]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        DriverDetailDictionary.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        } */

        #region Generic ViewListDetails Methods

        private DataTable getODMSMappingDetail(DataTable requirementDT, string listName)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSMappingListItems = null;
                if (listName == Constants.LIST_NAME_ODMSValueMapping)
                {
                    ODMSMappingListItems = new ReportFinderMethods().getODMSValueMappingListItemCollection(requirementDT);
                }
                else if (listName == Constants.LIST_NAME_ODMSApplicationMapping)
                {
                    ODMSMappingListItems = new ReportFinderMethods().getODMSApplicationMappingListItemCollection(requirementDT);
                }
                

                if (ODMSMappingListItems != null)
                {
                    tempDT = ODMSMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }

        private List<string> UniqueID(DataTable MappingDetail, string listName)
        {
            List<string> uniqueIDs = new List<string>();
            //get unique IDs
            if (MappingDetail != null && MappingDetail.Rows.Count > 0)
            {
                foreach (DataRow applicationRow in MappingDetail.Rows)
                {
                    //string val = Convert.ToString(applicationRow[Constants.ODMSValueMapping_FIELDS_ValueKey]).Split('.').FirstOrDefault();
                    string val = null;

                    if (listName == Constants.LIST_NAME_ODMSValue)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSValueMapping_FIELDS_ValueKey]).Split('.').FirstOrDefault();
                    }
                    else if (listName == Constants.LIST_NAME_ODMSApplication)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSApplicationMapping_FIELDS_ApplicationID]).Split('.').FirstOrDefault();
                    }
                    
                    if (!uniqueIDs.Contains(val))
                    {
                        uniqueIDs.Add(val);
                    }
                }
            }
            return uniqueIDs;
        }

        private DataTable getODMSDetailAllItems(string listName)
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSListItems = null;//Constants.LIST_NAME_ODMSValueMapping
            if (listName == Constants.LIST_NAME_ODMSValue)
            {
                ODMSListItems = new ReqFinderInfoMethods().getODMSValueListAllItemCollection();
            }
            else if (listName == Constants.LIST_NAME_ODMSApplication)
            {
                ODMSListItems = new ReqFinderInfoMethods().getODMSApplicationListAllItemCollection();
            }
           

            if (ODMSListItems != null)
            {
                tempDT = ODMSListItems.GetDataTable();
            }
            return tempDT;

        }

        #endregion

        /*private DataTable getODMSDriverDetailAllItens()
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSDriverListItems = new ReportFinderMethods().getODMSDriverListAllItemCollection();
            if (ODMSODMSDriverListItems != null)
            {
                tempDT = ODMSODMSDriverListItems.GetDataTable();
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
        }*/

    }
}
