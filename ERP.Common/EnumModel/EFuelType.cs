using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    /// <summary>
    /// 燃料
    /// </summary>
    public enum EFuelType
    {
        /// <summary>
        /// 汽油
        /// </summary>
        [Description("汽油")]
        Gasoline = 1,

        /// <summary>
        /// 柴油
        /// </summary>
        [Description("柴油")]
        Diesel = 2,
        /// <summary>
        /// 电动
        /// </summary>
        [Description("电动")]
        Electric = 3,
        /// <summary>
        /// 混合电动
        /// </summary>
        [Description("混合电动")]
        Mixture = 4,
    }
}
