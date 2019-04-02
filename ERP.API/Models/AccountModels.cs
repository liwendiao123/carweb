using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.API.Models
{
    public class AccountModels
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
            public result_model result { get; set; }
        }

        /// <summary>
        /// 用户实体
        /// </summary>
        [Serializable]
        public class result_model
        {
            /// <summary>
            /// 令牌  登录之后分配一个
            /// </summary>
            public string token { get; set; }
            public long memberid { get; set; }
            public string realname { get; set; }
            public int sex { get; set; }
            public string mobile { get; set; }
            public string email { get; set; }
            public string picture { get; set; }
            /// <summary>
            /// 推广码
            /// </summary>
            public int promocode { get; set; }
            /// <summary>
            /// 等级 默认0 会员默认1级
            /// </summary>
            public int memberlevel { get; set; }
            /// <summary>
            /// 积分
            /// </summary>
            public int integral { get; set; }
            public string password { get; set; }
            public string openid { get; set; }
            public string imei { get; set; }
        }
    }
}