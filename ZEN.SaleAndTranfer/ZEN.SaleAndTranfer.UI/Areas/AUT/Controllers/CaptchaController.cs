using SRVTextToImage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZEN.SaleAndTranfer.UI.Areas.AUT.Controllers
{
    public class CaptchaController : Controller
    {
        //
        // GET: /AUT/Captcha/
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] // This is for output cache false
        public FileResult GetCaptchaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();
            string captcha;
            do { captcha = CI.GetRandomString(5).ToUpper(); } while (captcha.Contains('O') || captcha.Contains('0'));
            this.Session["CaptchaImageText"] = captcha; // here 5 means I want to get 5 char long captcha
            //CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75);
            // Or We can use another one for get custom color Captcha Image 
            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75, Color.DarkGray, Color.Khaki);
            MemoryStream stream = new MemoryStream();
            CI.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }
    }
}