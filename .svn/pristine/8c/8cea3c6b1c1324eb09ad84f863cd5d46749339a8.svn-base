using EntityFramework.Extensions;
using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class AreasProvider
    {
        private UCMSContext db = new UCMSContext();
        public IQueryable<SysAreas> GetListById(List<long> ids)
        {
            var query = from a in db.SysAreas
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && ids.Contains(a.AreaId)
                        select a;
            return query;
        }
        public int Edit(SysAreas model)
        {
            if (model.Id == 0)
            {
                db.Set<SysAreas>().Add(model);
            }
            else
            {
                return db.SysAreas.Where(c => c.Id == model.Id).Update(r => new SysAreas
                {
                    ShortName=model.ShortName,
                    AreaId=model.AreaId,
                    AreaLevel=model.AreaLevel,
                    ParentId=model.ParentId,
                    AreaName = model.AreaName,
                    TimeStamp = model.TimeStamp,
                });
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.SysAreas.Where(c => ids.Contains(c.Id)).Update(r => new SysAreas { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted, TimeStamp = DateTime.Now });
        }
    }
}
