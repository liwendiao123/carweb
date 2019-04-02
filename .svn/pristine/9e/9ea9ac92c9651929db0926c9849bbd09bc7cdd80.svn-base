using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class ShoppingCartModels
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
            public List<shop_model> result { get; set; }
        }
        [Serializable]
        public class shop_model
        {
            public long shopid { get; set; }
            public string shopname { get; set; }
            public List<result_model> product { get; set; }
        }
        /// <summary>
        /// 实体
        /// </summary>
        [Serializable]
        public class result_model
        {
            public long cartid { get; set; }
            /// <summary>
            /// 结算时需要提交的id value就是数量
            /// </summary>
            public string orderid { get; set; }
            public int quantity { get; set; }
            /// <summary>
            /// 是否多属性
            /// </summary>
            public int ismoveprop { get; set; }
            /// <summary>
            /// 1正常 0商品已下架或删除
            /// </summary>
            public int cartstatus { get; set; }
            /// <summary>
            /// 原价
            /// </summary>
            public decimal realprice { get; set; }
            /// <summary>
            /// 实际价格
            /// </summary>
            public decimal promoprice { get; set; }
            public string pictureurl { get; set; }
            /// <summary>
            /// 商品名称
            /// </summary>
            public string productname { get; set; }
            /// <summary>
            /// sku串名
            /// </summary>
            public string skuname { get; set; }
        }
    }
}