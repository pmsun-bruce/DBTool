namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	
	#endregion

	/// <summary>
	/// Between����
	/// </summary>
    [Serializable]
    public class BetweenCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// ��ʼ��ֵ
        /// </summary>
        public object StartValue { get; set; }

        /// <summary>
        /// ������ֵ
        /// </summary>
        public object EndValue { get; set; }


        #endregion

        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="startValue">��ֵ��ʼ��ֵ</param>
        /// <param name="endValue">��ֵ������ֵ</param>
        public BetweenCondition(object startValue, object endValue) 
            : this(ConditionRelation.And, null, startValue, endValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="startValue">��ֵ��ʼ��ֵ</param>
        /// <param name="endValue">��ֵ������ֵ</param>
        public BetweenCondition(ConditionRelation relation, object startValue, object endValue)
            : this(relation, null, startValue, endValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="startValue">��ֵ��ʼ��ֵ</param>
        /// <param name="endValue">��ֵ������ֵ</param>
        public BetweenCondition(SearchColumn column, object startValue, object endValue)
            : this(ConditionRelation.And, column, startValue, endValue)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">������ϵ</param>
        /// <param name="column">��ѯ�ֶζ���</param>
        /// <param name="startValue">��ֵ��ʼ��ֵ</param>
        /// <param name="endValue">��ֵ������ֵ</param>
        public BetweenCondition(ConditionRelation relation, SearchColumn column, object startValue, object endValue)
            : base(relation, column)
        {
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        #endregion
    }
}