namespace NFramework.DBTool.QueryTool
{
    #region Reference

    #endregion

    /// <summary>
    /// NotExists条件类
    /// </summary>
    public class NotExistsCondition : Condition
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

        #region Public Contructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sql">子查询语句</param>
        public NotExistsCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public NotExistsCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="sql">子查询语句</param>
        public NotExistsCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询语句</param>
        public NotExistsCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SQL = sql;
        }

        #endregion

    }
}