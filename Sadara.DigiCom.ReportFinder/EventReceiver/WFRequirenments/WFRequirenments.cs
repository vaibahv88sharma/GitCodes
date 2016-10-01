using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.WFRequirenments
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class WFRequirenments : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            
            base.ItemAdded(properties);
            try
            {

                int maxValue = 1;
                string Field = Constants.ODMSValue_FIELDS_ValueKey;
                SPListItemCollection objItems = Utility.GetMaxKeyFromList(properties.List, Field); ;

                foreach (SPListItem item in objItems)
                {
                    try
                    {
                        string id = Convert.ToString(item[Field]);
                        if (!string.IsNullOrEmpty(id))
                        {
                            maxValue = Convert.ToInt32(id) + 1;
                        }
                        break;
                    }
                    catch (Exception ex)
                    {                        
                        throw;
                    }
                  
                }
                properties.ListItem[Field] = maxValue;
                properties.ListItem.SystemUpdate(false);

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


    }
}