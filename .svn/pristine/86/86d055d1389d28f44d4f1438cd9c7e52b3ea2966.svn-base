using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    /// <summary>
    /// 角色菜单缓存
    /// </summary>
    public class Sys_RoleMenuCache
    {
        MemcachedClient mc;
        public Sys_RoleMenuCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加角色菜单缓存
        /// </summary>
        /// <param name="RoleId">用户ID</param>
        /// <param name="list">角色ID</param>
        /// <returns></returns>
        public bool Set(long RoleId, List<MenuRoleModel> list)
        {
            return mc.Set(this.GetCacheName(RoleId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取角色菜单缓存
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public List<MenuRoleModel> Get(long RoleId)
        {
            var list = new List<MenuRoleModel>();
            if (!this.Exists(RoleId))
            {
                var db = new Entitys.UCMSContext();
                var t = db.SysRoleMenu.Where(c => c.IsDelete == 0 && c.RoleId == RoleId);
                if (t != null && t.Count() > decimal.Zero)
                {
                    foreach (var item in t)
                    {
                        list.Add(new MenuRoleModel
                        {
                            MenuId = item.MenuId,
                            Id=item.Id
                        });
                    }
                    //保存缓存
                    this.Set(RoleId, list);
                }
            }
            else
            {
                list = (List<MenuRoleModel>)mc.Get(this.GetCacheName(RoleId));
            }
            return list;
        }

        /// <summary>
        /// 删除角色菜单
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public bool Delete(long RoleId)
        {
            return mc.Delete(this.GetCacheName(RoleId));
        }

        /// <summary>
        /// 是否存在角色菜单
        /// </summary>
        /// <param name="RoleId">角色ID</param>
        /// <returns></returns>
        public bool Exists(long RoleId)
        {
            return mc.KeyExists(this.GetCacheName(RoleId));
        }

        /// <summary>
        /// 角色系统菜单
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(long RoleId)
        {
            return String.Format("{0}-{1}", MemcachedKey.MENU_ROLE, RoleId);
        }

        /// <summary>
        /// 角色菜实体
        /// </summary>
        [Serializable]
        public class MenuRoleModel
        {
            public long MenuId { get; set; }
            public long Id { get; set; }
        }
    }
}