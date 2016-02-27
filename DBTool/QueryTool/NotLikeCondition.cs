namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// ����������
	/// </summary>
    [Serializable]
    public class NotLikeCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// ����ƥ���ֵ
        /// </summary>
        private object paramValue;
        /// <summary>
        /// ����ƥ���ֵ
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
        public NotLikeCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public NotLikeCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValue">������ֵ</param>
        public NotLikeCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public NotLikeCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion
    }
}