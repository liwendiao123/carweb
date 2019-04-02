using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class CarBrandProvider
    {
        UCMSContext db = new UCMSContext();
        public IEnumerable<CarBrand> GetList()
        {
            var brand = db.CarBrand.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete);
            return brand;
        }
        public IQueryable<CarBrand> GetList(Common.PagingModels paging, string Search)
        {
            var query = from a in db.CarBrand
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete
                        &&  (string.IsNullOrEmpty(Search) ? true : a.BrandName.Contains(Search))
                        select a;
            paging.totalrows = query.Count();
            query = query.OrderBy(c => c.BrandSort).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return query;
        }
        public int Edit(CarBrand model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<CarBrand>().Add(model);
            }
            else
            {
                db.Entry<CarBrand>(model).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.CarBrand.Where(c => ids.Contains(c.Id)).Update(r => new CarBrand { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
    }
}
