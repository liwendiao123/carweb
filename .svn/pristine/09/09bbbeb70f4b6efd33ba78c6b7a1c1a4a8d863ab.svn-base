using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    /// <summary>
    ///帐号缓存类
    /// </summary>
    public class AccountCodeCache
    {
        MemcachedClient mc;
        public AccountCodeCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加缓存,如存在则更新
        /// </summary>
        /// <param name="model"></param>
        public bool Set(LoginCodeModel model)
        {
            return mc.Set(this.GetCacheName(model.LoginCode), model, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 判断是否存在缓存
        /// </summary>
        /// <param name="LoginCode">登录帐号</param>
        public bool Exists(int LoginCode)
        {
            return mc.KeyExists(this.GetCacheName(LoginCode));
        }

        /// <summary>
        /// 返回缓存帐号信息
        /// </summary>
        /// <param name="LoginCode">登录帐号</param>
        /// <returns></returns>
        public LoginCodeModel Get(int LoginCode)
        {
            var model = new LoginCodeModel();
            if (!this.Exists(LoginCode))
            {
                var db = new UCMS.Entitys.UCMSContext();
                var t = db.SysUserBasis.Where(c => c.IsDelete == 0 && c.LoginCode == LoginCode).FirstOrDefault();
                if (t != null)
                {
                    var query = from a in db.SysRoleBasis
                                from b in db.SysRoleUser
                                where a.Id == b.RoleId && b.UserId == t.Id && a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete
                                select new { a.RoleCode };
                    int type = 0;
                    foreach (var item in query)
                    {
                        if (item.RoleCode.ToLower() == "system")
                        {
                            type = 1;
                            break;
                        }
                    }
                    model = new LoginCodeModel
                    {
                        UserId = t.Id,
                        LoginCode = LoginCode,
                        Passwords = t.Passwords,
                        RealName = t.RealName,
                        LastTime = t.LastTime,
                        Mobile = t.Mobile,
                        QQ = t.QQ,
                        Sex = t.Sex,
                        Weixin = t.Weixin,
                        UserType = type
                    };
                    //保存缓存
                    this.Set(model);
                }
                else
                {
                    model = null;
                }
            }
            else
            {
                model = (LoginCodeModel)mc.Get(this.GetCacheName(LoginCode));
            }
            return model;
        }

        /// <summary>
        /// 移除缓存帐号信息
        /// </summary>
        /// <param name="LoginCode">登录帐号</param>
        public bool Delete(int LoginCode)
        {
            return mc.Delete(this.GetCacheName(LoginCode));
        }

        /// <summary>
        /// 清除用户登录信息
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Mobile"></param>
        /// <param name="LoginCode"></param>
        public void Remove(int LoginCode)
        {
            //清除帐号登录信息
            if (LoginCode!=0)
            {
                if (this.Exists(LoginCode))
                {
                    this.Delete(LoginCode);
                }
            }
        }

        /// <summary>
        /// 返回缓存名称
        /// </summary>
        /// <param name="LoginCode">登录帐号</param>
        private string GetCacheName(int LoginCode)
        {
            return string.Format("{0}-{1}", MemcachedKey.ACCOUNT_CODE, LoginCode);
        }
        
        #region 实体对象
        [Serializable]
        public class LoginCodeModel
        {
            public long UserId { get; set; }
            /// <summary>
            /// 账号
            /// </summary>
            public int LoginCode { get; set; }
            public string Passwords { get; set; }
            public string RealName { get; set; }
            public byte Sex { get; set; }
            public string Mobile { get; set; }
            public string Weixin { get; set; }
            public int QQ { get; set; }
            /// <summary>
            /// 1管理员
            /// </summary>
            public int UserType { get; set; }
            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime LastTime { get; set; }
        }
        #endregion
    }
}
