using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    public class CarPhoto
    {
        public long Id { get; set; }
        public long CarId { get; set; }
        /// <summary>
        /// 图片类型 1外观 2中控 3座椅 4细节 5特点
        /// </summary>
        public byte PhotoType { get; set; }
        /// <summary>
        /// 1主图
        /// </summary>
        public byte PhotoStatus { get; set; }
        public string PhotoURL { get; set; }
        public byte IsDelete { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
