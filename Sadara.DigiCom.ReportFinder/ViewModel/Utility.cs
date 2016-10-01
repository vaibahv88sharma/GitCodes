using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    public class Utility
    {
        /// <summary>
        /// method to get web object of current web
        /// </summary>
        /// <returns></returns>
        public static SPWeb getWeb()
        {
            return SPContext.Current.Web;
        }
        public static SPSite getSite()
        {
            return SPContext.Current.Site;
        }

        public static bool IsSiteAdminCurrentUser()
        {
            // SPContext.Current.Site.RootWeb.CurrentUser.get;

            return System.Web.Security.Roles.IsUserInRole(SPRoleType.Administrator.ToString());
        }

        public static List<string> GetChoiceFieldValues(string listName, string fieldName)
        {
            List<string> fieldList;
            SPWeb spWeb = Utility.getWeb();
            SPList spList = spWeb.Lists[listName];
            SPFieldChoice field = (SPFieldChoice)spList.Fields[fieldName];
            fieldList = new List<string>();
            foreach (string str in field.Choices)
            {
                fieldList.Add(str);
            }
            return fieldList;
        }


        public static SPListItemCollection GetMaxKeyFromList(SPList list, string fieldName)
        {
            SPListItemCollection objItems;
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                               "<FieldRef Name='" + fieldName + "'/>",
                                               "</ViewFields>",
                                               "<Query>",
                                               "<OrderBy>",
                                               "<FieldRef Name='" + fieldName + "' Ascending='FALSE'/>",
                                               "</OrderBy>",
                                               "</Query>",
                                               "<RowLimit>1</RowLimit>",
                                               "</View>");

            objItems = list.GetItems(qryCourse);
            return objItems;
        }


        public static SPListItemCollection GetMaxKeyFromList(string listName, string fieldName)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[listName];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                               "<FieldRef Name='" + fieldName + "'/>",
                                               "</ViewFields>",
                                               "<Query>",
                                               "<OrderBy>",
                                               "<FieldRef Name='" + fieldName + "' Ascending='FALSE'/>",
                                               "</OrderBy>",
                                               "</Query>",
                                               "<RowLimit>1</RowLimit>",
                                               "</View>");

            objItems = list.GetItems(qryCourse);
            return objItems;
        }

        public static SPListItemCollection getODMSRequirement(SPList list, string requirementReference, string requirementNumber)
        {

            SPListItemCollection objItems;
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where><And>",


                                               "<Eq><FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "' LookupId='True'/>",
                                               "<Value Type='Lookup'>" + requirementReference + "</Value></Eq>",
                //<Eq>
                                              "<Eq><FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                              "<Value Type='Text'>" + requirementNumber + "</Value>",
                                              "</Eq></And></Where></Query></View>");
            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public static void AddSharePointNotification(System.Web.UI.WebControls.Literal _Literal, string text)
        {
            //build up javascript to inject at the tail end of the page 
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<script>");
            //First wait until the SP.js is loaded, otherwise the notification doesn’t work 
            //gets an null reference exception 
            stringBuilder.AppendLine("ExecuteOrDelayUntilScriptLoaded(ShowNotification, 'sp.js');");
            stringBuilder.AppendLine("function ShowNotification()");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(string.Format("SP.UI.Notify.addNotification('{0}');", text));
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("</script>");
            //add to the page 
            // page.Controls.Add(new LiteralControl(stringBuilder.ToString()));
            _Literal.Text = Convert.ToString(stringBuilder);

        }
    }
}
