using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UCMS.Common
{
    public class EnumHelper
    {
        private static IDictionary<string, Type> listType = null;

        /// <summary>
        /// 注册枚举值类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <returns></returns>
        public static void Reg<T>(T en)
        {
            listType = listType==null ? new Dictionary<string, Type>() : listType;
            Type type = typeof(T);
            string name = type.Name;
            if (!listType.ContainsKey(name))
            {
                listType.Add(name, type);
            }
        }

        /// <summary>
        /// 注册枚举值类型
        /// </summary>
        /// <param name="type"></param>
        public static void Reg(params Type[] types)
        {
            listType = listType==null ? new Dictionary<string, Type>() : listType;
            if (types != null)
            {
                foreach (Type type in types)
                {
                    string name = type.Name;
                    if (!listType.ContainsKey(name))
                    {
                        listType.Add(name, type);
                    }
                }
            }
        }
        /// <summary>
        /// 获得js内容
        /// </summary>
        /// <returns></returns>
        public static string GetJs()
        {
            StringBuilder sb = new StringBuilder();
            if (listType != null)
            {
                foreach (Type type in listType.Values)
                {
                    sb.AppendFormat("var {0}={1};", type.Name,Newtonsoft.Json.JsonConvert.SerializeObject((GetEnumList(type))));
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取enum集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<ReadEnum> GetEnumList(Type type)
        {
            var list = new List<ReadEnum>();
            foreach (int num in Enum.GetValues(type))
            {
                var readEnum = new ReadEnum();
                readEnum.Value = num.ToString();
                string name = Enum.GetName(type, num);
                readEnum.Name = name;
                var field = type.GetField(name);
                object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (customAttributes.Length > 0)
                {
                    DescriptionAttribute descriptionAttribute = customAttributes[0] as DescriptionAttribute;
                    if (descriptionAttribute != null)
                    {
                        readEnum.Description = descriptionAttribute.Description;
                    }
                }
                list.Add(readEnum);
            }
            return list;
        }
        public static string GetEnumName<T>(byte type)
        {
            var list = GetEnumList(typeof(T));
            foreach (var item in list)
            {
                if (item.Value == type.ToString())
                {
                    return item.Description;
                }
            }
            return "";
        }
        /// <summary>
        /// 获取Enum下拉框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <param name="IsShowAll"></param>
        /// <param name="IsSelect"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumItem<T>(int Value, bool IsShowAll, bool IsSelect = false)
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
            var tb = Common.EnumHelper.GetEnumList(typeof(T));
            foreach (var item in tb)
            {
                list.Add(new SelectListItem
                {
                    Text = item.Description,
                    Value = item.Value
                });
            }
            foreach (var item in list)
            {
                if (item.Value == Value.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
            return list;
        }
    }

    public class ReadEnum
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
    
}
