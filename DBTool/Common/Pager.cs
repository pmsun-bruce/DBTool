namespace NFramework.DBTool.Common
{
    /// <summary>
    /// 分页对象
    /// </summary>
    public class Pager
    {
        #region Fields & Properties

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 默认每页的条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 默认第一条开始的记录，从0开始还是从1开始
        /// </summary>
        public int FirstRecord { get; set; }

        /// <summary>
        /// 当前页数据从第几条开始
        /// </summary>
        public int StartRecord
        {
            get
            {
                return (this.CurrentPage - 1) * this.PageSize + this.FirstRecord;
            }
        }

        #endregion

        #region

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pager()
        {
            this.CurrentPage = 1;
            this.PageSize = 10;
            this.FirstRecord = 0;
        }

        #endregion
    }
}
