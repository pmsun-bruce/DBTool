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
        /// 未指定
        /// </summary>
        None = 0,
        /// <summary>
        /// 正序
        /// </summary>
        Asc = 1,
        /// <summary>
        /// 逆序
        /// </summary>
        Desc = 2
    }
}
