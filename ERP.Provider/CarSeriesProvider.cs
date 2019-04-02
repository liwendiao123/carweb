using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class CarSeriesProvider
    {
        UCMSContext db = new UCMSContext();
        public IEnumerable<CarSeries> GetList()
        {
            var brand = db.CarSeries.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete);
            return brand;
        }
        public int Edit(CarSeries model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<CarSeries>().Add(model);
            }
            else
            {
                db.Entry<CarSeries>(model).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.CarSeries.Where(c => ids.Contains(c.Id)).Update(r => new CarSeries { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
    }
}
