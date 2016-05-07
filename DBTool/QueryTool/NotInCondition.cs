namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 排除的范围条件
	/// </summary>
    [Serializable]
	public class NotInCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 需要排除的值的集合
        /// </summary>
        private object [] paramValueList;
        /// <summary>
        /// 需要排除的值的集合
        /// </summary>
		public object[] ParamValueList
        {
            get
            {
                return this.paramValueList;
            }

            set
            {
                this.paramValueList = value;
            }
        }

        #endregion
        
        #region Public Constructors
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramValueList">需要排除的值的集合</param>
        public NotInCondition(object[] paramValueList)
            : this(ConditionRelation.And, null, paramValueList)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">需要排除的值的集合</param>
        public NotInCondition(SearchColumn column, object[] paramValueList)
            : this(ConditionRelation.And, column, paramValueList)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="paramValueList">需要排除的值的集合</param>
        public NotInCondition(ConditionRelation relation, object[] paramValueList)
            : this(relation, null, paramValueList)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">需要排除的值的集合</param>
        public NotInCondition(ConditionRelation relation, SearchColumn column, object[] paramValueList)
            : base(relation, column)
        {
            this.ParamValueList = paramValueList;
        }

        #endregion
    }
}