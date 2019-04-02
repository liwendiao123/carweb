﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class DefaultModels
    {
        public class CarInfoModel
        {
            public string Id { get; set; }
            /// <summary>
            /// 车辆编号 对应用户账号
            /// </summary>
            public string CarNo { get; set; }
            /// <summary>
            /// 车名
            /// </summary>
            public string CarName { get; set; }
            /// <summary>
            /// 车主报价
            /// </summary>
            public decimal RetailPrice { get; set; }
            /// <summary>
            /// 车型名称
            /// </summary>
            public string TypeName { get; set; }
            /// 品牌名称
            /// </summary>
            public string BrandName { get; set; }
            /// <summary>
            /// 车系名称
            /// </summary>
            public string SeriesName { get; set; }
            /// <summary>
            /// 上牌时间
            /// </summary>
            public DateTime LicenseTime { get; set; }
            /// <summary>
            /// 国内过外
            /// </summary>
            public string ProductAddress { get; set; }
            /// <summary>
            /// 燃料 1汽油
            /// </summary>
            public string Fuel { get; set; }
            /// <summary>
            /// 颜色
            /// </summary>
            public string CarColor { get; set; }
            /// <summary>
            /// 排气量
            /// </summary>
            public string SweptVolume { get; set; }
            /// <summary>
            /// 变速器 0手动 1自动 2手自一体
            /// </summary>
            public string Transmission { get; set; }
            /// <summary>
            /// 里程表 
            /// </summary>
            public string Odometer { get; set; }
            /// <summary>
            /// 排放标准 4国四 5国五
            /// </summary>
            public string EmissionStandards { get; set; }
            /// <summary>
            /// 说明
            /// </summary>
            public string Remark { get; set; }
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string Contact { get; set; }
            public string IsRepay { get; set; }
            public string PhotoURL { get; set; }
            public List<PhotoModel> Photo { get; set; }
        }
        public class PhotoModel
        {
            public long Id { get; set; }
            public string URL { get; set; }
        }
        /// <summary>
        /// 车型
        /// </summary>
        public class CarTypeModel
        {
            public long Id { get; set; }
            public int TypeSort { get; set; }
            public string TypeName { get; set; }
            public List<CarInfoModel> CarInfo { get; set; }
        }

        /// <summary>
        /// 车型
        /// </summary>
        public class CarType
        {
            public long Id { get; set; }
            public string TypeName { get; set; }
            public IEnumerable<TypeInfo> TypeInfo { get; set; }
        }
        public class TypeInfo
        {
            public long Id { get; set; }
            public string SericeName { get; set; }
            public long ParentId { get; set; }
            public int Sort { get; set; }
        }
    }
}