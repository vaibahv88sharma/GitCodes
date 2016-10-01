using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.ERApplications
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ERApplications : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            int maxValue = 1;

            string Field = Constants.ODMSApplication_FIELDS_ApplicationID;
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


    }
}