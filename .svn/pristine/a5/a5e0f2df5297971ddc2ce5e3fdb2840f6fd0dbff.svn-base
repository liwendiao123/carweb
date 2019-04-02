using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UCMS.WebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册枚举值
            Common.EnumHelper.Reg(
                typeof(Common.EnumModel.EIsDelete),
                typeof(Common.EnumModel.EAuditStatus),
                typeof(Common.EnumModel.EProductStatus),
                typeof(Common.EnumModel.ESex),
                typeof(Common.EnumModel.EUserType),
                typeof(Common.EnumModel.EFuelType),
                typeof(Common.EnumModel.EEmissionStandards),
                typeof(Common.EnumModel.ECarStatus),
                typeof(Common.EnumModel.ETransmission),
                typeof(Common.EnumModel.EPromiseStatus)
                );

            //初始化缓存连接池
            var memcached = new UCMS.Cache.MemcachedPool();
            memcached.MemcachedSockIOPool();
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
        //    var log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //    log.Error("error", Server.GetLastError());
        //    //var error = Server.GetLastError();
        //    //log.Error(string.Format("异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n", error.GetType().Name, error.Message, error.StackTrace));
        //}
    }
}
