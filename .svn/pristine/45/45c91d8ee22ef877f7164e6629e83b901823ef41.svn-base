using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Order
{
    public class TempOrderController : BaseController
    {
        // GET: TempOrder
        /// <summary>
        /// 创建临时订单  单个购买
        /// </summary>
        /// <param name="token"></param>
        /// <param name="memberid">会员id</param>
        /// <param name="productid">商品id</param>
        /// <param name="skuid"></param>
        /// <param name="quantity">数量</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TempOrder(string token, string memberid, string productid, int quantity, string skuid = "")
        {
            var data = new Models.TempOrderModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(productid) || quantity==0)
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                var tc = new Cache.MemberTokenCache().Get(token);
                if (tc == null)
                {
                    return Json(new { response = Extensions.ErrorInfo.TokenError, message = "token验证失败请重新登录" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", Request["timestamp"]);
                    dic.Add("memberid", memberid);
                    dic.Add("token", token);
                    dic.Add("productid", productid);
                    dic.Add("quantity", quantity.ToString());
                    VerifyAuthorize(dic);
                }

                #region 业务处理

                var pro = new Provider.ProductBasisProvider().GetProduct(Common.ToolHelper.ConvertToLong(productid));
                if (pro != null)
                {
                    decimal price = 0;//有优惠需要计算优惠后的价格
                    string pic = "";
                    string skuName = "";
                    if (!string.IsNullOrEmpty(skuid))
                    {
                        var sku = new Provider.ProductSKUProvider().GetSKU(Common.ToolHelper.ConvertToLong(skuid));
                        if (sku != null)
                        {
                            price = sku.PromoPrice;
                            pic = sku.PictureURL;
                            skuName = sku.SKUName;
                        }
                    }
                    else
                    {
                        price = pro.PromoPrice;
                        pic = pro.PictureURL;
                    }
                    var entity = new Entitys.Order_TempOrderBasis()
                    {
                        OrderId= Common.PrimaryKey.GetHashCodeID,
                        ShopId = pro.ShopId,
                        OrderPrice = price,
                        MemberId = Common.ToolHelper.ConvertToLong(memberid),
                        CreateTime = DateTime.Now,
                        IsCart=0
                    };
                    var detail = new List<Entitys.Order_TempOrderDetail>();
                    var info = new Entitys.Order_TempOrderDetail()
                    {
                        SKUId = Common.ToolHelper.ConvertToLong(skuid),
                        SKUName = skuName,
                        IsGift = 0,
                        OrderId = entity.OrderId,
                        PayPrice = price,
                        PictureURL = pic,
                        ProductId = Common.ToolHelper.ConvertToLong(productid),
                        Quantity = quantity,
                        ProductName = pro.ProductName,
                        ProductPrice = pro.RealPrice,
                        CartId=Common.Constant.LONG_DEFAULT,
                        CreateTime=DateTime.Now
                    };
                    detail.Add(info);
                    var line = new Provider.TempOrderProvider().AddTempOrder(entity, detail);

                    if (line > 0)
                    {
                        var dinfo = new List<Models.TempOrderModels.detail>();
                        foreach (var item in detail)
                        {
                            dinfo.Add(new Models.TempOrderModels.detail
                            {
                                skuid = item.SKUId,
                                skuname = item.SKUName,
                                isgift = item.IsGift,
                                orderid = item.OrderId,
                                payprice = item.PayPrice,
                                pictureurl = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL, Common.FileConfig.FileType.ProductPhoto.ToString()),
                                productid = item.ProductId,
                                productname = item.ProductName,
                                productprice = item.ProductPrice,
                                quantity = item.Quantity
                            });
                        }
                        var model = new Models.TempOrderModels.result_model()
                        {
                            orderid = entity.OrderId,
                            shopid = entity.ShopId,
                            list = dinfo,
                            orderprice = entity.OrderPrice,
                        };
                        data.response = (int)Extensions.ErrorInfo.OK;
                        data.message = "成功";
                        data.result = model;
                    }
                    else
                    {
                        data.response = (int)Extensions.ErrorInfo.UpError;
                        data.message = "失败";
                    }
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.ParameterError;
                    data.message = "提交信息有误";
                }
                #endregion
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(TempOrderController), ex);
            }
            return Json(data);
        }
        /// <summary>
        /// 购物车去结算
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TempCardOrder()
        {
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.TempOrderModels.param>(Request["param"]);
            //TODO:修改为json格式获取参数
            var data = new Models.TempOrderModels.cart_json();
            try
            {
                if (string.IsNullOrEmpty(param.memberid) || string.IsNullOrEmpty(param.token))
                {
                    return Json(new { response = Extensions.ErrorInfo.ParameterError, message = "参数错误" });
                }
                var tc = new Cache.MemberTokenCache().Get(param.token);
                if (tc == null)
                {
                    return Json(new { response = Extensions.ErrorInfo.TokenError, message = "token验证失败请重新登录" });
                }
                if (AuthIsOpen)
                {
                    var dic = new SortedDictionary<string, string>();
                    dic.Add("timestamp", param.timestamp);
                    dic.Add("memberid", param.memberid);
                    dic.Add("token", param.token);
                    VerifyAuthorize(dic, param.timestamp, param.imei, param.sign);
                }
                var cartProvider = new Provider.ShoppingCartProvider();
                var order = new List<Entitys.Order_TempOrderBasis>();
                var detail = new List<Entitys.Order_TempOrderDetail>();
                //选中购物车需要结算的商品
                foreach (var item in param.shop)
                {
                    var entity = new Entitys.Order_TempOrderBasis()
                    {
                        OrderId = Common.PrimaryKey.GetHashCodeID,
                        ShopId = Common.ToolHelper.ConvertToLong(item.shopid),
                        OrderPrice = 0,
                        MemberId = Common.ToolHelper.ConvertToLong(param.memberid),
                        CreateTime = DateTime.Now,
                        IsCart = 1
                    };
                    decimal amount = 0;
                    foreach (var pro in item.product)
                    {
                        var cart = cartProvider.GetCartProd(Common.ToolHelper.ConvertToLong(pro.cartid));
                        if (cart != null)
                        {
                            var pd = new Provider.ProductBasisProvider().GetProduct(cart.ProductId);
                            decimal price = pd.PromoPrice;//有优惠需要计算优惠后的价格
                            string pic = pd.PictureURL;
                            string skuName = "";
                            if (pd.IsMoveProp == (int)Common.EnumModel.EIsMoveProp.MoveProp)
                            {
                                var sku = new Provider.ProductSKUProvider().GetSKU(Common.ToolHelper.ConvertToLong(cart.SKUId));
                                if (sku != null)
                                {
                                    price = sku.PromoPrice;
                                    pic = sku.PictureURL;
                                    skuName = sku.SKUName;
                                }
                            }
                            var info = new Entitys.Order_TempOrderDetail()
                            {
                                CartId=cart.Id,
                                SKUId = cart.SKUId,
                                SKUName = skuName,
                                IsGift = 0,
                                OrderId = entity.OrderId,
                                PayPrice = price,
                                PictureURL = pic,
                                ProductId = cart.ProductId,
                                Quantity = pro.quantity,
                                ProductName = pd.ProductName,
                                ProductPrice = pd.RealPrice,
                                CreateTime = DateTime.Now
                            };
                            amount += info.PayPrice;
                            detail.Add(info);
                        }
                    }
                    entity.OrderPrice = amount;
                    order.Add(entity);
                }
                #region 业务处理
                var line = new Provider.TempOrderProvider().AddTempOrder(order, detail);
                if (line > 0)
                {
                    var result = new List<Models.TempOrderModels.result_model>();
                    var prod = new List<Models.TempOrderModels.detail>();
                    foreach (var item in order)
                    {
                        prod = new List<Models.TempOrderModels.detail>();
                        foreach (var pro in detail.Where(c => c.OrderId == item.OrderId))
                        {
                            prod.Add(new Models.TempOrderModels.detail
                            {
                                isgift = pro.IsGift,
                                skuid = pro.SKUId,
                                skuname = pro.SKUName,
                                orderid = pro.OrderId,
                                payprice = pro.PayPrice,
                                pictureurl = pro.PictureURL,
                                productid=pro.ProductId,productname=pro.ProductName,
                                productprice=pro.ProductPrice,
                                quantity=pro.Quantity
                            });
                        }
                        result.Add(new Models.TempOrderModels.result_model
                        {
                            orderid = item.OrderId,
                            shopid = item.ShopId,
                            orderprice = item.OrderPrice,
                            list = prod
                        });
                    }
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "失败";
                }
                #endregion
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(TempOrderController), ex);
            }
            return Json(data);
        }
    }
}