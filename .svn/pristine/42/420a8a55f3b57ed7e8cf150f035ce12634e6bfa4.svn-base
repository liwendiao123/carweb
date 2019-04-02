using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_MenuOperateCache
    {
        MemcachedClient mc;
        public Sys_MenuOperateCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加菜单操作码缓存
        /// </summary>
        /// <param name="MenuId">菜单id</param>
        /// <param name="list">菜单列表</param>
        /// <returns></returns>
        public bool Set(long MenuId, List<MenuOperateModel> list)
        {
            return mc.Set(this.GetCacheName(MenuId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取菜单操作码缓存
        /// </summary>
        /// <param name="MenuId">菜单id</param>
        /// <returns></returns>
        public List<MenuOperateModel> Get(long MenuId)
        {
            var list = new List<MenuOperateModel>();
            if (!this.Exists(MenuId))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.SysMenuOperate.Where(c => c.IsDelete == 0&&c.MenuId== MenuId).OrderBy(c=>c.OperateSort);
                if (tb != null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new MenuOperateModel
                        {
                            Id = item.Id,
                            OperateSort=item.OperateSort,
                            MenuId=item.MenuId,
                            OperateCode=item.OperateCode,
                            OperateName=item.OperateName
                        });
                    }
                    //保存缓存
                    this.Set(MenuId, list);
                }
            }
            else
            {
                list = (List<MenuOperateModel>)mc.Get(this.GetCacheName(MenuId));
            }
            return list;
        }

        /// <summary>
        /// 删除菜单缓存
        /// </summary>
        /// <param name="MenuId">菜单id</param>
        /// <returns></returns>
        public bool Delete(long MenuId)
        {
            return mc.Delete(this.GetCacheName(MenuId));
        }

        /// <summary>
        /// 是否存在菜单缓存
        /// </summary>
        /// <param name="MenuId">菜单id</param>
        /// <returns></returns>
        public bool Exists(long MenuId)
        {
            return mc.KeyExists(this.GetCacheName(MenuId));
        }

        /// <summary>
        /// 系统菜单
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(long MenuId)
        {
            return String.Format("{0}-{1}", MemcachedKey.MENU_BASIS, MenuId);
        }

        [Serializable]
        public class MenuOperateModel
        {
            public long Id { get; set; }
            public int OperateSort { get; set; }
            public string OperateName { get; set; }
            public string OperateCode { get; set; }
            public long MenuId { get; set; }
        }
    }
}