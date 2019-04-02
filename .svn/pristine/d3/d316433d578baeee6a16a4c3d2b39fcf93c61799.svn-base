using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Car
{
    public class CarSeriesController : Controller
    {
        Provider.CarSeriesProvider provider = new Provider.CarSeriesProvider();
        // GET: CarBrand
        [Extension.UserAuthorize]
        public ActionResult Index(long? BrandId)
        {
            var brandItem = UserControl.SelectItem.CarBrandItem(BrandId==null?0:BrandId.Value, false);
            if (BrandId == null && brandItem.Count > 0)
            {
                BrandId = Common.ToolHelper.ConvertToLong(brandItem.FirstOrDefault().Value);
            }
            else if(BrandId == null)
            {
                BrandId = 0;
            }
            var list = new Cache.CarSeriesCache().Get(BrandId.Value);
            var model = new List<Models.CarSeriesModels.CarSeriesModel>();
            foreach (var item in list)
            {
                model.Add(new Models.CarSeriesModels.CarSeriesModel
                {
                    Id = item.Id,
                    SeriesName=item.SeriesName,
                    SeriesSort=item.SeriesSort
                });
            }
            ViewBag.BrandItem = brandItem;
            return View(model);
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? Id,long BrandId)
        {
            var typeItem = new List<SelectListItem>();
            var model = new Models.CarSeriesModels.CarSeriesModel();
            if (Id != null)
            {
                var item = new Cache.CarSeriesCache().Get(BrandId).Where(c => c.Id == Id).FirstOrDefault();
                typeItem = UserControl.SelectItem.CarTypeItem(item.TypeId, false, true);
                model = new Models.CarSeriesModels.CarSeriesModel
                {
                    Id = item.Id,
                    SeriesName = item.SeriesName,
                    SeriesSort = item.SeriesSort,
                    BrandId=item.BrandId,
                    TypeId=item.TypeId
                };
            }
            else
            {
                typeItem = UserControl.SelectItem.CarTypeItem(0, false,true);
                model.BrandId = BrandId;
            }
            ViewBag.TypeItem = typeItem;
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.CarSeriesModels.CarSeriesModel model)
        {
            var group = new UCMS.Entitys.CarSeries()
            {
                Id = model.Id,
                SeriesName = model.SeriesName,
                SeriesSort = model.SeriesSort,
                BrandId=model.BrandId,
                TypeId=model.TypeId,
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(group);
            if (line > 0)
            {
                //删除缓存
                new Cache.CarSeriesCache().Delete(model.BrandId);
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="checkedId"></param>
        /// <returns></returns>
        [Extension.UserAuthorize]
        public ActionResult Delete(string checkedId,long BrandId)
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
                if (line > 0)
                {
                    //删除缓存
                    new Cache.CarSeriesCache().Delete(BrandId);
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}