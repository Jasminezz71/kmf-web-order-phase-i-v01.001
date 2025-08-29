using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.UI.LogWs;

namespace ZEN.SaleAndTranfer.UI.Helper
{
    public class LogWsHelper
    {
        public void WriteLog(LogWs.LogMAET logMAET)
        {
            try
            {
                var ws = new LogWs.LogServiceClient();
                ws.InsertLog(logMAET);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertLog(string userName, string accessType, Controller controller)
        {
            try
            {
                string browserValue = string.Empty; 
                System.Web.HttpBrowserCapabilitiesBase browser = controller.Request.Browser;
                browserValue = "Browser Capabilities\n"
                    + "Type = " + browser.Type + "\n"
                    + "Name = " + browser.Browser + "\n"
                    + "Version = " + browser.Version + "\n"
                    + "Major Version = " + browser.MajorVersion + "\n";

                string ipAddress = this.GetFullIPAddress(controller);
                //string compName = this.GetComputerName(controller);
                //string compName = this.GetComputerName(ipAddress);

                //Waiting for code
                string compName = "dummyComputerName";

                string actionName = controller.ControllerContext.RouteData.Values["action"].ToString();
                LogMAET logMAET = new LogMAET();

                logMAET.logDate = DateTime.Now;
                logMAET.appID = AppIDConst.SALE_AND_TRANSFER;
                logMAET.userName = userName;
                logMAET.accessType = accessType;
                logMAET.actionControl = actionName;
                logMAET.pageName = controller.Request.Url.OriginalString;
                logMAET.clientBrowser = browserValue;
                logMAET.ipAddress = ipAddress;
                logMAET.computerName = compName;

                this.WriteLog(logMAET);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private string GetComputerName(string IP)
        //{
        //    try
        //    {
        //        IPAddress myIP = IPAddress.Parse(IP);
        //        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        //        return GetIPHost.HostName.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private string GetComputerName(Controller controller)
        {
            try
            {
                System.Net.IPHostEntry GetIPHost = new System.Net.IPHostEntry();
                GetIPHost = System.Net.Dns.GetHostEntry(controller.Request.ServerVariables["REMOTE_HOST"]);
                return GetIPHost.HostName.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetFullIPAddress(Controller controller)
        {
            try
            {
                return controller.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}