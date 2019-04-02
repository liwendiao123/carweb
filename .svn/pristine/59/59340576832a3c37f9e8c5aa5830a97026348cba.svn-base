using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class ProductInfoModels
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
        [Serializable]
        public class result_model
        {
            public product_model product { get; set; }
            public List<sku_model> sku { get; set; }
            public List<pic_model> picture { get; set; }
            public List<property> property { get; set; }
        }
        /// <summary>
        /// 商品
        /// </summary>
        [Serializable]
        public class product_model
        {
            public long shopid { get; set; }
            public string shopname { get; set; }
            public long productid { get; set; }
            public string productname { get; set; }
            public int ismoveprop { get; set; }
            /// <summary>
            /// 实际价格
            /// </summary>
            public decimal promoprice { get; set; }
            /// <summary>
            /// 原价
            /// </summary>
            public decimal realprice { get; set; }
            /// <summary>
            /// 销量
            /// </summary>
            public int salecount { get; set; }
            /// <summary>
            /// 库存
            /// </summary>
            public int stock { get; set; }
            /// <summary>
            /// 评价数
            /// </summary>
            public int review { get; set; }
        }
        /// <summary>
        /// sku
        /// </summary>
        [Serializable]
        public class sku_model
        {
            public long skuid { get; set; }
            /// <summary>
            /// 实际价格
            /// </summary>
            public decimal promoprice { get; set; }
            /// <summary>
            /// 原价
            /// </summary>
            public decimal realprice { get; set; }
            /// <summary>
            /// 销量
            /// </summary>
            public int salecount { get; set; }
            /// <summary>
            /// 库存
            /// </summary>
            public int stock { get; set; }
            public string skustring { get; set; }
            public string pictureurl { get; set; }
        }
        /// <summary>
        /// pic
        /// </summary>
        [Serializable]
        public class pic_model
        {
            public string pictureurl { get; set; }
        }
        /// <summary>
        /// 属性
        /// </summary>
        public class property
        {
            public int id { get; set; }
            public string name { get; set; }
            public List<property_value> values { get; set; }
        }
        /// <summary>
        /// 属性值
        /// </summary>
        public class property_value
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        #region 评论
        public class review_json
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
            public List<review_model> result { get; set; }
        }
        public class review_model
        {
            public int id { get; set; }
            public string membername { get; set; }
            public string skuname { get; set; }
            /// <summary>
            /// 首次评论 内容
            /// </summary>
            public string reviewpremiere { get; set; }
            /// <summary>
            /// 追评内容
            /// </summary>
            public string reviewappend { get; set; }
            /// <summary>
            /// 追评时间
            /// </summary>
            public DateTime appendtime { get; set; }
            /// <summary>
            /// 评价等级
            /// </summary>
            public byte reviewlevel { get; set; }
            /// <summary>
            /// 评论回复
            /// </summary>
            public string reviewreply { get; set; }
            public int isappend { get; set; }
            public int ispicture { get; set; }
            public DateTime replytime { get; set; }
            public string pictureurl1 { get; set; }
            public string pictureurl2 { get; set; }
            public string pictureurl3 { get; set; }
            public string pictureurl4 { get; set; }
            public string pictureurl5 { get; set; }
            public DateTime createtime { get; set; }
        }
        #endregion
    }
}