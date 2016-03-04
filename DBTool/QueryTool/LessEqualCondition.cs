namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// С�ڵ�������
	/// </summary>
    [Serializable]
    public class LessEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// ����ֵ
        /// </summary>
        public object ParamValue { get; set; }

        #endregion
        
        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="paramValue">������ֵ</param>
        public LessEqualCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public LessEqualCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValue">������ֵ</param>
        public LessEqualCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public LessEqualCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion
    }
}