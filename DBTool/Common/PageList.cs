namespace NFramework.DBTool.Common
{
    #region Reference
    
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// 返回列表对象的分页对象
    /// </summary>
    /// <typeparam name="T">泛型数据对象</typeparam>
    public class PageList<T> : PageResult<IList<T>>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageList() : base()
        {
            this.RecordList = new List<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总记录条数，非当前页条数，而是所有可查询出的条数</param>
        public PageList(int pageIndex, long totalCount) : base(pageIndex, totalCount)
        {
            this.RecordList = new List<T>();
        }
    }
}
