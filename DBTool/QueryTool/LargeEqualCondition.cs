namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 大于等于条件
	/// </summary>
    [Serializable]
    public class LargeEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 条件值
        /// </summary>
        private object paramValue;
        /// <summary>
        /// 条件值
        /// </summary>
        public object ParamValue
        {
            get
            {
                return this.paramValue;
            }

            set
            {
                this.paramValue = value;
            }
        }

        #endregion
        
        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramValue">条件的值</param>
        public LargeEqualCondition(object paramValue) 
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LargeEqualCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="paramValue">条件的值</param>
        public LargeEqualCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValue">条件的值</param>
        public LargeEqualCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion
    }
}