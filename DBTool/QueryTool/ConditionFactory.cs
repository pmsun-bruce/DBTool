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
            BetweenCondition cond = new BetweenCondition(stratVal, endVal);
            return cond;
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
            BetweenCondition cond = new BetweenCondition(column, stratVal, endVal);
            return cond;
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
            BetweenCondition cond = new BetweenCondition(relation, stratVal, endVal);
            return cond;
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
            BetweenCondition cond = new BetweenCondition(relation, column, stratVal, endVal);
            return cond;
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(object paramVal)
        {
            EqualCondition cond = new EqualCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(ConditionRelation relation, object paramVal)
        {
            EqualCondition cond = new EqualCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回等于条件对象</returns>
        public static EqualCondition Equal(SearchColumn column, object paramVal)
        {
            EqualCondition cond = new EqualCondition(column, paramVal);
            return cond;
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
            EqualCondition cond = new EqualCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(object paramVal)
        {
            NotEqualCondition cond = new NotEqualCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(SearchColumn column, object paramVal)
        {
            NotEqualCondition cond = new NotEqualCondition(column, paramVal);
            return cond;
        }

        /// <summary>
        /// 不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回不等于条件对象</returns>
        public static NotEqualCondition NotEqual(ConditionRelation relation, object paramVal)
        {
            NotEqualCondition cond = new NotEqualCondition(relation, paramVal);
            return cond;
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
            NotEqualCondition cond = new NotEqualCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(object[] paramValueList)
        {
            InCondition cond = new InCondition(paramValueList);
            return cond;
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(ConditionRelation relation, object[] paramValueList)
        {
            InCondition cond = new InCondition(relation, paramValueList);
            return cond;
        }

        /// <summary>
        /// 包含集合条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回集合条件对象</returns>
        public static InCondition In(SearchColumn column, object[] paramValueList)
        {
            InCondition cond = new InCondition(column, paramValueList);
            return cond;
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
            InCondition cond = new InCondition(relation, column, paramValueList);
            return cond;
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(object[] paramValueList)
        {
            NotInCondition cond = new NotInCondition(paramValueList);
            return cond;
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(ConditionRelation relation, object[] paramValueList)
        {
            NotInCondition cond = new NotInCondition(relation, paramValueList);
            return cond;
        }

        /// <summary>
        /// 非包含集合条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramValueList">比较的值列表</param>
        /// <returns>返回非包含集合条件对象</returns>
        public static NotInCondition NotIn(SearchColumn column, object[] paramValueList)
        {
            NotInCondition cond = new NotInCondition(column, paramValueList);
            return cond;
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
            NotInCondition cond = new NotInCondition(relation, column, paramValueList);
            return cond;
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull()
        {
            IsNullCondition cond = new IsNullCondition();
            return cond;
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(ConditionRelation relation)
        {
            IsNullCondition cond = new IsNullCondition(relation);
            return cond;
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(SearchColumn column)
        {
            IsNullCondition cond = new IsNullCondition(column);
            return cond;
        }

        /// <summary>
        /// 空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回空条件对象</returns>
        public static IsNullCondition IsNull(ConditionRelation relation, SearchColumn column)
        {
            IsNullCondition cond = new IsNullCondition(relation, column);
            return cond;
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull()
        {
            IsNotNullCondition cond = new IsNotNullCondition();
            return cond;
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(ConditionRelation relation)
        {
            IsNotNullCondition cond = new IsNotNullCondition(relation);
            return cond;
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(SearchColumn column)
        {
            IsNotNullCondition cond = new IsNotNullCondition(column);
            return cond;
        }

        /// <summary>
        /// 非空条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="column">查询字段对象</param>
        /// <returns>返回非空条件对象</returns>
        public static IsNotNullCondition IsNotNull(ConditionRelation relation, SearchColumn column)
        {
            IsNotNullCondition cond = new IsNotNullCondition(relation, column);
            return cond;
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(object paramVal)
        {
            LargeEqualCondition cond = new LargeEqualCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(ConditionRelation relation, object paramVal)
        {
            LargeEqualCondition cond = new LargeEqualCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 大于等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于等于条件对象</returns>
        public static LargeEqualCondition LargeEqual(SearchColumn column, object paramVal)
        {
            LargeEqualCondition cond = new LargeEqualCondition(column, paramVal);
            return cond;
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
            LargeEqualCondition cond = new LargeEqualCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(object paramVal)
        {
            LargeThanCondition cond = new LargeThanCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(ConditionRelation relation, object paramVal)
        {
            LargeThanCondition cond = new LargeThanCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 大于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回大于条件对象</returns>
        public static LargeThanCondition LargeThan(SearchColumn column, object paramVal)
        {
            LargeThanCondition cond = new LargeThanCondition(column, paramVal);
            return cond;
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
            LargeThanCondition cond = new LargeThanCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(object paramVal)
        {
            LessEqualCondition cond = new LessEqualCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(ConditionRelation relation, object paramVal)
        {
            LessEqualCondition cond = new LessEqualCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 小于等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于等于条件对象</returns>
        public static LessEqualCondition LessEqual(SearchColumn column, object paramVal)
        {
            LessEqualCondition cond = new LessEqualCondition(column, paramVal);
            return cond;
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
            LessEqualCondition cond = new LessEqualCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(object paramVal)
        {
            LessThanCondition cond = new LessThanCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(ConditionRelation relation, object paramVal)
        {
            LessThanCondition cond = new LessThanCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 小于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回小于条件对象</returns>
        public static LessThanCondition LessThan(SearchColumn column, object paramVal)
        {
            LessThanCondition cond = new LessThanCondition(column, paramVal);
            return cond;
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
            LessThanCondition cond = new LessThanCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(object paramVal)
        {
            LikeCondition cond = new LikeCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(ConditionRelation relation, object paramVal)
        {
            LikeCondition cond = new LikeCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 相似条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回相似条件对象</returns>
        public static LikeCondition Like(SearchColumn column, object paramVal)
        {
            LikeCondition cond = new LikeCondition(column, paramVal);
            return cond;
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
            LikeCondition cond = new LikeCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(object paramVal)
        {
            NotLikeCondition cond = new NotLikeCondition(paramVal);
            return cond;
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(ConditionRelation relation, object paramVal)
        {
            NotLikeCondition cond = new NotLikeCondition(relation, paramVal);
            return cond;
        }

        /// <summary>
        /// 非相似条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="paramVal">比较的值</param>
        /// <returns>返回非相似条件对象</returns>
        public static NotLikeCondition NotLike(SearchColumn column, object paramVal)
        {
            NotLikeCondition cond = new NotLikeCondition(column, paramVal);
            return cond;
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
            NotLikeCondition cond = new NotLikeCondition(relation, column, paramVal);
            return cond;
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(string sql)
        {
            SQLInCondition cond = new SQLInCondition(sql);
            return cond;
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(ConditionRelation relation, string sql)
        {
            SQLInCondition cond = new SQLInCondition(relation, sql);
            return cond;
        }

        /// <summary>
        /// 子查询包含条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询包含条件对象</returns>
        public static SQLInCondition SQLIn(SearchColumn column, string sql)
        {
            SQLInCondition cond = new SQLInCondition(column, sql);
            return cond;
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
            SQLInCondition cond = new SQLInCondition(relation, column, sql);
            return cond;
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(string sql)
        {
            SQLNotInCondition cond = new SQLNotInCondition(sql);
            return cond;
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(ConditionRelation relation, string sql)
        {
            SQLNotInCondition cond = new SQLNotInCondition(relation, sql);
            return cond;
        }

        /// <summary>
        /// 子查询非包含条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询非包含条件对象</returns>
        public static SQLNotInCondition SQLNotIn(SearchColumn column, string sql)
        {
            SQLNotInCondition cond = new SQLNotInCondition(column, sql);
            return cond;
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
            SQLNotInCondition cond = new SQLNotInCondition(relation, column, sql);
            return cond;
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(string sql)
        {
            SQLEqualCondition cond = new SQLEqualCondition(sql);
            return cond;
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(ConditionRelation relation, string sql)
        {
            SQLEqualCondition cond = new SQLEqualCondition(relation, sql);
            return cond;
        }

        /// <summary>
        /// 子查询等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询等于条件对象</returns>
        public static SQLEqualCondition SQLEqual(SearchColumn column, string sql)
        {
            SQLEqualCondition cond = new SQLEqualCondition(column, sql);
            return cond;
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
            SQLEqualCondition cond = new SQLEqualCondition(relation, column, sql);
            return cond;
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(string sql)
        {
            SQLNotEqualCondition cond = new SQLNotEqualCondition(sql);
            return cond;
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="relation">条件关系</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(ConditionRelation relation, string sql)
        {
            SQLNotEqualCondition cond = new SQLNotEqualCondition(relation, sql);
            return cond;
        }

        /// <summary>
        /// 子查询不等于条件
        /// </summary>
        /// <param name="column">查询字段对象</param>
        /// <param name="sql">子查询SQL</param>
        /// <returns>返回子查询不等于条件对象</returns>
        public static SQLNotEqualCondition SQLNotEqual(SearchColumn column, string sql)
        {
            SQLNotEqualCondition cond = new SQLNotEqualCondition(column, sql);
            return cond;
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
            SQLNotEqualCondition cond = new SQLNotEqualCondition(relation, column, sql);
            return cond;
        }

        /// <summary>
        /// 条件分组
        /// </summary>
        /// <param name="condition">添加条件或组</param>
        /// <param name="conditions">可选参数，可以添加多个条件和组</param>
        /// <returns></returns>
        public static ConditionGroup Group(ICondition condition, params ICondition[] conditions)
        {
            ConditionGroup group = new ConditionGroup(condition, conditions);
            return group;
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
            ConditionGroup group = new ConditionGroup(relation, condition, conditions);
            return group;
        }

        #endregion
    }
}
