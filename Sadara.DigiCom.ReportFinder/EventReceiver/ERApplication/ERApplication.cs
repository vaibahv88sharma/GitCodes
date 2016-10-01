using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.ERApplication
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ERApplication : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
            try
            {

                int maxValue = 1;

                string Field = "ApplicationKey";
                SPListItemCollection objItems = Utility.GetMaxKeyFromList(properties.List, Field); ;

                foreach (SPListItem item in objItems)
                {
                    string id = Convert.ToString(item[Field]);
                    if (!string.IsNullOrEmpty(id))
                    {
                        maxValue = Convert.ToInt32(id) + 1;
                    }
                    break;
                }
                properties.ListItem[Field] = maxValue;
                properties.ListItem.SystemUpdate(false);

                base.EnableEventFiring();

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}