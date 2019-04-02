using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Cache
{
    public class CarSeriesCache
    {
        MemcachedClient mc;
        public CarSeriesCache()
        {
            mc = new MemcachedClient();
            mc.EnableCompression = false;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="BrandId">系统编号</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Set(long BrandId, List<CarSeriesModel> list)
        {
            return mc.Set(this.GetCacheName(BrandId), list, DateTime.Now.AddDays(7));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="BrandId">系统编号</param>
        /// <returns></returns>
        public List<CarSeriesModel> Get(long BrandId)
        {
            var list = new List<CarSeriesModel>();
            if (!this.Exists(BrandId))
            {
                var db = new UCMS.Entitys.UCMSContext();

                var tb = db.CarSeries.Where(c =>c.BrandId==BrandId&& c.IsDelete == 0).OrderBy(c => c.SeriesSort);
                if (tb != null && tb.Count() > decimal.Zero)
                {
                    foreach (var item in tb)
                    {
                        list.Add(new CarSeriesModel
                        {
                            Id = item.Id,
                            SeriesSort=item.SeriesSort,
                            SeriesName=item.SeriesName,
                            BrandId=item.BrandId,
                            TypeId=item.TypeId
                        });
                    }
                    //保存缓存
                    this.Set(BrandId, list);
                }
            }
            else
            {
                list = (List<CarSeriesModel>)mc.Get(this.GetCacheName(BrandId));
            }
            return list;
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="BrandId">系统编号</param>
        /// <returns></returns>
        public bool Delete(long BrandId)
        {
            return mc.Delete(this.GetCacheName(BrandId));
        }

        /// <summary>
        /// 是否存在缓存
        /// </summary>
        /// <param name="BrandId">系统编号</param>
        /// <returns></returns>
        public bool Exists(long BrandId)
        {
            return mc.KeyExists(this.GetCacheName(BrandId));
        }

        /// <summary>
        /// 缓存id
        /// </summary>
        /// <returns></returns>
        private string GetCacheName(long BrandId)
        {
            return String.Format("{0}-{1}", MemcachedKey.CAR_SERIES, BrandId);
        }
        [Serializable]
        public class CarSeriesModel
        {
            public long Id { get; set; }
            public long BrandId { get; set; }
            public long TypeId { get; set; }
            public int SeriesSort { get; set; }
            public string SeriesName { get; set; }
        }
    }
}
