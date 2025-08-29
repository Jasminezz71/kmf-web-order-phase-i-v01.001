using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.ET.RPT;

namespace ZEN.SaleAndTranfer.UI.Reports
{
    public class ReportBasePage : System.Web.UI.Page
    {
        protected ReportData ReportDataObj { get; set; }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);
                if (HttpContext.Current != null)
                    if (HttpContext.Current.Session["ReportData"] != null)
                    {
                        ReportDataObj = HttpContext.Current.Session["ReportData"] as ReportData;
                        return;
                    }
                ReportDataObj = new ReportData();
                CaptureRouteData(Page.RouteData);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CaptureRouteData<T>(T obj) where T : System.Web.Routing.RouteData
        {
            try
            {
                var mode = (obj.Values["rptmode"] + "").Trim();
                ReportDataObj.IsServer = mode == "server" ? true : false;
                ReportDataObj.ReportName = obj.Values["reportname"] + "";
                string dquerystr = obj.Values["parameters"] + "";
                if (!String.IsNullOrEmpty(dquerystr.Trim()))
                {
                    var param1 = dquerystr.Split('&');
                    foreach (string pm in param1)
                    {
                        var rp = new Parameter();
                        var kd = pm.Split('=');
                        if (kd[0].Substring(0, 2) == "rp")
                        {
                            rp.ParameterName = kd[0].Replace("rp", "");
                            if (kd.Length > 1) rp.Value = kd[1];
                            ReportDataObj.ReportParameters.Add(rp);
                        }
                        else if (kd[0].Substring(0, 2) == "dp")
                        {
                            rp.ParameterName = kd[0].Replace("dp", "");
                            if (kd.Length > 1) rp.Value = kd[1];
                            ReportDataObj.DataParameters.Add(rp);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}