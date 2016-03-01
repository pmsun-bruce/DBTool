namespace NFramework.DBTool.QueryTool
{
    #region Reference

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion

    /// <summary>
    /// 查询语句解析器抽象类
    /// </summary>
    public abstract class QueryParser
    {
        #region Fields & Properties
        
        /// <summary>
        /// 用于存储解析后的查询条件语句
        /// </summary>
        public string ConditionString { get; private set; }

        /// <summary>
        /// 用于存储解析后的排序语句
        /// </summary>
        public string SortString { get; private set; }

        /// <summary>
        /// 用于存储解析后的所有参数
        /// </summary>
        public DBParamCollection ParamCollection { get; set; }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// 进行查询对象的解析
        /// </summary>
        /// <param name="searcher">传入需要解析的查询对象</param>
        public void SearcherParse(Searcher searcher)
        {
            if (searcher == null)
            {
                this.ConditionString = string.Empty;
                this.SortString = string.Empty;
                return;
            }

            ConditionGroup mainGroup = new ConditionGroup(0);
            List<SearchColumn> sortColumnList = new List<SearchColumn>();
            this.ParamCollection = new DBParamCollection();
            this.ParseGroup(searcher, mainGroup, sortColumnList);
            this.ConditionString = this.ParseConditionString(mainGroup);
            this.ParseSortString(sortColumnList);

            this.SortString = string.IsNullOrEmpty(this.SortString) ? string.Empty : this.SortString + " ";
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 查找根组，即最外层的条件组
        /// </summary>
        /// <param name="group">需要查找的条件组对象</param>
        /// <returns>返回最外层的条件组</returns>
        protected ConditionGroup GetRootGroup(ConditionGroup group)
        {
            if (group.ParentGroup == null)
            {
                return group;
            }

            return this.GetRootGroup(group.ParentGroup);
        }

        /// <summary>
        /// 递归解析组，即将Searcher对象中的条件，按照他们的组别进行里外层的组划分，好为条件添加括号来判定他们的执行顺序
        /// </summary>
        /// <param name="searcher">需要解析的查询对象</param>
        /// <param name="mainGroup">用于返回排好层级的条件组，并填充好各级的条件对象</param>
        /// <param name="sortColumnList">用于返回所有需要排序的字段</param>
        protected void ParseGroup(Searcher searcher, ConditionGroup mainGroup, List<SearchColumn> sortColumnList)
        {
            if (searcher == null)
            {
                return;
            }

            var conditionColumnList = searcher.ConditionColumnList;
            var relationSearcherList = searcher.RelationSearcherList;
            var mainSubGroupList = mainGroup.SubGroup;
            var mainConditionList = mainGroup.ConditionCollection;
            ConditionGroup rootGroup = null;
            IList<Condition> conditionList = null;

            foreach (var column in conditionColumnList)
            {
                if (column.SortOrder != SortOrder.None)
                {
                    sortColumnList.Add(column);
                }

                if (column.ConditionCollection.Count == 0)
                {
                    continue;
                }

                conditionList = column.ConditionCollection;

                foreach (var condition in conditionList)
                {
                    if (condition.Group == null)
                    {
                        mainConditionList.Add(condition);
                        continue;
                    }

                    rootGroup = this.GetRootGroup(condition.Group);

                    if (!mainGroup.GroupId.Equals(rootGroup.GroupId) && !mainSubGroupList.Contains(rootGroup))
                    {
                        mainGroup.AddSubGroup(rootGroup);
                    }
                }
            }

            if(rootGroup == null)
            {
                rootGroup = mainGroup;
            }

            foreach (var relationSearcher in relationSearcherList)
            {
                this.ParseGroup(relationSearcher, rootGroup, sortColumnList);
            }
        }

        /// <summary>
        /// 递归解析条件组，进行条件语句的拼接
        /// </summary>
        /// <param name="group">需要解析的条件组</param>
        /// <returns>返回拼接后的条件字符串</returns>
        protected string ParseConditionString(ConditionGroup group)
        {
            var conditionCollection = group.ConditionCollection;
            var currCondStr = new StringBuilder("");
            var count = 0;

            foreach (var condition in conditionCollection)
            {
                if (count != 0)
                {
                    currCondStr.Append(this.RelationParse(condition.Relation));
                    currCondStr.Append(" ");
                }

                currCondStr.Append(this.ConditionParse(condition));
                currCondStr.Append(" ");
                count++;
            }

            List<ConditionGroup> sortedSubGroupList = null;

            if (group.SubGroup.Count() > 0)
            {
                sortedSubGroupList = group.SubGroup.OrderBy(g => g.GroupIndex).ToList();

                foreach (var subGroup in sortedSubGroupList)
                {
                    if(count > 0)
                    {
                        currCondStr.Append(this.RelationParse(subGroup.GroupRelation));
                    }

                    currCondStr.Append(this.ParseConditionString(subGroup));
                    count++;
                }
            }

            if(currCondStr.Length > 0)
            {
                currCondStr.Insert(0, " (");
                currCondStr.Append(") ");
            }

            return currCondStr.ToString();
        }

        /// <summary>
        /// 解析排序字段，进行排序语句的拼接
        /// </summary>
        /// <param name="sortColumnList">所有需要排序的字段集合</param>
        protected void ParseSortString(List<SearchColumn> sortColumnList)
        {
            if (sortColumnList.Count == 0)
            {
                return;
            }

            sortColumnList = sortColumnList.OrderBy(col => col.SortIndex).ToList();

            var tmpSortString = new StringBuilder("");
            var sortForDbTypeStr = string.Empty;
            var colName = string.Empty;
            var cout = 0;

            foreach (var column in sortColumnList)
            {
                colName = column.CurrentSearcher.TableName + "." + column.ColumnName;

                if (cout != 0)
                {
                    tmpSortString.Append(", ");
                }

                // 如果字段被赋予了需要转换类型等特殊排序语句，则按照特殊排序语句进行拼接
                if (string.IsNullOrEmpty(column.SortString))
                {
                    tmpSortString.Append(colName);
                }
                else
                {
                    tmpSortString.Append(column.SortString.Replace(SQLPlaceholder.ColName, colName));
                }

                tmpSortString.Append(" ");
                tmpSortString.Append(this.SortOrderParse(column.SortOrder));

                cout++;
            }

            this.SortString = tmpSortString.ToString();
        }

        /// <summary>
        /// 解析查询条件，返回指定数据库的条件语句
        /// </summary>
        /// <param name="condition">需要解析的条件对象</param>
        /// <returns>返回指定的条件字符串</returns>
        protected string ConditionParse(Condition condition)
        {
            // 调用区间条件解析器
            if (condition is BetweenCondition)
            {
                return BetweenParse((BetweenCondition)condition);
            }
            
            // 调用等于条件
            if (condition is EqualCondition)
            {
                return EqualParse((EqualCondition)condition);
            }

            // 调用不等于条件
            if (condition is NotEqualCondition)
            {
                return NotEqualParse((NotEqualCondition)condition);
            }

            // 调用大于等于条件
            if (condition is LargeEqualCondition)
            {
                return LargeEqualParse((LargeEqualCondition)condition);
            }

            // 调用大于条件
            if (condition is LargeThanCondition)
            {
                return LargeThanParse((LargeThanCondition)condition);
            }

            // 调用小于等于条件
            if (condition is LessEqualCondition)
            {
                return LessEqualParse((LessEqualCondition)condition);
            }

            // 调用小于条件
            if (condition is LessThanCondition)
            {
                return LessThanParse((LessThanCondition)condition);
            }

            // 调用包含条件
            if (condition is InCondition)
            {
                return InParse((InCondition)condition);
            }

            // 调用排除条件
            if (condition is NotInCondition)
            {
                return NotInParse((NotInCondition)condition);
            }

            // 调用相似条件
            if (condition is LikeCondition)
            {
                return LikeParse((LikeCondition)condition);
            }

            // 调用相似条件
            if (condition is NotLikeCondition)
            {
                return NotLikeParse((NotLikeCondition)condition);
            }

            // 调用等于条件
            if (condition is SQLEqualCondition)
            {
                return SQLEqualParse((SQLEqualCondition)condition);
            }

            // 调用不等于条件
            if (condition is SQLNotEqualCondition)
            {
                return NotSQLEqualParse((SQLNotEqualCondition)condition);
            }

            // 调用等于条件
            if (condition is SQLInCondition)
            {
                return SQLInParse((SQLInCondition)condition);
            }

            // 调用不等于条件
            if (condition is SQLNotInCondition)
            {
                return NotSQLInParse((SQLNotInCondition)condition);
            }

            // 调用是空条件
            if (condition is IsNullCondition)
            {
                return IsNullParse((IsNullCondition)condition);
            }

            // 调用不是空条件
            if (condition is IsNotNullCondition)
            {
                return IsNotNullParse((IsNotNullCondition)condition);
            }

            return string.Empty;
        }

        #endregion

        #region Protected Virtual Methods

        #region Sort Parse

        /// <summary>
        /// 返回默认的排序关键字
        /// </summary>
        /// <returns>返回排序关键字字符串</returns>
        protected virtual string SortOrderParse(SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.Asc:
                    return " ASC ";
                case SortOrder.Desc:
                    return " DESC ";
                default:
                    return "";
            }
        }

        #endregion

        #region Relation Parse

        /// <summary>
        /// 返回条件连接关键字，默认返回And
        /// </summary>
        /// <returns>返回条件连接关键字字符串，默认返回And</returns>
        protected virtual string RelationParse(ConditionRelation relation)
        {
            switch (relation)
            {
                case ConditionRelation.And:
                    return " AND ";
                case ConditionRelation.Or:
                    return " OR ";
                default:
                    return " AND ";
            }
        }

        #endregion

        #endregion

        #region Protected Abstract Methods

        #region Condition Parse

        /// <summary>
        /// 解析Between条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string BetweenParse(BetweenCondition condition);
        /// <summary>
        /// 解析等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string EqualParse(EqualCondition condition);
        /// <summary>
        /// 解析不等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string NotEqualParse(NotEqualCondition condition);
        /// <summary>
        /// 解析大于等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string LargeEqualParse(LargeEqualCondition condition);
        /// <summary>
        /// 解析大于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string LargeThanParse(LargeThanCondition condition);
        /// <summary>
        /// 解析小于等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string LessEqualParse(LessEqualCondition condition);
        /// <summary>
        /// 解析小于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string LessThanParse(LessThanCondition condition);
        /// <summary>
        /// 解析LIKE条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string LikeParse(LikeCondition condition);
        /// <summary>
        /// 解析NOT LIKE条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string NotLikeParse(NotLikeCondition condition);
        /// <summary>
        /// 解析IN条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string InParse(InCondition condition);
        /// <summary>
        /// 解析NOT IN条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string NotInParse(NotInCondition condition);
        /// <summary>
        /// 解析子查询的IN条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string SQLInParse(SQLInCondition condition);
        /// <summary>
        /// 解析字查询的NOT IN条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string NotSQLInParse(SQLNotInCondition condition);
        /// <summary>
        /// 解析子查询等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string SQLEqualParse(SQLEqualCondition condition);
        /// <summary>
        /// 解析子查询不等于条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string NotSQLEqualParse(SQLNotEqualCondition condition);
        /// <summary>
        /// 解析IS NULL条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string IsNullParse(IsNullCondition condition);
        /// <summary>
        /// 解析IS NOT NULL条件的抽象方法
        /// </summary>
        /// <returns>返回解析后的条件字符串</returns>
        protected abstract string IsNotNullParse(IsNotNullCondition condition);

        #endregion

        #endregion

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryParser()
        {
            this.ConditionString = "${group}";
        }

        #endregion
    }
}
