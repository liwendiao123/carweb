using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class RoleBasisController : Controller
    {
        // GET: RoleBasis
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var cache = new Cache.Sys_RoleBasisCache().Get(Common.FormsTicket.SystemCode);
            var list = new List<Models.RoleBasisModels.RoleBasisModel>();
            foreach (var item in cache)
            {
                list.Add(new Models.RoleBasisModels.RoleBasisModel {
                    Id=item.Id,
                    IsSystem=item.IsSystem,
                    RoleSort=item.RoleSort,
                    RoleCode=item.RoleCode,
                    RoleName=item.RoleName
                });
            }
            return View(list);
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? RoleId)
        {
            var model = new Models.RoleBasisModels.RoleBasisModel();
            if (RoleId != null)
            {
                var cache = new Cache.Sys_RoleBasisCache().Get(Common.FormsTicket.SystemCode).Where(c => c.Id == RoleId).FirstOrDefault();
                model = new Models.RoleBasisModels.RoleBasisModel
                {
                    Id = cache.Id,
                    RoleSort=cache.RoleSort,
                    IsSystem=cache.IsSystem,
                    RoleCode=cache.RoleCode,
                    RoleName=cache.RoleName
                };
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName ="Create")]
        public ActionResult Save(Models.RoleBasisModels.RoleBasisModel model)
        {
            var role = new UCMS.Entitys.SysRoleBasis()
            {
                Id = model.Id,
                IsSystem =0,
                RoleName = model.RoleName == null ? "" : model.RoleName,
                RoleCode = model.RoleCode == null ? "" : model.RoleCode,
                RoleSort=model.RoleSort,
                IsDelete = 0,
                TimeStamp = DateTime.Now,
            };
            var db = new Entitys.UCMSContext();
            if (model.Id == 0)
            {
                role.Id = Common.PrimaryKey.GetHashCodeID;
                db.SysRoleBasis.Add(role);
            }
            else
            {
                db.Entry<Entitys.SysRoleBasis>(role).State = System.Data.Entity.EntityState.Modified;
            }
            var line = db.SaveChanges();
            if (line > 0)
            {
                //删除缓存
                new Cache.Sys_RoleBasisCache().Delete(Common.FormsTicket.SystemCode);
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
                var db = new Entitys.UCMSContext();
                line = db.SysRoleBasis.Where(c => ids.Contains(c.Id)).Update(r => new Entitys.SysRoleBasis { IsDelete = 1 });
                if (line > 0)
                {
                    //删除缓存
                    new Cache.Sys_RoleBasisCache().Delete(Common.FormsTicket.SystemCode);
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        #region 角色权限
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult RoleMenu(long? RoleId)
        {
            if (RoleId == null || RoleId == 0)
            {
                return Content("请重新操作！");
            }
            var list = new List<Models.RoleBasisModels.RoleMenuModel>();
            var menu = new Cache.Sys_MenuBasisCache().Get(Common.FormsTicket.SystemCode);
            var roleMenu = new Cache.Sys_RoleMenuCache().Get(RoleId.Value);

            var operate = new Cache.Sys_MenuOperateCache();
            var roleOp = new Cache.Sys_RoleOperateCache().Get(RoleId.Value);
            foreach (var item in menu.Where(c=>c.ParentId==Common.Constant.LONG_DEFAULT))
            {
                foreach (var mItem in menu.Where(c => c.ParentId == item.Id))
                {
                    var opList = new List<Models.RoleBasisModels.RoleOperateModel>();
                    foreach (var r in operate.Get(mItem.Id))
                    {
                        //操作码权限
                        var ro = roleOp.Where(c => c.OperateId == r.Id).FirstOrDefault();
                        opList.Add(new Models.RoleBasisModels.RoleOperateModel
                        {
                            OperateId = r.Id,
                            OperateName = r.OperateName,
                            MenuId=mItem.Id,
                            IsSelect = ro != null ? true : false,
                            Id = ro != null ? ro.Id : 0
                        });
                    }
                    //二级菜单
                    var m1 = roleMenu.Where(c => c.MenuId == mItem.Id).FirstOrDefault();
                    list.Add(new Models.RoleBasisModels.RoleMenuModel
                    {
                        MenuId = mItem.Id,
                        ParentId = mItem.ParentId,
                        MenuName = mItem.MenuName,
                        IsSelect = m1 != null ? true : false,
                        Id= m1 != null ? m1.Id:0,
                        OperateList = opList
                    });
                }
                //一级菜单
                var m = roleMenu.Where(c => c.MenuId == item.Id).FirstOrDefault();
                list.Add(new Models.RoleBasisModels.RoleMenuModel
                {
                    MenuId = item.Id,
                    ParentId = item.ParentId,
                    MenuName = item.MenuName,
                    IsSelect = m != null ? true : false,
                    Id = m != null ? m.Id : 0,
                });
            }
            ViewBag.RoleId = RoleId.Value;
            return View(list);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        [HttpPost]
        public ActionResult SaveRoleMenu(string checkedId,string RoleId)
        {
            var line = 0;
            if (!string.IsNullOrEmpty(checkedId)&& !string.IsNullOrEmpty(RoleId))
            {
                var temp = checkedId.Split(',');
                var roleMenu = new List<Entitys.SysRoleMenu>();
                var roleOperate = new List<Entitys.SysRoleOperate>();
                foreach (var item in temp)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var str = item.Split('_');
                        if (str[0] == "cm")
                        {
                            roleMenu.Add(new Entitys.SysRoleMenu
                            {
                                Id = Common.ToolHelper.ConvertToLong(str[2]),
                                MenuId = Common.ToolHelper.ConvertToLong(str[1]),
                                RoleId = Common.ToolHelper.ConvertToLong(RoleId),
                                TimeStamp = DateTime.Now,
                                IsDelete = 0,
                            });
                        }
                        else
                        {
                            roleOperate.Add(new Entitys.SysRoleOperate
                            {
                                Id = Common.ToolHelper.ConvertToLong(str[3]),
                                MenuId = Common.ToolHelper.ConvertToLong(str[1]),
                                OperateId= Common.ToolHelper.ConvertToLong(str[2]),
                                RoleId = Common.ToolHelper.ConvertToLong(RoleId),
                                TimeStamp = DateTime.Now,
                                IsDelete = 0,
                            });
                        }
                    }
                }
                line = new Provider.RoleBasisProvider().SetMenu(roleMenu, roleOperate);
                //删除缓存
                if (roleMenu.Count() > 0)
                {
                    new Cache.Sys_RoleMenuCache().Delete(Common.ToolHelper.ConvertToLong(RoleId));
                }
                if (roleOperate.Count() > 0)
                {
                    new Cache.Sys_RoleOperateCache().Delete(Common.ToolHelper.ConvertToLong(RoleId));
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        #endregion
    }
}