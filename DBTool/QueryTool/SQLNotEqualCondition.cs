namespace NFramework.DBTool.QueryTool
{
	#region Reference

    using System;

	#endregion

	/// <summary>
    /// �Ӳ�ѯ����������
	/// </summary>
    [Serializable]
    public class SQLNotEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// �Ӳ�ѯ���
        /// </summary>
		public string SubQuery { get; set; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public SQLNotEqualCondition(string sql)
            : this(ConditionRelation.And, null, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public SQLNotEqualCondition(SearchColumn column, string sql)
            : this(ConditionRelation.And, column, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public SQLNotEqualCondition(ConditionRelation relation, string sql)
            : this(relation, null, sql)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="sql">�Ӳ�ѯ���</param>
        public SQLNotEqualCondition(ConditionRelation relation, SearchColumn column, string sql)
            : base(relation, column)
        {
            this.SubQuery = sql;
        }

        #endregion
    }
}