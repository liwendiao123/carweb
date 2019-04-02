using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class ProductCategoryModels
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
            public long categoryid { get; set; }
            public string categoryname { get; set; }
            public long parentid { get; set; }
            public List<result_model> list { get; set; }
        }
    }
}