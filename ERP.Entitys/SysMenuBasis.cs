using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public partial class SysMenuBasis
    {
        public long Id { get; set; }
        public int MenuSort { get; set; }
        public string MenuIcon { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public long ParentId { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
