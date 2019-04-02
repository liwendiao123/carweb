using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    /// <summary>
    /// Memcached Key对象
    /// </summary>
    public class MemcachedKey
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public static string SYTEM_CODE = "UCMS" + ConfigurationManager.AppSettings["Memcached.ServerCode"];

        #region Memcached Key
        #region 系统权限

        /// <summary>
        /// 登录帐号
        /// </summary>
        public static string ACCOUNT_CODE = String.Format("{0}-{1}", SYTEM_CODE, KeyCode.ACCOUNT.ToString());

        /// <summary>
        /// 系统菜单
        /// </summary>
        public static string MENU_BASIS = String.Format("{0}-{1}-BASIS", SYTEM_CODE, KeyCode.MENU.ToString());

        /// <summary>
        /// 权限菜单
        /// </summary>
        public static string MENU_ROLE = String.Format("{0}-{1}-MENU", SYTEM_CODE, KeyCode.ROLE.ToString());
        
        /// <summary>
        /// 角色
        /// </summary>
        public static string ROLE_BASIS = String.Format("{0}-{1}-BASIS", SYTEM_CODE, KeyCode.ROLE.ToString());
        /// <summary>
        /// 用户角色
        /// </summary>
        public static string USER_ROLE = String.Format("{0}-{1}-USER", SYTEM_CODE, KeyCode.ROLE.ToString());
        /// <summary>
        /// 角色操作码
        /// </summary>
        public static string ROLE_OPERATE = String.Format("{0}-{1}-OPERATE", SYTEM_CODE, KeyCode.ROLE.ToString());
        /// <summary>
        /// 区域
        /// </summary>
        public static string AREA = String.Format("{0}-{1}-AREA", SYTEM_CODE, KeyCode.AREA.ToString());

        #endregion
        #region 用户信息
        /// <summary>
        /// 用户
        /// </summary>
        public static string USER_BASIS = string.Format("{0}-{1}-BASIS", SYTEM_CODE, KeyCode.USER.ToString());

        #endregion
        #region 会员
        /// <summary>
        /// 用户
        /// </summary>
        public static string TOKEN_CODE = string.Format("{0}-{1}", SYTEM_CODE, KeyCode.TOKEN.ToString());

        #endregion

        public static string CAR_BRAND = string.Format("{0}-{1}-BRAND", SYTEM_CODE, KeyCode.CAR.ToString());
        public static string CAR_TYPE = string.Format("{0}-{1}-TYPE", SYTEM_CODE, KeyCode.CAR.ToString());
        public static string CAR_SERIES = string.Format("{0}-{1}-SERIES", SYTEM_CODE, KeyCode.CAR.ToString());
        /// <summary>
        /// 系统设置
        /// </summary>
        public static string SETTING = string.Format("{0}-{1}-SETTING", SYTEM_CODE, KeyCode.SETTING.ToString());
        #endregion
        /// <summary>
        /// Memcached key
        /// </summary>
        enum KeyCode
        {
            /// <summary>
            /// 帐户信息
            /// </summary>
            ACCOUNT,
            /// <summary>
            /// 用户信息
            /// </summary>
            USER,
            /// <summary>
            /// 菜单
            /// </summary>
            MENU,
            /// <summary>
            /// 权限
            /// </summary>
            ROLE,
            /// <summary>
            /// 车辆管理
            /// </summary>
            CAR,
            /// <summary>
            /// token
            /// </summary>
            TOKEN,
            /// <summary>
            /// 区域
            /// </summary>
            AREA,
            /// <summary>
            /// 系统设置
            /// </summary>
            SETTING,
        }
    }
}
