using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    /// <summary>
    /// 车型
    /// </summary>
    public class CarType
    {
        public long Id { get; set; }
        public int TypeSort { get; set; }
        public string TypeName { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
