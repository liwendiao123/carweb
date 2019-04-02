using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_MenuBasisCache
    {
        MemcachedClient mc;
        public Sys_MenuBasisCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加系统菜单缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <param name="list">菜单列表</param>
        /// <returns></returns>
        public bool Set(int SystemCode, List<MenuBasisModel> list)
        {
            return mc.Set(this.GetCacheName(SystemCode), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取系统菜单缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public List<MenuBasisModel> Get(int SystemCode)
        {
            var list = new List<MenuBasisModel>();
            if (!this.Exists(SystemCode))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.SysMenuBasis.Where(c => c.IsDelete == 0).OrderBy(c => c.MenuSort);
                if (tb!=null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new MenuBasisModel
                        {
                            Id = item.Id,
                            MenuName = item.MenuName,
                            MenuSort=item.MenuSort,
                            MenuIcon=item.MenuIcon,
                            ParentId=item.ParentId,
                            ActionName=item.ActionName,
                            ControllerName=item.ControllerName
                        });
                    }
                    //保存缓存
                    this.Set(SystemCode, list);
                }
            }
            else
            {
                list = (List<MenuBasisModel>)mc.Get(this.GetCacheName(SystemCode));
            }
            return list;
        }

        /// <summary>
        /// 删除菜单缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public bool Delete(int SystemCode)
        {
            return mc.Delete(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 是否存在菜单缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public bool Exists(int SystemCode)
        {
            return mc.KeyExists(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 系统菜单
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(int SystemCode)
        {
            return String.Format("{0}-{1}", MemcachedKey.MENU_BASIS, SystemCode);
        }

        [Serializable]
        public class MenuBasisModel
        {
            public long Id { get; set; }
            public int MenuSort { get; set; }
            public string MenuIcon { get; set; }
            public string MenuName { get; set; }
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
            public long ParentId { get; set; }
        }
    }
}
