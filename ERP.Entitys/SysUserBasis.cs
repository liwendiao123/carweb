using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public class SysUserBasis
    {
        public long Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public int LoginCode { get; set; }
        public string Passwords { get; set; }
        public string RealName { get; set; }
        public byte Sex { get; set; }
        public string Mobile { get; set; }
        public string Weixin { get; set; }
        public int QQ { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime LastTime { get; set; }
        public string Photo { get; set; }
        public string Remark { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
