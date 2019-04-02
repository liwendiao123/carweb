using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    /// <summary>
    /// 服务承诺状态
    /// </summary>
    public enum EPromiseStatus
    {
        /// <summary>
        /// 开启
        /// </summary>
        [Description("开启")]
        On = 1,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Off = 2,
    }
}
