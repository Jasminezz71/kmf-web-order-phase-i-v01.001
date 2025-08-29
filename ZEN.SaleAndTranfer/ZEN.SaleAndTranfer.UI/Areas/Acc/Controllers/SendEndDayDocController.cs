using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZEN.SaleAndTranfer.VM.ACC;
using ZEN.SaleAndTranfer.BC;
using ZEN.SaleAndTranfer.UI.Controllers;
using ZEN.SaleAndTranfer.UI.Helper;
using ZEN.SaleAndTranfer.ET;
using System.Data.Entity;
using System.IO;
using System.Net;
using ZEN.SaleAndTranfer.BC.Acc;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;
using System.Drawing;
using System.Drawing.Imaging;


namespace ZEN.SaleAndTranfer.UI.Areas.Acc.Controllers
{
    public class SendEndDayDocController : BaseController
    {
        //UploadEndDayDocVM vmUpload = new UploadEndDayDocVM();
        //
        // GET: /Acc/SendEndDayDoc/
        
        /// <summary>
        /// หน้าจอ Index ที่แสดงข้อมูลการ Upload รูปต่อ 1 End Day 
        /// </summary>
        /// <param name="id">End Day Date</param>
        /// <returns>Index View</returns>
        public ActionResult Index(DateTime? id, string msd) 
        {
            var vm = new IndexEndDayDocVM();
            try
            {
                vm.SessionLogin = GetCurrentUser;

                if(!id.HasValue){
                    id = id ?? DateTime.Now;
                    vm.EndDayDate = id;
                }

                if (!string.IsNullOrWhiteSpace(msd))
                {
                    vm.MessageList.Add(MessageBC.GetMessage(msd));
                }

                vm.EndDayDate = id;
                var bc = new EndDayDocBC();
                bc.GetByEndDayDate(vm);

                return View(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View(vm);
            }          
        }        
        
        /// <summary>
        /// Upload 
        /// </summary>
        /// <param name="id">End Day Date</param>
        /// <param name="tid">End Day Doc Type ID</param>
        /// <returns>View</returns>
        public ActionResult Upload(DateTime? id, int tid)
        {
            var vm = new UploadEndDayDocVM();
            try
            {
                vm.EndDayDate = id;
                vm.EndDayDocTypeID = tid;

                var bc = new EndDayDocBC();
                bc.OnInitUpload(vm);

                return View(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Upload(UploadEndDayDocVM vm)
        {
            try
            {
                vm.SessionLogin = GetCurrentUser;

                LogWsHelper hp = new LogWsHelper();
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.END_DAY_DOC__UPLOAD_FILE__BEGIN.ToString(), this);

                // step 0: validate before save, file size ต้องมากกว่า 0, รองรับเฉพาะไฟล์ *.jpg และ *.png
                bool validatePass = this.ValidateBeforeSave(vm); //step 0: ให้โบว์ทำ 2018-11-13 14:43
                if (!validatePass)
                {
                    //step 0.1: message แจ้ง user <-- ควรจะแจ้ง user ว่า file size ต้องมากกว่า 0, รองรับเฉพาะไฟล์ *.jpg และ *.png
                    return View(vm);
                }

                // step 1: write file to folder -> UI (Controller) Layer
                bool savefileSuccuess = this.SaveFileToFolder(vm); //step 0: ให้พี่ปาล์มทำ 2018-11-13 14:43
                if (!savefileSuccuess)
                {
                    // step 1.1:  message แจ้ง user <-- ควรจะแจ้ง user ว่า ติดปัญหาการ save file โปรดลองอีกครั้ง
                    // step 2.1: ให้โบว์ทำ 2018-11-13 14:43
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00093));
                    return View(vm);
                }

                // step 2: save data to database -> BC, DC Layer                                  
                var bc = new EndDayDocBC();
                bool saveDBSuccess = bc.SaveFileDetail(vm); // step 2: ให้โบว์ทำ 2018-11-13 14:43  

                if (!saveDBSuccess)
                {
                    // step 2.1: message แจ้ง user <-- ควรจะแจ้ง user ว่า ติดปัญหาการบันทึกข้อมูลลง database โปรดลองอีกครั้ง
                    // step 2.1: ให้โบว์ทำ 2018-11-13 14:43
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00092));
                    return View(vm);
                }

                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.END_DAY_DOC__UPLOAD_FILE__SUCCESS.ToString(), this);
                // step 3: return to index -> UI (Controller)
                return RedirectToAction("Index", new { id = vm.EndDayDate.Value.ToString("yyyy-MM-dd"), msd = "M00089" });
                //return View(vm);
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return View(vm);
            }
        }
         
        public ActionResult Delete(int id)
        {
            var vm = new IndexEndDayDocVM();
            try
            {
                vm.SessionLogin = GetCurrentUser;

                LogWsHelper hp = new LogWsHelper();
                hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.END_DAY_DOC__UPLOAD_FILE__BEGIN.ToString(), this);
                
                vm.END_DAY_DOC_ID = id;

                var bc = new EndDayDocBC();                

                // step 1 : query by id(pk)
                bc.GetDataByPK(vm);

                // step 2: delete on db
                bool deleteDBSuccess = bc.DeleteFileDetail(vm);

                //if (deleteDBSuccess)
                //{
                //    hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.END_DAY_DOC__DELETE_FILE.ToString(), this);
                //}

                if (!deleteDBSuccess)
                {
                    bc.UnDeleteFileDetail(vm);
                    // step 1.1:  message แจ้ง user <-- ควรจะแจ้ง user ว่า ลบข้อมูลไม่สำเร็จ
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00091));
                    return RedirectToAction("Index", new { id = vm.RET.END_DAY_DATE.Value.ToString("yyyy-MM-dd"), msd = "M00091" });
                }

                // step 3: delete on folder
                bool deletefileSuccuess = this.DeleteFileAsFolder(vm);
                if (deletefileSuccuess)
                {
                    hp.InsertLog(vm.SessionLogin.USER_NAME, AccressTypeConst.END_DAY_DOC__DELETE_FILE__SUCCESS.ToString(), this);
                    return RedirectToAction("Index", new { id = vm.RET.END_DAY_DATE.Value.ToString("yyyy-MM-dd"), msd = "M00090" });
                }

                if (!deletefileSuccuess)
                {
                    bc.UnDeleteFileDetail(vm);  // delete on folder un
                    return RedirectToAction("Index", new { id = vm.RET.END_DAY_DATE.Value.ToString("yyyy-MM-dd"), msd = "M00091" });
                }               

                return RedirectToAction("Index", new { id = vm.RET.END_DAY_DATE.Value.ToString("yyyy-MM-dd"), msd = "M00091" });
            }
            catch (Exception ex)
            {
                LogHelperUI.Write(ex, this);
                vm.AddMessage(MessageBC.GetMessage(MessageCodeConst.M00001, new string[] { ex.Message }));
                return RedirectToAction("Index", new { id = vm.RET.END_DAY_DATE.Value.ToString("yyyy-MM-dd"), msd = ex });
            }
        }

        private bool ValidateBeforeSave(UploadEndDayDocVM vm)
        {
            try
            {
                var maximumFileSize = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.END_DAY_DOC_SIZE);
                var fileType = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.END_DAY_DOC_TYPE);

                if (vm.EndDayDate == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00097));
                }

                if (vm.EndDayDocTypeID == 0)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00098));
                }

                if (vm.SelectedFile == null)
                {
                    vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00099));
                }
                else
                {
                    if (vm.SelectedFile != null && vm.SelectedFile.ContentLength == 0)
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00099));
                    }

                    if (vm.SelectedFile.ContentLength >= Convert.ToInt32(maximumFileSize))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00095));
                    }

                    if (!(fileType.Contains(Path.GetExtension(vm.SelectedFile.FileName.ToLower()))))
                    {
                        vm.MessageList.Add(MessageBC.GetMessage(MessageCodeConst.M00096));
                    }
                }

                // before return 
                if (vm.MessageList != null && vm.MessageList.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SaveFileToFolder(UploadEndDayDocVM vm)
        {
            try
            {
                #region -- prepare variable --
                var currDate = DateTime.Now;
                //var rootPath = "~/App_Data/AccEndDayDoc/";
                var rootPath = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.FILE, ConfigNameEnum.ACC_END_DAY_DOC_ROOT_FOLDER);
                var brand = vm.SessionLogin.BRAND_CODE;
                var branch = vm.SessionLogin.BRANCH_CODE;
                var dateFolderName = vm.EndDayDate.Value.ToString("yyyy-MM-dd");
                var fileName = "";
                var fileExtension = Path.GetExtension(vm.SelectedFile.FileName);

                #region -- file name --     
                //if (vm.EndDayDocTypeID == 1)
                //{
                //    fileName = string.Format("{0}_{1}_EndDay_{2}{3}", branch, vm.EndDayDate.Value.ToString("yyMMdd"), currDate.ToString("yyMMddHHmmss"), fileExtension);
                //}

                //if (vm.EndDayDocTypeID == 2)
                //{
                //    fileName = string.Format("{0}_{1}_SCB_{2}{3}", branch, vm.EndDayDate.Value.ToString("yyMMdd"), currDate.ToString("yyMMddHHmmss"), fileExtension);
                //}

                //if (vm.EndDayDocTypeID == 3)
                //{
                //    fileName = string.Format("{0}_{1}_KTC_{2}{3}", branch, vm.EndDayDate.Value.ToString("yyMMdd"), currDate.ToString("yyMMddHHmmss"), fileExtension);
                //}
                //}


                var typeBC = new TB_M_END_DAY_DOC_TYPE_BC();
                var typeRET = typeBC.GetByPK(vm.EndDayDocTypeID);
                fileName = string.Format(typeRET.FILE_NAME_FORMAT, branch, vm.EndDayDate.Value.ToString("yyMMdd"), currDate.ToString("yyMMddHHmmss"), fileExtension); // format "{0}_{1}_XXX_{2}{3}"
                
                #endregion

                var configUsername = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__USERNAME);
                var configPassword = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__PASSWORD);
                var configDomain = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__DOMAIN);
                var theNetworkCredential = new NetworkCredential(configUsername, configPassword, configDomain);
                #endregion

                #region -- create brand folder --
                //var brandFolder = string.Format("{0}/{1}/", rootPath,branch);
                var brandFolder = Path.Combine(rootPath, brand);
                using (new ConnectToSharedFolderBC(rootPath, theNetworkCredential))
                {
                    if (!Directory.Exists(brandFolder))
                    {
                        Directory.CreateDirectory(brandFolder);
                    }
                }
                #endregion

                #region -- create branch folder --
                ///var branchFolder = string.Format("{0}/{1}/", brandFolder, branch);
                var branchFolder = Path.Combine(brandFolder, branch);
                using (new ConnectToSharedFolderBC(brandFolder, theNetworkCredential))
                {
                    if (!Directory.Exists(branchFolder))
                    {
                        Directory.CreateDirectory(branchFolder);
                    }
                }
                #endregion

                #region -- create current date folder & write file --
                //var currentDateFolder = string.Format("{0}/{1}/", branchFolder, dateFolderName);
                var currentDateFolder = Path.Combine(branchFolder, dateFolderName);
                using (new ConnectToSharedFolderBC(branchFolder, theNetworkCredential))
                {
                    if (!Directory.Exists(currentDateFolder))
                    {
                        Directory.CreateDirectory(currentDateFolder);
                    }

                    //var savePath = string.Format("{0}/{1}", dateFolderName, fileName); 
                    var savePath = Path.Combine(currentDateFolder, fileName);

                    //vm.SelectedFile.SaveAs(savePath);
                    var fileType = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.END_DAY_DOC_TYPE);

                    var compressScale = ConfigBC.GetConfigValue(CategoryConfigEnum.FILE, SubCategoryConfigEnum.UPLOAD, ConfigNameEnum.END_DAY_DOC_TYPE_COMPRESS_SCALE);

                    if (fileExtension.Contains("jpg") || fileExtension.Contains("png"))
                    {
                        #region -- compress & write --

                        // Get a bitmap. The using statement ensures objects
                        // are automatically disposed from memory after use. 
                        using (Bitmap bmp1 = new Bitmap(vm.SelectedFile.InputStream))
                        {
                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                            // Create an Encoder object based on the GUID  
                            // for the Quality parameter category.  
                            System.Drawing.Imaging.Encoder myEncoder =
                                System.Drawing.Imaging.Encoder.Quality;

                            // Create an EncoderParameters object.  
                            // An EncoderParameters object has an array of EncoderParameter  
                            // objects. In this case, there is only one  
                            // EncoderParameter object in the array.  
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);

                            //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Int64.Parse(compressScale));
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            bmp1.Save(savePath, jpgEncoder, myEncoderParameters);
                        }

                        #endregion
                    }
                    else // pdf
                    {
                        using (System.IO.FileStream output = new System.IO.FileStream(savePath, FileMode.Create))
                        {
                            vm.SelectedFile.InputStream.CopyTo(output);
                        }
                    }

                    vm.FilePath = Path.GetDirectoryName(savePath);
                    vm.FileNameDest = Path.GetFileName(savePath);
                }
                #endregion
                
                return true;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }  

        private bool DeleteFileAsFolder(IndexEndDayDocVM vm)
        {
            try
            {
                var beforeRename = Path.Combine(vm.RET.FILE_PATH, vm.RET.FILE_NAME_DEST);
                var fileExtension = Path.GetExtension(vm.RET.FILE_NAME_DEST);
                var fileNameDest = Path.GetFileNameWithoutExtension(vm.RET.FILE_NAME_DEST);

                var fileName = string.Format("{0}_Delete{1}", fileNameDest, fileExtension);
                var renameFile = Path.Combine(vm.RET.FILE_PATH, fileName);

                // Delete a file by using File class static method...
                //if (System.IO.File.Exists(@"C:\Users\Public\DeleteTest\test.txt"))
                if (System.IO.File.Exists(beforeRename))
                {
                    // Use a try block to catch IOExceptions, to
                    // handle the case of the file already being
                    // opened by another process.
                    try
                    {
                        var configUsername = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__USERNAME);
                        var configPassword = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__PASSWORD);
                        var configDomain = ConfigBC.GetConfigValue(CategoryConfigEnum.PATH, SubCategoryConfigEnum.AUTHEN, ConfigNameEnum.UPLOAD_TEMP__ACC_END_DAY_DOC__DOMAIN);
                        var theNetworkCredential = new NetworkCredential(configUsername, configPassword, configDomain);

                        using (new ConnectToSharedFolderBC(vm.RET.FILE_PATH, theNetworkCredential))
                        {
                            //System.IO.File.Delete(fileForDelete);
                            //System.IO.File.Move(beforeRename, renameFile);
                            System.IO.File.Copy(beforeRename, renameFile);
                            System.IO.File.Delete(beforeRename);
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        //Console.WriteLine(e.Message);                        
                        //return false;
                        throw e;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}