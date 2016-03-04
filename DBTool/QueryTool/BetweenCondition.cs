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
	/// Between条件
	/// </summary>
    [Serializable]
    public class BetweenCondition : Condition
    {
        #region Fields & Properties

        /// <summary>
        /// 开始的值
        /// </summary>
        public object StartValue { get; set; }

        /// <summary>
        /// 结束的值
        /// </summary>
        public object EndValue { get; set; }


        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startValue">赋值开始的值</param>
        /// <param name="endValue">赋值结束的值</param>
        public BetweenCondition(object startValue, object endValue) 
            : this(ConditionRelation.And, null, startValue, endValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="startValue">赋值开始的值</param>
        /// <param name="endValue">赋值结束的值</param>
        public BetweenCondition(ConditionRelation relation, object startValue, object endValue)
            : this(relation, null, startValue, endValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="startValue">赋值开始的值</param>
        /// <param name="endValue">赋值结束的值</param>
        public BetweenCondition(SearchColumn column, object startValue, object endValue)
            : this(ConditionRelation.And, column, startValue, endValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relation">关联关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="startValue">赋值开始的值</param>
        /// <param name="endValue">赋值结束的值</param>
        public BetweenCondition(ConditionRelation relation, SearchColumn column, object startValue, object endValue)
            : base(relation, column)
        {
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        #endregion
    }
}