using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult GetAreaItem(long? AreaId)
        {
            if (AreaId == null)
            {
                return Content("");
            }
            var AreaItem = UserControl.SelectItem.AreaItem(AreaId.Value, false, true);
            return Json(AreaItem);
        }
    }
}