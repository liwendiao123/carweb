using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Product
{
    public class ProductCategoryController : BaseController
    {
        // GET: ProductCategory
        /// <summary>
        /// 获取分类  默认只取第一个分类下的子类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index()
        {
            var data = new Models.ProductCategoryModels.json_model();
            try
            {
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ProductCategoryModels.result_model>();
                var cache = new Cache.Prod_ProductCategoryCache();
                bool b = true;
                var list = new List<Models.ProductCategoryModels.result_model>();
                foreach (var item in cache.Get(Common.Constant.LONG_DEFAULT))
                {
                    if (b)
                    {
                        b = false;
                        var child = new List<Models.ProductCategoryModels.result_model>();
                        foreach (var c1 in cache.Get(item.Id))
                        {
                            foreach (var c2 in cache.Get(c1.Id))
                            {
                                child.Add(new Models.ProductCategoryModels.result_model
                                {
                                    categoryid = item.Id,
                                    categoryname = item.CategoryName,
                                    parentid = item.ParentId,
                                });
                            }
                            list.Add(new Models.ProductCategoryModels.result_model
                            {
                                categoryid = item.Id,
                                categoryname = item.CategoryName,
                                parentid = item.ParentId,
                                list = child
                            });
                        }
                    }
                    model.Add(new Models.ProductCategoryModels.result_model
                    {
                        categoryid = item.Id,
                        categoryname = item.CategoryName,
                        parentid = item.ParentId,
                        list=list
                    });
                }
                data.response = (int)Extensions.ErrorInfo.OK;
                data.message = "";
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
        /// <summary>
        /// 根据分类id获取子类
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCategory(string categoryid)
        {
            var data = new Models.ProductCategoryModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(categoryid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    dic.Add("categoryid", Request["categoryid"]);
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ProductCategoryModels.result_model>();
                var list = new List<Models.ProductCategoryModels.result_model>();
                var cache = new Cache.Prod_ProductCategoryCache();
                foreach (var item in cache.Get(Common.ToolHelper.ConvertToLong(categoryid)))
                {
                    list = new List<Models.ProductCategoryModels.result_model>();
                    foreach (var c in cache.Get(item.Id))
                    {
                        list.Add(new Models.ProductCategoryModels.result_model
                        {
                            categoryid = item.Id,
                            categoryname = item.CategoryName,
                            parentid = item.ParentId
                        });

                    }
                    model.Add(new Models.ProductCategoryModels.result_model
                    {
                        categoryid = item.Id,
                        categoryname = item.CategoryName,
                        parentid = item.ParentId,
                        list= list
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