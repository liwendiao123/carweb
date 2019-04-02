using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class SysFileProvider
    {
        UCMSContext db = new UCMSContext();
        public IEnumerable<SysFile> GetList()
        {
            var file = db.SysFile.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete);
            return file;
        }
        public SysFile GetFile(long Id)
        {
            var file = db.SysFile.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete&&c.Id==Id).FirstOrDefault();
            return file;
        }
        public int Edit(SysFile model)
        {
            if (model.Id == 0)
            {
                model.Id = Common.PrimaryKey.GetHashCodeID;
                db.Set<SysFile>().Add(model);
            }
            else
            {
                if(!string.IsNullOrEmpty(model.FilePath))
                {
                    db.SysFile.Where(c => c.Id == model.Id).Update(r => new SysFile
                    {
                        FilePath=model.FilePath
                    });
                }
                return db.SysFile.Where(c => c.Id==model.Id).Update(r => new SysFile {
                    IsDelete = (int)Common.EnumModel.EIsDelete.NotDelete,
                    FileName=model.FileName,
                    TimeStamp=DateTime.Now
                });
            }
            return db.SaveChanges();
        }
        public int Delete(List<long> ids)
        {
            return db.SysFile.Where(c => ids.Contains(c.Id)).Update(r => new SysFile { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted });
        }
    }
}
