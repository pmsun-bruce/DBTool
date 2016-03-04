namespace NFramework.DBTool.QueryTool
{
    /// <summary>
    /// Exists条件类
    /// </summary>
    public class ExistsCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 子查询语句
        /// </summary>
        public string SubQuery { get; set; }

        #endregion

        #region Public Contructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">子查询语句</param>
        public ExistsCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public ExistsCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="sql">子查询语句</param>
        public ExistsCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public ExistsCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SubQuery = sql;
        }

        #endregion

    }
}