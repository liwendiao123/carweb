using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class SysNoticeController : Controller
    {
        Provider.SysNoticeProvider provider = new Provider.SysNoticeProvider();
        // GET: CarBrand
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var list = provider.GetList();
            var model = new List<Models.SysNoticeModels.SysNoticeModel>();
            foreach (var item in list)
            {
                model.Add(new Models.SysNoticeModels.SysNoticeModel
                {
                    Id = item.Id,
                    CreateTime=item.CreateTime,
                    Content=item.Content,
                    NoticeType=item.NoticeType,
                    Title=item.Title
                });
            }
            return View(model);
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? Id)
        {
            var model = new Models.SysNoticeModels.SysNoticeModel();
            if (Id != null)
            {
                var item = provider.GetNotice(Id.Value);
                model = new Models.SysNoticeModels.SysNoticeModel
                {
                    Id = item.Id,
                    CreateTime = item.CreateTime,
                    Content = item.Content,
                    NoticeType = item.NoticeType,
                    Title = item.Title
                };
            }
            return View(model);
        }
        [ValidateInput(false)]
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.SysNoticeModels.SysNoticeModel model)
        {
            var random = Common.PrimaryKey.GetHashCodeID.ToString();
            string FilePath = Common.FileConfig.TempPath + random.Substring(0, 4) + "/" + random.Substring(4) + "/";
            model.Content = MoveContentImage(model.Content, FilePath);
            var group = new UCMS.Entitys.SysNotice()
            {
                Id = model.Id,
                Content=model.Content,
                NoticeType=model.NoticeType,
                Title=model.Title,
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(group);
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        public ActionResult Detail(long? Id)
        {
            var model = new Models.SysNoticeModels.SysNoticeModel();
            if (Id != null)
            {
                var item = provider.GetNotice(Id.Value);
                model = new Models.SysNoticeModels.SysNoticeModel
                {
                    Id = item.Id,
                    CreateTime = item.CreateTime,
                    Content = item.Content,
                    NoticeType = item.NoticeType,
                    Title = item.Title
                };
            }
            return View(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="checkedId"></param>
        /// <returns></returns>
        [Extension.UserAuthorize]
        public ActionResult Delete(string checkedId)
        {
            var ids = new List<long>();
            foreach (var item in checkedId.Split(','))
            {
                ids.Add(Common.ToolHelper.ConvertToLong(item));
            }
            var line = 0;
            if (ids.Count > 0)
            {
                line = provider.Delete(ids);
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }

        /// <summary>
        /// 上传内容图片
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadImage()
        {
            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            try
            {
                string fileTypes = ".gif,.jpg,.jpeg,.png,.bmp,.mp4,.flv,.swf";
                HttpPostedFileBase file = Request.Files["imgFile"];
                if (file != null)
                {
                    string fileExt = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (string.IsNullOrEmpty(fileExt) || fileTypes.Split(',').ToList().Contains(fileExt))
                    {
                        var ImagesId = string.Empty;
                        var fileMinPath = string.Empty;
                        //文件夹
                        var Folder = string.Empty;
                        //文件ID
                        var FileId = string.Empty;

                        var TempPath = string.Empty;
                        if (fileExt == ".mp4" || fileExt == ".flv" || fileExt == ".swf")
                        {
                            TempPath = Common.FileConfig.VideoPath;
                        }
                        else
                        {
                            TempPath = Common.FileConfig.TempPath;
                        }
                        while (true)
                        {
                            ImagesId = Common.PrimaryKey.GetHashCodeID.ToString();
                            //文件夹
                            Folder = ImagesId.Substring(0, 4) + "/";
                            //文件ID
                            FileId = ImagesId.Substring(4);
                            fileMinPath = Server.MapPath(TempPath + Folder + FileId + fileExt);
                            if (!System.IO.File.Exists(fileMinPath))
                            {
                                break;
                            }
                        }

                        //判断存储文件路径是否存在
                        if (!System.IO.Directory.Exists(this.Server.MapPath(TempPath + Folder)))
                        {
                            System.IO.Directory.CreateDirectory(this.Server.MapPath(TempPath + Folder));
                        }

                        if (fileExt == ".mp4" || fileExt == ".flv" || fileExt == ".swf")
                        {
                            file.SaveAs(fileMinPath);
                            var fileUrl = Url.Content(TempPath + Folder + FileId + fileExt);
                            hash["error"] = 0;
                            hash["url"] = fileUrl;
                        }
                        else
                        {
                            //保存原始图片
                            var fileBigPath = System.IO.Path.GetTempPath() + "BIG-" + ImagesId + fileExt;
                            file.SaveAs(fileBigPath);


                            Image pic = Image.FromFile(fileBigPath);
                            int intWidth = pic.Width;//长度像素值 
                            int intHeight = pic.Height;//高度像素值
                            pic.Dispose();
                            var picWidth = intWidth;
                            var picHeight = intHeight;
                            if (intWidth > 1000 || picHeight > 900)
                            {
                                //图片压缩
                                if (intWidth > intHeight)
                                {
                                    picWidth = 1000;
                                    picHeight = 600;
                                }
                                else
                                {
                                    picWidth = 1000;
                                    picHeight = 900;
                                }
                            }
                            new Common.FileHelper().MakeThumbnail(fileBigPath, fileMinPath, picWidth, picHeight, "W");
                            //删除原始图片
                            if (System.IO.File.Exists(fileBigPath))
                            {
                                System.IO.File.Delete(fileBigPath);
                            }
                            var fileUrl = Url.Content(TempPath + Folder + FileId + fileExt);
                            hash["error"] = 0;
                            hash["url"] = fileUrl;
                        }
                    }
                    else
                    {
                        hash["error"] = 1;
                        hash["message"] = string.Format("上传文件扩展名是不允许的扩展名{0}", fileTypes);
                    }
                }
                else
                {
                    hash["error"] = 1;
                    hash["message"] = "请选择文件";
                }
            }
            catch (Exception ex)
            {
                hash["error"] = 1;
                hash["message"] = "上传错误：" + ex.Message;
            }
            return Json(hash, "text/html", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 移动内容图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="randomNumber"></param>
        string MoveContentImage(string content, string FilePath)
        {
            string result = content;
            var contentList = Common.ToolHelper.GetHtmlImageUrlList(content);
            foreach (var item in contentList)
            {
                if (item.Contains("/Temp/"))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        //url虚拟路径处理
                        var path = item;
                        //原文件是否存在
                        FileInfo file = new FileInfo(Server.MapPath(path));
                        if (file.Exists)
                        {
                            //保存到新目录
                            var newFilePath = this.Server.MapPath(FilePath);
                            if (!System.IO.Directory.Exists(newFilePath))
                            {
                                //新目录不存在则创建
                                System.IO.Directory.CreateDirectory(newFilePath);
                            }
                            //将原文件移到新目录下
                            file.MoveTo(newFilePath + "\\" + file.Name);
                            string NewFileName = this.Url.Content(string.Format("{0}{1}", FilePath, file.Name));

                            result = result.Replace(path, NewFileName);
                        }
                    }
                }
            }
            return result;
        }
    }
}