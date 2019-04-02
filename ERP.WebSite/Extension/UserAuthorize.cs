using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Extension
{
    /// <summary>
    /// 权限控制 目前重名控制器的情况没有考虑
    /// </summary>
    public class UserAuthorize : AuthorizeAttribute
    {
        public string ActionName { get; set; }
        /// <summary>
        /// 重写OnAuthorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Stopwatch st = new Stopwatch();
            //st.Start();
            var controllerName = (filterContext.RouteData.Values["controller"].ToString()).ToLower();
            var actionName = (filterContext.RouteData.Values["action"].ToString()).ToLower();
            if (Common.FormsTicket.UserId == decimal.Zero)
            {
                var type = "/1";
                if ("Home".ToLower().Contains(controllerName))
                {
                    type = string.Empty;
                }
                filterContext.Result = new RedirectResult("~/Account/Logout" + type);
            }
            else
            {
                if (!"Home".ToLower().Contains(controllerName))
                {
                    actionName = string.IsNullOrEmpty(ActionName) ? actionName : ActionName.ToLower();
                    var IsChecked = false;
                    //TODO:返回结果需要重新赋值一个对象不然会引发System.NullReferenceException: 未将对象引用设置到对象的实例。
                    
                    var menuId = new Cache.Sys_MenuBasisCache().Get(Common.FormsTicket.SystemCode).FirstOrDefault(a => a.ControllerName.ToLower() == controllerName);
                    if (menuId!=null)
                    {
                        var id = menuId.Id;//这里不重新定义 menuId 会引发System.NullReferenceException: 未将对象引用设置到对象的实例。 因为下文有对象使用
                        var operateId = (from a in new Cache.Sys_MenuOperateCache().Get(id)
                                         where a.OperateCode.ToLower() == actionName
                                         select a.Id).FirstOrDefault();

                        var UserRole = new Cache.Sys_UserRoleCache().Get(Common.FormsTicket.UserId);

                        var roleMenu = new Cache.Sys_RoleMenuCache();
                        var roleOperate = new Cache.Sys_RoleOperateCache();
                        bool menuChecked = false;
                        foreach (var item in UserRole)
                        {
                            var menu = roleMenu.Get(item.RoleId).FirstOrDefault(c => c.MenuId == id);
                            if (menu != null && !menuChecked)
                            {
                                //存在菜单权限
                                menuChecked = true;
                            }
                            if (menuChecked)
                            {
                                if (actionName == "index")
                                {
                                    //默认权限，有菜单权限都能访问index
                                    IsChecked = true;
                                    break;
                                }
                                var operate = roleOperate.Get(item.RoleId).FirstOrDefault(c => c.OperateId == operateId);
                                if (operate != null)
                                {
                                    //存在按钮权限，跳出循环
                                    IsChecked = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!IsChecked)
                    {
                        filterContext.HttpContext.Response.Write("没有权限访问该页面");
                        filterContext.HttpContext.Response.End();
                        filterContext.HttpContext.Response.StatusCode = 401;//定义状态后就不会在往下执行了
                    }
                }
            }
            //st.Stop();
            //Common.LogHelper.WriteLog(typeof(UserAuthorize),st.ElapsedMilliseconds.ToString());
        }
    }
}