using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    /// <summary>
    /// 1正常  2退款中 3退款完成 4已完成
    /// 1显示可以退货 2退款详情 3查看退款 4售后
    /// </summary>
    public enum EDetailStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        正常 = 1,
        /// <summary>
        /// 退款中
        /// </summary>
        [Description("退款中")]
        退款中 = 2,
        /// <summary>
        /// 退款完成
        /// </summary>
        [Description("退款完成")]
        退款完成 = 3,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        已完成 = 4,
    }
}
