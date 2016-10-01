using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    class ReportFinderMethods
    {

        #region Requirement Methods

        public SPListItemCollection getAllODMSRequirementReference()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENTREFERENCE];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<ViewFields>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCENAME + "'/>",
                                               "</ViewFields>",
                                               "</View>");

            objItems = list.GetItems(qryCourse);
            return objItems;
        }
        public SPListItemCollection getODMSRequirementReference(string requirementReferenceNameOrReference)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENTREFERENCE];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "</ViewFields>",
                                               "<Query><Where>",
                                               "<Or>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "<Value Type='Text'>" + requirementReferenceNameOrReference + "</Value></Eq>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSREQUIREMENTREFERENCE_FIELDS_REQUIREMENTREFERENCENAME + "'/>",
                                               "<Value Type='Text'>" + requirementReferenceNameOrReference + "</Value></Eq>",
                                                "</Or>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            return objItems;
        }
        public SPListItemCollection getODMSRequirement(string requirementReference)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENT];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",                                                                                            
                                              "</ViewFields>",
                                              "<Query><Where><Eq><FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                              "<Value Type='Text'>" + requirementReference + "</Value></Eq></Where></Query></View>");
            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public SPListItemCollection getSingleODMSRequirement(string requirementKey)
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSREQUIREMENT];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_ID + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<Eq><FieldRef Name='" + Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY + "'/>",
                                              "<Value Type='Number'>" + requirementKey + "</Value></Eq>",
                                              "</Where></Query><RowLimit>1</RowLimit></View>");
            // qryCourse.Query = string.Empty;
            objItems = list.GetItems(qryCourse);
            return objItems;
        }
        #endregion

        #region Driver Detail methods

        public void DeleteODMSDriverFromDriverList(string ODMSDriverListIDHiddenField)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSDriver];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query><Where>",                                               
                                               "<Eq><FieldRef Name='" + Constants.ODMSDriver_FIELDS_ID + "'/>",
                                               "<Value Type='Number'>" + ODMSDriverListIDHiddenField + "</Value></Eq>",                                             
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Recycle();
                    break;
                }
            }
            catch (Exception exc) {
                throw;
            }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public void DeleteODMSDriver(string requirementKey, string driverID)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSDriverMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query><Where>",
                                               "<And>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSDriverMapping_FIELDS_DriverID + "'/>",
                                               "<Value Type='Number'>" + driverID + "</Value></Eq>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSDriverMapping_FIELDS_RequirementKey + "'/>",
                                               "<Value Type='Number'>" + requirementKey + "</Value></Eq>",
                                                "</And>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Delete();
                    break;
                }
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public void AddODMSDriver(string requirementKey, string driverID, int ID)
        {
            if (string.IsNullOrEmpty(requirementKey) || string.IsNullOrEmpty(driverID)) { return; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSDriverMapping];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSDriverMapping_FIELDS_RequirementKey] = new SPFieldLookupValue(Convert.ToInt32(Constants.Requirement_Value_ID), requirementKey);
                item[Constants.ODMSDriverMapping_FIELDS_DriverID] = new SPFieldLookupValue(ID, driverID);
                item.Update();
            }
            catch (Exception exc) {
                throw;
            }
            finally { web.AllowUnsafeUpdates = false; }


        }

        public int AddODMSDriverToDriver(string DriverKey, string Description, string DescriptionID, string DetailsDescription)
        {
            if (string.IsNullOrEmpty(DriverKey) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(DetailsDescription)) { return 0; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSDriver];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSDriver_FIELDS_DriverID] = DriverKey;
                item[Constants.ODMSDriver_FIELDS_DriverDescription] = new SPFieldLookupValue(Convert.ToInt32(DescriptionID), Description);
                item[Constants.ODMSDriver_FIELDS_DetailedDriverDescription] = DetailsDescription;
                item.Update();

            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }
            return item.ID;


        }

        public SPListItemCollection getODMSDriverMappingListItemCollection(DataTable odmsRequirementDatatable)
        {
            string value = string.Empty;
            List<string> uniqueREQUIREMENTKEY = new List<string>();
            foreach (DataRow keyRow in odmsRequirementDatatable.Rows)
            {
                string val = Convert.ToString(keyRow[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY]);
                if (!uniqueREQUIREMENTKEY.Contains(val))
                {
                    uniqueREQUIREMENTKEY.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSDriverMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSDriverMapping_FIELDS_DriverDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSDriverMapping_FIELDS_DriverID + "'/>",
                                               "<FieldRef Name=" + Constants.ODMSDriverMapping_FIELDS_RequirementKey + "/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSDriverMapping_FIELDS_RequirementKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSDriverListItemCollection(DataTable odmsDeiverMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueDriver = new List<string>();
            foreach (DataRow applicationRow in odmsDeiverMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSDriverMapping_FIELDS_DriverID]);
                if (!uniqueDriver.Contains(val))
                {
                    uniqueDriver.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSDriver];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverDescription + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DetailedDriverDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverID + "'/>",
                // "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationID + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSDriver_FIELDS_DriverID + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }


        public SPListItemCollection getODMSDriverDescriptionListItemCollection()
        {

            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSDriverDescription];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSDriverDescription_FIELDS_ID + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSDriverDescription_FIELDS_DriverDescription + "'/>",
                                              "</ViewFields>",
                                              "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSDriverListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSDriver];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverID + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }


        public SPListItemCollection GetDuplicateDriver(string DriverDeacription, string DriverDetailDescription)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSDriver];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query>",
                                               "<Where>",                                               
                                               "<And>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSDriver_FIELDS_DriverDescription + "'/>",
                                               "<Value Type='Text'>" + DriverDeacription + "</Value>",
                                               "</Eq>",
                                               "<Eq>",
                                               "<FieldRef Name='" + Constants.ODMSDriver_FIELDS_DetailedDriverDescription + "'/>",
                                               "<Value Type='Text'>" + DriverDetailDescription + "</Value>",
                                               "</Eq>",
                                               "</And>",                                                
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            return objItems;


        }
        #endregion

        #region Application Detail methode
        public SPListItemCollection getODMSApplicationMappingListItemCollection(DataTable odmsRequirementDatatable)
        {
            string value = string.Empty;
            List<string> uniqueREQUIREMENTKEY = new List<string>();
            foreach (DataRow keyRow in odmsRequirementDatatable.Rows)
            {
                string val = Convert.ToString(keyRow[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY]);
                if (!uniqueREQUIREMENTKEY.Contains(val))
                {
                    uniqueREQUIREMENTKEY.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplicationMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_Title + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSApplicationListItemCollection(DataTable odmsApplicationMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueApplication = new List<string>();
            foreach (DataRow applicationRow in odmsApplicationMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSApplicationMapping_FIELDS_ApplicationID]);
                if (!uniqueApplication.Contains(val))
                {
                    uniqueApplication.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplication];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationCategory + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationDescription + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationID + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSApplication_FIELDS_ApplicationID + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public SPListItemCollection getODMSApplicationListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSApplication];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_ApplicationCategory + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSApplication_FIELDS_TipText + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public void DeleteODMSApplicationMapping(string requirementKey, string ApplicationID)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSApplicationMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query>",
                                               "<Where>",
                                               "<And>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_ApplicationID + "'/>",
                                               "<Value Type='Number'>" + ApplicationID + "</Value></Eq>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                               "<Value Type='Number'>" + requirementKey + "</Value></Eq>",
                                                "</And>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Delete();
                    break;
                }
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public void AddODMSApplicationMapping(string requirementKey, string ApplicationID, string AID)
        {
            if (string.IsNullOrEmpty(requirementKey) || string.IsNullOrEmpty(ApplicationID)) { return; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSApplicationMapping];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSApplicationMapping_FIELDS_ApplicationID] = new SPFieldLookupValue(Convert.ToInt32(AID), ApplicationID.Trim());
                item[Constants.ODMSApplicationMapping_FIELDS_RequirementKey] = new SPFieldLookupValue(Convert.ToInt32(Constants.Requirement_Value_ID), requirementKey.Trim());
                item.Update();
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        #endregion

        #region Req Diff Code Detail methodes

        public void AddODMSReqDiffCodeMapping(string requirementKey, string GeographyID, string Geography, string ReqDiffCodeID, string ReqDiffCode)
        {
            if (string.IsNullOrEmpty(requirementKey) || string.IsNullOrEmpty(ReqDiffCode) || string.IsNullOrEmpty(Geography)) { return; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSReqDiffCodeMapping];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSReqDiffCodeMapping_FIELDS_RequirementKey] = new SPFieldLookupValue(Convert.ToInt32(Constants.Requirement_Value_ID), requirementKey);
                item[Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID] = new SPFieldLookupValue(Convert.ToInt32(ReqDiffCodeID), ReqDiffCode);
                item[Constants.ODMSReqDiffCodeMapping_FIELDS_Geography_Lookup] = new SPFieldLookupValue(Convert.ToInt32(GeographyID), Geography);
                item.Update();
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }

        public SPListItemCollection GetODMSReqDiffMappingMapping(string requirementKey, string geography, string reqDiffCode)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSReqDiffCodeMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query>",
                                               "<Where>",
                                                "<And>",
                                                        "<And>",
                                                        "<Eq><FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_RequirementKey + "'/>",
                                                        "<Value Type='Number'>" + requirementKey + "</Value>",
                                                        "</Eq>",
                                                        "<Eq>",
                                                        "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID + "'/>",
                                                        "<Value Type='Text'>" + reqDiffCode + "</Value>",
                                                        "</Eq>",
                                                        "</And>",
                                                "<Eq><FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_Geography_Lookup + "'/>",
                                                "<Value Type='Text'>" + geography + "</Value>",
                                                "</Eq>",
                                                "</And>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            return objItems;


        }


        public void DeleteODMSReqDiffCodeMapping(string ReqDiffCodeMappingID)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSReqDiffCodeMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query><Where>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_ID + "'/>",
                                               "<Value Type='Number'>" + ReqDiffCodeMappingID + "</Value></Eq>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Delete();
                    break;
                }
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }




        public SPListItemCollection getODMSReqDiffCodeMappingListItemCollection(DataTable odmsRequirementDatatable)
        {
            string value = string.Empty;
            List<string> uniqueREQUIREMENTKEY = new List<string>();
            foreach (DataRow keyRow in odmsRequirementDatatable.Rows)
            {
                string val = Convert.ToString(keyRow[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY]);
                if (!uniqueREQUIREMENTKEY.Contains(val))
                {
                    uniqueREQUIREMENTKEY.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSReqDiffCodeMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_RequirementKey + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_RDKey + "'/>",
                                                "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_Geography_Lookup + "'/>",
                                                 "<FieldRef Name='" + Constants.ODMSReqDiffCodeMapping_FIELDS_ID + "'/>",
                //"<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSReqDiffCodeMapping_FIELDS_RequirementKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSReqDiffCodeListItemCollection(DataTable odmsReqDiffCodeMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueApplication = new List<string>();
            foreach (DataRow applicationRow in odmsReqDiffCodeMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSReqDiffCodeMapping_FIELDS_ReqDiffCodeID]);
                if (!uniqueApplication.Contains(val))
                {
                    uniqueApplication.Add(val);
                    value += "<Value Type='Text'>" + val + "</Value>";
                }
            }

            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSReqDiffCode];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                //  "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCode + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSReqDiffCodeListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSReqDiffCode];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ReqDiffCodeID + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSReqDiffCode_FIELDS_ID + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSGeographyListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSGeography];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSGeography_FIELDS_Geography + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSGeography_FIELDS_ID + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion

        #region value details methode
        public SPListItemCollection getODMSValueListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSValue];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_Value + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueKey + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ID + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSValueMappingListItemCollection(DataTable odmsRequirementDatatable)
        {
            string value = string.Empty;
            List<string> uniqueREQUIREMENTKEY = new List<string>();
            foreach (DataRow keyRow in odmsRequirementDatatable.Rows)
            {
                string val = Convert.ToString(keyRow[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY]);
                if (!uniqueREQUIREMENTKEY.Contains(val))
                {
                    uniqueREQUIREMENTKEY.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSValueMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSValueMapping_FIELDS_RequirementKey + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValueMapping_FIELDS_ValueKey + "'/>",
                //"<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSValueMapping_FIELDS_RequirementKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSValueListItemCollection(DataTable odmsValueMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueApplication = new List<string>();
            foreach (DataRow applicationRow in odmsValueMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSValueMapping_FIELDS_ValueKey]);
                if (!uniqueApplication.Contains(val))
                {
                    uniqueApplication.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }

            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSValue];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_Value + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueDescription + "'/>",
                                               "<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSValue_FIELDS_ValueKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }

        public void DeleteODMSValueMapping(string requirementKey, string valueID)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSValueMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query>",
                                               "<Where>",
                                               "<And>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSValueMapping_FIELDS_ValueKey + "'/>",
                                               "<Value Type='Number'>" + valueID + "</Value></Eq>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSValueMapping_FIELDS_RequirementKey + "'/>",
                                               "<Value Type='Number'>" + requirementKey + "</Value></Eq>",
                                                "</And>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Delete();
                    break;
                }
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public void AddODMSValueMapping(string requirementKey, string ValueID, string VID)
        {
            if (string.IsNullOrEmpty(requirementKey) || string.IsNullOrEmpty(ValueID)) { return; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSValueMapping];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSValueMapping_FIELDS_ValueKey] = new SPFieldLookupValue(Convert.ToInt32(VID), ValueID.Trim());
                item[Constants.ODMSValueMapping_FIELDS_RequirementKey] = new SPFieldLookupValue(Convert.ToInt32(Constants.Requirement_Value_ID), requirementKey);
                item.Update();
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        #endregion

        #region CoordinationCode details methode


        public void AddODMSCoordinationMapping(string requirementKey, string CoordinationID, string CCID)
        {
            if (string.IsNullOrEmpty(requirementKey) || string.IsNullOrEmpty(CoordinationID)) { return; }
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSCoordinationCodeMapping];
            SPListItem item = list.Items.Add();
            web.AllowUnsafeUpdates = true;
            try
            {
                item[Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode] = new SPFieldLookupValue(Convert.ToInt32(CCID), CoordinationID.Trim());
                item[Constants.ODMSCoordinationCodeMapping_FIELDS_RequirementKey] = new SPFieldLookupValue(Convert.ToInt32(Constants.Requirement_Value_ID), requirementKey);
                item.Update();
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public void DeleteODMSCoordinationCodeMapping(string requirementKey, string CoordinationCodeID)
        {
            SPListItemCollection objItems;
            SPWeb web = Utility.getWeb();
            SPList list = web.Lists[Constants.LIST_NAME_ODMSCoordinationCodeMapping];
            SPQuery qryCourse = new SPQuery();
            qryCourse.ViewXml = string.Concat("<View>",
                                               "<Query>",
                                               "<Where>",
                                               "<And>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode + "'/>",
                                               "<Value Type='Number'>" + CoordinationCodeID + "</Value></Eq>",
                                               "<Eq><FieldRef Name='" + Constants.ODMSCoordinationCodeMapping_FIELDS_RequirementKey + "'/>",
                                               "<Value Type='Number'>" + requirementKey + "</Value></Eq>",
                                                "</And>",
                                               "</Where></Query> <RowLimit>1</RowLimit></View>");

            objItems = list.GetItems(qryCourse);
            web.AllowUnsafeUpdates = true;
            try
            {
                foreach (SPListItem item in objItems)
                {
                    item.Delete();
                    break;
                }
            }
            catch (Exception exc) { throw; }
            finally { web.AllowUnsafeUpdates = false; }


        }
        public SPListItemCollection getODMSCoordinationcodeListAllItemCollection()
        {
            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSCoordinationCode];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey + "'/>",
                                              "</ViewFields>",
                                             "</View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSCoordinationCodeMappingListItemCollection(DataTable odmsRequirementDatatable)
        {
            string value = string.Empty;
            List<string> uniqueREQUIREMENTKEY = new List<string>();
            foreach (DataRow keyRow in odmsRequirementDatatable.Rows)
            {
                string val = Convert.ToString(keyRow[Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY]);
                if (!uniqueREQUIREMENTKEY.Contains(val))
                {
                    uniqueREQUIREMENTKEY.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }


            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSCoordinationCodeMapping];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCodeMapping_FIELDS_RequirementKey + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode + "'/>",
                //"<FieldRef Name='" + Constants.ODMSApplicationMapping_FIELDS_RequirementKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSCoordinationCodeMapping_FIELDS_RequirementKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        public SPListItemCollection getODMSCoordinationCodeListItemCollection(DataTable odmsCoordinationCodeMappingDatatable)
        {
            string value = string.Empty;

            List<string> uniqueApplication = new List<string>();
            foreach (DataRow applicationRow in odmsCoordinationCodeMappingDatatable.Rows)
            {
                string val = Convert.ToString(applicationRow[Constants.ODMSCoordinationCodeMapping_FIELDS_CoordinationCode]);
                if (!uniqueApplication.Contains(val))
                {
                    uniqueApplication.Add(val);
                    value += "<Value Type='Number'>" + val + "</Value>";
                }
            }

            SPListItemCollection objItems;
            SPList list = Utility.getWeb().Lists[Constants.LIST_NAME_ODMSCoordinationCode];
            SPQuery qryCourse = new SPQuery();

            qryCourse.ViewXml = string.Concat("<View><ViewFields>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription + "'/>",
                                              "<FieldRef Name='" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey + "'/>",
                //"<FieldRef Name='" + Constants.ODMSValue_FIELDS_ValueKey + "'/>",
                                              "</ViewFields>",
                                              "<Query><Where>",
                                              "<In>",
                                              "<FieldRef Name=" + Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey + "/>",
                                              "<Values>",
                                                value,
                                              " </Values>",
                                              "</In>",
                                              "</Where></Query></View>");

            objItems = list.GetItems(qryCourse);

            return objItems;
        }
        #endregion
    }
}
