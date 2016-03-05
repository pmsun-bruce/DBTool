namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// �ų��ķ�Χ����
	/// </summary>
    [Serializable]
	public class NotInCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// ��Ҫ�ų���ֵ�ļ���
        /// </summary>
        private object [] paramValueList;
        /// <summary>
        /// ��Ҫ�ų���ֵ�ļ���
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
        /// ���캯��
        /// </summary>
        /// <param name="paramValueList">��Ҫ�ų���ֵ�ļ���</param>
        public NotInCondition(object[] paramValueList)
            : this(ConditionRelation.And, null, paramValueList)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValueList">��Ҫ�ų���ֵ�ļ���</param>
        public NotInCondition(SearchColumn column, object[] paramValueList)
            : this(ConditionRelation.And, column, paramValueList)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValueList">��Ҫ�ų���ֵ�ļ���</param>
        public NotInCondition(ConditionRelation relation, object[] paramValueList)
            : this(relation, null, paramValueList)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValueList">��Ҫ�ų���ֵ�ļ���</param>
        public NotInCondition(ConditionRelation relation, SearchColumn column, object[] paramValueList)
            : base(relation, column)
        {
            this.ParamValueList = paramValueList;
        }

        #endregion
    }
}