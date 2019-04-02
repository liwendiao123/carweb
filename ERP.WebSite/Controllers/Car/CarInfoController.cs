using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Car
{
    public class CarInfoController : Controller
    {
        Provider.CarInfoProvider provider = new Provider.CarInfoProvider();
        // GET: CarInfo
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var TypeItem = UserControl.SelectItem.CarTypeItem(0 , true);
            ViewBag.TypeItem = TypeItem;
            return View();
        }
        [Extension.UserAuthorize(ActionName = "Index")]
        public ActionResult GetList(Common.PagingModels paging,long? TypeId,string Search)
        {
            if(TypeId==Common.Constant.LONG_DEFAULT)
            {
                TypeId = null;
            }
            long userId = 0;
            if(Common.FormsTicket.UserType==1)
            {
            }
            else
            {
                //不是超级管理员只能看自己数据
                userId = Common.FormsTicket.UserId;
            }
            var model = new List<Models.CarInfoModels.CarInfoModel>();
            var list = provider.GetList(paging, userId, TypeId, Search);
            foreach (var item in list)
            {
                model.Add(new Models.CarInfoModels.CarInfoModel
                {
                    Id = item.Id.ToString(),
                    SeriesName = item.SeriesName,
                    CarNo=item.CarNo,
                    TypeName=item.TypeName,
                    BrandName=item.BrandName,
                    RetailPrice=item.RetailPrice,
                    LicenseTime=item.LicenseTime,
                    AuditStatus=item.AuditStatus,
                    Remark=item.Remark,
                    CreateTime=item.CreateTime,
                    CarName=item.CarName,
                    CarStatus=item.CarStatus,
                });
            }
            var json = new
            {
                Data = model,
                RowCount = paging.totalrows
            };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(json));
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? Id)
        {
            var TypeItem = new List<SelectListItem>();
            var BrandItem = new List<SelectListItem>();
            var SeriesItem = new List<SelectListItem>();
            var FualItem = new List<SelectListItem>();
            var EmissionStandards = new List<SelectListItem>();
            //TODO:如果已审核不给修改
            var model = new Models.CarInfoModels.CarInfoModel();
            if (Id != null)
            {
                var item = provider.GetCarById(Id.Value);
                if (item.AuditStatus != (int)Common.EnumModel.EAuditStatus.Normal)
                {
                    return Content("不能编辑此数据！");
                }
                var pt = provider.GetCarPhotoById(item.Id);
                var photo = new List<Models.CarInfoModels.PhotoModel>();
                foreach (var p in pt)
                {
                    photo.Add(new Models.CarInfoModels.PhotoModel {
                        Id=p.Id,
                        URL = new Common.FileHelper().GetFileUrl(p.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                    });
                }
                model = new Models.CarInfoModels.CarInfoModel
                {
                    Id = item.Id.ToString(),
                    SeriesId = item.SeriesId,
                    SweptVolume = item.SweptVolume,
                    ProductAddress = item.ProductAddress,
                    BrandId = item.BrandId,
                    CarColor = item.CarColor,
                    CarName = item.CarName,
                    EmissionStandards = item.EmissionStandards,
                    Fuel = item.Fuel,
                    LicenseTime = item.LicenseTime,
                    Odometer = item.Odometer,
                    Remark = item.Remark,
                    RetailPrice = item.RetailPrice,
                    Transmission = item.Transmission,
                    TypeId = item.TypeId,
                    VIN = item.VIN,
                    IsRepay=item.IsRepay,
                    VehicleLicense = new Common.FileHelper().GetFileUrl(item.VehicleLicense, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    TestReport = new Common.FileHelper().GetFileUrl(item.TestReport, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                    Photo = photo
                };
                TypeItem = UserControl.SelectItem.CarTypeItem(model.TypeId,false, true);
                BrandItem = UserControl.SelectItem.CarBrandItem(model.BrandId, false, true);
                SeriesItem = UserControl.SelectItem.CarSeriesItem(model.BrandId,model.SeriesId, false,true);
                FualItem = Common.EnumHelper.GetEnumItem<Common.EnumModel.EFuelType>(model.Fuel, false, false);
                EmissionStandards = Common.EnumHelper.GetEnumItem<Common.EnumModel.EEmissionStandards>(model.EmissionStandards, false, false);
            }
            else
            {
                model.LicenseTime = DateTime.Now;
                TypeItem = UserControl.SelectItem.CarTypeItem(model.TypeId, false, true);
                BrandItem = UserControl.SelectItem.CarBrandItem(model.BrandId, false, true);
                SeriesItem = UserControl.SelectItem.CarSeriesItem(model.BrandId, model.SeriesId, false, true);
                FualItem = Common.EnumHelper.GetEnumItem<Common.EnumModel.EFuelType>(0, false, false);
                EmissionStandards = Common.EnumHelper.GetEnumItem<Common.EnumModel.EEmissionStandards>(0, false, false);
            }
            ViewBag.TypeItem = TypeItem;
            ViewBag.BrandItem = BrandItem;
            ViewBag.SeriesItem = SeriesItem;
            ViewBag.FualItem = FualItem;
            ViewBag.EmissionStandards = EmissionStandards;
            return View(model);
        }
        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult SoldOut(long Id,int Status)
        {
            var line = provider.SoldOut(Id, Status == 1 ? Common.EnumModel.ECarStatus.Ok : Common.EnumModel.ECarStatus.Not);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 车型下拉信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetItem(long Id)
        {
            var SeriesItem = UserControl.SelectItem.CarSeriesItem(Id, 0, false, true);
            return Json(SeriesItem);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        [ValidateInput(false)]
        public ActionResult Save(Models.CarInfoModels.CarInfoModel model, string ImgList, string ImgDelete)
        {
            //TODO:如果已审核不给修改
            if (!string.IsNullOrEmpty(model.Id))
            {
                var car = provider.GetCarById(Common.ToolHelper.ConvertToLong(model.Id));
                if (car.AuditStatus != (int)Common.EnumModel.EAuditStatus.Normal)
                {
                    return Json(new { d = -1 });
                }
            }
            #region 图片处理
            var VehicleLicense = new Common.FileHelper().SaveImgRelative("VehicleLicense", "", Common.FileConfig.OtherPhotoPath);
            var TestReport = new Common.FileHelper().SaveImgRelative("TestReport", "", Common.FileConfig.OtherPhotoPath);
            var temp = ImgList.Split(new char[] { ',' });
            var photoId = new List<string>();
            foreach (var item in temp)
            {
                var pid = new Common.FileHelper().SaveImgRelative(item, "", Common.FileConfig.CarPhotoPath);
                if (!string.IsNullOrEmpty(pid))
                {
                    photoId.Add(pid);
                }
            }
            //主图
            var PhotoURL = new Common.FileHelper().SaveImgRelative("PhotoURL", "", Common.FileConfig.CarPhotoPath);

            var random = Common.PrimaryKey.GetHashCodeID.ToString();
            string FilePath = Common.FileConfig.TempPath + random.Substring(0, 4) + "/" + random.Substring(4) + "/";
            model.Remark=model.Remark == null ? model.Remark = "" : model.Remark;
            model.Remark = MoveContentImage(model.Remark, FilePath);
            #endregion
            var t = UserControl.SelectItem.CarTypeItem(model.TypeId, false).Where(c => c.Value == model.TypeId.ToString()).FirstOrDefault();
            var s = UserControl.SelectItem.CarSeriesItem(model.BrandId, model.SeriesId, false).Where(c => c.Value == model.SeriesId.ToString()).FirstOrDefault();
            var b = UserControl.SelectItem.CarBrandItem(model.BrandId, false).Where(c => c.Value == model.BrandId.ToString()).FirstOrDefault();
            var carInfo = new Entitys.CarInfo()
            {
                SeriesId = model.SeriesId,
                SweptVolume = model.SweptVolume,
                ProductAddress = model.ProductAddress,
                BrandId = model.BrandId,
                CarColor = model.CarColor == null ? "" : model.CarColor,
                CarName = model.CarName == null ? "" : model.CarName,
                EmissionStandards = model.EmissionStandards,
                Fuel = model.Fuel,
                LicenseTime = model.LicenseTime,
                Odometer = model.Odometer,
                Remark = model.Remark == null ? "" : model.Remark,
                RetailPrice = model.RetailPrice,
                TestReport = TestReport,
                Transmission = model.Transmission,
                TypeId = model.TypeId,
                VehicleLicense = VehicleLicense,
                VIN = model.VIN == null ? "" : model.VIN,
                TypeName = t == null ? "" : t.Text,
                BrandName = b == null ? "" : b.Text.Substring(1, b.Text.Length - 1),
                SeriesName = s == null ? "" : s.Text,
                UserId=Common.FormsTicket.UserId,
                IsRepay=model.IsRepay,
                PhotoURL=PhotoURL,
            };
            if (!string.IsNullOrEmpty(model.Id))
            {
                //修改
                carInfo.Id = Common.ToolHelper.ConvertToLong(model.Id);
            }
            else
            {
                //新增
                carInfo.Id = Common.PrimaryKey.GetHashCodeID;
                carInfo.AuditStatus = (byte)Common.EnumModel.EAuditStatus.Normal;
                carInfo.AuditExplan = "";
                carInfo.AuditPerson = Common.Constant.LONG_DEFAULT;
                carInfo.AuditTime = DateTime.Now;
                carInfo.CarNo = Common.FormsTicket.CarNo;
                carInfo.CarStatus = model.CarStatus;
                carInfo.CreateTime = DateTime.Now;
                carInfo.IsDelete = (byte)Common.EnumModel.EIsDelete.NotDelete;
                carInfo.TimeStamp = DateTime.Now;
            }
            var pModel = new List<Entitys.CarPhoto>();
            foreach (var item in photoId)
            {
                pModel.Add(new Entitys.CarPhoto
                {
                    Id = Common.PrimaryKey.GetHashCodeID,
                    CarId = carInfo.Id,
                    PhotoStatus = 0,
                    PhotoType = 0,
                    PhotoURL = item,
                    IsDelete = (byte)Common.EnumModel.EIsDelete.NotDelete,
                    TimeStamp = DateTime.Now,
                });
            }
            var line = provider.Edit(carInfo, pModel, string.IsNullOrEmpty(model.Id), ImgDelete);
            return Json(new { d = line > 0 ? 1 : 0 });
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


                            var pic = System.Drawing.Image.FromFile(fileBigPath);
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