using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class CarBrandCache
    {
        MemcachedClient mc;
        public CarBrandCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Set(int SystemCode, List<CarBrandModel> list)
        {
            return mc.Set(this.GetCacheName(SystemCode), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public List<CarBrandModel> Get(int SystemCode)
        {
            var list = new List<CarBrandModel>();
            if (!this.Exists(SystemCode))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.CarBrand.Where(c => c.IsDelete == 0).OrderBy(c => c.BrandSort);
                if (tb != null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new CarBrandModel
                        {
                            Id = item.Id,
                            BrandSort=item.BrandSort,
                            BrandName=item.BrandName,
                            Initial=item.Initial
                        });
                    }
                    //保存缓存
                    this.Set(SystemCode, list);
                }
            }
            else
            {
                list = (List<CarBrandModel>)mc.Get(this.GetCacheName(SystemCode));
            }
            return list;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public bool Delete(int SystemCode)
        {
            return mc.Delete(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="SystemCode">系统编号</param>
        /// <returns></returns>
        public bool Exists(int SystemCode)
        {
            return mc.KeyExists(this.GetCacheName(SystemCode));
        }

        /// <summary>
        /// 缓存id
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(int SystemCode)
        {
            return String.Format("{0}-{1}", MemcachedKey.CAR_BRAND, SystemCode);
        }
        [Serializable]
        public class CarBrandModel
        {
            public long Id { get; set; }
            public int BrandSort { get; set; }
            public string BrandName { get; set; }
            public string Initial { get; set; }
        }
    }
}
