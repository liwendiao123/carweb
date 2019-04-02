using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_RoleBasisCache
    {
        MemcachedClient mc;
        public Sys_RoleBasisCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加角色缓存
        /// </summary>
        /// <param name="list">角色列表</param>
        /// <returns></returns>
        public bool Set(int SystemCode, List<RoleBasisModel> list)
        {
            return mc.Set(this.GetCacheName(SystemCode), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取角色缓存
        /// </summary>
        /// <returns></returns>
        public List<RoleBasisModel> Get(int SystemCode)
        {
            var list = new List<RoleBasisModel>();
            if (!this.Exists(SystemCode))
            {
                var db =new Entitys.UCMSContext();
                var t = db.SysRoleBasis.Where(c => c.IsDelete == 0).OrderBy(c => c.RoleSort);
                if (t!=null&&t.Count() > decimal.Zero)
                {
                    foreach (var item in t)
                    {
                        list.Add(new RoleBasisModel
                        {
                            Id = item.Id,
                            RoleSort=item.RoleSort,
                            IsSystem = item.IsSystem,
                            RoleName = item.RoleName,
                            RoleCode=item.RoleCode,
                        });
                    }
                    //保存缓存
                    this.Set(SystemCode, list);
                }
            }
            else
            {
                list = (List<RoleBasisModel>)mc.Get(this.GetCacheName(SystemCode));
            }
            return list;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        public bool Delete(int SystemCode)
        {
            return mc.Delete(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 是否存在角色
        /// </summary>
        /// <returns></returns>
        public bool Exists(int SystemCode)
        {
            return mc.KeyExists(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 角色
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(int SystemCode)
        {
            return String.Format("{0}-{1}", MemcachedKey.ROLE_BASIS, SystemCode);
        }

        /// <summary>
        /// 角色实体
        /// </summary>
        [Serializable]
        public class RoleBasisModel
        {
            public long Id { get; set; }

            public int RoleSort { get; set; }
            public string RoleCode { get; set; }
            
            public string RoleName { get; set; }

            public int IsSystem { get; set; }
        }
    }
}
