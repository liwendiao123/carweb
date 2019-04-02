using EntityFramework.Extensions;
using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
   public  class RoleBasisProvider
    {
        UCMSContext db = new UCMSContext();
        public int Edit(SysMenuBasis model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<SysMenuBasis>().Add(model);
            }
            else
            {
                db.Entry<SysMenuBasis>(model).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.SysMenuBasis.Where(c => ids.Contains(c.Id)).Update(r => new SysMenuBasis { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
        public int SetMenu(List<SysRoleMenu> roleMenu, List<SysRoleOperate> roleOperate)
        {
            int line = 0;
            var mAdd = roleMenu.Where(c => c.Id == 0).ToList();
            var mDelete = roleMenu.Where(c => c.Id != 0).Select(c => c.Id).ToList();
            foreach (var item in mAdd)
            {
                item.Id = Common.PrimaryKey.GetHashCodeID;

                db.SysRoleMenu.Add(item);
            }
            if(mDelete.Count()>0)
            {
                line+=db.SysRoleMenu.Where(c => mDelete.Contains(c.Id)).Update(c => new SysRoleMenu { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted, TimeStamp = DateTime.Now });
            }

            var oAdd = roleOperate.Where(c => c.Id == 0).ToList();
            var oDelete = roleOperate.Where(c => c.Id != 0).Select(c => c.Id).ToList();
            foreach (var item in oAdd)
            {
                item.Id = Common.PrimaryKey.GetHashCodeID;

                db.SysRoleOperate.Add(item);
            }
            if (oDelete.Count() > 0)
            {
                line+=db.SysRoleOperate.Where(c => oDelete.Contains(c.Id)).Update(c => new SysRoleOperate { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted, TimeStamp = DateTime.Now });
            }
            line += db.SaveChanges();
            if (line == (roleMenu.Count() + roleOperate.Count()))
            {
                line = 1;
            }
            else
            {
                line = 0;
            }
            return line;
        }
    }
}
