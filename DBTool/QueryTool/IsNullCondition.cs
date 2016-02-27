namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;

	#endregion

	/// <summary>
	/// Ϊ���ж�����
	/// </summary>
    [Serializable]
    public class IsNullCondition : Condition
    {
        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        public IsNullCondition()
            : this(ConditionRelation.And, null)
        {
            
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        public IsNullCondition(SearchColumn column)
            : this(ConditionRelation.And, column)
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        public IsNullCondition(ConditionRelation relation)
            : this(relation, null)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        public IsNullCondition(ConditionRelation relation, SearchColumn column)
            : base(relation, column)
        {
            
        }


        #endregion
    }
}