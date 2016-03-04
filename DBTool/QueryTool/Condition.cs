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
    /// 条件对象父类
    /// </summary>
    [Serializable]
    public abstract class Condition : ICondition
    {
        #region Fields & Properties

        /// <summary>
        /// 条件关系，AND或OR
        /// </summary>
        private ConditionRelation relation;
        /// <summary>
        /// 条件关系，AND或OR
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
        /// 条件分组，当前条件与那些条件分为一组，进行查询
        /// </summary>
        private ConditionGroup group;
        /// <summary>
        /// 条件分组，当前条件与那些条件分为一组，进行查询
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
        /// 承载这个条件的字段对象，为了后期拼接查询条件时获取字段名称。
        /// SearchColumn在条件读取的时候，会自动赋值，不用用户自己赋值。
        /// </summary>
        private SearchColumn searchColumn;
        /// <summary>
        /// 承载这个条件的字段对象，为了后期拼接查询条件时获取字段名称。
        /// SearchColumn在条件读取的时候，会自动赋值，不用用户自己赋值。
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
        /// 克隆当前的条件
        /// </summary>
        /// <returns></returns>
        public Condition Clone()
        {
            return ObjectFactory.Clone<Condition>(this);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public Condition(): this(ConditionRelation.And, null)
        {
            // 默认的条件关系为与的关系
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">条件关联关系</param>
        public Condition(ConditionRelation relation) : this(relation, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">条件关联字段</param>
        public Condition(SearchColumn column) : this(ConditionRelation.And, column)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">条件关联关系</param>
        /// <param name="column">条件关联字段</param>
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