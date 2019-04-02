using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Text;

namespace UCMS.Common
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class ToolHelper
    {
        /// <summary>
        /// 取得客户端IP地址
        /// </summary>
        public static string GetClientIP
        {
            get
            {
                string _IP = String.Empty;
                try
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    {
                        _IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                    }
                    else
                    {
                        _IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(ToolHelper), ex);
                }
                return _IP;
            }
        }
        public static bool IsMoney(string s)
        {
            string pattern = @"^\-?([1-9]\d*|0)(\.\d{1,2})?$";
            return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.AppendHeader("Pragma", "no-cache");
            System.Web.HttpContext.Current.Response.Cache.SetNoServerCaching();
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.Cache.SetExpires(new DateTime(1900, 01, 01, 00, 00, 00, 00));
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <param name="length">长度</param>
        /// <returns>返回截取后的字符</returns>
        public static string GetString(string str, int length)
        {
            //限制输出字符串
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return str;
        }

        /// <summary> 
        /// 四舍五入 
        /// eg:return Round(5.55, 1); 
        /// </summary> 
        /// <param name="value"></param> 
        /// <param name="digits">精度</param> 
        /// <returns></returns> 
        public static decimal Round(decimal value, int digits)
        {
            decimal rv = value;
            rv = System.Math.Round(value, digits, MidpointRounding.AwayFromZero);
            return rv;
        }

        /// <summary>
        /// 验证字符串是否是图片路径
        /// </summary>
        /// <param name="Input">待检测的字符串</param>
        /// <returns>返回true 或 false</returns>
        public static bool IsPicturePath(string checkStr)
        {
            bool re_Val = false;
            if (checkStr != string.Empty)
            {
                string s_input = checkStr;
                if (s_input.IndexOf(checkStr.ToLower()) != -1 && s_input.IndexOf(".") != -1)
                {
                    string Ex_Name = s_input.Substring(s_input.LastIndexOf(".") + 1).ToString().ToLower();
                    if (Ex_Name == "jpg" || Ex_Name == "gif" || Ex_Name == "bmp" || Ex_Name == "png")
                    {
                        re_Val = true;
                    }
                }
            }
            return re_Val;
        }
        
        public static string GetAsciiCode(string str)
        {
            byte[] code = new byte[50];
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int val = (int)str[i];
                if (val > 127)
                {
                    code[j] = Convert.ToByte(val / 256);
                    code[++j] = Convert.ToByte(val % 256);
                }
                else
                {
                    code[j] = Convert.ToByte(val);
                }
                j++;
            }
            return BitConverter.ToString(code).Replace("-", "").Substring(0, 32);
        }

        /// <summary>
        /// 高质量图片压缩
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图指定宽度</param>
        /// <param name="height">缩略图指定高度</param>
        /// <param name="mode"></param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形） 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

 

        public static string GetFileSize(int filesize)
        {
            return FileSize(filesize);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string FileSize(double sizeinbytes)
        {
            string result = string.Empty;
            double sizeinkbytes = Math.Round(sizeinbytes / 1024);
            double sizeinmbytes = Math.Round(sizeinkbytes / 1024);
            double sizeingbytes = Math.Round(sizeinmbytes / 1024);
            if (sizeingbytes > 1)
            {
                return string.Format("{0}GB", sizeingbytes);
            }
            else
            {
                if (sizeinmbytes > 1)
                {
                    result = string.Format("{0}MB", sizeinmbytes);
                }
                else
                {
                    if (sizeinkbytes > 1)
                    {
                        result = string.Format("{0}KB", sizeinkbytes);
                    }
                    else
                    {
                        result = string.Format("{0}B", sizeinbytes);
                    }
                }
            }
            return result;
        }

        #region 去除html标记


        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="htmlstring"></param>
        /// <returns></returns>
        public static string NoHtml(string htmlstring)
        {
            //删除脚本   
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring.Replace("<", "");
            htmlstring.Replace(">", "");
            htmlstring.Replace("\r\n", "");
            htmlstring = HttpContext.Current.Server.HtmlEncode(htmlstring).Trim();
            return htmlstring;
        }
        #endregion


        /// <summary>
        /// 判断一个字符串是否为合法整数(不限制长度)
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsInteger(string s)
        {
            //string pattern = @"^\d*$";
            string pattern = @"([-\\+]?[1-9]([0-9]*)(\\.[0-9]+)?)|(^0$)";
            return Regex.IsMatch(s, pattern);
        }

        /// <summary>
        /// 用递归方法删除文件夹目录及文件
        /// </summary>
        /// <param name="dir">带文件夹名的路径</param> 
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir, true); //删除已空文件夹                 
            }
        }

        /// <summary>
        /// 检查是否图片类型
        /// </summary>
        /// <param name="ContentType">文件类型</param>
        /// <returns></returns>
        public static bool CheckPicture(string ContentType)
        {
            if (ContentType == "image/pjpeg" || ContentType == "image/jpeg" || ContentType == "image/x-png" || ContentType == "image/gif" || ContentType == "image/bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 自动抓取并保存远程图片
        /// </summary>
        /// <param name="savePath">要保存图片的路径</param>
        /// <param name="HtmlText">图片地址，支持html</param>
        /// <returns></returns>
        public static string PictureSave(string savePath, string HtmlText)
        {
            string _ret = "";
            //自动保存远程图片
            WebClient client = new WebClient();
            //备用Reg:<img.*?src=([\"\'])(http:\/\/.+\.(jpg|gif|bmp|bnp))\1.*?>
            Regex reg = new Regex(@"<img.*src\s*=\s*[""']?\s*(?<imgUrl>[^\s""']*).*/?\s*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(HtmlText);
            foreach (Match math in m)
            {
                string imgUrl = math.Groups[1].Value;
                //在原图片名称前加YYMMDD重命名
                Regex regName = new Regex(@"\w+.(?:jpg|gif|bmp|png)", RegexOptions.IgnoreCase);
                string strNewImgName = DateTime.Now.ToShortDateString().Replace("-", "") + regName.Match(imgUrl).ToString();
                try
                {
                    //保存图片
                    client.DownloadFile(imgUrl, HttpContext.Current.Server.MapPath(savePath + strNewImgName));
                }
                catch (Exception ex)
                {
                    _ret = ex.ToString();
                }
                client.Dispose();
                _ret = "远程图片保存成功！";
            }
            return _ret;
        }

        /// <summary>
        /// Json对敏感字符的过滤
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String StringToJson(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        #region 类型转换

        /// <summary>
        /// 转换Int类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ConvertToInt(object str)
        {
            if (str != null)
            {
                int val;
                int.TryParse(str.ToString(), out val);
                return val;
            }
            return 0;
        }

        public static DateTime MinDateTime
        {
            get
            {
                return DateTime.Parse("1900-01-01");
            }
        }

        /// <summary>
        /// 将字符串转换成long类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ConvertToLong(object str)
        {
            if (str != null)
            {
                long ret;
                long.TryParse(str.ToString(), out ret);
                return ret;
            }
            return 0;
        }



        /// <summary>
        /// 转换decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(object str)
        {
            if (str != null)
            {
                decimal ret;
                decimal.TryParse(str.ToString(), out ret);
                return ret;
            }

            return decimal.Zero;
        }

        /// <summary>
        /// 转换DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(object str)
        {
            DateTime val;
            DateTime.TryParse(str.ToString(), out val);
            return (val == DateTime.MinValue ? MinDateTime : val);
        }

        /// <summary>
        /// 日期long转换为DateTime类型
        /// </summary>
        /// <param name="date">long日期数值，大于等于14位</param>
        /// <returns></returns>
        public static DateTime ParseExact(long date)
        {
            if (date >= 14)
            {
                return DateTime.ParseExact(date.ToString(), "yyyyMMddhhmmss", System.Globalization.CultureInfo.CurrentCulture);
            }
            else
            {
                return MinDateTime;
            }
        }
        #endregion

        /// <summary>
        /// 将数字转成成字母1-26
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Char IntToChar(int i)
        {
            if (i > 0 && i < 27)
                return Convert.ToChar(i + 64);
            else
                return char.MinValue;
        }

        /// <summary>
        /// 时间转换Long
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long DateToLong(DateTime date)
        {
            long Result = 9999;
            try
            {
                Result = Convert.ToInt64(date.ToString("yyyyMMddhhmm"));
            }
            catch
            {
                Result = long.MinValue;
            }
            return Result;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000; //除10000调整为13位
            return t;
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertIntDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 转换十位的时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ConvertTimeStamp(DateTime dt)
        {
            // Default implementation of UNIX time of the current UTC time  
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)Math.Floor(ts.TotalSeconds);
        }

        /// <summary>
        /// MD5 32位加密 
        /// </summary>
        /// <param name="str">加密后密码</param>
        /// <returns></returns>
        public static string GetMD5Hash32(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString().ToUpper();
        }
        /// <summary>
        /// MD5 16位加密 
        /// </summary>
        /// <param name="str">加密后密码</param>
        /// <returns></returns>
        public static string GetMD5Hash16(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString().ToUpper().Substring(8, 16);
        }

        #region 【提取内容中图片】

        /// <summary>
        /// 提取内容中图片
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>       
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            var regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            System.Text.RegularExpressions.MatchCollection matches = regImg.Matches(sHtmlText); 
            var list = new System.Collections.Generic.List<string>();
            // 取得匹配项列表
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                var imagePath = match.Groups["imgUrl"].Value;
                //判断是否为网络图片
                if (imagePath.IndexOf("http://") != 0)
                {
                    list.Add(match.Groups["imgUrl"].Value);
                } 
            }
            return list.ToArray();
        }

        #endregion

        public static string TextToHtml(string strContent)
        {
            strContent = strContent.Replace("&", "&amp");
            strContent = strContent.Replace("'", "''");
            strContent = strContent.Replace("<", "&lt");
            strContent = strContent.Replace(">", "&gt");
            strContent = strContent.Replace("chr(60)", "&lt");
            strContent = strContent.Replace("chr(37)", "&gt");
            strContent = strContent.Replace("\"", "&quot");
            strContent = strContent.Replace(";", ";");
            strContent = strContent.Replace("\n", "<br/>");
            strContent = strContent.Replace("  ", "&nbsp;&nbsp;");
            return strContent;
        }

        /// <summary>
        /// 判断一个字符串是否为合法数字(不限制长度)
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string s)
        {
            string pattern = @"^(\d*)|([0-9]+(.[0-9]{1})?)$";
            return Regex.IsMatch(s, pattern);
        }


        private static char[] constant =
              {
                '0','1','2','3','4','5','6','7','8','9'
              };
        private static char[] constantAB =
              {
                '0','1','2','3','4','5','6','7','8','9',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
              };
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string RandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 判断是否移动设备。
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileDevice()
        {
            string[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "googlebot-mobile" };

            bool isMoblie = false;

            string userAgent = HttpContext.Current.Request.UserAgent.ToString().ToLower();

            //排除 Windows 桌面系统或苹果桌面系统 
            if (!string.IsNullOrEmpty(userAgent) && !userAgent.Contains("macintosh") && (!userAgent.Contains("windows nt") || (userAgent.Contains("windows nt") && userAgent.Contains("compatible; msie 9.0;"))))
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (userAgent.ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }

            return isMoblie;
        }
    }
}
