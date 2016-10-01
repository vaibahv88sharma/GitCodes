using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.ERReqDiffCode
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ERReqDiffCode : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            try
            {
                string Field = "CoOrdinationKey";
                if (properties.ListItem[Field] == null)
                {
                    int maxValue = 1;

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
                }
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