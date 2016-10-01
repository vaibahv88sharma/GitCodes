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

namespace Sadara.DigiCom.ReportFinder.WebParts.ViewRequirementDetails
{
    [ToolboxItemAttribute(false)]
    public partial class ViewRequirementDetails : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public ViewRequirementDetails()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        Dictionary<string, string> DriverDetailDictionary = new Dictionary<string, string>();
        string _REQUIREMENTNUMBER = string.Empty;
        string _REQUIREMENTKEY = string.Empty;
        string _REQUIREMENTREFERENCE = string.Empty;
        DataTable DriverDetail = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            _REQUIREMENTKEY = "1";
            BindData(_REQUIREMENTKEY);

        }

        private void BindData(string _REQUIREMENTKEY)
        {
            DataTable requirementDT = new DataTable();
            requirementDT.Columns.Add(Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY);

            if (_REQUIREMENTKEY != null)
            {
                DataRow de = requirementDT.NewRow();
                de[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                requirementDT.Rows.Add(de);
            }

            //get driver mapping Detail
            DataTable DriverMappingDetail = getODMSDriverMappingDetail(requirementDT);
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
            DriverDetail = getODMSDriverDetailAllItens();

            AddRequirementGridView.DataSource = GetDataTableToBind(uniqueDriverIDs);
            AddRequirementGridView.DataBind();

        }
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
        }

        private DataTable getODMSDriverDetailAllItens()
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
        }
    }
}
