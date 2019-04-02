using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    /// <summary>
    /// 通知公告
    /// </summary>
    public class SysNotice
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 是否主要通告 1是 只有一个主要通告
        /// </summary>
        public byte NoticeType { get; set; }
        public byte IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
