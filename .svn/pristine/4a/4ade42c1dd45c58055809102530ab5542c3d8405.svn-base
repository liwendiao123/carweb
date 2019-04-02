using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class CarTypeProvider
    {
        UCMSContext db = new UCMSContext();
        public IEnumerable<CarType> GetList()
        {
            var brand = db.CarType.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete);
            return brand;
        }
        public int Edit(CarType model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<CarType>().Add(model);
            }
            else
            {
                db.Entry<CarType>(model).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.CarType.Where(c => ids.Contains(c.Id)).Update(r => new CarType { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
    }
}
