namespace NFramework.DBTool.Common
{
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
        public int PageIndex { get; set; }

        /// <summary>
        /// 总记录条数，非当前页条数，而是所有可查询出的条数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 记录列表
        /// </summary>
        public T RecordList { get; set; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResult()
        {
            this.PageIndex = 1;
            this.TotalCount = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总记录条数，非当前页条数，而是所有可查询出的条数</param>
        public PageResult(int pageIndex, long totalCount)
        {
            this.PageIndex = pageIndex;
            this.TotalCount = totalCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总记录条数，非当前页条数，而是所有可查询出的条数</param>
        /// <param name="recordList">查询出的记录集合</param>
        public PageResult(int pageIndex, long totalCount, T recordList)
        {
            this.PageIndex = pageIndex;
            this.TotalCount = totalCount;
            this.RecordList = recordList;
        }

        #endregion
    }
}
