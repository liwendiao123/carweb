using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Order
{
    public class OrderBasisController : BaseController
    {
        // GET: OrderBasis

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="token"></param>
        /// <param name="memberid">会员id</param>
        /// <param name="productid">商品id</param>
        /// <param name="skuid"></param>
        /// <param name="quantity">数量</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitOrder()
        {
            var param = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.OrderBasisModels.param>(Request["param"]);
            var data = new Models.ShoppingCartModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(param.memberid) || string.IsNullOrEmpty(param.token) || string.IsNullOrEmpty(param.addressid))
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
                    dic.Add("productid", param.addressid);
                    VerifyAuthorize(dic, param.timestamp, param.imei, param.sign);
                }

                #region 业务处理
                
                #endregion
                
                var model = new List<Models.ShoppingCartModels.result_model>();
                data.response = (int)Extensions.ErrorInfo.OK;
                data.message = "成功";
                //data.result = model;
            }
            catch (Exception ex)
            {
                data.response = (int)Extensions.ErrorInfo.ServerError;
                data.message = "服务器内部错误";
                Common.LogHelper.WriteLog(typeof(OrderBasisController), ex);
            }
            return Json(data);
        }
        
    }
}