using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_UserRoleCache
    {
        MemcachedClient mc;
        public Sys_UserRoleCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加角色用户缓存
        /// </summary>
        /// <param name="UserId">ID</param>
        /// <param name="list">角色ID列表</param>
        public bool Set(long UserId, List<RoleUserModel> list)
        {
            return mc.Set(this.GetCacheName(UserId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取角色用户缓存
        /// </summary>
        /// <param name="UserId">ID</param>
        /// <returns></returns>
        public List<RoleUserModel> Get(long UserId)
        {
            var list = new List<RoleUserModel>();
            if (!this.Exists(UserId))
            {
                var tb= new UCMS.Entitys.UCMSContext().SysRoleUser.Where(c => c.IsDelete == 0 && c.UserId == UserId);

                if (tb != null && tb.Count() > 0)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new RoleUserModel
                        {
                            RoleUserId=item.Id,
                            RoleId = item.RoleId
                        });
                    }
                }
                if (list.Count > decimal.Zero)
                {
                    //提交缓存
                    this.Set(UserId, list);
                }
            }
            else
            {
                list = (List<RoleUserModel>)mc.Get(this.GetCacheName(UserId));
            }
            return list;
        }

        /// <summary>
        /// 是否存在角色用户缓存
        /// </summary>
        /// <param name="UserId">ID</param>
        /// <returns></returns>
        public bool Exists(long UserId)
        {
            return mc.KeyExists(this.GetCacheName(UserId));
        }

        /// <summary>
        /// 删除角色用户缓存
        /// </summary>
        /// <param name="UserId">ID</param>
        public bool Delete(long UserId)
        {
            return mc.Delete(this.GetCacheName(UserId));
        }

        /// <summary>
        /// 角色用户缓存名称
        /// </summary>
        /// <param name="UserId">ID</param>
        /// <returns></returns>
        private string GetCacheName(long UserId)
        {
            return String.Format("{0}-{1}", Cache.MemcachedKey.USER_ROLE, UserId);
        }

        [Serializable]
        public class RoleUserModel
        {
            public long RoleUserId { get; set; }
            public long RoleId { get; set; }
        }
    }
}
