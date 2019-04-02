using EntityFramework.Extensions;
using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class UserBasisProvider
    {
        private UCMSContext db = new UCMSContext();
        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SysUserBasis GetUser(long UserId)
        {
            var user = db.SysUserBasis.Single(c => c.IsDelete == 0 && c.Id == UserId);
            return user;
        }
        public bool IsSystem(long UserId)
        {
            var query = from a in db.SysRoleBasis
                        from b in db.SysRoleUser
                        where a.Id == b.RoleId && b.UserId == UserId&&a.IsDelete ==(int)Common.EnumModel.EIsDelete.NotDelete
                        select new { a.RoleCode };
            foreach (var item in query)
            {
                if (item.RoleCode.ToLower() == "system")
                {
                    return true;
                }
            }
            return false;
        }
        public IQueryable<SysUserBasis> GetList(string search, Common.PagingModels paging)
        {
            IQueryable<SysUserBasis> model = db.SysUserBasis.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && c.RealName.Contains(search)).OrderBy(c=>c.Id);
            paging.totalrows = model.Count();
            model = model.Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return model;
        }
        public int Edit(SysUserBasis model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<SysUserBasis>().Add(model);
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Photo))
                {
                    db.SysUserBasis.Where(c => c.Id == model.Id).Update(r => new SysUserBasis {Photo=model.Photo });
                }
                return db.SysUserBasis.Where(c => c.Id == model.Id).Update(r => new SysUserBasis
                {
                    Sex = model.Sex,
                    Mobile = model.Mobile,
                    RealName = model.RealName,
                    LastTime=model.LastTime,
                    QQ=model.QQ,
                    Weixin=model.Weixin,
                    Remark=model.Remark,
                    TimeStamp = DateTime.Now,
                });
            }
            return db.SaveChanges();
        }
        public int UpdatePwd(string pwd,long UserId)
        {
            return db.SysUserBasis.Where(c => c.Id == UserId).Update(r => new SysUserBasis { TimeStamp = DateTime.Now, Passwords = pwd });
        }
        public int Delete(List<long> ids)
        {
            return db.SysUserBasis.Where(c => ids.Contains(c.Id)).Update(r => new SysUserBasis { TimeStamp = DateTime.Now, IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
        public int SetRole(List<SysRoleUser> list)
        {
            int line = 0;
            var uAdd = list.Where(c => c.Id == 0).ToList();
            var uDelete = list.Where(c => c.Id != 0).Select(c => c.Id).ToList();
            foreach (var item in uAdd)
            {
                item.Id = Common.PrimaryKey.GetHashCodeID;

                db.SysRoleUser.Add(item);
            }
            if (uDelete.Count() > 0)
            {
                line += db.SysRoleUser.Where(c => uDelete.Contains(c.Id)).Update(c => new SysRoleUser { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
            }
            line += db.SaveChanges();
            if (line == (uAdd.Count() + uDelete.Count()))
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
