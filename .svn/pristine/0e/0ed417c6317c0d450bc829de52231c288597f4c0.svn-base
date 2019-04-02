using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class DeliverAddressModels
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
            public long addressid { get; set; }
            /// <summary>
            /// 省id
            /// </summary>
            public long province { get; set; }
            public long city { get; set; }
            public long district { get; set; }
            public long street { get; set; }
            /// <summary>
            /// 区域名称
            /// </summary>
            public string areaname { get; set; }
            public string addressdetail { get; set; }
            /// <summary>
            /// 收货人
            /// </summary>
            public string fullname { get; set; }
            /// <summary>
            /// 手机
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 电话
            /// </summary>
            public string phone { get; set; }
            /// <summary>
            /// 是否默认地址  1默认地址
            /// </summary>
            public int isdefault { get; set; }
        }
    }
}