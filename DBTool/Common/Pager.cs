namespace NFramework.DBTool.Common
{
    #region Reference

    

    #endregion

    /// <summary>
    /// 分页对象
    /// </summary>
    public class Pager
    {
        #region Fields & Properties

        /// <summary>
        /// 当前页码
        /// </summary>
        private int currentPage = 1;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        /// <summary>
        /// 默认每页的条数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 默认每页的条数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// 默认第一条开始的记录，从0开始还是从1开始
        /// </summary>
        private int firstRecord = 0;
        /// <summary>
        /// 默认第一条开始的记录，从0开始还是从1开始
        /// </summary>
        public int FirstRecord
        {
            get { return firstRecord; }
            set { firstRecord = value; }
        }

        /// <summary>
        /// 当前页数据从第几条开始
        /// </summary>
        public int StartRecord
        {
            get
            {
                return (this.CurrentPage - 1) * this.PageSize + this.firstRecord;
            }
        }

        #endregion
    }
}
