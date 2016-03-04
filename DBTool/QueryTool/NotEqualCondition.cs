namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// ����������
	/// </summary>
    [Serializable]
    public class NotEqualCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// �����ڵ�ֵ
        /// </summary>
        private object paramValue;
        /// <summary>
        /// �����ڵ�ֵ
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
        /// ���캯��
        /// </summary>
        /// <param name="paramValue">������ֵ</param>
        public NotEqualCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public NotEqualCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValue">������ֵ</param>
        public NotEqualCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public NotEqualCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion
    }
}