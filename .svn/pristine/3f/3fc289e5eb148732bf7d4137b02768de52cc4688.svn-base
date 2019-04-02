using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class SysFileController : Controller
    {
        Provider.SysFileProvider provider = new Provider.SysFileProvider();
        // GET: SysFile
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var list = new List<Models.SysFileModels.SysFileModel>();
            foreach (var item in provider.GetList())
            {
                list.Add(new Models.SysFileModels.SysFileModel {
                    Id=item.Id,
                    FileName=item.FileName,
                    //FilePath = new Common.FileHelper().GetFileUrl(item.FileName, Common.FileConfig.FilePath, this.HttpContext),
                });
            }
            return View(list);
        }
        [Extension.UserAuthorize]
        public ActionResult Create(long? Id)
        {
            var model = new Models.SysFileModels.SysFileModel();
            if (Id != null)
            {
                var file = provider.GetFile(Id.Value);
                if (file != null)
                {
                    model = new Models.SysFileModels.SysFileModel
                    {
                        Id = file.Id,
                        FileName = file.FileName,
                        FilePath = new Common.FileHelper().GetFileUrl(file.FilePath, Common.FileConfig.FilePath, this.HttpContext),
                    };
                }
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.SysFileModels.SysFileModel model)
        {
            var group = new UCMS.Entitys.SysFile()
            {
                Id = model.Id,
                FileName = model.FileName,
                FilePath = new Common.FileHelper().SaveFileRelative("FilePath", "", Common.FileConfig.FilePath),
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(group);
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="checkedId"></param>
        /// <returns></returns>
        [Extension.UserAuthorize]
        public ActionResult Delete(string checkedId)
        {
            var ids = new List<long>();
            foreach (var item in checkedId.Split(','))
            {
                ids.Add(Common.ToolHelper.ConvertToLong(item));
            }
            var line = 0;
            if (ids.Count > 0)
            {
                line = provider.Delete(ids);
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}