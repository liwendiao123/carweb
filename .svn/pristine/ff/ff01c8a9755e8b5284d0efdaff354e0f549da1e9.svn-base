using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace UCMS.Common
{
    public class FormsTicket
    {
        /// <summary>
        /// 登录帐号用户ID
        /// </summary>
        public static long UserId
        {
            get
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                {
                    return Convert.ToInt64(HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 系统编码 暂时未分多用户
        /// </summary>
        public static int SystemCode
        {
            get
            {
                var result = 2018;
                //HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                //if (cookie != null)
                //{
                //    if (!String.IsNullOrEmpty(cookie.Value))
                //    {
                //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                //        if (authTicket != null && !String.IsNullOrEmpty(authTicket.UserData))
                //        {
                //            result = Convert.ToInt32(authTicket.UserData.Split(new char[] { '|' })[0]);
                //        }
                //    }
                //}
                return result;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName
        {
            get
            {
                var result = String.Empty;
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    if (!String.IsNullOrEmpty(cookie.Value))
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                        if (authTicket != null && !String.IsNullOrEmpty(authTicket.UserData))
                        {
                            result = authTicket.UserData.Split(new char[] { '|' })[0];
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public static int UserType
        {
            get
            {
                var result = 0;
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    if (!String.IsNullOrEmpty(cookie.Value))
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                        if (authTicket != null && !String.IsNullOrEmpty(authTicket.UserData))
                        {
                            result = Convert.ToInt32(authTicket.UserData.Split(new char[] { '|' })[1]);
                        }
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 登录账号
        /// </summary>
        public static string CarNo
        {
            get
            {
                string result = "";
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    if (!String.IsNullOrEmpty(cookie.Value))
                    {
                        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                        if (authTicket != null && !String.IsNullOrEmpty(authTicket.UserData))
                        {
                            result = authTicket.UserData.Split(new char[] { '|' })[2];
                        }
                    }
                }
                return result;
            }
        }
        /// <summary>
        /// 登录票据处理方法
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="LoginCode">登录帐号</param>
        /// <param name="UserName">用户姓名</param>
        public void AuthenticationTicket(long UserId, string UserName, int UserType,int CarNo)
        {
            string UserData = UserName + "|" + UserType + "|" + CarNo;
            var ticket = new FormsAuthenticationTicket(1, UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(120), true, UserData);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
            cookie.Value = FormsAuthentication.Encrypt(ticket);
            if (!String.IsNullOrEmpty(FormsAuthentication.CookieDomain))
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            FormsIdentity identity = new FormsIdentity(ticket);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            System.Web.HttpContext.Current.User = principal;
        }
    }
}
