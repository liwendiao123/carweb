using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCMS.Entitys;

namespace UCMS.Provider
{
    public class CarInfoProvider
    {
        private UCMSContext db = new UCMSContext();
        public CarInfo GetCarById(long Id)
        {
            var query = from a in db.CarInfo
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && a.Id==Id
                        select a;
            return query.FirstOrDefault();
        }
        public IQueryable<CarPhoto> GetCarPhotoById(long CarId)
        {
            var query = from a in db.CarPhoto
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && a.CarId == CarId
                        select a;
            return query.OrderBy(c=>c.TimeStamp);
        }
        public IQueryable<CarInfo> GetList(Common.PagingModels paging, long UserId, long? TypeId, string Search)
        {
            var query = from a in db.CarInfo
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete
                        && (TypeId != null ? a.TypeId == TypeId.Value : true) && (UserId==0 ? true : a.UserId == UserId) && (string.IsNullOrEmpty(Search) ? true : (a.BrandName.Contains(Search) || a.TypeName.Contains(Search) || a.SeriesName.Contains(Search)))
                        select a;
            paging.totalrows = query.Count();
            query = query.OrderByDescending(c => c.CreateTime).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return query;
        }
        /// <summary>
        /// 首页查询只取前五
        /// </summary>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public IQueryable<CarInfo> GetList(long TypeId)
        {
            return db.CarInfo.Where(c => c.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && c.AuditStatus == (int)Common.EnumModel.EAuditStatus.Ok && c.TypeId == TypeId).OrderByDescending(c => c.CreateTime).Take(5);
        }
        public IQueryable<CarInfo> GetAuditList(Common.PagingModels paging, string Search)
        {
            var query = from a in db.CarInfo
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && a.AuditStatus== (int)Common.EnumModel.EAuditStatus.Normal
                        && (string.IsNullOrEmpty(Search) ? true : (a.BrandName.Contains(Search) || a.TypeName.Contains(Search) || a.SeriesName.Contains(Search)))
                        select a;
            paging.totalrows = query.Count();
            query = query.OrderByDescending(c => c.CreateTime).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return query;
        }
        public int Edit(CarInfo model,List<CarPhoto> photo,bool IsAdd,string ImgDelete)
        {
            //TODO:暂时不用审核
            //model.AuditStatus = 1;
            foreach (var item in photo)
            {
                db.Set<CarPhoto>().Add(item);
            }
            if (IsAdd)
            {
                db.Set<CarInfo>().Add(model);
            }
            else
            {
                if (!string.IsNullOrEmpty(ImgDelete))
                {
                    //删除图片
                    var ids = new List<long>();
                    foreach (var item in ImgDelete.Split(new char[] { ',' }))
                    {
                        ids.Add(Common.ToolHelper.ConvertToLong(item));
                    }
                    db.CarPhoto.Where(c => ids.Contains(c.Id)).Update(r => new CarPhoto { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted, TimeStamp = DateTime.Now });
                }
                //修改行驶证
                if (!string.IsNullOrEmpty(model.VehicleLicense))
                {
                    db.CarInfo.Where(c => c.Id == model.Id).Update(r => new CarInfo
                    {
                        VehicleLicense = model.VehicleLicense,
                    });
                }
                //修改检测报告
                if (!string.IsNullOrEmpty(model.TestReport))
                {
                    db.CarInfo.Where(c => c.Id == model.Id).Update(r => new CarInfo
                    {
                        TestReport = model.TestReport,
                    });
                }
                //修改主图
                if (!string.IsNullOrEmpty(model.PhotoURL))
                {
                    db.CarInfo.Where(c => c.Id == model.Id).Update(r => new CarInfo
                    {
                        PhotoURL = model.PhotoURL,
                    });
                }
                db.SaveChanges();
                return db.CarInfo.Where(c => c.Id == model.Id).Update(r => new CarInfo
                {
                    SeriesId = model.SeriesId,
                    SeriesName = model.SeriesName,
                    SweptVolume = model.SweptVolume,
                    ProductAddress = model.ProductAddress,
                    BrandId = model.BrandId,
                    BrandName = model.BrandName,
                    CarColor = model.CarColor,
                    CarName = model.CarName,
                    EmissionStandards = model.EmissionStandards,
                    Fuel = model.Fuel,
                    LicenseTime = model.LicenseTime,
                    Odometer = model.Odometer,
                    Remark = model.Remark,
                    RetailPrice = model.RetailPrice,
                    Transmission = model.Transmission,
                    TypeId = model.TypeId,
                    TypeName = model.TypeName,
                    VIN = model.VIN,
                    IsRepay=model.IsRepay,
                    TimeStamp = DateTime.Now,
                });
            }
            return db.SaveChanges();
        }
        
        public int SoldOut(long Id,Common.EnumModel.ECarStatus status)
        {
            return db.CarInfo.Where(c => c.Id==Id).Update(r => new CarInfo { CarStatus= (byte)status, TimeStamp = DateTime.Now });
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int Audit(CarInfo model)
        {
            return db.CarInfo.Where(c => c.Id == model.Id).Update(r => new CarInfo
            {
                AuditStatus = model.AuditStatus,
                AuditExplan = model.AuditExplan,
                AuditPerson=model.AuditPerson,
                AuditTime=DateTime.Now,
                TimeStamp = DateTime.Now
            });
        }
        public int Delete(List<long> ids)
        {
            return db.CarInfo.Where(c => ids.Contains(c.Id)).Update(r => new CarInfo { IsDelete = (int)Common.EnumModel.EIsDelete.Deleted, TimeStamp = DateTime.Now });
        }
        /// <summary>
        /// 我的车源
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="TypeId"></param>
        /// <param name="Search"></param>
        /// <returns></returns>
        public IEnumerable<CarInfo> GetList(Common.PagingModels paging,long TypeId,string Search)
        {
            var query = from a in db.CarInfo
                        where a.IsDelete == (int)Common.EnumModel.EIsDelete.NotDelete && a.UserId == Common.FormsTicket.UserId
                        && (TypeId >0 ? a.TypeId == TypeId : true) && (string.IsNullOrEmpty(Search) ? 1==1 : (a.BrandName.Contains(Search) || a.TypeName.Contains(Search) || a.SeriesName.Contains(Search)))
                        select a;
            paging.totalrows = query.Count();
            query = query.OrderByDescending(c => c.CreateTime).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            return query;
        }
        /// <summary>
        /// 前台查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable<CarInfo> GetList(ListModel model, Common.PagingModels paging)
        {
            //明明已经提供了参数还报错 是因为0比较特殊需要加‘’
            //参数化查询 '(@XX)select * from XX where XX=XX' 需要参数 '@XX'，但未提供该参数。
            var param = new List<SqlParameter>();
            var param1 = new List<SqlParameter>();
            var sql = " and AuditStatus=1";
            if (model.TypeId>0&&model.TypeId!=Common.Constant.LONG_DEFAULT)
            {
                sql += " and TypeId="+ model.TypeId;
            }
            if (!string.IsNullOrEmpty(model.CarNo))
            {
                sql += " and CarNo=@CarNo";
                param.Add(new SqlParameter("CarNo", model.CarNo));
                param1.Add(new SqlParameter("CarNo", model.CarNo));
            }
            if (model.RetailPrice1>0)
            {
                sql += " and RetailPrice>="+ model.RetailPrice1;
            }
            if (model.RetailPrice2 > 0)
            {
                sql += " and RetailPrice<=" + model.RetailPrice2;
            }
            if (!string.IsNullOrEmpty(model.Transmission)&&model.Transmission!="-1")
            {
                sql += " and Transmission="+ model.Transmission;
            }
            if (model.BrandId > 0 && model.BrandId != Common.Constant.LONG_DEFAULT)
            {
                sql += " and BrandId=" + model.BrandId;
            }
            if (model.SeriesId > 0 && model.SeriesId != Common.Constant.LONG_DEFAULT)
            {
                sql += " and SeriesId=" + model.SeriesId;
            }
            if (model.LicenseTime1 != Common.ToolHelper.MinDateTime)
            {
                sql += " and LicenseTime>='" + model.LicenseTime1+"'";
            }
            if (model.LicenseTime2 != Common.ToolHelper.MinDateTime)
            {
                sql += " and LicenseTime<='" + model.LicenseTime2+"'";
            }
            if (!string.IsNullOrEmpty(model.Search))
            {
                sql += " and (BrandName like @Search or SeriesName  like @Search)";
                param.Add(new SqlParameter("Search", "%" + model.Search + "%"));
                param1.Add(new SqlParameter("Search", "%" + model.Search + "%"));
            }
            var cd = db.Database.SqlQuery<int>("select count(*) from CarInfo where IsDelete=0" + sql, param1.ToArray());
            paging.totalrows = cd.FirstOrDefault();

            var str = "select*from(select row_number()";
            switch(paging.sort)
            {
                case 1:
                    str += " over(order by RetailPrice)";
                    break;
                case 2:
                    str += " over(order by RetailPrice desc)";
                    break;
                case 3:
                    str += " over(order by CreateTime)";
                    break;
                case 4:
                    str += " over(order by CreateTime desc)";
                    break;
                default:
                    str += " over(order by CreateTime desc)";
                    break;
            }
            str += "row_num,* from CarInfo where IsDelete=0" + sql + ") as list where list.row_num>" + ((paging.page - 1) * 50) + " and list.row_num<=" + (paging.page * 50) + " ";
            var query = db.Database.SqlQuery<CarInfo>(str, param.ToArray());

            return query;
        }
        public class ListModel
        {
            public long TypeId { get; set; }
            public string CarNo { get; set; }
            public decimal RetailPrice1 { get; set; }
            public decimal RetailPrice2 { get; set; }
            public string Transmission { get; set; }
            public long BrandId { get; set; }
            public long SeriesId { get; set; }
            public DateTime LicenseTime1 { get; set; }
            public DateTime LicenseTime2 { get; set; }
            public string Search { get; set; }
        }
        /// <summary>
        /// 移动端
        /// </summary>
        /// <param name="CarType"></param>
        /// <param name="UseYears"></param>
        /// <param name="HopePrice"></param>
        /// <param name="page"></param>
        /// <param name="Search"></param>
        /// <returns></returns>
        public IEnumerable<CarInfo> GetList(long CarType, int UseYears,int HopePrice,int page,string Search)
        {
            //明明已经提供了参数还报错 是因为0比较特殊需要加‘’
            //参数化查询 '(@XX)select * from XX where XX=XX' 需要参数 '@XX'，但未提供该参数。
            var param = new List<SqlParameter>();
            var sql = " and AuditStatus=1";
            if (CarType > 0 && CarType != Common.Constant.LONG_DEFAULT)
            {
                sql += " and TypeId=" + CarType;
            }
            if (HopePrice > 0)
            {
                switch (HopePrice)
                {
                    case 1:
                        sql += " and RetailPrice<5";//5万以下
                        break;
                    case 2:
                        sql += " and RetailPrice>4.9 and RetailPrice<10";//5-10万
                        break;
                    case 3:
                        sql += " and RetailPrice>9.9 and RetailPrice<15";//10-15万
                        break;
                    case 4:
                        sql += " and RetailPrice>14.9 and RetailPrice<25";//15-25万
                        break;
                    case 5:
                        sql += " and RetailPrice>24.9 and RetailPrice<40";//25-40万
                        break;
                    case 6:
                        sql += " and RetailPrice>39.9 and RetailPrice<50";//40-50万
                        break;
                    case 7:
                        sql += " and RetailPrice>50";//50万以上
                        break;
                    default:
                        break;
                }
            }
            if (UseYears > 0)
            {
                switch (UseYears)
                {
                    case 1:
                        sql += " and LicenseTime>='" + DateTime.Now.AddYears(-1) + "'";//1年内
                        break;
                    case 2:
                        sql += " and LicenseTime<='" + DateTime.Now.AddYears(-1) + "' and LicenseTime>='" + DateTime.Now.AddYears(-2) + "'";//1-2年
                        break;
                    case 3:
                        sql += " and LicenseTime<='" + DateTime.Now.AddYears(-3) + "' and LicenseTime>='" + DateTime.Now.AddYears(-5) + "'";//3-5年
                        break;
                    case 4:
                        sql += " and LicenseTime<='" + DateTime.Now.AddYears(-5) + "'";//5年以上
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(Search))
            {
                sql += " and (BrandName like @Search or SeriesName  like @Search)";
                param.Add(new SqlParameter("Search", "%" + Search + "%"));
            }
            var str = "select*from(select row_number() over(order by CreateTime desc)";
            str += "row_num,* from CarInfo where IsDelete=0" + sql + ") as list where list.row_num>" + ((page - 1) * 10) + " and list.row_num<=" + (page * 10) + " ";
            var query = db.Database.SqlQuery<CarInfo>(str, param.ToArray());
            return query;
        }

    }
}
