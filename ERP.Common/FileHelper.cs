using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UCMS.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 高质量图片压缩
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图指定宽度</param>
        /// <param name="height">缩略图指定高度</param>
        /// <param name="mode"></param>
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
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

        /// <summary>
        /// 上传文件  相对路径保存   
        /// 示例： Picture = UCMS.Common.FileHelper.SaveFileAbsolute(fileName, "", UCMS.Common.FileConfig.UserPhotoPath, false, 0, 0, "");
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileId">原文件名 上传成功删除原文件</param>
        /// <param name="thumbnail">图片是否压缩</param>
        /// <param name="filePath">>FileConfig 对应的路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">"HW"://指定高宽缩放（可能变形） "W"://指定宽，高按比例 "H"://指定高，宽按比例 "Cut"://指定高宽裁减（不变形）  </param>
        /// <returns></returns>
        public string SaveFileRelative(string fileName, string fileId, string filePath)
        {
            var fileUpload = HttpContext.Current.Request.Files[fileName];
            var FileId = "";
            if (fileUpload != null && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                try
                {
                    while (true)
                    {
                        FileId = PrimaryKey.GetHashCodeID.ToString();
                        string FileExt = Path.GetExtension(fileUpload.FileName);
                        var FolderName = FileId.Substring(0, 4);
                        var FileName = FileId.Substring(4);
                        var FilePath = string.Format("{0}/{1}/", filePath, FolderName);
                        var fileMinPath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}{2}", FilePath, FileName, FileExt));//上传文件的全路径
                        if (!System.IO.File.Exists(fileMinPath))
                        {
                            //目录不存在则创建
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FilePath));
                            }
                            fileUpload.SaveAs(fileMinPath);
                            if (!String.IsNullOrEmpty(fileMinPath))
                            {
                                if (!string.IsNullOrEmpty(fileId))
                                {
                                    //删除旧头像
                                    var oldFilePath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4)));
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }
                            }
                            if (!String.IsNullOrEmpty(fileMinPath))
                            {
                                if (!string.IsNullOrEmpty(fileId))
                                {
                                    //删除旧头像
                                    var oldFilePath = string.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4));
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }
                            }
                            FileId += FileExt;
                            //执行完毕，跳出循环
                            break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    FileId = "";
                    throw ex;
                }
            }
            return FileId;
        }
        /// <summary>
        /// 上传图片文件  相对路径保存   
        /// 示例： Picture = UCMS.Common.FileHelper.SaveFileAbsolute(fileName, "", UCMS.Common.FileConfig.UserPhotoPath, false, 0, 0, "");
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileId">原文件名 上传成功删除原文件</param>
        /// <param name="thumbnail">图片是否压缩</param>
        /// <param name="filePath">>FileConfig 对应的路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">"HW"://指定高宽缩放（可能变形） "W"://指定宽，高按比例 "H"://指定高，宽按比例 "Cut"://指定高宽裁减（不变形）  </param>
        /// <returns></returns>
        public string SaveImgRelative(string fileName, string fileId, string filePath, bool isThumbnail = false, int width = 0, int height = 0, string mode = "")
        {
            var fileUpload = HttpContext.Current.Request.Files[fileName];

            var FileId = "";
            if (fileUpload != null&&!string.IsNullOrEmpty(fileUpload.FileName))
            {
                try
                {
                    while (true)
                    {
                        FileId = PrimaryKey.GetHashCodeID.ToString();
                        string FileExt = Path.GetExtension(fileUpload.FileName);
                        FileExt = GetExt(fileUpload.InputStream);
                        var fileBigPath = Path.GetTempPath() + "BIG" + FileId + FileExt;
                        var FolderName = FileId.Substring(0, 4);
                        var FileName = FileId.Substring(4);
                        var FilePath = string.Format("{0}/{1}/", filePath, FolderName);
                        var fileMinPath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}{2}", FilePath, FileName, FileExt));//上传文件的全路径
                        if (!System.IO.File.Exists(fileMinPath))
                        {
                            //目录不存在则创建
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(FilePath)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FilePath));
                            }
                            //文件处理
                            if (isThumbnail)
                            {
                                //保存原始文件
                                fileUpload.SaveAs(fileBigPath);
                                //压缩小图
                                MakeThumbnail(fileBigPath, fileMinPath, width, height, mode);
                                //删除上传原图
                                if (System.IO.File.Exists(fileBigPath))
                                {
                                    System.IO.File.Delete(fileBigPath);
                                }
                            }
                            else
                            {
                                //不压缩直接保存图片
                                fileUpload.SaveAs(fileMinPath);
                            }
                            if (!String.IsNullOrEmpty(fileMinPath))
                            {
                                //1上传七牛  0默认不上传
                                if (Common.FileConfig.ResourceStatus == 1)
                                {
                                    var frst = filePath.Substring(0, 1);
                                    var fp = "";
                                    if(frst=="/")
                                    {
                                        fp = filePath.Substring(1, filePath.Length-1);
                                    }
                                    var fname=string.Format("{0}/{1}/{2}", fp, FileId.Substring(0, 4), FileId.Substring(4)+ FileExt);
                                    //Task.Factory.StartNew(() =>
                                    //{
                                        try
                                        {
                                            var res=QiniuHelper.UploadFile(fileMinPath, fname);
                                            if (res != 200)
                                            {
                                                Common.LogHelper.WriteLog(typeof(FileHelper), string.Format("七牛上传没成功：{0}-{1}",FileId, fileMinPath));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Common.LogHelper.WriteLog(typeof(FileHelper), string.Format("七牛上传报错：{0}-{1}", FileId, fileMinPath));
                                            Common.LogHelper.WriteLog(typeof(FileHelper), ex);
                                        }
                                    //});
                                }
                                if (!string.IsNullOrEmpty(fileId))
                                {
                                    //删除旧头像
                                    var oldFilePath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4)));
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }
                            }
                            if (!String.IsNullOrEmpty(fileMinPath))
                            {
                                if (!string.IsNullOrEmpty(fileId))
                                {
                                    //删除旧头像
                                    var oldFilePath = string.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4));
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }
                            }
                            FileId += FileExt;
                            //执行完毕，跳出循环
                            break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    FileId = "";
                    throw ex;
                }
            }
            return FileId;
        }
        /// <summary>
        /// 上传文件 绝对路径保存
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileId">原文件名 上传成功删除原文件</param>
        /// <param name="thumbnail">图片是否压缩</param>
        /// <param name="filePath">>FileConfig 对应的路径</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="mode">"HW"://指定高宽缩放（可能变形） "W"://指定宽，高按比例 "H"://指定高，宽按比例 "Cut"://指定高宽裁减（不变形）  </param>
        /// <returns></returns>
        public string SaveImgAbsolute(string fileName, string fileId, string filePath, bool isThumbnail = false, int width = 0, int height = 0, string mode = "")
        {
            var fileUpload = HttpContext.Current.Request.Files[fileName];
            var FileId = "";
            if (fileUpload != null)
            {
                try
                {
                    while (true)
                    {
                        FileId = PrimaryKey.GetHashCodeID.ToString();
                        string FileExt = Path.GetExtension(fileUpload.FileName);
                        FileExt = GetExt(fileUpload.InputStream);
                        var fileBigPath = Path.GetTempPath() + "BIG" + FileId + FileExt;
                        var FolderName = FileId.Substring(0, 4);
                        var FileName = FileId.Substring(4);
                        var FilePath = string.Format("{0}/{1}/", filePath, FolderName);
                        var fileMinPath = string.Format("{0}/{1}{2}", FilePath, FileName, FileExt);//上传文件的全路径
                        if (!System.IO.File.Exists(fileMinPath))
                        {
                            //目录不存在则创建
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);
                            }
                            //文件处理
                            if (isThumbnail)
                            {
                                //保存原始文件
                                fileUpload.SaveAs(fileBigPath);
                                //压缩小图
                                MakeThumbnail(fileBigPath, fileMinPath, width, height, mode);
                                //删除上传原图
                                if (System.IO.File.Exists(fileBigPath))
                                {
                                    System.IO.File.Delete(fileBigPath);
                                }
                            }
                            else
                            {
                                //不压缩直接保存图片
                                fileUpload.SaveAs(fileMinPath);
                            }
                            if (!String.IsNullOrEmpty(fileMinPath))
                            {
                                if (!string.IsNullOrEmpty(fileId))
                                {
                                    //删除旧头像
                                    var oldFilePath = string.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4));
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }
                            }
                            FileId += FileExt;
                            //执行完毕，跳出循环
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    FileId = "";
                    throw ex;
                }
            }
            return FileId;
        }
        /// <summary>
        /// 获取文件地址
        /// </summary>
        /// <param name="fileId">文件名</param>
        /// <param name="filePath">FileConfig 对应的路径</param>
        /// <param name="httpContext">当前上下文 this.HttpContext</param>
        /// <returns>返回相对路径</returns>
        public string GetFileUrl(string fileId, string filePath, HttpContextBase httpContext)
        {
            string fileUrl = "";
            if (!String.IsNullOrEmpty(fileId))
            {
                var photo = String.Format("{0}/{1}/{2}", filePath, fileId.Substring(0, 4), fileId.Substring(4));
                //var path = HttpContext.Current.Server.MapPath(photo);
                if (FileConfig.ResourceStatus == 1)
                {
                    //七牛路径
                    fileUrl = String.Format("{0}/{1}/{2}/{3}", FileConfig.FileWebPath, filePath, fileId.Substring(0, 4), fileId.Substring(4));
                }
                else
                {
                    var url = new System.Web.Mvc.UrlHelper(httpContext.Request.RequestContext);
                    fileUrl = url.Content(photo);
                }
            }
            return fileUrl;
        }
        /// <summary>
        /// 动态获取扩展名
        /// </summary>
        /// <param name="InputStream"></param>
        /// <returns></returns>
        public string GetExt(System.IO.Stream InputStream)
        {
            var ext = "";

            System.IO.BinaryReader r = new System.IO.BinaryReader(InputStream);
            string fileExt = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileExt = buffer.ToString();
                buffer = r.ReadByte();
                fileExt += buffer.ToString();
            }
            catch
            {
                ext = ".jpg";
            }
            //r.Close();
            switch (fileExt)
            {
                case "7173":
                    ext = ".gif";
                    break;
                case "255216":
                    ext = ".jpg";
                    break;
                case "13780":
                    ext = ".png";
                    break;
                default:
                    ext = ".jpg";
                    break;
            }
            return ext;
        }
    }
}
