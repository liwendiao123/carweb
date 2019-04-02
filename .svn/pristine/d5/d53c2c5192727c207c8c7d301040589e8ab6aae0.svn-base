using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Product
{
    public class ProductInfoController : BaseController
    {
        // GET: ProductInfo
        /// <summary>
        /// 商品详情页
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string productid)
        {
            var data = new Models.ProductInfoModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(productid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    dic.Add("productid", productid);
                    VerifyAuthorize(dic);
                }
                var product = new Models.ProductInfoModels.product_model();//商品信息
                var skuModel = new List<Models.ProductInfoModels.sku_model>();//sku
                var propertity = new List<Models.ProductInfoModels.property>();//商品属性
                var proveder = new Provider.ProductBasisProvider();
                var prod = proveder.GetProduct(Common.ToolHelper.ConvertToLong(productid));
                var shop = new Cache.BIZ_ShopBasisCache().Get(Common.FormsTicket.SystemCode).Where(c => c.Id == prod.ShopId).FirstOrDefault();
                if (prod.IsMoveProp == (int)Common.EnumModel.EIsMoveProp.OneProp)
                {
                    product = new Models.ProductInfoModels.product_model
                    {
                        productid = prod.Id,
                        salecount = prod.SaleCount,
                        productname = prod.ProductName,
                        promoprice = prod.PromoPrice,
                        realprice = prod.RealPrice,
                        review = prod.Review,
                        ismoveprop=prod.IsMoveProp,
                        stock=prod.Stock,
                        shopid=prod.ShopId,
                        shopname=shop.ShopName
                    };
                }
                else
                {
                    product = new Models.ProductInfoModels.product_model
                    {
                        productid = prod.Id,
                        salecount = prod.SaleCount,
                        productname = prod.ProductName,
                        promoprice = prod.PromoPrice,
                        realprice = prod.RealPrice,
                        review = prod.Review,
                        ismoveprop = prod.IsMoveProp,
                        stock = prod.Stock,
                        shopid = prod.ShopId,
                        shopname = shop.ShopName
                    };
                    #region sku信息
                    var sku = new Provider.ProductSKUProvider().GetSKUByProd(Common.ToolHelper.ConvertToLong(productid));
                    var spic = "";
                    foreach (var item in sku)
                    {
                        spic = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL, Common.FileConfig.FileType.ProductPhoto.ToString());
                        skuModel.Add(new Models.ProductInfoModels.sku_model
                        {
                            salecount = item.SaleCount,
                            skuid = item.Id,
                            stock = item.Stock,
                            promoprice = item.PromoPrice,
                            realprice = item.RealPrice,
                            skustring = item.SKUString.Trim('-').Replace("-", ","),
                            pictureurl = spic
                        });
                    }

                    #endregion
                    #region 商品属性
                    var propValue = new List<Models.ProductInfoModels.property_value>();
                    var prop = new HashSet<int>();//属性集合
                    var value = new HashSet<int>();//属性值集合
                    var ps = prod.PropString.Split('|');
                    foreach (var item in ps[0].Split(','))
                    {
                        prop.Add(Common.ToolHelper.ConvertToInt(item));
                    }
                    foreach (var item in ps[1].Split(','))
                    {
                        value.Add(Common.ToolHelper.ConvertToInt(item));
                    }
                    var props = new Provider.ProductPropertyProvider().GetListByIds(prop);
                    var values = new Provider.ProductPropertyProvider().GetValueByIds(prop);
                    foreach (var item in props)
                    {
                        propValue = new List<Models.ProductInfoModels.property_value>();
                        foreach (var v in values.Where(c => c.PropertyId == item.Id))
                        {
                            propValue.Add(new Models.ProductInfoModels.property_value
                            {
                                id = v.Id,
                                name = v.Name
                            });
                        }
                        propertity.Add(new Models.ProductInfoModels.property
                        {
                            id = item.Id,
                            name = item.PropName,
                            values = propValue
                        });
                    }
                    #endregion
                }
                var picModel = new List<Models.ProductInfoModels.pic_model>();
                #region 商品图片
                var pics = proveder.GetPic(Common.ToolHelper.ConvertToLong(productid));
                var pic = "";
                foreach (var item in pics)
                {
                    pic = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL, Common.FileConfig.FileType.ProductPhoto.ToString());
                    picModel.Add(new Models.ProductInfoModels.pic_model
                    {
                        pictureurl = pic
                    });
                }
                #endregion

                var model = new Models.ProductInfoModels.result_model() {
                    picture=picModel,
                    sku=skuModel,
                    product=product,
                    property=propertity
                };
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
        /// <summary>
        /// 商品介绍
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProdDetaile(string productid)
        {
            var response = 0;
            var message = "";
            var result = "";
            try
            {
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    dic.Add("productid", productid);
                    VerifyAuthorize(dic);
                }
                var info = new Provider.ProductBasisProvider().GetInfo(Common.ToolHelper.ConvertToLong(productid));
                if (info != null)
                {
                    response = (int)Extensions.ErrorInfo.OK;
                    message = "成功";
                    result = info.ProductDetail;
                }
                else
                {
                    response = 0;
                    message = "请求失败";
                }

            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(ProductCategoryController), ex);
            }
            return Json(new { response = response, message = message, result = result });
        }
        /// <summary>
        /// 商品评价  其他以后在加
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ActionResult ProdReview(string productid, int index = 1, int row = 5)
        {
            var data = new Models.ProductInfoModels.review_json();
            try
            {
                if (string.IsNullOrEmpty(productid))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    dic.Add("productid", productid);
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ProductInfoModels.review_model>();
                var review = new Provider.ProductReviewProvider().GetList(Common.ToolHelper.ConvertToLong(productid), index, row);
                foreach (var item in review)
                {
                    model.Add(new Models.ProductInfoModels.review_model
                    {
                        id = item.Id,
                        skuname = item.SKUString,
                        appendtime = item.AppendTime,
                        createtime = item.CreateTime,
                        isappend = item.IsAppend,
                        ispicture = item.IsPicture,
                        membername = item.MemberName,
                        pictureurl1 = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL1, Common.FileConfig.FileType.ProductPhoto.ToString()),
                        pictureurl2 = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL2, Common.FileConfig.FileType.ProductPhoto.ToString()),
                        pictureurl3 = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL3, Common.FileConfig.FileType.ProductPhoto.ToString()),
                        pictureurl4 = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL4, Common.FileConfig.FileType.ProductPhoto.ToString()),
                        pictureurl5 = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL5, Common.FileConfig.FileType.ProductPhoto.ToString()),
                        replytime = item.ReplyTime,
                        reviewappend = item.ReviewAppend,
                        reviewlevel = item.ReviewLevel,
                        reviewpremiere = item.ReviewPremiere,
                        reviewreply = item.ReviewReply
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