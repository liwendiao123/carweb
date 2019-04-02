using UCMS.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UCMS.WebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var list = new List<Cache.Sys_MenuBasisCache.MenuBasisModel>();
            var menu = new Cache.Sys_MenuBasisCache().Get(Common.FormsTicket.SystemCode);

            var role = new Cache.Sys_UserRoleCache().Get(Common.FormsTicket.UserId);
            var rMenu = new Cache.Sys_RoleMenuCache();
            var ids = new List<long>();
            foreach (var item in role)
            {
                foreach (var rm in rMenu.Get(item.RoleId))
                {
                    ids.Add(rm.MenuId);
                }
            }
            foreach (var item in menu)
            {
                if (ids.FirstOrDefault(c => c == item.Id) > 0)
                {
                    list.Add(item);
                }
            }
            var u = new Provider.UserBasisProvider().GetUser(Common.FormsTicket.UserId);
            ViewBag.LastTime = u != null ? u.LastTime.ToString("yyyy年MM月dd日") : "";
            ViewBag.MenuItem = list;
            var title = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            ViewBag.Title = title.SystemName == null ? "" : title.SystemName;
            return View();
        }

        /// <summary>
        /// 输出枚举转化JS
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 30 * 1000)]
        public ActionResult Js()
        {
            string js = Common.EnumHelper.GetJs();
            return Content(js);
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            Provider.SysNoticeProvider provider = new Provider.SysNoticeProvider();

            var list = provider.GetList();
            var model = new List<Models.SysNoticeModels.SysNoticeModel>();
            string Title = "";
            string Content = "";
            foreach (var item in list)
            {
                if (item.NoticeType == 1)
                {
                    var t = provider.GetNotice(item.Id);
                    if (t != null)
                    {
                        Title = t.Title;
                        Content = t.Content;
                    }
                }
                else
                {
                    model.Add(new Models.SysNoticeModels.SysNoticeModel
                    {
                        Id = item.Id,
                        CreateTime = item.CreateTime,
                        Content = item.Content,
                        NoticeType = item.NoticeType,
                        Title = item.Title
                    });
                }
            }
            ViewBag.Title = Title;
            ViewBag.Content = Content;
            return View(model);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePwd()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="currentPassword"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePwd(string currentPassword, string Password)
        {
            int line = 0;
            var provider = new Provider.UserBasisProvider();
            var user=provider.GetUser(Common.FormsTicket.UserId);
            if (user != null)
            {
                currentPassword = Common.ToolHelper.GetMD5Hash32(currentPassword);
                if (user.Passwords == currentPassword)
                {
                    line = provider.UpdatePwd(Common.ToolHelper.GetMD5Hash32(Password), user.Id);
                    if (line > 0)
                    {
                        new Cache.AccountCodeCache().Delete(user.LoginCode);
                    }
                }
            }
            return Json(new { d = line });
        }
        public ActionResult Test1()
        {
            /*
 Mozilla/5.0 (Linux; U; Android 6.0.1; zh-CN; Redmi 4 Build/MMB29M) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.108 UCBrowser/11.8.8.968 UWS/2.13.2.17 Mobile Safari/537.36 UCBS/2.13.2.17_180814094018 NebulaSDK/1.8.100112 Nebula AlipayDefined(nt:WIFI,ws:360|0|3.0) AliApp(AP/10.1.33.30) AlipayClient/10.1.33.30 Language/zh-Hans useStatusBar/true isConcaveScreen/false
 Mozilla/5.0 (Linux; Android 6.0.1; Redmi 4 Build/MMB29M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/57.0.2987.132 MQQBrowser/6.2 TBS/044208 Mobile Safari/537.36 MicroMessenger/6.7.2.1340(0x2607023A) NetType/WIFI Language/zh_CN

             */
            string host = Request.Url.Host;
            string path = Request.Path;
            //Common.LogHelper.WriteLog(typeof(HomeController), "http://" + host + path);
            //Common.LogHelper.WriteLog(typeof(HomeController), Request.UserAgent);
           ViewBag.Rand= new Random().Next(100);
            return View();
        }
    }
}