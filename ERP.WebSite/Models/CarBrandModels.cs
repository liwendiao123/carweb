using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class CarBrandModels
    {
        public class CarBrandModel
        {
            public string Id { get; set; }
            public int BrandSort { get; set; }
            public string BrandName { get; set; }
            public string Initial { get; set; }
        }
    }
}