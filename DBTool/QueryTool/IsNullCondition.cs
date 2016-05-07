namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 为空判断条件
	/// </summary>
    [Serializable]
    public class IsNullCondition : Condition
    {
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public IsNullCondition()
            : this(ConditionRelation.And, null)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        public IsNullCondition(SearchColumn column)
            : this(ConditionRelation.And, column)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        public IsNullCondition(ConditionRelation relation)
            : this(relation, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        public IsNullCondition(ConditionRelation relation, SearchColumn column)
            : base(relation, column)
        {
            
        }


        #endregion
    }
}