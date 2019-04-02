using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class AreaBasisModels
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
            public long areaid { get; set; }
            public string areaname { get; set; }
        }
    }
}