using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.WFEventReceiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class WFEventReceiver : SPItemEventReceiver
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

                string Field = Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY;
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
            /*
            int maxValue = 0;
            try
            {

                SPList spList = properties.List;
                if (spList != null)
                {
                    SPQuery qry = new SPQuery();
                    qry.Query =
                    @"   <OrderBy>
                         <FieldRef Name='RequirementKey' Ascending='FALSE' />
                          </OrderBy>";
                    qry.ViewFields = @"<FieldRef Name='RequirementKey' />";
                    qry.RowLimit = 1;
                    SPListItemCollection objItems = spList.GetItems(qry);

                    if (objItems.Count > 0)
                    {
                        maxValue = Convert.ToInt32(objItems.GetDataTable().Rows[0]["RequirementKey"]) + 1;

                    }

                    properties.ListItem["RequirementKey"] = maxValue;
                    properties.ListItem.SystemUpdate(false);
                    base.EnableEventFiring();
                }
            }
            catch (Exception ex)
            {

                throw;
            }*/


        }
    }
}