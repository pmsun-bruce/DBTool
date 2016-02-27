namespace NFramework.DBTool.QueryTool
{
	#region Reference
	
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using NFramework.ObjectTool;
	 
	#endregion

    /// <summary>
    /// ����������
    /// </summary>
    [Serializable]
    public abstract class Condition : ICondition
    {
        #region Fields & Properties

        /// <summary>
        /// ������ϵ��AND��OR
        /// </summary>
        private ConditionRelation relation;
        /// <summary>
        /// ������ϵ��AND��OR
        /// </summary>
        public ConditionRelation Relation
        {
            get
            {
                return this.relation;
            }
            set
            {
                this.relation = value;
            }
        }

        /// <summary>
        /// �������飬��ǰ��������Щ������Ϊһ�飬���в�ѯ
        /// </summary>
        private ConditionGroup group;
        /// <summary>
        /// �������飬��ǰ��������Щ������Ϊһ�飬���в�ѯ
        /// </summary>
        public ConditionGroup Group
        {
            get
            {
                return this.group;
            }
            set
            {
                this.group = value;

                if (this.group != null)
                {
                    this.group.ConditionCollection.Add(this);
                }
            }
        }

        /// <summary>
        /// ��������������ֶζ���Ϊ�˺���ƴ�Ӳ�ѯ����ʱ��ȡ�ֶ����ơ�
        /// SearchColumn��������ȡ��ʱ�򣬻��Զ���ֵ�������û��Լ���ֵ��
        /// </summary>
        private SearchColumn searchColumn;
        /// <summary>
        /// ��������������ֶζ���Ϊ�˺���ƴ�Ӳ�ѯ����ʱ��ȡ�ֶ����ơ�
        /// SearchColumn��������ȡ��ʱ�򣬻��Զ���ֵ�������û��Լ���ֵ��
        /// </summary>
        public SearchColumn SearchColumn
        {
            get
            {
                return this.searchColumn;
            }

            set
            {
                this.searchColumn = value;
            }
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// ��¡��ǰ������
        /// </summary>
        /// <returns></returns>
        public Condition Clone()
        {
            return ObjectFactory.Clone<Condition>(this);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// ���캯��
        /// </summary>
        public Condition(): this(ConditionRelation.And, null)
        {
            // Ĭ�ϵ�������ϵΪ��Ĺ�ϵ
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">����������ϵ</param>
        public Condition(ConditionRelation relation) : this(relation, null)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="column">���������ֶ�</param>
        public Condition(SearchColumn column) : this(ConditionRelation.And, column)
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="relation">����������ϵ</param>
        /// <param name="column">���������ֶ�</param>
        public Condition(ConditionRelation relation, SearchColumn column)
        {
            this.relation = relation;

            if (column != null)
            {
                this.searchColumn = column;
                this.searchColumn.ConditionCollection.Add(this);
            }
        }

        #endregion

    }
}