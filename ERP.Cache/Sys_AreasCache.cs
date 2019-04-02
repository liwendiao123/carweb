using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class Sys_AreasCache
    {
        MemcachedClient mc;
        public Sys_AreasCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="AreaId">id</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Set(long AreaId, List<AreasBasisModel> list)
        {
            return mc.Set(this.GetCacheName(AreaId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="AreaId">id</param>
        /// <returns></returns>
        public List<AreasBasisModel> Get(long AreaId)
        {
            var list = new List<AreasBasisModel>();
            if (!this.Exists(AreaId))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.SysAreas.Where(c => c.IsDelete == 0&&c.ParentId== AreaId);
                if (tb != null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new AreasBasisModel
                        {
                            AreaId=item.AreaId,
                            AreaName=item.AreaName,
                            ShortName=item.ShortName
                        });
                    }
                    //保存缓存
                    this.Set(AreaId, list);
                }
            }
            else
            {
                list = (List<AreasBasisModel>)mc.Get(this.GetCacheName(AreaId));
            }
            return list;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="AreaId">id</param>
        /// <returns></returns>
        public bool Delete(long AreaId)
        {
            return mc.Delete(this.GetCacheName(AreaId));
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="AreaId">id</param>
        /// <returns></returns>
        public bool Exists(long AreaId)
        {
            return mc.KeyExists(this.GetCacheName(AreaId));
        }

        /// <summary>
        /// 缓存id
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(long AreaId)
        {
            return String.Format("{0}-{1}", MemcachedKey.AREA, AreaId);
        }
        [Serializable]
        public class AreasBasisModel
        {
            public long AreaId { get; set; }
            public string AreaName { get; set; }
            public string ShortName { get; set; }
        }
    }
}
