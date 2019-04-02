using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    public enum EOrderStatus
    {
        //订单状态0未付款 1已付款 2已发货 3已收货 4待评价 5已完成 6已关闭
        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        未付款 = 0,
        /// <summary>
        /// 已付款
        /// </summary>
        [Description("已付款")]
        已付款 = 1,
        /// <summary>
        /// 已发货
        /// </summary>
        [Description("已发货")]
        已发货 = 2,
        /// <summary>
        /// 已收货
        /// </summary>
        [Description("已收货")]
        已收货 = 3,
        /// <summary>
        /// 待评价
        /// </summary>
        [Description("待评价")]
        待评价 = 4,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        已完成 = 5,
        /// <summary>
        /// 已关闭
        /// </summary>
        [Description("已关闭")]
        已关闭 = 6,
    }
}
