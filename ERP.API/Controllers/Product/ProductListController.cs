using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Product
{
    public class ProductListController : BaseController
    {
        // GET: ProductList
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="index"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string categoryid, string search, int sort = 0,int index=1,int row=5)
        {
            var data = new Models.ProductListModels.json_model();
            try
            {
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ProductListModels.result_model>();
                var list = new Provider.ProductBasisProvider().GetList(categoryid, search, sort, index, row);
                var pic = "";
                foreach (var item in list)
                {
                    pic = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL, Common.FileConfig.FileType.ProductPhoto.ToString());
                    model.Add(new Models.ProductListModels.result_model
                    {
                        pictureurl= pic,
                        salecount=item.SaleCount,
                        productid=item.Id,
                        productname=item.ProductName,
                        promoprice=item.PromoPrice,
                        realprice=item.RealPrice,
                        review=item.Review
                    });
                }
                data.response = (int)Extensions.ErrorInfo.OK;
                data.message = "成功";
                data.result = model;
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(ProductCategoryController), ex);
            }
            return Json(data);
        }
    }
}