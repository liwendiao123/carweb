using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCMS.Common;

namespace UCMS.WebSite.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult GetAreaItem(long? AreaId)
        {
            if (AreaId == null)
            {
                return Content("");
            }
            var AreaItem = UserControl.SelectItem.AreaItem(AreaId.Value, false, true);
            return Json(AreaItem);
        }

        public ActionResult GetUploadToken()
        {
          string token =  QiniuHelper.GetUploadToken();
          
            return Json(new
            {
                FileConfig.AccessKey,
                FileConfig.SecretKey,
                FileConfig.Bucket,
                Token = token


            },JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCarImgPath(string ext)
        {
           var  fileId = PrimaryKey.GetHashCodeID.ToString();
            var filePath = Common.FileConfig.CarPhotoPath;
            var frst = filePath.Substring(0, 1);
            var fp = "";
            if (frst == "/")
            {
                fp = filePath.Substring(1, filePath.Length - 1);
            }
            var fname = string.Format("{0}/{1}/{2}", fp, fileId.Substring(0, 4), fileId.Substring(4) + ext);


            //var folderName = fileId.Substring(0, 4);
            //var fileName = fileId.Substring(4);
            //var keyfilePath = string.Format("{0}/{1}/", Common.FileConfig.CarPhotoPath, folderName);
            return Json(new
            {
               key = fname,
               name = fileId + ext

            }, JsonRequestBehavior.AllowGet);
        }
    }
}