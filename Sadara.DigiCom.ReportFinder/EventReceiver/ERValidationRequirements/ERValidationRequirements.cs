using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Sadara.DigiCom.ReportFinder.ViewModel;
using System.Web;

namespace Sadara.DigiCom.ReportFinder.EventReceiver.ERValidationRequirements
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ERValidationRequirements : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
            
            string requirementReference  = properties.AfterProperties[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE].ToString();
            string requirementNumber = properties.AfterProperties[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER].ToString();
            SPListItemCollection objItems = Utility.getODMSRequirement(properties.List,requirementReference,requirementNumber);
            if (objItems != null)
            {
                foreach (SPListItem item in objItems)
                {
                    string id = Convert.ToString(item[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE]);                    
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = "Please enter a valid Requirement Number";
                    properties.Cancel = true;
                    

                }
            }
        }
    }
}