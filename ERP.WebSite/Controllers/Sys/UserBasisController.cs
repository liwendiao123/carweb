using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class UserBasisController : Controller
    {
        Provider.UserBasisProvider provider = new Provider.UserBasisProvider();
        // GET: UserBasis
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [Extension.UserAuthorize(ActionName = "Index")]
        public ActionResult GetList(Common.PagingModels paging, string search = "")
        {
            var list = provider.GetList(search, paging);
            var model = new List<Models.UserBasisModels.UserBasisModel>();
            foreach (var item in list)
            {
                model.Add(new Models.UserBasisModels.UserBasisModel
                {
                    Id = item.Id.ToString(),
                    Sex=item.Sex,
                    LoginCode=item.LoginCode,
                    Mobile=item.Mobile,
                    Passwords=item.Passwords,   
                    RealName=item.RealName,
                    LastTime=item.LastTime,
                    QQ=item.QQ.ToString(),
                    Weixin=item.Weixin,
                    CreateTime=item.CreateTime
                });
            }
            var json = new
            {
                Data = model,
                RowCount = paging.totalrows
            };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(json));
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? UserId)
        {
            var model = new Models.UserBasisModels.UserBasisModel();
            if (UserId != null)
            {
                var user = provider.GetUser(UserId.Value);
                if (user == null)
                {
                    return Content("请重新操作！");
                }
                model = new Models.UserBasisModels.UserBasisModel
                {
                    Id = user.Id.ToString(),
                    LoginCode = user.LoginCode,
                    Mobile = user.Mobile,
                    Passwords = user.Passwords,
                    LastTime=user.LastTime,
                    QQ=user.QQ.ToString(),
                    Weixin=user.Weixin,
                    RealName = user.RealName,
                    Sex = user.Sex,
                    Remark =user.Remark,
                    Photo = new Common.FileHelper().GetFileUrl(user.Photo, Common.FileConfig.UserPhotoPath, this.HttpContext),
                };
            }
            else
            {
                model.LastTime = DateTime.Now.AddMonths(6);
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.UserBasisModels.UserBasisModel model)
        {
            var Photo = new Common.FileHelper().SaveImgRelative("Photo", "", Common.FileConfig.UserPhotoPath);
            var user = new UCMS.Entitys.SysUserBasis()
            {
                Id = Common.ToolHelper.ConvertToLong(model.Id),
                Sex = (byte)model.Sex,
                LoginCode = model.LoginCode,
                Mobile = model.Mobile == null ? "" : model.Mobile,
                Passwords = model.Passwords == null ? "" :Common.ToolHelper.GetMD5Hash32(model.Passwords),
                RealName = model.RealName,
                LastTime=model.LastTime,
                QQ=Common.ToolHelper.ConvertToInt(model.QQ),
                Weixin=model.Weixin,
                Remark=model.Remark,
                Photo=Photo,
                CreateTime=DateTime.Now,
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(user);
            if (line > 0)
            {
                //删除缓存
                var cache = new Cache.AccountCodeCache().Delete(model.LoginCode);
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
                    foreach (var item in ids)
                    {
                        var us=provider.GetUser(item);
                        if (us != null)
                        {
                            //删除缓存
                            var cache = new Cache.AccountCodeCache().Delete(us.LoginCode);
                        }
                    }
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult UserRole(long? UserId)
        {
            if (UserId == null||UserId ==0)
            {
                return Content("请重新操作");
            }
            var list = new List<Models.UserBasisModels.UserRoleModel>();
            var role = new Cache.Sys_RoleBasisCache().Get(Common.FormsTicket.SystemCode);
            var user = new Cache.Sys_UserRoleCache().Get(UserId.Value);
            foreach (var item in role)
            {
                var u = user.FirstOrDefault(c => c.RoleId == item.Id);
                list.Add(new Models.UserBasisModels.UserRoleModel
                {
                    RoleId = item.Id,
                    RoleName = item.RoleName,
                    IsSelect = u != null,
                    Id = u != null ? u.RoleUserId : 0
                });
            }
            ViewBag.UserId = UserId.Value;
            return View(list);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult SaveUserRole(string checkedId, string UserId)
        {
            var line = 0;
            if (!string.IsNullOrEmpty(checkedId) && !string.IsNullOrEmpty(UserId))
            {
                var temp = checkedId.Split(',');
                var roleMenu = new List<Entitys.SysRoleUser>();
                foreach (var item in temp)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var str = item.Split('_');
                        roleMenu.Add(new Entitys.SysRoleUser
                        {
                            Id = Common.ToolHelper.ConvertToLong(str[2]),
                            UserId=Common.ToolHelper.ConvertToLong(UserId),
                            RoleId = Common.ToolHelper.ConvertToLong(str[1]),
                            TimeStamp = DateTime.Now,
                            IsDelete = 0,
                        });
                    }
                }
                line = new Provider.UserBasisProvider().SetRole(roleMenu);
                //删除缓存
                if (roleMenu.Count() > 0)
                {
                    new Cache.Sys_UserRoleCache().Delete(Common.ToolHelper.ConvertToLong(UserId));
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}