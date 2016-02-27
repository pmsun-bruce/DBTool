namespace NFramework.DBTool.QueryTool
{
	#region Reference

    using System;

	#endregion

	/// <summary>
	/// 通过SQL子查询进行排除的范围条件
    /// </summary>
    [Serializable]
    public class SQLNotInCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// SQL子查询语句
        /// </summary>
        private string sql;
        /// <summary>
        /// SQL子查询语句
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
        public SQLNotInCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public SQLNotInCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="sql">子查询语句</param>
        public SQLNotInCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">SQL子查询语句</param>
        public SQLNotInCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SQL = sql;
        }

        #endregion

    }
}