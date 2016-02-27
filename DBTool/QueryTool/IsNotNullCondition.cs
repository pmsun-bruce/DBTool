namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 不为空条件
	/// </summary>
    [Serializable]
    public class IsNotNullCondition : Condition
    {
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public IsNotNullCondition()
            : this(ConditionRelation.And, null)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        public IsNotNullCondition(SearchColumn column)
            : this(ConditionRelation.And, column)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        public IsNotNullCondition(ConditionRelation relation)
            : this(relation, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        public IsNotNullCondition(ConditionRelation relation, SearchColumn column)
            : base(relation, column)
        {
            
        }

        #endregion
    }
}