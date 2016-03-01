namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// ��������
	/// </summary>
    [Serializable]
    public class LargeThanCondition : Condition
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
        public LargeThanCondition(object paramValue)
            : this(ConditionRelation.And, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public LargeThanCondition(SearchColumn column, object paramValue)
            : this(ConditionRelation.And, column, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="paramValue">������ֵ</param>
        public LargeThanCondition(ConditionRelation relation, object paramValue)
            : this(relation, null, paramValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="paramValue">������ֵ</param>
        public LargeThanCondition(ConditionRelation relation, SearchColumn column, object paramValue)
            : base(relation, column)
        {
            this.ParamValue = paramValue;
        }

        #endregion

    }
}