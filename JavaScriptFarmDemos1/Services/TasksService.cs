using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Microsoft.SharePoint;
using System.Data;
using Microsoft.SharePoint.Utilities;
using System.Runtime.Serialization;

namespace JavaScriptFarmDemos.Services
{
    [DataContractFormat]
    public class TaskInfo
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public DateTime DueDate { get; set; }

        public TaskInfo(string title, DateTime dueDate)
        {
            this.Title = title;
            this.DueDate = dueDate;
        }
    }
    [ServiceContract]
    public interface ITasksService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetUpcomingTasks/{days}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json)]
        TaskInfo[] GetUpcomingTasks(string days);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    class TasksService : ITasksService
    {
        public TaskInfo[] GetUpcomingTasks(string days)
        {
            var web = SPContext.Current.Web;
            var query = new SPSiteDataQuery();
            query.ViewFields = @"<FieldRef Name='DueDate' /><FieldRef Name='Title' />";
            query.Query = @"<Query><Where><Gt><FieldRef Name='DueDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>2015-09-29T00:57:00Z</Value></Gt></Where></Query>";
            query.Lists = "<Lists ServerTemplate='171'";
            query.Webs = "<Webs Scope='SiteCollection' />";
            var tasks = web.GetSiteData(query);
            var results = new List<TaskInfo>();
            foreach (DataRow dr in tasks.Rows)
            {
                var title = dr["Title"].ToString();
                var dueDate = SPUtility.CreateDateTimeFromISO8601DateTimeString(dr["DueDate"].ToString());
                var item = new TaskInfo(title, dueDate);
                results.Add(item);               
            }
            return results.ToArray();
        }
    }

}
