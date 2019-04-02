using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class RoleBasisModels
    {
        public class RoleBasisModel
        {
            public long Id { get; set; }
            public int RoleSort { get; set; }
            public string RoleCode { get; set; }
            public string RoleName { get; set; }
            public int IsSystem { get; set; }
        }
        public class RoleMenuModel
        {
            public long Id { get; set; }
            public long MenuId { get; set; }
            public long ParentId { get; set; }
            public string MenuName { get; set; }
            public bool IsSelect { get; set; }
            public List<RoleOperateModel> OperateList { get; set; }
        }
        public class RoleOperateModel
        {
            public long Id { get; set; }
            public long MenuId { get; set; }
            public long OperateId { get; set; }
            public string OperateName { get; set; }
            public bool IsSelect { get; set; }
        }
    }
}