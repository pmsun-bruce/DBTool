namespace NFramework.DBTool.QueryTool
{
	#region Reference

    using System;

	#endregion

	/// <summary>
    /// 子查询不等于条件
	/// </summary>
    [Serializable]
    public class SQLNotEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 子查询语句
        /// </summary>
        private string sql;
        /// <summary>
        /// 子查询语句
        /// </summary>
		public string SQL
        {
            get
            {
                return this.sql;
            }

            set
            {
                this.sql = value;
            }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">子查询语句</param>
        public SQLNotEqualCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public SQLNotEqualCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="sql">子查询语句</param>
        public SQLNotEqualCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public SQLNotEqualCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SQL = sql;
        }

        #endregion
    }
}