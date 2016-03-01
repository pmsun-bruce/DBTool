namespace NFramework.DBTool.QueryTool
{
    #region Reference

    

    #endregion

    /// <summary>
    /// 条件工厂
    /// </summary>
    public static class ConditionFactory
    {
        #region Public Static Methods

        /// <summary>
        /// Between条件，用于查询介于两个值之间的值
        /// </summary>
        /// <param name="stratVal">开始值</param>
        /// <param name="endVal">结束值</param>
        /// <returns>返回Between条件对象</returns>
        public static BetweenCondition Between(object stratVal, object endVal)
        {
            return new BetweenCondition(stratVal, endVal);
        }

        /// <summary>
        /// Between条件，用于查询介于两个值之间的值
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="stratVal">开始值</param>
        /// <param name="endVal">结束值</param>
        /// <returns>返回Between条件对象</returns>
        public static BetweenCondition Between(SearchColumn column, object stratVal, object endVal)
        {
            return new BetweenCondition(column, stratVal, endVal);
        }

        /// <summary>
        /// Between条件，用于查询介于两个值之间的值
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="stratVal">开始值</param>
        /// <param name="endVal">结束值</param>
        /// <returns>返回Between条件对象</returns>
        public static BetweenCondition Between(ConditionRelation relation, object stratVal, object endVal)
        {
            return new BetweenCondition(relation, stratVal, endVal);
        }

        /// <summary>
        /// Between条件，用于查询介于两个值之间的值
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="stratVal">开始值</param>
        /// <param name="endVal">结束值</param>
        /// <returns>返回Between条件对象</returns>
        public static BetweenCondition Between(ConditionRelation relation, SearchColumn column, object stratVal, object endVal)
        {
            return new BetweenCondition(relation, column, stratVal, endVal);
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(object paramVal)
        {
            return new EqualCondition(paramVal);
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(ConditionRelation relation, object paramVal)
        {
            return new EqualCondition(relation, paramVal);
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(SearchColumn column, object paramVal)
        {
            return new EqualCondition(column, paramVal);
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new EqualCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(object paramVal)
        {
            return new NotEqualCondition(paramVal);
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(SearchColumn column, object paramVal)
        {
            return new NotEqualCondition(column, paramVal);
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(ConditionRelation relation, object paramVal)
        {
            return new NotEqualCondition(relation, paramVal);
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new NotEqualCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(object[] paramValueList)
        {
            return new InCondition(paramValueList);
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(ConditionRelation relation, object[] paramValueList)
        {
            return new InCondition(relation, paramValueList);
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(SearchColumn column, object[] paramValueList)
        {
            return new InCondition(column, paramValueList);
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(ConditionRelation relation, SearchColumn column, object[] paramValueList)
        {
            return new InCondition(relation, column, paramValueList);
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(object[] paramValueList)
        {
            return new NotInCondition(paramValueList);
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(ConditionRelation relation, object[] paramValueList)
        {
            return new NotInCondition(relation, paramValueList);
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(SearchColumn column, object[] paramValueList)
        {
            return new NotInCondition(column, paramValueList);
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(ConditionRelation relation, SearchColumn column, object[] paramValueList)
        {
            return new NotInCondition(relation, column, paramValueList);
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull()
        {
            return new IsNullCondition();
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(ConditionRelation relation)
        {
            return new IsNullCondition(relation);
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(SearchColumn column)
        {
            return new IsNullCondition(column);
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(ConditionRelation relation, SearchColumn column)
        {
            return new IsNullCondition(relation, column);
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull()
        {
            return new IsNotNullCondition();
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(ConditionRelation relation)
        {
            return new IsNotNullCondition(relation);
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(SearchColumn column)
        {
            return new IsNotNullCondition(column);
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(ConditionRelation relation, SearchColumn column)
        {
            return new IsNotNullCondition(relation, column);
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(object paramVal)
        {
            return new LargeEqualCondition(paramVal);
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(ConditionRelation relation, object paramVal)
        {
            return new LargeEqualCondition(relation, paramVal);
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(SearchColumn column, object paramVal)
        {
            return new LargeEqualCondition(column, paramVal);
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new LargeEqualCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(object paramVal)
        {
            return new LargeThanCondition(paramVal);
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(ConditionRelation relation, object paramVal)
        {
            return new LargeThanCondition(relation, paramVal);
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(SearchColumn column, object paramVal)
        {
            return new LargeThanCondition(column, paramVal);
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new LargeThanCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(object paramVal)
        {
            return new LessEqualCondition(paramVal);
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(ConditionRelation relation, object paramVal)
        {
            return new LessEqualCondition(relation, paramVal);
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(SearchColumn column, object paramVal)
        {
            return new LessEqualCondition(column, paramVal);
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new LessEqualCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(object paramVal)
        {
            return new LessThanCondition(paramVal);
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(ConditionRelation relation, object paramVal)
        {
            return new LessThanCondition(relation, paramVal);
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(SearchColumn column, object paramVal)
        {
            return new LessThanCondition(column, paramVal);
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new LessThanCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(object paramVal)
        {
            return new LikeCondition(paramVal);
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(ConditionRelation relation, object paramVal)
        {
            return new LikeCondition(relation, paramVal);
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(SearchColumn column, object paramVal)
        {
            return new LikeCondition(column, paramVal);
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new LikeCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(object paramVal)
        {
            return new NotLikeCondition(paramVal);
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(ConditionRelation relation, object paramVal)
        {
            return new NotLikeCondition(relation, paramVal);
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(SearchColumn column, object paramVal)
        {
            return new NotLikeCondition(column, paramVal);
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(ConditionRelation relation, SearchColumn column, object paramVal)
        {
            return new NotLikeCondition(relation, column, paramVal);
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(string sql)
        {
            return new SQLInCondition(sql);
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(ConditionRelation relation, string sql)
        {
            return new SQLInCondition(relation, sql);
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(SearchColumn column, string sql)
        {
            return new SQLInCondition(column, sql);
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(ConditionRelation relation, SearchColumn column, string sql)
        {
            return new SQLInCondition(relation, column, sql);
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(string sql)
        {
            return new SQLNotInCondition(sql);
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(ConditionRelation relation, string sql)
        {
            return new SQLNotInCondition(relation, sql);
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(SearchColumn column, string sql)
        {
            return new SQLNotInCondition(column, sql);
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(ConditionRelation relation, SearchColumn column, string sql)
        {
            return new SQLNotInCondition(relation, column, sql);
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(string sql)
        {
            return new SQLEqualCondition(sql);
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(ConditionRelation relation, string sql)
        {
            return new SQLEqualCondition(relation, sql);
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(SearchColumn column, string sql)
        {
            return new SQLEqualCondition(column, sql);
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(ConditionRelation relation, SearchColumn column, string sql)
        {
            return new SQLEqualCondition(relation, column, sql);
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(string sql)
        {
            return new SQLNotEqualCondition(sql);
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(ConditionRelation relation, string sql)
        {
            return new SQLNotEqualCondition(relation, sql);
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(SearchColumn column, string sql)
        {
            return new SQLNotEqualCondition(column, sql);
        }
        
        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(ConditionRelation relation, SearchColumn column, string sql)
        {
            return new SQLNotEqualCondition(relation, column, sql);
        }

        /// <summary>
        /// 条件分组
        /// </summary>
        /// <param name="condition">添加条件或组</param>
        /// <param name="conditions">可选参数，可以添加多个条件和组</param>
        /// <returns></returns>
        public static ConditionGroup Group(ICondition condition, params ICondition[] conditions)
        {
            return new ConditionGroup(condition, conditions);
        }

        /// <summary>
        /// 条件分组
        /// </summary>
        /// <param name="relation">组与组间的关系</param>
        /// <param name="condition">添加条件或组</param>
        /// <param name="conditions">可选参数，可以添加多个条件和组</param>
        /// <returns></returns>
        public static ConditionGroup Group(ConditionRelation relation, ICondition condition, params ICondition[] conditions)
        {
            return new ConditionGroup(relation, condition, conditions);
        }

        #endregion
    }
}
