namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// ����������
	/// </summary>
    [Serializable]
    public class InCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// ����ֵ�ļ���
        /// </summary>
        private object[] paramValueList;
        /// <summary>
        /// ����ֵ�ļ���
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
        /// <param name="paramValueList">����ֵ�ļ���</param>
        public InCondition(object[] paramValueList)
            : this(ConditionRelation.And, null, paramValueList)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValueList">������ֵ�ļ���</param>
        public InCondition(SearchColumn column, object[] paramValueList)
            : this(ConditionRelation.And, column, paramValueList)
        {
            this.ParamValueList = paramValueList;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValueList">������ֵ�ļ���</param>
        public InCondition(ConditionRelation relation, object[] paramValueList)
            : this(relation, null, paramValueList)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValueList">������ֵ�ļ���</param>
        public InCondition(ConditionRelation relation, SearchColumn column, object[] paramValueList)
            : base(relation, column)
        {
            this.ParamValueList = paramValueList;
        }

        #endregion

    }
}