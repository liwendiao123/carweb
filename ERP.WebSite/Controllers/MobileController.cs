using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace UCMS.WebSite.Controllers
{
    public class MobileController : Controller
    {
        Provider.CarInfoProvider provider = new Provider.CarInfoProvider();
        // GET: Mobile
        public ActionResult Index(string HopePrice, string UseYears, string CarType, string Search)
        {
            SetUser();
            #region 下拉
            var PriceItem = new List<SelectListItem>();
            PriceItem.Add(new SelectListItem { Text = "价格", Value = "0", Selected = string.IsNullOrEmpty(HopePrice) ? true : HopePrice == "0" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "5万以下", Value = "1", Selected = HopePrice == "1" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "5-10万", Value = "2", Selected = HopePrice == "2" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "10-15万", Value = "3", Selected = HopePrice == "3" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "15-25万", Value = "4", Selected = HopePrice == "4" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "25-40万", Value = "5", Selected = HopePrice == "5" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "40-50万", Value = "6", Selected = HopePrice == "6" ? true : false });
            PriceItem.Add(new SelectListItem { Text = "50万以上", Value = "7", Selected = HopePrice == "7" ? true : false });

            var YearItme = new List<SelectListItem>();
            YearItme.Add(new SelectListItem { Text = "车龄", Value = "0", Selected = string.IsNullOrEmpty(UseYears) ? true : UseYears == "0" ? true : false });
            YearItme.Add(new SelectListItem { Text = "1年内", Value = "1", Selected = UseYears == "1" ? true : false });
            YearItme.Add(new SelectListItem { Text = "1-2年", Value = "2", Selected = UseYears == "2" ? true : false });
            YearItme.Add(new SelectListItem { Text = "3-5年", Value = "3", Selected = UseYears == "3" ? true : false });
            YearItme.Add(new SelectListItem { Text = "5年以上", Value = "4", Selected = UseYears == "4" ? true : false });

            var TypeItem = CarTypeItem(Common.ToolHelper.ConvertToLong(CarType), true);
            #endregion
            ViewBag.PriceItem = PriceItem;
            ViewBag.YearItme = YearItme;
            ViewBag.TypeItem = TypeItem;
            var list = provider.GetList(Common.ToolHelper.ConvertToLong(CarType), Common.ToolHelper.ConvertToInt(UseYears), Common.ToolHelper.ConvertToInt(HopePrice), 1, Search).ToList();
            foreach (var item in list)
            {
                item.PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext);
            }
            ViewBag.Search = Search;
            return View(list);
        }
        public void SetUser()
        {
            var str = "";
            if (Common.FormsTicket.UserId > 0)
            {
                var u = new Provider.UserBasisProvider().GetUser(Common.FormsTicket.UserId);
                str = "<div><a style='color: red;margin-right: 10px;'>" + u.RealName + "</a><a href='" + Url.Content("~/Mobile/Logout") + "'><font>退出</font></a></div>";
            }
            else
            {
                str = "<div><a href='" + Url.Content("~/Mobile/Login") + "'><span>登录</span></a></div>";
            }
            ViewBag.User = str;

            //系统配置
            var title = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            ViewBag.Title = title.SystemName;

        }
        #region 下拉框

        /// <summary>
        /// 车型下拉框
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarTypeItem(long TypeId, bool IsShowAll)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "车型", Value = "", Selected = false });
            }
            var tb = new Cache.CarTypeCache().Get(Common.FormsTicket.SystemCode);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == TypeId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// 品牌下拉框
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarBrandItem(long BrandId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--品牌--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.CarBrandCache().Get(Common.FormsTicket.SystemCode).OrderBy(c => c.Initial);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Initial + " " + item.BrandName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == BrandId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// 车系下拉框
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarSeriesItem(long BrandId, long SeriesId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--车系--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.CarSeriesCache().Get(BrandId);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.SeriesName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == SeriesId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// 车型下拉信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetItem(long Id)
        {
            var SeriesItem = CarSeriesItem(Id, 0, false, true);
            return Json(SeriesItem);
        }
        #endregion
        public ActionResult GetList(FormCollection collection)
        {
            var CarType = Common.ToolHelper.ConvertToLong(collection["CarType"]);
            var Search = collection["Search"];
            var UseYears = Common.ToolHelper.ConvertToInt(collection["UseYears"]);
            var HopePrice = Common.ToolHelper.ConvertToInt(collection["HopePrice"]);
            var page = Common.ToolHelper.ConvertToInt(collection["page"]);
            var list = provider.GetList(CarType, UseYears, HopePrice, page, Search);
            var str = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                /*
                 * 
                            <li>
                                <a href="~/Home/carinfo?id=@item.Id">
                                    <table>
                                        <tr>
                                            <th><img src="@item.PhotoURL" /></th>
                                            <td style="width:62%;">
                                                <h1>车辆型号：@Html.Raw(item.BrandName + item.SeriesName + item.CarName)</h1><br /><span>@item.Odometer / @Html.Raw(item.LicenseTime.ToString("yyyy-M"))</span><br /><label>￥@Html.Raw(item.RetailPrice)万元</label>
                                            </td>
                                        </tr>
                                    </table>
                                </a>
                            </li>
                 */
                str.Append("<li>");
                str.Append("<a href='/Home/carinfo?id=" + item.Id + "'>");
                str.Append("<table>");
                str.Append("<tr>");
                str.Append("<th><img src=" + new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext) + " /></th>");
                str.Append("<td style='width:62%;'>");
                str.Append("<h1>车辆型号：" + item.BrandName + item.SeriesName + item.CarName + "</h1><br /><span>" + item.Odometer + " / " + item.LicenseTime.ToString("yyyy-M") + "</span><br /><label>￥" + item.RetailPrice + "万元</label>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("</a>");
                str.Append("</li>");
            }
            return Content(str.ToString());
        }

        public ActionResult CarInfo(long? Id)
        {
            SetUser();
            var model = new Models.MobileModels.CarInfoModel();
            if (Id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var item = provider.GetCarById(Id.Value);
                if (item.AuditStatus != (int)Common.EnumModel.EAuditStatus.Ok)
                {
                    return RedirectToAction("Index");
                }
                var pt = provider.GetCarPhotoById(item.Id);
                var photo = new List<Models.MobileModels.PhotoModel>();
                foreach (var p in pt)
                {
                    photo.Add(new Models.MobileModels.PhotoModel
                    {
                        Id = p.Id,
                        URL = new Common.FileHelper().GetFileUrl(p.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                    });
                }
                model = new Models.MobileModels.CarInfoModel
                {
                    Id = item.Id.ToString(),
                    CarNo = item.CarNo,
                    CreateTime = item.CreateTime,
                    SweptVolume = item.SweptVolume,
                    CarColor = item.CarColor,
                    CarName = item.CarName,
                    LicenseTime = item.LicenseTime,
                    Odometer = item.Odometer,
                    Remark = item.Remark,
                    RetailPrice = item.RetailPrice,
                    BrandName = item.BrandName,
                    SeriesName = item.SeriesName,
                    TypeName = item.TypeName,
                    IsRepay = item.IsRepay == 1 ? "(还价不多)" : "",
                    Transmission = Common.EnumHelper.GetEnumName<Common.EnumModel.ETransmission>(item.Transmission),
                    Fuel = Common.EnumHelper.GetEnumName<Common.EnumModel.EFuelType>(item.Fuel),
                    EmissionStandards = Common.EnumHelper.GetEnumName<Common.EnumModel.EEmissionStandards>(item.EmissionStandards),
                    ProductAddress = Common.EnumHelper.GetEnumName<Common.EnumModel.EProductAddress>(item.ProductAddress),
                    PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                    Photo = photo
                };
                if (Common.FormsTicket.UserId > 0)
                {
                    var u = new Provider.UserBasisProvider().GetUser(item.UserId);
                    model.Contact = "<span>QQ：" + u.QQ + "</span><span>微信：" + u.Weixin + "</span><span>电话：" + u.Mobile + "</span>";
                }
                else
                {
                    model.Contact = "<span>联系方式请登录查看</span>";
                }
            }
            return View(model);
        }

        #region 我的车源

        public ActionResult MyCar(string CarType, string Search)
        {
            SetUser();
            var userId = Common.FormsTicket.UserId;
            if (userId > 0)
            {
            }
            else
            {
                return RedirectToAction("Index");
            }
            var paging = new Common.PagingModels();

            var list = provider.GetList(paging, Common.ToolHelper.ConvertToLong(CarType), Search).ToList();
            foreach (var item in list)
            {
                item.PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext);
            }
            var TypeItem = CarTypeItem(Common.ToolHelper.ConvertToLong(CarType), true);
            ViewBag.TypeItem = TypeItem;
            ViewBag.Search = Search;
            return View(list);
        }
        public ActionResult GetMyList(FormCollection collection, Common.PagingModels paging, string Search)
        {
            var CarType = Common.ToolHelper.ConvertToLong(collection["CarType"]);
            if (CarType == Common.Constant.LONG_DEFAULT)
            {
                CarType = 0;
            }
            var list = provider.GetList(paging, CarType, Search);
            var str = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                str.Append("<li>");
                str.Append("<table>");
                str.Append("<tr>");
                str.Append("<th><img src=" + new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext) + " /></th>");
                str.Append("<td style='width:62%;'>");
                str.Append("<h1>车辆型号：" + item.BrandName + item.SeriesName + item.CarName + "</h1><br /><span>" + item.Odometer + " / " + item.LicenseTime.ToString("yyyy-M") + "</span><br /><label>￥" + item.RetailPrice + "万元</label>");
                if (item.AuditStatus == 0)
                {
                    str.Append("<a href='~/Home/Create?id=" + item.Id + "' style='display: inline-block;margin-left: 30px;'>修改</a>");
                }
                str.Append("<a href='~/Mobile/Delete?checkedId="+item.Id+"'>删除</a>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("</li>");
            }
            return Content(str.ToString());
        }

        public ActionResult Create(long? Id)
        {
            SetUser();
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
                if (item == null || item.AuditStatus != (int)Common.EnumModel.EAuditStatus.Normal)
                {
                    return RedirectToAction("MyCar");
                }
                var pt = provider.GetCarPhotoById(item.Id);
                var photo = new List<Models.CarInfoModels.PhotoModel>();
                foreach (var p in pt)
                {
                    photo.Add(new Models.CarInfoModels.PhotoModel
                    {
                        Id = p.Id,
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
                    IsRepay = item.IsRepay,
                    VehicleLicense = new Common.FileHelper().GetFileUrl(item.VehicleLicense, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    TestReport = new Common.FileHelper().GetFileUrl(item.TestReport, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                    PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                    Photo = photo
                };
                TypeItem = CarTypeItem(model.TypeId, true);
                BrandItem = CarBrandItem(model.BrandId, false, true);
                SeriesItem = CarSeriesItem(model.BrandId, model.SeriesId, false, true);
                FualItem = Common.EnumHelper.GetEnumItem<Common.EnumModel.EFuelType>(model.Fuel, false, false);
                EmissionStandards = Common.EnumHelper.GetEnumItem<Common.EnumModel.EEmissionStandards>(model.EmissionStandards, false, false);
            }
            else
            {
                model.LicenseTime = DateTime.Now;
                TypeItem = CarTypeItem(model.TypeId, true);
                BrandItem = CarBrandItem(model.BrandId, false, true);
                SeriesItem = CarSeriesItem(model.BrandId, model.SeriesId, false, true);
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

        public ActionResult Save(Models.CarInfoModels.CarInfoModel model, FormCollection conllection)
        {
            string ImgList = conllection["ImgList"];
            string ImgDelete = conllection["ImgDelete"];
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

            //保存行驶证图片
            var VehicleLicense = new Common.FileHelper().SaveImgRelative("VehicleLicense", "", Common.FileConfig.OtherPhotoPath);

            //车源主图
            var PhotoURL = new Common.FileHelper().SaveImgRelative("PhotoURL", "", Common.FileConfig.CarPhotoPath);

            //车子图片
            var temp = ImgList.Split(new char[] { ',' });
            var photoId = new List<string>();
            foreach (var item in temp)
            {
               // var pid = new Common.FileHelper().SaveImgRelative(item, "", Common.FileConfig.CarPhotoPath);
                if (!string.IsNullOrEmpty(item))
                {
                    photoId.Add(item);
                }
            }

            ///保存测试报告
            var TestReport = new Common.FileHelper().SaveImgRelative("TestReport", "", Common.FileConfig.OtherPhotoPath);

            #endregion
            var t = CarTypeItem(model.TypeId, false).Where(c => c.Value == model.TypeId.ToString()).FirstOrDefault();
            var s = CarSeriesItem(model.BrandId, model.SeriesId, false).Where(c => c.Value == model.SeriesId.ToString()).FirstOrDefault();
            var b = CarBrandItem(model.BrandId, false).Where(c => c.Value == model.BrandId.ToString()).FirstOrDefault();
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
                UserId = Common.FormsTicket.UserId,
                IsRepay = model.IsRepay,
                PhotoURL = PhotoURL
            };
            if (!string.IsNullOrEmpty(model.Id))
            {
                //修改
                carInfo.Id = Common.ToolHelper.ConvertToLong(model.Id);
                long oid = -1;
             long.TryParse(model.Id,out oid);

                var pt = provider.GetCarPhotoById(oid);

                var listDElID = pt.ToList();
                var strlistDElID = listDElID.Where(x => !photoId.Contains(x.PhotoURL)).Select(x=>x.Id).ToList();
                ImgDelete = string.Join(",", strlistDElID);
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
            var line = 1;
            try
            {

                provider.Edit(carInfo, pModel, string.IsNullOrEmpty(model.Id),ImgDelete);
            }
            catch (Exception ex)
            {
                line = 0;
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }

        public ActionResult Test()
        {
            return View();
        }
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
            return RedirectToAction("MyCar");
        }
        #endregion

        #region 审核车源

        public ActionResult AuditList()
        {
            if (Common.FormsTicket.UserType != 1)
            {
                return RedirectToAction("Index");
            }
            SetUser();
            var paging = new Common.PagingModels();
            var list= provider.GetAuditList(paging, "").ToList();
            foreach (var item in list)
            {
                item.PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext);
            }
            return View(list);
        }
        public ActionResult GetAuditList(Common.PagingModels paging)
        {
            var model = new List<Models.CarInfoModels.CarInfoModel>();
            var list = provider.GetAuditList(paging, "");
            var str = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                str.Append("<li>");
                str.Append("<table>");
                str.Append("<tr>");
                str.Append("<th><img src=" + new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext) + " /></th>");
                str.Append("<td style='width:62%;'>");
                str.Append("<h1>车辆型号：" + item.BrandName + item.SeriesName + item.CarName + "</h1><br /><span>" + item.Odometer + " / " + item.LicenseTime.ToString("yyyy-M") + "</span><br /><label>￥" + item.RetailPrice + "万元</label>");
                str.Append("<a href='~/Mobile/AuditInfo?id=" + item.Id + "'>删除</a>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("</li>");
            }
            return Content(str.ToString());
        }

        public ActionResult AuditInfo(long? Id)
        {
            //TODO:如果已审核不给修改
            var model = new Models.CarInfoModels.CarInfoModel();
            if (Id == null)
            {
                return RedirectToAction("AuditList");
            }
            var item = provider.GetCarById(Id.Value);
            if (item.AuditStatus != (int)Common.EnumModel.EAuditStatus.Normal)
            {
                return RedirectToAction("AuditList");
            }
            var pt = provider.GetCarPhotoById(item.Id);
            var photo = new List<Models.CarInfoModels.PhotoModel>();
            foreach (var p in pt)
            {
                photo.Add(new Models.CarInfoModels.PhotoModel
                {
                    Id = p.Id,
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
                BrandName = item.BrandName,
                SeriesName = item.SeriesName,
                TypeName = item.TypeName,
                VehicleLicense = new Common.FileHelper().GetFileUrl(item.VehicleLicense, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                TestReport = new Common.FileHelper().GetFileUrl(item.TestReport, Common.FileConfig.OtherPhotoPath, this.HttpContext),
                PhotoURL = new Common.FileHelper().GetFileUrl(item.PhotoURL, Common.FileConfig.CarPhotoPath, this.HttpContext),
                Photo = photo
            };
            return View(model);
        }
        public ActionResult Audit(long Id, byte Status, string AuditExplan)
        {
            var model = new Entitys.CarInfo()
            {
                Id = Id,
                AuditExplan = AuditExplan,
                AuditPerson = Common.FormsTicket.UserId,
                AuditStatus = Status,
                AuditTime = DateTime.Now,
            };
            var line = provider.Audit(model);
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        #endregion

        #region 登录

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string loginCode, string password, string code)
        {
            var cache = new Cache.SysSettingCache().Get(Common.FormsTicket.SystemCode);
            if (cache != null && cache.Id > 0)
            {
                if (cache.IsEnable == 1)
                {
                    if (!loginCode.Contains("xw"))
                    {
                        return Content("999");
                    }
                    else
                    {
                        loginCode = loginCode.Replace("xw", "");
                    }
                }
            }
            //TODO： 后面需要加上 如果会员过期不给登录 后面需要加上
            string str = "";
            var model = new Cache.AccountCodeCache().Get(Common.ToolHelper.ConvertToInt(loginCode));
            if (model != null)
            {
                password = Common.ToolHelper.GetMD5Hash32(password);
                if (model.Passwords == password)
                {
                    //添加访问记录
                    var entity = new Entitys.SysLoginLog
                    {
                        TimeStamp = DateTime.Now,
                        LoginCode = loginCode.ToString(),
                        LoginStatus = 1,
                        LoginIP = Common.ToolHelper.GetClientIP,
                        LoginType = 0,
                    };
                    var db = new UCMS.Entitys.UCMSContext();
                    db.SysLoginLog.Add(entity);
                    db.SaveChanges();
                    //添加票据
                    var ticket = new Common.FormsTicket();
                    ticket.AuthenticationTicket(model.UserId, model.RealName, model.UserType, model.LoginCode);
                    str = "1";
                }
                else
                {
                    str = "0";
                }
            }
            else
            {
                str = "0";
            }
            return Content(str);
        }
        public ActionResult Logout(string ID)
        {
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            ViewBag.ActionTpye = ID;
            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// 通知公告
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Notice(long? Id)
        {
            SetUser();
            var list = new Provider.SysNoticeProvider().GetList();
            var model = new List<Models.SysNoticeModels.SysNoticeModel>();
            long nId = 0;
            foreach (var item in list)
            {
                model.Add(new Models.SysNoticeModels.SysNoticeModel
                {
                    Id = item.Id,
                    CreateTime = item.CreateTime,
                    Content = item.Content,
                    NoticeType = item.NoticeType,
                    Title = item.Title
                });
                if (item.NoticeType == 1)
                {
                    nId = item.Id;
                }
            }
            if (Id == null)
            {
                Id = nId;
            }
            var pro = new Provider.SysNoticeProvider().GetNotice(Id.Value);
            var str = "";
            if (pro == null)
            {
                str = "<h3 style='text-align: center;'>通知公告</h3><p></p>";
            }
            else
            {
                str = "<h3 style='text-align: center;'>" + pro.Title + "</h3><p>" + pro.Content + "</p>";
            }
            ViewBag.Content = str;
            return View(model);
        }
    }
}