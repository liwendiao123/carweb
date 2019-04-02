using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class TempOrderModels
    {
        [Serializable]
        public class json_model
        {
            /// <summary>
            /// 返回状态
            /// 0-失败 1-成功 
            /// </summary>
            public int response { get; set; }
            /// <summary>
            /// 消息
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// 信息
            /// </summary>
            public result_model result { get; set; }
        }

        /// <summary>
        /// 实体
        /// </summary>
        [Serializable]
        public class result_model
        {
            public long orderid { get; set; }
            public long shopid { get; set; }
            /// <summary>
            /// 订单价格
            /// </summary>
            public decimal orderprice { get; set; }
            public List<detail> list { get; set; }
        }
        [Serializable]
        public class detail
        {
            public long orderid { get; set; }
            public long productid { get; set; }
            public long skuid { get; set; }
            public string skuname { get; set; }
            public string pictureurl { get; set; }
            public string productname { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int quantity { get; set; }
            public decimal productprice { get; set; }
            /// <summary>
            /// 支付价 优惠价
            /// </summary>
            public decimal payprice { get; set; }
            /// <summary>
            /// 是否赠品 1赠品
            /// </summary>
            public byte isgift { get; set; }
        }
        #region 购物车去结算
        [Serializable]
        public class param
        {
            public string token { get; set; }
            public string memberid { get; set; }
            public string timestamp { get; set; }
            public string imei { get; set; }
            public string sign { get; set; }
            public List<shop_model> shop { get; set; }
        }
        [Serializable]
        public class shop_model
        {
            public string shopid { get; set; }
            public List<product_model> product { get; set; }
        }
        [Serializable]
        public class product_model
        {
            public string cartid { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int quantity { get; set; }
        }

        #endregion

        #region 购物车结算响应
        [Serializable]
        public class cart_json
        {
            /// <summary>
            /// 返回状态
            /// 0-失败 1-成功 
            /// </summary>
            public int response { get; set; }
            /// <summary>
            /// 消息
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// 信息
            /// </summary>
            public List<result_model> result { get; set; }
        }
        #endregion
    }
}