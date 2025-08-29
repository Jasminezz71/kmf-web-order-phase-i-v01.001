using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.BC.LOG;
using ZEN.SaleAndTranfer.VM.AUT;

namespace ZEN.SaleAndTranfer.UI.Helper
{
    public class LogHelperUI
    {
        public static void Write(Exception errorEx, Controller controller)
        {
            try
            {
                UserInfoVM userLogin = (UserInfoVM)controller.Session[SessionConst.CURRENT_USER_LOGIN];
                if (userLogin == null)
                    userLogin = new UserInfoVM();


                string browserValue = string.Empty;
                System.Web.HttpBrowserCapabilitiesBase browser = controller.Request.Browser;
                browserValue = "Browser Capabilities\n"
                    + "Type = " + browser.Type + "\n"
                    + "Name = " + browser.Browser + "\n"
                    + "Version = " + browser.Version + "\n"
                    + "Major Version = " + browser.MajorVersion + "\n"
                    + "Minor Version = " + browser.MinorVersion + "\n"
                    + "Platform = " + browser.Platform + "\n"
                    + "Is Beta = " + browser.Beta + "\n"
                    + "Is Crawler = " + browser.Crawler + "\n"
                    + "Is AOL = " + browser.AOL + "\n"
                    + "Is Win16 = " + browser.Win16 + "\n"
                    + "Is Win32 = " + browser.Win32 + "\n"
                    + "Supports Frames = " + browser.Frames + "\n"
                    + "Supports Tables = " + browser.Tables + "\n"
                    + "Supports Cookies = " + browser.Cookies + "\n"
                    + "Supports VBScript = " + browser.VBScript + "\n"
                    + "Supports JavaScript = " +
                        browser.EcmaScriptVersion.ToString() + "\n"
                    + "Supports Java Applets = " + browser.JavaApplets + "\n"
                    + "Supports ActiveX Controls = " + browser.ActiveXControls
                          + "\n"
                    + "Supports JavaScript Version = " +
                        browser["JavaScriptVersion"] + "\n";
                LogBC.WriteLogUI(errorEx,
                                controller.Session.SessionID,
                                userLogin.USER_NAME,
                                controller.Request.Url.ToString(),
                                errorEx.Source,
                                browserValue,
                                "dummyComputerName",// wait find in google computer name from controller 
                                controller.Request.ServerVariables["REMOTE_ADDR"]
                                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}