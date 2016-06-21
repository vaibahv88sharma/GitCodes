using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.SharePoint.Client.WorkflowServices;
using Microsoft.SharePoint.Client.Workflow;

namespace ExportToSPList
{
    class GlobalLogic
    {
        public ClientContext ConnectSP(string tenant, string userName, string passwordString)
        {
            ClientContext context = null;
            try
            {
                ClientContext ctx = new ClientContext(tenant);
                var passWord = new SecureString();
                foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);
                context = ctx;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return context;
        }

        public ListItemCollection getListData(string tenant, string userName, string passwordString, string appSettingsKey)
        {
            ListItemCollection getListItemsCollection = null;
            try
            {
                GlobalLogic gl = new GlobalLogic();
                ClientContext ctx = gl.ConnectSP(tenant, userName, passwordString);

                List getList = ctx.Web.Lists.GetByTitle(appSettingsKey);
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = "<View><Query></Query></View>";
                ListItemCollection getListItemsCol = getList.GetItems(camlQuery);
                ctx.Load(getListItemsCol);
                ctx.ExecuteQuery();

                if (getListItemsCol != null && getListItemsCol.Count > 0)
                {
                    getListItemsCollection = getListItemsCol;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return getListItemsCollection;
        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        public Boolean createList(ClientContext ctx, string appSettingsKey)
        {
            bool success = false;
            try
            {
                Web hostWeb = ctx.Web;
                ListCreationInformation olist = new ListCreationInformation();

                olist.Title = appSettingsKey;
                olist.Description = appSettingsKey;
                olist.TemplateType = (int)ListTemplateType.TasksWithTimelineAndHierarchy;
                hostWeb.Lists.Add(olist);                
                ctx.ExecuteQuery();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
            //return getListItemsCollection;
        }

        public Boolean createListColumns(ClientContext ctx, string appSettingsKey)
        {
            bool success = false;
            try
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(appSettingsKey);

                Field field1 = list.Fields.AddFieldAsXml("<Field DisplayName='Sr No' Type='Number' />", true, AddFieldOptions.DefaultValue);
                FieldNumber fld1 = ctx.CastTo<FieldNumber>(field1);
                fld1.Update();

                Field field2 = list.Fields.AddFieldAsXml("<Field DisplayName='Cost Code' Type='Number' />", true, AddFieldOptions.DefaultValue);
                FieldNumber fld2 = ctx.CastTo<FieldNumber>(field2);
                fld2.Update();

                //Field field3 = list.Fields.AddFieldAsXml("<Field DisplayName='Activity' Type='Text' />", true, AddFieldOptions.DefaultValue);
                //FieldText fld3 = ctx.CastTo<FieldText>(field3);
                //fld3.Update();
                //Title

                Field userField1 = list.Fields.AddFieldAsXml("<Field DisplayName='Supplier' Type='User' />", true, AddFieldOptions.DefaultValue);
                FieldUser user1 = ctx.CastTo<FieldUser>(userField1);
                user1.Update();                

                Field dateField1 = list.Fields.AddFieldAsXml("<Field DisplayName='Called' Type='DateTime' />", true, AddFieldOptions.DefaultValue);
                FieldDateTime date1 = ctx.CastTo<FieldDateTime>(dateField1);
                date1.Update();

                Field dateField2 = list.Fields.AddFieldAsXml("<Field DisplayName='Called For' Type='DateTime' />", true, AddFieldOptions.DefaultValue);
                FieldDateTime date2 = ctx.CastTo<FieldDateTime>(dateField2);
                date1.Update();

                Field dateField3 = list.Fields.AddFieldAsXml("<Field DisplayName='Called Start' Type='DateTime' />", true, AddFieldOptions.DefaultValue);
                FieldDateTime date3 = ctx.CastTo<FieldDateTime>(dateField3);
                date1.Update();

                Field dateField4 = list.Fields.AddFieldAsXml("<Field DisplayName='Completion' Type='DateTime' />", true, AddFieldOptions.DefaultValue);
                FieldDateTime date4 = ctx.CastTo<FieldDateTime>(dateField4);
                date1.Update();

                ctx.ExecuteQuery();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        public Boolean createListView(ClientContext ctx, string appSettingsKey)
        {
            bool success = false;
            try
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(appSettingsKey);
    
                ViewCollection viewColl = list.Views;
                string[] viewFields = { "Checkmark", "PercentComplete", "Sr No", "Cost Code", "Title", "Supplier", "Called", "Called For", "Called Start", "Completion" };
                ViewCreationInformation creationInfo = new ViewCreationInformation();  
                creationInfo.Title = "TasksCreated";  
                creationInfo.RowLimit = 50;  
                creationInfo.ViewFields = viewFields;  
                creationInfo.ViewTypeKind = ViewType.None;  
                creationInfo.SetAsDefaultView = true;  
                viewColl.Add(creationInfo);  
                
                ctx.ExecuteQuery();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        public Guid getListGuid(ClientContext ctx, string appSettingsKey)
        {
            Guid success = Guid.Empty;
            try
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(appSettingsKey);

                ctx.Load(list);
                ctx.ExecuteQuery();
                //Guid id = list.Id;
                success = list.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return success;
        }

        public void addWorkflowSubscription(ClientContext clientContext, string listname, Guid targetListGuid)
        {            
            //Name of the SharePoint2013 List Workflow
            string workflowName = "EmailSupplier";
            //GUID of list on which to create the subscription (association);

            //Guid targetListGuid = new Guid("1145D77B-EB61-4C54-87A7-5B8FEEE05F07"); //fc50af29-8ae5-4303-bad1-213151818215
            //Name of the new Subscription (association)
            string newSubscriptionName = "WF-" + listname;
            //Workflow Lists
                //string workflowHistoryListID = "A0D1C88E-3F4A-44EF-AE1D-1B9450D3652B";  // EmailSupplier History   
                //string taskListID = "D631230F-23C1-41FA-AE02-7714BE79ED0D"; // emailsupplier tasks   
            string workflowHistoryListID = ConfigurationManager.AppSettings.Get("workflowHistoryListID");
            string taskListID = ConfigurationManager.AppSettings.Get("taskListID");


                Web web = clientContext.Web;
                //Workflow Services Manager which will handle all the workflow interaction.
                WorkflowServicesManager wfServicesManager = new WorkflowServicesManager(clientContext, web);
                //Deployment Service which holds all the Workflow Definitions deployed to the site
                WorkflowDeploymentService wfDeploymentService = wfServicesManager.GetWorkflowDeploymentService();
                //Get all the definitions from the Deployment Service, or get a specific definition using the GetDefinition method.
                WorkflowDefinitionCollection wfDefinitions = wfDeploymentService.EnumerateDefinitions(false);

                clientContext.Load(wfDefinitions, wfDefs => wfDefs.Where(wfd => wfd.DisplayName == workflowName));
                clientContext.ExecuteQuery();

                WorkflowDefinition wfDefinition = wfDefinitions.First();

                //The Subscription service is used to get all the Associations currently on the SPSite
                WorkflowSubscriptionService wfSubscriptionService = wfServicesManager.GetWorkflowSubscriptionService();
                //The subscription (association)
                WorkflowSubscription wfSubscription = new WorkflowSubscription(clientContext);
                wfSubscription.DefinitionId = wfDefinition.Id;
                wfSubscription.Enabled = true;
                wfSubscription.Name = newSubscriptionName;
                var startupOptions = new List<string>();

                // automatic start
                startupOptions.Add("ItemAdded");
                startupOptions.Add("ItemUpdated");
                // manual start
                startupOptions.Add("WorkflowStart");
                // set the workflow start settings
                wfSubscription.EventTypes = startupOptions;
                // set the associated task and history lists
                wfSubscription.SetProperty("HistoryListId", workflowHistoryListID);

                wfSubscription.SetProperty("TaskListId", taskListID);
                //Create the Association
                wfSubscriptionService.PublishSubscriptionForList(wfSubscription, targetListGuid);

                clientContext.ExecuteQuery();
        }

    }
}

