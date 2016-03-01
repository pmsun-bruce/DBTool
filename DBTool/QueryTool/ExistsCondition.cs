namespace NFramework.DBTool.QueryTool
{
    /// <summary>
    /// Exists������
    /// </summary>
    public class ExistsCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// �Ӳ�ѯ���
        /// </summary>
        public string SubQuery { get; set; }

        #endregion

        #region Public Contructors

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public ExistsCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public ExistsCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public ExistsCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public ExistsCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SubQuery = sql;
        }

        #endregion

    }
}