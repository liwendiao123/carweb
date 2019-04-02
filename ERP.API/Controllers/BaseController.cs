using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 默认图片 TODO：待
        /// </summary>
        public string DefultPicture
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// 是否打开授权
        /// </summary>
        public bool AuthIsOpen
        {
            get
            {
                if (ConfigurationManager.AppSettings["AuthIsOpen"] == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        ///  验证接口授权认证
        /// </summary>
        /// <param name="postValue"></param>
        public void VerifyAuthorize(SortedDictionary<string, string> postValue)
        {
            var timestamp = Request["timestamp"];
            var imei = Request["imei"];
            var sign = Request["sign"];

            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(imei) || string.IsNullOrEmpty(sign))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;//定义状态后就不会在往下执行了
            }
            var tsDate = Common.ToolHelper.ConvertIntDateTime(Common.ToolHelper.ConvertToLong(timestamp));
            int interface_outtime = ERP.Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["interface_outtime"]);//接口超时时间
            if (tsDate > DateTime.Now.AddMinutes(interface_outtime) || tsDate < DateTime.Now.AddMinutes(-interface_outtime))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.TimeOut,message="接口超时" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }

            string isAllow = ConfigurationManager.AppSettings["allow_simulate_access"];
            if (imei == "000000000000000" && isAllow == "false")
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.ImeiError, message = "没有设备码" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }
            
            //判断签名
            string APIPrivateKey = ConfigurationManager.AppSettings["encryption_key"];//密钥
            string mysign = Extensions.CommonHelper.GetResponseMysign(postValue, APIPrivateKey);
            if (!sign.Equals(mysign, StringComparison.InvariantCultureIgnoreCase))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.SignError, message = "签名错误" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }
        }
        /// <summary>
        /// 参数验证授权
        /// </summary>
        /// <param name="postValue"></param>
        /// <param name="timestamp"></param>
        /// <param name="imei"></param>
        /// <param name="sign"></param>
        public void VerifyAuthorize(SortedDictionary<string, string> postValue,string timestamp,string imei,string sign)
        {
            if (string.IsNullOrEmpty(timestamp) || string.IsNullOrEmpty(imei) || string.IsNullOrEmpty(sign))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;//定义状态后就不会在往下执行了
            }
            var tsDate = Common.ToolHelper.ConvertIntDateTime(Common.ToolHelper.ConvertToLong(timestamp));
            int interface_outtime = ERP.Common.ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["interface_outtime"]);//接口超时时间
            if (tsDate > DateTime.Now.AddMinutes(interface_outtime) || tsDate < DateTime.Now.AddMinutes(-interface_outtime))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.TimeOut, message = "接口超时" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }

            string isAllow = ConfigurationManager.AppSettings["allow_simulate_access"];
            if (imei == "000000000000000" && isAllow == "false")
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.ImeiError, message = "没有设备码" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }

            //判断签名
            string APIPrivateKey = ConfigurationManager.AppSettings["encryption_key"];//密钥
            string mysign = Extensions.CommonHelper.GetResponseMysign(postValue, APIPrivateKey);
            if (!sign.Equals(mysign, StringComparison.InvariantCultureIgnoreCase))
            {
                HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { response = Extensions.ErrorInfo.SignError, message = "签名错误" }));
                HttpContext.Response.End();
                HttpContext.Response.StatusCode = 401;
            }
        }
    }
}