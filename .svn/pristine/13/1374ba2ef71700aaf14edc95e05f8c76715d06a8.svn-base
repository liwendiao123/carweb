using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class SysHotSearchProvider
    {
        UCMSContext db = new UCMSContext();
        public IEnumerable<SysHotSearch> GetList()
        {
            var brand = db.SysHotSearch.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete);
            return brand;
        }
        public int Edit(long BrandId, long SeriesId)
        {
            if (SeriesId > 0)
            {
                var ser = db.SysHotSearch.Where(c => c.SearchId == SeriesId).FirstOrDefault();
                if (ser == null)
                {
                    //新增
                    var entity = new Entitys.SysHotSearch
                    {
                        IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                        SearchId = SeriesId,
                        TimeStamp = DateTime.Now,
                        ParentId = BrandId
                    };
                    db.SysHotSearch.Add(entity);
                }
                else
                {
                    if (ser.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete)
                    {
                        ser.IsDelete = (int)Common.EnumModel.EIsDelete.Deleted;
                    }
                    else
                    {
                        ser.IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete;
                    }
                    ser.TimeStamp = DateTime.Now;
                }
            }
            else
            {
                var brand = db.SysHotSearch.Where(c => c.SearchId == BrandId).FirstOrDefault();
                if (brand == null)
                {
                    //新增
                    var entity = new Entitys.SysHotSearch
                    {
                        IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                        SearchId = BrandId,
                        TimeStamp = DateTime.Now,
                        ParentId = 0
                    };
                    db.SysHotSearch.Add(entity);
                }
                else
                {
                    if (brand.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete)
                    {
                        brand.IsDelete = (int)Common.EnumModel.EIsDelete.Deleted;
                    }
                    else
                    {
                        brand.IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete;
                    }
                    brand.TimeStamp = DateTime.Now;
                }
            }
            return db.SaveChanges();
        }
    }
}
