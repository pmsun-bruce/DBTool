namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;
	 
	#endregion

    /// <summary>
    /// 排序
    /// </summary>
    [Serializable]
    public enum SortOrder : int
    {
        /// <summary>
        /// 无排序
        /// </summary>
        None = 0,
        /// <summary>
        /// 升序排序
        /// </summary>
        Asc = 1,
        /// <summary>
        /// 降序排序
        /// </summary>
        Desc = 2
    }
}