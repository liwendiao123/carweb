﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common.EnumModel
{
    public enum EIsDelete
    {
        /// <summary>
        /// 未删除
        /// </summary>
        [Description("未删除")]
        NotDelete = 0,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = 1
    }
}