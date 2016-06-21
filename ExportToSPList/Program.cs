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
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportToSPList
{
    class Program
    {
        //public static DataTable dt = new DataTable();
        static void Main(string[] args)
        {
            //ExportToExcel ex = new ExportToExcel();
            CreateListWF lw = new CreateListWF();
            lw.CreateListWFs();
            //ex.SaveToExcel();

            //WF wf = new WF();
            //wf.AssignWF();

            //ReadAllSettings();
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
                    string sqlQuery;
                    foreach (var key in appSettings.AllKeys)
                    {
                        sqlQuery = System.String.Empty;
                        switch (key)
                        {
                            case "JobsDataList":
                                Console.WriteLine("JobsDataList");
                                sqlQuery = @"
                                                SELECT  [JobNumber]
                                                      ,[JobAddress]
                                                      ,[Supervisor]
                                                      ,[ConstructionManager]
                                                  FROM [WatersunData].[dbo].[vFrameworkJobs]
                                                where left(JobNumber,1) in (3,4,5)";

                                SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["JobsDataColumn"], sqlQuery);
                                break;
                            case "SuppliersList":
                                Console.WriteLine("SuppliersList");
                                sqlQuery = @"
                                                SELECT [Supplier_Code]
                                                      ,[SupplierName]
                                                      ,[AccountEmail]
                                                      ,[GroupList]
                                                  FROM [WatersunData].[dbo].[vSupplierList] order by [GroupList], [SupplierName]
                                                ";

                                SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["SuppliersColumn"], sqlQuery);
                                break;
                            case "ETSData":
                                Console.WriteLine("ETSData");
                                #region query
                                sqlQuery = @"
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
													  ,[RechargeID]
													  ,[RechargeAmount]
													  ,[RechargeAMSupID]
													  ,[RechargeNZSupID]
													  ,[ReasonDescription]

                                                  FROM [WatersunData].[dbo].[ETSDataExportDemo]
                                                ";
                                #endregion

                                SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["ETSDataColumn"], sqlQuery);
                                break;
                            case "ClientData":
                                Console.WriteLine("ClientData");
                                #region query
                                sqlQuery = @"
                                            SELECT 
                                                job.l_job_id
                                                ,client.s_name as Client
                                                ,job.s_job_num,
                                                addr.s_addr_full as JobAddress
                                                ,list_item.s_name_ref  as jobstatus
                                                ,wfl_stgMinor.s_name_ref as Stage
                                                --,wfl_stgMajor.s_name_ref as Stage2
                                                --,addr.s_street_num
                                                --client.s_name_ref as Contact
                                                --,client.s_salutation
                                                --,job_Type.s_name 

                                                FROM FworkSQLEcm.dbo.job job
                                                LEFT JOIN FworkSQLEcm.dbo.clientFile  clientFile ON clientFile.l_file_id = job.l_file_id
                                                LEFT JOIN FworkSQLEcm.dbo.land  land ON land.l_land_id = clientFile.l_land_id
                                                LEFT JOIN FworkSQLEcm.dbo.client client ON client.l_client_id = clientFile.l_client_id
                                                LEFT JOIN FworkSQLEcm.dbo.cst cst on cst.l_job_id = job.l_job_id
                                                LEFT JOIN FworkSQLEcm.dbo.addr addr ON addr.l_addr_id = land.l_addr_id
                                                LEFT JOIN list_item ON list_item.l_list_item_id = job.l_job_status_gl_id 
                                                --FULL JOIN job_Type ON job_type.l_job_type_id = job.l_job_id --job_Type.s_name 
                                                LEFT JOIN wfl_stgMinor ON wfl_stgMinor.l_wfl_stgMinor_id = job.l_wfl_stgMinor_id 
                                                LEFT JOIN wfl_stgMajor ON wfl_stgMajor.l_wfl_stgMajor_id = job.l_wfl_stgMajor_id 
                                                --where job.s_job_num='4270'
                                           ";
                                #endregion

                                //SqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["ClientDataColumn"], sqlQuery);
                                break;
                            case "JobsSuppList":
                                Console.WriteLine("JobsSuppList");
                                sqlQuery = @"
                                                SELECT  [JobNumber]
                                                      ,[JobAddress]
                                                      ,[Supervisor]
                                                      ,[ConstructionManager]
                                                  FROM [WatersunData].[dbo].[vFrameworkJobs]
                                                where left(JobNumber,1) in (3,4,5)";
                                UpdateSqlSpConnect(tenant, userName, passwordString, key, appSettings[key], appSettings["JobsIDColumn"], appSettings["JobsSuppColumn"], sqlQuery);
                                break;
                            default:
                                Console.WriteLine(key);
                                break;
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

                if (key == "SuppliersList" || key == "JobsDataList" || key == "ClientData")
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

                    dt.Columns.Add("RechargeID");
                    dt.Columns.Add("Recharge_x0020_Amount");
                    dt.Columns.Add("RechargeAMSupID");
                    dt.Columns.Add("RechargeNZSupID");
                    dt.Columns.Add("ReasonDescription");

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

                        dr["RechargeID"] = listItemsCol["RechargeID"];
                        dr["Recharge_x0020_Amount"] = listItemsCol["Recharge_x0020_Amount"];
                        dr["RechargeAMSupID"] = listItemsCol["RechargeAMSupID"];
                        dr["RechargeNZSupID"] = listItemsCol["RechargeNZSupID"];
                        dr["ReasonDescription"] = listItemsCol["ReasonDescription"];

                        dt.Rows.Add(dr);
                    }
                }

                dbConnection conn = new dbConnection();
                DataTable tempTable = null;
                tempTable = conn.executeSelectNoParameter(sqlQuery);
                ListItemCreationInformation itemCreateInfo = null;
                ListItem oListItem = null;
                List oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                if (key == "SuppliersList" || key == "JobsDataList" || key == "ClientData")
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        DataRow[] drExists = dt.Select("JobNumber = '" + tempTable.Rows[i][0].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + tempTable.Rows[i][0].ToString());
                            if (key == "JobsDataList")
                            {
                                xlWorkSheet.Cells[i + 1, 1] = "Found Job Number - ";
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }
                            else if (key == "SuppliersList")
                            {
                                xlWorkSheet.Cells[i + 1, 1] = "Found Supplier Code - ";
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }
                            else if (key == "ClientData")
                            {
                                xlWorkSheet.Cells[i + 1, 1] = "Found Client with JobId - ";
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }
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
                                oListItem["ConstructionManager"] = tempTable.Rows[i][3].ToString();
                                xlWorkSheet.Cells[i + 1, 1] = "Inserting Job Number - ";
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }
                            else if (key == "SuppliersList")
                            {
                                oListItem["Title"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierCode"] = tempTable.Rows[i][0].ToString();
                                oListItem["SupplierName"] = tempTable.Rows[i][1].ToString();
                                oListItem["SupplierEmail"] = tempTable.Rows[i][2].ToString();
                                //oListItem["ListGroup"] = tempTable.Rows[i][3].ToString();
                                oListItem["ListGroup"] = Int32.Parse(tempTable.Rows[i][3].ToString());
                                xlWorkSheet.Cells[i + 1, 1] = "Inserting Supplier Code - ";
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }
                            else if (key == "ClientData")
                            {
                                //oListItem["l_job_id"] = tempTable.Rows[i][0].ToString();
                                oListItem["l_job_id"] = Int32.Parse(tempTable.Rows[i][0].ToString());
                                oListItem["Client"] = tempTable.Rows[i][1].ToString();
                                oListItem["s_job_num"] = tempTable.Rows[i][2].ToString();
                                oListItem["s_addr_full"] = tempTable.Rows[i][3].ToString();
                                oListItem["JobStatus"] = tempTable.Rows[i][4].ToString();
                                oListItem["Stage"] = tempTable.Rows[i][5].ToString();
                                xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                            }

                            oListItem.Update();
                            ctx.ExecuteQuery();
                        }
                    }
                    xlWorkBook.SaveAs("C:\\Log\\SqlToSpLog_Inserting" + DateTime.Now.ToString("_ddMMyyyy_HHmmss") + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    gl.releaseObject(xlWorkSheet);
                    gl.releaseObject(xlWorkBook);
                    gl.releaseObject(xlApp);
                }
                else if (key == "ETSData")
                {
                    string job, eTSNo, itemsDescription, selectedJob, costCentre, reasonCode, supplier, deliveryDetails, supplierID, deliveryDate, price, gST, createdBy, approvedBy, regeneratePO, jobID, eTSId, costCentreID, created, recharge, itemType, path, RechargeID, RechargeAmount, RechargeAMSupID, RechargeNZSupID, ReasonDescription;
                    float id, sent, marked, complete;

                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow[] drExists = tempTable.Select("ETSId = '" + dt.Rows[i][1].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            Console.WriteLine("Found - " + dt.Rows[i][1].ToString());
                            xlWorkSheet.Cells[i + 1, 1] = "Found EtsId - ";
                            xlWorkSheet.Cells[i + 1, 2] = dt.Rows[i][1].ToString();
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine("Inserting - " + dt.Rows[i][1].ToString());
                                xlWorkSheet.Cells[i + 1, 1] = "Inserting EtsId - ";
                                xlWorkSheet.Cells[i + 1, 2] = dt.Rows[i][1].ToString();

                                job = dt.Rows[i][0].ToString();
                                eTSId = dt.Rows[i][1].ToString();

                                if (Int32.Parse(eTSId) > 999) { eTSNo = "E0000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 99) { eTSNo = "E00000" + eTSId; }
                                else if (Int32.Parse(eTSId) > 9) { eTSNo = "E00000" + eTSId; }
                                else { eTSNo = "E" + eTSId; }
                                //if (String.IsNullOrEmpty(eTSId))
                                //{
                                //    Console.WriteLine("Found Empty EtsId - " + dt.Rows[i][1].ToString());
                                //    eTSNo = String.Empty;
                                //}
                                //else
                                //{
                                //    if (Int32.Parse(eTSId) > 999) { eTSNo = "E0000" + eTSId; }
                                //    else if (Int32.Parse(eTSId) > 99) { eTSNo = "E00000" + eTSId; }
                                //    else if (Int32.Parse(eTSId) > 9) { eTSNo = "E00000" + eTSId; }
                                //    else { eTSNo = "E" + eTSId; }
                                //}
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

                                RechargeID = dt.Rows[i][23].ToString();
                                RechargeAmount = string.IsNullOrEmpty(dt.Rows[i][24].ToString()) ? "0" : dt.Rows[i][10].ToString(); //dt.Rows[i][29].ToString();
                                RechargeAMSupID = dt.Rows[i][25].ToString();
                                RechargeNZSupID = dt.Rows[i][26].ToString();
                                ReasonDescription = dt.Rows[i][27].ToString();


                                itemType = "Item";
                                path = "sites/365Build/Watersun/Lists/l_ETSData";

                                //insert
                                string sclearsqlIns = string.Concat("INSERT INTO [WatersunData].[dbo].[ETSDataExportDemo]" +
                                                                       "([Job], [ETS No], [ItemsDescription], [Selected Job], [Cost Centre], [Reason Code], [Supplier], [DeliveryDetails], [SupplierID], [DeliveryDate], [Price], [GST], [Created By], [Approved By], [RegeneratePO], [ID], [JobID], [ETSId], [CostCentreID], [Sent], [marked], [Created], [Complete], [Recharge], [Item Type], [Path], [RechargeID], [RechargeAmount], [RechargeAMSupID], [RechargeNZSupID], [ReasonDescription])" +
                                                                       "VALUES (@job, @eTSNo, @itemsDescription, @selectedJob, @costCentre, @reasonCode, @supplier, @deliveryDetails, @supplierID, @deliveryDate, @price, @gST, @createdBy, @approvedBy, @regeneratePO, @id, @jobID, @eTSId, @costCentreID, @sent, @marked, @created, @complete, @recharge, @itemType, @path                          , @RechargeID, @RechargeAmount,   @RechargeAMSupID,  @RechargeNZSupID,  @ReasonDescription  )");
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
                                                    new SqlParameter("@path", SqlDbType.NVarChar) { Value = path },

                                                    //@RechargeID, @RechargeAmount,   @RechargeAMSupID,  @RechargeNZSupID,  @ReasonDescription
                                                    new SqlParameter("@RechargeID", SqlDbType.NVarChar) { Value = RechargeID },
                                                    new SqlParameter("@RechargeAmount", SqlDbType.Money) { Value = RechargeAmount },
                                                    new SqlParameter("@RechargeAMSupID", SqlDbType.NVarChar) { Value = RechargeAMSupID },
                                                    new SqlParameter("@RechargeNZSupID", SqlDbType.NVarChar) { Value = RechargeNZSupID },
                                                    new SqlParameter("@ReasonDescription", SqlDbType.NVarChar) { Value = ReasonDescription }

                                                         };
                                bool isInsert = conn.executeInsertQuery(sclearsqlIns, parameterUpd);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                    xlWorkBook.SaveAs("C:\\Log\\SqlToSpLog_Inserting" + DateTime.Now.ToString("_ddMMyyyy_HHmmss") + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    gl.releaseObject(xlWorkSheet);
                    gl.releaseObject(xlWorkBook);
                    gl.releaseObject(xlApp);
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

        private static void UpdateSqlSpConnect(string tenant, string userName, string passwordString, string key, string appSettingsKey, string columnName, string columnName2, string sqlQuery)
        {
            try
            {
                GlobalLogic gl = new GlobalLogic();
                ClientContext ctx = gl.ConnectSP(tenant, userName, passwordString);

                ListItemCollection getListItemsCol = gl.getListData(tenant, userName, passwordString, appSettingsKey);

                DataTable dt = new DataTable();

                if (key == "JobsSuppList")
                {
                    dt.Columns.Add("ID");
                    dt.Columns.Add("JobNumber");
                    dt.Columns.Add("SuppName");
                    dt.Columns.Add("ConstructionManager");
                    if (getListItemsCol != null)
                    {
                        foreach (ListItem listItemsCol in getListItemsCol)
                        {
                            DataRow dr = dt.NewRow();
                            dr["JobNumber"] = listItemsCol[columnName];
                            dr["SuppName"] = listItemsCol[columnName2];
                            dr["ConstructionManager"] = listItemsCol["ConstructionManager"];
                            dr["ID"] = listItemsCol["ID"];
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["JobNumber"] = "";
                        dr["SuppName"] = "";
                        dr["ConstructionManager"] = "";
                        dr["ID"] = "";
                        dt.Rows.Add(dr);
                    }
                }

                dbConnection conn = new dbConnection();
                DataTable tempTable = null;
                tempTable = conn.executeSelectNoParameter(sqlQuery);
                ListItemCreationInformation itemCreateInfo = null;
                ListItem oListItem = null;
                List oList = ctx.Web.Lists.GetByTitle(appSettingsKey);

                if (key == "JobsSuppList")
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    for (int i = 0; i < tempTable.Rows.Count; i++)
                    {
                        DataRow[] drExists = dt.Select("JobNumber = '" + tempTable.Rows[i][0].ToString() + "'");
                        if (drExists != null && drExists.Length > 0)
                        {
                            DataRow[] drExists2 = dt.Select("JobNumber = '" + tempTable.Rows[i][0].ToString() + "'" + "AND SuppName = '" + tempTable.Rows[i][2].ToString() + "'" + "AND ConstructionManager = '" + tempTable.Rows[i][3].ToString() + "'");
                            if (drExists2 != null && drExists2.Length > 0)
                            {
                                if (key == "JobsSuppList")
                                {
                                    Console.WriteLine("Found - " + tempTable.Rows[i][0].ToString());
                                    xlWorkSheet.Cells[i + 1, 1] = "Found Job Number - ";
                                    xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Update - " + tempTable.Rows[i][0].ToString());

                                oListItem = oList.GetItemById(drExists[0].ItemArray[0].ToString());
                                if (key == "JobsSuppList")
                                {
                                    oListItem["Job_x0020_Supervisor"] = tempTable.Rows[i][2].ToString();
                                    oListItem["ConstructionManager"] = tempTable.Rows[i][3].ToString();
                                    xlWorkSheet.Cells[i + 1, 1] = "Updating Supervisor - ";
                                    xlWorkSheet.Cells[i + 1, 2] = tempTable.Rows[i][0].ToString();
                                }

                                oListItem.Update();
                                ctx.ExecuteQuery();
                            }
                            //Console.WriteLine("Found - " + tempTable.Rows[i][0].ToString());
                        }
                    }
                    xlWorkBook.SaveAs("C:\\Log\\SqlToSpLog_Updating" + DateTime.Now.ToString("_ddMMyyyy_HHmmss") + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    gl.releaseObject(xlWorkSheet);
                    gl.releaseObject(xlWorkBook);
                    gl.releaseObject(xlApp);
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
