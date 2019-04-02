using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.API.Controllers.Member
{
    public class DeliverAddressController : BaseController
    {
        // GET: DeliverAddress
        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string memberid, string token)
        {
            var data = new Models.DeliverAddressModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token))
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
                var model = new List<Models.DeliverAddressModels.result_model>();
                var list = new Cache.Member_DeliverAddressCache().Get(Common.ToolHelper.ConvertToLong(memberid));
                foreach (var item in list)
                {
                    model.Add(new Models.DeliverAddressModels.result_model {
                        addressid = item.Id,
                        street = item.Street,
                        addressdetail = item.AddressDetail,
                        city = item.City,
                        district = item.District,
                        isdefault = item.IsDefault,
                        province = item.Province,
                        areaname = item.AreaName,
                        fullname = item.FullName,
                        mobile = item.Mobile,
                        phone = item.Phone
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
        /// <summary>
        /// 新增地址
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string memberid, string token, Models.DeliverAddressModels.result_model model)
        {
            var data = new Models.DeliverAddressModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token))
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

                var entity = new Entitys.Member_DeliverAddress()
                {
                    Id = 0,
                    Mobile = model.mobile,
                    Phone = model.phone,
                    Province = model.province,
                    Street = model.street,
                    AddressDetail = model.addressdetail,
                    AreaName = model.areaname,
                    City = model.city,
                    District = model.district,
                    FullName = model.fullname,
                    IsDefault = (byte)model.isdefault,
                    MemberId = Common.ToolHelper.ConvertToLong(memberid),

                    TimeStamp = DateTime.Now,
                    CreateTime = DateTime.Now,
                    IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                };
                var res = new Provider.DeliverAddressProvider().Edit(entity);
                if (res > 0)
                {
                    new Cache.Member_DeliverAddressCache().Delete(Common.ToolHelper.ConvertToLong(memberid));
                    data.response = (int)Extensions.ErrorInfo.OK;
                    data.message = "成功";
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "新增失败";
                }
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
        /// 修改地址
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string memberid, string token, Models.DeliverAddressModels.result_model model)
        {
            var data = new Models.DeliverAddressModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token) || model.addressid == 0)
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
                    dic.Add("addressid", model.addressid.ToString());
                    VerifyAuthorize(dic);
                }
                var entity = new Entitys.Member_DeliverAddress()
                {
                    Id = model.addressid,
                    Mobile = model.mobile,
                    Phone = model.phone,
                    Province = model.province,
                    Street = model.street,
                    AddressDetail = model.addressdetail,
                    AreaName = model.areaname,
                    City = model.city,
                    District = model.district,
                    FullName = model.fullname,
                    IsDefault = (byte)model.isdefault,
                    MemberId = Common.ToolHelper.ConvertToLong(memberid),

                    TimeStamp = DateTime.Now,
                    CreateTime = DateTime.Now,
                    IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                };
                var res = new Provider.DeliverAddressProvider().Edit(entity);
                if (res > 0)
                {
                    new Cache.Member_DeliverAddressCache().Delete(Common.ToolHelper.ConvertToLong(memberid));
                    data.response = (int)Extensions.ErrorInfo.OK;
                    data.message = "成功";
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "修改失败";
                }
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
        /// 设置默认地址
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="addressid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetDefault(string memberid, string token,long addressid)
        {
            var data = new Models.DeliverAddressModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token) || addressid == 0)
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
                    dic.Add("addressid", addressid.ToString());
                    VerifyAuthorize(dic);
                }
                var res = new Provider.DeliverAddressProvider().SetDefault(Common.ToolHelper.ConvertToLong(memberid), addressid);
                if (res > 0)
                {
                    new Cache.Member_DeliverAddressCache().Delete(Common.ToolHelper.ConvertToLong(memberid));
                    data.response = (int)Extensions.ErrorInfo.OK;
                    data.message = "成功";
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "设置失败";
                }
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
        /// 删除地址
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="token"></param>
        /// <param name="addressid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string memberid, string token, long addressid)
        {
            var data = new Models.DeliverAddressModels.json_model();
            try
            {
                if (string.IsNullOrEmpty(memberid) || string.IsNullOrEmpty(token) || addressid == 0)
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
                    dic.Add("addressid", addressid.ToString());
                    VerifyAuthorize(dic);
                }
                var ids = new List<long>();
                ids.Add(Common.ToolHelper.ConvertToLong(memberid));
                var res = new Provider.DeliverAddressProvider().Delete(ids);
                if (res > 0)
                {
                    new Cache.Member_DeliverAddressCache().Delete(Common.ToolHelper.ConvertToLong(memberid));
                    data.response = (int)Extensions.ErrorInfo.OK;
                    data.message = "成功";
                }
                else
                {
                    data.response = (int)Extensions.ErrorInfo.UpError;
                    data.message = "设置失败";
                }
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