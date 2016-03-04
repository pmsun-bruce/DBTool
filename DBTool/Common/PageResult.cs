namespace NFramework.DBTool.Common
{
    #region Reference
    
    using NFramework.ExceptionTool;

    #endregion

    /// <summary>
    /// 分页查询结果集，也可以是所有结果，即一页
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class PageResult<T>
    {
        #region Fields & Properties

        /// <summary>
        /// 当前页码
        /// </summary>
        private int pageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        /// <summary>
        /// 总记录条数，非当前页条数，而是所有可查询出的条数
        /// </summary>
        private long totalCount;
        /// <summary>
        /// 总记录条数，非当前页条数，而是所有可查询出的条数
        /// </summary>
        public long TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }

        /// <summary>
        /// 记录列表
        /// </summary>
        private T recordList;
        /// <summary>
        /// 记录列表
        /// </summary>
        public T RecordList
        {
            get { return recordList; }
            set { recordList = value; }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResult()
        {
            this.pageIndex = 1;
            this.totalCount = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总记录条数，非当前页条数，而是所有可查询出的条数</param>
        public PageResult(int pageIndex, long totalCount)
        {
            this.pageIndex = pageIndex;
            this.totalCount = totalCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总记录条数，非当前页条数，而是所有可查询出的条数</param>
        /// <param name="recordList">查询出的记录集合</param>
        public PageResult(int pageIndex, long totalCount, T recordList)
        {
            this.pageIndex = pageIndex;
            this.totalCount = totalCount;
            this.RecordList = recordList;
        }

        #endregion
    }
}
