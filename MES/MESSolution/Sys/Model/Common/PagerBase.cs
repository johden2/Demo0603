using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sys.Model
{
    [Serializable]
    public class PagerBase
    {
        private bool _IsPage = true;
        /// <summary>
        /// 是否分页处理，默认为分页处理
        /// </summary>
        public bool IsPage
        {
            get
            {
                return _IsPage;
            }
            set
            {
                _IsPage = value;
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 获取或设置总记录数。
        /// </summary>
        public int TotalItemCount { get; set; }

        private int _pageSize;

        /// <summary>
        /// 获取或设置页面大小。
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_pageSize > 0)
                { return _pageSize; }
                return 25;
            }
            set
            {
                _pageSize = value;
            }
        }

        private int _currentPageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (_currentPageIndex > 0)
                    return _currentPageIndex;
                return 1;
            }
            set
            {
                _currentPageIndex = value;
            }
        }

        /// <summary>
        /// 开始记录号
        /// </summary>
        public int StartNo
        {
            get
            {
                return PageSize * (CurrentPageIndex - 1) + 1;
            }
        }

        /// <summary>
        /// 开始记录号
        /// </summary>
        public int EndNo
        {
            get
            {
                return PageSize * CurrentPageIndex;
            }
        }

    }
}
