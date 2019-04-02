using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common
{
    public class FileConfig
    {
        /// <summary>
        /// 个人图片上传存储地址
        /// url:物理路径/UploadFile/年月
        /// </summary>
        public static string UserPhotoPath
        {
            get
            {
                return String.Format("/UploadFile/{0}", FileType.UserPhoto.ToString());
            }
        }
        
        /// <summary>
        /// 车源图片上传存储地址
        /// url:物理路径/UploadFile/年月
        /// </summary>
        public static string CarPhotoPath
        {
            get
            {
                return String.Format("/UploadFile/{0}", FileType.CarPhoto.ToString());
            }
        }
        /// <summary>
        /// 其他图片上传存储地址
        /// url:物理路径/UploadFile/年月
        /// </summary>
        public static string OtherPhotoPath
        {
            get
            {
                return String.Format("/UploadFile/{0}", FileType.OtherPhoto.ToString());
            }
        }

        /// <summary>
        /// 默认用户头像
        /// </summary>
        public static string DefaultPhotoPath
        {
            get
            {
                return "~/Theme/img/99999.png";
            }
        }

        /// <summary>
        /// 上传文件临时文件夹
        /// </summary>
        public static string TempPath
        {
            get
            {
                return String.Format("/UploadFile/Temp");
            }
        }
        /// <summary>
        /// 上传 常用文件
        /// </summary>
        public static string FilePath
        {
            get
            {
                return String.Format("/UploadFile/File");
            }
        }
        /// <summary>
        /// 文章附件类音视频存储路径
        /// </summary>
        public static string VideoPath
        {
            get
            {
                return  "/UploadFile/Video";
            }
        }


        /// <summary>
        /// 附件存储类型配置
        /// </summary>
        public enum FileType
        {
            /// <summary>
            /// 个人头像
            /// </summary>
            UserPhoto,
            /// <summary>
            /// 身份证相片
            /// </summary>
            IDcardPhoto,
            /// <summary>
            /// 车辆图片
            /// </summary>
            CarPhoto,
            /// <summary>
            /// 其他图片
            /// </summary>
            OtherPhoto,
            /// <summary>
            /// 常用文件File
            /// </summary>
            File,
        }


        /// <summary>
        /// 文件类型 
        /// 0-文件夹 1-图片 2-文档 3-音乐 9-其他
        /// </summary>
        public enum DiskType
        {
            /// <summary>
            /// 文件夹
            /// </summary>
            Folder = 0,
            /// <summary>
            /// 图片
            /// </summary>
            Picture = 1,
            /// <summary>
            /// 文档
            /// </summary>
            Document = 2,
            /// <summary>
            /// 音乐
            /// </summary>
            Music = 3,
            /// <summary>
            /// 其他
            /// </summary>
            Other = 9
        }
        /// <summary>
        /// 文件物理路径  保存物理路径
        /// </summary>
        public static string FileLocalPath
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.AppSettings["FileLocalPath"] != null)
                {
                    result = ConfigurationManager.AppSettings["FileLocalPath"].ToString();
                }
                return result;
            }
        }
        /// <summary>
        /// Web端资源请求路径 请求web路径
        /// </summary>
        public static string FileWebPath
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.AppSettings["FileWebPath"] != null)
                {
                    result = ConfigurationManager.AppSettings["FileWebPath"].ToString();
                }
                return result;
            }
        }
        /// <summary>
        /// 七牛key
        /// </summary>
        public static string AccessKey
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.AppSettings["AccessKey"] != null)
                {
                    result = ConfigurationManager.AppSettings["AccessKey"].ToString();
                }
                return result;
            }
        }
        /// <summary>
        /// 七牛skey
        /// </summary>
        public static string SecretKey
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.AppSettings["SecretKey"] != null)
                {
                    result = ConfigurationManager.AppSettings["SecretKey"].ToString();
                }
                return result;
            }
        }
        /// <summary>
        /// 七牛空间
        /// </summary>
        public static string Bucket
        {
            get
            {
                string result = string.Empty;
                if (ConfigurationManager.AppSettings["Bucket"] != null)
                {
                    result = ConfigurationManager.AppSettings["Bucket"].ToString();
                }
                return result;
            }
        }
        /// <summary>
        /// 资源状态 0默认本地 1七牛
        /// </summary>
        public static int ResourceStatus
        {
            get
            {
                var result = 0;
                if (ConfigurationManager.AppSettings["ResourceStatus"] != null)
                {
                    result =ToolHelper.ConvertToInt(ConfigurationManager.AppSettings["ResourceStatus"]);
                }
                return result;
            }
        }
    }
}
