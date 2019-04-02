using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common
{
    /// <summary>
    /// 主键标识类
    /// </summary>
    public class PrimaryKey
    {
        /// <summary>
        /// 根据年月日+随机数获取19位的数据序列  不适用并发场景
        /// </summary>
        public static long GetRandomID
        {
            get
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                return long.Parse(DateTime.Now.ToString("yyMMddHHmmssff") + random.Next(10000, 99999).ToString());
            }
        }

        /// <summary>
        /// 根据年月日+随机数获取19位的数据序列
        /// </summary>
        public static long GetHashCodeID
        {
            get
            {
                var r = new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999);
                var s = Guid.NewGuid().GetHashCode().ToString().Replace("-", string.Empty) + r;
                if (s.Length > 13)
                {
                    s = s.Substring(0, 13);
                }
                var num = 19 - s.Length;
                var f = DateTime.Now.ToString("yyMMddHHmmssffff").Substring(0, num) + s;
                return long.Parse(f);
            }
        }

        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 生成8为登录帐号
        /// </summary>
        public static string GetLoginCode
        {
            get
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                return random.Next(10000000, 99999999).ToString();
            }
        }
    }
}
