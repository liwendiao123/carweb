using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class CarSeriesModels
    {
        /// <summary>
        /// 车系
        /// </summary>
        public class CarSeriesModel
        {
            public long Id { get; set; }
            public long BrandId { get; set; }
            public long TypeId { get; set; }
            public int SeriesSort { get; set; }
            public string SeriesName { get; set; }
        }
    }
}