using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class SysLoginLog
    {
        public int Id { get; set; }
        public string LoginIP { get; set; }
        public string LoginCode { get; set; }
        /// <summary>
        /// 1成功 0失败
        /// </summary>
        public byte LoginStatus { get; set; }
        public byte LoginType { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
