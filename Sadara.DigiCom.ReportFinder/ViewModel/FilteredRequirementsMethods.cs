using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    class FilteredRequirementsMethods
    {
        public SPListItemCollection geODMStApplicationMapping(string ApplicationKey)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplicationMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",                                             
                                              "</ViewFields>",
                                               "<Query><Where>",
                                              "<Eq>",
                                              "<FieldRef Name=" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "/>",
                                              "<Value Type='Lookup'>",
                                                ApplicationKey,
                                              "</Value>",
                                              "</Eq>",
                                              "</Where></Query>",                                             
                                              "</View>");


            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public SPListItemCollection getODMSRequirementsListItemCollection(DataTable odmsApplicationMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueApplication = new List<string>();
            foreach (DataRow applicationRow in odmsApplicationMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSApplicationMapping_FIELDS_RequirementKey]);
                if (!uniqueApplication.Contains(val))
                {
                    uniqueApplication.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }

            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENT];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "<FieldRef Name=" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
    }
}
