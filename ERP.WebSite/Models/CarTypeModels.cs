using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class CarTypeModels
    {
        /// <summary>
        /// 车型
        /// </summary>
        public class CarTypeModel
        {
            public long Id { get; set; }
            public int TypeSort { get; set; }
            public string TypeName { get; set; }
        }
    }
}