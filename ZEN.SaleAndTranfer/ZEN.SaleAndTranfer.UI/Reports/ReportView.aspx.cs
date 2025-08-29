using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.ET.RPT;

namespace ZEN.SaleAndTranfer.UI.Reports
{
    public partial class ReportView : ReportBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Render Report 
                    RenderReport(this.ReportDataObj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RenderReport(ReportData reportData)
        {
            try
            {
                // Set the processing mode for the ReportViewer to Remote
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();

                ServerReport serverReport = ReportViewer1.ServerReport;

                #region ## Set the report server URL and report path ##
                var MyReportServerUrl = ConfigBC.GetConfigValue(CategoryConfigEnum.REPORT, SubCategoryConfigEnum.ST_REPORT_SERVER, ConfigNameEnum.URL);

                var MyReportPath = string.Empty;

                var reportName = (ConfigNameEnum)Enum.Parse(typeof(ConfigNameEnum), reportData.ReportName);
                MyReportPath = ConfigBC.GetConfigValue(CategoryConfigEnum.REPORT, SubCategoryConfigEnum.ST_SERVER_PATH, reportName);

                serverReport.ReportServerUrl = new Uri(MyReportServerUrl);
                serverReport.ReportPath = MyReportPath;
                #endregion

                #region ## Set Dynamic Parameter ##
                var rpPms = ReportViewer1.ServerReport.GetParameters();
                string[] split;
                if (reportData.ReportName == "RPT_SALES_INVOICE" || reportData.ReportName == "RPT_CREDIT_NOTE")
                {
                    string rptPrms = reportData.ReportParameters[0].Value.ToString();
                    split = rptPrms.Split('|');
                    for (int i = 0; i < rpPms.Count(); i++)
                    {
                        string value = null;
                        if (split[i] == "") value = null;
                        else value = split[i];
                        ReportParameter rps = new ReportParameter(rpPms[i].Name, value);
                        ReportViewer1.ServerReport.SetParameters(rps);
                    }
                }
                else if (reportData.ReportName.Contains("RPT_STOCKCARD"))
                {
                    string rptPrms = reportData.ReportParameters[0].Value.ToString();
                    split = rptPrms.Split('|');
                    for (int i = 0; i < rpPms.Count(); i++)
                    {
                        if (rpPms[i].Name == "P_PAGE_INDEX" || rpPms[i].Name == "P_PAGE_SIZE")
                            continue;

                        string value = null;
                        if (split[i] == "") value = null;
                        else if (split[i] == "undefined") value = null;
                        else value = split[i];
                        ReportParameter rps = new ReportParameter(rpPms[i].Name, value);
                        ReportViewer1.ServerReport.SetParameters(rps);
                    }
                }
                else
                {
                    foreach (var rpm in rpPms)
                    {
                        foreach (var param in reportData.ReportParameters)
                        {
                            ReportParameter rps = new ReportParameter(rpm.Name, param.Value);
                            ReportViewer1.ServerReport.SetParameters(rps);
                        }
                    }
                }
                #endregion

                #region ## Set Display Name ##
                var rpPms2 = ReportViewer1.ServerReport.GetParameters();
                if (rpPms != null)
                {
                    ReportViewer1.ServerReport.DisplayName = rpPms2[0].Values[0];
                }
                #endregion

                // Refresh the report            
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [Serializable]
    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }
        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.


                var MyReportViewerUser = ConfigBC.GetConfigValue(CategoryConfigEnum.REPORT, SubCategoryConfigEnum.ST_REPORT_SERVER, ConfigNameEnum.USER);
                var MyReportViewerPassword = ConfigBC.GetConfigValue(CategoryConfigEnum.REPORT, SubCategoryConfigEnum.ST_REPORT_SERVER, ConfigNameEnum.PASSWORD);
                var MyReportViewerDomain = ConfigBC.GetConfigValue(CategoryConfigEnum.REPORT, SubCategoryConfigEnum.ST_REPORT_SERVER, ConfigNameEnum.DOMAIN_NAME);

                // User name
                string userName = MyReportViewerUser;

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password = MyReportViewerPassword;

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain = MyReportViewerDomain;

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing domain from web.config file");

                //return new NetworkCredential(userName, password, domain);
                var nc = new NetworkCredential(userName, password, domain);
                return nc;
            }
        }
        public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }
    }
}