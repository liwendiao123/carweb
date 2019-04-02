using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Car
{
    public class CarBrandController : Controller
    {
        Provider.CarBrandProvider provider = new Provider.CarBrandProvider();
        // GET: CarBrand
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [Extension.UserAuthorize(ActionName = "Index")]
        public ActionResult GetList(Common.PagingModels paging, string Search)
        {
            var model = new List<Models.CarBrandModels.CarBrandModel>();
            var list = provider.GetList(paging, Search);
            foreach (var item in list)
            {
                model.Add(new Models.CarBrandModels.CarBrandModel
                {
                    Id = item.Id.ToString(),
                    BrandSort=item.BrandSort,
                    BrandName=item.BrandName,
                    Initial=item.Initial
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
            var model = new Models.CarBrandModels.CarBrandModel();
            if (Id != null)
            {
                var cache = new Cache.CarBrandCache().Get(Common.FormsTicket.SystemCode).Where(c => c.Id == Id).FirstOrDefault();
                model = new Models.CarBrandModels.CarBrandModel
                {
                    Id=cache.Id.ToString(),
                    BrandSort=cache.BrandSort,
                    BrandName=cache.BrandName
                };
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.CarBrandModels.CarBrandModel model)
        {
            var group = new UCMS.Entitys.CarBrand()
            {
                Id =Common.ToolHelper.ConvertToLong(model.Id),
                BrandSort = model.BrandSort,
                BrandName = model.BrandName,
                Initial= Common.SpellHelper.GetSpellCode(model.BrandName.Trim()),
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(group);
            if (line > 0)
            {
                //删除缓存
                new Cache.CarBrandCache().Delete(Common.FormsTicket.SystemCode);
            }
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
                if (line > 0)
                {
                    //删除缓存
                    new Cache.CarBrandCache().Delete(Common.FormsTicket.SystemCode);
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}