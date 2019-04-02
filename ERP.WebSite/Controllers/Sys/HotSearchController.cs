using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Sys
{
    public class HotSearchController : Controller
    {
        Provider.SysHotSearchProvider provider = new Provider.SysHotSearchProvider();
        // GET: HotSearch
        [Extension.UserAuthorize]
        public ActionResult Index(long? BrandId)
        {
            var brandItem = UserControl.SelectItem.CarBrandItem(BrandId == null ? 0 : BrandId.Value, false);
            string brandName = "";
            if (BrandId == null && brandItem.Count > 0)
            {
                BrandId = Common.ToolHelper.ConvertToLong(brandItem.FirstOrDefault().Value);
                brandName = brandItem.FirstOrDefault().Text;
            }
            else if (BrandId == null)
            {
                BrandId = 0;
            }
            else
            {
                brandName = brandItem.FirstOrDefault(c=>c.Selected).Text;
            }
            var list = new List<Models.HotSearchModels.CarBrand>();
            var hc = provider.GetList();
            var info = new List<Models.HotSearchModels.CarSeries>();
            info = new List<Models.HotSearchModels.CarSeries>();
            foreach (var item in new Cache.CarSeriesCache().Get(BrandId.Value))
            {
                info.Add(new Models.HotSearchModels.CarSeries
                {
                    SeriesId = item.Id,
                    SeriesName = item.SeriesName,
                    IsSelect = hc.FirstOrDefault(c => c.SearchId == item.Id) != null
                });
            }
            list.Add(new Models.HotSearchModels.CarBrand
            {
                BrandId = BrandId.Value,
                BrandhName = brandName,
                IsSelect = hc.FirstOrDefault(c => c.SearchId == BrandId) != null,
                info = info
            });
            ViewBag.BrandItem = brandItem;
            return View(list);
        }
        [Extension.UserAuthorize(ActionName = "Index")]
        public ActionResult Save(long BrandId,long SeriesId)
        {
            var line=provider.Edit(BrandId, SeriesId);
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}