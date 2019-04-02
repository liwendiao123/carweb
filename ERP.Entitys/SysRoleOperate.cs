using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public class SysRoleOperate
    { 
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long OperateId { get; set; }
        public long MenuId { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
