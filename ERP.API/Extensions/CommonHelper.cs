using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ERP.API.Extensions
{
    public class CommonHelper
    {
        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UrlDeCode(string str, Encoding encoding = null)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                    
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                //将已经解码的字符再次进行编码.
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str.ToUpper() == encode)
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding("gb2312");
                }
            }
            return HttpUtility.UrlDecode(str, encoding);
        }
        /// <summary>
        /// 获取Sign
        /// </summary>
        /// <param name="inputPara"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string GetResponseMysign(SortedDictionary<string, string> inputPara, string privateKey)
        {
            string fullstring = GetPostStrings(inputPara) + privateKey;
            return Common.ToolHelper.GetMD5Hash32(fullstring);
        }
        private static string GetPostStrings(SortedDictionary<string, string> inputPara)
        {
            var sPara = new Dictionary<string, string>();

            //过滤空值
            foreach (var temp in inputPara)
            {
                if (temp.Value != "" && temp.Value != null)
                {
                    sPara.Add(temp.Key.ToLower(), temp.Value);
                }
            }

            //获得签名结果
            string buff = "";
            foreach (var pair in inputPara)
            {
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}