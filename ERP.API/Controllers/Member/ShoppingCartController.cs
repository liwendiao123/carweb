using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Member
{
    public class ShoppingCartController : BaseController
    {
        // GET: ShoppingCart
        /// <summary>
        /// 购物车列表
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="index"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string memberid, string token)
        {
            var data = new Models.ShoppingCartModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid)|| string.IsNullOrEmpty(token))
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
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ShoppingCartModels.result_model>();
                var shop= new List<Models.ShoppingCartModels.shop_model>();
                var list = new Provider.ShoppingCartProvider().GetList(Common.ToolHelper.ConvertToLong(memberid));
                var ids = list.Select(c => c.ShopId).Distinct();
                var cache = new Cache.BIZ_ShopBasisCache().Get(Common.FormsTicket.SystemCode);
                var pic = "";
                foreach (var s in ids)
                {
                    var sp = cache.Where(c => c.Id == s).FirstOrDefault();
                    model = new List<Models.ShoppingCartModels.result_model>();
                    foreach (var item in list.Where(c=>c.ShopId==s))
                    {
                        pic = new ERP.Common.FileHelper().GetWebFileUrl(item.PictureURL, Common.FileConfig.FileType.ProductPhoto.ToString());
                        model.Add(new Models.ShoppingCartModels.result_model
                        {
                            skuname = item.SKUName,
                            quantity = item.Quantity,
                            cartstatus = item.CartStatus,
                            cartid = item.Id,
                            ismoveprop = item.IsMoveProp,
                            pictureurl = pic,
                            productname = item.ProductName,
                            promoprice = item.PromoPrice,
                            realprice = item.RealPrice,
                            orderid = "cart_" + item.Id
                        });
                    }
                    shop.Add(new Models.ShoppingCartModels.shop_model {
                        product = model,
                        shopid=sp.Id,
                        shopname=sp.ShopName
                    });
                }
                data.response = (int)Extensions.ErrorInfo.OK;
                data.message = "成功";
                data.result = shop;
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(ShoppingCartController), ex);
            }
            return Json(data);
        }
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="productid"></param>
        /// <param name="skuid"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string memberid,string token,string productid,string shopid,string skuid, int quantity = 0)
        {
            int response = 0;
            string message = "";
            try
            {
                if (string.IsNullOrEmpty(memberid) ||string.IsNullOrEmpty(token) ||string.IsNullOrEmpty(productid) ||string.IsNullOrEmpty(shopid) || quantity == 0)
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
                    dic.Add("shopid", shopid);
                    VerifyAuthorize(dic);
                }
                    //判断是否已经存在购物车
                var isExist = new Provider.ShoppingCartProvider().IsCart(Common.ToolHelper.ConvertToLong(productid), Common.ToolHelper.ConvertToLong(memberid), Common.ToolHelper.ConvertToLong(skuid));
                if (isExist)
                {
                    response = (int)Extensions.ErrorInfo.OK;
                    message = "成功";
                }
                else
                {
                    var model = new List<Models.ShoppingCartModels.result_model>();
                    var entity = new Entitys.Member_ShoppingCart()
                    {
                        ProductId = Common.ToolHelper.ConvertToLong(productid),
                        Quantity = quantity,
                        MemberId = Common.ToolHelper.ConvertToLong(memberid),
                        SKUId = string.IsNullOrEmpty(skuid) ? Common.Constant.LONG_DEFAULT : Common.ToolHelper.ConvertToLong(skuid),
                        CreateTime = DateTime.Now,
                        IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                        TimeStamp = DateTime.Now,
                        ShopId = Common.ToolHelper.ConvertToLong(shopid)
                    };
                    var list = new Provider.ShoppingCartProvider().Edit(entity);
                    if (list > 0)
                    {
                        response = (int)Extensions.ErrorInfo.OK;
                        message = "成功";
                    }
                    else
                    {
                        response = (int)Extensions.ErrorInfo.UpError;
                        message = "修改失败";
                    }
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(ShoppingCartController), ex);
            }
            return Json(new { response = response, message = message });
        }
        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="cartid"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string cartid, string token, int quantity = 0)
        {
            int response = 0;
            string message = "";
            try
            {
                if (string.IsNullOrEmpty(cartid)||string.IsNullOrEmpty(token) || quantity == 0)
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
                    dic.Add("cartid", cartid);
                    dic.Add("token", token);
                    VerifyAuthorize(dic);
                }
                var model = new List<Models.ShoppingCartModels.result_model>();
                var entity = new Entitys.Member_ShoppingCart() { Id= Common.ToolHelper.ConvertToLong(cartid) ,Quantity= quantity };
                var list = new Provider.ShoppingCartProvider().Edit(entity);
                if (list > 0)
                {
                    response = (int)Extensions.ErrorInfo.OK;
                    message = "成功";
                }
                else
                {
                    response = (int)Extensions.ErrorInfo.UpError;
                    message = "修改失败";
                }
            }
            catch (Exception ex)
            {
                response = (int)Extensions.ErrorInfo.ServerError;
                message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(ShoppingCartController), ex);
            }
            return Json(new { response = response, message = message });
        }
    }
}