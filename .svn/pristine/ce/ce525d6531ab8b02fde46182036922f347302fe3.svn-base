using EntityFramework.Extensions;
using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class MenuOperateProvider
    {
        UCMSContext db = new UCMSContext();
        public int Edit(SysMenuOperate model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<SysMenuOperate>().Add(model);
            }
            else
            {
                db.Entry<SysMenuOperate>(model).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.SysMenuOperate.Where(c => ids.Contains(c.Id)).Update(r => new SysMenuOperate { IsDelete = 1 });
        }
    }
}
