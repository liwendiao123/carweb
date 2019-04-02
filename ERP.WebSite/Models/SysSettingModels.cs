﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class SysSettingModels
    {
        public class SysSetting
        {
            public long Id { get; set; }
            /// <summary>
            /// 系统名称
            /// </summary>
            public string SystemName { get; set; }
            /// <summary>
            /// 主页logo
            /// </summary>
            public string HomeLogo { get; set; }
            /// <summary>
            /// 登录页logo
            /// </summary>
            public string LoginLogo { get; set; }
            /// <summary>
            /// 底部logo
            /// </summary>
            public string FootLogo { get; set; }
            /// <summary>
            /// 账号开通联系方式
            /// </summary>
            public string Contact { get; set; }
            /// <summary>
            /// 问题反馈群
            /// </summary>
            public string Feeback { get; set; }
            public int IsEnable { get; set; }
        }
    }
}