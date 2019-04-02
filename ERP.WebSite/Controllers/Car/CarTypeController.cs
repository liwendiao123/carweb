using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers.Car
{
    public class CarTypeController : Controller
    {
        Provider.CarTypeProvider provider = new Provider.CarTypeProvider();
        // GET: CarBrand
        [Extension.UserAuthorize]
        public ActionResult Index()
        {
            var list = new Cache.CarTypeCache().Get(Common.FormsTicket.SystemCode);
            var model = new List<Models.CarTypeModels.CarTypeModel>();
            foreach (var item in list)
            {
                model.Add(new Models.CarTypeModels.CarTypeModel
                {
                    Id = item.Id,
                    TypeSort=item.TypeSort,
                    TypeName=item.TypeName
                });
            }
            return View(model);
        }

        [Extension.UserAuthorize]
        public ActionResult Create(long? Id)
        {
            var model = new Models.CarTypeModels.CarTypeModel();
            if (Id != null)
            {
                var cache = new Cache.CarTypeCache().Get(Common.FormsTicket.SystemCode).Where(c => c.Id == Id).FirstOrDefault();
                model = new Models.CarTypeModels.CarTypeModel
                {
                    Id = cache.Id,
                    TypeSort = cache.TypeSort,
                    TypeName = cache.TypeName
                };
            }
            return View(model);
        }
        [Extension.UserAuthorize(ActionName = "Create")]
        public ActionResult Save(Models.CarTypeModels.CarTypeModel model)
        {
            var group = new UCMS.Entitys.CarType()
            {
                Id = model.Id,
                TypeSort = model.TypeSort,
                TypeName = model.TypeName,
                IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                TimeStamp = DateTime.Now,
            };
            var line = provider.Edit(group);
            if (line > 0)
            {
                //删除缓存
                new Cache.CarTypeCache().Delete(Common.FormsTicket.SystemCode);
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
                    new Cache.CarTypeCache().Delete(Common.FormsTicket.SystemCode);
                }
            }
            return Json(new { d = line > 0 ? 1 : 0 });
        }
    }
}