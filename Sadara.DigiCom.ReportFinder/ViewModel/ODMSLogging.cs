
#region Page Header
/***************************************************************************************************************************
 * File Name:       ODMSLogging.cs
 * 
 * Purpose:         
 *                  
 * Created By:      ODMS IDC team
 * 
 * Created Date:    02 Feb 2015
 *  
 * Comments:        
***************************************************************************************************************************/
#endregion Page Header

#region Assemblies
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
#endregion Assemblies

namespace Sadara.DigiCom.ReportFinder.ViewModel
{
    public class ODMSLogging : SPDiagnosticsServiceBase
    {

        /// <summary>
        /// CategoryID to be used to log an error in 15 Hive ULS logging file.
        /// </summary>
        public enum CategoryId
        {
            Unknown = 0,
            Deployment = 100,
            EventReceiver = 200,
            FeatureActivation = 300,
            Branding = 400,
            Controls = 500,
            Globalization = 600,
            Template = 700,
            TimerJobs = 800
        }

        #region Static variables
        //Disagnostics Area Name
        private static string SMPBSSDiagnosticAreaName = Constants.DIAGNOSTICAREANAME;
        public static string strWebPart = Constants.WebPart;

        private static ODMSLogging _Current;
        #endregion Static variables

        public static ODMSLogging Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new ODMSLogging();
                }

                return _Current;
            }
        }

        private ODMSLogging()
            : base(Constants.ODMSREPORTFINDERLOGGINGSERVICE, SPFarm.Local)
        {

        }

        /// <summary>
        /// This method registers the category with the Diagnostics ULS log
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>
        {
            new SPDiagnosticsArea(SMPBSSDiagnosticAreaName, new List<SPDiagnosticsCategory>
            {
                new SPDiagnosticsCategory(strWebPart, TraceSeverity.Unexpected, EventSeverity.Error, 0, (uint)CategoryId.Unknown)
                
            })
        };
            return areas;
        }

        /// <summary>
        /// Log Error in Error log file
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="errorMessage"></param>
        public static void LogError(string categoryName, string methodName, string namespaceName, string errorMessage)
        {
            StringBuilder message = new StringBuilder();
            SPDiagnosticsCategory category = ODMSLogging.Current.Areas[SMPBSSDiagnosticAreaName].Categories[strWebPart];
            // SPDiagnosticsCategory category = new SPDiagnosticsCategory(strWebPart, TraceSeverity.Unexpected, EventSeverity.Error, 0, uint.Parse(categoryName));
            message.Append("Method=");
            message.Append(methodName);
            message.Append("::NameSpace=");
            message.Append(namespaceName);
            message.Append("::Error=");
            message.Append(errorMessage);

            //Write the Error in Shaepoint ULS Log
            ODMSLogging.Current.WriteTrace(0, category, TraceSeverity.Unexpected, message.ToString());
        }

        /// <summary>
        /// Log Trace details
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="errorMessage"></param>
        public static void LogTrace(string categoryName, string methodName, string namespaceName, string traceMessage)
        {
            StringBuilder message = new StringBuilder();
            SPDiagnosticsCategory category = ODMSLogging.Current.Areas[SMPBSSDiagnosticAreaName].Categories[categoryName];
            message.Append("Method=");
            message.Append(methodName);
            message.Append("::NameSpace=");
            message.Append(namespaceName);
            message.Append("::TraceInfo=");
            message.Append(traceMessage);

            //Write the Trace in Sharepoint ULS Log
            ODMSLogging.Current.WriteTrace(0, category, TraceSeverity.Monitorable, message.ToString());
        }
    }
}
