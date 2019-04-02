using EntityFramework.Extensions;
using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class MenuBasisProvider
    {
        UCMSContext db = new UCMSContext();
        public IQueryable<SysMenuBasis> GetMenuList(string search, Common.PagingModels paging)
        {
            IQueryable<SysMenuBasis> model = db.SysMenuBasis.Where(c => c.IsDelete == 0 && c.MenuName.Contains(search)).OrderBy(c => c.MenuSort);
            paging.totalrows = model.Count();
            model = model.Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return model;
        }
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
        public int Delete(List<long>ids)
        {
            return db.SysMenuBasis.Where(c => ids.Contains(c.Id)).Update(r => new SysMenuBasis { IsDelete = 1 });
        }
    }
}
