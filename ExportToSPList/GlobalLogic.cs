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

    }
}

