namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 相似条件
	/// </summary>
    [Serializable]
    public class LikeCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 用于匹配的值
        /// </summary>
        public object ParamValue { get; set; }

        #endregion
        
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramValue">条件的值</param>
        public LikeCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LikeCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="paramValue">条件的值</param>
        public LikeCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LikeCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion

    }
}