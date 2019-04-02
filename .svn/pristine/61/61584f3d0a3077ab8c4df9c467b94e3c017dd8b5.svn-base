using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Member
{
    public class AreaBasisController : BaseController
    {
        // GET: AreaBasis
        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="areaid">区域上级id  省上级id为100000</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string memberid, string token, long areaid)
        {
            var data = new Models.AreaBasisModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token) || areaid == 0)
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
                    dic.Add("areaid", areaid.ToString());
                    VerifyAuthorize(dic);
                }
                //100000
                var cache = new Cache.Sys_AreasCache().Get(areaid);

                var model = new List<Models.AreaBasisModels.result_model>();
                foreach (var item in cache)
                {
                    model.Add(new Models.AreaBasisModels.result_model
                    {
                        areaid = item.AreaId,
                        areaname = item.ShortName
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
                Common.LogHelper.WriteLog(typeof(ShoppingCartController), ex);
            }
            return Json(data);
        }
    }
}