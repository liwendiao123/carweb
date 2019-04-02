using ERP.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Base
        public ActionResult Index()
        {
            return Content("Hello Word !!");
            //return View();
        }
        /// <summary>
        /// 短信验证  如果是找回密码isaccount 为1   默认为0
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendSMS(string uuid, string mobile, string isaccount)
        {
            int response = 0;
            string message = "";
            if (string.IsNullOrEmpty(uuid) || string.IsNullOrEmpty(mobile))
            {
                return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
            }
            try
            {
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("uuid", uuid);
                    dic.Add("mobile", mobile);
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                #region 验证10分钟内发送次数 
                var blackDate = Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["blackDate"]);//黑名单时间
                //次数超过10条后拉入黑名单，过了时间在拉白
                var cache = HttpRuntime.Cache.Get("sms" + mobile);
                bool b = true;
                if (cache == null)
                {
                    //不在黑名单需要从数据库验证
                    var min = Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["short_date"]);
                    var count =new Provider.ShortMessageProvider().GetDateShort(mobile, DateTime.Now.AddMinutes(-min));
                    var errorLog = Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["short_count"]);
                    if (count >= errorLog)
                    {
                        HttpRuntime.Cache.Insert("sms" + mobile, "" + DateTime.Now + "", null, DateTime.Now.AddMinutes(blackDate), TimeSpan.Zero);
                        b = false;
                        response = (int)Extensions.ErrorInfo.SMSBlackError;
                        message = "短信发送太频繁，请"+ blackDate + "分钟后在试";
                    }
                }
                else
                {
                    if (Common.ToolHelper.ConvertToDateTime(cache).AddMinutes(Common.ToolHelper.ConvertToInt(blackDate) + 1) > DateTime.Now)
                    {
                        b = false;
                        response = (int)Extensions.ErrorInfo.SMSBlackError;
                        message = "短信发送太频繁，请" + blackDate + "分钟后在试";
                    }
                    else
                    {
                        //取消黑名单
                        HttpRuntime.Cache.Remove("sms" + mobile);
                    }
                }
                #endregion
                if (b)
                {
                    var code = Common.ToolHelper.RandomNumber(4);
                    bool b1 = true;
                    if (!string.IsNullOrEmpty(isaccount) && isaccount == "1")
                    {
                        //账号
                        var ret = new Provider.MemberBasisProvider().GetMemberByMobile(mobile);
                        if (ret != null)
                        {
                            b = true;
                        }
                        else
                        {
                            b = false;
                        }
                    }
                    if (b1)
                    {
                        var result = Common.SMSHelper.SendCode(mobile, code);
                        if (result > 0)
                        {
                            var model = new Entitys.SysShortMessage()
                            {
                                UUID = uuid,
                                Mobile = mobile,
                                CreateTime = DateTime.Now,
                                VerifyCode = code
                            };
                            var re=new Provider.ShortMessageProvider().Save(model);
                            if (re > 0)
                            {
                                response = (int)Extensions.ErrorInfo.OK;
                                message = "发送成功";
                            }
                            else
                            {
                                response = (int)Extensions.ErrorInfo.SMSVerifyError;
                                message = "发送失败";
                            }
                        }
                        else
                        {
                            response = (int)Extensions.ErrorInfo.SMSVerifyError;
                            message = "发送失败";
                        }
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.UserNone;
                        message = "账号不存在";
                    }
                }

            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(HomeController), ex);
            }
            return Json(new { response = response, message = message });
        }
        /// <summary>
        /// 验证码验证  TODO：需要定期清理数据
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifySMS(string mobile, string uuid, string verifycode)
        {
            int response = 0;
            string message = "";
            try
            {
                if (!string.IsNullOrEmpty(mobile) && !string.IsNullOrEmpty(uuid))
                {
                    if (AuthIsOpen)
                    {
                        var dic = new SortedDictionary<string, string>();
                        dic.Add("uuid", uuid);
                        dic.Add("mobile", mobile);
                        dic.Add("verifycode", verifycode);
                        dic.Add("timestamp", Request["timestamp"]);
                        VerifyAuthorize(dic);
                    }
                    var ver=new Provider.ShortMessageProvider().VerifySMS(mobile,uuid,verifycode);
                    if (ver >0)
                    {
                        response = (int)Extensions.ErrorInfo.OK;
                        message = "验证成功";
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.SMSVerifyError;
                        message = "验证失败";
                    }
                }
                else
                {
                    response = (int)Extensions.ErrorInfo.ParameterError;
                    message = "参数错误";
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(HomeController), ex);
            }
            return Json(new { response = response, message = message });
        }
    }
}