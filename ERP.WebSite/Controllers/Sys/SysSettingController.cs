using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class SysSettingController : Controller
    {
        // GET: SysSetting
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var cache = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            var model = new Models.SysSettingModels.SysSetting();
            if (cache != null && cache.Id > 0)
            {
                model = new Models.SysSettingModels.SysSetting
                {
                    SystemName = cache.SystemName,
                    Contact = cache.Contact,
                    Feeback = cache.Feeback,
                    HomeLogo = new Common.FileHelper().GetFileUrl(cache.HomeLogo, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    Id = cache.Id,
                    LoginLogo = new Common.FileHelper().GetFileUrl(cache.LoginLogo, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    FootLogo = new Common.FileHelper().GetFileUrl(cache.FootLogo, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    IsEnable=cache.IsEnable
                    //LoginLogo = new Common.FileHelper().GetWebFileUrl(cache.LoginLogo, Common.FileConfig.OtherPhotoPath),
                };
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Index")]
        public ActionResult Save(Models.SysSettingModels.SysSetting model)
        {
            var HomeLogo = new Common.FileHelper().SaveImgRelative("HomeLogo", "", Common.FileConfig.OtherPhotoPath);
            var LoginLogo = new Common.FileHelper().SaveImgRelative("LoginLogo", "", Common.FileConfig.OtherPhotoPath);
            var FootLogo = new Common.FileHelper().SaveImgRelative("FootLogo", "", Common.FileConfig.OtherPhotoPath);
            var set = new Entitys.SysSetting() {
                Id=model.Id,
                SystemName=model.SystemName,
                Contact=model.Contact,
                Feeback=model.Feeback,
                HomeLogo=HomeLogo,
                LoginLogo=LoginLogo,
                FootLogo= FootLogo,
                IsEnable=model.IsEnable,
                TimeStamp =DateTime.Now
            };
            var line = new Provider.SysSettingProvider().Edit(set);
            if (line > 0)
            {
                new Cache.SysSettingCache().Delete(Common.FormsTicket.SystemCode);
            }
            return RedirectToAction("Index");
        }
    }
}