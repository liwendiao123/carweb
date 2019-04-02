using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class MenuBasisController : Controller
    {
        Provider.MenuBasisProvider provider = new Provider.MenuBasisProvider();
        // GET: MenuBasis
        #region 菜单管理

        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [Extension.UserAuthorize(ActionName ="Index")]
        public ActionResult GetList(Common.PagingModels paging, string search = "")
        {
            var list = provider.GetMenuList(search, paging);
            var model = new List<Models.MenuBasisModels.MenuBasisModel>();
            foreach (var item in list)
            {
                model.Add(new Models.MenuBasisModels.MenuBasisModel
                {
                    Id = item.Id.ToString(),
                    MenuName = item.MenuName,
                    ActionName = item.ActionName,
                    ControllerName = item.ControllerName,
                    MenuIcon = item.MenuIcon,
                    MenuSort = item.MenuSort,
                    ParentId = item.ParentId.ToString()
                });
            }
            var json = new
            {
                Data = model,
                RowCount=paging.totalrows
            };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(json));
        }
        [Extension.UserAuthorize]
        public ActionResult Create(long? MenuId)
        {
            var model = new Models.MenuBasisModels.MenuBasisModel();
            if (MenuId != null)
            {
                var cache = new Cache.Sys_MenuBasisCache().Get(Common.FormsTicket.SystemCode).Where(c => c.Id == MenuId).FirstOrDefault();
                model = new Models.MenuBasisModels.MenuBasisModel
                {
                    Id = cache.Id.ToString(),
                    MenuName = cache.MenuName,
                    ActionName = cache.ActionName,
                    ControllerName = cache.ControllerName,
                    MenuIcon = cache.MenuIcon,
                    MenuSort = cache.MenuSort,
                    ParentId = cache.ParentId.ToString()
                };
            }
            ViewBag.MenuItem = UserControl.SelectItem.MenuItem(MenuId == null ? Common.Constant.LONG_NULL : MenuId.Value, true);
            return View(model);
        }
        [Extension.UserAuthorize(ActionName ="Create")]
        public ActionResult Save(Models.MenuBasisModels.MenuBasisModel model)
        {
            var menu = new Entitys.SysMenuBasis()
            {
                Id = Common.ToolHelper.ConvertToLong(model.Id),
                MenuName = model.MenuName == null ? "" : model.MenuName,
                ActionName = model.ActionName == null ? "" : model.ActionName,
                ControllerName = model.ControllerName == null ? "" : model.ControllerName,
                IsDelete = 0,
                MenuIcon = model.MenuIcon == null ? "" : model.MenuIcon,
                MenuSort = model.MenuSort,
                TimeStamp = DateTime.Now,
                ParentId = Common.ToolHelper.ConvertToLong(model.ParentId)
            };
            var line = provider.Edit(menu);
            if (line > 0)
            {
                //删除缓存
                new Cache.Sys_MenuBasisCache().Delete(Common.FormsTicket.SystemCode);
            }
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
                if (line > 0)
                {
                    //删除缓存
                    new Cache.Sys_MenuBasisCache().Delete(Common.FormsTicket.SystemCode);
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        #endregion

        #region 操作码
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult OperateList(long? MenuId)
        {
            if (MenuId == null)
            {
                return Content("请重新选择操作码！");
            }
            var cache = new Cache.Sys_MenuOperateCache().Get(MenuId.Value);
            var list = new List<Models.MenuBasisModels.MenuOperateModel>();
            foreach (var item in cache)
            {
                list.Add(new Models.MenuBasisModels.MenuOperateModel {
                    Id=item.Id.ToString(),
                    MenuId=item.MenuId.ToString(),
                    OperateCode=item.OperateCode,
                    OperateSort=item.OperateSort,
                    OperateName=item.OperateName
                });
            }
            ViewBag.MenuId = MenuId.Value;
            return View(list);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult OperateCreate(long? MenuId, long? OperateId)
        {
            var model = new Models.MenuBasisModels.MenuOperateModel();
            if (MenuId == null)
            {
                return Content("请重新选择操作码！");
            }
            if (OperateId != null)
            {
                var cache = new Cache.Sys_MenuOperateCache().Get(MenuId.Value).Where(c => c.Id == OperateId).FirstOrDefault();
                model = new Models.MenuBasisModels.MenuOperateModel
                {
                    Id = cache.Id.ToString(),
                    MenuId=cache.MenuId.ToString(),
                    OperateSort = cache.OperateSort,
                    OperateCode = cache.OperateCode,
                    OperateName = cache.OperateName
                };
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult OperateSave(Models.MenuBasisModels.MenuOperateModel model)
        {
            var operate = new Entitys.SysMenuOperate()
            {
                Id = Common.ToolHelper.ConvertToLong(model.Id),
                OperateName = model.OperateName == null ? "" : model.OperateName,
                OperateCode = model.OperateCode == null ? "" : model.OperateCode,
                OperateSort = model.OperateSort,
                MenuId = Common.ToolHelper.ConvertToLong(model.MenuId),
                IsDelete = 0,
                TimeStamp = DateTime.Now,
            };
            var line = new Provider.MenuOperateProvider().Edit(operate);
            if (line > 0)
            {
                //删除缓存
                new Cache.Sys_MenuOperateCache().Delete(Common.ToolHelper.ConvertToLong(model.MenuId));
            }
            return Json(new { d = 1 > 0 ? 1 : 0 });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="checkedId"></param>
        /// <returns></returns>
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult OperateDelete(string checkedId,string MenuId)
        {
            var ids = new List<long>();
            foreach (var item in checkedId.Split(','))
            {
                ids.Add(Common.ToolHelper.ConvertToLong(item));
            }
            var line = 0;
            if (ids.Count > 0)
            {
                line = new Provider.MenuOperateProvider().Delete(ids);
                if (line > 0)
                {
                    //删除缓存
                    new Cache.Sys_MenuOperateCache().Delete(Common.ToolHelper.ConvertToLong(MenuId));
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        #endregion
    }
}