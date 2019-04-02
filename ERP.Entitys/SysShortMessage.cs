using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Entitys
{
    /// <summary>
    /// 短信验证码
    /// </summary>
    public class SysShortMessage
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        [System.ComponentModel.DataAnnotations.Key]
        public string UUID { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
