using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCMS.WebSite.UserControl
{
    public class SelectItem
    {
        /// <summary>
        /// 一级菜单下拉
        /// </summary>
        /// <param name="MenuId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> MenuItem(long MenuId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--请选择--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.Sys_MenuBasisCache().Get(UCMS.Common.FormsTicket.SystemCode).Where(c=>c.ParentId==UCMS.Common.Constant.LONG_DEFAULT);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.MenuName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == MenuId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }

        /// <summary>
        /// 区域下拉框
        /// </summary>
        /// <param name="AreaId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> AreaItem(long AreaId, bool IsShowAll, bool IsSelect = false,long SelectId=-1)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = "-1", Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--请选择--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.Sys_AreasCache().Get(AreaId);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.ShortName,
                    Value = item.AreaId.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == SelectId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }

        /// <summary>
        /// 车型下拉框
        /// </summary>
        /// <param name="TypeId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarTypeItem(long TypeId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--请选择--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.CarTypeCache().Get(Common.FormsTicket.SystemCode);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.TypeName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == TypeId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// 品牌下拉框
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarBrandItem(long BrandId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--请选择--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.CarBrandCache().Get(Common.FormsTicket.SystemCode).OrderBy(c=>c.Initial);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Initial + " " + item.BrandName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == BrandId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
        /// <summary>
        /// 车系下拉框
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> CarSeriesItem(long BrandId,long SeriesId, bool IsShowAll, bool IsSelect = false)
        {
            var list = new List<SelectListItem>();
            if (IsShowAll)
            {
                list.Insert(0, new SelectListItem() { Text = "--全部--", Value = UCMS.Common.Constant.LONG_DEFAULT.ToString(), Selected = false });
            }
            if (IsSelect)
            {
                list.Insert(0, new SelectListItem() { Text = "--请选择--", Value = string.Empty, Selected = false });
            }
            var tb = new Cache.CarSeriesCache().Get(BrandId);
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.SeriesName,
                    Value = item.Id.ToString()
                });
            }
            foreach (var item in list)
            {
                if (item.Value == SeriesId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
    }
}