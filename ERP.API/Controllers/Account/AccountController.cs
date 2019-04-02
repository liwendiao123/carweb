using ERP.Provider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Account
{
    public class AccountController : BaseController
    {
        MemberBasisProvider proveder =new MemberBasisProvider();
        // GET: Account
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logincode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult Login(string logincode, string password)
        {
            var data = new Models.AccountModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(logincode) || string.IsNullOrEmpty(password))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("logincode", logincode);
                    dic.Add("password", password);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                #region 登录黑名单

                var cache = HttpRuntime.Cache.Get("login" + logincode);
                bool b = true;
                if (cache == null)
                {
                    //不在黑名单需要从数据库验证
                    var min = Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["member_log_date"]);
                    var count = proveder.LoginLog(logincode, DateTime.Now.AddMinutes(-min));
                    var errorLog = Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["member_log_count"]);
                    if (count >= errorLog)
                    {
                        HttpRuntime.Cache.Insert("login" + logincode, "" + DateTime.Now + "", null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                        b = false;
                        data.response = (int)Extensions.ErrorInfo.PwdLock;
                        data.message = "密码错误次数太多，请60分钟后在试";
                    }
                }
                else
                {
                    if (Common.ToolHelper.ConvertToDateTime(cache).AddMinutes(Common.ToolHelper.ConvertToInt(60) + 1) > DateTime.Now)
                    {
                        b = false;
                        data.response = (int)Extensions.ErrorInfo.PwdLock;
                        data.message = "密码错误次数太多，请60分钟后在试";
                    }
                    else
                    {
                        HttpRuntime.Cache.Remove("login" + logincode);
                    }
                }
                #endregion
                if (b)
                {
                    var member = proveder.GetMember(logincode);
                    if (member != null)
                    {
                        if (member.Passwords == password.Trim())
                        {
                            if (member.AbnormalLock == 0)
                            {
                                string token =Common.ToolHelper.GetMD5Hash32(Common.ToolHelper.ConvertDateTimeInt(DateTime.Now).ToString());
                                var pic =new ERP.Common.FileHelper().GetWebFileUrl(member.Picture, Common.FileConfig.FileType.MemberPhoto.ToString());
                                var model = new Models.AccountModels.result_model()
                                {
                                    integral = member.Integral,
                                    sex = member.Sex,
                                    email = member.Email,
                                    memberid = member.Id,
                                    mobile = member.Mobile,
                                    picture = pic,
                                    promocode = member.PromoCode,
                                    realname = member.RealName,
                                    memberlevel = member.MemberLevel,
                                    token= token
                                };
                                data.response = (int)Extensions.ErrorInfo.OK;
                                data.result = model;
                                data.message = "登录成功";
                                #region 添加缓存
                                var memCache = new Cache.MemberTokenCache.MemberTokenModel()
                                {
                                    Id = member.Id,
                                    Sex = member.Sex,
                                    Email = member.Email,
                                    IMEI = member.IMEI,
                                    Integral = member.Integral,
                                    IP = Common.ToolHelper.GetClientIP,
                                    LoginCode = member.LoginCode,
                                    Mobile = member.Mobile,
                                    OpenId = member.OpenId,
                                    Picture = member.Picture,
                                    PromoCode = member.PromoCode,
                                    RealName = member.RealName,
                                    Password=member.Passwords
                                };
                                new Cache.MemberTokenCache().Set(memCache, token);
                                #endregion
                            }
                            else
                            {
                                data.response = (int)Extensions.ErrorInfo.PassError;
                                data.message = "账号已被锁请联系管理员解锁！";
                            }
                        }
                        else
                        {
                            data.response = (int)Extensions.ErrorInfo.PassError;
                            data.message = "密码错误！";
                        }
                    }
                    else
                    {
                        data.response = (int)Extensions.ErrorInfo.PassError;
                        data.message = "账号不存在！";
                    }
                }
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            #region 添加登录日志
            if (data.response == (int)Extensions.ErrorInfo.PassError || data.response == (int)Extensions.ErrorInfo.OK)
            {
                var log = new Entitys.SysLoginLog()
                {
                    LoginCode = logincode,
                    TimeStamp = DateTime.Now,
                    LoginType = 1,
                    LoginStatus = data.response == (int)Extensions.ErrorInfo.OK ? (byte)1 : (byte)0,
                    LoginIP = Common.ToolHelper.GetClientIP,
                };
                proveder.SaveLoginLog(log);
            }
            #endregion
            
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Models.AccountModels.result_model model)
        {
            var data = new Models.AccountModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(model.realname) || string.IsNullOrEmpty(model.mobile) || string.IsNullOrEmpty(model.password) || string.IsNullOrEmpty(model.openid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("realname", model.realname);
                    dic.Add("password", model.password);
                    dic.Add("openid", model.openid);
                    dic.Add("mobile", model.mobile);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                //1 验证上级推广码
                var mem = proveder.GetMemberByPromoCode(model.promocode);
                var parentCode = "";
                if (mem == null)
                {
                    parentCode = "-100-";
                }
                else
                {
                    parentCode = mem.ParentCode + model.promocode + "-";
                }
                //2 验证账号  账号由系统统一生成
                string LoginCode = "";
                while (true)
                {
                    LoginCode = Common.PrimaryKey.GetLoginCode.ToString();
                    var acc = proveder.GetMemberByLoginCode(LoginCode);
                    if (acc == 0)
                    {
                        break;
                    }
                }
                var entity = new Entitys.Sys_MemberBasis()
                {
                    LoginCode = LoginCode,
                    PasswordPay = "",
                    Picture = "",
                    Email = "",
                    MemberStatus = 1,
                    IDcard = "",
                    Certification = 0,
                    AbnormalLock = 0,
                    Integral = 0,
                    MemberLevel = 1,

                    IMEI = model.imei == null ? "" : model.imei,
                    Mobile = model.mobile,
                    OpenId = model.openid,
                    Passwords = model.password,
                    RealName = model.realname,
                    ParentCode = parentCode,

                    Sex = (int)Common.EnumModel.ESex.Privary,
                    IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                    CreateTime = DateTime.Now,
                    TimeStamp = DateTime.Now
                };
                var m = proveder.Save(entity);
                if (m > 0)
                {
                    data.response = (int)Extensions.ErrorInfo.OK;
                    data.message = "注册成功";
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "注册失败";
                }
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传个人图像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostAvatar(string token,string memberid)
        {
            var data = new Models.AccountModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("token", token);
                    dic.Add("memberid", memberid);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                var cache = new Cache.MemberTokenCache().Get(token);
                if (cache != null&&cache.Id.ToString()==memberid)
                {
                    var Photo = new ERP.Common.FileHelper().SaveFileAbsolute("memimg", cache.Picture, ERP.Common.FileConfig.FileLocalPath + "/" + ERP.Common.FileConfig.FileType.MemberPhoto.ToString());
                    if (!string.IsNullOrEmpty(Photo))
                    {
                        //添加头像  这里直接返回图像地址就不需要重新缓存了
                        var re = proveder.SavePicture(cache.Id, Photo);

                        var picurl = new ERP.Common.FileHelper().GetWebFileUrl(Photo, ERP.Common.FileConfig.FileType.MemberPhoto.ToString());
                        var model = new Models.AccountModels.result_model();
                        model.picture = picurl;
                        data.response = (int)Extensions.ErrorInfo.OK;
                        data.message = "上传成功";
                        data.result = model;
                    }
                    else
                    {
                        data.response = (int)Extensions.ErrorInfo.UploadFailure;
                        data.message = "上传图像失败";
                    }
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.TokenError;
                    data.message = "token过期请重新登录";
                }
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            return Json(data);
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="password"></param>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifyPassword(string token,string memberid,string password)
        {
            int response=0;
            string message = "";
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(memberid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("token", token);
                    dic.Add("password", password);
                    dic.Add("memberid", memberid);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                var cache = new Cache.MemberTokenCache().Get(token);
                if (cache != null)
                {
                    if (memberid == cache.Id.ToString() && cache.Password == password)
                    {
                        response = (int)Extensions.ErrorInfo.OK;
                        message = "验证成功";
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.PassError;
                        message = "密码验证失败";
                    }
                }
                else
                {
                    response = (int)Extensions.ErrorInfo.TokenError;
                    message = "token过期请重新登录";
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            return Json(new { response = response, message = message });
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="memberid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult UpdatePassword(string token, string memberid, string password)
        {
            int response = 0;
            string message = "";
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(memberid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("token", token);
                    dic.Add("password", password);
                    dic.Add("memberid", memberid);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                var cache = new Cache.MemberTokenCache().Get(token);
                if (cache != null && memberid == cache.Id.ToString())
                {
                    var re = proveder.UpdatePassword(cache.Id, password.Trim());
                    if (re > 0)
                    {
                        response = (int)Extensions.ErrorInfo.OK;
                        message = "密码修改成功";
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.PassError;
                        message = "密码修改失败";
                    }
                }
                else
                {
                    response = (int)Extensions.ErrorInfo.TokenError;
                    message = "token过期请重新登录";
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            return Json(new { response = response, message = message });
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ActionResult NewPassword(string mobile, string password,string uuid,string verifycode)
        {
            int response = 0;
            string message = "";
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(mobile))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("mobile", mobile);
                    dic.Add("password", password);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                //安全起见 二次验证验证码
                var ver = new Provider.ShortMessageProvider().VerifySMS(mobile, uuid, verifycode);
                if (ver > 0)
                {
                    var re = proveder.NewPassword(mobile.Trim(), password.Trim());
                    if (re > 0)
                    {
                        response = (int)Extensions.ErrorInfo.OK;
                        message = "密码修改成功";
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.PassError;
                        message = "密码修改失败";
                    }
                }
                else
                {
                    response = (int)Extensions.ErrorInfo.SMSVerifyError;
                    message = "验证失败";
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(AccountController), ex);
            }
            return Json(new { response = response, message = message });
        }
    }
}