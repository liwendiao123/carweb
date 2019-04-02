using UCMS.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Provider
{
    public class ShortMessageProvider
    {
        UCMSContext db = new UCMSContext();
        /// <summary>
        /// 当前手机发送次数
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int GetDateShort(string Mobile, DateTime date)
        {
            var quer = from a in db.SysShortMessage
                       where a.Mobile == Mobile && a.CreateTime > date
                       select a;
            return quer.Count();
        }
        public int VerifySMS(string mobile, string uuid, string verifycode)
        {
            var query = from a in db.SysShortMessage
                        where a.UUID == uuid && a.Mobile == mobile && a.VerifyCode == verifycode
                        select a;
            return 0;
        }
        public int Save(SysShortMessage model)
        {
            db.SysShortMessage.Add(model);
            return db.SaveChanges();
        }
    }
}
