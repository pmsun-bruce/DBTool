namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;

    #endregion

    /// <summary>
    /// 条件或组的关系
    /// </summary>
    [Serializable]
    public enum ConditionRelation
    {
        /// <summary>
        /// 与关系
        /// </summary>
        And,
        /// <summary>
        /// 或关系
        /// </summary>
        Or
    }
}
