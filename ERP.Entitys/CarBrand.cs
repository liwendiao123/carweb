using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class CarBrand
    {
        public long Id { get; set; }
        public int BrandSort { get; set; }
        public string BrandName { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string Initial { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
