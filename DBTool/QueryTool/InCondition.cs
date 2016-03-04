namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// 包含条件类
	/// </summary>
    [Serializable]
    public class InCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 条件值的集合
        /// </summary>
        private object[] paramValueList;
        /// <summary>
        /// 条件值的集合
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
        /// <param name="paramValueList">条件值的集合</param>
        public InCondition(object[] paramValueList)
            : this(ConditionRelation.And, null, paramValueList)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">参数的值的集合</param>
        public InCondition(SearchColumn column, object[] paramValueList)
            : this(ConditionRelation.And, column, paramValueList)
        {
            this.ParamValueList = paramValueList;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="paramValueList">参数的值的集合</param>
        public InCondition(ConditionRelation relation, object[] paramValueList)
            : this(relation, null, paramValueList)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">参数的值的集合</param>
        public InCondition(ConditionRelation relation, SearchColumn column, object[] paramValueList)
            : base(relation, column)
        {
            this.ParamValueList = paramValueList;
        }

        #endregion

    }
}