using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class ProductListModels
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
            /// 用户登录信息
            /// </summary>
            public List<result_model> result { get; set; }
        }

        /// <summary>
        /// 用户实体
        /// </summary>
        [Serializable]
        public class result_model
        {
            public long productid { get; set; }
            public string productname { get; set; }
            /// <summary>
            /// 实际价格
            /// </summary>
            public decimal promoprice { get; set; }
            /// <summary>
            /// 原价
            /// </summary>
            public decimal realprice { get; set; }
            public string pictureurl { get; set; }
            /// <summary>
            /// 销量
            /// </summary>
            public int salecount { get; set; }
            /// <summary>
            /// 评价数
            /// </summary>
            public int review { get; set; }
        }
    }
}