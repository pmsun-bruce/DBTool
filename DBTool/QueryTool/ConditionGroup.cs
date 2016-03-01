namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System;
    using System.Collections.Generic;

    using NFramework.ObjectTool;

    #endregion

    /// <summary>
    /// 条件组对象
    /// </summary>
    [Serializable]
    public class ConditionGroup : ICondition
    {
        #region Fields & Properties

        /// <summary>
        /// 组号，当前条件组的唯一编号，由Guid生成
        /// 此为只读属性
        /// </summary>
        public string GroupId { get; }

        /// <summary>
        /// 组和组之间的关系
        /// </summary>
        public ConditionRelation GroupRelation { get; set; }

        /// <summary>
        /// 子条件组
        /// 此为只读属性
        /// </summary>
        private List<ConditionGroup> subGroup;

        /// <summary>
        /// 子条件组
        /// 此为只读属性
        /// </summary>
        public IEnumerable<ConditionGroup> SubGroup
        {
            get
            {
                if (this.subGroup == null)
                {
                    this.subGroup = new List<ConditionGroup>();
                }

                return this.subGroup;
            }
        }

        /// <summary>
        /// 当前组所包含的条件
        /// </summary>
        private List<Condition> conditionCollection;

        /// <summary>
        /// 当前组所包含的条件
        /// </summary>
        public List<Condition> ConditionCollection
        {
            get
            {
                if (this.conditionCollection == null)
                {
                    this.conditionCollection = new List<Condition>();
                }

                return this.conditionCollection;
            }
        }

        /// <summary>
        /// 父组
        /// </summary>
        public ConditionGroup ParentGroup { get; set; }

        /// <summary>
        /// 组的序号，即如果有同级组，则哪个组在前，哪个组在后
        /// </summary>
        public int GroupIndex { get; set; }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// 将子组对象的添加到子组的集合中
        /// </summary>
        /// <param name="subGroup">需要添加的子组</param>
        public void AddSubGroup(ConditionGroup subGroup)
        {
            // 如果要添加的子组的ID和当前组的ID相同，则为同一个组，不能将同组加到自己的子组集合中
            if (this.GroupId.Equals(subGroup.GroupId))
            {
                throw new ArgumentException("不能将当前组加到自己的子组列表中");
            }

            // 将当前组赋值给子组的父组
            subGroup.ParentGroup = this;
            this.subGroup.Add(subGroup);
        }

        /// <summary>
        /// 克隆分组对象
        /// </summary>
        /// <returns>返回克隆后的分组对象</returns>
        public ConditionGroup Clone()
        {
            return ObjectFactory.Clone(this);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConditionGroup()
        {
            // 初始化得时候生成组号
            this.GroupId = Guid.NewGuid().ToString().ToLower();
            // 默认组间的关系为与的关系
            this.GroupRelation = ConditionRelation.And;
            this.subGroup = new List<ConditionGroup>();
            this.GroupIndex = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupIndex">组的排序序号</param>
        public ConditionGroup(int groupIndex) : this()
        {
            this.GroupIndex = groupIndex;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">条件组和其他条件组或条件的关系</param>
        public ConditionGroup(ConditionRelation relation) : this()
        {
            this.GroupRelation = relation;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">条件组和其他条件组或条件的关系</param>
        /// <param name="groupIndex">组的排序序号</param>
        public ConditionGroup(ConditionRelation relation, int groupIndex)
            : this(groupIndex)
        {
            this.GroupRelation = relation;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="condition">条件组和其他条件组或条件的关系</param>
        /// <param name="conditions">条件组中所包含的各种条件对象</param>
        public ConditionGroup(ICondition condition, params ICondition[] conditions)
            : this(ConditionRelation.And, condition, conditions)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">条件组和其他条件组或条件的关系</param>
        /// <param name="condition">条件组和其他条件组或条件的关系</param>
        /// <param name="conditions">条件组中所包含的各种条件对象</param>
        public ConditionGroup(ConditionRelation relation, ICondition condition, params ICondition[] conditions) : this()
        {
            ConditionGroup group = null;

            if (condition != null)
            {
                if (condition is Condition)
                {
                    this.ConditionCollection.Add((Condition)condition);
                }
                else if (condition is ConditionGroup)
                {
                    group = (ConditionGroup)condition;
                    group.GroupIndex = this.subGroup.Count + 1;
                    this.AddSubGroup(group);
                }
            }

            if (conditions != null)
            {
                foreach (ICondition paramCond in conditions)
                {
                    if (paramCond is Condition)
                    {
                        this.ConditionCollection.Add((Condition)paramCond);
                    }
                    else if (paramCond is ConditionGroup)
                    {
                        group = (ConditionGroup)paramCond;
                        group.GroupIndex = this.subGroup.Count + 1;
                        this.AddSubGroup(group);
                    }
                }
            }
        }

        #endregion
    }
}
