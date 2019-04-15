using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common
{
    public class QiniuHelper
    {
        /// <summary>
        /// 七牛上传文件
        /// </summary>
        /// <param name="filePath">文件本地路径</param>
        /// <param name="fileName">文件名称</param>
        public static int UploadFile(string filePath,string fileName)
        {
            Mac mac = new Mac(FileConfig.AccessKey, FileConfig.SecretKey);
            // 本地文件路径
            // 存储空间名
            string Bucket = FileConfig.Bucket;
            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            var putPolicy = new PutPolicy();
            // 设置要上传的目标空间
            putPolicy.Scope = Bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            // 文件上传完毕后，在多少天后自动被删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传token
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
            Config config = new Config();
            // 设置上传区域
            config.Zone = Zone.ZONE_CN_South;
            // 设置 http 或者 https 上传
            config.UseHttps = true;
            config.UseCdnDomains = true;
            config.ChunkSize = ChunkUnit.U4096K;
            // 表单上传
            FormUploader target = new FormUploader(config);
            var result = target.UploadFile(filePath, fileName, token, null);
            return result.Code;
        }

        public static string GetUploadToken()
        {
            Mac mac = new Mac(FileConfig.AccessKey, FileConfig.SecretKey);
            // 本地文件路径
            // 存储空间名
            string Bucket = FileConfig.Bucket;
            // 设置上传策略，详见：https://developer.qiniu.com/kodo/manual/1206/put-policy
            var putPolicy = new PutPolicy();
           
            // 设置要上传的目标空间
            putPolicy.Scope = Bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            // 文件上传完毕后，在多少天后自动被删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传token
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            return token;
        }
    }
}
