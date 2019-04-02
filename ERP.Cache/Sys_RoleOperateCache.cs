using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_RoleOperateCache
    {
        MemcachedClient mc;
        public Sys_RoleOperateCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加角色操作码缓存
        /// </summary>
        /// <returns></returns>
        public bool Set(long RoleId, List<RoleOperateModel> list)
        {
            return mc.Set(this.GetCacheName(RoleId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取角色操作码缓存
        /// </summary>
        /// <returns></returns>
        public List<RoleOperateModel> Get(long RoleId)
        {
            var list = new List<RoleOperateModel>();
            if (!this.Exists(RoleId))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.SysRoleOperate.Where(c => c.IsDelete == 0 && c.RoleId == RoleId);
                if (tb != null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new RoleOperateModel
                        {
                            MenuId=item.MenuId,
                            OperateId=item.OperateId,
                            Id = item.Id
                        });
                    }
                    //保存缓存
                    this.Set(RoleId, list);
                }
            }
            else
            {
                list = (List<RoleOperateModel>)mc.Get(this.GetCacheName(RoleId));
            }
            return list;
        }

        /// <summary>
        /// 删除角色操作码缓存
        /// </summary>
        /// <returns></returns>
        public bool Delete(long RoleId)
        {
            return mc.Delete(this.GetCacheName(RoleId));
        }

        /// <summary>
        /// 是否存在角色操作码缓存
        /// </summary>
        /// <returns></returns>
        public bool Exists(long RoleId)
        {
            return mc.KeyExists(this.GetCacheName(RoleId));
        }

        /// <summary>
        /// 角色操作码
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(long RoleId)
        {
            return String.Format("{0}-{1}", MemcachedKey.ROLE_OPERATE, RoleId);
        }

        [Serializable]
        public class RoleOperateModel
        {
            public long Id { get; set; }
            public long OperateId { get; set; }
            public long MenuId { get; set; }
        }
    }
}