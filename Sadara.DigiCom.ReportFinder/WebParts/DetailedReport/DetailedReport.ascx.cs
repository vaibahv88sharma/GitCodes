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


namespace Sadara.DigiCom.ReportFinder.WebParts.DetailedReport
{
    [ToolboxItemAttribute(false)]
    public partial class DetailedReport : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public DetailedReport()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        #region Global Variables
        Dictionary<string, string> DriverDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ValueDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ApplicationDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> CoordinationCodeDetailDictionary = new Dictionary<string, string>();
        Dictionary<string, string> ReqDiffCodeDetailDictionary = new Dictionary<string, string>();

        Dictionary<string, string> DriverDetailDictionaryRepeater = new Dictionary<string, string>();
        Dictionary<string, string> ValueDetailDictionaryRepeater = new Dictionary<string, string>();
        Dictionary<string, string> ApplicationDetailDictionaryRepeater = new Dictionary<string, string>();
        Dictionary<string, string> CoordinationCodeDetailDictionaryRepeater = new Dictionary<string, string>();
        Dictionary<string, string> ReqDiffCodeDetailDictionaryRepeater = new Dictionary<string, string>();

        string _REQUIREMENTNUMBER = string.Empty;
        string _REQUIREMENTKEY = string.Empty;
        string _REQUIREMENTREFERENCE = string.Empty;

        DataTable DriverDetail = new DataTable();
        DataTable ValueDetail = new DataTable();
        DataTable ApplicationDetail = new DataTable();
        DataTable CoOrdinationDetail = new DataTable();
        DataTable RDCodeDetail = new DataTable();

        DataTable DriverDetailRepeater = new DataTable();
        DataTable ValueDetailRepeater = new DataTable();
        DataTable ApplicationDetailRepeater = new DataTable();
        DataTable CoOrdinationDetailRepeater = new DataTable();
        DataTable RDCodeDetailRepeater = new DataTable();
  
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //DisplayValueList
            //_REQUIREMENTKEY = "2";
            _REQUIREMENTKEY = Convert.ToString(Page.Request[Constants.QueryString_REQUIREMENTKEY]);
            //BindData(_REQUIREMENTKEY);
            //BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSValue, Constants.LIST_NAME_ODMSValueMapping);
            //BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSApplication, Constants.LIST_NAME_ODMSApplicationMapping);
            //BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSCoordinationCode, Constants.LIST_NAME_ODMSCoordinationCodeMapping);
            //BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSReqDiffCode, Constants.LIST_NAME_ODMSReqDiffCodeMapping);
            //BindData(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSDriver, Constants.LIST_NAME_ODMSDriverMapping);
            BindDataToRepeater(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSValue, Constants.LIST_NAME_ODMSValueMapping);
            BindDataToRepeater(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSApplication, Constants.LIST_NAME_ODMSApplicationMapping);
            BindDataToRepeater(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSCoordinationCode, Constants.LIST_NAME_ODMSCoordinationCodeMapping);
            BindDataToRepeater(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSReqDiffCode, Constants.LIST_NAME_ODMSReqDiffCodeMapping);
            BindDataToRepeater(_REQUIREMENTKEY, Constants.LIST_NAME_ODMSDriver, Constants.LIST_NAME_ODMSDriverMapping);         
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

            DataTable MappingDetail = null;
            //get mapping Detail
            if (lookUpListName == Constants.LIST_NAME_ODMSValueMapping)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ValueDetail = getODMSDetailAllItens(listName);
                DisplayValueList.DataSource = GetValueDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DisplayValueList.DataBind();
                //ValueRepeater.DataSource = GetValueDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                //ValueRepeater.DataBind();
            }
            else if (lookUpListName == Constants.LIST_NAME_ODMSApplicationMapping)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ApplicationDetail = getODMSDetailAllItens(listName);
                DisplayApplicationList.DataSource = GetApplicationDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DisplayApplicationList.DataBind();
            }
            else if (lookUpListName == Constants.LIST_NAME_ODMSCoordinationCodeMapping)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                CoOrdinationDetail = getODMSDetailAllItens(listName);
                DisplayCoOrdinationList.DataSource = GetCoordinationCodeDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DisplayCoOrdinationList.DataBind();
            }
            else if (lookUpListName == Constants.LIST_NAME_ODMSReqDiffCodeMapping)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                RDCodeDetail = getODMSDetailAllItens(listName);
                DisplayRDCodeList.DataSource = GetReqDiffCodeDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DisplayRDCodeList.DataBind();
            }
            else if (lookUpListName == Constants.LIST_NAME_ODMSDriverMapping)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                DriverDetail = getODMSDetailAllItens(listName);
                DisplayDriverList.DataSource = GetDriverDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DisplayDriverList.DataBind();
            }
            /*else if (lookUpListName == "")
            {
                lookUpListName = Constants.LIST_NAME_ODMSValueMapping;
                listName = Constants.LIST_NAME_ODMSValue;
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ValueDetailRepeater = getODMSDetailAllItens(listName);
                //DisplayValueList.DataSource = GetValueDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                //DisplayValueList.DataBind();
                ValueRepeater.DataSource = GetValueRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                ValueRepeater.DataBind();
            }*/
        }
        private void BindDataToRepeater(string _REQUIREMENTKEY, string listName, string lookUpListName)
        {
            DataTable requirementDT = new DataTable();
            requirementDT.Columns.Add(Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY);

            if (_REQUIREMENTKEY != null)
            {
                DataRow de = requirementDT.NewRow();
                de[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                requirementDT.Rows.Add(de);
            }

            DataTable MappingDetail = null;
            //get mapping Detail
            if (listName == Constants.LIST_NAME_ODMSValue)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ValueDetailRepeater = getODMSDetailAllItens(listName);
                ValueRepeater.DataSource = GetValueRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                ValueRepeater.DataBind();
            }
            else if (listName == Constants.LIST_NAME_ODMSApplication)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                ApplicationDetailRepeater = getODMSDetailAllItens(listName);
                ApplicationRepeater.DataSource = GetApplicationRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                ApplicationRepeater.DataBind();
            }
            else if (listName == Constants.LIST_NAME_ODMSCoordinationCode)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                CoOrdinationDetailRepeater = getODMSDetailAllItens(listName);
                CoOrdinationRepeater.DataSource = GetCoordinationCodeRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                CoOrdinationRepeater.DataBind();
            }
            else if (listName == Constants.LIST_NAME_ODMSReqDiffCode)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                RDCodeDetailRepeater = getODMSDetailAllItens(listName);
                RDCodeRepeater.DataSource = GetReqDiffCodeRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                RDCodeRepeater.DataBind();
            }
            else if (listName == Constants.LIST_NAME_ODMSDriver)
            {
                MappingDetail = getODMSMappingDetail(requirementDT, lookUpListName);
                DriverDetailRepeater = getODMSDetailAllItens(listName);
                DriverRepeater.DataSource = GetDriverRepeaterDataTableToBind(UniqueIDs(MappingDetail, lookUpListName));
                DriverRepeater.DataBind();
            }
        }
        #region Get Data Table To Bind
        private DataTable GetValueDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (ValueDetail != null && ValueDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in ValueDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueKey]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSValue_FIELDS_ValueDescription]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
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
        private DataTable GetApplicationDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);            
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory);
            if (ApplicationDetail != null && ApplicationDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in ApplicationDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationDescription]);
                    string ApplicationCategoryLookUp = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationCategory]);
                    
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory] = ApplicationCategoryLookUp;
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
        private DataTable GetCoordinationCodeDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (CoOrdinationDetail != null && CoOrdinationDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in CoOrdinationDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription]);
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
                        CoordinationCodeDetailDictionary.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetReqDiffCodeDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (RDCodeDetail != null && RDCodeDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in RDCodeDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription]);
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
                        ReqDiffCodeDetailDictionary.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetDriverDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription);
            
            if (DriverDetail != null && DriverDetail.Rows.Count > 0)
            {
                foreach (DataRow dr in DriverDetail.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverDescription]);
                    string DriverDetailedDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DetailedDriverDescription]);
                    
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription] = DriverDetailedDescription;
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

        private DataTable GetValueRepeaterDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (ValueDetailRepeater != null && ValueDetailRepeater.Rows.Count > 0)
            {
                foreach (DataRow dr in ValueDetailRepeater.Rows)
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
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        ValueDetailDictionaryRepeater.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetApplicationRepeaterDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory);
            if (ApplicationDetailRepeater != null && ApplicationDetailRepeater.Rows.Count > 0)
            {
                foreach (DataRow dr in ApplicationDetailRepeater.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationDescription]);
                    string ApplicationCategoryLookUp = Convert.ToString(dr[Constants.ODMSApplication_FIELDS_ApplicationCategory]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory] = ApplicationCategoryLookUp;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        ApplicationDetailDictionaryRepeater.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetCoordinationCodeRepeaterDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (CoOrdinationDetailRepeater != null && CoOrdinationDetailRepeater.Rows.Count > 0)
            {
                foreach (DataRow dr in CoOrdinationDetailRepeater.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription]);
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
                        CoordinationCodeDetailDictionaryRepeater.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetReqDiffCodeRepeaterDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            if (RDCodeDetailRepeater != null && RDCodeDetailRepeater.Rows.Count > 0)
            {
                foreach (DataRow dr in RDCodeDetailRepeater.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription]);
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
                        ReqDiffCodeDetailDictionaryRepeater.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }
        private DataTable GetDriverRepeaterDataTableToBind(List<string> _uniqueDriverIDs)
        {
            DataTable RequirementDataTable = new DataTable();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add();
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTNUMBER);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTKEY);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_REQUIREMENTREFERENCE);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_ID);
            RequirementDataTable.Columns.Add(Constants.DataTable_FIELDS_Name);
            RequirementDataTable.Columns.Add(ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription);
            if (DriverDetailRepeater != null && DriverDetailRepeater.Rows.Count > 0)
            {
                foreach (DataRow dr in DriverDetailRepeater.Rows)
                {
                    string DriverID = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverID]);
                    string DriverDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DriverDescription]);
                    string DriverDetailedDescription = Convert.ToString(dr[Constants.ODMSDriver_FIELDS_DetailedDriverDescription]);
                    if (_uniqueDriverIDs.Contains(DriverID))
                    {
                        DataRow newrow = RequirementDataTable.NewRow();
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTNUMBER] = _REQUIREMENTNUMBER;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTKEY] = _REQUIREMENTKEY;
                        newrow[Constants.DataTable_FIELDS_REQUIREMENTREFERENCE] = _REQUIREMENTREFERENCE;
                        newrow[Constants.DataTable_FIELDS_ID] = DriverID;
                        newrow[Constants.DataTable_FIELDS_Name] = DriverDescription;
                        newrow[ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription] = DriverDetailedDescription;
                        RequirementDataTable.Rows.Add(newrow);
                    }
                    else
                    {
                        DriverDetailDictionaryRepeater.Add(DriverID, DriverDescription);
                    }

                }
            }


            return RequirementDataTable;
        }

        #endregion

        #region Get Data from Data Lists and LookUp Lists

        private DataTable getODMSMappingDetail(DataTable requirementDT, string lookUpListName)
        {
            DataTable tempDT = new DataTable();
            if (requirementDT != null && requirementDT.Rows.Count > 0)
            {
                SPListItemCollection ODMSODMSDriverMappingListItems = null;
                if (lookUpListName == Constants.LIST_NAME_ODMSValueMapping)
                {
                    ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSValueMappingListItemCollection(requirementDT);
                }
                else if (lookUpListName == Constants.LIST_NAME_ODMSApplicationMapping)
                {
                    ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSApplicationMappingListItemCollection(requirementDT);
                }
                else if (lookUpListName == Constants.LIST_NAME_ODMSCoordinationCodeMapping)
                {
                    ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSCoordinationCodeMappingListItemCollection(requirementDT);
                }
                else if (lookUpListName == Constants.LIST_NAME_ODMSReqDiffCodeMapping)
                {
                    ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSReqDiffCodeMappingListItemCollection(requirementDT);
                }
                else if (lookUpListName == Constants.LIST_NAME_ODMSDriverMapping)
                {
                    ODMSODMSDriverMappingListItems = new ReportFinderMethods().getODMSDriverMappingListItemCollection(requirementDT);
                }
                

                if (ODMSODMSDriverMappingListItems != null)
                {
                    tempDT = ODMSODMSDriverMappingListItems.GetDataTable();
                }
            }
            return tempDT;
        }

        private List<string> UniqueIDs(DataTable MappingDetail, string lookUpListName)
        {
            List<string> uniqueID = new List<string>();
            //get unique driver IDs
            if (MappingDetail != null && MappingDetail.Rows.Count > 0)
            {
                foreach (DataRow applicationRow in MappingDetail.Rows)
                {
                    string val = null;
                    if (lookUpListName == Constants.LIST_NAME_ODMSValueMapping)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSValueMapping_FIELDS_ValueKey]).Split('.').FirstOrDefault();
                    }
                    else if (lookUpListName == Constants.LIST_NAME_ODMSApplicationMapping)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSApplicationMapping_FIELDS_ApplicationID]).Split('.').FirstOrDefault();
                    }
                    else if (lookUpListName == Constants.LIST_NAME_ODMSCoordinationCodeMapping)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode]).Split('.').FirstOrDefault();
                    }
                    else if (lookUpListName == Constants.LIST_NAME_ODMSReqDiffCodeMapping)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID]).Split('.').FirstOrDefault();
                    }
                    else if (lookUpListName == Constants.LIST_NAME_ODMSDriverMapping)
                    {
                        val = Convert.ToString(applicationRow[Constants.ODMSDriverMapping_FIELDS_DriverID]).Split('.').FirstOrDefault();
                    }


                    if (!uniqueID.Contains(val))
                    {
                        uniqueID.Add(val);
                    }
                }
            }
            return uniqueID;
        }

        private DataTable getODMSDetailAllItens(string listName)
        {
            DataTable tempDT = new DataTable();
            SPListItemCollection ODMSODMSDriverListItems = null;

            if (listName == Constants.LIST_NAME_ODMSValue)
            {
                ODMSODMSDriverListItems = new ReqFinderInfoMethods().getODMSValueListAllItemCollection();
            }
            else if (listName == Constants.LIST_NAME_ODMSApplication)
            {
                ODMSODMSDriverListItems = new ReqFinderInfoMethods().getODMSApplicationListAllItemCollection();
            }
            else if (listName == Constants.LIST_NAME_ODMSCoordinationCode)
            {
                ODMSODMSDriverListItems = new ReqFinderInfoMethods().getODMSCoOrdinationListAllItemCollection();
            }
            else if (listName == Constants.LIST_NAME_ODMSReqDiffCode)
            {
                ODMSODMSDriverListItems = new ReqFinderInfoMethods().getODMSReqDiffCodeListAllItemCollection();
            }
            else if (listName == Constants.LIST_NAME_ODMSDriver)
            {
                //ODMSODMSDriverListItems = new ReportFinderMethods().getODMSDriverListAllItemCollection();
                ODMSODMSDriverListItems = new ReqFinderInfoMethods().getODMSDriverListDetailsAllItemCollection();
            }


            if (ODMSODMSDriverListItems != null)
            {
                tempDT = ODMSODMSDriverListItems.GetDataTable();
            }
            return tempDT;

        }

        #endregion

        

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
         //   ExportToExcel objExportToExcel = new ExportToExcel();
            // expoExportListToExcel(string _listName);
          //  objExportToExcel.ExportListToExcel(Constants.LIST_NAME_ODMSREQUIREMENT);
        }
    }
}
