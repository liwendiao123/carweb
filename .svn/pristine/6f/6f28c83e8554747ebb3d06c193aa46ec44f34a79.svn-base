using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCMS.Common
{
    public class PagingModels
    {
        private int _rows = 5;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows
        {
            get
            {
                return _rows;
            }
            set
            {
                if (value == 0)
                {
                    _rows = 5;
                }
                else
                {
                    _rows = value;
                }
            }
        }
        private int _page = 1;
        /// <summary>
        /// 当前页是第几页
        /// </summary>
        public int page
        {
            get
            {
                return _page;
            }
            set
            {
                if (value == 0)
                {
                    _page = 1;
                }
                else
                {
                    _page = value;
                }
            }
        }
        private int _sort = 1;
        /// <summary>
        /// 排序列
        /// </summary>
        public int sort
        {
            get
            {
                return _sort;
            }
            set
            {
                if (value == 0)
                {
                    _sort = 1;
                }
                else
                {
                    _sort = value;
                }
            }
        }
        /// <summary>
        /// 总行数
        /// </summary>
        public int totalrows { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalpages
        {
            get
            {
                var total= (int)Math.Ceiling((float)totalrows / (float)rows);
                return total == 0 ? 1 : total;
            }
        }
    }
}
