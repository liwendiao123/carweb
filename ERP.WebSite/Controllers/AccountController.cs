﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            if (Common.ToolHelper.IsMobileDevice())
            {
                return RedirectToAction("Index", "Mobile");
            }
            if (Common.FormsTicket.UserId > 0)
            {
                return RedirectToAction("Index","Default");
            }
            //System.Web.HttpContext.Current.Session.Clear();
            //System.Web.Security.FormsAuthentication.SignOut();

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

            ViewBag.Head = Title;
            ViewBag.Content = Content;
            var title = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            ViewBag.Title = title.SystemName;
            ViewBag.LoginLogo = title.LoginLogo == null ? "" : new Common.FileHelper().GetFileUrl(title.LoginLogo, Common.FileConfig.OtherPhotoPath, this.HttpContext);
            ViewBag.Contact = title.Contact;
            ViewBag.Feeback = title.Feeback;
            return View(model);
        }
        /// <summary>
        /// 登录
        /// 1000: 登录成功
        /// 1001: 登录失败
        /// 1002: 验证码过期
        /// 1003：验证码错误
        /// </summary>
        /// <param name="loginCode"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string loginCode, string password, string code)
        {
            var cache = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            if (cache != null && cache.Id > 0)
            {
                if (cache.IsEnable == 1)
                {
                    if (!loginCode.Contains("xw"))
                    {
                        return Content("999");
                    }
                    else
                    {
                        loginCode = loginCode.Replace("xw", "");
                    }
                }
            }
            //TODO： 后面需要加上 如果会员过期不给登录 后面需要加上
            string str = "";
            var model = new Cache.AccountCodeCache().Get(Common.ToolHelper.ConvertToInt(loginCode));
            if (model != null)
            {
                password = Common.ToolHelper.GetMD5Hash32(password);
                if (model.Passwords == password)
                {
                    //添加访问记录
                    
                    var entity = new Entitys.SysLoginLog
                    {
                        TimeStamp = DateTime.Now,
                        LoginCode=loginCode.ToString(),
                        LoginStatus=1,
                        LoginIP = Common.ToolHelper.GetClientIP,
                        LoginType = 0,
                    };
                    var db = new UCMS.Entitys.UCMSContext();
                    db.SysLoginLog.Add(entity);
                    db.SaveChanges();
                    //添加票据
                    var ticket = new Common.FormsTicket();
                    ticket.AuthenticationTicket(model.UserId, model.RealName, model.UserType,model.LoginCode);
                    str = "1000";
                }
                else
                {
                    str = "1001";
                }
            }
            else
            {
                str = "1001";
            }
            return Content(str);
        }
        public ActionResult Logout(string ID)
        {
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            ViewBag.ActionTpye = ID;
            return View();
        }
    }
}