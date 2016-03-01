namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 小于等于条件
	/// </summary>
    [Serializable]
    public class LessEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 条件值
        /// </summary>
        public object ParamValue { get; set; }

        #endregion
        
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramValue">条件的值</param>
        public LessEqualCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LessEqualCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="paramValue">条件的值</param>
        public LessEqualCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LessEqualCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion
    }
}