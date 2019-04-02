using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum EUserType
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        System = 0,
        /// <summary>
        /// 会员
        /// </summary>
        [Description("会员")]
        Merchant = 1,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Manager = 2,
    }
}
