using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UCMS.Common
{
    public class SMSHelper
    {
        private static string x_eid = ConfigurationManager.AppSettings["x_eid"];
        private static string x_uid = ConfigurationManager.AppSettings["x_uid"];
        private static string x_pwd = ConfigurationManager.AppSettings["x_pwd"];
        /// <summary>
        /// 发送短信接口 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static int SendCode(string mobile, string strCode)
        {
            string retString = "0";
            //企业ID
            //帐号
            //密码
            string strSMS = "验证码为" + strCode;
            string url = "http://api.xhsms.com/utf8/web_api/?x_eid=" + x_eid + "&x_uid=" + x_uid + "&x_pwd_md5=" + x_pwd + "&x_ac=10&x_gate_id=300&x_target_no=" + mobile + "&x_memo=" + strSMS;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return Common.ToolHelper.ConvertToInt(retString);
        }
        /*
     大于0 发送成功, 此次发送成功条数
     0  服务器错误
	-1	参数无效
	-2	通道不存在或者当前业务不支持此通道
	-3	定时格式错误
	-4	接收号码无效
	-5	提交号码个数超过上限,每个通道都有批量提交的上限.详细值请参考通道说明
	-6	发送短信内容长度不符合要求,参考通道要求长度
	-7	当前账户余额不足
	 -8	网关发送短信时出现异常
	 -9	用户或者密码没输入
	 -10	企业ID或者会员账号不存在
	 -11	密码错误
	 -12	账户锁定
	 -13	网关状态关闭
	 -14	验证用户时执行异常
	 -15	网关初始化失败
	-16	当前IP已被系统屏蔽,可能是与您设置的接入IP不同或者是失败次数太多
	-17	发送异常
	-18	账号未审核
	-19	当前时间不允许此通道工作，主要对群发通道限制
	-20	传输密钥未设置，请登陆平台设置
	-21	提取密钥异常
	-22	签名验证失败
	-23	发现屏蔽关键字
	-100到-199	运营商返回失败代码
         */
    }
}