using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class CarInfoModels
    {
    public class CarInfoModel
        {
            public string Id { get; set; }
            /// <summary>
            /// 车辆编号 对应用户账号
            /// </summary>
            public string CarNo { get; set; }
            /// <summary>
            /// 车辆识别码
            /// </summary>
            public string VIN { get; set; }
            /// <summary>
            /// 行驶证
            /// </summary>
            public string VehicleLicense { get; set; }
            /// <summary>
            /// 车名
            /// </summary>
            public string CarName { get; set; }
            /// <summary>
            /// 车主报价
            /// </summary>
            public decimal RetailPrice { get; set; }
            /// <summary>
            /// 车型id
            /// </summary>
            public long TypeId { get; set; }
            /// <summary>
            /// 车型名称
            /// </summary>
            public string TypeName { get; set; }
            /// <summary>
            /// 品牌id
            /// </summary>
            public long BrandId { get; set; }
            /// <summary>
            /// 品牌名称
            /// </summary>
            public string BrandName { get; set; }
            /// <summary>
            /// 车系id
            /// </summary>
            public long SeriesId { get; set; }
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
            public byte ProductAddress { get; set; }
            /// <summary>
            /// 燃料 1汽油
            /// </summary>
            public byte Fuel { get; set; }
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
            public byte Transmission { get; set; }
            /// <summary>
            /// 里程表 
            /// </summary>
            public string Odometer { get; set; }
            /// <summary>
            /// 排放标准 4国四 5国五
            /// </summary>
            public byte EmissionStandards { get; set; }
            /// <summary>
            /// 检测报告
            /// </summary>
            public string TestReport { get; set; }
            /// <summary>
            /// 说明
            /// </summary>
            public string Remark { get; set; }
            /// <summary>
            /// 车源状态 0正常 1已售 2下架
            /// </summary>
            public byte CarStatus { get; set; }
            /// <summary>
            /// 审核状态 0默认 1通过 2失败
            /// </summary>
            public byte AuditStatus { get; set; }
            /// <summary>
            /// 审核说明
            /// </summary>
            public string AuditExplan { get; set; }
            /// <summary>
            /// 还价不多  1是 0否
            /// </summary>
            public byte IsRepay { get; set; }
            public string PhotoURL { get; set; }
            public DateTime CreateTime { get; set; }
            public List<PhotoModel> Photo { get; set; }
        }
        public class PhotoModel
        {
            public long Id { get; set; }
            public string URL { get; set; }
        }
    }
}