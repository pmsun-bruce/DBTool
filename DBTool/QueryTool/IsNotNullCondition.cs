namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// ��Ϊ������
	/// </summary>
    [Serializable]
    public class IsNotNullCondition : Condition
    {
        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        public IsNotNullCondition()
            : this(ConditionRelation.And, null)
        {
            
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        public IsNotNullCondition(SearchColumn column)
            : this(ConditionRelation.And, column)
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        public IsNotNullCondition(ConditionRelation relation)
            : this(relation, null)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        public IsNotNullCondition(ConditionRelation relation, SearchColumn column)
            : base(relation, column)
        {
            
        }

        #endregion
    }
}