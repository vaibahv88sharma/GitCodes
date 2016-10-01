using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    class ApplicabilityReportMethods
    {
        public SPListItemCollection getODMSApplicationCategory()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplicationCategory];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationCategory_FIELDS_ApplicationCategory + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationCategory_FIELDS_EvaluationQuestion + "'/>",
                                              "</ViewFields>",
                                              "<Query><OrderBy>",
                                              "<FieldRef Name='"+ Constants.ODMSApplicationCategory_FIELDS_ApplicationCategory +"' Ascending='FALSE' />",
                                              "</OrderBy></Query>",
                                              "</View>");
            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public SPListItemCollection getODMSApplication(string ApplicationCategory)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplication];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationCategory + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationID + "'/>",
                                              "</ViewFields>",
                                               "<Query><Where>",
                                              "<Eq>",
                                              "<FieldRef Name=" + Constants.ODMSApplication_FIELDS_ApplicationCategory + "/>",
                                              "<Value Type='Lookup'>",
                                                ApplicationCategory,
                                              "</Value>",
                                              "</Eq>",
                                              "</Where></Query>",
                                              "<OrderBy>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationDescription + "' Ascending='FALSE' />",
                                              "</OrderBy>",
                                              "</View>");     
            
                                              
            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);

            return objItems;
        }

                public SPListItemCollection getODMSRequirements(List<string> applicationIDs)
                {
                    string value = string.Empty;

                    if (applicationIDs != null && applicationIDs.Count > 0)
                    {
                        for (int i = 0; i < applicationIDs.Count; i++)
                        {
                            string val = applicationIDs[i].ToString();
                            value += "<Value Type='Number'>" + val + "</Value>";                           
                        }
                    }               


                    SPListItemCollection objItems;
                    SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplicationMapping];
                    SPQuery qryCourse = new SPQuery();

                    qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                                      "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "'/>",
                                                       "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                                      "</ViewFields>",
                                                      "<Query><Where>",
                                                      "<In>",
                                                      "<FieldRef Name=" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "/>",
                                                      "<Values>",
                                                        value,
                                                      " </Values>",
                                                      "</In>",
                                                      "</Where></Query></View>");

                    objItems = list.GetItems(qryCourse);
                    List<string> uniqueDriver = new List<string>();
                    foreach (DataRow requirementRow in objItems.GetDataTable().Rows)
                    {
                        string val = Convert.ToString(requirementRow[Constants.ODMSApplicationMapping_FIELDS_RequirementKey]);
                        if (!uniqueDriver.Contains(val))
                        {
                            uniqueDriver.Add(val);
                            value += "<Value Type='Number'>" + val + "</Value>";
                        }
                    }


                    SPListItemCollection objItemsReq;
                    SPList listReq = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENT];
                    SPQuery qryCourseReq = new SPQuery();

                    qryCourseReq.ViewXml = string.Concat("<View><ViewFields>",
                                                      "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                                      "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",
                                                      "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                                      "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_RequirementLevel_Lookup+ "'/>",                        
                                                      "</ViewFields>",
                                                      "<Query><Where>",
                                                      "<In>",
                                                      "<FieldRef Name=" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "/>",
                                                      "<Values>",
                                                        value,
                                                      " </Values>",
                                                      "</In>",
                                                      "</Where></Query></View>");

                    objItemsReq = listReq.GetItems(qryCourseReq);

                    return objItemsReq;
                }

                public SPListItemCollection getODMSCoordinationCode()
                {
                    SPListItemCollection objItems;
                    SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSCoordinationCode];
                    SPQuery qryCourse = new SPQuery();

                    qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                                      "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription + "'/>",
                                                      "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey + "'/>",
                                                      "</ViewFields></View>");


                    // qryCourse.Query = string.Empty;
                    objItems = list.GetItems(qryCourse);

                    return objItems;
                }
    }
}
