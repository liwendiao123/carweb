using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class HotSearchModels
    {
        public class CarBrand
        {

            public long BrandId { get; set; }
            public string BrandhName { get; set; }
            public bool IsSelect { get; set; }
            public List<CarSeries> info { get; set; }
        }
        public class CarSeries
        {
            public long SeriesId { get; set; }
            public string SeriesName { get; set; }
            public bool IsSelect { get; set; }
        }
    }
}