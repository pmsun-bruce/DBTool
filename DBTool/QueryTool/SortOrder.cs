namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;
	 
	#endregion

    /// <summary>
    /// ����
    /// </summary>
    [Serializable]
    public enum SortOrder : int
    {
        /// <summary>
        /// ������
        /// </summary>
        None = 0,
        /// <summary>
        /// ��������
        /// </summary>
        Asc = 1,
        /// <summary>
        /// ��������
        /// </summary>
        Desc = 2
    }
}