using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class OrderBasisModels
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
            public List<result_model> result { get; set; }
        }

        /// <summary>
        /// 实体
        /// </summary>
        [Serializable]
        public class result_model
        {
            public long categoryid { get; set; }
            public string categoryname { get; set; }
            public long parentid { get; set; }
            public List<result_model> list { get; set; }
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        [Serializable]
        public class param
        {
            public string token { get; set; }
            public string memberid { get; set; }
            public string timestamp { get; set; }
            public string imei { get; set; }
            public string sign { get; set; }
            public string addressid { get; set; }
            public List<order_model> shop { get; set; }
        }
        [Serializable]
        public class order_model
        {
            public string orderid { get; set; }
            public string shopid { get; set; }
            public string remark { get; set; }
        }
    }
}