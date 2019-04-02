using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public partial class SysMenuOperate
    {
        public long Id { get; set; }
        public int OperateSort { get; set; }
        public string OperateName { get; set; }
        public string OperateCode { get; set; }
        public long MenuId { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
