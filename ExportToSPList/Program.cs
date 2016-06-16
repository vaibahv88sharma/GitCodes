using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace ExportToSPList
{
    class Program
    {
        //public static DataTable dt = new DataTable();
        static void Main(string[] args)
        {
            ReadAllSettings();
        }

        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                ////////string tenant = "https://enterpriseuser.sharepoint.com/sites/worksite";
                ////////string userName = "officeuser@enterpriseuser.onmicrosoft.com";
                ////////string passwordString = "india@123";
                string tenant = appSettings.Get("URL");
                string userName = appSettings.Get("UserName");
                string passwordString = appSettings.Get("Password");

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        DataTable newDt = new DataTable();

                        if (key == "JobsDataList")
                        {
                            //////////////string sqlQuery = "";
                            //////string tenant = "https://enterpriseuser.sharepoint.com/sites/worksite";
                            //////string userName = "officeuser@enterpriseuser.onmicrosoft.com";
                            //////string passwordString = "india@123";
                            #region Fetch SQL Query

                            string sqlQuery = @"
                                        use FworkSQLEcm

                                        IF OBJECT_ID('tempdb..#temp') IS NOT NULL
                                        DROP TABLE #temp

                                        SELECT
	                                        job.s_job_num as JobNumber,
	                                        IsNull(addr.s_addr_full, '') as JobAddress,
	                                        (  select top 1  [s_name]  FROM [v_secUserStaffName] where l_staff_e_id=cst.l_super_e_id)  as Supervisor
	
                                        into #temp
	                                        FROM job
	                                        LEFT JOIN clientFile ON clientFile.l_file_id = job.l_file_id
	                                        LEFT JOIN land ON land.l_land_id = clientFile.l_land_id
	                                        LEFT JOIN client ON client.l_client_id = clientFile.l_client_id
	                                        LEFT JOIN cst on cst.l_job_id = job.l_job_id
	                                        LEFT JOIN addr ON addr.l_addr_id = land.l_addr_id
	                                        LEFT JOIN wfl_stgMinor ON wfl_stgMinor.l_wfl_stgMinor_id = job.l_wfl_stgMinor_id 
	                                        LEFT JOIN wfl_stgMajor ON wfl_stgMajor.l_wfl_stgMajor_id = job.l_wfl_stgMajor_id 
	                                        LEFT JOIN list_item ON list_item.l_list_item_id = job.l_job_status_gl_id 
	                                        LEFT JOIN prd_hSpec ON prd_hSpec.l_prd_hSpec_id = job.l_prd_hSpec_id
	                                        LEFT JOIN prd_hPack On prd_hPack.l_prd_hPack_id = job.l_prd_hPack_id
	                                        LEFT JOIN prd_hType On prd_hType.l_prd_hType_id = job.l_prd_hType_id
	                                        LEFT JOIN prd_hSeries On prd_hSeries.l_prd_hSeries_id = job.l_prd_hSeries_id
	                                        LEFT JOIN prd_hFacade On prd_hFacade.l_prd_hFacade_id = job.l_prd_hFacade_id

                                        select * from #temp where Supervisor is not null and Supervisor <> '[Unknown]'
                                        ";

                            //                            sqlQuery = @"
                            //                                 use FworkSQLEcm
                            //
                            //                                IF OBJECT_ID('tempdb..#temp') IS NOT NULL
                            //                                    DROP TABLE #temp
                            //
                            //                                 SELECT
                            //                                 'Import From Framework' as title, --varchar
                            //                                job.s_job_num as JobNumber,   --varchar
                            //                                 cst.d_pci_comp_act as CompleteDate, --datetime
                            //                                 addr.s_addr_full as JobAddress,   --varchar
                            //                                 '' as estimator,  --varchar
                            //                                 '' as designer,  --varchar
                            //                                 case when prd_hType.s_name='[Unknown]' then '' else prd_hType.s_name end as HouseType, --varchar
                            //                                 (  select top 1  [s_name]  FROM [v_secUserStaffName] where l_staff_e_id=cst.l_super_e_id)  as Supervisor, --varchar
                            //                                 ( select top 1 [s_name]  FROM [v_secUserStaffName] where l_staff_e_id=cst.l_mgr_e_id)  as ConstructionManager, --varchar
                            //                                 list_item .s_name_ref  as jobstatus --varchar
                            //                                 ,wfl_stgMinor.s_name --varchar
                            //                                 ,list_item.s_name_ref --varchar
                            //
                            //                                into #temp
                            //
                            //                                 FROM job
                            //                                 LEFT JOIN clientFile ON clientFile.l_file_id = job.l_file_id
                            //                                 LEFT JOIN land ON land.l_land_id = clientFile.l_land_id
                            //                                 LEFT JOIN client ON client.l_client_id = clientFile.l_client_id
                            //                                 LEFT JOIN cst on cst.l_job_id = job.l_job_id
                            //                                 LEFT JOIN addr ON addr.l_addr_id = land.l_addr_id
                            //                                 LEFT JOIN wfl_stgMinor ON wfl_stgMinor.l_wfl_stgMinor_id = job.l_wfl_stgMinor_id 
                            //                                 LEFT JOIN wfl_stgMajor ON wfl_stgMajor.l_wfl_stgMajor_id = job.l_wfl_stgMajor_id 
                            //                                 LEFT JOIN list_item ON list_item.l_list_item_id = job.l_job_status_gl_id 
                            //                                 LEFT JOIN prd_hSpec ON prd_hSpec.l_prd_hSpec_id = job.l_prd_hSpec_id
                            //                                 LEFT JOIN prd_hPack On prd_hPack.l_prd_hPack_id = job.l_prd_hPack_id
                            //                                 LEFT JOIN prd_hType On prd_hType.l_prd_hType_id = job.l_prd_hType_id
                            //                                 LEFT JOIN prd_hSeries On prd_hSeries.l_prd_hSeries_id = job.l_prd_hSeries_id
                            //                                 LEFT JOIN prd_hFacade On prd_hFacade.l_prd_hFacade_id = job.l_prd_hFacade_id

                            //select * from #temp where Supervisor is not null and Supervisor <> '[Unknown]'";

                            //use FworkSQLEcm IF OBJECT_ID('tempdb..#temp') IS NOT NULL DROP TABLE #temp SELECT 'Import From Framework' as title, job.s_job_num as JobNumber, cst.d_pci_comp_act as CompleteDate,  addr.s_addr_full as JobAddress, '' as estimator, '' as designer, case when prd_hType.s_name='[Unknown]' then '' else prd_hType.s_name end as HouseType, (  select top 1  [s_name]  FROM [v_secUserStaffName] where l_staff_e_id=cst.l_super_e_id)  as Supervisor, ( select top 1 [s_name]  FROM [v_secUserStaffName] where l_staff_e_id=cst.l_mgr_e_id)  as ConstructionManager,  list_item .s_name_ref  as jobstatus  ,wfl_stgMinor.s_name  ,list_item.s_name_ref into #temp  FROM job LEFT JOIN clientFile ON clientFile.l_file_id = job.l_file_id LEFT JOIN land ON land.l_land_id = clientFile.l_land_id LEFT JOIN client ON client.l_client_id = clientFile.l_client_id   LEFT JOIN cst on cst.l_job_id = job.l_job_id    LEFT JOIN addr ON addr.l_addr_id = land.l_addr_id   LEFT JOIN wfl_stgMinor ON wfl_stgMinor.l_wfl_stgMinor_id = job.l_wfl_stgMinor_id    LEFT JOIN wfl_stgMajor ON wfl_stgMajor.l_wfl_stgMajor_id = job.l_wfl_stgMajor_id   LEFT JOIN list_item ON list_item.l_list_item_id = job.l_job_status_gl_id   LEFT JOIN prd_hSpec ON prd_hSpec.l_prd_hSpec_id = job.l_prd_hSpec_id   LEFT JOIN prd_hPack On prd_hPack.l_prd_hPack_id = job.l_prd_hPack_id LEFT JOIN prd_hType On prd_hType.l_prd_hType_id = job.l_prd_hType_id    LEFT JOIN prd_hSeries On prd_hSeries.l_prd_hSeries_id = job.l_prd_hSeries_id    LEFT JOIN prd_hFacade On prd_hFacade.l_prd_hFacade_id = job.l_prd_hFacade_id   select * from #temp where Supervisor is not null and Supervisor <> '[Unknown]'

                            #endregion Fetch SQL Query
                            //ConnectOnline(tenant, userName, passwordString, key, appSettings[key], appSettings["JobsDataColumn"], sqlQuery);
                            SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["JobsDataColumn"], sqlQuery);
                        }
                        else if (key == "SuppliersList")
                        {
                            #region sql query
                            string sqlQuery = @"
                                                use WATERsysSQL

                                                SELECT 
                                                   Supplier_Code
                                                   ,replace([SupplierName],'''','')
                                                   ,IsNull(AccountEmail, '')
                                                   FROM [WATERsysSQL].[dbo].[Supplier]
                                                   where
                                                   SuppGroup in (1,2,3)
                                                   and isnull([Archived],0) <>1
                                                ";
                            #endregion
                            //////string tenant = "https://enterpriseuser.sharepoint.com/sites/worksite";
                            //////string userName = "officeuser@enterpriseuser.onmicrosoft.com";
                            //////string passwordString = "india@123";
                            //ConnectOnline(tenant, userName, passwordString, key, appSettings[key], appSettings["SuppliersColumn"], sqlQuery);
                            SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["SuppliersColumn"], sqlQuery);
                        }
                        else if (key == "ETSData")
                        {
                            #region sql query
                            string sqlQuery = @"
                                                SELECT [Job]
                                                      ,[ETS No]
                                                      ,[ItemsDescription]
                                                      ,[Selected Job]
                                                      ,[Cost Centre]
                                                      ,[Reason Code]
                                                      ,[Supplier]
                                                      ,[DeliveryDetails]
                                                      ,[SupplierID]
                                                      ,[DeliveryDate]
                                                      ,[Price]
                                                      ,[GST]
                                                      ,[Created By]
                                                      ,[Approved By]
                                                      ,[Purchase Order]
                                                      ,[RegeneratePO]
                                                      ,[ID]
                                                      ,[JobID]
                                                      ,[ETSId]
                                                      ,[CostCentreID]
                                                      ,[Sent]
                                                      ,[marked]
                                                      ,[Created]
                                                      ,[Complete]
                                                      ,[Recharge]
                                                      ,[Item Type]
                                                      ,[Path]

                                                  FROM [WatersunData].[dbo].[ETSDataExportDemo]
                                                ";
                            #endregion
                            ////////string tenant = "https://networkintegration.sharepoint.com/sites/365Build/Watersun";
                            ////////string userName = "andrew@365build.com.au";
                            ////////string passwordString = "187Ch@lleng3r";
                            //SpToSQL(tenant, userName, passwordString, key, appSettings[key], appSettings["SuppliersColumn"], sqlQuery);
                            SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["ETSDataColumn"], sqlQuery);
                        }
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        private static void SqlSpConnect(string tenant, string userName, string passwordString, string key, string appSettingsKey, string columnName, string sqlQuery)
        {
            try
            {
                GlobalLogic gl = new GlobalLogic();
                ClientContext ctx = gl.ConnectSP(tenant, userName, passwordString);

                ListItemCollection getListItemsCol = gl.getListData(tenant, userName, passwordString, appSettingsKey);

                DataTable dt = new DataTable();

                if (key == "SuppliersList" || key == "JobsDataList")
                {
                    dt.Columns.Add("JobNumber");
                    if (getListItemsCol != null)
                    {
                        foreach (ListItem listItemsCol in getListItemsCol)
                        {
                            DataRow dr = dt.NewRow();
                            dr["JobNumber"] = listItemsCol[columnName];
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["JobNumber"] = "";
                        dt.Rows.Add(dr);
                    }
                }
                else if (key == "ETSData")
                {
                    dt.Columns.Add("Job");
                    dt.Columns.Add("ETSId");
                    dt.Columns.Add("ItemsDescription");
                    dt.Columns.Add("Selected_x0020_Job");
                    dt.Columns.Add("Cost_x0020_Centre");
                    dt.Columns.Add("Reason_x0020_Code");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("DeliveryDetails");
                    dt.Columns.Add("SupplierID");
                    dt.Columns.Add("DeliveryDate");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("GST");
                    dt.Columns.Add("Author");
                    dt.Columns.Add("Approved_x0020_By");
                    dt.Columns.Add("RegeneratePO");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("JobID");
                    dt.Columns.Add("CostCentreID");
                    dt.Columns.Add("Sent");
                    dt.Columns.Add("marked");
                    dt.Columns.Add("Created");
                    dt.Columns.Add("Complete");
                    dt.Columns.Add("Recharge");

                    foreach (ListItem listItemsCol in getListItemsCol)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Job"] = listItemsCol["Title"];
                        dr["ETSId"] = listItemsCol["ETSId"];
                        dr["ItemsDescription"] = listItemsCol["ItemsDescription"];
                        dr["Selected_x0020_Job"] = listItemsCol["Selected_x0020_Job"];
                        dr["Cost_x0020_Centre"] = listItemsCol["Cost_x0020_Centre"];
                        dr["Reason_x0020_Code"] = listItemsCol["Reason_x0020_Code"];
                        dr["Supplier"] = listItemsCol["Supplier"];
                        dr["DeliveryDetails"] = listItemsCol["DeliveryDetails"];
                        dr["SupplierID"] = listItemsCol["SupplierID"];
                        dr["DeliveryDate"] = listItemsCol["DeliveryDate"];
                        dr["Price"] = listItemsCol["Price"];
                        dr["GST"] = listItemsCol["GST"];

                        if (listItemsCol["Author"] == null)
                        {
                            listItemsCol["Author"] = "";
                        }
                        else
                        {
                            FieldUserValue userAuthor = (FieldUserValue)listItemsCol["Author"];
                            dr["Author"] = userAuthor.LookupValue;
                        }
                        if (listItemsCol["Approved_x0020_By"] == null)
                        {
                            listItemsCol["Approved_x0020_By"] = "";
                        }
                        else
                        {
                            FieldUserValue userAuthor = (FieldUserValue)listItemsCol["Approved_x0020_By"];
                            dr["Approved_x0020_By"] = userAuthor.LookupValue;
                        }
                        dr["RegeneratePO"] = listItemsCol["RegeneratePO"];
                        dr["ID"] = listItemsCol["ID"];
                        dr["JobID"] = listItemsCol["JobID"];
                        dr["CostCentreID"] = listItemsCol["CostCentreID"];
                        dr["Sent"] = listItemsCol["Sent"];
                        dr["marked"] = listItemsCol["marked"];
                        dr["Created"] = listItemsCol["Created"];
                        dr["Complete"] = listItemsCol["Complete"];
                        dr["Recharge"] = listItemsCol["Recharge"];
                        dt.Rows.Add(dr);
                    }
                }
                
                dbConnection conn = new dbConnection();
                DataTable tempTable = null;
                tempTable = conn.executeSelectNoParameter(sqlQuery);
                ListItemCreationInformation itemCreateInfo = null;
                ListItem oListItem = null;
                List oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                if (key == "SuppliersList" || key == "JobsDataList")
                {
                     for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        DataRow[] drExists = dt.Select("JobNumber = '" + tempTable.Rows[i][0].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + tempTable.Rows[i][0].ToString());
                        }
                        else
                        {
                            Console.WriteLine("Inserting - " + tempTable.Rows[i][0].ToString());
                            itemCreateInfo = new ListItemCreationInformation();
                            oListItem = oList.AddItem(itemCreateInfo);
                            if (key == "JobsDataList")
                            {
                                oListItem["Title"] = tempTable.Rows[i][0].ToString();
                                oListItem["Job_x0020_Address"] = tempTable.Rows[i][1].ToString();
                                oListItem["Job_x0020_Supervisor"] = tempTable.Rows[i][2].ToString();
                            }
                            else if (key == "SuppliersList")
                            {
                                oListItem["Title"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierCode"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierName"] = tempTable.Rows[i][1].ToString();
                                oListItem["SupplierEmail"] = tempTable.Rows[i][2].ToString();
                            }

                            oListItem.Update();
                            ctx.ExecuteQuery();
                        }
                    }
                }
                else if (key == "ETSData")
                {
                    string job, eTSNo, itemsDescription, selectedJob, costCentre, reasonCode, supplier, deliveryDetails, supplierID, deliveryDate, price, gST, createdBy, approvedBy, regeneratePO, jobID, eTSId, costCentreID, created, recharge, itemType, path;
                    float id, sent, marked, complete;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drExists = tempTable.Select("ETSId = '" + dt.Rows[i][1].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + dt.Rows[i][1].ToString());
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Inserting - " + dt.Rows[i][1].ToString());
                                job = dt.Rows[i][0].ToString();
                                eTSId = dt.Rows[i][1].ToString();
                                if (Int32.Parse(eTSId) > 999) { eTSNo = "E0000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 99) { eTSNo = "E00000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 9) { eTSNo = "E00000" + eTSId; }
                                else { eTSNo = "E" + eTSId; }
                                itemsDescription = dt.Rows[i][2].ToString();
                                selectedJob = dt.Rows[i][3].ToString();
                                costCentre = dt.Rows[i][4].ToString();
                                reasonCode = dt.Rows[i][5].ToString();
                                supplier = dt.Rows[i][6].ToString();
                                deliveryDetails = dt.Rows[i][7].ToString();
                                supplierID = dt.Rows[i][8].ToString();
                                deliveryDate = string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][9].ToString();
                                price = string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? "0" : dt.Rows[i][10].ToString();
                                gST = dt.Rows[i][11].ToString();
                                createdBy = dt.Rows[i][12].ToString();
                                approvedBy = dt.Rows[i][13].ToString();
                                regeneratePO = string.IsNullOrEmpty(dt.Rows[i][14].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][14].ToString();
                                id = float.Parse(dt.Rows[i][15].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                jobID = dt.Rows[i][16].ToString();
                                costCentreID = dt.Rows[i][17].ToString();
                                sent = float.Parse(dt.Rows[i][18].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                marked = float.Parse(dt.Rows[i][19].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                created = string.IsNullOrEmpty(dt.Rows[i][20].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][20].ToString();
                                complete = float.Parse(dt.Rows[i][21].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                recharge = dt.Rows[i][22].ToString();
                                itemType = "Item";
                                path = "sites/365Build/Watersun/Lists/l_ETSData";

                                //insert
                                string sclearsqlIns = string.Concat("INSERT INTO [WatersunData].[dbo].[ETSDataExportDemo]" +
                                                                       "([Job], [ETS No], [ItemsDescription], [Selected Job], [Cost Centre], [Reason Code], [Supplier], [DeliveryDetails], [SupplierID], [DeliveryDate], [Price], [GST], [Created By], [Approved By], [RegeneratePO], [ID], [JobID], [ETSId], [CostCentreID], [Sent], [marked], [Created], [Complete], [Recharge], [Item Type], [Path])" +
                                                                       "VALUES (@job, @eTSNo, @itemsDescription, @selectedJob, @costCentre, @reasonCode, @supplier, @deliveryDetails, @supplierID, @deliveryDate, @price, @gST, @createdBy, @approvedBy, @regeneratePO, @id, @jobID, @eTSId, @costCentreID, @sent, @marked, @created, @complete, @recharge, @itemType, @path)");
                                SqlParameter[] parameterUpd = {                                

                                                    new SqlParameter("@job", SqlDbType.NVarChar) { Value = job },
                                                    new SqlParameter("@eTSNo", SqlDbType.NVarChar) { Value = eTSNo },
                                                    new SqlParameter("@itemsDescription", SqlDbType.NVarChar) { Value = itemsDescription },
                                                    new SqlParameter("@selectedJob", SqlDbType.NVarChar) { Value = selectedJob },
                                                    new SqlParameter("@costCentre", SqlDbType.NVarChar) { Value = costCentre },
                                                    new SqlParameter("@reasonCode", SqlDbType.NVarChar) { Value = reasonCode },
                                                    new SqlParameter("@supplier", SqlDbType.NVarChar) { Value = supplier },
                                                    new SqlParameter("@deliveryDetails", SqlDbType.NVarChar) { Value = deliveryDetails },
                                                    new SqlParameter("@supplierID", SqlDbType.NVarChar) { Value = supplierID },
                                                    new SqlParameter("@deliveryDate", SqlDbType.DateTime) { Value = deliveryDate },
                                                    new SqlParameter("@price", SqlDbType.Money) { Value = price },
                                                    new SqlParameter("@gST", SqlDbType.NVarChar) { Value = gST },
                                                    new SqlParameter("@createdBy", SqlDbType.NVarChar) { Value = createdBy },
                                                    new SqlParameter("@approvedBy", SqlDbType.NVarChar) { Value = approvedBy },
                                                    new SqlParameter("@regeneratePO", SqlDbType.DateTime) { Value = regeneratePO },
                                                    new SqlParameter("@id", SqlDbType.Float) { Value = id },
                                                    new SqlParameter("@jobID", SqlDbType.NVarChar) { Value = jobID },
                                                    new SqlParameter("@eTSId", SqlDbType.NVarChar) { Value = eTSId },
                                                    new SqlParameter("@costCentreID", SqlDbType.NVarChar) { Value = costCentreID },
                                                    new SqlParameter("@sent", SqlDbType.Float) { Value = sent },
                                                    new SqlParameter("@marked", SqlDbType.Float) { Value = marked },
                                                    new SqlParameter("@created", SqlDbType.DateTime) { Value = created },
                                                    new SqlParameter("@complete", SqlDbType.Float) { Value = complete },
                                                    new SqlParameter("@recharge", SqlDbType.NVarChar) { Value = recharge },
                                                    new SqlParameter("@itemType", SqlDbType.NVarChar) { Value = itemType },
                                                    new SqlParameter("@path", SqlDbType.NVarChar) { Value = path }
                                                         };
                                bool isInsert = conn.executeInsertQuery(sclearsqlIns, parameterUpd);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //dt.Dispose();
            }
        }

        #region old code
        private static void ConnectOnline(string tenant, string userName, string passwordString, string key, string appSettingsKey, string columnName, string sqlQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var ctx = new ClientContext(tenant))
                {
                    var passWord = new SecureString();
                    foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                    ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);

                    List getList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query></Query></View>";
                    ListItemCollection getListItemsCol = getList.GetItems(camlQuery);

                    ctx.Load(getListItemsCol);

                    ctx.ExecuteQuery();

                    dt.Columns.Add("JobNumber");

                    if (getListItemsCol != null && getListItemsCol.Count > 0)
                    {
                        foreach (ListItem listItemsCol in getListItemsCol)
                        {
                            DataRow dr = dt.NewRow();
                            dr["JobNumber"] = listItemsCol[columnName];
                            dt.Rows.Add(dr);
                        }
                    }

                    dbConnection conn = new dbConnection();
                    DataTable tempTable = null;
                    tempTable = conn.executeSelectNoParameter(sqlQuery);
                    List oList = null;
                    ListItemCreationInformation itemCreateInfo = null;
                    ListItem oListItem = null;
                    oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        DataRow[] drExists = dt.Select("JobNumber = '" + tempTable.Rows[i][0].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + tempTable.Rows[i][1].ToString());
                        }
                        else
                        {
                            Console.WriteLine("Inserting - " + tempTable.Rows[i][1].ToString());
                            itemCreateInfo = new ListItemCreationInformation();
                            oListItem = oList.AddItem(itemCreateInfo);
                            if (key == "JobsDataList")
                            {
                                oListItem["Title"] = tempTable.Rows[i][1].ToString();
                                oListItem["Job_x0020_Address"] = tempTable.Rows[i][3].ToString();
                                oListItem["Job_x0020_Supervisor"] = tempTable.Rows[i][7].ToString();
                            }
                            else if (key == "SuppliersList")
                            {
                                oListItem["Title"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierCode"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierName"] = tempTable.Rows[i][1].ToString();
                                oListItem["SupplierEmail"] = tempTable.Rows[i][2].ToString();
                            }

                            oListItem.Update();
                            ctx.ExecuteQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                dt.Dispose();
            }
        }

        private static void SpToSQL(string tenant, string userName, string passwordString, string key, string appSettingsKey, string columnName, string sqlQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var ctx = new ClientContext(tenant))
                {
                    var passWord = new SecureString();
                    foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                    ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);

                    List getList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query></Query></View>";
                    ListItemCollection getListItemsCol = getList.GetItems(camlQuery);

                    ctx.Load(getListItemsCol);

                    ctx.ExecuteQuery();

                    dt.Columns.Add("Job");
                    dt.Columns.Add("ETSId");
                    dt.Columns.Add("ItemsDescription");
                    dt.Columns.Add("Selected_x0020_Job");
                    dt.Columns.Add("Cost_x0020_Centre");
                    dt.Columns.Add("Reason_x0020_Code");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("DeliveryDetails");
                    dt.Columns.Add("SupplierID");
                    dt.Columns.Add("DeliveryDate");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("GST");
                    dt.Columns.Add("Author");
                    dt.Columns.Add("Approved_x0020_By");
                    dt.Columns.Add("RegeneratePO");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("JobID");
                    dt.Columns.Add("CostCentreID");
                    dt.Columns.Add("Sent");
                    dt.Columns.Add("marked");
                    dt.Columns.Add("Created");
                    dt.Columns.Add("Complete");
                    dt.Columns.Add("Recharge");

                    if (getListItemsCol != null && getListItemsCol.Count > 0)
                    {
                        try
                        {
                            foreach (ListItem listItemsCol in getListItemsCol)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Job"] = listItemsCol["Title"];
                                dr["ETSId"] = listItemsCol["ETSId"];
                                dr["ItemsDescription"] = listItemsCol["ItemsDescription"];
                                dr["Selected_x0020_Job"] = listItemsCol["Selected_x0020_Job"];
                                dr["Cost_x0020_Centre"] = listItemsCol["Cost_x0020_Centre"];
                                dr["Reason_x0020_Code"] = listItemsCol["Reason_x0020_Code"];
                                dr["Supplier"] = listItemsCol["Supplier"];
                                dr["DeliveryDetails"] = listItemsCol["DeliveryDetails"];
                                dr["SupplierID"] = listItemsCol["SupplierID"];
                                dr["DeliveryDate"] = listItemsCol["DeliveryDate"];
                                dr["Price"] = listItemsCol["Price"];
                                dr["GST"] = listItemsCol["GST"];

                                if (listItemsCol["Author"] == null)
                                {
                                    listItemsCol["Author"] = "";
                                }
                                else
                                {
                                    FieldUserValue userAuthor = (FieldUserValue)listItemsCol["Author"];
                                    dr["Author"] = userAuthor.LookupValue;
                                }
                                if (listItemsCol["Approved_x0020_By"] == null)
                                {
                                    listItemsCol["Approved_x0020_By"] = "";
                                }
                                else
                                {
                                    FieldUserValue userAuthor = (FieldUserValue)listItemsCol["Approved_x0020_By"];
                                    dr["Approved_x0020_By"] = userAuthor.LookupValue;
                                }
                                dr["RegeneratePO"] = listItemsCol["RegeneratePO"];
                                dr["ID"] = listItemsCol["ID"];
                                dr["JobID"] = listItemsCol["JobID"];
                                dr["CostCentreID"] = listItemsCol["CostCentreID"];
                                dr["Sent"] = listItemsCol["Sent"];
                                dr["marked"] = listItemsCol["marked"];
                                dr["Created"] = listItemsCol["Created"];
                                dr["Complete"] = listItemsCol["Complete"];
                                dr["Recharge"] = listItemsCol["Recharge"];
                                dt.Rows.Add(dr);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                    dbConnection conn = new dbConnection();
                    DataTable tempTable = null;
                    tempTable = conn.executeSelectNoParameter(sqlQuery);
                    List oList = null;
                    ListItemCreationInformation itemCreateInfo = null;
                    ListItem oListItem = null;
                    oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    string job, eTSNo, itemsDescription, selectedJob, costCentre, reasonCode, supplier, deliveryDetails, supplierID, deliveryDate, price, gST, createdBy, approvedBy, regeneratePO, jobID, eTSId, costCentreID, created, recharge, itemType, path;
                    float id, sent, marked, complete;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drExists = tempTable.Select("ETSId = '" + dt.Rows[i][1].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + dt.Rows[i][1].ToString());
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Inserting - " + dt.Rows[i][1].ToString());
                                job = dt.Rows[i][0].ToString();
                                eTSId = dt.Rows[i][1].ToString();
                                //=IF(ETSId>999,CONCATENATE("E00000",ETSId),IF(ETSId>99,CONCATENATE("E000000",ETSId),IF(ETSId>9,CONCATENATE("E000000",ETSId),CONCATENATE("E",ETSId))))
                                if (Int32.Parse(eTSId) > 999) { eTSNo = "E0000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 99) { eTSNo = "E00000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 9) { eTSNo = "E00000" + eTSId; }
                                else { eTSNo = "E" + eTSId; }
                                itemsDescription = dt.Rows[i][2].ToString();
                                selectedJob = dt.Rows[i][3].ToString();
                                costCentre = dt.Rows[i][4].ToString();
                                reasonCode = dt.Rows[i][5].ToString();
                                supplier = dt.Rows[i][6].ToString();
                                deliveryDetails = dt.Rows[i][7].ToString();
                                supplierID = dt.Rows[i][8].ToString();
                                deliveryDate = string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][9].ToString();
                                price = string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? "0" : dt.Rows[i][10].ToString();
                                gST = dt.Rows[i][11].ToString();
                                createdBy = dt.Rows[i][12].ToString();
                                approvedBy = dt.Rows[i][13].ToString();
                                regeneratePO = string.IsNullOrEmpty(dt.Rows[i][14].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][14].ToString();
                                id = float.Parse(dt.Rows[i][15].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                jobID = dt.Rows[i][16].ToString();
                                costCentreID = dt.Rows[i][17].ToString();
                                sent = float.Parse(dt.Rows[i][18].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                marked = float.Parse(dt.Rows[i][19].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                created = string.IsNullOrEmpty(dt.Rows[i][20].ToString()) ? DateTime.Today.ToString() : dt.Rows[i][20].ToString();
                                complete = float.Parse(dt.Rows[i][21].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                recharge = dt.Rows[i][22].ToString();
                                itemType = "Item";
                                path = "sites/365Build/Watersun/Lists/l_ETSData";

                                //insert
                                string sclearsqlIns = string.Concat("INSERT INTO [WatersunData].[dbo].[ETSDataExportDemo]" +
                                                                       "([Job], [ETS No], [ItemsDescription], [Selected Job], [Cost Centre], [Reason Code], [Supplier], [DeliveryDetails], [SupplierID], [DeliveryDate], [Price], [GST], [Created By], [Approved By], [RegeneratePO], [ID], [JobID], [ETSId], [CostCentreID], [Sent], [marked], [Created], [Complete], [Recharge], [Item Type], [Path])" +
                                                                       "VALUES (@job, @eTSNo, @itemsDescription, @selectedJob, @costCentre, @reasonCode, @supplier, @deliveryDetails, @supplierID, @deliveryDate, @price, @gST, @createdBy, @approvedBy, @regeneratePO, @id, @jobID, @eTSId, @costCentreID, @sent, @marked, @created, @complete, @recharge, @itemType, @path)");
                                SqlParameter[] parameterUpd = {                                

                                                    new SqlParameter("@job", SqlDbType.NVarChar) { Value = job },
                                                    new SqlParameter("@eTSNo", SqlDbType.NVarChar) { Value = eTSNo },
                                                    new SqlParameter("@itemsDescription", SqlDbType.NVarChar) { Value = itemsDescription },
                                                    new SqlParameter("@selectedJob", SqlDbType.NVarChar) { Value = selectedJob },
                                                    new SqlParameter("@costCentre", SqlDbType.NVarChar) { Value = costCentre },
                                                    new SqlParameter("@reasonCode", SqlDbType.NVarChar) { Value = reasonCode },
                                                    new SqlParameter("@supplier", SqlDbType.NVarChar) { Value = supplier },
                                                    new SqlParameter("@deliveryDetails", SqlDbType.NVarChar) { Value = deliveryDetails },
                                                    new SqlParameter("@supplierID", SqlDbType.NVarChar) { Value = supplierID },
                                                    new SqlParameter("@deliveryDate", SqlDbType.DateTime) { Value = deliveryDate },
                                                    new SqlParameter("@price", SqlDbType.Money) { Value = price },
                                                    new SqlParameter("@gST", SqlDbType.NVarChar) { Value = gST },
                                                    new SqlParameter("@createdBy", SqlDbType.NVarChar) { Value = createdBy },
                                                    new SqlParameter("@approvedBy", SqlDbType.NVarChar) { Value = approvedBy },
                                                    new SqlParameter("@regeneratePO", SqlDbType.DateTime) { Value = regeneratePO },
                                                    new SqlParameter("@id", SqlDbType.Float) { Value = id },
                                                    new SqlParameter("@jobID", SqlDbType.NVarChar) { Value = jobID },
                                                    new SqlParameter("@eTSId", SqlDbType.NVarChar) { Value = eTSId },
                                                    new SqlParameter("@costCentreID", SqlDbType.NVarChar) { Value = costCentreID },
                                                    new SqlParameter("@sent", SqlDbType.Float) { Value = sent },
                                                    new SqlParameter("@marked", SqlDbType.Float) { Value = marked },
                                                    new SqlParameter("@created", SqlDbType.DateTime) { Value = created },
                                                    new SqlParameter("@complete", SqlDbType.Float) { Value = complete },
                                                    new SqlParameter("@recharge", SqlDbType.NVarChar) { Value = recharge },
                                                    new SqlParameter("@itemType", SqlDbType.NVarChar) { Value = itemType },
                                                    new SqlParameter("@path", SqlDbType.NVarChar) { Value = path }
                                                         };
                                bool isInsert = conn.executeInsertQuery(sclearsqlIns, parameterUpd);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                dt.Dispose();
            }
        }

        /*
        private static void SpToSQL(string tenant, string userName, string passwordString, string key, string appSettingsKey, string columnName, string sqlQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var ctx = new ClientContext(tenant))
                {
                    var passWord = new SecureString();
                    foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                    ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);

                    List getList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query></Query></View>";
                    ListItemCollection getListItemsCol = getList.GetItems(camlQuery);

                    ctx.Load(getListItemsCol);

                    ctx.ExecuteQuery();

                    dt.Columns.Add("Job");
                    dt.Columns.Add("ETSId");
                    dt.Columns.Add("ItemsDescription");
                    dt.Columns.Add("Selected_x0020_Job");
                    dt.Columns.Add("Cost_x0020_Centre");
                    dt.Columns.Add("Reason_x0020_Code");
                    dt.Columns.Add("Supplier");
                    dt.Columns.Add("DeliveryDetails");
                    dt.Columns.Add("SupplierID");
                    dt.Columns.Add("DeliveryDate");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("GST");
                    dt.Columns.Add("Author");
                    dt.Columns.Add("Approved_x0020_By");
                    //dt.Columns.Add("-----Purchase Order]  ");
                    dt.Columns.Add("RegeneratePO");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("JobID");
                    dt.Columns.Add("CostCentreID");
                    dt.Columns.Add("Sent");
                    dt.Columns.Add("marked");
                    dt.Columns.Add("Created");
                    dt.Columns.Add("Complete");
                    dt.Columns.Add("Recharge");

                    if (getListItemsCol != null && getListItemsCol.Count > 0)
                    {
                        foreach (ListItem listItemsCol in getListItemsCol)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Job"] = listItemsCol["Title"];
                            dr["ETSId"] = listItemsCol["ETSId"];
                            dr["ItemsDescription"] = listItemsCol["ItemsDescription"];
                            dr["Selected_x0020_Job"] = listItemsCol["Selected_x0020_Job"];
                            dr["Cost_x0020_Centre"] = listItemsCol["Cost_x0020_Centre"];
                            dr["Reason_x0020_Code"] = listItemsCol["Reason_x0020_Code"];
                            dr["Supplier"] = listItemsCol["Supplier"];
                            dr["DeliveryDetails"] = listItemsCol["DeliveryDetails"];
                            dr["SupplierID"] = listItemsCol["SupplierID"];
                            dr["DeliveryDate"] = listItemsCol["DeliveryDate"];
                            dr["Price"] = listItemsCol["Price"];
                            dr["GST"] = listItemsCol["GST"];
                            dr["Author"] = listItemsCol["Author"];
                            dr["Approved_x0020_By"] = listItemsCol["Approved_x0020_By"];
                            //dr["-----Purchase Order]  "] = listItemsCol["-----Purchase Order]  "];
                            dr["RegeneratePO"] = listItemsCol["RegeneratePO"];
                            dr["ID"] = listItemsCol["ID"];
                            dr["JobID"] = listItemsCol["JobID"];
                            dr["CostCentreID"] = listItemsCol["CostCentreID"];
                            dr["Sent"] = listItemsCol["Sent"];
                            dr["marked"] = listItemsCol["marked"];
                            dr["Created"] = listItemsCol["Created"];
                            dr["Complete"] = listItemsCol["Complete"];
                            dr["Recharge"] = listItemsCol["Recharge"];
                            dt.Rows.Add(dr);
                        }
                    }

                    dbConnection conn = new dbConnection();
                    DataTable tempTable = null;
                    tempTable = conn.executeSelectNoParameter(sqlQuery);
                    List oList = null;
                    ListItemCreationInformation itemCreateInfo = null;
                    ListItem oListItem = null;
                    oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                    string job, eTSNo, itemsDescription, selectedJob, costCentre, reasonCode, supplier, deliveryDetails, supplierID, deliveryDate, price, gST, createdBy, approvedBy, regeneratePO, jobID, eTSId, costCentreID, created, recharge, itemType, path;
                    float id, sent, marked, complete;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drExists = tempTable.Select("ETSId = '" + dt.Rows[i][1].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + dt.Rows[i][1].ToString());
                        }
                        else
                        {
                            try
                            {
                                foreach (DataRow rr in dt.Rows)
                                {
                                    job = rr["Job"].ToString();
                                    eTSId = rr["ETSId"].ToString();

                                    if (Int32.Parse(eTSId) > 999) { eTSNo = "E00000" + eTSId; }
                                    else if (Int32.Parse(eTSId) > 99) { eTSNo = "E000000" + eTSId; }
                                    else if (Int32.Parse(eTSId) > 9) { eTSNo = "E000000" + eTSId; }
                                    else { eTSNo = "E" + eTSId; }

                                    itemsDescription = rr["ItemsDescription"].ToString();
                                    selectedJob = rr["Selected_x0020_Job"].ToString();
                                    costCentre = rr["Cost_x0020_Centre"].ToString();
                                    reasonCode = rr["Reason_x0020_Code"].ToString();
                                    supplier = rr["Supplier"].ToString();
                                    deliveryDetails = rr["DeliveryDetails"].ToString();
                                    supplierID = rr["SupplierID"].ToString();
                                    deliveryDate = string.IsNullOrEmpty(rr["DeliveryDate"].ToString()) ? DateTime.Today.ToString() : rr["DeliveryDate"].ToString();
                                    price = string.IsNullOrEmpty(rr["Price"].ToString()) ? "0" : rr["Price"].ToString();
                                    gST = rr["GST"].ToString();
                                    createdBy = rr["Author"].ToString();
                                    approvedBy = rr["Approved_x0020_By"].ToString();
                                    //string purchaseOrder    = "";
                                    regeneratePO = string.IsNullOrEmpty(rr["RegeneratePO"].ToString()) ? DateTime.Today.ToString() : rr["RegeneratePO"].ToString();
                                    id = float.Parse(rr["ID"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    jobID = rr["JobID"].ToString();
                                    costCentreID = rr["CostCentreID"].ToString();
                                    sent = float.Parse(rr["Sent"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    marked = float.Parse(rr["marked"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    created = string.IsNullOrEmpty(rr["Created"].ToString()) ? DateTime.Today.ToString() : rr["Created"].ToString();
                                    complete = float.Parse(rr["Complete"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    recharge = rr["Recharge"].ToString();
                                    itemType = "Item";
                                    path = "sites/365Build/Watersun/Lists/l_ETSData";
                                    //
                                    // job =                rr["Job"].ToString();
                                    // eTSId =              rr["ETSId"].ToString();

                                    // if (Int32.Parse(eTSId) > 999) { eTSNo = "E00000" + eTSId; }
                                    //     else if (Int32.Parse(eTSId) > 99) { eTSNo = "E000000" + eTSId; }
                                    //     else if (Int32.Parse(eTSId) > 9) { eTSNo = "E000000" + eTSId; }
                                    //     else { eTSNo = "E" + eTSId; }

                                    // itemsDescription =   rr["ItemsDescription"].ToString();
                                    // selectedJob =        rr["Selected_x0020_Job"].ToString();
                                    // costCentre =         rr["Cost_x0020_Centre"].ToString();
                                    // reasonCode =         rr["Reason_x0020_Code"].ToString();
                                    // supplier =           rr["Supplier"].ToString();
                                    // deliveryDetails =    rr["DeliveryDetails"].ToString();
                                    // supplierID =         rr["SupplierID"].ToString();
                                    // deliveryDate =       string.IsNullOrEmpty(rr["DeliveryDate"].ToString()) ? DateTime.Today.ToString() : rr["DeliveryDate"].ToString();
                                    // price =              string.IsNullOrEmpty(rr["Price"].ToString()) ? "0" : rr["Price"].ToString();
                                    // gST =                rr["GST"].ToString();
                                    // createdBy =          rr["Author"].ToString();
                                    // approvedBy =         rr["Approved_x0020_By"].ToString();
                                    ////string purchaseOrder    = "";
                                    // regeneratePO =       string.IsNullOrEmpty(rr["RegeneratePO"].ToString()) ? DateTime.Today.ToString() : rr["RegeneratePO"].ToString();
                                    // id =                 float.Parse(rr["ID"].ToString(), CultureInfo.InvariantCulture.NumberFormat);                                     
                                    // jobID =              rr["JobID"].ToString();
                                    // costCentreID =       rr["CostCentreID"].ToString();
                                    // sent =               float.Parse(rr["Sent"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    // marked =             float.Parse(rr["marked"].ToString(), CultureInfo.InvariantCulture.NumberFormat);                                    
                                    // created =            string.IsNullOrEmpty(rr["Created"].ToString()) ? DateTime.Today.ToString() : rr["Created"].ToString();
                                    // complete =           float.Parse(rr["Complete"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                                    // recharge =           rr["Recharge"].ToString();
                                    // itemType =           "Item";
                                    // path =               "sites/365Build/Watersun/Lists/l_ETSData";
                                    //
                                        //insert
                                     string sclearsqlIns = string.Concat("INSERT INTO [WatersunData].[dbo].[ETSDataExportDemo]" +
                                                                            "([Job], [ETS No], [ItemsDescription], [Selected Job], [Cost Centre], [Reason Code], [Supplier], [DeliveryDetails], [SupplierID], [DeliveryDate], [Price], [GST], [Created By], [Approved By], [RegeneratePO], [ID], [JobID], [ETSId], [CostCentreID], [Sent], [marked], [Created], [Complete], [Recharge], [Item Type], [Path])" +
                                                                            "VALUES (@job, @eTSNo, @itemsDescription, @selectedJob, @costCentre, @reasonCode, @supplier, @deliveryDetails, @supplierID, @deliveryDate, @price, @gST, @createdBy, @approvedBy, @regeneratePO, @id, @jobID, @eTSId, @costCentreID, @sent, @marked, @created, @complete, @recharge, @itemType, @path)");
                                        SqlParameter[] parameterUpd = {                                

                                                    new SqlParameter("@job", SqlDbType.NVarChar) { Value = job },
                                                    new SqlParameter("@eTSNo", SqlDbType.NVarChar) { Value = eTSNo },
                                                    new SqlParameter("@itemsDescription", SqlDbType.NVarChar) { Value = itemsDescription },
                                                    new SqlParameter("@selectedJob", SqlDbType.NVarChar) { Value = selectedJob },
                                                    new SqlParameter("@costCentre", SqlDbType.NVarChar) { Value = costCentre },
                                                    new SqlParameter("@reasonCode", SqlDbType.NVarChar) { Value = reasonCode },
                                                    new SqlParameter("@supplier", SqlDbType.NVarChar) { Value = supplier },
                                                    new SqlParameter("@deliveryDetails", SqlDbType.NVarChar) { Value = deliveryDetails },
                                                    new SqlParameter("@supplierID", SqlDbType.NVarChar) { Value = supplierID },
                                                    new SqlParameter("@deliveryDate", SqlDbType.DateTime) { Value = deliveryDate },
                                                    new SqlParameter("@price", SqlDbType.Money) { Value = price },
                                                    new SqlParameter("@gST", SqlDbType.NVarChar) { Value = gST },
                                                    new SqlParameter("@createdBy", SqlDbType.NVarChar) { Value = createdBy },
                                                    new SqlParameter("@approvedBy", SqlDbType.NVarChar) { Value = approvedBy },
                                                    new SqlParameter("@regeneratePO", SqlDbType.DateTime) { Value = regeneratePO },
                                                    new SqlParameter("@id", SqlDbType.Float) { Value = id },
                                                    new SqlParameter("@jobID", SqlDbType.NVarChar) { Value = jobID },
                                                    new SqlParameter("@eTSId", SqlDbType.NVarChar) { Value = eTSId },
                                                    new SqlParameter("@costCentreID", SqlDbType.NVarChar) { Value = costCentreID },
                                                    new SqlParameter("@sent", SqlDbType.Float) { Value = sent },
                                                    new SqlParameter("@marked", SqlDbType.Float) { Value = marked },
                                                    new SqlParameter("@created", SqlDbType.DateTime) { Value = created },
                                                    new SqlParameter("@complete", SqlDbType.Float) { Value = complete },
                                                    new SqlParameter("@recharge", SqlDbType.NVarChar) { Value = recharge },
                                                    new SqlParameter("@itemType", SqlDbType.NVarChar) { Value = itemType },
                                                    new SqlParameter("@path", SqlDbType.NVarChar) { Value = path }
                                                         };
                                        bool isInsert = conn.executeInsertQuery(sclearsqlIns, parameterUpd);
                                        

                                
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                dt.Dispose();
            }
        }
        */

        #endregion old code
    }
}
