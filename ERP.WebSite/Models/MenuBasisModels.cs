using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCMS.WebSite.Models
{
    public class MenuBasisModels
    {
        public class MenuBasisModel
        {
            public string Id { get; set; }
            public int MenuSort { get; set; }
            public string MenuIcon { get; set; }
            public string MenuName { get; set; }
            public string ControllerName { get; set; }
            public string ActionName { get; set; }
            public string ParentId { get; set; }
        }
        public class MenuOperateModel
        {
            public string Id { get; set; }
            public string MenuId { get; set; }
            public int OperateSort { get; set; }
            public string OperateName { get; set; }
            public string OperateCode { get; set; }
        }
    }
}